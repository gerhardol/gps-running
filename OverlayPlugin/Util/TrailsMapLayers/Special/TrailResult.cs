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

//A special version of TrailResult, to interface TrailMapLayers
//Some parts are shared Overlay and Unique Routes. Some adaptions of the Trail class

using System;
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;
using ZoneFiveSoftware.Common.Data;
using ZoneFiveSoftware.Common.Data.GPS;
using ZoneFiveSoftware.Common.Visuals.Fitness;
#if !ST_2_1
using ZoneFiveSoftware.Common.Visuals.Mapping;
#endif
using ZoneFiveSoftware.Common.Data.Fitness;
using System.Collections.Generic;
using Microsoft.Win32;
using TrailsPlugin.Data;
using GpsRunningPlugin.Source;

namespace TrailsPlugin.Data
{
    public class TrailResult
    {
        ActivityWrapper m_result;
        public TrailResult(ActivityWrapper r)
        {
            m_result = r;
        }
        public IActivity Activity
        {
            get
            {
                return m_result.Activity;
            }
        }
        public Color TrailColor
        {
            get
            {
                return m_result.ActColor;
            }
        }
        public int Order = 0;

        /* Common UniqueRoutes and Overlay below */
        public IList<IGPSPoint> GpsPoints()
        {
            IList<IGPSPoint> m_gpsPoints = new List<IGPSPoint>();
            if (Activity.GPSRoute != null)
            {
                for (int i = 0; i < Activity.GPSRoute.Count; i++)
                {
                    m_gpsPoints.Add(Activity.GPSRoute[i].Value);
                }
            }
            return m_gpsPoints;
        }

        public IList<IGPSPoint> GpsPoints(Data.TrailsItemTrackSelectionInfo t)
        {
            if (t.MarkedTimes != null && t.MarkedTimes.Count > 0)
            {
                return GpsPoints(t.MarkedTimes);
            }
            else if (t.MarkedDistances != null && t.MarkedDistances.Count > 0)
            {
                return GpsPoints(t.MarkedDistances);
            }
            return new List<IGPSPoint>();
        }
		
        private IList<IGPSPoint> GpsPoints(IValueRangeSeries<DateTime> t)
        {
            IList<IGPSPoint> result = new List<IGPSPoint>();

            if (Activity.GPSRoute != null)
            {
            foreach (IValueRange<DateTime> r in t)
            {
                IGPSRoute GpsTrack = Activity.GPSRoute;
                int i = 0;
                while (i < GpsTrack.Count &&
                    0 < r.Lower.CompareTo(GpsTrack.EntryDateTime(GpsTrack[i])))
                {
                    i++;
                }
                while (i < GpsTrack.Count &&
                    0 <= r.Upper.CompareTo(GpsTrack.EntryDateTime(GpsTrack[i])))
                {
                    result.Add(GpsTrack[i].Value);
                    i++;
                }
            }
            }

            return result;
        }
		
        private IList<IGPSPoint> GpsPoints(IValueRangeSeries<double> t)
        {
            IList<IGPSPoint> result = new List<IGPSPoint>();
            if (Activity.GPSRoute != null)
            {
            IGPSRoute GpsTrack = Activity.GPSRoute;
            IDistanceDataTrack DistanceMetersTrack = Activity.GPSRoute.GetDistanceMetersTrack();

            foreach (IValueRange<double> r in t)
            {
                int i = 0;
                while (i < GpsTrack.Count &&
                    r.Lower - FirstDist > DistanceMetersTrack[i].Value)
                {
                    i++;
                }
                while (i < GpsTrack.Count &&
                    r.Upper - FirstDist >= DistanceMetersTrack[i].Value)
                {
                    result.Add(GpsTrack[i].Value);
                    i++;
                }
            }
            }

            return result;
        }

        public DateTime FirstTime
        {
            get
            {
                if (Activity == null)
                {
                    return DateTime.Now;
                }
                return Activity.StartTime;
            }
        }
        public DateTime LastTime
        {
            get
            {
                if (Activity == null || Activity.GPSRoute == null || Activity.GPSRoute.Count == 0)
                {
                    return FirstTime;
                }
                return Activity.GPSRoute.EntryDateTime(Activity.GPSRoute[Activity.GPSRoute.Count - 1]);
            }
        }
        public const double FirstDist = 0;

        public DateTime getActivityTime(float t)
        {
            return FirstTime.AddSeconds(t);
        }
        public double getActivityDist(double t)
        {
            return FirstDist + t;
        }
    }
}
