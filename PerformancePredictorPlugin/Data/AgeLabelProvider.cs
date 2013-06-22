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
    class AgeLabelProvider : TreeList.DefaultLabelProvider
    {
        #region ILabelProvider Members

        public override string GetText(object element, ZoneFiveSoftware.Common.Visuals.TreeList.Column column)
        {
            AgeResult wrapper = (AgeResult)element;
            switch(column.Id)
            {
                case ResultColumnIds.Age:
                    return wrapper.Age.ToString("F0");
                case ResultColumnIds.EstimatedTime:
                    return UnitUtil.Time.ToString(wrapper.EstimatedTime);
                case ResultColumnIds.EstimatedSpeed:
                    return UnitUtil.PaceOrSpeed.ToString(Settings.ShowPace, wrapper.EstimatedSpeed);
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
        private static string present(double p, int digits)
        {
            string pad = "";
            for (int i = 0; i < digits; i++)
            {
                pad += "0";
            }
            return String.Format("{0:0." + pad + "}", p);
        }
    }
}
