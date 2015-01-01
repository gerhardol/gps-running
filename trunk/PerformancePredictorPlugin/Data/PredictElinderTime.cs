/*
Copyright (C) 2007, 2008 Kristian Bisgaard Lassen
Copyright (C) 2010 Kristian Helkjaer Lassen

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

        private static float? BreakEven = null;

        private static float ElianderPredict_short(double old_dist)
        {
        //v=b0/7.2*(7.313 – lg(s0)) / (7.313 – lg(b0)) , t=s0×1000 /v (s0 in km)-> t=7.2*s/b*(7.313-log(b/1000))/(7.313-(log(s)-log(1000)))=7.2*s/b*(10,313-log(b))/(10.313-log(s))
            return (float)(7.2 * old_dist / (float)BreakEven * (10.313 - Math.Log10((float)BreakEven)) / (10.313 - Math.Log10(old_dist)));
        }

        private static float ElianderPredict_long(double old_dist)
        {
            //v=b/7.2*(7.313 – 2.697*lg(s) + 1.697*lg(b)) / (7.313 – lg(b)) 
            return (float)(7.2 * old_dist / (float)BreakEven * (7.313 - Math.Log10((float)BreakEven)) / (7.313 - 2.697 * (Math.Log10(old_dist) - 3) + 1.687 * (Math.Log10(old_dist) - 3)));
        }

        public static double Predict(double new_dist, double old_dist, TimeSpan old_time)
        {
            double new_time;
            float age = PredictWavaTime.IdealAge((float)old_dist);
            float time = PredictWavaTime.IdealTime((float)old_dist, age);
            if (old_time.TotalSeconds <= 2 * 3600)
            {
                new_time = ElianderPredict_short(old_dist);
            }
            else
            {
                new_time = ElianderPredict_short(old_dist);
            }
            return new_time;
        }
    }
}
