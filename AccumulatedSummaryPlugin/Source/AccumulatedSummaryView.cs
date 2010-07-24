/*
Copyright (C) 2007, 2008 Kristian Bisgaard Lassen
Copyright (C) 2010 Kristian Helkjaer Lassen

This library is free software; you can redistribute it and/or
modify it under the terms of the GNU Lesser General Public
License as published by the Free Software Foundation; either
version 3 of the License, or (at your option) any later version.

This library is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
Lesser General Public License for more details.

You should have received a copy of the GNU Lesser General Public
License along with this library. If not, see <http://www.gnu.org/licenses/>.
 */

using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using ZoneFiveSoftware.Common.Data.Fitness;
using ZoneFiveSoftware.Common.Data.Measurement;

using SportTracksAccumulatedSummaryPlugin;
using SportTracksAccumulatedSummaryPlugin.Source;
using ZoneFiveSoftware.Common.Visuals;
using SportTracksAccumulatedSummaryPlugin.Properties;
using SportTracksAccumulatedSummaryPlugin.Util;

namespace SportTracksAccumulatedSummaryPlugin.Source
{
    class AccumulatedSummaryView : Form
    {
        private WebBrowser browser;
        private IList<IActivity> activities;
        private ITheme m_visualTheme;

        public AccumulatedSummaryView(IList<IActivity> activities)
        {
            m_visualTheme =
#if ST_2_1
                Plugin.GetApplication().VisualTheme;
#else
                Plugin.GetApplication().SystemPreferences.VisualTheme;
#endif

            InitializeComponent();
            this.activities = activities;
            StringBuilder builder = new StringBuilder(10000);
            makeReport(builder);
            browser.DocumentText = builder.ToString();
            if (activities.Count == 1)
                Text = Resources.AS1;
            else
                Text = String.Format(Resources.AS2,activities.Count);
            Icon = System.Drawing.Icon.FromHandle(Properties.Resources.Image_32_AccumulatedSummary.GetHicon());
            ShowDialog();
        }
        string getRGB(System.Drawing.Color col)
        {
            return String.Format("#{0,2:X}{1,2:X}{2,2:X}",col.R, col.G, col.B);
        }
        private void makeReport(StringBuilder builder)
        {
            String distMetric = UnitUtil.Distance.LabelAbbr;//m_visualTheme.
            builder.Append("<html><body style=\"background=" + getRGB(m_visualTheme.Control)
                + "\"><table border=\"1\" width=\"100%\">");
            double totalDistance = getTotalDistance();
            addEntry(builder, UnitUtil.Distance.LabelAxis, UnitUtil.Distance.ToString(totalDistance));
            TimeSpan totalTime = getTotalTime();
            showTotalTime(builder, totalTime);
            double averageSpeed = totalDistance / totalTime.TotalSeconds;
            double fastestSpeed = getFastestSpeed();
            addEntry(builder, UnitUtil.Pace.LabelAxis, UnitUtil.Pace.ToString(averageSpeed));
            addEntry(builder, CommonResources.Text.LabelFastestPace + UnitUtil.Pace.LabelAbbr2,
                UnitUtil.Pace.ToString(fastestSpeed));
            addEntry(builder, UnitUtil.Speed.LabelAxis,
                UnitUtil.Speed.ToString(averageSpeed));
            addEntry(builder, CommonResources.Text.LabelFastestSpeed + UnitUtil.Speed.LabelAbbr2,
                UnitUtil.Speed.ToString(fastestSpeed));
            addEntry(builder, StringResources.ClimbSummary + UnitUtil.Elevation.LabelAbbr2, getTotalUpsAndDowns());
            addEntry(builder, UnitUtil.Energy.LabelAxis, getCalories());
            addEntry(builder, CommonResources.Text.LabelAvgHR + UnitUtil.HeartRate.LabelAbbr2, getAverageHeartRate(totalTime));
            getSpeedZones(builder,totalTime);
            addEntry(builder, StringResources.ClimbZones, getClimbZones(totalTime));
            String ratio = getWorkoutRatio();
            TimeSpan realTotalTimeForWorkout = getRealTotalTimeForWorkout(totalTime);
            string fmt = StringResources.HRZone + UnitUtil.HeartRate.LabelAbbr2 + " (" + "{0}" + ")";
            addEntry(builder, String.Format(fmt, ratio),
                getWorkout(realTotalTimeForWorkout));
            builder.Append("</table></body></html>");
        }

        private TimeSpan getRealTotalTimeForWorkout(TimeSpan totalTime)
        {
            TimeSpan result = totalTime;
            foreach (IActivity activity in activities)
            {
                if (activity.HeartRatePerMinuteTrack == null)
                {
                    ActivityInfo info = ActivityInfoCache.Instance.GetInfo(activity);
                    if (info.Time.TotalSeconds == 0)
                        result = result.Subtract(activity.TotalTimeEntered);
                    else
                        result = result.Subtract(info.Time);
                }
            }
            return result;
        }

        private string getWorkoutRatio()
        {
            int good = 0;
            foreach (IActivity activity in activities)
            {
                if (activity.HeartRatePerMinuteTrack != null)
                    good++;
            }
            return String.Format(StringResources.NoutofM, good, activities.Count);
        }

        private string getClimbZones(TimeSpan totalTime)
        {
            return printCategories(Plugin.GetApplication().DisplayOptions.SelectedClimbZone,
                totalTime, false, true, false);
        }

        private void getSpeedZones(StringBuilder builder, TimeSpan totalTime)
        {
            foreach (IZoneCategory speedZone in Plugin.GetApplication().Logbook.SpeedZones)
            {
                addEntry(builder, StringResources.SpeedZone + ": " + speedZone.Name,
                    printCategories(speedZone, totalTime, true, false, false));
            }
        }

        private string getAverageHeartRate(TimeSpan totalTime)
        {
            double result = 0;
            foreach (IActivity activity in activities)
            {
                ActivityInfo info = ActivityInfoCache.Instance.GetInfo(activity);
                if (activity.HeartRatePerMinuteTrack != null)
                {
                    result += info.AverageHeartRate 
                        * (info.Time.TotalSeconds / totalTime.TotalSeconds);
                }
                else if (activity.AverageHeartRatePerMinuteEntered > 0)
                {
                    double time = info.Time.TotalSeconds;
                    if (time == 0)
                        time = activity.TotalTimeEntered.TotalSeconds;
                    result += activity.AverageHeartRatePerMinuteEntered 
                        * (info.Time.TotalSeconds / totalTime.TotalSeconds);
                }
            }
            return UnitUtil.HeartRate.ToString(result);
        }

        private string printCategories(IZoneCategory zones, TimeSpan totalTime,
            bool speed, bool climb, bool heart)
        {
            bool evenStart = even;
            IDictionary<String, TimeSpan> dict = new Dictionary<String, TimeSpan>();
            foreach (IActivity activity in activities)
            {
                ActivityInfo info = ActivityInfoCache.Instance.GetInfo(activity);
                ZoneCategoryInfo zinfos = null;
                bool cont = true;
                if (speed) zinfos = info.SpeedZoneInfo(zones);
                else if (climb) zinfos = info.ClimbZoneInfo(zones);
                else if (heart)
                {
                    if (activity.HeartRatePerMinuteTrack == null)
                        cont = false;
                    else
                        zinfos = info.HeartRateZoneInfo(zones);
                }
                if (cont)
                {
                    foreach (ZoneInfo zinfo in zinfos.Zones)
                    {
                        TimeSpan v = zinfo.TotalTime;
                        if (!dict.ContainsKey(zinfo.Name))
                        {
                            dict.Add(zinfo.Name, v);
                        }
                        else
                        {
                            dict[zinfo.Name] = dict[zinfo.Name].Add(v);
                        }
                    }
                }
            }
            StringBuilder builder = new StringBuilder();
            builder.Append("<table width=\"100%\">");
            foreach (String name in dict.Keys)
            {
                String timeType = StringResources.SecondsFormat;
                String time = String.Format("{0:00}", dict[name].Seconds);
                if (totalTime.TotalMinutes > 1)
                {
                    timeType = StringResources.MinutesFormat + ":" + timeType;
                    time = String.Format("{0:00}", dict[name].Minutes) + ":" + time;
                }
                if (totalTime.TotalHours > 1)
                {
                    timeType = StringResources.HoursFormat + ":" + timeType;
                    time = String.Format("{0:00}", dict[name].Hours) + ":" + time;
                }
                if (totalTime.TotalDays > 1)
                {
                    timeType = StringResources.DaysFormat + ":" + timeType;
                    time = String.Format("{0:00}", dict[name].Days) + ":" + time;
                }
                addEntry(builder, name + " (" + timeType + ")", time +
                    " (" + (dict[name].TotalSeconds / totalTime.TotalSeconds).ToString("P2") + ")");
            }
            builder.Append("</table>");
            even = evenStart;
            return builder.ToString();
        }

        private string getWorkout(TimeSpan totalTime)
        {
            return printCategories(Plugin.GetApplication().Logbook.HeartRateZones[0],
                totalTime,false,false,true);            
        }

        private string getCalories()
        {
            double result = 0;
            foreach (IActivity activity in activities)
            {
                result += activity.TotalCalories;
            }
            return ((int)Math.Round(result)).ToString();
        }

        private double getFastestSpeed()
        {
            double result = double.MinValue;
            foreach (IActivity activity in activities)
            {
                ActivityInfo info = ActivityInfoCache.Instance.GetInfo(activity);
                double tmp = info.FastestSpeedMetersPerSecond;
                if (tmp > result)
                    result = tmp;
            }
            return result;
        }

        private string getTotalUpsAndDowns()
        {
            double ups = 0, downs = 0;
            foreach (IActivity activity in activities)
            {
                ActivityInfo info = ActivityInfoCache.Instance.GetInfo(activity);
                IZoneCategory cat = Plugin.GetApplication().DisplayOptions.SelectedClimbZone;
                ups += info.TotalAscendingMeters(cat);
                downs += info.TotalDescendingMeters(cat);
            }
            return "+" + UnitUtil.Elevation.ToString(ups) + "/" + UnitUtil.Elevation.ToString(downs);
        }

        private void showTotalTime(StringBuilder builder, TimeSpan totalTime)
        {
            String timeType = StringResources.SecondsFormat;
            String time = String.Format("{0:00}", totalTime.Seconds);
            if (totalTime.TotalMinutes > 1)
            {
                timeType = StringResources.MinutesFormat + ":" + timeType;
                time = String.Format("{0:00}", totalTime.Minutes) + ":" + time;
            }
            if (totalTime.TotalHours > 1)
            {
                timeType = StringResources.HoursFormat + ":" + timeType;
                time = String.Format("{0:00}", totalTime.Hours) + ":" + time;
            }
            if (totalTime.TotalDays > 1)
            {
                timeType = StringResources.DaysFormat + ":" + timeType;
                time = String.Format("{0:00}", totalTime.Days) + ":" + time;
            }
            addEntry(builder, CommonResources.Text.LabelTime+ " (" + timeType + ")", time);
        }

        private TimeSpan getTotalTime()
        {
            TimeSpan result = new TimeSpan();
            foreach (IActivity activity in activities)
            {
                ActivityInfo info = ActivityInfoCache.Instance.GetInfo(activity);
                result = result.Add(info.Time);
            }
            return result;
        }

        private bool even = false;

        private void addEntry(StringBuilder builder, string name, string value)
        {
            builder.Append("<tr style=\"background:" + (even ? getRGB(m_visualTheme.Window) : getRGB(m_visualTheme.Border)) +
                "\"><td>" + name + "</td><td align=\"right\">"
                + value + "</td></tr>");
            even = !even;
        }

        private double getTotalDistance()
        {
            double result = 0;
            foreach (IActivity activity in activities)
            {
                ActivityInfo info = ActivityInfoCache.Instance.GetInfo(activity);
                result += info.DistanceMeters;
            }
            return result;
        }

        private void InitializeComponent()
        {
            this.browser = new System.Windows.Forms.WebBrowser();
            this.SuspendLayout();
            // 
            // browser
            // 
            this.browser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.browser.Location = new System.Drawing.Point(0, 0);
            this.browser.MinimumSize = new System.Drawing.Size(20, 20);
            this.browser.Name = "browser";
            this.browser.Size = new System.Drawing.Size(440, 349);
            this.browser.TabIndex = 0;
            // 
            // AccumulatedSummaryView
            // 
            this.ClientSize = new System.Drawing.Size(440, 349);
            this.Controls.Add(this.browser);
            this.Name = "AccumulatedSummaryView";
            this.ResumeLayout(false);

        }
    }
}
