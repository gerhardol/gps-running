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
        private float? m_actualTemp = null;
        private float? m_actualWeight = null;
        private float? m_actualShoe = null;
        private float? m_actualAge = null;
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

            weightList.LabelProvider = new WeightLabelProvider();
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

            shoeList.LabelProvider = new ShoeLabelProvider();
            ShoeLabelProvider.shoeUnit = Plugin.GetApplication().SystemPreferences.WeightUnits;
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

            ageList.LabelProvider = new AgeLabelProvider();
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

            this.temperatureBox.ThemeChanged(visualTheme);
            this.weightBox.ThemeChanged(visualTheme);
            this.shoeBox.ThemeChanged(visualTheme);
            this.ageBox.ThemeChanged(visualTheme);

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
            this.temperatureTab.Text = ZoneFiveSoftware.Common.Visuals.CommonResources.Text.LabelTemperature;
            this.weightTab.Text = ZoneFiveSoftware.Common.Visuals.CommonResources.Text.LabelWeight;
            this.shoeTab.Text = Resources.ShoeImpact;
            this.ageTab.Text = ZoneFiveSoftware.Common.Visuals.CommonResources.Text.LabelEquipmentAge;
            this.utopiaTab.Text = Resources.UtopiaImpact;

            //temperatureLabel2.Text
            temperatureLabel2.Text = String.Format(Resources.TemperatureNotification, 
                UnitUtil.Temperature.ToString(TemperatureResult.IdealTemperature, "F0u"));
            //weightLabel.Text
            weightLabel2.Text = String.Format(Resources.WeightNotification, 2 + " " + StringResources.Seconds,
                UnitUtil.Distance.ToString(1000, "u"));
            shoeLabel2.Text = String.Format(Resources.ShoeNotification);
            ageLabel2.Text = String.Format(Resources.AgeNotification);
            utopiaLabel2.Text = String.Format(Resources.UtopiaNotification);

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
                if (this.m_activity != this.m_ppcontrol.SingleActivity)
                {
                    m_actualTemp = null;
                    m_actualWeight = null;
                    m_actualShoe = null;
                    m_actualAge = null;
                }
                setTemperature();
                setWeight();
                setShoe();
                setAge();
                setUtopia();
            }
        }

        private void setTemperature()
        {
            if (this.m_actualTemp == null || float.IsNaN((float)this.m_actualTemp))
            {
                this.m_actualTemp = m_ppcontrol.SingleActivity.Weather.TemperatureCelsius;
            }
            if (float.IsNaN((float)this.m_actualTemp)) { this.m_actualTemp = TemperatureResult.DefaultTemperature; }

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
            }
            if (float.IsNaN((float)this.m_actualWeight))
            {
                weightLabel.Text = Resources.WeightUndefined;
                this.m_actualWeight = WeightResult.DefaultWeight;
            }

            weightLabel2.Visible = true;
            weightLabel.Text = Resources.WeightProjectedImpact + " " +
                UnitUtil.Distance.ToString(m_ppcontrol.Distance, "u") + " (" + UnitUtil.Weight.ToString((float)this.m_actualWeight, "u") + ")";

            const double inc = 1.4;
            double vdot = Predict.getVdot(m_ppcontrol.Time, m_ppcontrol.Distance);
            double idealWeight = WeightResult.IdealWeight((float)this.m_actualWeight, Plugin.GetApplication().Logbook.Athlete.HeightCentimeters);
            IList<WeightResult> result = new List<WeightResult>();
            WeightResult sel = null;
            for (int i = 0; i < 13; i++)
            {
                double predWeight = (float)this.m_actualWeight + (6-i) * inc;
                if (predWeight > idealWeight)
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
            weightList.SelectedItems = new List<WeightResult> { sel };
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
                    }
                }
            }
            if (this.m_actualShoe == null || float.IsNaN((float)this.m_actualShoe))
            {
                shoeLabel.Text = Resources.ShoeUndefined;
                this.m_actualShoe = ShoeResult.DefaultWeight;
                ShoeLabelProvider.shoeUnit = Plugin.GetApplication().SystemPreferences.WeightUnits;
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
            shoeList.SelectedItems = new List<ShoeResult> { sel };
        }

        private void setAge()
        {
            if (this.m_actualAge == null || float.IsNaN((float)this.m_actualAge))
            {
                this.m_actualAge = (float)(m_ppcontrol.SingleActivity.StartTime - Plugin.GetApplication().Logbook.Athlete.DateOfBirth).TotalDays/365.24f;
                PredictWavaTime.Sex = Plugin.GetApplication().Logbook.Athlete.Sex;
            }
            if (float.IsNaN((float)this.m_actualAge))
            {
                ageLabel.Text = Resources.AgeUndefined;
                this.m_actualAge = PredictWavaTime.DefaultAge;
            }

            float agePerf = PredictWavaTime.IdealTime((float)m_ppcontrol.Distance, (float)this.m_actualAge)/(float)m_ppcontrol.Time.TotalSeconds;
            float idealAge = PredictWavaTime.IdealAge((float)m_ppcontrol.Distance);
            ageLabel2.Visible = true;
            ageLabel.Text = Resources.AgeProjectedImpact + " " +
                UnitUtil.Distance.ToString(m_ppcontrol.Distance, "u") + " (" + agePerf.ToString("P1")+" at current age)";

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
            ageList.SelectedItems = new List<AgeResult> { sel };
        }

        private void setUtopia()
        {
            utopiaLabel2.Visible = true;
            float idealAge = PredictWavaTime.IdealAge((float)m_ppcontrol.Distance);
            double idealAgeTime = PredictWavaTime.IdealTime((float)m_ppcontrol.Distance, idealAge);
            double ideal = PredictWavaTime.WavaPredict((float)m_ppcontrol.Distance,(float)m_ppcontrol.Distance, m_ppcontrol.Time, idealAge, (float)this.m_actualAge);
            double f = ShoeResult.vdotFactor(ShoeResult.IdealWeight, (float)this.m_actualShoe);
            f*= WeightResult.vdotFactor(WeightResult.IdealWeight((float)this.m_actualWeight, Plugin.GetApplication().Logbook.Athlete.HeightCentimeters), (float)this.m_actualWeight);
            ideal *= Predict.getTimeFactorFromAdjVdot(f);
            ideal *= TemperatureResult.getTemperatureFactor(TemperatureResult.IdealTemperature)/TemperatureResult.getTemperatureFactor((float)this.m_actualTemp);
            double idealP = idealAgeTime / ideal;
            utopiaLabel.Text = "Estimate: "+TimeSpan.FromSeconds( ideal) + " at ideal age "+idealAge+" "+idealP.ToString("P1");

            this.temperatureBox.Text = UnitUtil.Temperature.ToString((float)this.m_actualTemp, "u");
            this.weightBox.Text = UnitUtil.Weight.ToString((float)this.m_actualWeight, "u");
            this.shoeBox.Text = UnitUtil.Weight.ToString((float)this.m_actualShoe, ShoeLabelProvider.shoeUnit, "u");
            this.ageBox.Text = ((float)this.m_actualAge).ToString("F0");

        }

        void temperatureBox_LostFocus(object sender, System.EventArgs e)
        {
            this.m_actualTemp = (float)UnitUtil.Temperature.Parse(this.temperatureBox.Text);
            this.setTemperature();
        }

        void weightBox_LostFocus(object sender, System.EventArgs e)
        {
            this.m_actualWeight = (float)UnitUtil.Weight.Parse(this.weightBox.Text);
            this.setWeight();
        }

        void shoeBox_LostFocus(object sender, System.EventArgs e)
        {
            this.m_actualShoe = (float)UnitUtil.Weight.Parse(this.shoeBox.Text);
            this.setShoe();
        }

        void ageBox_LostFocus(object sender, System.EventArgs e)
        {
            this.m_actualAge = (float)UnitUtil.Weight.Parse(this.ageBox.Text);
            this.setAge();
        }
    }
}
