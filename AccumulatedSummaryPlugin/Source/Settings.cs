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
using ZoneFiveSoftware.Common.Visuals;
using SportTracksAccumulatedSummaryPlugin.Properties;

namespace SportTracksAccumulatedSummaryPlugin.Source
{
    class Settings
    {
        static Settings()
        {
        }

        public static double parseDouble(string p)
        {
            //if (!p.Contains(".")) p += ".0";
            double d = double.Parse(p, NumberFormatInfo.InvariantInfo);
            return d;
        }

        public static double convertFrom(double p, Length.Units metric)
        {
            switch (metric)
            {
                case Length.Units.Kilometer: return p / 1000;
                case Length.Units.Mile: return p / (1.609344 * 1000);
                case Length.Units.Foot: return p * 3.2808399;
                case Length.Units.Inch: return p * 39.370079;
                case Length.Units.Centimeter: return p * 100;
                case Length.Units.Yard: return p * 1.0936133;
            }
            return p;
        }

        public static double convertFromDistance(double p)
        {
            return convertFrom(p, Plugin.GetApplication().SystemPreferences.DistanceUnits);
        }

        public static double convertFromElevation(double p)
        {
            return convertFrom(p, Plugin.GetApplication().SystemPreferences.ElevationUnits);
        }
        
        public static string present(double p)
        {
            return String.Format("{0:0.000}", p);
        }

        public static String ElevationUnit
        {
            get
            {
                return Length.Label(Plugin.GetApplication().SystemPreferences.ElevationUnits);
            }
        }
        public static String ElevationUnitShort
        {
            get
            {
                return Length.LabelAbbr(Plugin.GetApplication().SystemPreferences.ElevationUnits);
            }
        }

        //Limit distance to units where we have pace/speed labels
        private static Length.Units getDistUnit()
        {
            Length.Units distUnit = Plugin.GetApplication().SystemPreferences.DistanceUnits;
            if (distUnit.Equals(Length.Units.Foot) || distUnit.Equals(Length.Units.Inch) ||
                distUnit.Equals(Length.Units.Mile) || distUnit.Equals(Length.Units.Yard))
            {
                distUnit = Length.Units.Mile;
            }
            else
            {
                distUnit = Length.Units.Kilometer;
            }
            return distUnit;
        }
        public static String DistanceUnit
        {
            get
            { 
                return Length.Label(getDistUnit());
            }
        }

        public static String DistanceUnitShort
        {
            get
            {
                return Length.LabelAbbr(getDistUnit());
            }
        }

        public static String PaceUnit
        {
            get
            {
                String unit;
                if (getDistUnit().Equals(Length.Units.Mile))
                {
                    unit = CommonResources.Text.LabelMinPerMile;
                }
                else
                {
                    unit = CommonResources.Text.LabelMinPerKm;
                }
                return unit;
            }
        }
        public static String SpeedUnit
        {
            get
            {
                String unit;
                if (getDistUnit().Equals(Length.Units.Mile))
                {
                    unit = CommonResources.Text.LabelMilePerHour;
                }
                else
                {
                    unit = CommonResources.Text.LabelKmPerHour;
                }
                return unit;
            }
        }

        //Convert pace/speed from system type (m/s) to used value

        // distance unit per hour
        public static double getSpeed(double speedMS)
        {
            return convertFromDistance(60 * 60 * speedMS);
        }

        public static TimeSpan getPace(double speedMS)
        {
            double pace = 1 / convertFromDistance(speedMS);
            return new TimeSpan(0, (int)Math.Floor(pace / 60),
                (int)(pace - Math.Floor(pace / 60)*60));
        }

    }
}
