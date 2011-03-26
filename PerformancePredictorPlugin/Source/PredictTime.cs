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
using System.Reflection;
using System.Diagnostics;
using GpsRunningPlugin.Properties;

namespace GpsRunningPlugin.Source
{
    public class Predict
    {
        public delegate double PredictTime(double new_dist, double old_dist, double old_time);
        public static PredictTime Cameron = delegate(double new_dist, double old_dist, double old_time)
                    {
                        double a = 13.49681 - (0.000030363 * old_dist)
                            + (835.7114 / Math.Pow(old_dist, 0.7905));
                        double b = 13.49681 - (0.000030363 * new_dist)
                            + (835.7114 / Math.Pow(new_dist, 0.7905));
                        double new_time = (old_time / old_dist) * (a / b) * new_dist;
                        return new_time;
                    };

        public static PredictTime Riegel = delegate(double new_dist, double old_dist, double old_time)
                    {
                        double new_time = old_time * Math.Pow(new_dist / old_dist, 1.06);
                        return new_time;
                    };

    }
}
