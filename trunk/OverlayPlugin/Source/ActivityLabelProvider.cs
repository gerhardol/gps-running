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

namespace GpsRunningPlugin.Source
{
    class ActivityLabelProvider : TreeList.DefaultLabelProvider
    {
        #region ILabelProvider Members

        public override string GetText(object element, ZoneFiveSoftware.Common.Visuals.TreeList.Column column)
        {
            ActivityWrapper wrapper = (ActivityWrapper)element;
            ActivityInfoCache actInfoCache = new ActivityInfoCache();
            ActivityInfo actInfo = actInfoCache.GetInfo(wrapper.Activity);
            ActivityInfo refActInfo = null;

            bool boRefExists = (CommonData.refActWrapper != null);
            if (boRefExists)
                refActInfo = actInfoCache.GetInfo(CommonData.refActWrapper.Activity);

            switch(column.Id)
            {
                case OverlayColumnIds.StartTime:
                    return wrapper.Activity.StartTime.ToLocalTime().ToString();
                case OverlayColumnIds.Colour:
                    return null;
                case OverlayColumnIds.Offset:
                    if (Settings.UseTimeXAxis)
                        return wrapper.TimeOffset.ToString();
                    else
                        return UnitUtil.Distance.ToString(wrapper.DistanceOffset);
                case OverlayColumnIds.Visible:
                    return "";
                case OverlayColumnIds.Distance:
                    return UnitUtil.Distance.ToString(actInfo.DistanceMeters);
                case OverlayColumnIds.AvgSpeed:
                    return UnitUtil.Speed.ToString(actInfo.AverageSpeedMetersPerSecond);
                case OverlayColumnIds.AvgPace:
                    return UnitUtil.Pace.ToString(actInfo.AverageSpeedMetersPerSecond);
                case OverlayColumnIds.AvgHR:
                    return UnitUtil.HeartRate.ToString(actInfo.AverageHeartRate);
                case OverlayColumnIds.TotAsc:
                    return UnitUtil.Elevation.ToString(actInfo.TotalAscendingMeters(Plugin.GetApplication().DisplayOptions.SelectedClimbZone));
                case OverlayColumnIds.TotDesc:
                    return UnitUtil.Elevation.ToString(actInfo.TotalDescendingMeters(Plugin.GetApplication().DisplayOptions.SelectedClimbZone));
                case OverlayColumnIds.DistanceDiff:
                    if (!boRefExists)
                        return "0";
                    else
                        return UnitUtil.Distance.ToString(actInfo.DistanceMeters - refActInfo.DistanceMeters);
                case OverlayColumnIds.AvgSpeedDiff:
                    if (!boRefExists)
                        return "0";
                    else
                        return UnitUtil.Speed.ToString(actInfo.AverageSpeedMetersPerSecond - refActInfo.AverageSpeedMetersPerSecond);
                case OverlayColumnIds.AvgPaceDiff:
                    if (!boRefExists)
                        return "0";
                    else
                    {
                        double pace = UnitUtil.Pace.ConvertFrom(actInfo.AverageSpeedMetersPerSecond);
                        double refPace = UnitUtil.Pace.ConvertFrom(refActInfo.AverageSpeedMetersPerSecond);
                        TimeSpan time = new TimeSpan(0, 0, (int)(pace-refPace));
                        return time.ToString();
                    }
                case OverlayColumnIds.AvgHRDiff:
                    if (!boRefExists)
                        return "0";
                    else
                        return UnitUtil.HeartRate.ToString(actInfo.AverageHeartRate - refActInfo.AverageHeartRate);
                case OverlayColumnIds.AvgPowerDiff:
                    if (!boRefExists)
                        return "0";
                    else
                        return UnitUtil.Power.ToString(actInfo.AveragePower - refActInfo.AveragePower);
                case OverlayColumnIds.AvgCadDiff:
                    if (!boRefExists)
                        return "0";
                    else
                        return UnitUtil.Cadence.ToString(actInfo.AverageCadence - refActInfo.AverageCadence);
                case OverlayColumnIds.TimeDiff:
                    if (!boRefExists)
                        return new TimeSpan(0).ToString();
                    else
                        return (actInfo.Time - refActInfo.Time).ToString();
                default:
                    string text = base.GetText(actInfo, column);
                    if (text != "")
                        return text;
                    else
                        return base.GetText(wrapper.Activity, column);                    
            }
        }

        public override Image GetImage(object element, TreeList.Column column)
        {
            ActivityWrapper wrapper = (ActivityWrapper)element;

            if (column.Id == OverlayColumnIds.Colour)
            {
                Bitmap image = new Bitmap(column.Width, 15);
                for (int x = 0; x < image.Width; x++)
                {
                    for (int y = 0; y < image.Height; y++)
                    {
                        image.SetPixel(x, y, wrapper.ActColor);
                    }
                }
                return image;
            }
            else
            {
                return base.GetImage(wrapper.Activity, column);
            }
        }

        #endregion
    }
}
