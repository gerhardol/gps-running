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
using SportTracksOverlayPlugin.Properties;

namespace SportTracksOverlayPlugin.Source
{
    class Settings
    {
        static Settings()
        {
            defaults();
        }

        private static bool autoZoom;
        public static bool AutoZoom
        {
            get { return autoZoom; }
            set
            {
                autoZoom = value;
            }
        }

        private static bool showHeartRate;
        public static bool ShowHeartRate
        {
            get { return showHeartRate; }
            set
            {
                showHeartRate = value;
            }
        }

        private static bool showPace;
        public static bool ShowPace
        {
            get { return showPace; }
            set
            {
                showPace = value;
            }
        }

        private static bool showSpeed;
        public static bool ShowSpeed
        {
            get { return showSpeed; }
            set
            {
                showSpeed = value;
            }
        }

        private static bool showPower;
        public static bool ShowPower
        {
            get { return showPower; }
            set
            {
                showPower = value;
            }
        }

        private static bool showCadence;
        public static bool ShowCadence
        {
            get { return showCadence; }
            set
            {
                showCadence = value;
            }
        }

        private static bool showElevation;
        public static bool ShowElevation
        {
            get { return showElevation; }
            set
            {
                showElevation = value;
            }
        }

        private static bool showCategoryAverage;
        public static bool ShowCategoryAverage
        {
            get { return showCategoryAverage; }
            set
            {
                showCategoryAverage = value;
            }
        }

        private static bool showMovingAverage;
        public static bool ShowMovingAverage
        {
            get { return showMovingAverage; }
            set
            {
                showMovingAverage = value;
            }
        }

        private static double movingAverageLength;
        public static double MovingAverageLength
        {
            get { return movingAverageLength; }
            set
            {
                movingAverageLength = value;
            }
        }

        private static double movingAverageTime; // seconds
        public static double MovingAverageTime
        {
            get { return movingAverageTime; }
            set
            {
                movingAverageTime = value;
            }
        }

        private static bool showTime;
        public static bool ShowTime
        {
            get { return showTime; }
            set
            {
                showTime = value;
            }
        }

        private static Size windowSize; //viewWidth, viewHeight in xml
        public static Size WindowSize
        {
            get { return windowSize; }
            set
            {
                windowSize = value;
            }
        }

        private static int settingsVersion = 0;
        private const int settingsVersionCurrent = 1;

        public static void defaults()
        {
            showHeartRate = true;
            showPace = false;
            showSpeed = false;
            showPower = false;
            showCadence = false;
            showElevation = false;
            showCategoryAverage = false;
            showMovingAverage = false;
            movingAverageLength = 0;
            movingAverageTime = 0;
            autoZoom = true;
            showTime = true;
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

            attr = pluginNode.GetAttribute(xmlTags.showHeartRate);
            if (attr.Length > 0) { showHeartRate = XmlConvert.ToBoolean(attr); }
            attr = pluginNode.GetAttribute(xmlTags.showPace);
            if (attr.Length > 0) { showPace = XmlConvert.ToBoolean(attr); }
            attr = pluginNode.GetAttribute(xmlTags.showSpeed);
            if (attr.Length > 0) { showSpeed = XmlConvert.ToBoolean(attr); }
            attr = pluginNode.GetAttribute(xmlTags.showPower);
            if (attr.Length > 0) { showPower = XmlConvert.ToBoolean(attr); }
            attr = pluginNode.GetAttribute(xmlTags.showCadence);
            if (attr.Length > 0) { showCadence = XmlConvert.ToBoolean(attr); }
            attr = pluginNode.GetAttribute(xmlTags.showElevation);
            if (attr.Length > 0) { showElevation = XmlConvert.ToBoolean(attr); }
            attr = pluginNode.GetAttribute(xmlTags.showCategoryAverage);
            if (attr.Length > 0) { showCategoryAverage = XmlConvert.ToBoolean(attr); }
            attr = pluginNode.GetAttribute(xmlTags.showMovingAverage);
            if (attr.Length > 0) { showMovingAverage = XmlConvert.ToBoolean(attr); }
            attr = pluginNode.GetAttribute(xmlTags.movingAverageLength);
            if (attr.Length > 0) { movingAverageLength = XmlConvert.ToInt16(attr); }
            attr = pluginNode.GetAttribute(xmlTags.movingAverageTime);
            if (attr.Length > 0) { movingAverageTime = XmlConvert.ToInt16(attr); }
            attr = pluginNode.GetAttribute(xmlTags.autoZoom);
            if (attr.Length > 0) { autoZoom = XmlConvert.ToBoolean(attr); }
            attr = pluginNode.GetAttribute(xmlTags.showTime);
            if (attr.Length > 0) { showTime = XmlConvert.ToBoolean(attr); }

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

            pluginNode.SetAttribute(xmlTags.showHeartRate, XmlConvert.ToString(showHeartRate));
            pluginNode.SetAttribute(xmlTags.showPace, XmlConvert.ToString(showPace));
            pluginNode.SetAttribute(xmlTags.showSpeed, XmlConvert.ToString(showSpeed));
            pluginNode.SetAttribute(xmlTags.showPower, XmlConvert.ToString(showPower));
            pluginNode.SetAttribute(xmlTags.showCadence, XmlConvert.ToString(showCadence));
            pluginNode.SetAttribute(xmlTags.showElevation, XmlConvert.ToString(showElevation));
            pluginNode.SetAttribute(xmlTags.showCategoryAverage, XmlConvert.ToString(showCategoryAverage));
            pluginNode.SetAttribute(xmlTags.showMovingAverage, XmlConvert.ToString(showMovingAverage));
            pluginNode.SetAttribute(xmlTags.movingAverageLength, XmlConvert.ToString(movingAverageLength));
            pluginNode.SetAttribute(xmlTags.movingAverageTime, XmlConvert.ToString(movingAverageTime));
            pluginNode.SetAttribute(xmlTags.autoZoom, XmlConvert.ToString(autoZoom));
            pluginNode.SetAttribute(xmlTags.showTime, XmlConvert.ToString(showTime));

            pluginNode.SetAttribute(xmlTags.viewWidth, XmlConvert.ToString(windowSize.Width));
            pluginNode.SetAttribute(xmlTags.viewHeight, XmlConvert.ToString(windowSize.Height));
        }

        private class xmlTags
        {
            public const string settingsVersion = "settingsVersion";

            public const string showHeartRate = "showHeartRate";
            public const string showPace = "showPace";
            public const string showSpeed = "showSpeed";
            public const string showPower = "showPower";
            public const string showCadence = "showCadence";
            public const string showElevation = "showElevation";
            public const string showCategoryAverage = "showCategoryAverage";
            public const string showMovingAverage = "showMovingAverage";
            public const string movingAverageLength = "movingAverageLength";
            public const string movingAverageTime = "movingAverageTime";
            public const string autoZoom = "autoZoom";
            public const string showTime = "showTime";
            public const string viewWidth = "viewWidth";
            public const string viewHeight = "viewHeight";
        }
        
        private static bool load()
        {
            //Backwards compatibility, read old preferences file
            String prefsPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + Path.DirectorySeparatorChar + "OverlayPlugin" + Path.DirectorySeparatorChar + "preferences.xml";

            if (!File.Exists(prefsPath)) return false;
            XmlDocument document = new XmlDocument();
            XmlReader reader = new XmlTextReader(prefsPath);
            document.Load(reader);
            try
            {
                XmlNode elm = document.ChildNodes[0]["view"];
                windowSize = new Size(int.Parse(elm.Attributes["viewWidth"].Value),
                                                    int.Parse(elm.Attributes["viewHeight"].Value));
                showHeartRate = bool.Parse(elm.Attributes["showHeartRate"].Value);
                showPace = bool.Parse(elm.Attributes["showPace"].Value);
                showSpeed = bool.Parse(elm.Attributes["showSpeed"].Value);
                showPower = bool.Parse(elm.Attributes["showPower"].Value);
                showCadence = bool.Parse(elm.Attributes["showCadence"].Value);
                showElevation = bool.Parse(elm.Attributes["showElevation"].Value);
                showTime = bool.Parse(elm.Attributes["showTime"].Value);
                showCategoryAverage = bool.Parse(elm.Attributes["showCategoryAverage"].Value);
                showMovingAverage = bool.Parse(elm.Attributes["showMovingAverage"].Value);
                movingAverageLength = parseDouble(elm.Attributes["movingAverageLength"].Value);
                movingAverageTime = parseDouble(elm.Attributes["movingAverageTime"].Value);
                autoZoom = bool.Parse(elm.Attributes["autoZoom"].Value);
            }
            catch (Exception)
            {
                reader.Close();
                return false;
            }
            reader.Close();
            return true;
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
