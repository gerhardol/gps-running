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
        private static readonly String prefsPath;

        private static bool autoZoom;
        public static bool AutoZoom
        {
            get { return autoZoom; }
            set
            {
                autoZoom = value;
                save();
            }
        }

        private static bool showHeartRate;
        public static bool ShowHeartRate
        {
            get { return showHeartRate; }
            set
            {
                showHeartRate = value;
                save();
            }
        }

        private static bool showPace;
        public static bool ShowPace
        {
            get { return showPace; }
            set
            {
                showPace = value;
                save();
            }
        }

        private static bool showSpeed;
        public static bool ShowSpeed
        {
            get { return showSpeed; }
            set
            {
                showSpeed = value;
                save();
            }
        }

        private static bool showPower;
        public static bool ShowPower
        {
            get { return showPower; }
            set
            {
                showPower = value;
                save();
            }
        }

        private static bool showCadence;
        public static bool ShowCadence
        {
            get { return showCadence; }
            set
            {
                showCadence = value;
                save();
            }
        }

        private static bool showElevation;
        public static bool ShowElevation
        {
            get { return showElevation; }
            set
            {
                showElevation = value;
                save();
            }
        }

        private static bool showCategoryAverage;
        public static bool ShowCategoryAverage
        {
            get { return showCategoryAverage; }
            set
            {
                showCategoryAverage = value;
                save();
            }
        }

        private static bool showMovingAverage;
        public static bool ShowMovingAverage
        {
            get { return showMovingAverage; }
            set
            {
                showMovingAverage = value;
                save();
            }
        }

        private static double movingAverageLength;
        public static double MovingAverageLength
        {
            get { return movingAverageLength; }
            set
            {
                movingAverageLength = value;
                save();
            }
        }

        private static double movingAverageTime; // seconds
        public static double MovingAverageTime
        {
            get { return movingAverageTime; }
            set
            {
                movingAverageTime = value;
                save();
            }
        }

        private static bool showTime;
        public static bool ShowTime
        {
            get { return showTime; }
            set
            {
                showTime = value;
                save();
            }
        }

        private static Size windowSize;
        public static Size WindowSize
        {
            get { return windowSize; }
            set
            {
                windowSize = value;
                save();
            }
        }

        public static bool dontSave = false;

        public static void reset()
        {
            windowSize = new Size(800, 600);
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
        }
        
        static Settings()
        {
            prefsPath = Environment.GetEnvironmentVariable("APPDATA") + "/OverlayPlugin/preferences.xml";
            if (!load())
            {
                Directory.CreateDirectory(Environment.GetEnvironmentVariable("APPDATA") + "/OverlayPlugin/");
                reset();
            }
        }

        private static bool load()
        {
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

        public static void save()
        {
            if (dontSave) return;
            XmlDocument document = new XmlDocument();
            XmlElement root = document.CreateElement("overlay");
            document.AppendChild(root);

            XmlElement resultSetupElm = document.CreateElement("view");
            root.AppendChild(resultSetupElm);
            resultSetupElm.SetAttribute("viewWidth", windowSize.Width.ToString());
            resultSetupElm.SetAttribute("viewHeight", windowSize.Height.ToString());
            resultSetupElm.SetAttribute("showHeartRate", showHeartRate.ToString());
            resultSetupElm.SetAttribute("showPace", showPace.ToString());
            resultSetupElm.SetAttribute("showSpeed", showSpeed.ToString());
            resultSetupElm.SetAttribute("showPower", showPower.ToString());
            resultSetupElm.SetAttribute("showCadence", showCadence.ToString());
            resultSetupElm.SetAttribute("showElevation", showElevation.ToString());
            resultSetupElm.SetAttribute("showTime", showTime.ToString());
            resultSetupElm.SetAttribute("showCategoryAverage", showCategoryAverage.ToString());
            resultSetupElm.SetAttribute("showMovingAverage", showMovingAverage.ToString());
            resultSetupElm.SetAttribute("movingAverageTime", movingAverageTime.ToString());
            resultSetupElm.SetAttribute("movingAverageLength", movingAverageLength.ToString());
            resultSetupElm.SetAttribute("autoZoom", autoZoom.ToString());

            //StringWriter xmlString = new StringWriter();
            //XmlTextWriter writer2 = new XmlTextWriter(xmlString);
            XmlTextWriter writer = new XmlTextWriter(prefsPath, Encoding.UTF8);
            writer.Formatting = Formatting.Indented;
            writer.Indentation = 3;
            writer.IndentChar = ' ';
            document.WriteContentTo(writer);
            //document.WriteContentTo(writer2);
            writer.Close();
            //writer2.Close();

            //String text = xmlString.ToString();

            //Plugin.GetApplication().Logbook.SetExtensionText(new Guid(Properties.Resources.HighScoreGuid),
            //                                                    null);
            //Plugin.GetApplication().Logbook.SetExtensionText(new Guid(Properties.Resources.HighScoreGuid),
            //                                                    text);
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
