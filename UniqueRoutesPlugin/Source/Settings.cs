/*
Copyright (C) 2007, 2008 Kristian Bisgaard Lassen 
Copyright (C) 2010 Kristian Helkjaer Lassen
Copyright (C) 2010 Gerhard Olsson 

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
using System.ComponentModel;

using ZoneFiveSoftware.Common.Data.Fitness;
using ZoneFiveSoftware.Common.Data.Measurement;
using ZoneFiveSoftware.Common.Visuals;
using GpsRunningPlugin.Properties;

namespace SportTracksUniqueRoutesPlugin.Source
{
    //Compatibility - namespace changed
    class Settings
    {
        public static bool HasDirection
        {
            get { return GpsRunningPlugin.Source.Settings.HasDirection; }
            set { GpsRunningPlugin.Source.Settings.HasDirection = value; }
        }
    }
}
namespace GpsRunningPlugin.Source
{
    public class SendToPlugin
    {
        public string name;
        public string id;
        public Type pType;
        public object[] par;
        public SendToPlugin(string id, string name, string path, object [] par)
        {
            this.id = id;
            this.name = name;
            pType = getPlugin(id, path);
            this.par = par;
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
    }

    class Settings
    {
        public readonly static IList<SendToPlugin> aSendToPlugin = new List<SendToPlugin>(){
            new SendToPlugin("AccumulatedSummary", "Accumulated Summary", "GpsRunningPlugin.Source.AccumulatedSummaryView", new object[] { null, null }),
            new SendToPlugin("HighScore", "High Score", "GpsRunningPlugin.Source.HighScoreViewer", new object[] { null, null }),
            new SendToPlugin("Overlay", "Overlay", "GpsRunningPlugin.Source.OverlayView", new object[] { null, null }),
            new SendToPlugin("PerformancePredictor", "Performance Predictor", "GpsRunningPlugin.Source.PerformancePredictorView", new object[] { null, null }),
            new SendToPlugin("TRIMP", "TRIMP", "GpsRunningPlugin.Source.TRIMPView", new object[] { null, null })};
            //new SendToPlugin("AccumulatedSummary", "Accumulated Summary", "GpsRunningPlugin.Source.AccumulatedSummaryView", new object[] { null }),
            //new SendToPlugin("HighScore", "High Score", "GpsRunningPlugin.Source.HighScoreViewer", new object[] { null, true, true }),
            //new SendToPlugin("Overlay", "Overlay", "GpsRunningPlugin.Source.OverlayView", new object[] { null, true }),
            //new SendToPlugin("PerformancePredictor", "Performance Predictor", "GpsRunningPlugin.Source.PerformancePredictorView", new object[] { null, true }),
            //new SendToPlugin("TRIMP", "TRIMP", "GpsRunningPlugin.Source.TRIMPView", new object[] { null, true })};
        static Settings()
        {
            defaults();
        }
        
        private static String selectedPlugin;
        public static String SelectedPlugin
        {
            get { return selectedPlugin; }
            set { selectedPlugin = value; }
        }

        private static IActivityCategory selectedCategory;
        private static string categoryTmpStr = null;
        public static IActivityCategory SelectedCategory
        {
            get
            {
                if (null == selectedCategory && null != categoryTmpStr)
                {
                    //Race problem at startup: Application is not directly accessible, save temp string
                    selectedCategory = parseCategory(categoryTmpStr);
                    categoryTmpStr = null;
                }
                return selectedCategory;
            }
            set { selectedCategory = value; }
        }

        private static double errorMargin;
        public static double ErrorMargin
        {
            get { return errorMargin; }
            set { errorMargin = value; }
        }

        private static int radius;
        public static int Radius
        {
            get { return radius; }
            set { radius = value; }
        }

        private static bool hasDirection;
        public static bool HasDirection
        {
            get { return hasDirection; }
            set { hasDirection = value; }
        }

        private static bool useActive;
        public static bool UseActive
        {
            get { return useActive; }
            set { useActive = value; }
        }

        private static double ignoreBeginning;
        public static double IgnoreBeginning
        {
            get { return ignoreBeginning; }
            set { ignoreBeginning = value; }
        }

        private static double ignoreEnd;
        public static double IgnoreEnd
        {
            get { return ignoreEnd; }
            set { ignoreEnd = value; }
        }

        private static bool selectAll;
        public static bool SelectAll
        {
            get { return selectAll; }
            set { selectAll = value; }
        }

        private static bool showPace;
        public static bool ShowPace
        {
            get { return showPace; }
            set { showPace = value; }
        }

        private static string summaryViewSortColumn;
        public static string SummaryViewSortColumn
        {
            get { return summaryViewSortColumn; }
            set { summaryViewSortColumn = value; }
        }

        private static ListSortDirection summaryViewSortDirection;
        public static ListSortDirection SummaryViewSortDirection
        {
            get { return summaryViewSortDirection; }
            set { summaryViewSortDirection = value; }
        }

        private static Size windowSize; //viewWidth, viewHeight in xml
        public static Size WindowSize
        {
            get { return windowSize; }
            set { windowSize = value; }
        }
        private static IList<string> m_activityPageColumns;
        public static IList<string> ActivityPageColumns
        {
            get
            {
                //Consistency control, when loading only
                IList<string> result = new List<string>();
                ICollection<IListColumnDefinition> allCols = SummaryColumnIds.ColumnDefs(null);
                foreach (string id in m_activityPageColumns)
                {
                    if (!result.Contains(id))
                    {
                        foreach (ListColumnDefinition columnDef in allCols)
                        {
                            if (columnDef.Id == id)
                            {
                                result.Add(id);
                                break;
                            }
                        }
                    }
                }
                return result;
            }
            set
            {
                m_activityPageColumns = value;
            }
        }

        public static void defaults()
        {
            selectedPlugin = "";
            selectedCategory = null;
            errorMargin = 0.1;
            radius = 20;
            hasDirection = false;
            useActive = false;
            ignoreBeginning = 0;
            ignoreEnd = 0;
            selectAll = true;
            summaryViewSortColumn = SummaryColumnIds.StartDate;
            summaryViewSortDirection = ListSortDirection.Ascending;
            windowSize = new Size(800, 600);

            m_activityPageColumns = new List<string>();
            m_activityPageColumns.Add(SummaryColumnIds.StartDate);
            m_activityPageColumns.Add(SummaryColumnIds.StartTime);
            m_activityPageColumns.Add(SummaryColumnIds.Time);
            m_activityPageColumns.Add(SummaryColumnIds.Distance);
            m_activityPageColumns.Add(SummaryColumnIds.AvgSpeedPace);
            m_activityPageColumns.Add(SummaryColumnIds.AvgHR);
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

            attr = pluginNode.GetAttribute(xmlTags.selectedPlugin);
            if (attr.Length > 0) { selectedPlugin = attr; }
            attr = pluginNode.GetAttribute(xmlTags.selectedCategory);
            if (attr.Length > 0) { selectedCategory = parseCategory(attr); }
            attr = pluginNode.GetAttribute(xmlTags.errorMargin);
            if (attr.Length > 0) { errorMargin = (float)XmlConvert.ToDouble(attr); }
            attr = pluginNode.GetAttribute(xmlTags.radius);
            if (attr.Length > 0) { radius = XmlConvert.ToInt16(attr); }
            else
            { //Compatibility
                attr = pluginNode.GetAttribute(xmlTags.bandwidth);
                if (attr.Length > 0) { radius = XmlConvert.ToInt16(attr)/2; }
            }
            attr = pluginNode.GetAttribute(xmlTags.hasDirection);
            if (attr.Length > 0) { hasDirection = XmlConvert.ToBoolean(attr); }
            attr = pluginNode.GetAttribute(xmlTags.useActive);
            if (attr.Length > 0) { useActive = XmlConvert.ToBoolean(attr); }
            attr = pluginNode.GetAttribute(xmlTags.ignoreBeginning);
            if (attr.Length > 0) { ignoreBeginning = (float)XmlConvert.ToDouble(attr); }
            attr = pluginNode.GetAttribute(xmlTags.ignoreEnd);
            if (attr.Length > 0) { ignoreEnd = (float)XmlConvert.ToDouble(attr); }
            attr = pluginNode.GetAttribute(xmlTags.selectAll);
            if (attr.Length > 0) { selectAll = XmlConvert.ToBoolean(attr); }
            attr = pluginNode.GetAttribute(xmlTags.showPace);
            if (attr.Length > 0) { showPace = XmlConvert.ToBoolean(attr); }
            attr = pluginNode.GetAttribute(xmlTags.summaryViewSortColumn);
            if (attr.Length > 0) { summaryViewSortColumn = attr; }
            attr = pluginNode.GetAttribute(xmlTags.summaryViewSortDirection);
            if (attr.Length > 0) { summaryViewSortDirection = (ListSortDirection)Enum.Parse(typeof(ListSortDirection), attr); }
            attr = pluginNode.GetAttribute(xmlTags.viewWidth);
            attr2 = pluginNode.GetAttribute(xmlTags.viewHeight);
            if (attr.Length > 0 && attr2.Length > 0)
            {
                windowSize = new Size(XmlConvert.ToInt16(attr), XmlConvert.ToInt16(attr2));
            }
            attr = pluginNode.GetAttribute(xmlTags.sColumns);
            if (attr.Length > 0)
            {
                m_activityPageColumns.Clear();
                String[] values = attr.Split(';');
                foreach (String column in values)
                {
                    m_activityPageColumns.Add(column);
                }
            }
        }

        public static void WriteOptions(XmlDocument xmlDoc, XmlElement pluginNode)
        {
            pluginNode.SetAttribute(xmlTags.settingsVersion, XmlConvert.ToString(settingsVersionCurrent));

            pluginNode.SetAttribute(xmlTags.selectedPlugin, selectedPlugin);
            pluginNode.SetAttribute(xmlTags.selectedCategory, printFullCategoryPath(selectedCategory,null, "|"));
            pluginNode.SetAttribute(xmlTags.errorMargin, XmlConvert.ToString(errorMargin));
            pluginNode.SetAttribute(xmlTags.radius, XmlConvert.ToString(radius));
            pluginNode.SetAttribute(xmlTags.hasDirection, XmlConvert.ToString(hasDirection));
            pluginNode.SetAttribute(xmlTags.useActive, XmlConvert.ToString(useActive));
            pluginNode.SetAttribute(xmlTags.ignoreBeginning, XmlConvert.ToString(ignoreBeginning));
            pluginNode.SetAttribute(xmlTags.ignoreEnd, XmlConvert.ToString(ignoreEnd));
            pluginNode.SetAttribute(xmlTags.selectAll, XmlConvert.ToString(selectAll));
            pluginNode.SetAttribute(xmlTags.showPace, XmlConvert.ToString(showPace));
            pluginNode.SetAttribute(xmlTags.summaryViewSortColumn, summaryViewSortColumn);
            pluginNode.SetAttribute(xmlTags.summaryViewSortDirection, summaryViewSortDirection.ToString());
            pluginNode.SetAttribute(xmlTags.viewWidth, XmlConvert.ToString(windowSize.Width));
            pluginNode.SetAttribute(xmlTags.viewHeight, XmlConvert.ToString(windowSize.Height));
            String colText = null;
            foreach (String column in m_activityPageColumns)
            {
                if (colText == null) { colText = column; }
                else { colText += ";" + column; }
            }
            pluginNode.SetAttribute(xmlTags.sColumns, colText);
        }

        private static int settingsVersion = 0; //default when not existing
        private const int settingsVersionCurrent = 1;

        private class xmlTags
        {
            public const string settingsVersion = "settingsVersion";
            public const string Verbose = "Verbose";

            public const string selectedPlugin = "selectedPlugin";
            public const string selectedCategory = "selectedCategory";
            public const string errorMargin = "errorMargin";
            public const string bandwidth = "bandwidth";//Compatibility
            public const string radius = "radius";
            public const string hasDirection = "hasDirection";
            public const string useActive = "useActive";
            public const string ignoreBeginning = "ignoreBeginning";
            public const string ignoreEnd = "ignoreEnd";
            public const string selectAll = "selectAll";
            public const string showPace = "showPace";
            public const string summaryViewSortColumn = "summaryViewSortColumn";
            public const string summaryViewSortDirection = "summaryViewSortDirection";

            public const string viewWidth = "viewWidth";
            public const string viewHeight = "viewHeight";
            public const string sColumns = "sColumns";
        }

        private static bool load()
        {
            //Backwards compatibility, read old preferences file
            String prefsPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + Path.DirectorySeparatorChar + "UniqueRoutesPlugin" + Path.DirectorySeparatorChar + "preferences.xml";

            if (!File.Exists(prefsPath)) return false;
            XmlDocument document = new XmlDocument();
            XmlReader reader = new XmlTextReader(prefsPath);
            document.Load(reader);
            try
            {
                XmlNode elm = document.ChildNodes[0]["view"];
                windowSize = new Size(int.Parse(elm.Attributes["viewWidth"].Value),
                                                    int.Parse(elm.Attributes["viewHeight"].Value));
                errorMargin = GpsRunningPlugin.Source.Settings.parseDouble(elm.Attributes["errorMargin"].Value);
                radius = int.Parse(elm.Attributes["bandwidth"].Value)/2;
                hasDirection = bool.Parse(elm.Attributes["hasDirection"].Value);
                if (elm.Attributes["ignoreBeginning"] != null)
                    ignoreBeginning = GpsRunningPlugin.Source.Settings.parseDouble(elm.Attributes["ignoreBeginning"].Value);
                if (elm.Attributes["ignoreEnd"] != null)
                    ignoreEnd = GpsRunningPlugin.Source.Settings.parseDouble(elm.Attributes["ignoreEnd"].Value);
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

        public static string printFullCategoryPath(IActivityCategory iActivityCategory)
        {
            return printFullCategoryPath(iActivityCategory, null, ": ");
        }
        private static string printFullCategoryPath(IActivityCategory iActivityCategory, string p, string sep)
        {
            if (iActivityCategory == null) return p;
            if (p == null) return printFullCategoryPath(iActivityCategory.Parent,
                                        iActivityCategory.Name, sep);
            return printFullCategoryPath(iActivityCategory.Parent,
                                iActivityCategory.Name + sep + p, sep);
        }
        private static IActivityCategory parseCategory(string p)
        {
            if (p == null || p.Equals("")) return null;
            string[] ps = p.Split('|');
            IActivityCategory cat = null;
            if (null == Plugin.GetApplication().Logbook)
            {
                //Cannot parse right now, save value for later
                categoryTmpStr = p;
            }
            else
            {
                cat = GpsRunningPlugin.Source.Settings.getCategory(ps, 0, Plugin.GetApplication().Logbook.ActivityCategories);
            }
            return cat;
        }
        public static IActivityCategory getCategory(string[] ps, int p, IEnumerable<IActivityCategory> iList)
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
        private static double parseDouble(string p)
        {
            //if (!p.Contains(".")) p += ".0";
            double d = double.Parse(p, NumberFormatInfo.InvariantInfo);
            return d;
        }
    }
}
