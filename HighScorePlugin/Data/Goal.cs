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
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using ZoneFiveSoftware.Common.Visuals;
using ZoneFiveSoftware.Common.Visuals.Chart;
using GpsRunningPlugin.Properties;
using GpsRunningPlugin.Util;

namespace GpsRunningPlugin.Source
{
    public abstract class Goal
    {
        public Goal(bool upperBound, GoalParameter domain, GoalParameter image)
        {
            this.UpperBound = upperBound;
            this.Domain = domain;
            this.Image = image;
        }

        readonly public bool UpperBound;

        readonly public GoalParameter Domain, Image;

        public abstract String ImageToString(string speedUnit);

        public static bool IsZoneGoal(GoalParameter gp)
        {
            return (gp != GoalParameter.Time && gp != GoalParameter.Distance && gp != GoalParameter.Elevation);
        }

        public static void generateGoals(GoalParameter domain, GoalParameter image, bool upperBound, IList<Goal> goals)
        {
            switch (image)
            {
                case GoalParameter.Distance:
                    foreach (double distance in Settings.distances.Keys)
                    {
                        goals.Add(new PointGoal(distance, upperBound,
                                    domain, GoalParameter.Distance));
                    }
                    break;
                case GoalParameter.Time:
                    foreach (int time in Settings.times.Keys)
                    {
                        goals.Add(new PointGoal(time, upperBound,
                                    domain, GoalParameter.Time));
                    }
                    break;
                case GoalParameter.Elevation:
                    foreach (double elevation in Settings.elevations.Keys)
                    {
                        goals.Add(new PointGoal(elevation, upperBound,
                                    domain, GoalParameter.Elevation));
                    }
                    break;
                case GoalParameter.PulseZone:
                    foreach (double min in Settings.pulseZones.Keys)
                    {
                        foreach (double max in Settings.pulseZones[min].Keys)
                        {
                            IList<IList<double>> intervals = new List<IList<double>>();
                            IList<double> interval = new List<double>();
                            interval.Add(min);
                            interval.Add(max);
                            intervals.Add(interval);
                            goals.Add(new IntervalsGoal(intervals, upperBound,
                                        domain, GoalParameter.PulseZone));
                        }
                    }
                    break;
                //case GoalParameter.SpeedZone:
                //    foreach (double min in Settings.speedZones.Keys)
                //    {
                //        foreach (double max in Settings.speedZones[min].Keys)
                //        {
                //            IList<IList<double>> intervals = new List<IList<double>>();
                //            IList<double> interval = new List<double>();
                //            interval.Add(min);
                //            interval.Add(max);
                //            intervals.Add(interval);
                //            goals.Add(new IntervalsGoal(intervals, upperBound,
                //                        domain, GoalParameter.SpeedZone));
                //        }
                //    }
                //    break;
                case GoalParameter.PulseZoneSpeedZone:
                    foreach (double minPulse in Settings.pulseZones.Keys)
                    {
                        foreach (double maxPulse in Settings.pulseZones[minPulse].Keys)
                        {
                            foreach (double minSpeed in Settings.speedZones.Keys)
                            {
                                foreach (double maxSpeed in Settings.speedZones[minSpeed].Keys)
                                {
                                    IList<IList<double>> intervals = new List<IList<double>>();
                                    IList<double> interval = new List<double>();
                                    interval.Add(minPulse);
                                    interval.Add(maxPulse);
                                    intervals.Add(interval);
                                    interval = new List<double>();
                                    interval.Add(minSpeed);
                                    interval.Add(maxSpeed);
                                    intervals.Add(interval);
                                    goals.Add(new IntervalsGoal(intervals, upperBound,
                                        domain, GoalParameter.PulseZoneSpeedZone));
                                }
                            }
                        }
                    }
                    break;
            }
        }

        public static IList<Goal> generateSettingsGoals()
        {
            IList<Goal> goals = new List<Goal>();
            generateGoals(Settings.Domain, Settings.Image, Settings.UpperBound, goals);
            return goals;
        }

        public static IList<Goal> generateAllGoals()
        {
            IList<Goal> goals = new List<Goal>();
            foreach (GoalParameter domain in Enum.GetValues(typeof(GoalParameter)))
            {
                foreach (GoalParameter image in Enum.GetValues(typeof(GoalParameter)))
                {
                    if (image != domain &&
                        !Goal.IsZoneGoal(domain))
                    {
                        Goal.generateGoals(domain, image, false, goals);
                        Goal.generateGoals(domain, image, true, goals);
                    }
                }
            }
            return goals;
        }

        public static String translateParameter(String str)
        {
            if (str.Equals(CommonResources.Text.LabelDistance)) return "distance";
            if (str.Equals(CommonResources.Text.LabelElevation)) return "elevation";
            if (str.Equals(CommonResources.Text.LabelTime)) return "time";
            if (str.Equals(StringResources.HRZone)) return "pulsezone";
            if (str.Equals(StringResources.SpeedZone)) return "speedzone";
            if (str.Equals(Resources.HRAndSpeedZones)) return "pulsezonespeedzone";
            return null;
        }

        public static string translateToLanguage(GoalParameter goalParameter)
        {
            if (goalParameter.Equals(GoalParameter.PulseZone))
                return StringResources.HRZone;
            if (goalParameter.Equals(GoalParameter.SpeedZone))
                return StringResources.SpeedZone;
            if (goalParameter.Equals(GoalParameter.PulseZoneSpeedZone))
                return Resources.HRAndSpeedZones;
            if (goalParameter.Equals(GoalParameter.Distance))
                return CommonResources.Text.LabelDistance;
            if (goalParameter.Equals(GoalParameter.Elevation))
                return CommonResources.Text.LabelElevation;
            if (goalParameter.Equals(GoalParameter.Time))
                return CommonResources.Text.LabelTime;
            return null;
        }

        public static String getGoalParameterLabel(GoalParameter gp, String speedUnit)
        {
            switch (gp)
            {
                case GoalParameter.Distance:
                    return UnitUtil.Distance.LabelAxis;
                case GoalParameter.Time:
                    return UnitUtil.Time.LabelAxis;
                case GoalParameter.Elevation:
                    return UnitUtil.Elevation.LabelAxis;
                case GoalParameter.PulseZone:
                    return UnitUtil.HeartRate.LabelAxis;
                case GoalParameter.SpeedZone:
                    if (speedUnit.Equals(CommonResources.Text.LabelPace)) return UnitUtil.Pace.LabelAxis;
                    return UnitUtil.Speed.LabelAxis;
                case GoalParameter.PulseZoneSpeedZone:
                    return UnitUtil.HeartRate.LabelAxis;
            }
            throw new Exception();
        }

        public static void setAxisType(IAxis axis, GoalParameter goal)
        {
            switch (goal)
            {
                case GoalParameter.Time:
                    axis.Formatter = new Formatter.SecondsToTime(); 
                    return;
                case GoalParameter.Distance:
                    axis.Formatter = new Formatter.General(UnitUtil.Distance.DefaultDecimalPrecision); 
                    return;
                case GoalParameter.Elevation:
                    axis.Formatter = new Formatter.General(UnitUtil.Elevation.DefaultDecimalPrecision); 
                    return;
                //case GoalParameter.SpeedZone:
                //    ArrayList categories = new ArrayList();
                //    ArrayList keys = new ArrayList();
                //    int index = 0;
                //    foreach (double from in Settings.speedZones.Keys)
                //    {
                //        foreach (double to in Settings.speedZones[from].Keys)
                //        {
                //            categories.Add(UnitUtil.Speed.ToString(from) + "-" + UnitUtil.Speed.ToString(to));
                //            keys.Add(index++);
                //        }
                //    }
                //    axis.Formatter = new Formatter.Category(categories, keys);
                //    return;
                default:
                    axis.Formatter = new Formatter.General(); 
                    return;
            }
        }

        internal string getDomainString()
        {
            String str;
            switch (Domain)
            {
                case GoalParameter.Distance:
                    if (UpperBound) str = Resources.LongestDistanceTraveled;
                    else str = Resources.ShortestDistanceTraveled;
                    break;
                case GoalParameter.Time:
                    if (UpperBound) str = Resources.LongestTimeSpent;
                    else str = Resources.ShortestTimeSpent;
                    break;
                case GoalParameter.Elevation:
                    if (UpperBound) str = Resources.BiggestElevationDifference;
                    else str = Resources.SmallestElevationDifference;
                    break;
                default:
                    throw new Exception();
            }
            return str;
        }

        internal abstract string getImageString(String speedUnit);

        public String ToString(String speedUnit)
        {
            String str = this.getDomainString();
            str += " ";
            str += this.getImageString(speedUnit);
            return str;
        }

    }

    /**************************************************/
    public class PointGoal : Goal
    {
        public PointGoal(double goal, bool upperBound, GoalParameter domain, GoalParameter image)
            : base(upperBound, domain, image)
        {
            this.Value = goal;
        }

        readonly public double Value;

        internal override string getImageString(String speedUnit)
        {
            String str;
            switch (Image)
            {
                case GoalParameter.Distance:
                    str = Resources.OnADistanceOf + " " + UnitUtil.Distance.ToString(Value, "u");
                    break;
                case GoalParameter.Time:
                    str = Resources.OnATimeOf + " " + UnitUtil.Time.ToString(Value, "u");
                    break;
                case GoalParameter.Elevation:
                    str = Resources.OnAnElevationOf + " " + UnitUtil.Elevation.ToString(Value, "u");
                    break;
                default:
                    throw new Exception();
            }
            return str;
        }

        public override String ImageToString(string speedUnit)
        {
            switch (Image)
            {
                case GoalParameter.Distance: 
                    return UnitUtil.Distance.ToString(Value, "u");
                case GoalParameter.Time: 
                    return UnitUtil.Time.ToString(Value);
                case GoalParameter.Elevation:
                    return UnitUtil.Elevation.ToString(Value, "u");;
            }
            return null;
        }
    }

    public class IntervalsGoal : Goal
    {
        public IntervalsGoal(IList<IList<double>> intervals, bool upperBound, 
            GoalParameter domain, GoalParameter image)
            : base(upperBound, domain, image)
        {
            this.Intervals = intervals;
        }

        readonly public IList<IList<double>> Intervals;

        private string getInfo(string speedUnit, double min, double max, int format)
        {
            string str;
            string from;
            string to;

            if (speedUnit.Equals(CommonResources.Text.LabelPace))
            {
                from = UnitUtil.Pace.ToString(max);
                to = UnitUtil.Pace.ToString(min);
                str = Resources.WithAPaceBetween;
            }
            else
            {
                from = UnitUtil.Speed.ToString(min);
                to = UnitUtil.Speed.ToString(max);
                str = Resources.WithASpeedBetween;
             }
            if (0 == format)
            {
                str += " " + from + " " + UnitUtil.Pace.LabelAbbr + 
                    " " + StringResources.And.ToLower() + " " +
                    to + " " + UnitUtil.Pace.LabelAbbr;
            } else {
                str = from + "\n-\n" + to;
            }
            return str;
        }

        internal override string getImageString(String speedUnit)
        {
            String str;
            switch (Image)
            {
                case GoalParameter.PulseZone:
                    str = Resources.WithAHRBetween;
                    str += " " + Intervals[0][0] + " - " + Intervals[0][1];
                    break;
                case GoalParameter.SpeedZone:
                    str = getInfo(speedUnit, Intervals[0][0], Intervals[0][1], 0);
                    break;
                case GoalParameter.PulseZoneSpeedZone:
                    str = Resources.WithAHRBetween;
                    str += " " + Intervals[0][0] + " - " + Intervals[0][1];
                    str += " " + StringResources.And.ToLower() + " ";
                    str += getInfo(speedUnit, Intervals[1][0], Intervals[1][1], 0);
                    break;
                default:
                    throw new Exception();
            }
            return str;
        }

        public override String ImageToString(string speedUnit)
        {
            String str = null;
            switch (Image)
            {
                case GoalParameter.PulseZone:
                    str = Intervals[0][0] + "\n-\n" + Intervals[0][1];
                    break;
                case GoalParameter.SpeedZone:
                    str = getInfo(speedUnit, Intervals[0][0], Intervals[0][1], 1);
                    break;
                case GoalParameter.PulseZoneSpeedZone:
                    String res = Intervals[0][0] + "\n" + Intervals[0][1]+"\n/\n";
                    str = getInfo(speedUnit, Intervals[1][0], Intervals[1][1], 1);
                    break;
            }
            return str;
        }
    }

    public enum GoalParameter
    {
        //CadenceZone is not implemented
        //SpeedZone is implemented, but generation disabled (and untested)
        Time, Distance, Elevation, PulseZone, PulseZoneSpeedZone, SpeedZone, CadenceZone
    }
}

