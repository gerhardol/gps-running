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
    public class TimePredictionResultUtil
    {
        public static void getTimeSeries(IList<TimePredictionResult> list, ChartDataSeries series)
        {
            series.Points.Clear();
            foreach (TimePredictionResult t in list)
            {
                float x = (float)UnitUtil.Distance.ConvertFrom(t.Distance);
                if (!x.Equals(float.NaN) && series.Points.IndexOfKey(x) == -1)
                {
                    series.Points.Add(x, new PointF(x, (float)t.PredictedTime));
                }
            }
        }

        public static void getSpeedSeries(IList<TimePredictionResult> list, ChartDataSeries series, bool isPace)
        {
            series.Points.Clear();
            foreach (TimePredictionResult t in list)
            {
                float x = (float)UnitUtil.Distance.ConvertFrom(t.Distance);
                float y = (float)UnitUtil.PaceOrSpeed.ConvertFrom(isPace, t.Speed);
                if (!x.Equals(float.NaN) && series.Points.IndexOfKey(x) == -1)
                {
                    series.Points.Add(x, new PointF(x, y));
                }
            }
        }
    }

    public class TimePredictionResult
    {
        private IActivity activity;

        public IActivity Activity
        {
            get
            {
                return activity;
            }
        }

        public double Distance;
        public double DistanceNominal;
        public Length.Units UnitNominal;
        public double PredictedTime;
        public double Speed
        {
            get
            {
                return Distance / PredictedTime;
            }
        }
        public DateTime StartDate
        {
            get
            {
                return activity.StartTime;
            }
        }
        public DateTime StartUsedTime
        {
            get
            {
                return activity.StartTime.AddSeconds(StartTime);
            }
        }

        //TODO: Protect these, only in multiresults
        public double UsedDistance;
        public TimeSpan UsedTime;
        public double StartDistance;
        private double StartTime;

        public TimePredictionResult(IActivity activity, double Distance, double DistanceNominal, Length.Units UnitNominal, double PredictedTime)
        {
            this.activity = activity;
            this.Distance = Distance;
            this.DistanceNominal = DistanceNominal;
            this.UnitNominal = UnitNominal;
            this.PredictedTime = PredictedTime;
        }
        public TimePredictionResult(IActivity activity, double Distance, double DistanceNominal, Length.Units UnitNominal, double PredictedTime, double UsedDistance, TimeSpan UsedTime, double StartDistance, double StartTime)
            : this(activity, Distance, DistanceNominal, UnitNominal, PredictedTime)
        {
            this.UsedDistance = UsedDistance;
            this.UsedTime = UsedTime;
            this.StartDistance = StartDistance;
            this.StartTime = StartTime;
        }
//NoSeed results
        public TimePredictionResult(double Distance, double DistanceNominal, Length.Units UnitNominal)
        {
            this.activity = null;
            this.Distance = Distance;
            this.DistanceNominal = DistanceNominal;
            this.UnitNominal = UnitNominal;
            this.PredictedTime = -1;
        }
    }
}
