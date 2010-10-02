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
                case "StartTime":
                    return wrapper.Activity.StartTime.ToLocalTime().ToString();
                case "Colour":
                    return null;
                case "Offset":
                    if (Settings.UseTimeXAxis)
                        return UnitUtil.Time.ToString(wrapper.TimeOffset);
                    else
                        return UnitUtil.Distance.ToString(wrapper.DistanceOffset);
                case "Visible":
                    return "";
                case "DistanceMeters":
                    return ((actInfo.DistanceMeters/1000).ToString("0.##"));
                case "AverageSpeedMetersPerSecond":
                    return UnitUtil.Speed.ToString(actInfo.AverageSpeedMetersPerSecond);
                case "AvgPace":
                    return UnitUtil.Pace.ToString(actInfo.AverageSpeedMetersPerSecond);
                case "AverageHeartRate":
                    return actInfo.AverageHeartRate.ToString("###");
                case "DistanceMetersDiff":
                    if (!boRefExists)
                        return "0";
                    else
                        return ((actInfo.DistanceMeters - refActInfo.DistanceMeters) / 1000).ToString("0.##");
                case "AverageSpeedMetersPerSecondDiff":
                    if (!boRefExists)
                        return "0";
                    else
                        return UnitUtil.Speed.ToString(actInfo.AverageSpeedMetersPerSecond - refActInfo.AverageSpeedMetersPerSecond);
                case "AvgPaceDiff":
                    if (!boRefExists)
                        return "0";
                    else
                    {
                        double pace = UnitUtil.Pace.ConvertFrom(actInfo.AverageSpeedMetersPerSecond);
                        double refPace = UnitUtil.Pace.ConvertFrom(refActInfo.AverageSpeedMetersPerSecond);
                        TimeSpan time = new TimeSpan(0, 0, (int)(pace-refPace));
                        return time.ToString();
                    }
                case "AverageHeartRateDiff":
                    if (!boRefExists)
                        return "0";
                    else
                        return (actInfo.AverageHeartRate - refActInfo.AverageHeartRate).ToString("N0");
                case "TimeDiff":
                    if (!boRefExists)
                        return new TimeSpan(0).ToString();
                    else
                        return (actInfo.Time - refActInfo.Time).ToString();
                default:
                    string text = base.GetText(actInfo, column);
                    if (text != null)
                        return text;
                    else
                        return base.GetText(wrapper.Activity, column);                    
            }
        }

        public override Image GetImage(object element, TreeList.Column column)
        {
            ActivityWrapper wrapper = (ActivityWrapper)element;

            if (column.Id == "Colour")
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
