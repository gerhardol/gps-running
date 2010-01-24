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
using SportTracksHighScorePlugin.Properties;

namespace SportTracksHighScorePlugin.Source
{
    class Settings
    {
        public readonly static String prefsPath;

        public readonly static SortedList<double, bool> distances;

        public readonly static SortedList<int, TimeSpan> times;

        public readonly static SortedList<double, bool> elevations;

        public readonly static SortedList<double, SortedList<double, bool>> pulseZones;

        public readonly static SortedList<double, SortedList<double, bool>> speedZones;

        private static bool showTable;
        public static bool ShowTable
        {
            set { showTable = value; save(); }
            get { return showTable; }
        }

        private static bool ignoreManualData;
        public static bool IgnoreManualData
        {
            set { ignoreManualData = value; save(); }
            get { return ignoreManualData; }
        }

        private static GoalParameter domain;
        public static GoalParameter Domain
        {
            set { domain = value; save(); }
            get { return domain; }
        }

        private static GoalParameter image;
        public static GoalParameter Image
        {
            set { image = value; save(); }
            get { return image; }
        }

        private static bool upperBound;
        public static bool UpperBound
        {
            set { upperBound = value; save(); }
            get { return upperBound; }
        }

        private static Size windowSize;
        public static Size WindowSize
        {
            set { windowSize = value; save(); }
            get { return windowSize; }
        }

        public static void resetDistances()
        {
            distances.Clear();
            distances.Add(100, true);
            distances.Add(200, true);
            distances.Add(400, true);
            distances.Add(500, true);
            distances.Add(800, true);
            distances.Add(1000, true);
            distances.Add(1500, true);
            distances.Add(convertTo(1, Length.Units.Mile), true);
            distances.Add(2000, true);
            distances.Add(3000, true);
            distances.Add(convertTo(2, Length.Units.Mile), true);
            distances.Add(4000, true);
            distances.Add(convertTo(3, Length.Units.Mile), true);
            distances.Add(convertTo(5, Length.Units.Kilometer), true);
            distances.Add(convertTo(4, Length.Units.Mile), true);
            distances.Add(convertTo(8, Length.Units.Kilometer), true);
            distances.Add(convertTo(5, Length.Units.Mile), true);
            distances.Add(convertTo(10, Length.Units.Kilometer), true);
            distances.Add(convertTo(15, Length.Units.Kilometer), true);
            distances.Add(convertTo(10, Length.Units.Mile), true);
            distances.Add(convertTo(20, Length.Units.Kilometer), true);
            distances.Add(convertTo(21.0975, Length.Units.Kilometer), true);
            distances.Add(convertTo(15, Length.Units.Mile), true);
            distances.Add(convertTo(25, Length.Units.Kilometer), true);
            distances.Add(convertTo(30, Length.Units.Kilometer), true);
            distances.Add(convertTo(20, Length.Units.Mile), true);
            distances.Add(convertTo(25, Length.Units.Mile), true);
            distances.Add(convertTo(42.195, Length.Units.Kilometer), true);
        }

        public static void resetTimes()
        {
            times.Clear();
            times.Add(tt(0, 0, 10), new TimeSpan(0, 0, 10));
            times.Add(tt(0, 0, 30), new TimeSpan(0, 0, 30));
            times.Add(tt(0, 1, 0), new TimeSpan(0, 1, 0));
            times.Add(tt(0, 2, 0), new TimeSpan(0, 2, 0));
            times.Add(tt(0, 3, 0), new TimeSpan(0, 3, 0));
            times.Add(tt(0, 5, 0), new TimeSpan(0, 5, 0));
            times.Add(tt(0, 10, 0), new TimeSpan(0, 10, 0));
            times.Add(tt(0, 15, 0), new TimeSpan(0, 15, 0));
            times.Add(tt(0, 20, 0), new TimeSpan(0, 20, 0));
            times.Add(tt(0, 25, 0), new TimeSpan(0, 25, 0));
            times.Add(tt(0, 30, 0), new TimeSpan(0, 30, 0));
            times.Add(tt(0, 40, 0), new TimeSpan(0, 40, 0));
            times.Add(tt(0, 50, 0), new TimeSpan(0, 50, 0));
            times.Add(tt(1, 0, 0), new TimeSpan(1, 0, 0));
            times.Add(tt(1, 30, 0), new TimeSpan(1, 30, 0));
            times.Add(tt(2, 0, 0), new TimeSpan(2, 0, 0));
            times.Add(tt(2, 30, 0), new TimeSpan(2, 30, 0));
            times.Add(tt(3, 0, 0), new TimeSpan(3, 0, 0));
            times.Add(tt(3, 30, 0), new TimeSpan(3, 30, 0));
            times.Add(tt(4, 0, 0), new TimeSpan(4, 0, 0));
        }

        public static void resetElevations()
        {
            elevations.Clear();
            elevations.Add(10, true);
            elevations.Add(20, true);
            elevations.Add(30, true);
            elevations.Add(50, true);
            elevations.Add(75, true);
            elevations.Add(100, true);
            elevations.Add(150, true);
            elevations.Add(200, true);
            elevations.Add(300, true);
            elevations.Add(400, true);
            elevations.Add(500, true);
            elevations.Add(600, true);
            elevations.Add(700, true);
            elevations.Add(800, true);
            elevations.Add(1000, true);
            elevations.Add(1200, true);
            elevations.Add(1400, true);
            elevations.Add(1600, true);
            elevations.Add(1800, true);
            elevations.Add(2000, true);
        }

        public static void resetPulseZones()
        {
            pulseZones.Clear();
            addPulse(0, 100);
            addPulse(100, 120);
            addPulse(120, 140);
            addPulse(140, 160);
            addPulse(160, 180);
            addPulse(180, 200);
            addPulse(200, 220);
        }

        public static void resetSpeedZones()
        {
            speedZones.Clear();
            addSpeed(2.77777777777778, 3.33333333333333);
            addSpeed(3.03030303030303, 3.33333333333333);
            addSpeed(3.03030303030303, 3.7037037037037);
            addSpeed(3.33333333333333, 3.7037037037037);
            addSpeed(3.50877192982456, 3.92156862745098);
            addSpeed(3.7037037037037, 4.16666666666667);
            addSpeed(3.7037037037037, 4.44444444444444);
            addSpeed(3.92156862745098, 4.44444444444444);
            addSpeed(4.16666666666667, 4.76190476190476);
            addSpeed(4.76190476190476, 5.55555555555556);
        }

        private static int resetLists()
        {
            resetDistances();
            resetTimes();
            resetElevations();
            resetPulseZones();
            resetSpeedZones();

            return 0;
        }

        public static bool addSpeed(double min, double max)
        {
            SortedList<double, bool> list = null;
            if (speedZones.ContainsKey(min))
                list = speedZones[min];
            if (list == null)
            {
                list = new SortedList<double, bool>();
                list.Add(max, true);
                speedZones.Add(min, list);
                return true;
            }
            else
            {
                if (!list.ContainsKey(max))
                {
                    list.Add(max,true);
                }
                return false;
            }
        }

        public static bool addPulse(double min, double max)
        {
            SortedList<double, bool> list = null;
            if (pulseZones.ContainsKey(min))
                list = pulseZones[min];
            if (list == null)
            {
                list = new SortedList<double, bool>();
                list.Add(max, true);
                pulseZones.Add(min, list);
                return true;
            }
            else
            {
                if (!list.ContainsKey(max))
                {
                    list.Add(max,true);
                }
                return false;
            }
        }

        private static int tt(int hours, int minutes, int seconds)
        {
            return 3600 * hours + 60 * minutes + seconds;
        }

        public static double convertTo(double p, Length.Units metric)
        {
            switch (metric)
            {
                case Length.Units.Kilometer: return 1000 * p;
                case Length.Units.Mile: return 1.609344 * 1000 * p;
                case Length.Units.Centimeter: return p / 100.0;
                case Length.Units.Yard: return 0.9144 * p;
                case Length.Units.Foot: return 0.3048 * p;
                case Length.Units.Inch: return 0.0254 * p;
            }
            return p;
        }

        public static void reset()
        {
            resetLists();
            domain = GoalParameter.Time;
            image = GoalParameter.Distance;
            upperBound = false;
            windowSize = new Size(800, 600);
            showTable = true;
            ignoreManualData = true;
        }

        static Settings()
        {
            prefsPath = Environment.GetEnvironmentVariable("APPDATA") + "/HighScorePlugin/preferences.xml";
            distances = new SortedList<double, bool>();
            times = new SortedList<int, TimeSpan>();
            elevations = new SortedList<double, bool>();
            pulseZones = new SortedList<double, SortedList<double, bool>>();
            speedZones = new SortedList<double, SortedList<double, bool>>();
            if (!load())
            {
                Directory.CreateDirectory(Environment.GetEnvironmentVariable("APPDATA") + "/HighScorePlugin/");
                reset();
            }
        }

        private static bool load()
        {
            if (!File.Exists(prefsPath)) return false;
            XmlDocument document = new XmlDocument();
            XmlReader reader = new XmlTextReader(prefsPath);
            document.Load(reader);
            XmlNode root = document.FirstChild;
            if (!root.Name.Equals("highscore")) return false;
            try
            {
                foreach (XmlNode elm in root.ChildNodes)
                {
                    if (elm.Name.Equals("distances"))
                    {
                        if (elm.Attributes["values"].Value.Length > 0)
                        {
                            String[] values = elm.Attributes["values"].Value.Split(';');
                            foreach (String distance in values)
                            {
                                distances.Add(parseDouble(distance), true);
                            }
                        }
                    }
                    else if (elm.Name.Equals("times"))
                    {
                        if (elm.Attributes["values"].Value.Length > 0)
                        {
                            String[] values = elm.Attributes["values"].Value.Split(';');
                            foreach (String value in values)
                            {
                                int seconds = int.Parse(value);
                                if (!times.ContainsKey(seconds))
                                    times.Add(seconds, new TimeSpan(0, 0, seconds));
                            }
                        }
                    }
                    else if (elm.Name.Equals("elevations"))
                    {
                        if (elm.Attributes["values"].Value.Length > 0)
                        {
                            String[] values = elm.Attributes["values"].Value.Split(';');
                            foreach (String elevation in values)
                            {
                                elevations.Add(parseDouble(elevation), true);
                            }
                        }
                    }
                    else if (elm.Name.Equals("pulseZones"))
                    {
                        if (elm.Attributes["values"].Value.Length > 0)
                        {
                            String[] values = elm.Attributes["values"].Value.Split(';');
                            foreach (String value in values)
                            {
                                String[] pair = value.Split(' ');
                                double min = parseDouble(pair[0]);
                                double max = parseDouble(pair[1]);
                                if (pulseZones.ContainsKey(min))
                                {
                                    if (!pulseZones[min].ContainsKey(max))
                                    {
                                        pulseZones[min].Add(max, true);
                                    }
                                }
                                else
                                {
                                    SortedList<double, bool> list = new SortedList<double, bool>();
                                    list.Add(max, true);
                                    pulseZones.Add(min, list);
                                }
                            }
                        }
                    }
                    else if (elm.Name.Equals("speedZones"))
                    {
                        if (elm.Attributes["values"].Value.Length > 0)
                        {
                            String[] values = elm.Attributes["values"].Value.Split(';');
                            foreach (String value in values)
                            {
                                String[] pair = value.Split(' ');
                                double min = parseDouble(pair[0]);
                                double max = parseDouble(pair[1]);
                                if (speedZones.ContainsKey(min))
                                {
                                    if (!speedZones[min].ContainsKey(max))
                                    {
                                        speedZones[min].Add(max, true);
                                    }
                                }
                                else
                                {
                                    SortedList<double, bool> list = new SortedList<double, bool>();
                                    list.Add(max, true);
                                    speedZones.Add(min, list);
                                }
                            }
                        }
                    }
                    else if (elm.Name.Equals("view"))
                    {
                        domain = (GoalParameter) Enum.Parse(typeof(GoalParameter),elm.Attributes["domain"].Value,true);
                        image = (GoalParameter)Enum.Parse(typeof(GoalParameter), elm.Attributes["image"].Value, true);
                        upperBound = bool.Parse(elm.Attributes["upperBound"].Value);
                        windowSize = new Size(int.Parse(elm.Attributes["viewWidth"].Value),
                                                    int.Parse(elm.Attributes["viewHeight"].Value));
                        if (elm.Attributes["showTable"] == null)
                            showTable = true;
                        else
                            showTable = bool.Parse(elm.Attributes["showTable"].Value);
                        if (elm.Attributes["ignoreManualData"] == null)
                            ignoreManualData = true;
                        else
                            ignoreManualData = bool.Parse(elm.Attributes["ignoreManualData"].Value);
                    }
                }
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
            double d = double.Parse(p,NumberFormatInfo.InvariantInfo);
            return d;
        }

        public static void save()
        {
            XmlDocument document = new XmlDocument();
            XmlElement root = document.CreateElement("highscore");
            document.AppendChild(root);

            XmlElement distancesElm = document.CreateElement("distances");
            root.AppendChild(distancesElm);
            String distanceText = null;
            foreach (double distance in distances.Keys)
            {
                if (distanceText == null) distanceText = distance.ToString(NumberFormatInfo.InvariantInfo);
                else distanceText += ";" + distance.ToString(NumberFormatInfo.InvariantInfo);
            }
            distancesElm.SetAttribute("values", distanceText);

            XmlElement timesElm = document.CreateElement("times");
            root.AppendChild(timesElm);
            String timeText = null;
            foreach (int time in times.Keys)
            {
                if (timeText == null) timeText = time.ToString();
                else timeText += ";" + time.ToString();
            }
            timesElm.SetAttribute("values", timeText);

            XmlElement elevationsElm = document.CreateElement("elevations");
            root.AppendChild(elevationsElm);
            String elevationText = null;
            foreach (double elevation in elevations.Keys)
            {
                if (elevationText == null) elevationText = elevation.ToString(NumberFormatInfo.InvariantInfo);
                else elevationText += ";" + elevation;
            }
            elevationsElm.SetAttribute("values", elevationText);

            XmlElement pulsesElm = document.CreateElement("pulseZones");
            root.AppendChild(pulsesElm);
            String pulseText = null;
            foreach (double min in pulseZones.Keys)
                foreach (double max in pulseZones[min].Keys)
                    if (pulseText == null) pulseText = min + " " + max;
                    else pulseText += ";" + min + " " + max;
            pulsesElm.SetAttribute("values", pulseText);

            XmlElement speedsElm = document.CreateElement("speedZones");
            root.AppendChild(speedsElm);
            String speedText = null;
            foreach (double min in speedZones.Keys)
                foreach (double max in speedZones[min].Keys)
                {
                    String minS = min.ToString(NumberFormatInfo.InvariantInfo);
                    String maxS = max.ToString(NumberFormatInfo.InvariantInfo);
                    if (speedText == null) speedText = minS + " " + maxS;
                    else speedText += ";" + minS + " " + maxS;
                }
            speedsElm.SetAttribute("values", speedText);

            XmlElement resultSetupElm = document.CreateElement("view");
            root.AppendChild(resultSetupElm);
            resultSetupElm.SetAttribute("domain", domain.ToString());
            resultSetupElm.SetAttribute("image", image.ToString());
            resultSetupElm.SetAttribute("upperBound", upperBound.ToString());
            resultSetupElm.SetAttribute("viewWidth", windowSize.Width.ToString());
            resultSetupElm.SetAttribute("viewHeight", windowSize.Height.ToString());
            resultSetupElm.SetAttribute("showTable", showTable.ToString());
            resultSetupElm.SetAttribute("ignoreManualData", ignoreManualData.ToString());
            
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

        private static String translateUnit(Length.Units unit)
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

        private static String translateUnitShort(Length.Units unit)
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
