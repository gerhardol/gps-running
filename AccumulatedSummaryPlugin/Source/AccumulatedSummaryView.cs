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
using ZoneFiveSoftware.Common.Visuals;
using ZoneFiveSoftware.Common.Visuals.Fitness;

using GpsRunningPlugin;
using GpsRunningPlugin.Source;
using GpsRunningPlugin.Properties;
using GpsRunningPlugin.Util;

namespace GpsRunningPlugin.Source
{
    class AccumulatedSummaryView : Form
    {
        private WebBrowser m_browser;
        private IList<IActivity> m_activities;
        private ITheme m_visualTheme;

        public AccumulatedSummaryView()
        {
            m_visualTheme =
#if ST_2_1
                Plugin.GetApplication().VisualTheme;
#else
                Plugin.GetApplication().SystemPreferences.VisualTheme;
#endif

            InitializeComponent();
        }
        public AccumulatedSummaryView(IList<IActivity> activities)
            : this()
        {
            this.Activities = activities;
        }
#if !ST_2_1
        //UniqueRoutes sendto
        public AccumulatedSummaryView(IList<IActivity> activities, IDailyActivityView view)
            : this(activities)
        {
            //m_layer = TrailPointsLayer.Instance((IView)view);
        }
        public AccumulatedSummaryView(IList<IActivity> activities, IActivityReportsView view)
            : this(activities)
        {
            //m_layer = TrailPointsLayer.Instance((IView)view);
        }
#endif
        public IList<IActivity> Activities
        {
            set
            {
                this.m_activities = value;
                StringBuilder builder = new StringBuilder(10000);
                makeReport(builder);
                m_browser.DocumentText = builder.ToString();
                if (m_activities.Count == 1)
                    Text = Resources.AS1;
                else
                    Text = String.Format(Resources.AS2, m_activities.Count);
                Icon = System.Drawing.Icon.FromHandle(Properties.Resources.Image_32_AccumulatedSummary.GetHicon());
                ShowDialog();
            }
        }
        string getRGB(System.Drawing.Color col)
        {
            return String.Format("#{0,2:X}{1,2:X}{2,2:X}",col.R, col.G, col.B);
        }
        private void makeReport(StringBuilder builder)
        {
            String distMetric = UnitUtil.Distance.LabelAbbr;//m_visualTheme.

            builder.Append("<html><head><style type=\"text/css\"> \n"
                + "body { font-size:medium; font-family:sans-serif; background=#DEE0E2;  } \n"
                + "table {border-style: groove; border-color: black; border-width: 1px; width : 100%;} \n"
                + ".tre { background: " + getRGB(m_visualTheme.Window) + " ; } \n"
                + ".tro { background: " + getRGB(m_visualTheme.Border) + " ; } \n"
                + ".tre { border-style:none; background: " + getRGB(m_visualTheme.Window) + " ; } \n"
                + ".tro { border-style:none; background: " + getRGB(m_visualTheme.Border) + " ; } \n"
                + " td{ border-style: solid; border-color: black; border-width: 1px; padding-left: 10px; padding-right: 10px; } \n"
                + ".td_right{text-align: right;} \n"
                + ".td_sub_right{border-top-style:none; border-left-style:none;border-right-style:solid;border-bottom-style:none;text-align: right;} \n"
                + ".td_sub_left{border-top-style:none; border-left-style:solid;border-right-style:none;border-bottom-style:none; text-align: left;} \n"
                + ".td_sub_top_left{border-top-style:solid; border-left-style:solid;border-right-style:none;border-bottom-style:none;text-align: left;} \n"
                + ".td_sub_top_right{border-top-style:solid; border-left-style:none;border-right-style:solid;border-bottom-style:none;text-align: right;} \n"
                + ".td_sub_bottom_left{border-top-style:none; border-left-style:solid;border-right-style:none;border-bottom-style:solid;text-align: left;} \n"
                + ".td_sub_bottom_right{border-top-style:none; border-left-style:none;border-right-style:solid;border-bottom-style:solid;text-align: right;} \n"
            + "</style></head><body><table>\n");

            //Single accumulation
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
            
            //Accumulation per Zones
            getSpeedZones(builder,totalTime);            
            addEntry(builder, StringResources.ClimbZones, getClimbZonesAsArray(totalTime));
            
            
            String ratio = getWorkoutRatio();
            TimeSpan realTotalTimeForWorkout = getRealTotalTimeForWorkout(totalTime);
            string fmt = StringResources.HRZone + UnitUtil.HeartRate.LabelAbbr2 + " (" + "{0}" + ")";
            addEntry(builder, String.Format(fmt, ratio),
                getWorkoutAsArray(realTotalTimeForWorkout));
            
            builder.Append("</table></body></html>");
        }

        

        private TimeSpan getRealTotalTimeForWorkout(TimeSpan totalTime)
        {
            TimeSpan result = totalTime;
            foreach (IActivity activity in m_activities)
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
            foreach (IActivity activity in m_activities)
            {
                if (activity.HeartRatePerMinuteTrack != null)
                    good++;
            }
            return String.Format(StringResources.NoutofM, good, m_activities.Count);
        }

        private string getClimbZones(TimeSpan totalTime)
        {
            return printCategories(Plugin.GetApplication().DisplayOptions.SelectedClimbZone,
                totalTime, false, true, false);
        }


        private string[,] getClimbZonesAsArray(TimeSpan totalTime)
        {
            return printCategoriesAsArrays(Plugin.GetApplication().DisplayOptions.SelectedClimbZone,
                totalTime, false, true, false);
        }


        private void getSpeedZones(StringBuilder builder, TimeSpan totalTime)
        {
            foreach (IZoneCategory speedZone in Plugin.GetApplication().Logbook.SpeedZones)
            {
                addEntry(builder, StringResources.SpeedZone + ": " + speedZone.Name, printCategoriesAsArrays(speedZone, totalTime, true, false, false));
            }
        }

        private string getAverageHeartRate(TimeSpan totalTime)
        {
            double result = 0;
            foreach (IActivity activity in m_activities)
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

        private string[,] printCategoriesAsArrays(IZoneCategory zones, TimeSpan totalTime,
            bool speed, bool climb, bool heart)
        {
            if (m_activities.Count == 0) return null;

            IDictionary<String, TimeSpan> dict = new Dictionary<String, TimeSpan>();
            foreach (IActivity activity in m_activities)
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

            string[,] returnvalues = new string[2, dict.Keys.Count];
            int i = 0;

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
                
                returnvalues[0, i] = name + " (" + timeType + ")";
                returnvalues[1, i] = time + " (" + (dict[name].TotalSeconds / totalTime.TotalSeconds).ToString("P2") + ")";

                i++;
            }
                        
            return returnvalues;                    


        }
        

        private string printCategories(IZoneCategory zones, TimeSpan totalTime,
            bool speed, bool climb, bool heart)
        {
            bool evenStart = m_even;
            IDictionary<String, TimeSpan> dict = new Dictionary<String, TimeSpan>();
            foreach (IActivity activity in m_activities)
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
            m_even = evenStart;
            return builder.ToString();
        }

        private string getWorkout(TimeSpan totalTime)
        {
            return printCategories(Plugin.GetApplication().Logbook.HeartRateZones[0],
                totalTime,false,false,true);            
        }

        private string[,] getWorkoutAsArray(TimeSpan totalTime)
        {
            return printCategoriesAsArrays(Plugin.GetApplication().Logbook.HeartRateZones[0],
                totalTime, false, false, true);
        }


        private string getCalories()
        {
            double result = 0;
            foreach (IActivity activity in m_activities)
            {
                result += activity.TotalCalories;
            }
            return ((int)Math.Round(result)).ToString();
        }

        private double getFastestSpeed()
        {
            double result = double.MinValue;
            foreach (IActivity activity in m_activities)
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
            foreach (IActivity activity in m_activities)
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
            foreach (IActivity activity in m_activities)
            {
                ActivityInfo info = ActivityInfoCache.Instance.GetInfo(activity);
                result = result.Add(info.Time);
            }
            return result;
        }

        private bool m_even = false;

        private void addEntry(StringBuilder builder, string name, string value)
        {  
            builder.Append("\n <tr class=\"tr" + (m_even ? "e" : "o") + "\"><td>" + name + "</td><td class=\"td_right\" colspan=\"2\">" + value + "</td></tr>");    

            m_even = !m_even;
        }

        private void addEntry(StringBuilder builder, string name, string[,] value)
        {  
            bool localeven = m_even;

            int valcount = value.Length / value.Rank;
            for (int i = 0; i < valcount; i++)
            {
                builder.Append("\n <tr class=\"tr" + (localeven? "e" : "o") + "\">");
                if (i == 0)
                {
                    builder.Append("<td rowspan=\"" + valcount + "\">" + name + "</td>");
                }
                builder.Append("<td class=\"" + ((i==0)?"td_sub_top_left":(i==valcount?"td_sub_bottom_left":"td_sub_left"))+"\">" + value[0, i] + "</td>");
                builder.Append("<td class=\"" + ((i == 0) ? "td_sub_top_right" : (i == valcount ? "td_sub_bottom_right" : "td_sub_right")) + "\">" + value[1, i] + "</td></tr>");

                localeven = !localeven;
            }


            m_even = !m_even;





        }

        private double getTotalDistance()
        {
            double result = 0;
            foreach (IActivity activity in m_activities)
            {
                ActivityInfo info = ActivityInfoCache.Instance.GetInfo(activity);
                result += info.DistanceMeters;
            }
            return result;
        }

        private void InitializeComponent()
        {
            this.m_browser = new System.Windows.Forms.WebBrowser();
            this.SuspendLayout();
            // 
            // browser
            // 
            this.m_browser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_browser.Location = new System.Drawing.Point(0, 0);
            this.m_browser.MinimumSize = new System.Drawing.Size(20, 20);
            this.m_browser.Name = "browser";
            this.m_browser.Size = new System.Drawing.Size(440, 349);
            this.m_browser.TabIndex = 0;
            // 
            // AccumulatedSummaryView
            // 
            this.ClientSize = new System.Drawing.Size(440, 349);
            this.Controls.Add(this.m_browser);
            this.Name = "AccumulatedSummaryView";
            this.ResumeLayout(false);

        }
    }
}
