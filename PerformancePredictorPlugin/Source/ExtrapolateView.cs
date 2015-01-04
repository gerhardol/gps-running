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
using System.Globalization;
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
        private float? m_actualTemp = null;
        private float? m_actualWeight = null;
        private float? m_actualShoe = null;
        private float? m_actualAge = null;
        private float m_idealTemp;
        private float m_idealWeight;
        private float m_idealShoe;
        private float m_idealAge;
        private float m_oldDistance; 
        private IActivity m_activity = null; //Just to chack if variables should be cleared

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
            shoeList.LabelProvider = new ShoeLabelProvider();
            ageList.LabelProvider = new AgeLabelProvider();
        }

        private void updateColumns()
        {
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

            shoeList.Columns.Clear();
            foreach (string id in ResultColumnIds.ShoeColumns)
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
                        shoeList.Columns.Add(column);
                        break;
                    }
                }
            }

            ageList.Columns.Clear();
            foreach (string id in ResultColumnIds.AgeColumns)
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
                        ageList.Columns.Add(column);
                        break;
                    }
                }
            }
        }

        private void SystemPreferences_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            this.m_actualTemp = null;
            this.m_actualWeight = null;
            if (this.InvokeRequired)
            {
                this.Invoke((System.ComponentModel.PropertyChangedEventHandler)SystemPreferences_PropertyChanged, sender, e);
            }
            else
            {
                if (m_showPage)
                {
                    RefreshData();
                }
            }
        }

#if ST_2_1
        private void Athlete_DataChanged(object sender, ZoneFiveSoftware.Common.Data.NotifyDataChangedEventArgs e)
#else
        private void Athlete_PropertyChanged(object sender, PropertyChangedEventArgs e)
#endif
        {
            this.m_actualWeight = null;
            this.m_actualShoe = null;
            this.m_actualAge = null;
            if (this.InvokeRequired)
            {
                this.Invoke((System.ComponentModel.PropertyChangedEventHandler)Athlete_PropertyChanged, sender, e);
            }
            else
            {
                if (m_showPage)
                {
                    RefreshData();
                }
            }
        }
#if ST_2_1
        private void Logbook_DataChanged(object sender, ZoneFiveSoftware.Common.Data.NotifyDataChangedEventArgs e)
#else
        private void Logbook_PropertyChanged(object sender, PropertyChangedEventArgs e)
#endif
        {
            this.m_actualTemp = null;
            this.m_actualShoe = null;
            this.m_actualAge = null;
            if (this.InvokeRequired)
            {
                this.Invoke((System.ComponentModel.PropertyChangedEventHandler)Logbook_PropertyChanged, sender, e);
            }
            else
            {
                if (m_showPage)
                {
                    RefreshData();
                }
            }
        }

        private ITheme m_visualTheme;
        public void ThemeChanged(ITheme visualTheme)
        {
            this.m_visualTheme = visualTheme;
            Color bColor = visualTheme.Control;
            Color fColor = visualTheme.ControlText;

            this.timeBox.ThemeChanged(visualTheme);
            this.timeBox2.ThemeChanged(visualTheme);
            this.distBox.ThemeChanged(visualTheme);
            this.distBox2.ThemeChanged(visualTheme);
            this.paceBox.ThemeChanged(visualTheme);
            this.paceBox2.ThemeChanged(visualTheme);
            this.temperatureBox.ThemeChanged(visualTheme);
            this.temperatureBox2.ThemeChanged(visualTheme);
            this.weightBox.ThemeChanged(visualTheme);
            this.weightBox2.ThemeChanged(visualTheme);
            this.shoeBox.ThemeChanged(visualTheme);
            this.shoeBox2.ThemeChanged(visualTheme);
            this.ageBox.ThemeChanged(visualTheme);
            this.ageBox2.ThemeChanged(visualTheme);

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
            updateColumns();

            this.temperatureTab.Text = ZoneFiveSoftware.Common.Visuals.CommonResources.Text.LabelTemperature;
            this.weightTab.Text = ZoneFiveSoftware.Common.Visuals.CommonResources.Text.LabelWeight;
            this.shoeTab.Text = Resources.ShoeTab;
            this.ageTab.Text = ZoneFiveSoftware.Common.Visuals.CommonResources.Text.LabelEquipmentAge;
            this.idealTab.Text = Resources.IdealTab;

            //temperatureLabel2.Text
            temperatureLabel2.Text = String.Format(Resources.TemperatureNotification, 
                UnitUtil.Temperature.ToString(TemperatureResult.IdealTemperature, "F0u"));
            //weightLabel.Text
            weightLabel2.Text = String.Format(Resources.WeightNotification, 2 + " " + StringResources.Seconds,
                UnitUtil.Distance.ToString(1000, "u"));
            shoeLabel2.Text = String.Format(Resources.ShoeNotification);
            ageLabel2.Text = String.Format(Resources.AgeNotification);
            idealLabel2.Text = String.Format(Resources.IdealNotification);

            copyTableMenuItem.Text = ZoneFiveSoftware.Common.Visuals.CommonResources.Text.ActionCopy;

            idealActualLabel.Text = ZoneFiveSoftware.Common.Visuals.CommonResources.Text.LabelActivity;
            idealIdealLabel.Text = "Ideal"; //TBD
            idealTimeLabel.Text = ZoneFiveSoftware.Common.Visuals.CommonResources.Text.LabelTime;
            idealDistLabel.Text = ZoneFiveSoftware.Common.Visuals.CommonResources.Text.LabelDistance;
            idealPaceLabel.Text = UnitUtil.PaceOrSpeed.LabelAxis(Settings.ShowPace);
            idealTempLabel.Text = ZoneFiveSoftware.Common.Visuals.CommonResources.Text.LabelTemperature;
            idealWeightLabel.Text = ZoneFiveSoftware.Common.Visuals.CommonResources.Text.LabelWeight;
            idealShoeLabel.Text = Resources.ShoeTab;
            idealAgeLabel.Text = ZoneFiveSoftware.Common.Visuals.CommonResources.Text.LabelEquipmentAge;
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
                if (this.m_activity != this.m_ppcontrol.SingleActivity)
                {
                    m_actualTemp = null;
                    m_actualWeight = null;
                    m_actualShoe = null;
                    m_actualAge = null;
                    this.m_oldDistance = -1;
                    //Keep value in form, no varable
                    this.distBox2.LostFocus -= distBox_LostFocus;
                    this.distBox2.Text = "";
                    this.distBox2.LostFocus += distBox_LostFocus;
                }
                this.m_activity = this.m_ppcontrol.SingleActivity;
                setTemperature();
                setWeight();
                setShoe();
                setAge();
                setIdeal();
            }
        }

        private void setTemperature()
        {
            if (this.m_actualTemp == null || float.IsNaN((float)this.m_actualTemp))
            {
                this.m_actualTemp = m_ppcontrol.SingleActivity.Weather.TemperatureCelsius;
                this.m_idealTemp = TemperatureResult.IdealTemperature;
            }
            if (float.IsNaN((float)this.m_actualTemp))
            {
                this.m_actualTemp = TemperatureResult.DefaultTemperature;
                this.m_idealTemp = TemperatureResult.IdealTemperature;
            }

            temperatureLabel.Text = Resources.TemperatureProjectedImpact + " " + UnitUtil.Distance.ToString(m_ppcontrol.Distance, "u") + " (" + UnitUtil.Temperature.ToString((float)this.m_actualTemp, "u") + ")";
 
            float[] aTemperature = TemperatureResult.aTemperature;
            IList<TemperatureResult> result = new List<TemperatureResult>();
            TemperatureResult sel = null;
            for (int i = 0; i < aTemperature.Length; i++)
            {
                TemperatureResult t = new TemperatureResult(m_ppcontrol.SingleActivity, aTemperature[i], (float)this.m_actualTemp, m_ppcontrol.Time, m_ppcontrol.Distance);
                result.Add(t);
                if ((i == 0) || 
                    (Math.Abs(aTemperature[i] - (float)this.m_actualTemp) < Math.Abs(aTemperature[i-1] - (float)this.m_actualTemp)))
                {
                    sel = t;
                }
            }
            temperatureList.RowData = result;
            if (sel != null)
            {
                temperatureList.SelectedItems = new List<TemperatureResult> { sel };
            }
        }

        private void setWeight()
        {
            if (this.m_actualWeight == null || float.IsNaN((float)this.m_actualWeight))
            {
                this.m_actualWeight = Plugin.GetApplication().Logbook.Athlete.InfoEntries.LastEntryAsOfDate(m_ppcontrol.SingleActivity.StartTime).WeightKilograms;
                this.m_idealWeight = WeightResult.IdealWeight((float)this.m_actualWeight, Plugin.GetApplication().Logbook.Athlete.HeightCentimeters);
            }
            if (float.IsNaN((float)this.m_actualWeight))
            {
                this.weightLabel.Text = Resources.WeightUndefined;
                this.m_actualWeight = WeightResult.DefaultWeight;
                this.m_idealWeight = WeightResult.IdealWeight((float)this.m_actualWeight, Plugin.GetApplication().Logbook.Athlete.HeightCentimeters);
            }

            weightLabel2.Visible = true;
            weightLabel.Text = Resources.WeightProjectedImpact + " " +
                UnitUtil.Distance.ToString(m_ppcontrol.Distance, "u") + " (" + UnitUtil.Weight.ToString((float)this.m_actualWeight, "u") + ")";

            const double inc = 1.4;
            double vdot = Predict.getVdot(m_ppcontrol.Time, m_ppcontrol.Distance);

            IList<WeightResult> result = new List<WeightResult>();
            WeightResult sel = null;
            for (int i = 0; i < 13; i++)
            {
                double predWeight = (float)this.m_actualWeight + (6-i) * inc;
                if (predWeight > this.m_idealWeight)
                {
                    WeightResult t = new WeightResult(m_ppcontrol.SingleActivity, vdot, predWeight, (float)this.m_actualWeight, m_ppcontrol.Time, m_ppcontrol.Distance);
                    result.Add(t);
                    if (t.Weight >= (float)this.m_actualWeight)
                    {
                        sel = t;
                    }
                }
            }
            weightList.RowData = result;
            if (sel != null)
            {
                weightList.SelectedItems = new List<WeightResult> { sel };
            }
        }

        private void setShoe()
        {
            if (this.m_actualShoe == null || float.IsNaN((float)this.m_actualShoe))
            {
                //Guess from equipment
                foreach (IEquipmentItem eq in m_ppcontrol.SingleActivity.EquipmentUsed)
                {
                    if (eq.WeightKilograms < 1 && eq.WeightKilograms > 0)
                    {
                        //get weight per shoe, present unit
                        this.m_actualShoe = eq.WeightKilograms/2;
                        ShoeLabelProvider.shoeUnit = eq.WeightUnits;
                        break;
                    }
                }
                this.m_idealShoe = ShoeResult.IdealWeight;
            }
            if (this.m_actualShoe == null || float.IsNaN((float)this.m_actualShoe))
            {
                shoeLabel.Text = Resources.ShoeUndefined;
                this.m_actualShoe = ShoeResult.DefaultWeight;
                ShoeLabelProvider.shoeUnit = UnitUtil.Weight.SmallUnit(Plugin.GetApplication().SystemPreferences.WeightUnits);
                this.m_idealShoe = ShoeResult.IdealWeight;
            }

            shoeLabel2.Visible = true;
            shoeLabel.Text = Resources.ShoeProjectedImpact + " " +
                UnitUtil.Distance.ToString(m_ppcontrol.Distance, "u") + " (" + UnitUtil.Weight.ToString((float)this.m_actualShoe, ShoeLabelProvider.shoeUnit, "u") + ")";

            foreach (TreeList.Column c in shoeList.Columns)
            {
                if (c.Id == ResultColumnIds.ShoeWeight)
                {
                    c.Text = ResultColumnIds.TextShoeWeightColumn(ShoeLabelProvider.shoeUnit);
                    break;
                }
            }

            float[] aShoeWeight = ShoeResult.aShoeWeight;
            double vdot = Predict.getVdot(m_ppcontrol.Time, m_ppcontrol.Distance);
            IList<ShoeResult> result = new List<ShoeResult>();
            ShoeResult sel = null;
            for (int i = 0; i < aShoeWeight.Length; i++)
            {
                ShoeResult t = new ShoeResult(m_ppcontrol.SingleActivity, vdot, aShoeWeight[i], (float)this.m_actualShoe, m_ppcontrol.Time, m_ppcontrol.Distance);
                result.Add(t);
                if ((i == 0)
                    || Math.Abs(aShoeWeight[i] - (float)this.m_actualShoe) < Math.Abs(aShoeWeight[i - 1] - (float)this.m_actualShoe))
                {
                    sel = t;
                }
            }
            shoeList.RowData = result;
            if (sel != null)
            {
                shoeList.SelectedItems = new List<ShoeResult> { sel };
            }
        }

        private void setAge()
        {
            if (this.m_actualAge == null || float.IsNaN((float)this.m_actualAge))
            {
                this.m_actualAge = (float)(m_ppcontrol.SingleActivity.StartTime - Plugin.GetApplication().Logbook.Athlete.DateOfBirth).TotalDays/365.24f;
                Predict.Sex = Plugin.GetApplication().Logbook.Athlete.Sex;
                this.m_idealAge = PredictWavaTime.IdealAge((float)m_ppcontrol.Distance);
            }
            if (float.IsNaN((float)this.m_actualAge))
            {
                ageLabel.Text = Resources.AgeUndefined;
                this.m_actualAge = Predict.DefaultAge;
                this.m_idealAge = PredictWavaTime.IdealAge((float)m_ppcontrol.Distance);
            }

            float agePerf = PredictWavaTime.IdealTime((float)m_ppcontrol.Distance, (float)this.m_actualAge)/(float)m_ppcontrol.Time.TotalSeconds;
            ageLabel2.Visible = true;
            ageLabel.Text = Resources.AgeProjectedImpact + " " +
                UnitUtil.Distance.ToString(m_ppcontrol.Distance, "u") + " (" + agePerf.ToString("P1")+" of world class)";

            IList<AgeResult> result = new List<AgeResult>();
            AgeResult sel = null;
            float[] aAge = AgeResult.aAge;
            for (int i = 0; i < aAge.Length; i++)
            {
                AgeResult t = new AgeResult(m_ppcontrol.SingleActivity, aAge[i], (float)this.m_actualAge, m_ppcontrol.Time, m_ppcontrol.Distance);
                result.Add(t);
                if ((i == 0)
                    || Math.Abs(aAge[i] - (float)this.m_actualAge) < Math.Abs(aAge[i - 1] - (float)this.m_actualAge))
                {
                    sel = t;
                }
            }
            ageList.RowData = result;
            if (sel != null)
            {
                ageList.SelectedItems = new List<AgeResult> { sel };
            }
        }

        private void setIdeal()
        {
            idealLabel2.Visible = true;
            double idealAgeTime = PredictWavaTime.IdealTime((float)m_ppcontrol.Distance, this.m_idealAge);
            if (this.m_oldDistance < 0)
            {
                this.m_oldDistance = (float)this.m_ppcontrol.Distance;
            }

            double f = ShoeResult.vdotFactor(this.m_idealShoe, (float)this.m_actualShoe);
            f *= WeightResult.vdotFactor(this.m_idealWeight, (float)this.m_actualWeight);
            double ideal = m_ppcontrol.Time.TotalSeconds * Predict.getTimeFactorFromAdjVdot(f);
            ideal *= TemperatureResult.getTemperatureFactor(this.m_idealTemp) / TemperatureResult.getTemperatureFactor((float)this.m_actualTemp);
            ideal = PredictWavaTime.WavaPredict((float)m_ppcontrol.Distance, this.m_oldDistance, ideal, this.m_idealAge, (float)this.m_actualAge);
            double idealP = idealAgeTime / ideal;
            idealLabel.Text = idealP.ToString("P1")+" of world class";  //TBD
            double idealDistance = m_ppcontrol.Distance;
            if (this.distBox2.Text != "")
            {
                idealDistance = UnitUtil.Distance.Parse(this.distBox2.Text);
                if (idealDistance != m_ppcontrol.Distance)
                {
                    Predict.SetAgeSexFromActivity(this.m_ppcontrol.FirstActivity);
                    ideal = (Predict.Predictor(Settings.Model))(idealDistance, m_ppcontrol.Distance, TimeSpan.FromSeconds(ideal));
                }
            }

            this.timeBox.LostFocus -= timeBox_LostFocus;
            this.timeBox2.LostFocus -= timeBox2_LostFocus;
            this.distBox.LostFocus -= distBox_LostFocus;
            this.distBox2.LostFocus -= distBox2_LostFocus;
            this.paceBox.LostFocus -= paceBox_LostFocus;
            this.paceBox2.LostFocus -= paceBox2_LostFocus;
            this.temperatureBox.LostFocus -= temperatureBox_LostFocus;
            this.temperatureBox2.LostFocus -= temperatureBox2_LostFocus;
            this.weightBox.LostFocus -= weightBox_LostFocus;
            this.weightBox2.LostFocus -= weightBox2_LostFocus;
            this.shoeBox.LostFocus -= shoeBox_LostFocus;
            this.shoeBox2.LostFocus -= shoeBox2_LostFocus;
            this.ageBox.LostFocus -= ageBox_LostFocus;
            this.ageBox2.LostFocus -= ageBox2_LostFocus;

            this.timeBox.Text = UnitUtil.Time.ToString(m_ppcontrol.Time, "u");
            this.timeBox2.Text = UnitUtil.Time.ToString(ideal, "u");
            this.distBox.Text = UnitUtil.Distance.ToString(m_ppcontrol.Distance, "u");
            this.distBox2.Text = UnitUtil.Distance.ToString(idealDistance, "u");
            this.paceBox.Text = UnitUtil.PaceOrSpeed.ToString(Settings.ShowPace, m_ppcontrol.Distance / m_ppcontrol.Time.TotalSeconds);
            this.paceBox2.Text = UnitUtil.PaceOrSpeed.ToString(Settings.ShowPace, idealDistance / ideal);
            this.temperatureBox.Text = UnitUtil.Temperature.ToString((float)this.m_actualTemp, "u");
            this.temperatureBox2.Text = UnitUtil.Temperature.ToString(this.m_idealTemp, "u");
            this.weightBox.Text = UnitUtil.Weight.ToString((float)this.m_actualWeight, "u");
            this.weightBox2.Text = UnitUtil.Weight.ToString(this.m_idealWeight, "u");
            this.shoeBox.Text = UnitUtil.Weight.ToString((float)this.m_actualShoe, ShoeLabelProvider.shoeUnit, "u");
            this.shoeBox2.Text = UnitUtil.Weight.ToString(this.m_idealShoe, ShoeLabelProvider.shoeUnit, "u");
            this.ageBox.Text = ((float)this.m_actualAge).ToString("F0");
            this.ageBox2.Text = this.m_idealAge.ToString("F0");

            this.timeBox.LostFocus += timeBox_LostFocus;
            this.timeBox2.LostFocus += timeBox2_LostFocus;
            this.distBox.LostFocus += distBox_LostFocus;
            this.distBox2.LostFocus += distBox_LostFocus;
            this.paceBox.LostFocus += paceBox_LostFocus;
            this.paceBox2.LostFocus += paceBox2_LostFocus;
            this.temperatureBox.LostFocus += temperatureBox_LostFocus;
            this.temperatureBox2.LostFocus += temperatureBox2_LostFocus;
            this.weightBox.LostFocus += weightBox_LostFocus;
            this.weightBox2.LostFocus += weightBox2_LostFocus;
            this.shoeBox.LostFocus += shoeBox_LostFocus;
            this.shoeBox2.LostFocus += shoeBox2_LostFocus;
            this.ageBox.LostFocus += ageBox_LostFocus;
            this.ageBox2.LostFocus += ageBox2_LostFocus;

            this.m_oldDistance = (float)this.m_ppcontrol.Distance;
        }

        void timeBox_LostFocus(object sender, System.EventArgs e)
        {
            m_ppcontrol.Time = TimeSpan.FromSeconds(UnitUtil.Time.Parse(this.timeBox.Text));
        }

        void timeBox2_LostFocus(object sender, System.EventArgs e)
        {
            //TBD
            //double time = UnitUtil.Time.Parse(this.timeBox2.Text);
            //double idealDistance = UnitUtil.Distance.Parse(this.distBox2.Text);
            //this.paceBox2.LostFocus -= timeBox2_LostFocus;
            //this.paceBox2.Text = UnitUtil.PaceOrSpeed.ToString(Settings.ShowPace, idealDistance / time);
            //this.paceBox2.LostFocus += timeBox2_LostFocus;
        }

        void distBox_LostFocus(object sender, System.EventArgs e)
        {
            m_ppcontrol.Distance = UnitUtil.Distance.Parse(this.distBox.Text);
        }

        void distBox2_LostFocus(object sender, System.EventArgs e)
        {
            this.setIdeal();
        }

        void paceBox_LostFocus(object sender, System.EventArgs e)
        {
            double speed = UnitUtil.PaceOrSpeed.Parse(Settings.ShowPace, this.paceBox.Text);
            m_ppcontrol.Time = TimeSpan.FromSeconds(m_ppcontrol.Distance / speed);
        }

        void paceBox2_LostFocus(object sender, System.EventArgs e)
        {
            //TBD, update Actual?
            //double speed = UnitUtil.PaceOrSpeed.Parse(Settings.ShowPace, this.paceBox2.Text);
            //double idealDistance = UnitUtil.Distance.Parse(this.distBox2.Text);
            //this.timeBox2.LostFocus -= timeBox2_LostFocus;
            //this.timeBox2.Text = UnitUtil.Time.ToString(idealDistance / speed, "u");
            //this.timeBox2.LostFocus += timeBox2_LostFocus;
        }

        void temperatureBox_LostFocus(object sender, System.EventArgs e)
        {
            this.m_actualTemp = (float)UnitUtil.Temperature.Parse(this.temperatureBox.Text);
            this.setTemperature();
            this.setIdeal();
        }

        void weightBox_LostFocus(object sender, System.EventArgs e)
        {
            this.m_actualWeight = (float)UnitUtil.Weight.Parse(this.weightBox.Text);
            this.setWeight();
            this.setIdeal();
        }

        void shoeBox_LostFocus(object sender, System.EventArgs e)
        {
            this.m_actualShoe = (float)UnitUtil.Weight.Parse(this.shoeBox.Text, ShoeLabelProvider.shoeUnit);
            this.setShoe();
            this.setIdeal();
        }

        void ageBox_LostFocus(object sender, System.EventArgs e)
        {
            this.m_actualAge = Settings.parseFloat(this.ageBox.Text);
            this.setAge();
            this.setIdeal();
        }

        void temperatureBox2_LostFocus(object sender, System.EventArgs e)
        {
            this.m_idealTemp = (float)UnitUtil.Temperature.Parse(this.temperatureBox2.Text);
            this.setTemperature();
            this.setIdeal();
        }

        void weightBox2_LostFocus(object sender, System.EventArgs e)
        {
            this.m_idealWeight = (float)UnitUtil.Weight.Parse(this.weightBox2.Text);
            this.setWeight();
            this.setIdeal();
        }

        void shoeBox2_LostFocus(object sender, System.EventArgs e)
        {
            this.m_idealShoe = (float)UnitUtil.Weight.Parse(this.shoeBox2.Text, ShoeLabelProvider.shoeUnit);
            this.setShoe();
            this.setIdeal();
        }

        void ageBox2_LostFocus(object sender, System.EventArgs e)
        {
            this.m_idealAge = Settings.parseFloat(this.ageBox2.Text);
            this.setAge();
            this.setIdeal();
        }

        //Static method for "ideal" calculation
        //Duplicating some of the "dynamic" code
        public static double GetIdeal(IActivity act, double new_dist, double dist, double time)
        {
            double ideal = time;

            //if act is null (like for summaryresult), only predict
            if (act != null)
            {
                //Guess from equipment
                float actualShoe = ShoeResult.DefaultWeight;
                foreach (IEquipmentItem eq in act.EquipmentUsed)
                {
                    if (eq.WeightKilograms < 1 && eq.WeightKilograms > 0)
                    {
                        //get weight per shoe, present unit
                        actualShoe = eq.WeightKilograms / 2;
                        break;
                    }
                }
                float idealShoe = ShoeResult.IdealWeight;
                double f = ShoeResult.vdotFactor(idealShoe, actualShoe);

                float actualWeight = Plugin.GetApplication().Logbook.Athlete.InfoEntries.LastEntryAsOfDate(act.StartTime).WeightKilograms;
                float idealWeight = WeightResult.IdealWeight((float)actualWeight, Plugin.GetApplication().Logbook.Athlete.HeightCentimeters);
                if (float.IsNaN(actualWeight))
                {
                    actualWeight = WeightResult.DefaultWeight;
                    idealWeight = WeightResult.IdealWeight(actualWeight, Plugin.GetApplication().Logbook.Athlete.HeightCentimeters);
                }

                f *= WeightResult.vdotFactor(idealWeight, actualWeight);
                ideal *= Predict.getTimeFactorFromAdjVdot(f);

                float actualTemp = act.Weather.TemperatureCelsius;
                float idealTemp = TemperatureResult.IdealTemperature;
                if (float.IsNaN(actualTemp))
                {
                    actualTemp = TemperatureResult.DefaultTemperature;
                    idealTemp = TemperatureResult.IdealTemperature;
                }
                ideal *= TemperatureResult.getTemperatureFactor(idealTemp) / TemperatureResult.getTemperatureFactor(actualTemp);

                float idealAge = PredictWavaTime.IdealAge((float)dist);
                double idealAgeTime = PredictWavaTime.IdealTime((float)dist, idealAge);
                float actualAge = (float)(act.StartTime - Plugin.GetApplication().Logbook.Athlete.DateOfBirth).TotalDays / 365.24f;
                Predict.Sex = Plugin.GetApplication().Logbook.Athlete.Sex;
                ideal = PredictWavaTime.WavaPredict(dist, dist, ideal, idealAge, actualAge);
                Predict.SetAgeSexFromActivity(act);
            }
            ideal = (Predict.Predictor(Settings.Model))(new_dist, dist, TimeSpan.FromSeconds(ideal));

            return ideal;
        }
    }
}
