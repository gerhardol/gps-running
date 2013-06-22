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
    public class ShoeResult
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

        public ShoeResult(IActivity activity, double vdot, float predWeight, float currWeight, 
            TimeSpan time, double dist)
        {
            this.activity = activity;
            this.Weight = predWeight;
            double f = vdotFactor(predWeight, currWeight);
            this.AjustedVdot = vdot * f;
            this.EstimatedTime = Predict.scaleTime(time, Predict.getTimeFactorFromAdjVdot(f));

            this.EstimatedSpeed = dist / EstimatedTime.TotalSeconds;
        }

        public static float[] aShoeWeight = new float[] { 0, 0.1f, 0.2f, 0.25f, 0.3f, 0.35f, 0.5f, 1f };
        public static float DefaultWeight = 0.35f;
        public static float IdealWeight = 0.1f; //The lightest race shoes are about this light, assume faster than barefoot

        public static float vdotFactor(float predWeight, float currWeight)
        {
            //Jack Daniels, http://runsmartproject.com/coaching/2012/02/06/how-much-does-shoe-weight-affect-performance/
            //100g (per shoe?) affect 1%
            float f = 1 + (currWeight - predWeight) * 1000 / 100 * 0.01f;
            if (f < 0) { f = 1; }
            return f;
        }
    }
}
