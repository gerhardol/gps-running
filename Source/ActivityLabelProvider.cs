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

            if (column.Id == "Date")
                return wrapper.Activity.StartTime.ToLocalTime().ToString();
            else if (column.Id == "Colour")
                return null;
            else if (column.Id == "Offset")
            {
                if (Settings.UseTimeXAxis)
                    return UnitUtil.Time.ToString(wrapper.TimeOffset);
                else
                    return UnitUtil.Distance.ToString(wrapper.DistanceOffset);
            }
            else if (column.Id == "Visible")
                return "";
            else
                return base.GetText(wrapper.Activity, column);
        }

        public override System.Drawing.Image GetImage(object element, TreeList.Column column)
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
