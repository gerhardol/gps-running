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
using System.Windows.Forms;
using SportTracksTRIMPPlugin;
using SportTracksTRIMPPlugin.Source;
using System.Drawing;
using ZoneFiveSoftware.Common.Visuals;
using ZoneFiveSoftware.Common.Visuals.Chart;
using ZoneFiveSoftware.Common.Data;
using ZoneFiveSoftware.Common.Data.Fitness;
using ZoneFiveSoftware.Common.Data.Measurement;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Collections;
using SportTracksTRIMPPlugin.Properties;
using SportTracksTRIMPPlugin.Util;

namespace SportTracksTRIMPPlugin.Source
{
    class TRIMPView : UserControl
    {
        private DataGridView dataGridView1;
        private System.Windows.Forms.Label label1;
        private IList<IActivity> activities;
        private TabControl tabControl1;
        private TabPage tablePage;
        private TabPage graphPage;
        private System.ComponentModel.IContainer components;
        private bool showDialog;
        private DataGridViewTextBoxColumn Zone;
        private DataGridViewTextBoxColumn HeartRate;
        private DataGridViewTextBoxColumn Time;
        private DataGridViewTextBoxColumn TRIMP;
        private ContextMenuStrip contextMenuTable;
        private ToolStripMenuItem copyTableToClipboardToolStripMenuItem;
        private IDictionary<IActivity, double> activityTrimp;
        private ChartBase chartBase;
        private TabPage graphTab;
        private LineChart tableChart;
            
        public IList<IActivity> Activities
        {
            set
            {
                activities = value;
                updateTable();
            }
            get
            {
                return activities;
            }
        }

        private Form form;

        public TRIMPView(IList<IActivity> activities, bool showDialog)
        {
            InitializeComponent();            
            correctLanguage();
            this.activities = activities;
            this.showDialog = showDialog;
            graphTab = tabControl1.TabPages[1];
            updateTable();
            SizeChanged += new EventHandler(TRIMPView_SizeChanged);
            contextMenuTable.ItemClicked += new ToolStripItemClickedEventHandler(contextMenuTable_Click);
            if (showDialog)
            {
                form = new Form();
                form.Controls.Add(this);
                form.Size = Settings.WindowSize;
                form.Icon = Icon.FromHandle(Properties.Resources.Image_32_TRIMP.GetHicon());
                form.SizeChanged += new EventHandler(form_SizeChanged);
                setSize();
                if (activities.Count > 1)
                {
                    form.Text = String.Format(Resources.T2,activities.Count);
                }
                else
                {
                    form.Text = Resources.T1;
                }
                form.ShowDialog();
            }
        }

        void Settings_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            updateTable();
            setSize();
        }

        private void correctLanguage()
        {
            tabControl1.TabPages[0].Text = StringResources.Summary;
            tabControl1.TabPages[1].Text = StringResources.Graph;
            if (Settings.UseMaxHR)
            {
                dataGridView1.Columns[0].HeaderText = CommonResources.Text.LabelZone + " (" + CommonResources.Text.LabelPercentOfMax + ")";
            }
            else
            {
                dataGridView1.Columns[0].HeaderText = CommonResources.Text.LabelZone + " (" + CommonResources.Text.LabelPercentOfReserve + ")"; ;
            }

            dataGridView1.Columns[1].HeaderText = UnitUtil.HeartRate.LabelAxis;
            dataGridView1.Columns[2].HeaderText = UnitUtil.Time.LabelAxis;
        }

        private void contextMenuTable_Click(object sender, EventArgs e)
        {
            StringBuilder builder = new StringBuilder();
            foreach (DataGridViewColumn column in dataGridView1.Columns)
            {
                builder.Append(column.Name + "\t");
            }
            builder.Append("\n");
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                foreach (DataGridViewCell cell in row.Cells)
                {
                    builder.Append(cell.Value + "\t");
                }
                builder.Append("\n");
            }
            try
            {
                Clipboard.SetText(builder.ToString());
            }
            catch (Exception) { }
        }

        private void form_SizeChanged(object sender, EventArgs e)
        {
            setSize();
        }

        private void TRIMPView_SizeChanged(object sender, EventArgs e)
        {
            setSize();
        }

        private void setSize()
        {
            if (showDialog && Parent != null)
            {
                Size = new Size(form.Size.Width, //paceBox.Location.X + paceBox.Width),
                                form.Size.Height-40);
                Settings.WindowSize = new Size(Parent.Size.Width, Parent.Size.Height);
            }
            if (dataGridView1.Columns.Count > 0 && dataGridView1.Rows.Count > 0)
            {

                int columnWidth = ((Size.Width - dataGridView1.Location.X - 50)
                    / dataGridView1.Columns.Count);
                foreach (DataGridViewColumn column in dataGridView1.Columns)
                {
                    column.Width = columnWidth;
                }
                dataGridView1.Size = new Size(columnWidth * dataGridView1.Columns.Count+10,
                                            dataGridView1.Rows[0].Height * dataGridView1.Rows.Count
                                            + dataGridView1.ColumnHeadersHeight + 2);
                tableChart.Location = new Point(tableChart.Location.X,
                    dataGridView1.Location.Y + dataGridView1.Height + 5);
                tableChart.Size = new Size(dataGridView1.Width-5,
                    Size.Height - (tableChart.Location.Y + 50));                
            }
            chartBase.Size = new Size(dataGridView1.Size.Width - 5,
                dataGridView1.Height + tableChart.Height);
            tabControl1.Size = new Size(Size.Width-40,
                dataGridView1.Height+tableChart.Height + 30);
        }

        private void updateTable()
        {
            if (activities.Count == 0)
            {
                tabControl1.Visible = false;
                label1.Text = StringResources.NoSelectedActivities;
                return;
            }
            label1.Text = "";
            tabControl1.Visible = true;
            if (form != null)
            {
                if (activities.Count > 1)
                {
                    form.Text = String.Format(Resources.T2, activities.Count);
                }
                else
                {
                    form.Text = Resources.T1;
                }
            }
            double restHR = Plugin.GetApplication().Logbook.Athlete.InfoEntries.LastEntryAsOfDate(DateTime.Now).RestingHeartRatePerMinute;
            double maxHR = Plugin.GetApplication().Logbook.Athlete.InfoEntries.LastEntryAsOfDate(DateTime.Now).MaximumHeartRatePerMinute;
            if (double.IsNaN(maxHR) || double.IsNaN(restHR))
            {
                label1.Text = Resources.NoRestOrMaxHR;
                tabControl1.Visible = false;
            }
            else
            {
                tabControl1.Visible = true;
                if (tabControl1.TabPages.Contains(graphTab))
                {
                    tabControl1.TabPages.Remove(graphTab);
                }
                dataGridView1.Rows.Clear();
                int ignoreActivities = 0;
                IList<TimeSpan> times = new List<TimeSpan>();
                IList<double> trimps = new List<double>();
                for (int i = 0; i < Settings.Factors.Count; i++)
                {
                    times.Add(new TimeSpan());
                    trimps.Add(0);
                }
                activityTrimp = new Dictionary<IActivity, double>();
                TRIMPZone zones = getZones(restHR,maxHR);
                foreach (IActivity activity in activities)
                {
                    restHR = Plugin.GetApplication().Logbook.Athlete.InfoEntries.LastEntryAsOfDate(activity.StartTime).RestingHeartRatePerMinute;
                    maxHR = Plugin.GetApplication().Logbook.Athlete.InfoEntries.LastEntryAsOfDate(activity.StartTime).MaximumHeartRatePerMinute;
                    if (activity.HeartRatePerMinuteTrack == null)
                    {
                        ignoreActivities++;
                    }
                    else if (!restHR.Equals(double.NaN) &&
                        !maxHR.Equals(double.NaN))
                    {
                        ActivityInfo info = ActivityInfoCache.Instance.GetInfo(activity);
                        ZoneCategoryInfo zinfo = info.HeartRateZoneInfo(zones);
                        double trimp = 0;
                        for (int i = 0; i < Settings.Factors.Count; i++)
                        {
                            times[i] = times[i].Add(zinfo.Zones[i].TotalTime);
                            double zoneTrimp = Settings.Factors[i] * zinfo.Zones[i].TotalTime.TotalMinutes;
                            trimps[i] += zoneTrimp;
                            trimp += zoneTrimp;
                        }
                        activityTrimp.Add(activity, trimp);
                    }
                }

                IList<INamedLowHighZone> lhs = zones.Zones;
                double current = Settings.StartZone;
                double delta = (100.0 - Settings.StartZone) / Settings.Factors.Count;
                tableChart.DataSeries.Clear();
                tableChart.XAxis.Formatter = new Formatter.Percent();
                ChartDataSeries trimpSeries = new ChartDataSeries(tableChart, tableChart.YAxis);
                trimpSeries.ChartType = ChartDataSeries.Type.StepFill;
                float previousTrimp = 0;
                int nextIndex = 0;
                for (int i = 0; i < Settings.Factors.Count; i++)
                {
                    float low = (float)current;
                    float high = (float)(current + delta);
                    dataGridView1.Rows.Add(new object[] 
                    {low + " - " + high,
                    Settings.present(lhs[i].Low, 1) + " - " + 
                        Settings.present(lhs[i].High, 1),
                    times[i].ToString(),
                    Settings.present(trimps[i],1)});
                    trimpSeries.Points.Add(i,
                        new PointF(low, (float)trimps[i]));
                    current = high;
                    previousTrimp = (float)trimps[i];
                    nextIndex = i + 1;
                }
                trimpSeries.Points.Add(nextIndex, new PointF((float)current, previousTrimp));
                tableChart.DataSeries.Add(trimpSeries);
                if (Settings.UseMaxHR)
                {
                    tableChart.XAxis.Label = CommonResources.Text.LabelPercentOfMax; 
                } else
                {
                    tableChart.XAxis.Label = CommonResources.Text.LabelPercentOfReserve;
                }
                tableChart.YAxis.Label = "TRIMP";
                tableChart.AutozoomToData(true);
                double t = 0;
                foreach (double d in trimps)
                {
                    t += d;
                }
                if (restHR.Equals(double.NaN) || maxHR.Equals(double.NaN))
                {
                    label1.Text = Resources.NoRestOrMaxHR;
                    tabControl1.Visible = false;
                }
                else
                {
                    if (activities.Count == 1)
                    {
                        label1.Text = String.Format(Resources.CurrentRestAndMax, 
                            UnitUtil.HeartRate.ToString(restHR, "u"),
                            UnitUtil.HeartRate.ToString(maxHR, "u")) +
                            " " + String.Format(Resources.TotalTRIMP, Settings.present(t, 1));
                    }
                    else
                    {
                        label1.Text += String.Format(Resources.TotalTRIMP, Settings.present(t, 1));
                    }
                    if (ignoreActivities > 0)
                    {
                        if (activities.Count == 1)
                        {
                            label1.Text += Resources.IgnoredActivity;
                        }
                        else
                        {
                            label1.Text += String.Format(Resources.IgnoredActivities, ignoreActivities);
                        }
                    }
                }
                dataGridView1.ClearSelection();
                if (activityTrimp.Count > 1)
                {
                    tabControl1.TabPages.Add(graphTab);
                }
                if (activityTrimp.Count > 1)
                {
                    SortedList<DateTime, double> dailyTrimps = new SortedList<DateTime, double>();
                    foreach (IActivity activity in activityTrimp.Keys)
                    {
                        if (activity.StartTime != null)
                        {
                            DateTime dateTime = activity.StartTime.ToLocalTime().Date;
                            if (dailyTrimps.Keys.Contains(dateTime))
                            {
                                dailyTrimps[dateTime] += activityTrimp[activity];
                            }
                            else
                            {
                                dailyTrimps[dateTime] = activityTrimp[activity];
                            }
                        }
                    }
                    ChartDataSeries series = new ChartDataSeries(chartBase, chartBase.YAxis);
                    series.ChartType = ChartDataSeries.Type.Bar;
                    DateTime startTime = dailyTrimps.Keys[0];
                    chartBase.XAxis.Formatter = new Formatter.DaysToDate(startTime);
                    chartBase.XAxis.OriginValue = 0;
                    chartBase.XAxis.Label = CommonResources.Text.LabelDate;
                    chartBase.YAxis.Label = "TRIMP";
                    int index = 0;
                    foreach (DateTime dateTime in dailyTrimps.Keys)
                    {
                        TimeSpan span = dateTime - startTime;
                        float trimp = (float)dailyTrimps[dateTime];
                        series.Points.Add(index++,
                                new PointF((float)span.TotalDays,
                                trimp));
                    }
                    chartBase.DataSeries.Add(series);
                    chartBase.AutozoomToData(true);
                }
                chartBase.Refresh();
            }
        }

        private TRIMPZone getZones(double restHR,double maxHr)
        {
            IList<INamedLowHighZone> zones = new List<INamedLowHighZone>();
            double current = Settings.StartZone;
            double delta = (100.0 - Settings.StartZone) / Settings.Factors.Count;
            foreach (double factor in Settings.Factors)
            {
                double low = current;
                double high = current + delta;
                zones.Add(new TRIMPLowHighZone(getZone(low, high, restHR,maxHr)));
                current = high;
            }
            return new TRIMPZone(zones);
        }

        private IList<double> getZone(double low, double high, double restHR, double maxHR)
        {
            if (Settings.UseMaxHR)
            {
                return new double[] { low * maxHR / 100, high * maxHR / 100 };
            }
            return new double[] { low * (maxHR-restHR) / 100+restHR, 
                high * (maxHR-restHR) / 100 +restHR};
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.Zone = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.HeartRate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Time = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TRIMP = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.contextMenuTable = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.copyTableToClipboardToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.label1 = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tablePage = new System.Windows.Forms.TabPage();
            this.tableChart = new ZoneFiveSoftware.Common.Visuals.Chart.LineChart();
            this.graphPage = new System.Windows.Forms.TabPage();
            this.chartBase = new ZoneFiveSoftware.Common.Visuals.Chart.ChartBase();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.contextMenuTable.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tablePage.SuspendLayout();
            this.graphPage.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToResizeColumns = false;
            this.dataGridView1.AllowUserToResizeRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Zone,
            this.HeartRate,
            this.Time,
            this.TRIMP});
            this.dataGridView1.ContextMenuStrip = this.contextMenuTable;
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridView1.RowHeadersVisible = false;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dataGridView1.RowsDefaultCellStyle = dataGridViewCellStyle4;
            this.dataGridView1.ShowEditingIcon = false;
            this.dataGridView1.Size = new System.Drawing.Size(483, 133);
            this.dataGridView1.TabIndex = 4;
            this.dataGridView1.TabStop = false;
            // 
            // Zone
            // 
            this.Zone.Frozen = true;
            this.Zone.HeaderText = "Zone (%max HR)";
            this.Zone.Name = "Zone";
            this.Zone.ReadOnly = true;
            this.Zone.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Zone.Width = 120;
            // 
            // HeartRate
            // 
            this.HeartRate.Frozen = true;
            this.HeartRate.HeaderText = "Heart rate (BPM)";
            this.HeartRate.Name = "HeartRate";
            this.HeartRate.ReadOnly = true;
            this.HeartRate.Width = 120;
            // 
            // Time
            // 
            this.Time.Frozen = true;
            this.Time.HeaderText = "Time (min)";
            this.Time.Name = "Time";
            this.Time.ReadOnly = true;
            this.Time.Width = 120;
            // 
            // TRIMP
            // 
            this.TRIMP.HeaderText = "TRIMP";
            this.TRIMP.Name = "TRIMP";
            this.TRIMP.ReadOnly = true;
            this.TRIMP.Width = 120;
            // 
            // contextMenuTable
            // 
            this.contextMenuTable.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.copyTableToClipboardToolStripMenuItem});
            this.contextMenuTable.Name = "contextMenuTable";
            this.contextMenuTable.Size = new System.Drawing.Size(197, 26);
            // 
            // copyTableToClipboardToolStripMenuItem
            // 
            this.copyTableToClipboardToolStripMenuItem.Name = "copyTableToClipboardToolStripMenuItem";
            this.copyTableToClipboardToolStripMenuItem.Size = new System.Drawing.Size(196, 22);
            this.copyTableToClipboardToolStripMenuItem.Text = "Copy table to clipboard";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "label1";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tablePage);
            this.tabControl1.Controls.Add(this.graphPage);
            this.tabControl1.Location = new System.Drawing.Point(15, 25);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(493, 272);
            this.tabControl1.TabIndex = 6;
            // 
            // tablePage
            // 
            this.tablePage.Controls.Add(this.tableChart);
            this.tablePage.Controls.Add(this.dataGridView1);
            this.tablePage.Location = new System.Drawing.Point(4, 22);
            this.tablePage.Name = "tablePage";
            this.tablePage.Padding = new System.Windows.Forms.Padding(3);
            this.tablePage.Size = new System.Drawing.Size(485, 246);
            this.tablePage.TabIndex = 0;
            this.tablePage.Text = "Summary";
            this.tablePage.UseVisualStyleBackColor = true;
            // 
            // tableChart
            // 
            this.tableChart.BackColor = System.Drawing.Color.White;
            this.tableChart.Border = ZoneFiveSoftware.Common.Visuals.ControlBorder.Style.SmallRoundShadow;
            this.tableChart.Location = new System.Drawing.Point(0, 133);
            this.tableChart.Name = "tableChart";
            this.tableChart.Size = new System.Drawing.Size(482, 287);
            this.tableChart.TabIndex = 5;
            // 
            // graphPage
            // 
            this.graphPage.Controls.Add(this.chartBase);
            this.graphPage.Location = new System.Drawing.Point(4, 22);
            this.graphPage.Name = "graphPage";
            this.graphPage.Padding = new System.Windows.Forms.Padding(3);
            this.graphPage.Size = new System.Drawing.Size(485, 246);
            this.graphPage.TabIndex = 1;
            this.graphPage.Text = "Graph";
            this.graphPage.UseVisualStyleBackColor = true;
            // 
            // chartBase
            // 
            this.chartBase.BackColor = System.Drawing.Color.Transparent;
            this.chartBase.Border = ZoneFiveSoftware.Common.Visuals.ControlBorder.Style.SmallRoundShadow;
            this.chartBase.Location = new System.Drawing.Point(0, 0);
            this.chartBase.Name = "chartBase";
            this.chartBase.Size = new System.Drawing.Size(462, 138);
            this.chartBase.TabIndex = 0;
            // 
            // TRIMPView
            // 
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.label1);
            this.Name = "TRIMPView";
            this.Size = new System.Drawing.Size(520, 485);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.contextMenuTable.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tablePage.ResumeLayout(false);
            this.graphPage.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private class ActivityComparator : IComparer<IActivity>
        {
            #region IComparer<IActivity> Members

            public int Compare(IActivity x, IActivity y)
            {
                return x.StartTime.CompareTo(y.StartTime);
            }

            #endregion
        }

        public class TRIMPZone : IZoneCategory
        {
            #region IZoneCategory Members

            private string name = "TRIMPZone";
            private IList<INamedLowHighZone> zones;

            public TRIMPZone(IList<INamedLowHighZone> zones)
            {
                this.zones = zones;
            }

            public string Name
            {
                get
                {
                    return name;
                }
                set
                {
                    name = value;
                }
            }

            public string ReferenceId
            {
                get
                {
                    Plugin plugin = new SportTracksTRIMPPlugin.Plugin();
                    return plugin.Id.ToString();
                }
            }

            public IList<INamedLowHighZone> Zones
            {
                get { return zones; }
            }

            #endregion

            #region INotifyDataChanged Members

#pragma warning disable 67
            public event NotifyDataChangedEventHandler DataChanged;

            public bool QueueEvents
            {
                get
                {
                    return false;
                }
                set
                {
                    
                }
            }

            #endregion
        }

        public class TRIMPLowHighZone : INamedLowHighZone
        {

            #region INamedLowHighZone Members

            private float low, high;

            public TRIMPLowHighZone(IList<double> lowHigh)
            {
                this.low = (float)lowHigh[0];
                this.high = (float)lowHigh[1];
            }

            public float High
            {
                get { return high; }
            }

            public float Low
            {
                get { return low; }
            }

            public string Name
            {
                get { return low + "/" + high; }
            }

            #endregion
        }
    }
}
