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
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using ZoneFiveSoftware.Common.Visuals;
using ZoneFiveSoftware.Common.Data.Fitness;
using ZoneFiveSoftware.Common.Data.Measurement;
using System.Reflection;
using GpsRunningPlugin.Properties;
using GpsRunningPlugin.Util;
#if !ST_2_1
using ZoneFiveSoftware.Common.Data;
#endif
using ZoneFiveSoftware.Common.Visuals.Fitness;
using ZoneFiveSoftware.Common.Visuals.Mapping;
using TrailsPlugin;
using TrailsPlugin.Data;
using TrailsPlugin.Utils;
using TrailsPlugin.UI.MapLayers;

namespace GpsRunningPlugin.Source
{
    public partial class ExtrapolateView : UserControl
    {
#if ST_2_1
        private const object m_DetailPage = null;
#else
        private IDetailPage m_DetailPage = null;
        private IDailyActivityView m_view = null;
        private TrailPointsLayer m_layer = null;
#endif
        private PerformancePredictorControl m_ppcontrol = null;

        public ExtrapolateView()
        {
            InitializeComponent();
        }

        public void InitControls(IDetailPage detailPage, IDailyActivityView view, TrailPointsLayer layer, PerformancePredictorControl ppControl)
        {
#if !ST_2_1
            m_DetailPage = detailPage;
            m_view = view;
            m_layer = layer;
#endif
            m_ppcontrol = ppControl;

            //Set in user code rather than generated code, to make GUI editing possible
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;

            copyTableMenuItem.Image = ZoneFiveSoftware.Common.Visuals.CommonResources.Images.DocumentCopy16;

            temperatureList.LabelProvider = new TemperatureLabelProvider();
            weightList.LabelProvider = new WeightLabelProvider();
            temperatureList.Columns.Clear();
            foreach (string id in ResultColumnIds.TemperatureColumns)
            {
                foreach (IListColumnDefinition columnDef in ResultColumnIds.ColumnDefs())
                {
                    if (columnDef.Id == id)
                    {
                        TreeList.Column column = new TreeList.Column(
                            columnDef.Id,
                            columnDef.Text(columnDef.Id),
                            columnDef.Width,
                            columnDef.Align
                        );
                        temperatureList.Columns.Add(column);
                        break;
                    }
                }
            }
            weightList.Columns.Clear();
            foreach (string id in ResultColumnIds.WeightColumns)
            {
                foreach (IListColumnDefinition columnDef in ResultColumnIds.ColumnDefs())
                {
                    if (columnDef.Id == id)
                    {
                        TreeList.Column column = new TreeList.Column(
                            columnDef.Id,
                            columnDef.Text(columnDef.Id),
                            columnDef.Width,
                            columnDef.Align
                        );
                        weightList.Columns.Add(column);
                        break;
                    }
                }
            }
        }

        private void SystemPreferences_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (m_showPage)
            {
                RefreshData();
            }
        }

#if ST_2_1
        private void Athlete_DataChanged(object sender, ZoneFiveSoftware.Common.Data.NotifyDataChangedEventArgs e)
        {
            if (m_showPage)
            {
                setPages();
            }
        }

        private void Logbook_DataChanged(object sender, ZoneFiveSoftware.Common.Data.NotifyDataChangedEventArgs e)
        {
            if (m_showPage)
            {
                setPages();
            }
        }
#else
        private void Athlete_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (m_showPage)
            {
                RefreshData();
            }
        }
        private void Logbook_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (m_showPage)
            {
                RefreshData();
            }
        }
#endif

        private ITheme m_visualTheme;
        public void ThemeChanged(ITheme visualTheme)
        {
            m_visualTheme = visualTheme;
            Color bColor = visualTheme.Control;
            Color fColor = visualTheme.ControlText;

            //Set color for non ST controls
            this.BackColor = bColor;

            foreach (TabPage tab in this.tabControl1.TabPages)
            {
                tab.BackColor = bColor;
                tab.ForeColor = fColor;
                //Note: Tabs are not changed.
                //Requires DrawMode set to OwnerDraw, DrawItem implemented
                foreach (Control tablePanel in tab.Controls)
                {
                    foreach (Control grid0 in tablePanel.Controls)
                    {
                        if (grid0 is TreeList)
                        {
                            (grid0 as TreeList).ThemeChanged(visualTheme);
                        }
                    }
                }
            }
        }

        public void UICultureChanged(System.Globalization.CultureInfo culture)
        {
            this.temperatureTab.Text = Resources.TemperatureImpact;
            this.weightTab.Text = Resources.WeighImpact;

            //temperatureLabel2.Text
            temperatureLabel2.Text = String.Format(Resources.TemperatureNotification, UnitUtil.Temperature.ToString(16, "F0u"));
            //weightLabel.Text
            weightLabel2.Text = String.Format(Resources.WeightNotification, 2 + " " + StringResources.Seconds,
                UnitUtil.Distance.ToString(1000, "u"));

            copyTableMenuItem.Text = ZoneFiveSoftware.Common.Visuals.CommonResources.Text.ActionCopy;
        }

        private bool m_showPage = false;

        public void ShowPage(string bookmark)
        {
            m_showPage = true;
            RefreshData();
            activateListeners();
            this.Visible = true;
        }

        public bool HidePage()
        {
            m_showPage = false;
            this.Visible = false;
            deactivateListeners();
            return true;
        }

        private void activateListeners()
        {
            if (m_showPage)
            {
#if ST_2_1
                Plugin.GetApplication().Logbook.DataChanged += new ZoneFiveSoftware.Common.Data.NotifyDataChangedEventHandler(Logbook_DataChanged);
                Plugin.GetApplication().Logbook.Athlete.DataChanged += new ZoneFiveSoftware.Common.Data.NotifyDataChangedEventHandler(Athlete_DataChanged);
#else
                Plugin.GetApplication().Logbook.Athlete.PropertyChanged += new PropertyChangedEventHandler(Athlete_PropertyChanged);
                Plugin.GetApplication().Logbook.PropertyChanged += new PropertyChangedEventHandler(Logbook_PropertyChanged);
#endif
                Plugin.GetApplication().SystemPreferences.PropertyChanged += new PropertyChangedEventHandler(SystemPreferences_PropertyChanged);
            }
        }

        private void deactivateListeners()
        {
#if ST_2_1
            Plugin.GetApplication().Logbook.DataChanged -= new ZoneFiveSoftware.Common.Data.NotifyDataChangedEventHandler(Logbook_DataChanged);
            Plugin.GetApplication().Logbook.Athlete.DataChanged -= new ZoneFiveSoftware.Common.Data.NotifyDataChangedEventHandler(Athlete_DataChanged);
#else
            Plugin.GetApplication().Logbook.Athlete.PropertyChanged -= new PropertyChangedEventHandler(Athlete_PropertyChanged);
            Plugin.GetApplication().Logbook.PropertyChanged -= new PropertyChangedEventHandler(Logbook_PropertyChanged);
#endif
            Plugin.GetApplication().SystemPreferences.PropertyChanged -= new PropertyChangedEventHandler(SystemPreferences_PropertyChanged);
        }

        public void RefreshData()
        {
            if (m_showPage && m_ppcontrol.SingleActivity != null && Predict.Predictor(Settings.Model) != null)
            {
                setTemperature();
                setWeight();
            }
        }

        private void setWeight()
        {
            double weight = Plugin.GetApplication().Logbook.Athlete.InfoEntries.LastEntryAsOfDate(m_ppcontrol.SingleActivity.StartTime).WeightKilograms;
            if (weight.Equals(double.NaN))
            {
                weightLabel.Text = Resources.SetWeight;
                weightLabel2.Visible = false;
                return;
            }
            weightLabel2.Visible = true;
            ActivityInfo info = ActivityInfoCache.Instance.GetInfo(m_ppcontrol.SingleActivity);
            weightLabel.Text = Resources.ProjectedWeightImpact + " " +
                UnitUtil.Distance.ToString(info.DistanceMeters, "u");
            TimeSpan time = info.Time;
            const double inc = 1.4;
            double vdot = Predict.getVdot(m_ppcontrol.SingleActivity);
            IList<WeightResult> result = new List<WeightResult>();
            WeightResult sel = null;
            for (int i = 0; i < 13; i++)
            {
                WeightResult t = new WeightResult(m_ppcontrol.SingleActivity, 6 - i, vdot, weight, inc, time, info);
                result.Add(t);
                if (t.Weight > weight)
                {
                    sel = t;
                }
            }
            weightList.RowData = result;
            weightList.SelectedItems = new List<WeightResult>{sel};
        }

        private void setTemperature()
        {
            ActivityInfo info = ActivityInfoCache.Instance.GetInfo(m_ppcontrol.SingleActivity);
            TimeSpan time = info.Time;
            temperatureLabel.Text = Resources.ProjectedTemperatureImpact+" "+UnitUtil.Distance.ToString(info.DistanceMeters,"u");
            double speed = info.DistanceMeters / time.TotalSeconds;
            float actualTemp = m_ppcontrol.SingleActivity.Weather.TemperatureCelsius;
            if (!TemperatureResult.isValidtemperature(actualTemp)) { actualTemp = 15; }
            double[] aTemperature = TemperatureResult.aTemperature;
            
            IList<TemperatureResult> result = new List<TemperatureResult>();
            TemperatureResult sel = null;
            for (int i = 0; i < aTemperature.Length; i++)
            {
                TemperatureResult t = new TemperatureResult(m_ppcontrol.SingleActivity, aTemperature[i], actualTemp, time, speed);
                result.Add(t);
                if ((i == 0 || actualTemp >= aTemperature[i - 1]) && (actualTemp < aTemperature[i]))
                {
                    sel = t;
                }
            }
            temperatureList.RowData = result;
            temperatureList.SelectedItems = new List<TemperatureResult>{sel};
        }
    }
}
