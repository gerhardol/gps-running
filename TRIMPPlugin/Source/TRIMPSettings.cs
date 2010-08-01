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
using System.Diagnostics;
using ZoneFiveSoftware.Common.Data.Fitness;
using GpsRunningPlugin;
using GpsRunningPlugin.Source;
using ZoneFiveSoftware.Common.Visuals;
using ZoneFiveSoftware.Common.Visuals.Chart;
using GpsRunningPlugin.Properties;
using GpsRunningPlugin.Util;

namespace GpsRunningPlugin.Source
{
    public partial class TRIMPSettings : UserControl
    {
        private void correctUI(IList<Control> comp)
        {
            Control prev = null;
            foreach (Control c in comp)
            {
                if (prev != null)
                {
                    c.Location = new Point(prev.Location.X + prev.Size.Width,
                                           prev.Location.Y);
                }
                prev = c;
            }
        }

        public TRIMPSettings()
        {
            InitializeComponent();
            if (Settings.UseMaxHR)
            {
                useMaxHR.Checked = true;
            }
            else
            {
                useHRReserve.Checked = true;
            }
            double restHR = Plugin.GetApplication().Logbook.Athlete.InfoEntries.LastEntryAsOfDate(DateTime.Now).RestingHeartRatePerMinute;
            double maxHR = Plugin.GetApplication().Logbook.Athlete.InfoEntries.LastEntryAsOfDate(DateTime.Now).MaximumHeartRatePerMinute;
            if (double.IsNaN(maxHR) || double.IsNaN(restHR))
            {
                maxHRLabel.Text = Resources.NoRestOrMaxHR;
                dataGridView1.Visible = false;
            }
            else
            {
                maxHRLabel.Text = String.Format(Resources.CurrentRestAndMax,
                            UnitUtil.HeartRate.ToString(restHR, "u"),
                            UnitUtil.HeartRate.ToString(maxHR, "u"));
                reset();                
                dataGridView1.ClearSelection();
            }
        }

        public void ThemeChanged(ZoneFiveSoftware.Common.Visuals.ITheme visualTheme)
        {
            chartBase.ThemeChanged(visualTheme);
        }
        public void UICultureChanged(System.Globalization.CultureInfo culture)
        {
            resetSettings.Text = StringResources.ResetAllSettings;
            label1.Text = StringResources.NumberOfZones;
            label2.Text = StringResources.StartZone;
            label4.Text = StringResources.Use;
            useMaxHR.Text = CommonResources.Text.LabelPercentOfMax;
            useHRReserve.Text = CommonResources.Text.LabelPercentOfReserve;
            this.linkLabel1.Text = "TRIMP plugin webpage";

            dataGridView1.Columns[0].HeaderText = CommonResources.Text.LabelPercentOfMax; //Placeholder, set in fillTableAndGraph()
            dataGridView1.Columns[1].HeaderText = UnitUtil.HeartRate.LabelAbbr2;
            dataGridView1.Columns[2].HeaderText = StringResources.Factor;
            correctUI(new Control[] { label4, useMaxHR });
            label1.Location = new Point(numberOfZones.Location.X - label1.Size.Width - 5,
                                        label1.Location.Y);
            label2.Location = new Point(numberOfZones.Location.X - label2.Size.Width - 5,
                                        label2.Location.Y);
            useHRReserve.Location = new Point(useMaxHR.Location.X, useHRReserve.Location.Y);
        }
        private bool updating;

        private void reset()
        {
            updating = true;
            numberOfZones.Value = Settings.Factors.Count;
            startZone.Value = Settings.StartZone;
            useMaxHR.Checked = Settings.UseMaxHR;
            useHRReserve.Checked = !Settings.UseMaxHR;
            fillTableAndGraph();
            setSize();
            updating = false;
        }

        private void fillTableAndGraph()
        {
            dataGridView1.Rows.Clear();
            chartBase.DataSeries.Clear();

            double restHR = Plugin.GetApplication().Logbook.Athlete.InfoEntries.LastEntryAsOfDate(DateTime.Now).RestingHeartRatePerMinute;
            double maxHR = Plugin.GetApplication().Logbook.Athlete.InfoEntries.LastEntryAsOfDate(DateTime.Now).MaximumHeartRatePerMinute;
            double delta = (100.0 - Settings.StartZone) / Settings.Factors.Count;
            double current = Settings.StartZone;
            
            ChartDataSeries series = new ChartDataSeries(chartBase, chartBase.YAxis);
            series.ChartType = ChartDataSeries.Type.StepFill;
            chartBase.XAxis.Formatter = new Formatter.Percent();
            chartBase.XAxis.OriginValue = 0;
            if (Settings.UseMaxHR)
            {
                chartBase.XAxis.Label = CommonResources.Text.LabelZone + " (" + CommonResources.Text.LabelPercentOfMax+")";
                dataGridView1.Columns[0].HeaderText = CommonResources.Text.LabelPercentOfMax;
            }
            else
            {
                chartBase.XAxis.Label = CommonResources.Text.LabelZone + " (" + CommonResources.Text.LabelPercentOfReserve + ")"; ;
                dataGridView1.Columns[0].HeaderText = CommonResources.Text.LabelPercentOfReserve;
            }
            chartBase.YAxis.Label = StringResources.Factor;
            double low, high, lastFactor = 1;
            DataTable table = new DataTable();
            foreach (double factor in Settings.Factors)
            {
                lastFactor = factor;
                low = current;
                high = current + delta;
                if (Settings.UseMaxHR)
                {
                    dataGridView1.Rows.Add(new object[]{
                    Settings.present(low,1)+" - "+Settings.present(high,1),
                    Settings.present(low * maxHR / 100, 1) + " - " +
                    Settings.present(high * maxHR / 100, 1),
                    factor});
                }
                else
                {
                    dataGridView1.Rows.Add(new object[]{
                    Settings.present(low,1)+" - "+Settings.present(high,1),
                    Settings.present(low * (maxHR-restHR) / 100 + restHR, 1) + " - " +
                    Settings.present(high * (maxHR-restHR) / 100 + restHR, 1),
                    factor});
                }
                float x = (float)current;
                if (!x.Equals(float.NaN) && series.Points.IndexOfKey(x) == -1)
                {
                    series.Points.Add(x, new PointF(x, (float)factor));
                }
                current = high;
            }
            if (series.Points.IndexOfKey(100) == -1)
            {
                series.Points.Add(100, new PointF(100, (float)lastFactor));
            }
      
            chartBase.DataSeries.Add(series);
            chartBase.AutozoomAxis(chartBase.XAxis, true);
            chartBase.AutozoomAxis(chartBase.YAxis, true);

            dataGridView1.Columns[0].ReadOnly = true;
            dataGridView1.Columns[1].ReadOnly = true;
            dataGridView1.CellValueChanged += new DataGridViewCellEventHandler(dataGridView1_CellValueChanged);

            int columnWidth = dataGridView1.Size.Width / dataGridView1.Columns.Count;
            foreach (DataGridViewColumn column in dataGridView1.Columns)
            {
                column.Width = columnWidth;
            }
        }

        private int lastCellRowIndex = -1;

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex.Equals(lastCellRowIndex) || dataGridView1.Rows.Count == 0) return;
            lastCellRowIndex = e.RowIndex;
            object cellValue = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
            try
            {
                double newFactor = Settings.parseDouble((string)cellValue);
                Settings.Factors[e.RowIndex] = newFactor;
            }
            catch (Exception)
            { }
            fillTableAndGraph();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(new ProcessStartInfo("IExplore",
                "http://code.google.com/p/gps-running/wiki/TRIMP"));
        }

        private YesNoDialog dialog;

        private void resetSettings_Click_1(object sender, EventArgs e)
        {
            dialog = new YesNoDialog(StringResources.ResetQuestion);
            dialog.Disposed += new EventHandler(dialog_Disposed);
            dialog.ShowDialog();
        }

        void dialog_Disposed(object sender, EventArgs e)
        {
            if (dialog.answer)
            {
                Settings.defaults();
                reset();
            }
        }

        private void numberOfZones_ValueChanged(object sender, EventArgs e)
        {
            if (!updating)
            {
                IList<double> newFactors = new List<double>();
                for (int i = 0; i < (int)numberOfZones.Value; i++)
                {
                    newFactors.Add(1);
                }
                Settings.Factors = newFactors;
                fillTableAndGraph();
                setSize();
            }
        }

        private void setSize()
        {
            dataGridView1.Size = new Size(dataGridView1.Width,
                    dataGridView1.Rows[0].Height * Settings.Factors.Count +
                    dataGridView1.ColumnHeadersHeight);
            chartBase.Location = new Point(
                chartBase.Location.X,
                dataGridView1.Location.Y +
                dataGridView1.Size.Height + 10);
        }

        private void startZone_ValueChanged(object sender, EventArgs e)
        {
            if (!updating)
            {
                Settings.StartZone = (int)startZone.Value;
                fillTableAndGraph();
            }
        }

        private void useMaxHR_CheckedChanged(object sender, EventArgs e)
        {
            if (useMaxHR.Checked)
            {
                Settings.UseMaxHR = true;
                fillTableAndGraph();
            }
        }

        private void useHRReserve_CheckedChanged(object sender, EventArgs e)
        {
            if (useHRReserve.Checked)
            {
                Settings.UseMaxHR = false;
                fillTableAndGraph();
            }
        }
    }
}
