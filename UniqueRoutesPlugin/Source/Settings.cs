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
using System.Reflection;
using ZoneFiveSoftware.Common.Data.Fitness;
using ZoneFiveSoftware.Common.Data.Measurement;
using SportTracksUniqueRoutesPlugin.Properties;

namespace SportTracksUniqueRoutesPlugin.Source
{
    class Settings
    {
        public readonly static String prefsPath;

        public readonly static Type accumulatedSummary,highScore,overlay,
            trimp;

        private static String selectedPlugin;
        public static String SelectedPlugin
        {
            get { return selectedPlugin; }
            set
            {
                selectedPlugin = value;
                save();
            }
        }

        private static IActivityCategory selectedCategory;
        public static IActivityCategory SelectedCategory
        {
            get { return selectedCategory; }
            set
            {
                selectedCategory = value;
                save();
            }
        }

        private static double errorMargin;
        public static double ErrorMargin
        {
            get { return errorMargin; }
            set
            {
                errorMargin = value;
                save();
            }
        }

        private static int bandwidth;
        public static int Bandwidth
        {
            get { return bandwidth; }
            set
            {
                bandwidth = value;
                save();
            }
        }

        private static bool hasDirection;
        public static bool HasDirection
        {
            get { return hasDirection; }
            set
            {
                hasDirection = value;
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

        private static double ignoreBeginning;
        public static double IgnoreBeginning
        {
            get { return ignoreBeginning; }
            set
            {
                ignoreBeginning = value;
                save();
            }
        }

        private static double ignoreEnd;
        public static double IgnoreEnd
        {
            get { return ignoreEnd; }
            set
            {
                ignoreEnd = value;
                save();
            }
        }

        private static bool selectAll;
        public static bool SelectAll
        {
            get { return selectAll; }
            set
            {
                selectAll = value;
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

        public static bool dontSave = false;

        public static void reset()
        {
            errorMargin = 0.1;
            bandwidth = 40;
            hasDirection = false;
            ignoreBeginning = 0;
            ignoreEnd = 0;
            windowSize = new Size(800, 600);
            selectAll = true;
            selectedPlugin = "";
            selectedCategory = null;
            save();
        }

        static Settings()
        {
            prefsPath = Environment.GetEnvironmentVariable("APPDATA") + "/UniqueRoutesPlugin/preferences.xml";
            if (!load())
            {
                Directory.CreateDirectory(Environment.GetEnvironmentVariable("APPDATA") + "/UniqueRoutesPlugin/");
                reset();
            }
            accumulatedSummary = getPlugin("AccumulatedSummary", "SportTracksAccumulatedSummaryPlugin.Source.AccumulatedSummaryView");
            highScore = getPlugin("HighScore","SportTracksHighScorePlugin.Source.HighScoreViewer");
            overlay = getPlugin("Overlay", "SportTracksOverlayPlugin.Source.OverlayView");
            trimp = getPlugin("TRIMP", "SportTracksTRIMPPlugin.Source.TRIMPView");
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
                    path = getPath(plugin,pluginPath);
                else
                    path = getPath(plugin,Directory.GetCurrentDirectory() + "/Plugins/");
                Assembly assembly = Assembly.LoadFrom(path);
                return assembly.GetType(klass);
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
                path = getPath(plugin,sub);
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
                errorMargin = parseDouble(elm.Attributes["errorMargin"].Value);
                bandwidth = int.Parse(elm.Attributes["bandwidth"].Value);
                hasDirection = bool.Parse(elm.Attributes["hasDirection"].Value);
                if (elm.Attributes["ignoreBeginning"] != null)
                    ignoreBeginning = parseDouble(elm.Attributes["ignoreBeginning"].Value);
                if (elm.Attributes["ignoreEnd"] != null)
                    ignoreEnd = parseDouble(elm.Attributes["ignoreEnd"].Value);
                selectAll = bool.Parse(elm.Attributes["selectAll"].Value);
                selectedCategory = parseCategory(elm.Attributes["selectedCategory"].Value);
                selectedPlugin = elm.Attributes["selectedPlugin"].Value;
            }
            catch (Exception)
            {
                reader.Close();
                return false;
            }
            reader.Close();
            return true;
        }

        private static IActivityCategory parseCategory(string p)
        {
            if (p.Equals("")) return null;
            string[] ps = p.Split('|');
            return getCategory(ps, 0,Plugin.GetApplication().Logbook.ActivityCategories);
        }

        private static IActivityCategory getCategory(string[] ps, int p, IList<IActivityCategory> iList)
        {
            if (iList == null) return null;
            foreach (IActivityCategory category in iList)
            {
                if (category.Name.Equals(ps[p]))
                {
                    if (p == ps.Length - 1)
                    {
                        return category;
                    }
                    return getCategory(ps, p + 1, category.SubCategories);
                }
            }
            return null;
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
            if (PropertyChanged != null)
                PropertyChanged.Invoke(null, null);
            XmlDocument document = new XmlDocument();
            XmlElement root = document.CreateElement("uniqueRoutes");
            document.AppendChild(root);

            XmlElement resultSetupElm = document.CreateElement("view");
            root.AppendChild(resultSetupElm);
            resultSetupElm.SetAttribute("errorMargin", errorMargin.ToString(NumberFormatInfo.InvariantInfo));
            resultSetupElm.SetAttribute("bandwidth", bandwidth.ToString(NumberFormatInfo.InvariantInfo));
            resultSetupElm.SetAttribute("hasDirection", hasDirection.ToString());
            resultSetupElm.SetAttribute("ignoreBeginning", ignoreBeginning.ToString());
            resultSetupElm.SetAttribute("ignoreEnd", ignoreEnd.ToString());
            resultSetupElm.SetAttribute("viewWidth", windowSize.Width.ToString());
            resultSetupElm.SetAttribute("viewHeight", windowSize.Height.ToString());
            resultSetupElm.SetAttribute("selectAll", selectAll.ToString());
            resultSetupElm.SetAttribute("selectedCategory", printFullPath(selectedCategory));
            resultSetupElm.SetAttribute("selectedPlugin", selectedPlugin);
            resultSetupElm.SetAttribute("showPace", showPace.ToString());

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

        private static string printFullPath(IActivityCategory selectedCategory)
        {
            String str = "";
            bool first = true;
            while (selectedCategory != null)
            {
                if (first) first = false;
                else str = "|" + str;
                str = selectedCategory.Name + str;
                selectedCategory = selectedCategory.Parent;
            }
            return str;
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

        public static double convertTo(double p, Length.Units metric)
        {
            switch (metric)
            {
                case Length.Units.Kilometer: return p * 1000;
                case Length.Units.Mile: return p * (1.609344 * 1000);
                case Length.Units.Foot: return p / 3.2808399;
                case Length.Units.Inch: return p / 39.370079;
                case Length.Units.Centimeter: return p / 100;
                case Length.Units.Yard: return p / 1.0936133;
            }
            return p;
        }

        public static double convertToDistance(double p)
        {
            return convertTo(p, Plugin.GetApplication().SystemPreferences.DistanceUnits);
        }

        public static double convertToElevation(double p)
        {
            return convertTo(p, Plugin.GetApplication().SystemPreferences.ElevationUnits);
        }

        public static string present(double p)
        {
            return String.Format("{0:0.000}", p);
        }

        public static string present(double p, int decimals)
        {
            string s = "";
            for (int i = 0; i < decimals; i++) s += "0";
            return String.Format("{0:0." + s + "}", p);
        }

        #region INotifyPropertyChanged Members

        public static event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;

        #endregion

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
