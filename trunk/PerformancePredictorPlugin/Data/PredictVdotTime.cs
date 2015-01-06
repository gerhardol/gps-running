//Copyright (C) 1996 Patrick Hoffman
//http://www.cs.uml.edu/~phoffman/xcinfo3.html

//Modification to compile in C# by Gerhard Olsson 2015

using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using ZoneFiveSoftware.Common.Data.Measurement;
using ZoneFiveSoftware.Common.Data.Fitness;
using System.Reflection;
using System.Diagnostics;
using GpsRunningPlugin.Properties;

namespace GpsRunningPlugin.Source
{
    public class PredictVdotTime
    {
        //TBD
        public static double Predict(double new_dist, double old_dist, TimeSpan old_time)
        {
            throw new Exception("Not implemented yet");
            //double new_time = old_time.TotalSeconds;
            //return new_time;
        }
    }

    public static class Vdot
    {
        //http://run-down.com/statistics/calcs_explained.php
        /*        VO2 Max: This is the most complicated model to calculate, as the times for each distance must be found by narrowing 
         * down a time prediction until they are "close enough" through various combinations of Newton's Method and derivatives of quadratic 
         * equations. For the Run-Down calculator, times within a tenth of a second or 0.001% of the time prediction, which ever is less,
         * were deemed reasonable. This is especially true since at most a tenth of a second in a distance race is negligible and the VO2 Max
         * predictions are not very accurate in the first place for events that are short enough for 0.001% to be meaningful. 
         * The standard predictions for calculating VO2 Max are:

        percent_max = 0.8 + 0.1894393 * e^(-0.012778 * time) + 0.2989558 * e^(-0.1932605 * time)
        vo2 = -4.60 + 0.182258 * velocity + 0.000104 * velocity^2
        vo2max = vo2 / percent_max

        where time is in minutes and velocity is in meters per minute. These equations are also used for working backward to determine a time 
         * corresponding to a known VO2 Max and distance, although it requires approximating percent_max, combining equations, and treating vo2 
         * as a quadratic equation to solve for velocity, which is in turn used to calculate time (time = distance / velocity) and check how 
         * close the initial time estimate was.
        */
    }
}
