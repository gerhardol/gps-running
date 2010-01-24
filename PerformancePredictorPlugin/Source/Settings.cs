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
using SportTracksPerformancePredictorPlugin.Properties;

namespace SportTracksPerformancePredictorPlugin.Source
{
    class Settings
    {
        private static readonly String prefsPath;

        public readonly static Type highScore;

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

        private static bool showPrediction;
        public static bool ShowPrediction
        {
            get { return showPrediction; }
            set
            {
                showPrediction = value;
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

        private static PredictionModel model;
        public static PredictionModel Model
        {
            get
            {
                return model;
            }
            set
            {
                model = value;
                save();
            }
        }

        private static SortedList<double, SortedList<Length.Units, bool>> distances;
        public static SortedList<double, SortedList<Length.Units, bool>> Distances
        {
            get { return distances; }
        }

        private static bool showChart;
        public static bool ShowChart
        {
            get { return showChart; }
            set
            {
                showChart = value;
                save();
            }
        }

        private static int percentOfDistance;
        public static int PercentOfDistance
        {
            get { return percentOfDistance; }
            set
            {
                percentOfDistance = value;
                save();
            }
        }

        public static double convertPace(TimeSpan pace)
        {
            return 1 / pace.TotalHours;
        }

        public static string present(TimeSpan time) {
            if (time.TotalMinutes < 60)
            {
                return string.Format("{0}:{1:00}", time.Minutes, time.Seconds);
            }
            return string.Format("{0}:{1:00}:{2:00}", time.Days * 24 + time.Hours,
                time.Minutes, time.Seconds);
        }

        public static void reset()
        {
            showPrediction = true;
            showPace = true;
            windowSize = new Size(800, 600);
            model = PredictionModel.DAVE_CAMERON;
            showChart = true;
            percentOfDistance = 40;
            distances.Clear();
            addDistance(100, Length.Units.Meter, false);
            addDistance(200, Length.Units.Meter, false);
            addDistance(400, Length.Units.Meter, false);
            addDistance(500, Length.Units.Meter, false);
            addDistance(800, Length.Units.Meter, false);
            addDistance(1, Length.Units.Kilometer, false);
            addDistance(1.5, Length.Units.Kilometer, false);
            addDistance(1, Length.Units.Mile, false);
            addDistance(2, Length.Units.Kilometer, false);
            addDistance(3, Length.Units.Kilometer, false);
            addDistance(2, Length.Units.Mile, false);
            addDistance(4, Length.Units.Kilometer, false);
            addDistance(3, Length.Units.Mile, false);
            addDistance(5, Length.Units.Kilometer, false);
            addDistance(4, Length.Units.Mile, false);
            addDistance(8, Length.Units.Kilometer, false);
            addDistance(5, Length.Units.Mile, false);
            addDistance(10, Length.Units.Kilometer, false);
            addDistance(15, Length.Units.Kilometer, false);
            addDistance(10, Length.Units.Mile, false);
            addDistance(20, Length.Units.Kilometer, false);
            addDistance(21097.5, Length.Units.Meter, true);
            addDistance(15, Length.Units.Mile, false);
            addDistance(25, Length.Units.Kilometer, false);
            addDistance(30, Length.Units.Kilometer, false);
            addDistance(20, Length.Units.Mile, false);
            addDistance(25, Length.Units.Mile, false);
            addDistance(42195, Length.Units.Meter, true);
        }

        public static void addDistance(double d, Length.Units metric, bool userDefined)
        {
            if (!userDefined)
            {
                d = Length.Convert(d, metric, Length.Units.Meter);
            }
            SortedList<Length.Units, bool> list = new SortedList<Length.Units, bool>();
            list.Add(metric, userDefined);
            distances.Add(d, list);
            if (DistanceChanged != null)
            {
                DistanceChanged(null, null);
            }
        }

        public static void removeDistance(int index)
        {
            distances.RemoveAt(index);
            if (DistanceChanged != null)
            {
                DistanceChanged(null, null);
            }
        }
        
        static Settings()
        {
            distances = new SortedList<double, SortedList<Length.Units, bool>>();
            prefsPath = Environment.GetEnvironmentVariable("APPDATA") + "/PerformancePredictorPlugin/preferences.xml";
            if (!load())
            {
                Directory.CreateDirectory(Environment.GetEnvironmentVariable("APPDATA") + "/PerformancePredictorPlugin/");
                reset();
            }
            highScore = getPlugin("HighScore", "SportTracksHighScorePlugin.Source.HighScore");
        }

        private static Type getPlugin(string plugin, string klass)
        {
            try
            {
                String pluginPath = Directory.GetCurrentDirectory();
                while (pluginPath != null && !pluginPath.EndsWith("Plugins"))
                {
                    DirectoryInfo parent = Directory.GetParent(pluginPath);
                    if (parent != null)
                        pluginPath = parent.FullName;
                    else pluginPath = null;
                }
                String path = null;
                if (pluginPath != null)
                    path = getPath(plugin, pluginPath);
                else
                    path = getPath(plugin, Directory.GetCurrentDirectory() + "/Plugins/");
                Assembly assembly = Assembly.LoadFrom(path);
                FileVersionInfo info = FileVersionInfo.GetVersionInfo(assembly.Location);
                if ((info.ProductMinorPart >= 3 && info.ProductBuildPart >= 1) ||
                    info.ProductMinorPart > 3)
                {
                    return assembly.GetType(klass);
                }
            }
            catch (Exception) { }
            return null;
        }

        private static string getPath(string plugin, string pluginPath)
        {
            String path = pluginPath + "/" + plugin + "Plugin.dll";
            if (File.Exists(path))
                return path;
            foreach (String sub in Directory.GetDirectories(pluginPath))
            {
                path = getPath(plugin, sub);
                if (path != null)
                    return path;
            }
            return null;
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
                model = (PredictionModel) Enum.Parse(typeof(PredictionModel), elm.Attributes["metric"].Value);
                showChart = bool.Parse(elm.Attributes["showChart"].Value);
                parseDistances(elm.Attributes["distances"].Value.Split(';'));
                percentOfDistance = int.Parse(elm.Attributes["percentOfDistance"].Value);
                showPrediction = bool.Parse(elm.Attributes["showPrediction"].Value);
                showPace = bool.Parse(elm.Attributes["showPace"].Value);
            }
            catch (Exception)
            {
                reader.Close();
                reset();
                save();
                return false;
            }
            reader.Close();
            return true;
        }

        private static void parseDistances(string[] p)
        {
            foreach (String s in p)
            {
                string[] split = s.Split(' ');
                double distance = parseDouble(split[0]);
                Length.Units unit = (Length.Units)Enum.Parse(typeof(Length.Units), split[1]);
                bool userDefined = bool.Parse(split[2]);
                SortedList<Length.Units, bool> list = new SortedList<Length.Units, bool>();
                list.Add(unit, userDefined);
                distances.Add(distance, list);
            }
        }

        public static double parseDouble(string p)
        {
            //if (!p.Contains(".")) p += ".0";
            double d = double.Parse(p, NumberFormatInfo.InvariantInfo);
            return d;
        }

        public static void save()
        {
            XmlDocument document = new XmlDocument();
            XmlElement root = document.CreateElement("preformancePredictor");
            document.AppendChild(root);

            XmlElement resultSetupElm = document.CreateElement("view");
            root.AppendChild(resultSetupElm);
            resultSetupElm.SetAttribute("viewWidth", windowSize.Width.ToString());
            resultSetupElm.SetAttribute("viewHeight", windowSize.Height.ToString());
            resultSetupElm.SetAttribute("metric", model.ToString());
            resultSetupElm.SetAttribute("showChart", showChart.ToString());
            resultSetupElm.SetAttribute("percentOfDistance", percentOfDistance.ToString());
            resultSetupElm.SetAttribute("showPrediction", showPrediction.ToString());
            resultSetupElm.SetAttribute("showPace", showPace.ToString());
            StringBuilder att = new StringBuilder();
            bool first = true;
            foreach (double d in distances.Keys)
            {
                if (first) first = false;
                else att.Append(";");
                att.Append(d.ToString(NumberFormatInfo.InvariantInfo) + " ");
                att.Append(distances[d].Keys[0].ToString() + " ");
                att.Append(distances[d].Values[0].ToString());
            }
            resultSetupElm.SetAttribute("distances", att.ToString());

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

        public static string present(double p)
        {
            return String.Format("{0:0.000}", p);
        }

        public static string present(double p, int digits)
        {
            string pad = "";
            for (int i = 0; i < digits; i++)
            {
                pad += "0";
            }
            return String.Format("{0:0." + pad + "}", p);
        }

        public static event System.ComponentModel.PropertyChangedEventHandler DistanceChanged;

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

    public enum PredictionModel
    {
        DAVE_CAMERON, PETE_RIEGEL
    }
}
