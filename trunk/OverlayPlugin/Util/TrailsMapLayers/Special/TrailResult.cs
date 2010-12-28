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

//A special version of the TrailResult, to interface TrailMapLayers

using System;
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;
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
        ActivityWrapper m_urResult;
        public TrailResult(ActivityWrapper r)
        {
            m_urResult = r;
        }
        public IActivity Activity
        {
            get
            {
                return m_urResult.Activity;
            }
        }
        public int Order=0;
        public IList<IGPSPoint> GpsPoints(TrailsItemTrackSelectionInfo sel)
        {
            return GpsPoints();
        }
        public IList<IGPSPoint> GpsPoints()
        {
            IList<IGPSPoint>  m_gpsPoints = new List<IGPSPoint>();
            for (int i = 0; i < Activity.GPSRoute.Count; i++)
            {
                m_gpsPoints.Add(Activity.GPSRoute[i].Value);
            }
            return m_gpsPoints;
        }
        public Color TrailColor
        {
            get
            {
                    return m_urResult.ActColor;
            }
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
                return Activity.GPSRoute.EntryDateTime(Activity.GPSRoute[Activity.GPSRoute.Count-1]);
            }
        }
    }
}
