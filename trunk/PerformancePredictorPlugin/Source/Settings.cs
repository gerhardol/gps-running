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
    class Settings
    {
        public readonly static Type highScore;
        static Settings()
        {
            distances = new SortedList<double, SortedList<Length.Units, bool>>();
            highScore = getPlugin("HighScore", "GpsRunningPlugin.Source.HighScore");
            defaults();
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

        private static bool showPrediction;
        public static bool ShowPrediction
        {
            get { return showPrediction; }
            set
            {
                showPrediction = value;
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
            }
        }

        private static int percentOfDistance;
        public static int PercentOfDistance
        {
            get { return percentOfDistance; }
            set
            {
                percentOfDistance = value;
            }
        }

        private static Size windowSize;
        public static Size WindowSize
        {
            get { return windowSize; }
            set
            {
                windowSize = value;
            }
        }

        public static void defaults()
        {
            showPrediction = true;
            showPace = true;
            showChart = false;//show chart by default
            percentOfDistance = 40;
            model = PredictionModel.DAVE_CAMERON;
            distances.Clear();
            //Some distances removed by default
            //addDistance(100, Length.Units.Meter, false);
            //addDistance(200, Length.Units.Meter, false);
            addDistance(400, Length.Units.Meter, false);
            //addDistance(500, Length.Units.Meter, false);
            //addDistance(800, Length.Units.Meter, false);
            addDistance(1, Length.Units.Kilometer, false);
            //addDistance(1.5, Length.Units.Kilometer, false);
            addDistance(1, Length.Units.Mile, false);
            addDistance(2, Length.Units.Kilometer, false);
            //addDistance(3, Length.Units.Kilometer, false);
            addDistance(2, Length.Units.Mile, false);
            //addDistance(4, Length.Units.Kilometer, false);
            //addDistance(3, Length.Units.Mile, false);
            addDistance(5, Length.Units.Kilometer, false);
            //addDistance(4, Length.Units.Mile, false);
            //addDistance(8, Length.Units.Kilometer, false);
            addDistance(5, Length.Units.Mile, false);
            addDistance(10, Length.Units.Kilometer, false);
            //addDistance(15, Length.Units.Kilometer, false);
            addDistance(10, Length.Units.Mile, false);
            addDistance(20, Length.Units.Kilometer, false);
            addDistance(21097.5, Length.Units.Meter, true);
            //addDistance(15, Length.Units.Mile, false);
            addDistance(25, Length.Units.Kilometer, false);
            addDistance(30, Length.Units.Kilometer, false);
            addDistance(20, Length.Units.Mile, false);
            //addDistance(25, Length.Units.Mile, false);
            addDistance(42195, Length.Units.Meter, true);
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

            attr = pluginNode.GetAttribute(xmlTags.showPrediction);
            if (attr.Length > 0) { showPrediction = XmlConvert.ToBoolean(attr); }
            attr = pluginNode.GetAttribute(xmlTags.showPace);
            if (attr.Length > 0) { showPace = XmlConvert.ToBoolean(attr); }
            attr = pluginNode.GetAttribute(xmlTags.showChart);
            if (attr.Length > 0) { showChart = XmlConvert.ToBoolean(attr); }
            attr = pluginNode.GetAttribute(xmlTags.percentOfDistance);
            if (attr.Length > 0) { percentOfDistance = XmlConvert.ToInt16(attr); }

            attr = pluginNode.GetAttribute(xmlTags.model);
            if (attr.Length > 0) { model = (PredictionModel)Enum.Parse(typeof(PredictionModel), attr); }
            attr = pluginNode.GetAttribute(xmlTags.distances);
            if (attr.Length > 0) { distances = parseDistances(attr.Split(';')); }

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

            pluginNode.SetAttribute(xmlTags.showPrediction, XmlConvert.ToString(showPrediction));
            pluginNode.SetAttribute(xmlTags.showPace, XmlConvert.ToString(showPace));
            pluginNode.SetAttribute(xmlTags.showChart, XmlConvert.ToString(showChart));
            pluginNode.SetAttribute(xmlTags.percentOfDistance, XmlConvert.ToString(percentOfDistance));

            pluginNode.SetAttribute(xmlTags.model, model.ToString());
            String att = "";
            bool first = true;
            foreach (double d in distances.Keys)
            {
                if (first) first = false;
                else att +=";";
                att += d.ToString(NumberFormatInfo.InvariantInfo) + " ";
                att += distances[d].Keys[0].ToString() + " ";
                att += distances[d].Values[0].ToString();
            }
            pluginNode.SetAttribute(xmlTags.distances, att);
            pluginNode.SetAttribute(xmlTags.viewWidth, XmlConvert.ToString(windowSize.Width));
            pluginNode.SetAttribute(xmlTags.viewHeight, XmlConvert.ToString(windowSize.Height));
        }

        private static int settingsVersion = 0; //default when not existing
        private const int settingsVersionCurrent = 1;

        private class xmlTags
        {
            public const string settingsVersion = "settingsVersion";
            public const string showPrediction = "showPrediction";
            public const string showPace = "showPace";
            public const string showChart = "showChart";
            public const string model = "model";
            public const string percentOfDistance = "percentOfDistance";
            public const string distances = "distances";

            public const string viewWidth = "viewWidth";
            public const string viewHeight = "viewHeight";
        }

        private static bool load()
        {
            //Backwards compatibility, read old preferences file
            String prefsPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + Path.DirectorySeparatorChar + "PerformancePredictorPlugin" + Path.DirectorySeparatorChar + "preferences.xml";

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
                return false;
            }
            reader.Close();
            return true;
        }

        private static Type getPlugin(string plugin, string klass)
        {
            try
            {
                //List the assemblies in the current application domain
                AppDomain currentDomain = AppDomain.CurrentDomain;
                Assembly[] assems = currentDomain.GetAssemblies();

                foreach (Assembly assem in assems)
                {
                    AssemblyName assemName = new AssemblyName((assem.FullName));
                    if (assemName.Name.Equals(plugin + "Plugin"))
                    {
                        return assem.GetType(klass);
                    }
                }
            }
            catch (Exception) { }
            return null;
        }

        public static void addDistance(double d, Length.Units metric, bool userDefined)
        {
            if (!userDefined)
            {
                d = Length.Convert(d, metric, Length.Units.Meter);
            }
            SortedList<Length.Units, bool> list = new SortedList<Length.Units, bool>();
            list.Add(metric, userDefined);
            if (!Settings.distances.ContainsKey(d))
            {
                distances.Add(d, list);
            }
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

        private static SortedList<double, SortedList<Length.Units, bool>> parseDistances(string[] p)
        {
            SortedList<double, SortedList<Length.Units, bool>> dlist = new SortedList<double, SortedList<Length.Units, bool>>();
            foreach (String s in p)
            {
                string[] split = s.Split(' ');
                double distance = parseDouble(split[0]);
                Length.Units unit = (Length.Units)Enum.Parse(typeof(Length.Units), split[1]);
                bool userDefined = bool.Parse(split[2]);
                SortedList<Length.Units, bool> list = new SortedList<Length.Units, bool>();
                list.Add(unit, userDefined);
                dlist.Add(distance, list);
            }
            return dlist;
        }

        private static double parseDouble(string p)
        {
            //if (!p.Contains(".")) p += ".0";
            double d = double.Parse(p, NumberFormatInfo.InvariantInfo);
            return d;
        }

        public static event System.ComponentModel.PropertyChangedEventHandler DistanceChanged;
    }

    public enum PredictionModel
    {
        DAVE_CAMERON, PETE_RIEGEL
    }
}
