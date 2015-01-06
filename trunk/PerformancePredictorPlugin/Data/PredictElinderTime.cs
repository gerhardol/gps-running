/*
Copyright (C) 2015 Gerhard Olsson

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
    public class PredictElinderTime
    {
        //http://www.ultradistans.se/wp-content/uploads/2014/05/FORMLER-F%C3%96R-BER%C3%84KNING-AV-L%C3%96PRESULTAT.pdf

        //Current distance to switch distance algorithm
        private static double BreakEvenDist;
        private static double BreakEvenTime; //should be close to Settings.ElinderBreakEvenTime.TotalSeconds
        private static double ElinderBreakEvenTimeMax = 2*3600; //cannot change much, formula has valid range
        //Current values for BreakEvenDist
        private static double currBreakDist = 0;
        private static double currBreakTime = 0;

        private static double getBreakEven(double dist, double time)
        {
            if (dist != currBreakDist || time != currBreakTime)
            {
                double timeBreakEven = Settings.ElinderBreakEvenTime.TotalSeconds;

                //Get initial seed for dist, time should be close to timeBreakEven
                EstimateBreakEven(timeBreakEven, dist, time);
                double bestTime = BreakEvenTime;
                double bestDist = BreakEvenDist;
                //TBD get closer to target time, especially for Ultra
                int i = 3;
                while (i-- > 0 && Math.Abs(BreakEvenTime - timeBreakEven) > 10)
                {
                    double t2 = Predict(dist);
                    double t3 = time * (1 + 0.7 * (time - t2) / time);
                    BreakEvenDist = dist * Math.Pow(timeBreakEven / t3, 1 / Settings.RiegelFatigueFactor);
                    if (time <= timeBreakEven)
                    {
                        BreakEvenTime = PredictInfra(BreakEvenDist, dist, time);
                    }
                    else
                    {
                        BreakEvenTime = PredictUltra(BreakEvenDist, dist, time);
                    }

                    if (Math.Abs(timeBreakEven - BreakEvenTime) < Math.Abs(timeBreakEven - bestTime))
                    {
                        bestTime = BreakEvenTime;
                        bestDist = BreakEvenDist;
                    }
                }

                BreakEvenTime = bestTime;
                BreakEvenDist = bestDist;

                currBreakDist = dist;
                currBreakTime = time;
            }
            return BreakEvenDist;
        }

        private static void EstimateBreakEven(double timeBreakEven, double dist, double time)
        {
            if (time <= ElinderBreakEvenTimeMax*1.3)
            {
                //From private conversation with Elinder
                //562,9 / (t1/s1 *  (7.313 - lg(s1)))^0,9307
                BreakEvenDist = 562900 / Math.Pow((time * ElinderBreakEvenTimeMax / timeBreakEven / 60 * 1000 / dist * (10.313 - Math.Log10(dist))), 0.9307);
                //This should be very close to 2h if timeBreakEven is not changed...
                BreakEvenTime = PredictInfra(BreakEvenDist, dist, time);
            }
            else
            {
                //Use Riegel reversed - may use iterations to get better data or a separate algorithm
                BreakEvenDist = dist * Math.Pow(timeBreakEven / time, 1 / Settings.RiegelFatigueFactor);
            }

            if (time <= timeBreakEven)
            {
                BreakEvenTime = PredictInfra(BreakEvenDist, dist, time);
            }
            else
            {
                BreakEvenTime = PredictUltra(BreakEvenDist, dist, time);
            }
        }

        //Infra: The algorithm valid before BreakEven (named from the article)
        private static float PredictInfra(double new_dist, double bDist, double bTime)
        {
            //v=b0/7.2*(7.313 – lg(s0)) / (7.313 – lg(b0)) , t=s0 * 1000 /v (s0 in km) -> 
            //t=7.2*s/b*(7.313-log(b/1000))/(7.313-(log(s)-log(1000)))=7.2*s/b*(10,313-log(b))/(10.313-log(s))
            return (float)(new_dist * bTime / bDist * (10.313 - Math.Log10(bDist)) / (10.313 - Math.Log10(new_dist)));
        }

        //Ultra: Valid after BreakEven
        private static float PredictUltra(double new_dist, double bDist, double bTime)
        {
            //v=b/7.2*(7.313 – 2.697*lg(s) + 1.697*lg(b)) / (7.313 – lg(b)) 
            return (float)(new_dist * bTime / bDist * (10.313 - Math.Log10(bDist)) / (7.313 - 2.697 * (Math.Log10(new_dist) - 3) + 1.697 * (Math.Log10(bDist) - 3)));
        }

        //Use latest cached break even factors
        private static double Predict(double new_dist)
        {
            double new_time;

            if (new_dist <= BreakEvenDist)
            {
                new_time = PredictInfra(new_dist, BreakEvenDist, BreakEvenTime);
            }
            else
            {
                new_time = PredictUltra(new_dist, BreakEvenDist, BreakEvenTime);
            }
            return new_time;
        }

        //Predict, check the factors first
        public static double Predict(double new_dist, double old_dist, TimeSpan old_time)
        {
            getBreakEven(old_dist, old_time.TotalSeconds);

            return Predict(new_dist);
        }
    }
}
