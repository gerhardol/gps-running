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

namespace GpsRunningPlugin.Source
{
    public class WeightResult
    {
        private IActivity activity;

        public IActivity Activity
        {
            get
            {
                return activity;
            }
        }

        public double Weight;
        public double AjustedVdot;
        public TimeSpan EstimatedTime;
        public double EstimatedSpeed;

        public WeightResult(IActivity activity, double vdot, double predWeight, double currWeight,
            TimeSpan time, double dist)
        {
            this.activity = activity;
            this.Weight = predWeight;
            double f = vdotFactor(predWeight, currWeight);
            this.AjustedVdot = vdot * f;
            this.EstimatedTime = Predict.scaleTime(time, Predict.getTimeFactorFromAdjVdot(f));

            this.EstimatedSpeed = dist / EstimatedTime.TotalSeconds;
        }

        public static double vdotFactor(double predWeight, double currWeight)
        {
            return currWeight / predWeight;
        }

        public static float DefaultWeight = 80f;
        //Using (random) BMI of 18.5, from here http://www.livestrong.com/article/548473-the-best-bmi-for-running-5k/
        public static float IdealWeight(float weight, float lengthCm) { float bmiWeight = Settings.IdealBmi * lengthCm * lengthCm/10000f; return ((weight> bmiWeight) && !float.IsNaN(lengthCm)) ? bmiWeight : weight; }
    }
}
