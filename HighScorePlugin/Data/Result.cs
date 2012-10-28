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

using ZoneFiveSoftware.Common.Data;
using ZoneFiveSoftware.Common.Data.Fitness;
using ZoneFiveSoftware.Common.Data.GPS;
using ZoneFiveSoftware.Common.Visuals;
using ZoneFiveSoftware.Common.Visuals.Fitness;
using ZoneFiveSoftware.Common.Data.Measurement;
using GpsRunningPlugin.Source;
using GpsRunningPlugin.Properties;
using GpsRunningPlugin.Util;

namespace GpsRunningPlugin.Source
{
    public class Result : IComparable
    {
        public Result(Goal goal, IActivity activity, IValueRangeSeries<DateTime> pause,
            double domainStart, double domainEnd,
            double timeStart, double timeEnd, double meterStart, double meterEnd, double elevationStart,
            double elevationEnd, DateTime firstDate, DateTime endDate)
        {
            this.Goal = goal;
            this.DomainDiff = domainEnd - domainStart;
            this.Activity = activity;
            this.pause = pause;
            //this.TimeStart = timeStart;
            //this.TimeEnd = timeEnd;
            this.Seconds = timeEnd - timeStart;
            //this.MeterStart = meterStart;
            //this.MeterEnd = meterEnd;
            this.Meters = meterEnd - meterStart;
            //this.ElevationStart = elevationStart;
            //this.ElevationEnd = elevationEnd;
            this.Elevations = elevationEnd - elevationStart;
            this.DateStart = firstDate;
            this.DateEnd = endDate;
        }

        public Goal Goal;

        public IActivity Activity;
        IValueRangeSeries<DateTime> pause;

        //private double MeterEnd, ElevationStart, ElevationEnd, TimeEnd;
        private double DomainDiff;
        public double Meters, Elevations, Seconds;
        public DateTime DateStart, DateEnd;
        private double? meterStart, timeStart, avgPulse;
        //public int Order = 1;

        public double TimeStart
        {
            get
            {
                if (timeStart == null)
                {
                    ActivityInfo info = ActivityInfoCache.Instance.GetInfo(this.Activity);
                    //TODO: Not always correct start time for Trails
                    timeStart = ZoneFiveSoftware.Common.Data.Algorithm.DateTimeRangeSeries.TimeNotPaused(info.ActualTrackStart, this.DateStart, this.pause).TotalSeconds;
                }
                return (double)timeStart;
            }
        }

        public double MeterStart
        {
            get
            {
                if (meterStart == null)
                {
                    ActivityInfo info = ActivityInfoCache.Instance.GetInfo(this.Activity);
                    meterStart = double.NaN;

                    //TODO: Not always correct for Trail results...
                    ITimeValueEntry<float> value = info.MovingDistanceMetersTrack.GetInterpolatedValue(DateStart);
                    if (value != null)
                    {
                        meterStart = value.Value;
                    }
                }
                return (double)meterStart;
            }
        }

        public double AveragePulse
        {
            get
            {
                if (avgPulse == null)
                {
                    ActivityInfo info = ActivityInfoCache.Instance.GetInfo(this.Activity);
                    if (info.SmoothedHeartRateTrack == null || info.SmoothedHeartRateTrack.Max <= 0)
                    {
                        return double.NaN;
                    }

                    //From TrailResult
                    INumericTimeDataSeries track = new NumericTimeDataSeries();
                    track.AllowMultipleAtSameTime = false;
                    int oldElapsed = int.MinValue;
                    foreach (ITimeValueEntry<float> t in info.SmoothedHeartRateTrack)
                    {
                        DateTime time = info.SmoothedHeartRateTrack.EntryDateTime(t);
                        if (this.DateStart <= time && time <= this.DateEnd &&
                            //TODO: (?) Incorrect pause check for "custom" pauses
                            !ZoneFiveSoftware.Common.Data.Algorithm.DateTimeRangeSeries.IsPaused(time, this.Activity.TimerPauses))
                        {
                            uint elapsed = t.ElapsedSeconds;
                            if (elapsed > oldElapsed)
                            {
                                track.Add(time, t.Value);
                                oldElapsed = (int)elapsed;
                            }
                        }
                        if (time > this.DateEnd)
                        {
                            break;
                        }
                    }

                    avgPulse = track.Avg;
                }
                return (double)avgPulse;
            }
        }

        public double getValue(GoalParameter gp, string speedUnit)
        {
            switch (gp)
            {
                case GoalParameter.Distance:
                    return UnitUtil.Distance.ConvertFrom(this.Meters);
                case GoalParameter.Time:
                    return this.Seconds;
                case GoalParameter.Elevation:
                    return UnitUtil.Elevation.ConvertFrom(this.Elevations);
                case GoalParameter.PulseZone:
                    return this.AveragePulse;
                case GoalParameter.SpeedZone:
                    double speed = this.Meters / this.Seconds;
                    return UnitUtil.PaceOrSpeed.ConvertFrom(speedUnit.Equals(CommonResources.Text.LabelPace), speed);
                case GoalParameter.PulseZoneSpeedZone:
                    return this.AveragePulse;
            }
            throw new Exception();
        }

        public override String ToString()
        {
            string str = Time.LabelPlural(Time.TimeRange.Second);
            //The label from ST is empty
            if (str == null || str.Equals("")) { str = "s"; }
            return String.Format("{0} : {1} {3}, {2} {4}", Goal.ToString(), Meters, Seconds, Length.LabelPlural(Length.Units.Meter), str);
        }

        //Relates to CompareTo
        public static Result LastResult(SortedList<Result, Result> results)
        {
            Result res = null;
            if (results.Count > 0)
            {
                //res = results.Values[results.Count - 1];
                res = results.Values[0];
            }
            return res;
        }

        //Note: CompareTo returns opposite of usual, to get best result first
        public int CompareTo(object obj)
        {
            int result = 1; //Default larger than
            if (obj != null && (obj is Result))
            {
                if (this.BetterResult(obj as Result))
                {
                    result = -1;
                }
            }
            return result;
        }

        public bool BetterResult(Result other)
        {
            bool result = this.DomainDiff > other.DomainDiff ? this.Goal.UpperBound : !this.Goal.UpperBound;
            return result;
        }        
    }
}