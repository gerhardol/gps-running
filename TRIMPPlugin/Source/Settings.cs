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
using SportTracksTRIMPPlugin.Properties;

namespace SportTracksTRIMPPlugin.Source
{
    class Settings
    {
        static Settings()
        {
            defaults();
        }

        private static bool useMaxHR;
        public static bool UseMaxHR
        {
            get { return useMaxHR; }
            set { useMaxHR = value; }
        }

        private static int startZone;
        public static int StartZone
        {
            get { return startZone; }
            set { startZone = value; }
        }

        private static IList<double> factors;
        public static IList<double> Factors
        {
            get { return factors; }
            set { factors = value; }
        }

        private static Size windowSize;
        public static Size WindowSize
        {
            get { return windowSize; }
            set { windowSize = value; }
        }

        public static void defaults()
        {
            startZone = 50;
            factors = new double[] { 1, 1.1, 1.2, 2.2, 4.5 };
            useMaxHR = true;
            windowSize = new Size(800, 600);
        }

        public static void ReadOptions(XmlDocument xmlDoc, XmlNamespaceManager nsmgr, XmlElement pluginNode)
        {
            String attr, attr2;

            attr = pluginNode.GetAttribute(xmlTags.settingsVersion);
            if (attr.Length > 0) { settingsVersion = (Int16)XmlConvert.ToInt16(attr); }
            if (0 == settingsVersion)
            {
                // No settings in Preferences.System found, try read old files
                load();
            }

            attr = pluginNode.GetAttribute(xmlTags.startZone);
            if (attr.Length > 0) { startZone = XmlConvert.ToInt32(attr); }
            attr = pluginNode.GetAttribute(xmlTags.factors);
            if (attr.Length > 0) { factors = parseFactors(attr); }
            attr = pluginNode.GetAttribute(xmlTags.useMaxHR);
            if (attr.Length > 0) { useMaxHR = XmlConvert.ToBoolean(attr); }

            attr = pluginNode.GetAttribute(xmlTags.viewWidth);
            attr2 = pluginNode.GetAttribute(xmlTags.viewHeight);
            if (attr.Length > 0 && attr2.Length > 0)
            {
                windowSize = new Size(XmlConvert.ToInt16(attr), XmlConvert.ToInt16(attr2));
            }
        }

        public static void WriteOptions(XmlDocument xmlDoc, XmlElement pluginNode)
        {
            pluginNode.SetAttribute(xmlTags.settingsVersion, XmlConvert.ToString(settingsVersionCurrent));

            pluginNode.SetAttribute(xmlTags.startZone, XmlConvert.ToString(startZone));
            String str = "";
            foreach (double factor in factors)
            {
                str += factor + " ";
            }
            pluginNode.SetAttribute(xmlTags.factors, str);
            pluginNode.SetAttribute(xmlTags.useMaxHR, XmlConvert.ToString(useMaxHR));
            pluginNode.SetAttribute(xmlTags.viewWidth, XmlConvert.ToString(windowSize.Width));
            pluginNode.SetAttribute(xmlTags.viewHeight, XmlConvert.ToString(windowSize.Height));
        }

        private static int settingsVersion = 0; //default when not existing
        private const int settingsVersionCurrent = 1;

        private class xmlTags
        {
            public const string settingsVersion = "settingsVersion";
            public const string startZone = "startZone";
            public const string factors = "factors";
            public const string useMaxHR = "useMaxHR";

            public const string viewWidth = "viewWidth";
            public const string viewHeight = "viewHeight";
        }

        private static bool load()
        {
            //Backwards compatibility, read old preferences file
            String prefsPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + Path.DirectorySeparatorChar + "TRIMPPlugin" + Path.DirectorySeparatorChar + "preferences.xml";

            if (!File.Exists(prefsPath)) return false;
            XmlDocument document = new XmlDocument();
            XmlReader reader = new XmlTextReader(prefsPath);
            try
            {               
                document.Load(reader);
                XmlNode elm = document.ChildNodes[0]["view"];
                windowSize = new Size(int.Parse(elm.Attributes["viewWidth"].Value),
                                                    int.Parse(elm.Attributes["viewHeight"].Value));
                startZone = int.Parse(elm.Attributes["startZone"].Value);
                factors = parseFactors(elm.Attributes["factors"].Value);
                useMaxHR = bool.Parse(elm.Attributes["useMaxHR"].Value);
            }
            catch (Exception)
            {
                reader.Close();
                return false;
            }
            reader.Close();
            return true;
        }

        private static IList<double> parseFactors(String str)
        {
            IList<double> list = new List<double>();
            string[] strings = str.Split(new char[] { ' ' });
            foreach (string factor in strings)
            {
                if (!factor.Equals(""))
                {
                    list.Add(parseDouble(factor));
                }
            }
            return list;
        }

        public static double parseDouble(string p)
        {
            //if (!p.Contains(".")) p += ".0";
            double d = double.Parse(p, System.Globalization.NumberStyles.Any);
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

        public static string present(double p, int n)
        {
            if (n < 1) return ((int)Math.Round(p)).ToString();
            string s = "0";
            for (int i = 0; i < n - 1; i++)
                s += "0";
            return String.Format("{0:0." + s + "}", p);
        }

        public static String translateUnit(Length.Units unit)
        {
            switch (unit)
            {
                case Length.Units.Centimeter: return Resources.UnitCentimeter;
                case Length.Units.Foot: return Resources.UnitFoot;
                case Length.Units.Inch: return Resources.UnitInch;
                case Length.Units.Kilometer: return Resources.UnitKilometer;
                case Length.Units.Meter: return Resources.UnitMeter;
                case Length.Units.Mile: return Resources.UnitMile;
                case Length.Units.Yard: return Resources.UnitYard;
            }
            return null;
        }

        public static String DistanceUnit
        {
            get
            {
                return translateUnit(Plugin.GetApplication().SystemPreferences.DistanceUnits);
            }
        }

        public static String ElevationUnit
        {
            get
            {
                return translateUnit(Plugin.GetApplication().SystemPreferences.ElevationUnits);
            }
        }

        public static String translateUnitShort(Length.Units unit)
        {
            switch (unit)
            {
                case Length.Units.Centimeter: return Resources.UnitCentimeterShort;
                case Length.Units.Foot: return Resources.UnitFootShort;
                case Length.Units.Inch: return Resources.UnitInchShort;
                case Length.Units.Kilometer: return Resources.UnitKilometerShort;
                case Length.Units.Meter: return Resources.UnitMeterShort;
                case Length.Units.Mile: return Resources.UnitMileShort;
                case Length.Units.Yard: return Resources.UnitYardShort;
            }
            return null;
        }

        public static String DistanceUnitShort
        {
            get
            {
                return translateUnitShort(Plugin.GetApplication().SystemPreferences.DistanceUnits);
            }
        }

        public static String ElevationUnitShort
        {
            get
            {
                return translateUnitShort(Plugin.GetApplication().SystemPreferences.ElevationUnits);
            }
        }
    }
}
