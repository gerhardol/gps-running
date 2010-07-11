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

using ZoneFiveSoftware.Common.Visuals;
using System.Collections.Generic;
using System.Drawing;
using ZoneFiveSoftware.Common.Data.Measurement;
using ZoneFiveSoftware.Common.Data.Fitness;
using SportTracksUniqueRoutesPlugin.Util;

namespace SportTracksUniqueRoutesPlugin.Source
{
    public class UniqueRoutesResult
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
                return UnitUtil.Time.ToString(info.Time);
            }
        }
        public string Distance
        {
            get
            {
                return UnitUtil.Distance.ToString(info.DistanceMeters);
            }
        }
        public string AvgSpeedPace
        {
            get
            {
                return UnitUtil.PaceOrSpeed.ToString(Settings.ShowPace, ScaleSpeed());
            }
        }
        public string AvgSpeed
        {
            get
            {
                return UnitUtil.Speed.ToString(ScaleSpeed());
            }
        }
        public string AvgPace
        {
            get
            {
                return UnitUtil.Pace.ToString(ScaleSpeed());
            }
        }
        public string AvgHR
        {
            get
            {
                return UnitUtil.HeartRate.ToString(info.AverageHeartRate);
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
        //There should probably be separate fields for active/total speed/pace
        private double ScaleSpeed()
        {
            double speed;
            if (Settings.UseActive)
            {
                speed = info.ActiveLapsTotalDetail.LapDistanceMeters * 1000 /
                info.ActiveLapsTotalDetail.LapElapsed.TotalMilliseconds;
            }
            else
            {
                speed = info.AverageSpeedMetersPerSecond;
            }
            return speed;
        }
        public string getField(string id)
        {
            //Should be using reflection....
            switch(id)
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
                    return AvgSpeed;
            }
        }
    }
}
