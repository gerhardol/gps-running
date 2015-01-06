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
            PredictorData wrapper = (PredictorData)element;

            //The time/speed fields
            if (column.Id == ResultColumnIds.PredictedTime)
            {
                PredictionModel model = Settings.Model;
                if (!wrapper.result.ContainsKey(model)) return null;
                return UnitUtil.Time.ToString(wrapper.result[Settings.Model].PredictedTime);
            }
            else if (column.Id == ResultColumnIds.Speed)
            {
                PredictionModel model = Settings.Model;
                if (!wrapper.result.ContainsKey(model)) return null;
                return UnitUtil.PaceOrSpeed.ToString(Settings.ShowPace, wrapper.Distance / wrapper.result[Settings.Model].PredictedTime);
            }
            else if (column.Id.StartsWith(ResultColumnIds.PredictedTimeModel))
            {
                string s = column.Id.Substring(ResultColumnIds.PredictedTimeModel.Length + 1);
                PredictionModel model = (PredictionModel)Enum.Parse(typeof(PredictionModel), s);
                if (!wrapper.result.ContainsKey(model)) return null;
                double time = wrapper.result[model].PredictedTime;
                return UnitUtil.Time.ToString(time);
            }
            else if (column.Id.StartsWith(ResultColumnIds.SpeedModel))
            {
                string s = column.Id.Substring(ResultColumnIds.SpeedModel.Length + 1);
                PredictionModel model = (PredictionModel)Enum.Parse(typeof(PredictionModel), s);
                if (!wrapper.result.ContainsKey(model)) return null;
                double time = wrapper.result[model].PredictedTime;
                return UnitUtil.PaceOrSpeed.ToString(Settings.ShowPace, wrapper.Distance / time);
            }

            if (wrapper.source == null)
            {
                return null;
            }

            switch (column.Id)
            {
                case ResultColumnIds.Distance:
                    return UnitUtil.Distance.ToString(wrapper.Distance);
                case ResultColumnIds.DistanceNominal:
                    return UnitUtil.Distance.ToString(wrapper.Distance, wrapper.Unit, "u");
                case ResultColumnIds.StartTime:
                    return wrapper.source.StartTime;
                case ResultColumnIds.UsedTime:
                    return UnitUtil.Time.ToString(wrapper.source.UsedTime);
                case ResultColumnIds.StartDistance:
                    return UnitUtil.Distance.ToString(wrapper.source.StartDistance);
                case ResultColumnIds.UsedDistance:
                    return UnitUtil.Distance.ToString(wrapper.source.UsedDistance);

                default:
                    ActivityInfo actInfo = ActivityInfoCache.Instance.GetInfo(wrapper.source.Activity);
                    string text = base.GetText(actInfo, column);
                    if (text != "")
                        return text;
                    else
                        return base.GetText(wrapper.source.Activity, column);
            }
        }

        public override Image GetImage(object element, TreeList.Column column)
        {
            PredictorData wrapper = (PredictorData)element;
            if (wrapper.source != null && wrapper.source.Activity != null) { return base.GetImage(wrapper.source.Activity, column); }
            else { return null; }
        }

        #endregion
    }
}
