/*
Copyright (C) 2010 Staffan Nilsson

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
using System.Drawing;
using ZoneFiveSoftware.Common.Data;
using ZoneFiveSoftware.Common.Data.Fitness;
using ZoneFiveSoftware.Common.Data.Measurement;
using ZoneFiveSoftware.Common.Visuals.Chart;
using ZoneFiveSoftware.Common.Visuals.Fitness;
using GpsRunningPlugin.Properties;
using GpsRunningPlugin.Util;
using TrailsPlugin.Data;

namespace GpsRunningPlugin.Source
{
    public class TimePredictionSource
    {
        public IActivity Activity;

        public String StartTime
        {
            get
            {
                if (Activity == null)
                {
                    return Resources.NoSeedActivity;
                }
                DateTime t = Activity.StartTime.AddSeconds(offsetTime).ToLocalTime();
                return t.ToShortDateString() + " " + t.ToShortTimeString();
            }
        }

        public DateTime StartUsedTime
        {
            get
            {
                return Activity == null ? DateTime.MinValue : Activity.StartTime.AddSeconds(offsetTime);
            }
        }

        public double UsedDistance;
        public TimeSpan UsedTime;
        public double StartDistance;
        private double offsetTime;

        public TimePredictionSource(IActivity activity, double UsedDistance, TimeSpan UsedTime, double StartDistance, double offsetTime)
        {
            this.Activity = activity;
            //this.Distance = Distance;
            this.UsedDistance = UsedDistance;
            this.UsedTime = UsedTime;
            this.StartDistance = StartDistance;
            this.offsetTime = offsetTime;
        }

        public TimePredictionSource(IActivity activity, double UsedDistance, TimeSpan UsedTime)
            : this(activity, UsedDistance, UsedTime, 0, 0)
        {
        }

        public TimePredictionSource(IItemTrackSelectionInfo info)
        {
            if (info is TrailsItemTrackSelectionInfo)
            {
                //Assume format is Trails for now, all info in MarkedTimes
                //The times could be used too, converting back and forward now...
                TrailsItemTrackSelectionInfo tinfo = info as TrailsItemTrackSelectionInfo;

                this.Activity = tinfo.Activity;
                this.UsedDistance = 0;
                this.UsedTime = TimeSpan.Zero;

                bool first = true;
                IDistanceDataTrack distanceTrack =
                        ActivityInfoCache.Instance.GetInfo(Activity).MovingDistanceMetersTrack;
                foreach (ValueRange<DateTime> t in tinfo.MarkedTimes)
                {
                    TimeSpan lowerTime = ZoneFiveSoftware.Common.Data.Algorithm.DateTimeRangeSeries.TimeNotPaused(
                      Activity.StartTime, t.Lower, Activity.TimerPauses);
                    TimeSpan upperTime = ZoneFiveSoftware.Common.Data.Algorithm.DateTimeRangeSeries.TimeNotPaused(
                      Activity.StartTime, t.Upper, Activity.TimerPauses);

                    try
                    {
                        //GetInterpolated will fail if time is outside the interval, dont care
                        float lowerDist = distanceTrack.GetInterpolatedValue(t.Lower).Value;
                        float upperDist = distanceTrack.GetInterpolatedValue(t.Upper).Value;
                        if (first)
                        {
                            this.StartDistance = lowerDist;
                            this.offsetTime = lowerTime.TotalSeconds;
                            first = false;
                        }
                        this.UsedTime += upperTime - lowerTime;
                        this.UsedDistance += upperDist - lowerDist;
                    }
                    catch { }
                }
            }
            else
            {
                //Need to get activity from logbook, adjust distances etc
                throw new NotImplementedException("Expects IItemTrackSelectionInfo as TrailsItemTrackSelectionInfo");
            }
        }
    }
}
