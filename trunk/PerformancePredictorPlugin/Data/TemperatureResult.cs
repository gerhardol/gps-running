﻿/*
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
            double f = getTemperatureFactor(temperature) / getTemperatureFactor(actual);
            this.EstimatedSpeed = speed / f;
            this.EstimatedTime = TrainingView.scaleTime(time, f);
            this.Temperature = temperature;
        }

        //The temperatures are closely related to the values, so it is defined here
        public static double[] aTemperature = new double[] { 16, 18, 21, 24, 27, 29, 32, 35, 38 };

        //Table from Kristian Bisgaard Lassen (unknown source)
        //Celcius factor
        //16 1
        //18 1.0075
        //21  1.015
        //24 1.0225
        //27 1.03 
        //29 1.0375
        //32 1.045
        //35 1.0525
        //38 1.06

        public static double getTemperatureFactor(double temperature)
        {
            if (!isValidtemperature(temperature))
            {
                //Outside range or invalid
                //Assume over 45 is invalid
                return 1;
            }
            else if (temperature < 20) { return 1.0075; }
            else if (temperature < 23) { return 1.015; }
            else if (temperature < 26) { return 1.0225; }
            else if (temperature < 28) { return 1.03; }
            else if (temperature < 31) { return 1.0375; }
            else if (temperature < 34) { return 1.045; }
            else if (temperature < 37) { return 1.0525; }
            return 1.06;
        }

        public static bool isValidtemperature(double temperature)
        {
            if (double.IsNaN(temperature) || temperature <= 16 || temperature > 45)
            {
                return false;
            }
            return true;
        }
    }
}
