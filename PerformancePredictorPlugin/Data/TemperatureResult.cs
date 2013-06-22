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
        private double Distance;
        public double EstimatedSpeed { get { return this.Distance / this.EstimatedTime.TotalSeconds; } }

        public TemperatureResult(IActivity activity, float temperature, float actual, TimeSpan time, double dist)
        {
            this.activity = activity;
            double f = getTemperatureFactor(temperature) / getTemperatureFactor(actual);
            this.EstimatedTime = Predict.scaleTime(time, f);
            this.Distance = dist;
            this.Temperature = temperature;
        }

        public static float DefaultTemperature = 16;
        public static float IdealTemperature = DefaultTemperature;

        //The temperatures are closely related to the values, so it is defined here
        public static float[] aTemperature = new float[] { 16, 18, 21, 24, 27, 29, 32, 35, 38 };

        //Likely from Burton and Edholm 1969
        //Hardcoded values were previously used
        public static double getTemperatureFactor(float temperature)
        {
            //Outside range or invalid
            //Not use isValidtemperature() here
            if (float.IsNaN(temperature))
            {
                return 1;
            }
            if (temperature < aTemperature[0])
            {
                temperature = aTemperature[0];
            }
            if (temperature > aTemperature[aTemperature.Length-1])
            {
                temperature = aTemperature[aTemperature.Length - 1];
            }
            return temperature * 0.002727273 + 0.956363636; 
        }
    }
}
