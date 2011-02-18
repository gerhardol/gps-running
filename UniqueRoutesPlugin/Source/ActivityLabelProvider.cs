/*
Copyright (C) 2010 Staffan Nilsson
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
            UniqueRoutesResult wrapper = (UniqueRoutesResult)element;

            switch(column.Id)
            {
                case SummaryColumnIds.ActColor:
                    return null;
                case SummaryColumnIds.StartTime:
                    return wrapper.StartTime;
                case SummaryColumnIds.StartDate:
                    return wrapper.StartDate;
                case SummaryColumnIds.Time:
                    return wrapper.Time;
                case SummaryColumnIds.Distance:
                    return wrapper.Distance;
                case SummaryColumnIds.AvgSpeed:
                    return wrapper.AvgSpeed;
                case SummaryColumnIds.AvgSpeedPace:
                    return wrapper.AvgSpeedPace;
                case SummaryColumnIds.AvgPace:
                    return wrapper.AvgPace;
                case SummaryColumnIds.AvgHR:
                    return wrapper.AvgHR;
                case SummaryColumnIds.CommonStretches:
                    return wrapper.CommonStretches;
                default:
                    string text = base.GetText(ActivityInfoCache.Instance.GetInfo(wrapper.Activity), column);
                    if (text != "")
                        return text;
                    else
                        return base.GetText(wrapper.Activity, column);                    
            }
        }

        public override Image GetImage(object element, TreeList.Column column)
        {
            UniqueRoutesResult wrapper = (UniqueRoutesResult)element;

            if (column.Id == SummaryColumnIds.ActColor)
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
