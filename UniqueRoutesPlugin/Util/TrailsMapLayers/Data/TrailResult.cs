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

//Used in both Trails and Matrix plugin

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

namespace TrailsPlugin.UI.MapLayers
{
    public class TrailResult
    {
        public IActivity Activity;
        public int Order;
        public IList<IGPSPoint> GpsPoints(TrailsItemTrackSelectionInfo sel)
        {
            return new List<IGPSPoint>();
        }
        public IList<IGPSPoint> GpsPoints()
        {
            return new List<IGPSPoint>();
        }
        public Color TrailColor
        {
            get
            {
                return Color.Black;
            }
        }
    }
}
