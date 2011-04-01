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
using ZoneFiveSoftware.Common.Visuals;
using ZoneFiveSoftware.Common.Data.Fitness;
using GpsRunningPlugin.Util;
using GpsRunningPlugin.Properties;

namespace GpsRunningPlugin.Source
{
    class IntervalLabelProvider : TreeList.DefaultLabelProvider
    {
        #region ILabelProvider Members

        public override string GetText(object element, ZoneFiveSoftware.Common.Visuals.TreeList.Column column)
        {
            IntervalResult wrapper = (IntervalResult)element;
            switch(column.Id)
            {
                case ResultColumnIds.Distance:
                    return UnitUtil.Distance.ToString(wrapper.Distance);
                case ResultColumnIds.OneMile:
                    return UnitUtil.Pace.ToString(wrapper.OneMile, "mm:ss");
                case ResultColumnIds.FiveKm:
                    return UnitUtil.Pace.ToString(wrapper.FiveKm, "mm:ss");
                case ResultColumnIds.TenKm:
                    return UnitUtil.Pace.ToString(wrapper.TenKm, "mm:ss");
                default:
                    ActivityInfo actInfo = ActivityInfoCache.Instance.GetInfo(wrapper.Activity);
                    string text = base.GetText(actInfo, column);
                    if (text != "")
                        return text;
                    else
                        return base.GetText(wrapper.Activity, column);                    
            }
        }

        public override Image GetImage(object element, TreeList.Column column)
        {
            //TimePredictionResult wrapper = (TimePredictionResult)element;
            //return base.GetImage(wrapper.Activity, column);
            return null;
        }

        #endregion
    }
}
