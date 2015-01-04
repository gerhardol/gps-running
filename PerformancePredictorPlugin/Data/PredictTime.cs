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
    public enum PredictionModel
    {
        //TODO: Implement Vdot, Elinder, Purdy, adjust Riegel?
        DAVE_CAMERON, PETE_RIEGEL, WAVA, VDOT, ELINDER, PURDY
    }

    public class PredictionModelUtil
    {

        public static PredictionModel Default = PredictionModel.ELINDER;

        //The implemented models
        public static IList<PredictionModel> List = new List<PredictionModel> { PredictionModel.ELINDER, PredictionModel.WAVA, PredictionModel.DAVE_CAMERON, PredictionModel.PETE_RIEGEL };

        public static String Name(PredictionModel model)
        {
            switch (model)
            {
                default:
                case PredictionModel.DAVE_CAMERON:
                    return "Dave Cameron";
                case PredictionModel.PETE_RIEGEL:
                    return "Pete Riegel";
                case PredictionModel.WAVA:
                    return "WAVA";
                case PredictionModel.VDOT:
                    return "Vdot";
                case PredictionModel.ELINDER:
                    return "Elinder";
                case PredictionModel.PURDY:
                    return "Purdy Points";
            }
        }

        //Parse from displayed name
        public static PredictionModel Parse(String s)
        {
            PredictionModel model = Settings.Model;
            foreach (PredictionModel m in PredictionModelUtil.List)
            {
                if (s.Equals(PredictionModelUtil.Name(m)))
                { model = m; break; }
            }
            return model;
        }

        public static ChartColors Colors(PredictionModel model)
        {
            switch (model)
            {
                default:
                case PredictionModel.DAVE_CAMERON:
                    return new ChartColors(Color.FromArgb(0xff, 0xff, 0x00, 0x00), Color.FromArgb(0x70, 0xf0, 0xc0, 0xc0), Color.FromArgb(0xc8, 0xf0, 0xa0, 0xa0));
                case PredictionModel.PETE_RIEGEL:
                    return new ChartColors(Color.FromArgb(0xff, 0x00, 0x26, 0xff), Color.FromArgb(0x70, 0xcc, 0xdd, 0xff), Color.FromArgb(0xc8, 0x77, 0x88, 0xff));
                case PredictionModel.WAVA:
                    return new ChartColors(Color.FromArgb(0xff, 0xe8, 0xc9, 0x00), Color.FromArgb(0x70, 0xee, 0xdd, 0xcc), Color.FromArgb(0xc8, 0xff, 0xee, 0x77));
                case PredictionModel.VDOT:
                    return new ChartColors(Color.FromArgb(0xff, 0x00, 0xf0, 0x8a), Color.FromArgb(0x70, 0xcc, 0xff, 0xdd), Color.FromArgb(0xc8, 0x77, 0xff, 0x99));
                case PredictionModel.ELINDER:
                    return new ChartColors(Color.FromArgb(0xff, 0xff, 0x00, 0xdc), Color.FromArgb(0x70, 0xff, 0xcc, 0xee), Color.FromArgb(0xc8, 0xff, 0x77, 0xee));
                case PredictionModel.PURDY:
                    return new ChartColors(Color.FromArgb(0xff, 0x88, 0x88, 0x88), Color.FromArgb(0x70, 0xdd, 0xdd, 0xdd), Color.FromArgb(0xc8, 0xbb, 0xbb, 0xbb));
            }
        }
        /*
                        public static readonly IDictionary<int, ChartColors> ResultColor = new Dictionary<int, ChartColors>()
                    {
                        //The colors are on the border of "color circle" to get most contrast
                        //Some brighter values darkend to get better visibility without decreasing contrast too much
                        //transparency lowered as many lines can be layered
                        { 6,  new ChartColors(Color.FromArgb(0xff, 0x88, 0x88, 0x88), Color.FromArgb(0x70, 0xdd, 0xdd, 0xdd),Color.FromArgb(0xc8, 0xbb, 0xbb, 0xbb)) },
                        { 1,  new ChartColors(Color.FromArgb(0xff, 0xff, 0x00, 0x00), Color.FromArgb(0x70, 0xf0, 0xc0, 0xc0),Color.FromArgb(0xc8, 0xf0, 0xa0, 0xa0)) },
                        { 9,  new ChartColors(Color.FromArgb(0xff, 0xff, 0x6a, 0x00), Color.FromArgb(0x70, 0xf0, 0xd0, 0x90),Color.FromArgb(0xc8, 0xf0, 0xb0, 0x70)) },
                        { 3,  new ChartColors(Color.FromArgb(0xff, 0xe8, 0xc9, 0x00), Color.FromArgb(0x70, 0xee, 0xdd, 0xcc),Color.FromArgb(0xc8, 0xff, 0xee, 0x77)) },
                        { 12, new ChartColors(Color.FromArgb(0xff, 0xb2, 0xe1, 0x00), Color.FromArgb(0x70, 0xdd, 0xff, 0xcc),Color.FromArgb(0xc8, 0xcc, 0xff, 0x77)) },
                        { 7,  new ChartColors(Color.FromArgb(0xff, 0x4c, 0xf0, 0x00), Color.FromArgb(0x70, 0xcc, 0xff, 0xcc),Color.FromArgb(0xc8, 0x99, 0xff, 0x77)) },
                        { 0,  new ChartColors(Color.FromArgb(0xff, 0x00, 0xc2, 0x21), Color.FromArgb(0x70, 0xcc, 0xdd, 0xcc),Color.FromArgb(0xc8, 0x77, 0xee, 0x88)) },
                        { 4,  new ChartColors(Color.FromArgb(0xff, 0x00, 0xf0, 0x8a), Color.FromArgb(0x70, 0xcc, 0xff, 0xdd),Color.FromArgb(0xc8, 0x77, 0xff, 0x99)) },
                        { 13, new ChartColors(Color.FromArgb(0xff, 0x00, 0xf0, 0xf0), Color.FromArgb(0x70, 0xcc, 0xff, 0xff),Color.FromArgb(0xc8, 0x77, 0xff, 0xff)) },
                        { 10, new ChartColors(Color.FromArgb(0xff, 0x00, 0x94, 0xff), Color.FromArgb(0x70, 0xcc, 0xdd, 0xff),Color.FromArgb(0xc8, 0x77, 0xcc, 0xff)) },
                        { 2,  new ChartColors(Color.FromArgb(0xff, 0x00, 0x26, 0xff), Color.FromArgb(0x70, 0xcc, 0xdd, 0xff),Color.FromArgb(0xc8, 0x77, 0x88, 0xff)) },
                        { 14, new ChartColors(Color.FromArgb(0xff, 0x48, 0x00, 0xff), Color.FromArgb(0x70, 0xcc, 0xcc, 0xff),Color.FromArgb(0xc8, 0x99, 0x77, 0xff)) },
                        { 8,  new ChartColors(Color.FromArgb(0xff, 0xb2, 0x00, 0xff), Color.FromArgb(0x70, 0xdd, 0xcc, 0xff),Color.FromArgb(0xc8, 0xee, 0x77, 0xff)) },
                        { 5,  new ChartColors(Color.FromArgb(0xff, 0xff, 0x00, 0xdc), Color.FromArgb(0x70, 0xff, 0xcc, 0xee),Color.FromArgb(0xc8, 0xff, 0x77, 0xee)) },
                        { 11, new ChartColors(Color.FromArgb(0xff, 0xff, 0x00, 0x6e), Color.FromArgb(0x70, 0xff, 0xcc, 0xdd),Color.FromArgb(0xc8, 0xff, 0x77, 0xaa)) },
                    };
         * */

        //From Trails, ColorUtil, so are the colors
        public class ChartColors
        {
            public ChartColors(Color LineNormal, Color FillNormal, Color FillSelected)
            {
                this.LineNormal = LineNormal;
                this.FillNormal = FillNormal;
                this.FillSelected = FillSelected;
            }
            public ChartColors(Color LineNormal)
            {
                //Manually set color
                this.LineNormal = LineNormal;
                //Auto colors look strange, use gray instead
                this.FillNormal = Color.FromArgb(0x78, 200, 200, 205);
                this.FillSelected = Color.FromArgb(0xc8, 197, 197, 197);
            }
            public Color LineNormal;
            public Color FillNormal;
            public Color FillSelected;
        }
    }

    public class Predict
    {
        public delegate double PredictTime(double new_dist, double old_dist, TimeSpan old_time);

        public static PredictTime Predictor(PredictionModel model)
        {
            switch (model)
            {
                default:
                case PredictionModel.DAVE_CAMERON:
                    return Predict.Cameron;
                case PredictionModel.PETE_RIEGEL:
                    return Predict.Riegel;
                case PredictionModel.WAVA:
                    return Predict.Wava;
                case PredictionModel.VDOT:
                    return Predict.Vdot;
                case PredictionModel.ELINDER:
                    return Predict.Elinder;
                case PredictionModel.PURDY:
                    return Predict.Purdy;
            }
        }

        public static AthleteSex Sex = AthleteSex.NotSpecified;
        public static float CurrentAge = DefaultAge;
        public static float DefaultAge = 25f;
        public static void SetAgeSexFromActivity(IActivity act)
        {
            float age = (float)(act.StartTime - Plugin.GetApplication().Logbook.Athlete.DateOfBirth).TotalDays / 365.24f;
            Predict.Sex = Plugin.GetApplication().Logbook.Athlete.Sex;
            if (!float.IsNaN(age))
            {
                Predict.CurrentAge = age;
            }
        }

        /*************************************/
        public static PredictTime Cameron = delegate(double new_dist, double old_dist, TimeSpan old_time)
        {
            double a = 13.49681 - (0.000030363 * old_dist)
                + (835.7114 / Math.Pow(old_dist, 0.7905));
            double b = 13.49681 - (0.000030363 * new_dist)
                + (835.7114 / Math.Pow(new_dist, 0.7905));
            double new_time = (old_time.TotalSeconds / old_dist) * (a / b) * new_dist;
            return new_time;
        };

        //Possible: Riegel, other sports:
        //http://www.runscore.com/coursemeasurement/Articles/ARHE.pdf
        public static PredictTime Riegel = delegate(double new_dist, double old_dist, TimeSpan old_time)
        {
            double new_time = old_time.TotalSeconds * Math.Pow(new_dist / old_dist, Settings.RiegelFatigueFactor);
            return new_time;
        };

        public static PredictTime Wava = delegate(double new_dist, double old_dist, TimeSpan old_time)
        {
            double new_time = PredictWavaTime.WavaPredict(new_dist, old_dist, old_time);
            return new_time;
        };

        public static PredictTime Vdot = delegate(double new_dist, double old_dist, TimeSpan old_time)
        {
            return PredictVdotTime.Predict(new_dist, old_dist, old_time);
        };

        public static PredictTime Elinder = delegate(double new_dist, double old_dist, TimeSpan old_time)
        {
            return PredictElinderTime.Predict(new_dist, old_dist, old_time);
        };

        public static PredictTime Purdy = delegate(double new_dist, double old_dist, TimeSpan old_time)
        {
            return PredictPurdyTime.Predict(new_dist, old_dist, old_time);
        };


        /***********************************************************/
        //Util

        public static TimeSpan scaleTime(TimeSpan time, double p)
        {
            return TimeSpan.FromSeconds(time.TotalSeconds * p);
        }

        public static double getTrainingSpeed(double new_dist, double old_dist, TimeSpan old_time)
        {
            return new_dist / (Predict.Predictor(Settings.Model))(new_dist, old_dist, old_time);
        }

        //Get training speed from vdot
        public static double getTrainingSpeed(double vdot, double percentZone)
        {
            return (29.54 + 5.000663 * (vdot * (percentZone - 0.05))
                - 0.007546 * Math.Pow(vdot * (percentZone - 0.05), 2)) / 60;
        }

        public static double getVo2max(TimeSpan time)
        {
            double seconds = time.TotalSeconds;
            return getVo2max(seconds);
        }

        public static double getVo2max(double seconds)
        {
            return 0.8 + 0.1894393 * Math.Exp(-0.012778 * seconds / 60)
                + 0.2989558 * Math.Exp(-0.1932605 * seconds / 60);
        }

        public static double getVdot(TimeSpan time, double dist)
        {
            double seconds = time.TotalSeconds;
            return getVdot(seconds, dist);
        }
        public static double getVdot(double seconds, double dist)
        {
            return (-4.6 + 0.182258 * (dist * 60 / seconds)
                + 0.000104 * Math.Pow(dist * 60 / seconds, 2))
                / getVo2max(seconds);
        }

        public static double getTimeFactorFromAdjVdot(double ajustedVdotFactor)
        {
            return Math.Pow(1 / ajustedVdotFactor, 0.83);
        }
    }
}
