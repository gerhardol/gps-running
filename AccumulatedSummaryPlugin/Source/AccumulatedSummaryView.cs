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
using SportTracksAccumulatedSummaryPlugin;
using SportTracksAccumulatedSummaryPlugin.Source;
using ZoneFiveSoftware.Common.Visuals;
using SportTracksAccumulatedSummaryPlugin.Properties;

namespace SportTracksAccumulatedSummaryPlugin.Source
{
    class AccumulatedSummaryView : Form
    {
        private WebBrowser browser;
        private IList<IActivity> activities;

        public AccumulatedSummaryView(IList<IActivity> activities)
        {
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

        private void makeReport(StringBuilder builder)
        {
            String distMetric = Settings.DistanceUnitShort;
            String elevMetric = Settings.ElevationUnitShort;
            builder.Append("<html><body><table border=\"1\" width=\"100%\">");
            double totalDistance = getTotalDistance();
            addEntry(builder, Resources.Distance+" (" + distMetric + ")", Settings.present(totalDistance));
            TimeSpan totalTime = getTotalTime();
            showTotalTime(builder, totalTime);
            addEntry(builder, String.Format(Resources.Pace,distMetric), getTotalPace(totalDistance, totalTime));
            double fastestsSpeed = Settings.convertFromDistance(getFastestsSpeed());
            addEntry(builder, Resources.FastestsPace+" (min/" + distMetric + ")",
                new TimeSpan(0,0,(int)Math.Round(1/fastestsSpeed)).ToString().Substring(3));
            addEntry(builder, String.Format(Resources.Speed,distMetric), getTotalSpeed(totalDistance, totalTime));
            addEntry(builder, String.Format(Resources.FastestsSpeed,distMetric),
                Settings.present(60 * 60 * fastestsSpeed));
            addEntry(builder, String.Format(Resources.Climb,elevMetric), getTotalUpsAndDowns());
            addEntry(builder, Resources.Calories, getCalories());
            addEntry(builder, Resources.AverageHR, getAverageHeartRate(totalTime));
            getSpeedZones(builder,totalTime);
            addEntry(builder, Resources.ClimbZones, getClimbZones(totalTime));
            String ratio = getWorkoutRatio();
            TimeSpan realTotalTimeForWorkout = getRealTotalTimeForWorkout(totalTime);
            addEntry(builder, String.Format(Resources.HeartRateZones,ratio),
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
            return String.Format(Resources.NoutofM, good, activities.Count);
        }

        private string getClimbZones(TimeSpan totalTime)
        {
            return printCategories(Plugin.GetApplication().Logbook.ClimbZones[0],
                totalTime, false, true, false);
        }

        private void getSpeedZones(StringBuilder builder, TimeSpan totalTime)
        {
            foreach (IZoneCategory speedZone in Plugin.GetApplication().Logbook.SpeedZones)
            {
                addEntry(builder, Resources.SpeedZones + ": " + speedZone.Name,
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
            return Settings.present(result);
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
                String timeType = Resources.Seconds;
                String time = String.Format("{0:00}", dict[name].Seconds);
                if (totalTime.TotalMinutes > 1)
                {
                    timeType = Resources.Minutes + timeType;
                    time = String.Format("{0:00}", dict[name].Minutes) + ":" + time;
                }
                if (totalTime.TotalHours > 1)
                {
                    timeType = Resources.Hours + timeType;
                    time = String.Format("{0:00}", dict[name].Hours) + ":" + time;
                }
                if (totalTime.TotalDays > 1)
                {
                    timeType = Resources.Days + timeType;
                    time = String.Format("{0:00}", dict[name].Days) + ":" + time;
                }
                addEntry(builder, name + " (" + timeType + ")", time +
                    " (" +
                    Settings.present(100 *
                    dict[name].TotalSeconds / totalTime.TotalSeconds) +
                    "%)");
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

        private double getFastestsSpeed()
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
                IList<IZoneCategory> climbZones = Plugin.GetApplication().Logbook.ClimbZones;
                ups += info.TotalAscendingMeters(climbZones[0]);
                downs += info.TotalDescendingMeters(climbZones[0]);
            }
            return "+"+Settings.present(ups) + "/" + Settings.present(downs);
        }

        private string getTotalSpeed(double totalDistance, TimeSpan totalTime)
        {
            return Settings.present(totalDistance / totalTime.TotalHours);
        }

        private void showTotalTime(StringBuilder builder, TimeSpan totalTime)
        {
            String timeType = Resources.Seconds;
            String time = String.Format("{0:00}", totalTime.Seconds);
            if (totalTime.TotalMinutes > 1)
            {
                timeType = Resources.Minutes + timeType;
                time = String.Format("{0:00}", totalTime.Minutes) + ":" + time;
            }
            if (totalTime.TotalHours > 1)
            {
                timeType = Resources.Hours + timeType;
                time = String.Format("{0:00}", totalTime.Hours) + ":" + time;
            }
            if (totalTime.TotalDays > 1)
            {
                timeType = Resources.Days + timeType;
                time = String.Format("{0:00}", totalTime.Days) + ":" + time;
            }
            addEntry(builder, String.Format(Resources.Time,timeType), time);
        }

        private string getTotalPace(double totalDistance, TimeSpan totalTime)
        {
            double pace = totalTime.TotalMinutes / totalDistance;
            return new TimeSpan(0, (int)Math.Floor(pace),
                (int)(60 * (pace - Math.Floor(pace)))).ToString().Substring(3);
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
            builder.Append("<tr style=\"background:" + (even ? "#CFECEC" : "#FFF380") +
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
            return Settings.convertFromDistance(result);
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
