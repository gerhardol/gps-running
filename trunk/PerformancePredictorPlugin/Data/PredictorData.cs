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
using ZoneFiveSoftware.Common.Data.Fitness;
using ZoneFiveSoftware.Common.Data.Measurement;
using ZoneFiveSoftware.Common.Visuals.Chart;
using GpsRunningPlugin.Util;

namespace GpsRunningPlugin.Source
{
    //DataValid is not used, all data is currently calculated
    //public class PredictorDataValid
    //{
    //    public PredictorDataValid()
    //    {
    //        model = new List<PredictionModel>();
    //        hsresult = false;
    //    }
    //    public IList<PredictionModel> model;
    //    public bool hsresult;
    //}

    public class PredictorData
    {
        public PredictorData(double distanceNominal, Length.Units unitNominal)
        {
            this.Distance = distanceNominal;
            this.Unit = unitNominal;
            Activity = null;
            result = new Dictionary<PredictionModel, TimePredictionResult>();
            hsResult = null;
        }
        public double Distance;
        public Length.Units Unit;
        public IActivity Activity;
        public IDictionary<PredictionModel, TimePredictionResult> result;
        public TimePredictionHSResult hsResult;

        public static void getChartSeries(IDictionary<double, PredictorData> predictorData, PredictionModel model, ChartDataSeries tseries, ChartDataSeries pseries, bool isPace)
        {
            tseries.Points.Clear();
            pseries.Points.Clear();
            foreach (PredictorData t in predictorData.Values)
            {
                if (t.result.ContainsKey(model))
                {
                    TimePredictionResult r = t.result[model];
                    float x = (float)UnitUtil.Distance.ConvertFrom(t.Distance);
                    if (!x.Equals(float.NaN))
                    {
                        if (tseries.Points.IndexOfKey(x) == -1)
                        {
                            tseries.Points.Add(x, new PointF(x, (float)r.PredictedTime));
                        }
                        float y = (float)UnitUtil.PaceOrSpeed.ConvertFrom(isPace, t.Distance/r.PredictedTime);
                        if (pseries.Points.IndexOfKey(x) == -1)
                        {
                            pseries.Points.Add(x, new PointF(x, y));
                        }
                    }
                }
            }
        }
    }
}
