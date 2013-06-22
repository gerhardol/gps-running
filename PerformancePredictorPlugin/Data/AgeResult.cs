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
    public class AgeResult
    {
        private IActivity activity;

        public IActivity Activity
        {
            get
            {
                return activity;
            }
        }

        public double Age;
        public TimeSpan EstimatedTime;
        public double EstimatedSpeed;
        public static float[] aAge = new float[] { 20, 30, 40, 50, 60, 70, 80 };

        public AgeResult(IActivity activity, float predAge, float currAge, TimeSpan time, double dist)
        {
            this.activity = activity;
            this.Age = predAge;
            this.EstimatedTime = TimeSpan.FromSeconds(PredictWavaTime.WavaPredict(dist, dist, time, predAge, currAge));

            this.EstimatedSpeed = dist / EstimatedTime.TotalSeconds;
        }
    }
}
