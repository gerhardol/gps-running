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
    class ResultLabelProvider : TreeList.DefaultLabelProvider
    {
        #region ILabelProvider Members

        public override string GetText(object element, ZoneFiveSoftware.Common.Visuals.TreeList.Column column)
        {
            Result result = (Result)element;
            switch(column.Id)
            {
                case ResultColumnIds.Distance:
                    return UnitUtil.Distance.ToString(result.Meters);
                case ResultColumnIds.Time:
                    return UnitUtil.Time.ToString(result.Seconds);
                case ResultColumnIds.Speed:
                    if (result.Seconds > 0 && result.Meters > 0)
                    {
                        double speedMS = result.Meters / result.Seconds;
                        return UnitUtil.PaceOrSpeed.ToString(Settings.ShowPace, speedMS);
                    }
                    else
                    {
                        return "-";
                    }
                case ResultColumnIds.StartTime:
                    return UnitUtil.Time.ToString(result.TimeStart);
                case ResultColumnIds.StartDistance:
                    return UnitUtil.Distance.ToString(result.MeterStart);
                case ResultColumnIds.Elevation:
                    return UnitUtil.Elevation.ToString(result.Elevations);
                case ResultColumnIds.AvgHR:
                    if (!result.AveragePulse.Equals(double.NaN))
                    {
                        return UnitUtil.HeartRate.ToString(result.AveragePulse);
                    }
                    return "-";
                case ResultColumnIds.Date:
                    return result.Activity.StartTime.ToLocalTime().ToShortDateString();
                case ResultColumnIds.Location:
                    return result.Activity.Location;
                default:
            ActivityInfo actInfo = ActivityInfoCache.Instance.GetInfo(result.Activity);
                    string text = base.GetText(actInfo, column);
                    if (text != "")
                        return text;
                    else
                        return base.GetText(result.Activity, column);                    
            }
        }

        public override Image GetImage(object element, TreeList.Column column)
        {
            Result wrapper = (Result)element;
            return base.GetImage(wrapper.Activity, column);
        }

        #endregion
    }
}
