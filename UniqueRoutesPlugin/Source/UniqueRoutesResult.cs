/*
Copyright (C) 2010 Gerhard Olsson

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

using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System;

using ZoneFiveSoftware.Common.Visuals;
using ZoneFiveSoftware.Common.Data.Measurement;
using ZoneFiveSoftware.Common.Data.Fitness;
using GpsRunningPlugin.Util;

namespace GpsRunningPlugin.Source
{
    public class UniqueRoutesResult : IComparable
    {
        public UniqueRoutesResult(IActivity activity, string commonText)
        {
            m_activity = activity;
            m_commonStretch = commonText;
        }
        private IActivity m_activity = null;
        public IActivity Activity
        {
            get { return m_activity; }
            set { m_activity = value; }
        }

        //ST should cahe this info, but less to write below
        private ActivityInfo m_info = null;
        private ActivityInfo info
        {
            get {
                if (m_info==null){
                    m_info = ActivityInfoCache.Instance.GetInfo(m_activity);
                }
                return m_info;
            }
        }
        public string StartDate
        {
            get
            {
                return m_activity.StartTime.ToLocalTime().ToShortDateString();
            }
        }
        public string StartTime
        {
            get
            {
                return m_activity.StartTime.ToLocalTime().ToShortTimeString();
            }
        }
        public string Time
        {
            get
            {
                return UnitUtil.Time.ToString(getTime());
            }
        }
        public string Distance
        {
            get
            {
                return UnitUtil.Distance.ToString(getDistance());
            }
        }
        public string AvgSpeedPace
        {
            get
            {
                return UnitUtil.PaceOrSpeed.ToString(Settings.ShowPace, getAvgSpeed());
            }
        }
        public string AvgSpeed
        {
            get
            {
                return UnitUtil.Speed.ToString(getAvgSpeed());
            }
        }
        public string AvgPace
        {
            get
            {
                return UnitUtil.Pace.ToString(getAvgSpeed());
            }
        }
        public string AvgHR
        {
            get
            {
                return UnitUtil.HeartRate.ToString(getAvgHR());
            }
        }
        private string m_commonStretch;
        public string CommonStretches
        {
            get
            {
                if (null == m_commonStretch)
                {
                    return "";
                }
                else
                {
                    return m_commonStretch;
                }
            }
        }
        /******************************************/
        //There should probably be separate fields for active/total speed/pace
        private System.TimeSpan getTime()
        {
            System.TimeSpan value;
            if (Settings.UseActive)
            {
                value = info.ActiveLapsTotalDetail.LapElapsed;
            }
            else
            {
                value = info.Time;
            }
            return value;
        }
        private double getDistance()
        {
            double value;
            if (Settings.UseActive)
            {
                value = info.ActiveLapsTotalDetail.LapDistanceMeters;
            }
            else
            {
                value = info.DistanceMeters;
            }
            return value;
        }
        private double getAvgHR()
        {
            double value;
            if (Settings.UseActive)
            {
                value = info.ActiveLapsTotalDetail.AverageHeartRatePerMinute;
            }
            else
            {
                value = info.AverageHeartRate;
            }
            return value;
        }
        private double getAvgSpeed()
        {
            double speed;
            if (Settings.UseActive)
            {
                speed = info.ActiveLapsTotalDetail.LapDistanceMeters /
                info.ActiveLapsTotalDetail.LapElapsed.TotalSeconds;
            }
            else
            {
                speed = info.AverageSpeedMetersPerSecond;
            }
            return speed;
        }

        /******************************************/
        public string getField(string id)
        {
            //Should be using reflection....
            switch (id)
            {
                case SummaryColumnIds.StartDate:
                    return StartDate;
                case SummaryColumnIds.StartTime:
                    return StartTime;
                case SummaryColumnIds.Time:
                    return Time;
                case SummaryColumnIds.Distance:
                    return Distance;
                case SummaryColumnIds.AvgSpeedPace:
                    return AvgSpeedPace;
                case SummaryColumnIds.AvgSpeed:
                    return AvgSpeed;
                case SummaryColumnIds.AvgPace:
                    return AvgPace;
                case SummaryColumnIds.AvgHR:
                    return AvgHR;
                case SummaryColumnIds.CommonStretches:
                    return CommonStretches;
                default:
                    //Should be a assert...
                    return AvgSpeed;
            }
        }
#region IComparable<Product> Members

        public int CompareTo(object obj)
        {
            int result = 1;
            if (obj != null && obj is UniqueRoutesResult)
            {
                UniqueRoutesResult other = obj as UniqueRoutesResult;
                result = Compare(this, other);
            }
            return result;
        }
        public int CompareTo(UniqueRoutesResult other)
        {
            return Compare(this, other);
        }

    #endregion
        public int Compare(UniqueRoutesResult x, UniqueRoutesResult y)
        { 
            int result = (Settings.SummaryViewSortDirection == ListSortDirection.Ascending ? 1 : -1);

            if (Settings.SummaryViewSortColumn == SummaryColumnIds.CommonStretches)
            {
                result *= x.CommonStretches.CompareTo(y.CommonStretches);
            }
            else
            {
                result *= x.getCompareField(Settings.SummaryViewSortColumn).CompareTo(y.getCompareField(Settings.SummaryViewSortColumn));
            }
            return result;
        }
        //Helper function to get numerical value used in comparison
        private double getCompareField(string id)
        {
            //Should be using reflection....
            switch (id)
            {
                case SummaryColumnIds.StartDate:
                    return m_activity.StartTime.Ticks;
                case SummaryColumnIds.StartTime:
                    return m_activity.StartTime.TimeOfDay.TotalSeconds;
                case SummaryColumnIds.Time:
                    return getTime().TotalSeconds;
                case SummaryColumnIds.Distance:
                    return getDistance();
                case SummaryColumnIds.AvgSpeedPace:
                    return (Settings.ShowPace ? -1 : 1) * getAvgSpeed();
                case SummaryColumnIds.AvgSpeed:
                    return getAvgSpeed();
                case SummaryColumnIds.AvgPace:
                    //Use negative, to avoid division by zero
                    return -getAvgSpeed();
                case SummaryColumnIds.AvgHR:
                    return getAvgHR();
                //case SummaryColumnIds.CommonStretches:
                //    return CommonStretches.;
                default:
                    return getAvgSpeed();
            }
        }
    }
}
