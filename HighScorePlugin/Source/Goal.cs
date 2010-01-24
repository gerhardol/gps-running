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
using System.Diagnostics;
using SportTracksHighScorePlugin.Properties;

namespace SportTracksHighScorePlugin.Source
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

        public abstract String ToString(String speedUnit);
        
        public static String present(double d)
        {
            return string.Format("{0:0.000}", d);
        }

        public abstract String ImageToString(string speedUnit);
    }

    public class PointGoal : Goal
    {
        public PointGoal(double goal, bool upperBound, GoalParameter domain, GoalParameter image)
            :
            base(upperBound, domain, image)
        {
            this.Value = goal;
        }

        readonly public double Value;        

        public override String ToString(String speedUnit)
        {
            String metric = Settings.DistanceUnit;
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
            str += " ";
            switch (Image)
            {
                case GoalParameter.Distance:
                    str += Resources.OnADistanceOf + " "+present(HighScore.convertFromDistance(Value)) + " " + metric;
                    break;
                case GoalParameter.Time:
                    TimeSpan time = new TimeSpan(0, 0, (int) Math.Round(Value));
                    if (time.Hours == 0 && time.Minutes == 0)
                        str += String.Format(Resources.OnATimeOfSeconds,time.ToString().Substring(6));
                    else if (time.Hours == 0)
                        str += String.Format(Resources.OnATimeOfMinutes,time.ToString().Substring(3));
                    else
                        str += String.Format(Resources.OnATimeOfHours,time.ToString());
                    break;
                case GoalParameter.Elevation:
                    str += Resources.OnAnElevationOf + present(HighScore.convertFromElevation(Value))
                        + " " + Settings.ElevationUnit;
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
                case GoalParameter.Distance: return present(HighScore.convertFromElevation(Value)) 
                    + " " + Settings.ElevationUnit;
                case GoalParameter.Time:
                    TimeSpan time = new TimeSpan(0, 0, (int)Math.Round(Value));
                    if (time.Hours == 0 && time.Minutes == 0)
                        return String.Format(Resources.SomeSeconds,time.ToString().Substring(6));
                    else if (time.Hours == 0)
                        return String.Format(Resources.SomeMinutes,time.ToString().Substring(3));
                    else
                        return String.Format(Resources.SomeHours,time.ToString());
                case GoalParameter.Elevation:
                    return present(HighScore.convertFromElevation(Value))
                        + " " + Settings.ElevationUnit;
            }
            return null;
        }
    }

    public class IntervalsGoal : Goal
    {
        public IntervalsGoal(IList<IList<double>> intervals, bool upperBound, 
            GoalParameter domain, GoalParameter image)
            :
            base(upperBound, domain, image)
        {
            this.Intervals = intervals;
        }

        readonly public IList<IList<double>> Intervals;

        public override string ToString(String speedUnit)
        {
            String metric = Settings.DistanceUnit;
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
            str += " ";
            switch (Image)
            {
                case GoalParameter.PulseZone:
                    str += String.Format(Resources.WithAHRBetween,Intervals[0][0],Intervals[0][1]);
                    break;
                case GoalParameter.SpeedZone:
                    if (speedUnit.Equals("pace"))
                    {
                        TimeSpan from = new TimeSpan(0, 0, (int)Math.Round(1 / HighScore.convertFromDistance(Intervals[0][1])));
                        TimeSpan to = new TimeSpan(0, 0, (int)Math.Round(1 / HighScore.convertFromDistance(Intervals[0][0])));
                        str += String.Format(Resources.WithAPaceBetween,from.ToString().Substring(3),metric,
                            to.ToString().Substring(3),metric);
                    }
                    else
                    {
                        str += String.Format(Resources.WithASpeedBetween,present(HighScore.convertFromDistance(Intervals[0][0]) * 3600),
                            metric,present(HighScore.convertFromDistance(Intervals[0][1]) * 3600),metric);
                    }
                    break;
                case GoalParameter.PulseZoneSpeedZone:
                    str += String.Format(Resources.WithAHRBetween, Intervals[0][0], Intervals[0][1]);
                    str += " "+Resources.LowerCaseAnd;
                    if (speedUnit.Equals("pace"))
                    {
                        TimeSpan from = new TimeSpan(0, 0, (int)Math.Round(1 / HighScore.convertFromDistance(Intervals[1][1])));
                        TimeSpan to = new TimeSpan(0, 0, (int)Math.Round(1 / HighScore.convertFromDistance(Intervals[1][0])));
                        str += String.Format(Resources.WithAPaceBetween, from.ToString().Substring(3), metric,
                            to.ToString().Substring(3), metric);
                    }
                    else
                    {
                        str += String.Format(Resources.WithASpeedBetween, present(HighScore.convertFromDistance(Intervals[1][0]) * 3600),
                            metric, present(HighScore.convertFromDistance(Intervals[1][1]) * 3600), metric);
                    }
                    break;
            }
            return str;
        }

        public override String ImageToString(string speedUnit)
        {
            String metric = Settings.DistanceUnit;
            switch (Image)
            {
                case GoalParameter.PulseZone:
                    return Intervals[0][0] + "\n-\n" + Intervals[0][1];
                case GoalParameter.SpeedZone:
                    if (speedUnit.Equals("pace"))
                    {
                        TimeSpan from = new TimeSpan(0, 0, (int)Math.Round(1 / HighScore.convertFromDistance(Intervals[0][1])));
                        TimeSpan to = new TimeSpan(0, 0, (int)Math.Round(1 / HighScore.convertFromDistance(Intervals[0][0])));
                        return from.ToString().Substring(3) + 
                            "\n-\n" + to.ToString().Substring(3);
                    }
                    else
                    {
                        return present(HighScore.convertFromDistance(Intervals[0][0]) * 3600) +
                             "\n-\n" + present(HighScore.convertFromDistance(Intervals[0][1]) * 3600);
                    }
                case GoalParameter.PulseZoneSpeedZone:
                    String res = Intervals[0][0] + "\n" + Intervals[0][1]+"\n/\n";
                    if (speedUnit.Equals("pace"))
                    {
                        TimeSpan from = new TimeSpan(0, 0, (int)Math.Round(1 / HighScore.convertFromDistance(Intervals[1][1])));
                        TimeSpan to = new TimeSpan(0, 0, (int)Math.Round(1 / HighScore.convertFromDistance(Intervals[1][0])));
                        return res + from.ToString().Substring(3) +
                            "\n" + to.ToString().Substring(3);
                    }
                    else
                    {
                        return res + present(HighScore.convertFromDistance(Intervals[1][0]) * 3600) +
                             "\n" + present(HighScore.convertFromDistance(Intervals[1][1]) * 3600);
                    }
            }
            return null;
        }
    }

    public enum GoalParameter
    {
        Distance, Time, Elevation, PulseZone, SpeedZone, CadenceZone, PulseZoneSpeedZone
    }
}

