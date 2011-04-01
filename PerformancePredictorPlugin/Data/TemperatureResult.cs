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
    public class TemperatureResult
    {
        private IActivity activity;

        public IActivity Activity
        {
            get
            {
                return activity;
            }
        }

        public double Temperature;
        public TimeSpan EstimatedTime;
        public double EstimatedSpeed;

        public TemperatureResult(IActivity activity, double temperature, float actual, TimeSpan time, double speed)
        {
            this.activity = activity;
            double f = TrainingView.getTemperatureFactor(temperature) / TrainingView.getTemperatureFactor(actual);
            this.EstimatedSpeed = speed / f;
            this.EstimatedTime = TrainingView.scaleTime(time, f);
            this.Temperature = temperature;
        }

    }
}
