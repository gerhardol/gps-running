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

        public WeightResult(IActivity activity, int p, double vdot, double weight, double inc,
            TimeSpan time, double dist)
        {
            this.activity = activity;
            this.Weight = weight + p * inc;
            this.AjustedVdot = vdot * weight / this.Weight;
            this.EstimatedTime = Predict.scaleTime(time, Math.Pow(vdot / this.AjustedVdot, 0.83));

            this.EstimatedSpeed = dist / EstimatedTime.TotalSeconds;
        }

    }
}
