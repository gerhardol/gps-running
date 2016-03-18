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
using System.Drawing;
using System.IO;
using System.Globalization;

using ZoneFiveSoftware.Common.Visuals;
using ZoneFiveSoftware.Common.Visuals.Chart;
using ZoneFiveSoftware.Common.Data;
using ZoneFiveSoftware.Common.Data.Fitness;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Collections;
using System.Reflection;
using System.ComponentModel;

using ZoneFiveSoftware.Common.Data.Measurement;
using ZoneFiveSoftware.Common.Visuals.Fitness;
using ZoneFiveSoftware.Common.Data.Algorithm;
#if !ST_2_1
using ZoneFiveSoftware.Common.Visuals.Forms;
using ZoneFiveSoftware.Common.Visuals.Mapping;
#endif

using GpsRunningPlugin.Properties;
using GpsRunningPlugin.Util;
using TrailsPlugin.Integration;
using TrailsPlugin.Data;
using TrailsPlugin.UI.MapLayers;

namespace GpsRunningPlugin.Source
{
    public partial class OverlayView : UserControl
    {
#if !ST_2_1
        public OverlayView(IDetailPage detailPage, IDailyActivityView view)
           : this()
        {
            m_DetailPage = detailPage;
            m_view = view;
            if (m_DetailPage != null)
            {
                expandButton.Visible = true;
            }
            m_layer = TrailPointsLayer.Instance((IView)view);

            if (Settings.ShowTrailsHint)
            {
                String oneTimeMessage = "The Trails plugin feature set is more or less superseding Overlay, you should try Trails instead. Just select the Splits trail. See the Overlay or Trails documentation for more information. This message will not be shown again.";
                DialogResult r = MessageDialog.Show(oneTimeMessage, "Trails Plugin", MessageBoxButtons.OK, MessageBoxIcon.Information);
                if (r == DialogResult.OK)
                {
                    Settings.ShowTrailsHint = false;
                }
            }
        }

        //popup dialog
        public OverlayView(IDailyActivityView view)
            : this(true)
        {
            m_view = view;
            m_layer = TrailPointsLayer.Instance((IView)view);
            ShowPage("");
        }

        public OverlayView(IActivityReportsView view)
            : this(true)
        {
            m_layer = TrailPointsLayer.Instance((IView)view);
            ShowPage("");
        }

        //UniqueRoutes sendto
        public OverlayView(IList<IActivity> activities, IDailyActivityView view)
            : this(view)
        {
            this.Activities = activities;
        }
        public OverlayView(IList<IActivity> activities, IActivityReportsView view)
            : this(view)
        {
            this.Activities = activities;
        }
#endif
        //popup view
        public OverlayView(bool showDialog)
            : this()
        {
            if (showDialog)
            {
                //Theme and Culture must be set manually
                this.ThemeChanged(m_visualTheme);
                this.UICultureChanged(m_culture);
                this.ShowDialog();
            }
        }

        //ComparePlannedPopup
        public OverlayView(IList<IActivity> aAct, bool showDialog)
            : this(showDialog)
        {
            this.Activities = aAct;
            ShowPage("");
        }

        public OverlayView()
        {
            InitializeComponent();
            InitControls();

            heartRate.Checked = Settings.ShowHeartRate;
            pace.Checked = Settings.ShowPace;
            speed.Checked = Settings.ShowSpeed;
            power.Checked = Settings.ShowPower;
            cadence.Checked = Settings.ShowCadence;
            elevation.Checked = Settings.ShowElevation;
            time.Checked = Settings.ShowTime;
            distance.Checked = Settings.ShowDistance;

            if (Settings.UseTimeXAxis)
            {
                useTime.Checked = true;
                useDistance.Checked = false;
            }
            else
            {
                useDistance.Checked = true;
                useTime.Checked = false;
            }

            RefreshColumns();
            treeListAct.RowDataRenderer.RowAlternatingColors = true;
            treeListAct.LabelProvider = new ActivityLabelProvider();
            treeListAct.CheckedChanged += new TreeList.ItemEventHandler(treeView_CheckedChanged);
            treeListAct.ContextMenuStrip = treeListContextMenuStrip;

            actionBanner1.Text = Resources.OverlayChart;
            actionBanner1.MenuClicked += actionBanner1_MenuClicked;
            
            chart.SelectData += new ChartBase.SelectDataHandler(chart_SelectData);
            chart.SelectingData += new ChartBase.SelectDataHandler(chart_SelectingData);
            chart.Click += new EventHandler(chart_Click);

            this.selectWithURMenuItem.Enabled = TrailsPlugin.Integration.UniqueRoutes.UniqueRouteIntegrationEnabled;
            this.setOffsetWithURMenuItem.Enabled = TrailsPlugin.Integration.UniqueRoutes.UniqueRouteIntegrationEnabled;

            UpdateChartBar();
        }

        private void UpdateChartBar()
        {
            if (Settings.ShowChartBar)
            {
                this.tableLayoutPanel1.RowStyles[1].Height = 50;
            }
            else
            {
                this.tableLayoutPanel1.RowStyles[1].Height = 0;
            }
        }
        
        private void RefreshColumns()
        {
            treeListAct.Columns.Clear();
            foreach (string id in Settings.TreeListPermanentActColumns)
            {
                foreach (IListColumnDefinition columnDef in OverlayColumnIds.PermanentColumnDefs())
                {
                    if (columnDef.Id == id)
                    {
                        TreeList.Column column = new TreeList.Column(
                            columnDef.Id,
                            columnDef.Text(columnDef.Id),
                            columnDef.Width,
                            columnDef.Align
                        );
                        treeListAct.Columns.Add(column);
                        break;
                    }
                }
            }

            foreach (string id in Settings.TreeListActColumns)
            {
                foreach (IListColumnDefinition columnDef in OverlayColumnIds.ColumnDefs())
                {
                    if (columnDef.Id == id)
                    {
                        TreeList.Column column = new TreeList.Column(
                            columnDef.Id,
                            columnDef.Text(columnDef.Id),
                            columnDef.Width,
                            columnDef.Align
                        );
                        treeListAct.Columns.Add(column);
                        break;
                    }
                }
            }
        }

        public void InitControls()
        {
            SizeChanged += new EventHandler(OverlayView_SizeChanged);
#if ST_2_1
            this.tableSettingsMenuItem.Enabled = false;
#endif
#if !ST_2_1
            expandButton.BackgroundImage = CommonResources.Images.View2PaneLowerHalf16;
            //this.treeListAct.SelectedItemsChanged += new System.EventHandler(summaryList_SelectedItemsChanged);
#endif
            this.treeListAct.Click += new System.EventHandler(summaryList_Click);
            //TODO: Implement Toolbar
            this.showToolBarMenuItem.Visible = false;
            //Enabled when possible
            expandButton.Visible = false;
        }

        public void ShowDialog()
        {
            popupForm = new Form();
            popupForm.Controls.Add(this);
            popupForm.Size = Settings.WindowSize;
            //6 is the distance between controls
            popupForm.MinimumSize = new System.Drawing.Size(6 + elevation.Width + elevation.Left + this.Width - btnSaveImage.Left, 0);
            this.Size = new Size(Parent.Size.Width - 17, Parent.Size.Height - 38);
            this.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                    | System.Windows.Forms.AnchorStyles.Right | System.Windows.Forms.AnchorStyles.Bottom)));
            popupForm.SizeChanged += new EventHandler(form_SizeChanged);
            setSize();
            popupForm.Icon = Icon.FromHandle(Properties.Resources.Image_32_Overlay.GetHicon());
            Parent.SizeChanged += new EventHandler(Parent_SizeChanged);
            popupForm.StartPosition = FormStartPosition.CenterScreen;
            popupForm.FormClosed += new FormClosedEventHandler(popupForm_FormClosed);
            popupForm.Show();
        }

        public IList<IActivity> Activities
        {
            get
            {
                return m_activities;
            }
            set
            {
                //Deactivate listeners for activities
                this.deactivateListeners();
                m_activities.Clear();
                foreach (IActivity activity in value)
                {
                    m_activities.Add(activity);
                }
                this.activateListeners();

                m_activities.Sort(new ActivityDateComparer());
                nextIndex = 0;
                m_actWrappers.Clear();
                foreach (IActivity activity in m_activities)
                {
                    m_actWrappers.Add(new ActivityWrapper(activity, newColor()));
                }

                CommonData.refActWrapper = null;
                RefreshPage();
                if (popupForm != null)
                {
                    if (m_activities.Count == 1)
                        popupForm.Text = Resources.O1;
                    else
                        popupForm.Text = String.Format(Resources.O2, m_activities.Count);
                }

                if (m_layer != null)
                {
                    m_layer.DoZoom();
                }
            }
        }
        public void ThemeChanged(ITheme visualTheme)
        {
            m_visualTheme = visualTheme;

            this.chart.ThemeChanged(visualTheme);
            this.panel1.ThemeChanged(visualTheme);
            this.panel2.ThemeChanged(visualTheme);
            this.actionBanner1.ThemeChanged(visualTheme);
            this.treeListAct.ThemeChanged(visualTheme);
            //Non ST controls
            chartBackgroundPanel.BackColor = visualTheme.Window;
            this.BackColor = visualTheme.Control;
        }

        public void UICultureChanged(CultureInfo culture)
        {
            m_culture = culture;
            actionBanner1.Text = Resources.OverlayChart;
            showMeanMenuItem.Text = Resources.BCA;
            showRollingAverageMenuItem.Text = Resources.BMA;

            // Chart bar text and buttons labels
            labelXaxis.Text = StringResources.XAxis + ":"; 
            labelYaxis.Text = StringResources.YAxis + ":";
            useTime.Text = CommonResources.Text.LabelTime;
            useDistance.Text = CommonResources.Text.LabelDistance;
            heartRate.Text = CommonResources.Text.LabelHeartRate;
            pace.Text = CommonResources.Text.LabelPace;
            speed.Text = CommonResources.Text.LabelSpeed;
            power.Text = CommonResources.Text.LabelPower;
            cadence.Text = CommonResources.Text.LabelCadence;
            elevation.Text = CommonResources.Text.LabelElevation;
            time.Text = CommonResources.Text.LabelTime;
            distance.Text = CommonResources.Text.LabelDistance;

            // Chart context menu and submenus text
            xAxisMenuItem.Text = StringResources.XAxis;
            xAxisTimeMenuItem.Text = CommonResources.Text.LabelTime;
            xAxisDistanceMenuItem.Text = CommonResources.Text.LabelDistance;
            
            showMenuItem.Text = StringResources.YAxis;
            showDiffMenuItem.Text = StringResources.Difference;

            showHRMenuItem.Text = CommonResources.Text.LabelHeartRate;
            showPaceMenuItem.Text = CommonResources.Text.LabelPace;
            showSpeedMenuItem.Text = CommonResources.Text.LabelSpeed;
            showPowerMenuItem.Text = CommonResources.Text.LabelPower;
            showCadenceMenuItem.Text = CommonResources.Text.LabelCadence;
            showElevationMenuItem.Text = CommonResources.Text.LabelElevation;
            showTimeMenuItem.Text = CommonResources.Text.LabelTime;
            showDistanceMenuItem.Text = CommonResources.Text.LabelDistance;

            showHRDiffMenuItem.Text = CommonResources.Text.LabelHeartRate;
            showTimeDiffMenuItem.Text = CommonResources.Text.LabelTime;
            showDistDiffMenuItem.Text = CommonResources.Text.LabelDistance;

            offsetMenuItem.Text = Resources.SetOffset;
            setRollAvgWidthMenuItem.Text = Resources.SetMovingAveragePeriod;

            setRefActMenuItem.Text = StringResources.SetRefActivity;

            showChartToolsMenuItem.Text = Resources.Menu_ShowChartBar;
            showToolBarMenuItem.Text = Resources.Menu_ShowToolBar; 

            // table context menu and submenus text
            tableSettingsMenuItem.Text = Resources.TableSettings;
            setRefTreeListMenuItem.Text = StringResources.SetRefActivity;
            this.advancedMenuItem.Text = StringResources.UI_Activity_List_Advanced;
            this.limitActivityMenuItem.Text = StringResources.UI_Activity_List_LimitSelection;
            this.selectWithURMenuItem.Text = string.Format(StringResources.UI_Activity_List_URSelect, "");
            this.setOffsetWithURMenuItem.Text = Resources.SetOffsetWithUR;
            visibleMenuItem.Text = StringResources.Visible;
            allVisibleMenuItem.Text = CommonResources.Text.ActionSelectAll;
            noneVisibleMenuItem.Text = CommonResources.Text.ActionSelectNone;

            int max = Math.Max(labelXaxis.Location.X + labelXaxis.Size.Width,
                                labelYaxis.Location.X + labelYaxis.Size.Width) + 5;
            useTime.Location = new Point(max, labelXaxis.Location.Y);
            correctUI(new Control[] { useTime, useDistance });
            heartRate.Location = new Point(max, labelYaxis.Location.Y);
            correctUI(new Control[] { heartRate, pace, speed, power, cadence, elevation, time, distance });

            RefreshColumns();
            RefreshPage();
        }

        public bool HidePage()
        {
            m_showPage = false;
            this.deactivateListeners();
#if !ST_2_1
            if (m_layer != null)
            {
                m_layer.HidePage();
            }
#endif
            return true;
        }

        public void ShowPage(string bookmark)
        {
            m_showPage = true;
            activateListeners();
            RefreshPage();
#if !ST_2_1
            if (m_layer != null)
            {
                m_layer.ShowPage(bookmark);
                if (popupForm==null && m_firstZoom)
                {
                    m_layer.DoZoom();
                }
                m_firstZoom = false;
            }
#endif
        }

        private void activateListeners()
        {
            if (m_showPage)
            {
                if (m_activities != null)
                {
                    foreach (IActivity activity in m_activities)
                    {
#if ST_2_1
                        activity.DataChanged += new NotifyDataChangedEventHandler(activity_DataChanged);
#else
                        activity.PropertyChanged += new PropertyChangedEventHandler(Activity_PropertyChanged);
#endif
                    }
                }
            }
        }
        private void deactivateListeners()
        {
            if (m_activities != null)
            {
                foreach (IActivity activity in m_activities)
                {
#if ST_2_1
                    activity.DataChanged -= new NotifyDataChangedEventHandler(activity_DataChanged);
#else
                    activity.PropertyChanged -= new PropertyChangedEventHandler(Activity_PropertyChanged);
#endif
                }
            }
        }

        void popupForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.HidePage();
        }

        public void RefreshPage()
        {
            if (m_showPage)
            {
                updateActivities();
                updateChart();
                updateRoute();
            }
        }
        private void updateActivities()
        {
            m_activities.Sort(new ActivityDateComparer());

            nextIndex = 0;

            treeListAct.ClearAllChecked();
            treeListAct.RowData = m_actWrappers;
            foreach (ActivityWrapper wrapper in m_actWrappers)
            {
                treeListAct.SetChecked(wrapper, true);
            }
            if (m_actWrappers.Count > 0)
            {
                //TODO: Try to keep refAct
                CommonData.refActWrapper = m_actWrappers[0];
            }

        }

        private class ActivityDateComparer : Comparer<IActivity>
        {
            public override int Compare(IActivity x, IActivity y)
            {
                return x.StartTime.CompareTo(y.StartTime);
            }
        }

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
        private void setSize()
        {
          // Using SplitContainers eliminates the adjustions previously required
        }

        /*************************************************/
        private int nextIndex;

        private Color getColor(int color)
        {
            switch (color)
            {
                case 0: return Color.Blue;
                case 1: return Color.Red;
                case 2: return Color.Green;
                case 3: return Color.Orange;
                case 4: return Color.Plum;
                case 5: return Color.HotPink;
                case 6: return Color.Gold;
                case 7: return Color.Silver;
                case 8: return Color.YellowGreen;
                case 9: return Color.Turquoise;
            }
            return Color.Black;
        }

        private Color newColor()
        {
            int color = nextIndex;
            nextIndex = (nextIndex + 1) % 10;
            return getColor(color);
        }

        private void addSeries(Interpolator interpolator, 
            CanInterpolater canInterpolator, IAxis axis,
            GetDataSeries getDataSeries)
        {
            IList<ChartDataSeries> list = buildSeries(interpolator, canInterpolator, axis, getDataSeries);
            IList<ChartDataSeries> averages = new List<ChartDataSeries>();
            foreach (ChartDataSeries series in list)
            {
                series.ValueAxis = axis;
                chart.DataSeries.Add(series);
                if (Settings.ShowMovingAverage)
                {
                    averages.Add(makeMovingAverage(series, axis));
                }
            }
            if (Settings.ShowCategoryAverage && m_activities.Count > 1)
            {
                chart.DataSeries.Add(getCategoryAverage(axis,list));
                if (Settings.ShowMovingAverage)
                {
                    ChartDataSeries average = getCategoryAverage(axis, averages);
                    average.LineWidth = 2;
                    chart.DataSeries.Add(average);
                }
            }
        }

        private ChartDataSeries makeMovingAverage(ChartDataSeries series, IAxis axis)
        {
            if (series.Points.Count == 0) return new ChartDataSeries(chart, axis);
            double size;
            if (Settings.UseTimeXAxis)
            {
                size = Settings.MovingAverageTime; //No ConvertFrom, time is always in seconds
            }
            else
            {
                size = UnitUtil.Distance.ConvertFrom(Settings.MovingAverageLength);
            }
            ChartDataSeries average = new ChartDataSeries(chart, axis);
            Queue<double> queueX = new Queue<double>(), queueSum = new Queue<double>();
            double sum = 0;
            double lastX = 0, lastY = 0, firstX=0;
            bool first = true;
            foreach (PointF point in series.Points.Values)
            {
                if (!first)
                {
                    double diffX = point.X - lastX;
                    double diffY = point.Y - lastY;
                    double area = diffX * lastY + diffY * diffX / 2.0; 
                    sum += area;
                    if (size > 0)
                    {
                        queueX.Enqueue(point.X);
                        queueSum.Enqueue(area);
                    }
                }
                else
                {
                    firstX = point.X;
                }
                float y = float.NaN;
                if (first && size == 0)
                {
                    y = point.Y;
                }
                else
                {
                    if (size == 0)
                    {
                        y = (float)(sum / (point.X - firstX));
                    }
                    else
                    {
                        if (queueX.Count > 0 && 
                            size <= point.X - queueX.Peek())
                        {
                            float diffX = (float)(point.X - queueX.Dequeue());
                            sum -= queueSum.Dequeue();
                            y = (float)(sum / diffX);
                        }
                    }
                }
                if (!y.Equals(float.NaN) && 
                    !average.Points.ContainsKey(point.X))
                {
                    average.Points.Add(point.X, new PointF(point.X, y));
                }
                lastX = point.X;
                lastY = point.Y;
                if (first) first = false;                
            }
            chart.DataSeries.Add(average);
            average.LineColor = series.LineColor;
            average.LineWidth = 2;
            m_series2activity.Add(average, m_series2activity[series]);
            return average;
        }
        
        private ChartDataSeries getCategoryAverage(IAxis axis,
            IList<ChartDataSeries> list)
        {
            ChartDataSeries average = new ChartDataSeries(chart, axis);
            SortedList<float, bool> xs = new SortedList<float, bool>();
            foreach (ChartDataSeries series in list)
            {
                foreach (PointF point in series.Points.Values)
                {
                    if (!xs.ContainsKey(point.X))
                    {
                        xs.Add(point.X, true);
                    }
                }
            }
            foreach (float x in xs.Keys)
            {
                int seen = 0;
                float y = 0;
                foreach (ChartDataSeries series in list)
                {
                    float theX = x;
                    float theY = series.GetYValueAtX(ref theX);
                    if (!theY.Equals(float.NaN))
                    {
                        y += theY;
                        seen++;
                    }
                }
                if (seen > 1 &&
                    average.Points.IndexOfKey(x) == -1)
                {
                    average.Points.Add(x, new PointF(x, y / seen));
                }
             }
            return average;
        }

        private void updateChart()
        {
            //TODO: add show working
            chart.Visible = false;
            chart.UseWaitCursor = true;
//            this.chart.BackgroundImage = ZoneFiveSoftware.Common.Visuals.CommonResources.Images.Hourglass16;
//            this.chart.BackgroundImageLayout = ImageLayout.Center;
            chart.BeginUpdate();
            chart.AutozoomToData(false);
            chart.DataSeries.Clear();
            chart.YAxisRight.Clear();
            m_series2activity.Clear();
            bool useRight = false;

            if (Settings.UseTimeXAxis)
            {
                chart.XAxis.Formatter = new Formatter.SecondsToTime();
                chart.XAxis.Label = UnitUtil.Time.LabelAxis;
            }
            else
            {
                chart.XAxis.Formatter = new Formatter.General(UnitUtil.Distance.DefaultDecimalPrecision);
                chart.XAxis.Label = UnitUtil.Distance.LabelAxis; ;
            }

            if (Settings.ShowHeartRate)
            {
                nextIndex = 0;
                useRight = true;
                chart.YAxis.Formatter = new Formatter.General(UnitUtil.HeartRate.DefaultDecimalPrecision);
                chart.YAxis.Label = UnitUtil.HeartRate.LabelAxis;
                addSeries(
                    delegate(float value)
                    {
                        return value;
                    },
                    delegate(ActivityInfo info)
                    {
                        return info.Activity.HeartRatePerMinuteTrack != null;
                    },
                    chart.YAxis,
                    delegate(ActivityInfo info)
                    {
                        return info.Activity.HeartRatePerMinuteTrack;
                    }
                    );
            }
            if (Settings.ShowPace)
            {
                IAxis axis;
                if (useRight)
                {
                    axis = new RightVerticalAxis(chart);
                    chart.YAxisRight.Add(axis);
                }
                else
                {
                    axis = chart.YAxis;
                    useRight = true;
                }
                nextIndex = 0;
                axis.Formatter = new Formatter.SecondsToTime();
                axis.Label = UnitUtil.Pace.LabelAxis;
                axis.SmartZoom = true;
                addSeries(
                    delegate(float value)
                    {
                        return UnitUtil.Pace.ConvertFrom(value);
                    },
                    delegate(ActivityInfo info)
                    {
                        return info.SmoothedSpeedTrack.Count > 0;
                    },
                    axis,
                    delegate(ActivityInfo info)
                    {
                        return info.SmoothedSpeedTrack;
                    });
            }
            if (Settings.ShowSpeed)
            {
                IAxis axis;
                if (useRight)
                {
                    axis = new RightVerticalAxis(chart);
                    chart.YAxisRight.Add(axis);
                }
                else
                {
                    axis = chart.YAxis;
                    useRight = true;
                }
                nextIndex = 0;
                axis.Formatter = new Formatter.General(UnitUtil.Speed.DefaultDecimalPrecision);
                axis.Label = UnitUtil.Speed.LabelAxis;
                addSeries(
                    delegate(float value)
                    {
                        return UnitUtil.Speed.ConvertFrom(value);
                    },
                    delegate(ActivityInfo info) 
                    { 
                        return info.SmoothedSpeedTrack.Count > 0; 
                    },
                    axis,
                    delegate(ActivityInfo info)
                    {
                        return info.SmoothedSpeedTrack;
                    });
            }
            if (Settings.ShowPower)
            {
                IAxis axis;
                if (useRight)
                {
                    axis = new RightVerticalAxis(chart);
                    chart.YAxisRight.Add(axis);
                }
                else
                {
                    axis = chart.YAxis;
                    useRight = true;
                }
                nextIndex = 0;
                axis.Formatter = new Formatter.General(UnitUtil.Power.DefaultDecimalPrecision);
                axis.Label = UnitUtil.Power.LabelAxis;
                addSeries(
                    delegate(float value)
                    {
                        return value;
                    },
                    delegate(ActivityInfo info) { return info.SmoothedPowerTrack.Count > 0; },
                    axis,
                    delegate(ActivityInfo info)
                    {
                        return info.SmoothedPowerTrack;
                    });
            }
            if (Settings.ShowCadence)
            {
                IAxis axis;
                if (useRight)
                {
                    axis = new RightVerticalAxis(chart);
                    chart.YAxisRight.Add(axis);
                }
                else
                {
                    axis = chart.YAxis;
                    useRight = true;
                }
                nextIndex = 0;
                axis.Formatter = new Formatter.General(UnitUtil.Cadence.DefaultDecimalPrecision);
                axis.Label = UnitUtil.Cadence.LabelAxis;
                addSeries(
                     delegate(float value)
                     {
                         return value;
                     },
                     delegate(ActivityInfo info) { return info.SmoothedCadenceTrack.Count > 0; },
                     axis,
                    delegate(ActivityInfo info)
                    {
                        return info.SmoothedCadenceTrack;
                    });
            }
            if (Settings.ShowElevation)
            {
                IAxis axis;
                if (useRight)
                {
                    axis = new RightVerticalAxis(chart);
                    chart.YAxisRight.Add(axis);
                }
                else
                {
                    axis = chart.YAxis;
                    useRight = true;
                }
                nextIndex = 0;
                axis.Formatter = new Formatter.General(UnitUtil.Elevation.DefaultDecimalPrecision);
                axis.ChangeAxisZoom(new Point(0, 0), new Point(10, 10));
                axis.Label = UnitUtil.Elevation.LabelAxis;
                addSeries(
                    delegate(float value)
                    {
                        return UnitUtil.Elevation.ConvertFrom(value);
                    },
                    delegate(ActivityInfo info)
                    {
                        return info.SmoothedElevationTrack.Count > 0;
                    },
                    axis,
                    delegate(ActivityInfo info)
                    {
                        return info.SmoothedElevationTrack;
                    });
            }
            if (Settings.ShowTime)
            {
                IAxis axis;
                if (useRight)
                {
                    axis = new RightVerticalAxis(chart);
                    chart.YAxisRight.Add(axis);
                }
                else
                {
                    axis = chart.YAxis;
                    useRight = true;
                }
                nextIndex = 0;
                axis.Formatter = new Formatter.SecondsToTime();
                axis.ChangeAxisZoom(new Point(0, 0), new Point(10, 10));
                axis.Label = UnitUtil.Time.LabelAxis;

                addSeries(
                    delegate(float value)
                    {
                        return value;                        
                    },
                    delegate(ActivityInfo info)
                    {
                        return info.SmoothedSpeedTrack. Count > 0;
                    },
                    axis,
                    delegate(ActivityInfo info)
                    {
                        INumericTimeDataSeries TimeTrack = new ZoneFiveSoftware.Common.Data.NumericTimeDataSeries(info.MovingDistanceMetersTrack);
                        TimeTrack = CorrectTimeDataSeriesForPauses(info, TimeTrack);

                        // Copy the modified times into the value of TimeTrack - the time values of TimeTrack will be modified later 
                        for (int i = 0; i < TimeTrack.Count; i++)
                        {
                            TimeValueEntry<float> entry = (TimeValueEntry<float>)TimeTrack[i];
                            entry.Value = TimeTrack[i].ElapsedSeconds;
                        }
                        
                        return TimeTrack;
                    });
            }
            if (Settings.ShowDistance)
            {
                IAxis axis;
                if (useRight)
                {
                    axis = new RightVerticalAxis(chart);
                    chart.YAxisRight.Add(axis);
                }
                else
                {
                    axis = chart.YAxis;
                    useRight = true;
                }
                nextIndex = 0;
                axis.Formatter = new Formatter.General(UnitUtil.Distance.DefaultDecimalPrecision);
                axis.Label = UnitUtil.Distance.LabelAxis;
                addSeries(
                    delegate(float value)
                    {
                        return UnitUtil.Distance.ConvertFrom(value);
                    },
                    delegate(ActivityInfo info)
                    {
                        return info.MovingDistanceMetersTrack.Count > 0;
                    },
                    axis,
                    delegate(ActivityInfo info)
                    {
                        return info.MovingDistanceMetersTrack;
                    });
            }
            if (Settings.ShowDiffHeartRate)
            {
                IAxis axis;
                if (useRight)
                {
                    axis = new RightVerticalAxis(chart);
                    chart.YAxisRight.Add(axis);
                }
                else
                {
                    axis = chart.YAxis;
                    useRight = true;
                }
                nextIndex = 0;
                axis.Formatter = new Formatter.General(UnitUtil.HeartRate.DefaultDecimalPrecision);
                axis.Label = UnitUtil.HeartRate.LabelAxis;
                addSeries(
                    delegate(float value)
                    {
                        return value;
                    },
                    delegate(ActivityInfo info)
                    {
                        if (CommonData.refActWrapper == null)
                            return false;
                        else
                            return info.Activity.HeartRatePerMinuteTrack != null &&
                                   CommonData.refActWrapper.Activity.HeartRatePerMinuteTrack != null;
                    },
                    chart.YAxis,
                    delegate(ActivityInfo info)
                    {
                        ActivityInfo refInfo = ActivityInfoCache.Instance.GetInfo(CommonData.refActWrapper.Activity);

                        // Remove pauses in order to be able to calculate difference
                        INumericTimeDataSeries actTrack = CorrectTimeDataSeriesForPauses(info, info.Activity.HeartRatePerMinuteTrack);
                        INumericTimeDataSeries refTrack = CorrectTimeDataSeriesForPauses(refInfo, refInfo.Activity.HeartRatePerMinuteTrack);
                        return AddPausesToTimeDataSeries(info, TrackDifferences(info, refInfo, actTrack, refTrack));
                    }
                    );
            }
            if (Settings.ShowDiffTime)
            {
                IAxis axis;
                if (useRight)
                {
                    axis = new RightVerticalAxis(chart);
                    chart.YAxisRight.Add(axis);
                }
                else
                {
                    axis = chart.YAxis;
                    useRight = true;
                }
                nextIndex = 0;
                axis.Formatter = new Formatter.SecondsToTime();
                axis.ChangeAxisZoom(new Point(0, 0), new Point(10, 10));
                axis.Label = UnitUtil.Time.LabelAxis;
                addSeries(
                    delegate(float value)
                    {
                        return value;
                    },
                    delegate(ActivityInfo info)
                    {
                        if (CommonData.refActWrapper == null)
                            return false;
                        else
                        {
                            ActivityInfo refInfo = ActivityInfoCache.Instance.GetInfo(CommonData.refActWrapper.Activity);
                            return info.SmoothedSpeedTrack.Count > 0 && refInfo.SmoothedSpeedTrack.Count > 0;
                        }
                    },
                    axis,
                    delegate(ActivityInfo info)
                    {
                        ActivityInfo refInfo = ActivityInfoCache.Instance.GetInfo(CommonData.refActWrapper.Activity);

                        // Remove pauses in order to be able to calculate difference
                        INumericTimeDataSeries actTrack = CorrectTimeDataSeriesForPauses(info, info.MovingDistanceMetersTrack);
                        INumericTimeDataSeries refTrack = CorrectTimeDataSeriesForPauses(refInfo, refInfo.MovingDistanceMetersTrack);

                        //TBD Could probably use
                        //return AddPausesToTimeDataSeries(info, TrackDifferences(info, refInfo, actTrack, refTrack));
                        //but not sure, as well as diff is inverted (probably how it should be, but still a change)

                        int refOffset = 0;
                        INumericTimeDataSeries track = new ZoneFiveSoftware.Common.Data.NumericTimeDataSeries();
                        // Create track containing time difference
                        int lastFoundElapsedTime = Math.Max(0, - refOffset);
                        foreach (TimeValueEntry<float> entry in actTrack)
                        {
                            ITimeValueEntry<float> refEntry = null;
                            for (int i = lastFoundElapsedTime - refOffset; i < refTrack.TotalElapsedSeconds - refOffset; i++)
                            {
                                refEntry = refTrack.GetInterpolatedValue(refTrack.StartTime.Add(new TimeSpan(0, 0, i)));
                                if (refEntry == null)
                                {
                                    lastFoundElapsedTime = i;
                                    break;
                                }
                                else if (refEntry.Value >= entry.Value)
                                {
                                    track.Add(actTrack.EntryDateTime(entry), (float)entry.ElapsedSeconds - (float)refEntry.ElapsedSeconds);
                                    lastFoundElapsedTime = i;
                                    break;
                                }
                            }

                        }
                        return AddPausesToTimeDataSeries(info, track);
  
                    });
            }
            if (Settings.ShowDiffDistance)
            {
                IAxis axis;
                if (useRight)
                {
                    axis = new RightVerticalAxis(chart);
                    chart.YAxisRight.Add(axis);
                }
                else
                {
                    axis = chart.YAxis;
                    useRight = true;
                }
                nextIndex = 0;
                axis.Formatter = new Formatter.General(UnitUtil.Distance.DefaultDecimalPrecision);
                axis.Label = UnitUtil.Distance.LabelAxis;
                addSeries(
                    delegate(float value)
                    {
                        return UnitUtil.Distance.ConvertFrom(value);
                    },
                    delegate(ActivityInfo info)
                    {
                        if (CommonData.refActWrapper == null)
                            return false;
                        else
                        {
                            ActivityInfo refInfo = ActivityInfoCache.Instance.GetInfo(CommonData.refActWrapper.Activity);

                                return info.MovingDistanceMetersTrack.Count > 0 &&
                                       refInfo.MovingDistanceMetersTrack.Count > 0;
                        }
                    },
                    axis,
                    delegate(ActivityInfo info)
                    {
                        ActivityInfo refInfo = ActivityInfoCache.Instance.GetInfo(CommonData.refActWrapper.Activity);
                      
                        // Remove pauses in order to be able to calculate difference
                        INumericTimeDataSeries actTrack = CorrectTimeDataSeriesForPauses(info, info.MovingDistanceMetersTrack);
                        INumericTimeDataSeries refTrack = CorrectTimeDataSeriesForPauses(refInfo, refInfo.MovingDistanceMetersTrack);
                        return AddPausesToTimeDataSeries(info, TrackDifferences(info, refInfo, actTrack, refTrack));
                    });
            }


            //chart.AutozoomToData is the slowest part of this plugin
            chart.AutozoomToData(true);
            chart.Refresh();
            chart.EndUpdate();
            chart.UseWaitCursor = false;
            chart.Visible = true;
        }

        private void updateRoute()
        {
#if !ST_2_1
            IDictionary<string, MapPolyline> routes = new Dictionary<string, MapPolyline>();
            foreach (ActivityWrapper actWrapper in getListSelection(treeListAct.CheckedElements))
            {
                TrailResult tr = new TrailResult(actWrapper);
                TrailMapPolyline m = new TrailMapPolyline(tr);
                m.Click += new MouseEventHandler(mapPoly_Click);
                if (!routes.ContainsKey(m.key))
                {
                    routes.Add(m.key, m);
                }
            }
            if (m_layer != null)
            {
                m_layer.MarkedTrailRoutes = new Dictionary<string, MapPolyline>();
                m_layer.TrailRoutes = routes;
            }
#endif
        }

        public void MarkTrack(IList<TrailResultMarked> atr)
        {
#if !ST_2_1
            if (m_showPage)
            {
                    IDictionary<string, MapPolyline> result = new Dictionary<string, MapPolyline>();
                    foreach (TrailResultMarked trm in atr)
                    {
                        foreach (TrailMapPolyline m in TrailMapPolyline.GetTrailMapPolyline(trm.trailResult, trm.selInfo))
                        {
                            m.Click += new MouseEventHandler(mapPoly_Click);
                            result.Add(m.key, m);
                        }
                    }
                    m_layer.MarkedTrailRoutes = result;
            }
#endif
        }

        public void EnsureVisible(IList<TrailResult> atr, bool chart)
        {
            if (atr != null && atr.Count > 0 && atr[0].Activity != null)
            {
                foreach (ActivityWrapper urr in (IList<ActivityWrapper>)treeListAct.RowData)
                {
                    if (atr[0].Activity == urr.Activity)
                    {
                        this.treeListAct.EnsureVisible(urr);
                    }
                }
            }
        }

        public static IList<ActivityWrapper> getListSelection(System.Collections.IList tlist)
        {
            IList<ActivityWrapper> aTr = new List<ActivityWrapper>();
            if (tlist != null)
            {
                foreach (object t in tlist)
                {
                    if (t != null)
                    {
                        aTr.Add(((ActivityWrapper)t));
                    }
                }
            }
            return aTr;
        }

        //TBD Not handling differences over distance correctly
        private INumericTimeDataSeries TrackDifferences(ActivityInfo info, ActivityInfo refInfo,
            INumericTimeDataSeries actTrack, INumericTimeDataSeries refTrack)
        {
            //Adjust part compared from track start
            int actOffset = (int)(actTrack.StartTime - info.ActualTrackStart).TotalSeconds;
            int refOffset = (int)(refTrack.StartTime - info.ActualTrackStart).TotalSeconds - actOffset;
            int offset = (int)((actTrack.StartTime - info.ActualTrackStart).TotalSeconds -
                (refTrack.StartTime - refInfo.ActualTrackStart).TotalSeconds);

            INumericTimeDataSeries track = new ZoneFiveSoftware.Common.Data.NumericTimeDataSeries();
            foreach (ITimeValueEntry<float> p in actTrack)
            {
                DateTime refActualTime = refTrack.StartTime.AddSeconds(p.ElapsedSeconds + offset);

                ITimeValueEntry<float> refDist = refTrack.GetInterpolatedValue(refActualTime);
                if (refDist != null)
                {
                    DateTime actualTime = actTrack.StartTime.AddSeconds(p.ElapsedSeconds);
                    track.Add(actualTime, p.Value - refDist.Value);
                }
            }
            return track;
        }

        private INumericTimeDataSeries CorrectTimeDataSeriesForPauses(ActivityInfo info, INumericTimeDataSeries dataSeries)
        {
            IValueRangeSeries<DateTime> pauses = info.NonMovingTimes;
            INumericTimeDataSeries newDataSeries = new ZoneFiveSoftware.Common.Data.NumericTimeDataSeries();
            newDataSeries.AllowMultipleAtSameTime = true;

            foreach (TimeValueEntry<float> entry in dataSeries)
            {
                DateTime entryTime = dataSeries.EntryDateTime(entry);
                TimeSpan elapsed = DateTimeRangeSeries.TimeNotPaused(dataSeries.StartTime, entryTime, pauses);
                newDataSeries.Add(dataSeries.StartTime.Add(elapsed), entry.Value);
            }
            return newDataSeries;
        }

        private INumericTimeDataSeries AddPausesToTimeDataSeries(ActivityInfo info, INumericTimeDataSeries dataSeries)
        {
            IValueRangeSeries<DateTime> pauses = info.NonMovingTimes;
            INumericTimeDataSeries newDataSeries = new ZoneFiveSoftware.Common.Data.NumericTimeDataSeries();
            newDataSeries.AllowMultipleAtSameTime = true;
            foreach (TimeValueEntry<float> entry in dataSeries)
            {
                DateTime newEntryTime = DateTimeRangeSeries.AddTimeAndPauses(dataSeries.StartTime, new TimeSpan(0, 0, (int)entry.ElapsedSeconds), pauses);
                newDataSeries.Add(newEntryTime, entry.Value);
            }
            return newDataSeries;
        }
        
        
        private IList<ChartDataSeries> buildSeries(
            Interpolator interpolator, CanInterpolater canInterpolator, IAxis axis,
            GetDataSeries getDataSeriess)
        {
            IList<ChartDataSeries> list = new List<ChartDataSeries>();
            int index = 0;            
            
            foreach (ActivityWrapper actWrapper in m_actWrappers)
            {
                IActivity activity = actWrapper.Activity;
                ArrayList checkedWrappers = (ArrayList)treeListAct.CheckedElements;
                if (checkedWrappers.Contains(actWrapper))
                {
                    double offset=0;
                    if (Settings.UseTimeXAxis)
                    {
                        offset = actWrapper.TimeOffset.TotalSeconds;
                    }
                    else
                    {
                        offset = actWrapper.DistanceOffset;
                    }
                    ChartDataSeries series = getDataSeries(
                        interpolator, 
                        canInterpolator, 
                        ActivityInfoCache.Instance.GetInfo(activity),
                        axis,
                        getDataSeriess,
                        offset);
                    m_series2activity.Add(series, activity);
                    list.Add(series);
                }
                index++;
            }
            return list;
        }

        private delegate double Interpolator(float value);
        private delegate bool CanInterpolater(ActivityInfo info);
        private delegate INumericTimeDataSeries GetDataSeries(ActivityInfo info);

        private ChartDataSeries getDataSeries(Interpolator interpolator,
            CanInterpolater canInterpolater, ActivityInfo info, IAxis axis,
            GetDataSeries getDataSeries,
            double offset)
        {
            ChartDataSeries series = new ChartDataSeries(chart, axis);
            if (!canInterpolater(info))
            {
                return series;
            }
            bool first = true;
            float priorElapsed = float.NaN;
            INumericTimeDataSeries timeModData = CorrectTimeDataSeriesForPauses(info, getDataSeries(info));
            //Make sure track starts at correct time
            int trackOffset = (int)(timeModData.StartTime - info.ActualTrackStart).TotalSeconds;

            INumericTimeDataSeries data;
            if (Settings.UseTimeXAxis)
            {
                data = timeModData; // x-axis is time, use time with pauses excluded
            }
            else
            {
                data = getDataSeries(info); // x-axis is distance. Distance need to be looked up using original time.
            }
            foreach (ITimeValueEntry<float> entry in data)
            {
                float elapsed = entry.ElapsedSeconds;
				if ( elapsed != priorElapsed )
				{
					float x = float.NaN;
                    if (Settings.UseTimeXAxis)
					{
                        x = (float)(elapsed + offset + trackOffset);
					}
					else
					{
                        ITimeValueEntry<float> entryMoving = info.MovingDistanceMetersTrack.GetInterpolatedValue(info.MovingDistanceMetersTrack.StartTime.AddSeconds(entry.ElapsedSeconds + trackOffset));
                        if (entryMoving != null && (first || (!first && entryMoving.Value > 0)))
						{
							x = (float)UnitUtil.Distance.ConvertFrom( entryMoving.Value + offset );
						}
					}
					float y = (float)interpolator( entry.Value );
					if ( !x.Equals( float.NaN ) && !float.IsInfinity( y ) &&
						series.Points.IndexOfKey( x ) == -1 )
					{
						series.Points.Add( x, new PointF( x, y ) );
					}
				}
                priorElapsed = elapsed;
                first = false;
            }
            //activity color from activity
            //The wrapper should be included here, but the structure is not changed
            foreach (ActivityWrapper wrapper in m_actWrappers)
            {
                if (wrapper.Activity == info.Activity)
                {
                    series.LineColor = wrapper.ActColor;
                }
            }
            return series;
        }
        
        private void form_SizeChanged(object sender, EventArgs e)
        {
            if (m_showPage)
            {
                if (popupForm != null)
                {
                    Settings.WindowSize = popupForm.Size;
                }
                OverlayView_SizeChanged(sender, e);
            }
        }

        private void OverlayView_SizeChanged(object sender, EventArgs e)
        {
            if (m_showPage)
            {
                setSize();
            }
        }

#if ST_2_1
        private void activity_DataChanged(object sender, NotifyDataChangedEventArgs e)
#else
        private void Activity_PropertyChanged(object sender, PropertyChangedEventArgs e)
#endif
        {
            //Note: ST3 normally fires the event several times. Just use e.PropertyName == "GPSRoute"?
            if (this.InvokeRequired)
            {
                this.Invoke((PropertyChangedEventHandler)Activity_PropertyChanged, sender, e);
            }
            else
            {
                if (m_showPage)
                {
                    updateChart();
                    updateRoute();
                }
            }
        }

        private void treeView_CheckedChanged(object sender, EventArgs e)
        {
            updateChart();
            updateRoute();
        }

        void chart_Click(object sender, EventArgs e)
        {
#if ST_2_1
            treeListAct.Selected = new object[] { };
#else
            treeListAct.SelectedItems = new object[] { };
#endif
            m_bSelectDataFlag = false;

			if ( m_bSelectingDataFlag )
			{
				m_bSelectingDataFlag = false;
				return;
			}
        }

		void chart_SelectingData(object sender, ChartBase.SelectDataEventArgs e)
		{
            if ((m_lastSelectedSeries != null) && (m_lastSelectedSeries != e.DataSeries))
            {
#if ST_2_1
                treeListAct.Selected = new object[] { };
#else
                treeListAct.SelectedItems = new object[] { };
#endif
                m_lastSelectedSeries.ValueAxis.LabelColor = Color.Black;
            }
			m_lastSelectedSeries = e.DataSeries;
			m_bSelectingDataFlag = true;
		}

        void chart_SelectData(object sender, ChartBase.SelectDataEventArgs e)
        {
            if (e != null && e.DataSeries != null)
            {
                // Select the row of the treeview
                if (m_series2activity.ContainsKey(e.DataSeries))
                {
#if ST_2_1
                    treeListAct.Selected = new object[] { actWrappers[activities.IndexOf(series2activity[e.DataSeries])] };
#else
                    treeListAct.SelectedItems = new object[] { m_actWrappers[m_activities.IndexOf(m_series2activity[e.DataSeries])] };
#endif
                }
                else
                {
#if ST_2_1
                    treeListAct.Selected = new object[] { };
#else
                    treeListAct.SelectedItems = new object[] { };
#endif
                }
                e.DataSeries.ValueAxis.LabelColor = e.DataSeries.SelectedColor;
                m_bSelectingDataFlag = false;
                if (m_bSelectDataFlag)
                    chart_SelectingData(sender, e);
                m_bSelectDataFlag = true;

                if (m_series2activity.ContainsKey(e.DataSeries) && m_activities.Contains(m_series2activity[e.DataSeries]))
                {
                    //from Trails plugin
                    TrailResult tr = new TrailResult(m_actWrappers[m_activities.IndexOf(m_series2activity[e.DataSeries])]);
                    IList<float[]> regions;
                    e.DataSeries.GetSelectedRegions(out regions);

                    IList<TrailResultMarked> results = new List<TrailResultMarked>();
                    if (Settings.UseTimeXAxis)
                    {
                        IValueRangeSeries<DateTime> t = new ValueRangeSeries<DateTime>();
                        foreach (float[] at in regions)
                        {
                            t.Add(new ValueRange<DateTime>(
                                tr.getActivityTime(at[0]),
                                tr.getActivityTime(at[1])));
                        }
                        results.Add(new TrailResultMarked(tr, t));
                    }
                    else
                    {
                        IValueRangeSeries<double> t = new ValueRangeSeries<double>();
                        foreach (float[] at in regions)
                        {
                            t.Add(new ValueRange<double>(
                                tr.getActivityDist(UnitUtil.Distance.ConvertTo(at[0], CommonData.refActWrapper.Activity)),
                                tr.getActivityDist(UnitUtil.Distance.ConvertTo(at[1], CommonData.refActWrapper.Activity))));
                        }
                        results.Add(new TrailResultMarked(tr, t));
                    }

                    this.MarkTrack(results);
                    this.EnsureVisible(new List<TrailResult> { tr }, false);
                }
            }
        }

        private void Parent_SizeChanged(object sender, EventArgs e)
        {
            if (m_showPage)
            {
                setSize();
            }
        }

        private void useTime_CheckedChanged(object sender, EventArgs e)
        {
            if (!Settings.UseTimeXAxis)
            {
                Settings.UseTimeXAxis = true;
                updateChart();
                treeListAct.Refresh();
            }
        }

        private void useDistance_CheckedChanged(object sender, EventArgs e)
        {
            if (Settings.UseTimeXAxis)
            {
                Settings.UseTimeXAxis = false;
                updateChart();
                treeListAct.Refresh();
            }
        }

        private void heartRate_CheckedChanged(object sender, EventArgs e)
        {
            Settings.ShowHeartRate = heartRate.Checked;
            updateChart();
        }

        private void pace_CheckedChanged(object sender, EventArgs e)
        {
            Settings.ShowPace = pace.Checked;
            updateChart();
        }

        private void speed_CheckedChanged(object sender, EventArgs e)
        {
            Settings.ShowSpeed = speed.Checked;
            updateChart();
        }

        private void power_CheckedChanged(object sender, EventArgs e)
        {
            Settings.ShowPower = power.Checked;
            updateChart();
        }

        private void cadence_CheckedChanged(object sender, EventArgs e)
        {
            Settings.ShowCadence = cadence.Checked;
            updateChart();
        }

        private void elevation_CheckedChanged(object sender, EventArgs e)
        {
            Settings.ShowElevation = elevation.Checked;
            updateChart();
        }

        private void time_CheckedChanged(object sender, EventArgs e)
        {
            Settings.ShowTime = time.Checked;
            updateChart();
        }

        private void distance_CheckedChanged(object sender, EventArgs e)
        {
            Settings.ShowDistance = distance.Checked;
            updateChart();
        }

		private void btnSaveImage_Click( object sender, EventArgs e )
		{
#if ST_2_1
            OverlaySaveImageInfoPage siiPage = new OverlaySaveImageInfoPage();
            siiPage.UICultureChanged(m_culture); //Should be in ST3 too...
#else
            SaveImageDialog siiPage = new SaveImageDialog();
#endif
            siiPage.ThemeChanged(m_visualTheme);

            if (string.IsNullOrEmpty(saveImageProperties_fileName))
            {
                saveImageProperties_fileName = String.Format("{0} {1}", "Overlay", DateTime.Now.ToShortDateString());
                char[] cInvalid = Path.GetInvalidFileNameChars();
                for (int i = 0; i < cInvalid.Length; i++)
                    saveImageProperties_fileName = saveImageProperties_fileName.Replace(cInvalid[i], '-');
            }
            if (string.IsNullOrEmpty(Settings.SavedImageFolder))
            {
                Settings.SavedImageFolder = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            }
                siiPage.FileName = Settings.SavedImageFolder + Path.DirectorySeparatorChar + saveImageProperties_fileName;
            siiPage.ImageSize = Settings.SavedImageSize;
			siiPage.ImageFormat = Settings.SavedImageFormat;

			siiPage.ShowDialog();

			if ( siiPage.DialogResult == DialogResult.OK )
			{
				saveImageProperties_fileName = Path.GetFileName(siiPage.FileName);
                Settings.SavedImageFolder = Path.GetDirectoryName(siiPage.FileName);
                Settings.SavedImageSize = siiPage.ImageSize;
				Settings.SavedImageFormat = siiPage.ImageFormat;
#if ST_2_1
                if ((!System.IO.File.Exists(siiPage.FileName)) ||
                    (MessageDialog.Show(String.Format(SaveImageResources.FileAlreadyExists, siiPage.FileName),
                                        SaveImageResources.SaveImage, MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes))
					chart.SaveImage( siiPage.ImageSize, siiPage.FileName, siiPage.ImageFormat ); 
#else
                chart.SaveImage(siiPage.ImageSizes[siiPage.ImageSize], siiPage.FileName, siiPage.ImageFormat); 
#endif
            }
		}

        void actionBanner1_MenuClicked(object sender, System.EventArgs e)
        {
            //actionBanner1.ContextMenuStrip.Width = 100;
            actionBanner1.ContextMenuStrip.Show(actionBanner1.Parent.PointToScreen(new System.Drawing.Point(actionBanner1.Right - actionBanner1.ContextMenuStrip.Width - 2,
                actionBanner1.Bottom + 1)));
        }

        private void bannerContextMenuStrip_Opening(object sender, CancelEventArgs e)
        {
            showMeanMenuItem.Checked = Settings.ShowCategoryAverage;
            showRollingAverageMenuItem.Checked = Settings.ShowMovingAverage;

            setRefActMenuItem.Enabled = (treeListAct.SelectedItems.Count == 1);
            showDiffMenuItem.Enabled = (CommonData.refActWrapper != null);
            this.offsetMenuItem.Enabled = (m_activities != null && m_activities.Count > 1 && treeListAct.SelectedItems.Count > 0);
            this.setRefActMenuItem.Text = StringResources.SetRefActivity;
            if (CommonData.refActWrapper != null)
            {
                this.setRefActMenuItem.Text += " (" + CommonData.refActWrapper.Activity.StartTime.ToLocalTime().ToString() + ")";
            }
             showChartToolsMenuItem.Checked = Settings.ShowChartBar;
        }

        private void ShowMeanMenuItem_Click(object sender, EventArgs e)
        {
            Settings.ShowCategoryAverage = !Settings.ShowCategoryAverage;
            updateChart();
        }

        private void rollingAverageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Settings.ShowMovingAverage = !Settings.ShowMovingAverage;
            updateChart();
        }

        private void setRefActMenuItem_Click(object sender, EventArgs e)
        {
            if (treeListAct.SelectedItems != null && treeListAct.SelectedItems.Count == 1)
            {
                CommonData.refActWrapper = getListSelection(treeListAct.SelectedItems)[0];
            }
            treeListAct.Refresh();
            updateChart();
        }

        void limitActivityMenuItem_Click(object sender, System.EventArgs e)
        {
#if !ST_2_1
            if (m_view != null && treeListAct.SelectedItems != null && treeListAct.SelectedItems.Count > 0)
            {
                IList<ActivityWrapper> atr = getListSelection(treeListAct.SelectedItems);
                IList<IActivity> aAct = new List<IActivity>();
                foreach (ActivityWrapper tr in atr)
                {
                    aAct.Add(tr.Activity);
                }
                m_view.SelectionProvider.SelectedItems = (List<IActivity>)aAct;
            }
#endif
        }
        void selectWithURMenuItem_Click(object sender, System.EventArgs e)
        {
            if (CommonData.refActWrapper != null)
            {
                IList<IActivity> similarActivities = UniqueRoutes.GetUniqueRoutesForActivity(CommonData.refActWrapper.Activity, null);
                if (similarActivities != null)
                {
                    IList<IActivity> allActivities = new List<IActivity>();
                    foreach (IActivity act in m_activities)
                    {
                        allActivities.Add(act);
                    }
                    foreach (IActivity act in similarActivities)
                    {
                        if (!allActivities.Contains(act))
                        {
                            allActivities.Add(act);
                        }
                    }
                    this.Activities = allActivities;
                }
            }
        }
        void setOffsetWithURMenuItem_Click(object sender, System.EventArgs e)
        {
            if (CommonData.refActWrapper != null)
            {
                IList<IActivity> activities = new List<IActivity>();
                foreach (ActivityWrapper aw in getListSelection(treeListAct.SelectedItems))
                {
                    activities.Add(aw.Activity);
                }
                IDictionary<IActivity, IItemTrackSelectionInfo[]> commonStretches = UniqueRoutes.GetCommonStretchesForActivity(CommonData.refActWrapper.Activity, activities, null);
                foreach (ActivityWrapper aw in getListSelection(treeListAct.SelectedItems))
                {
                    if (commonStretches.Count > 0 &&
                        commonStretches.ContainsKey(aw.Activity) &&
                        commonStretches[aw.Activity] != null &&
                        commonStretches[aw.Activity][0].MarkedDistances.Count > 0 &&
                        commonStretches[aw.Activity][0].MarkedTimes.Count > 0)
                    {
                        //UR sets both MarkedDistances and MarkedTimes
                        //set both dist/time offset, regardless Settings.UseTimeXAxis
                        aw.TimeOffset = commonStretches[aw.Activity][1].MarkedTimes[0].Lower.Subtract(CommonData.refActWrapper.Activity.StartTime).Subtract(
                            commonStretches[aw.Activity][0].MarkedTimes[0].Lower.Subtract(aw.Activity.StartTime));
                        aw.DistanceOffset = commonStretches[aw.Activity][1].MarkedDistances[0].Lower - 
                            commonStretches[aw.Activity][0].MarkedDistances[0].Lower;
                        TrailResult tr = new TrailResult(m_actWrappers[0]);
                    }
                }
                treeListAct.Refresh();
                updateChart();
            }
        }

        private void setRollAvgWidthMenuItem_Click(object sender, EventArgs e)
        {
            bool valueOk = false;
            String textBoxInit;
            while (!valueOk)
            {
                String labelText = null;
                if (Settings.UseTimeXAxis)
                {
                    labelText = Resources.SetMovingAveragePeriod + ":";
                    textBoxInit = UnitUtil.Time.ToString(Settings.MovingAverageTime, "u");
                }
                else
                {
                    labelText = Resources.SetMovingAveragePeriod + ":";
                    textBoxInit = UnitUtil.Distance.ToString(Settings.MovingAverageLength, "u");
                }
                InputDialog dialog = new InputDialog("Set moving average width", labelText, textBoxInit);
                dialog.ThemeChanged(m_visualTheme);
                dialog.UICultureChanged(m_culture);
                dialog.ShowDialog();
                if (dialog.ReturnOk)
                {
                    try
                    {
                        if (Settings.UseTimeXAxis)
                        {
                            double value = UnitUtil.Time.Parse(dialog.TextResult);
                            if (value < 0) { throw new Exception(); }
                            Settings.MovingAverageTime = value;
                        }
                        else
                        {
                            double value = UnitUtil.Distance.Parse(dialog.TextResult);
                            if (double.IsNaN(value) || value < 0) { throw new Exception(); }
                            Settings.MovingAverageLength = value;
                        }
                        valueOk = true;
                        updateChart();
                    }
                    catch (Exception)
                    {
                        //Generic error message
                        new WarningDialog(Resources.NonNegativeNumber);
                    }
                }
                else
                {
                    valueOk = true; // cancelled
                }
            }
        }

        private void offsetMenuItem_Click(object sender, EventArgs e)
        {
            bool valueOk = false;
            String textBoxInit;
            while (!valueOk && treeListAct.SelectedItems.Count > 0)
            {
                String labelText = null;
                ActivityWrapper wrapper = getListSelection(treeListAct.SelectedItems)[0];
                if (Settings.UseTimeXAxis)
                {
                    labelText = Resources.SetOffset + " " + CommonResources.Text.LabelTime.ToLower() + ":";
                    textBoxInit = UnitUtil.Time.ToString(wrapper.TimeOffset, "u");
                }
                else
                {
                    labelText = Resources.SetOffset + " " + CommonResources.Text.LabelDistance.ToLower() + ":";
                    textBoxInit = UnitUtil.Distance.ToString(wrapper.DistanceOffset, "u");
                }
                InputDialog dialog = new InputDialog(Resources.SetOffset, labelText, textBoxInit);
                dialog.ThemeChanged(m_visualTheme);
                dialog.UICultureChanged(m_culture);
                dialog.ShowDialog();
                if (dialog.ReturnOk)
                {
                    try
                    {
                        double value;
                        if (Settings.UseTimeXAxis)
                        {
                            value = UnitUtil.Time.Parse(dialog.TextResult);
                            if (value < 0) { throw new Exception(); }
                        }
                        else
                        {
                            value = UnitUtil.Distance.Parse(dialog.TextResult);
                            if (double.IsNaN(value) || value < 0) { throw new Exception(); }
                        }
                        valueOk = true;
                        foreach (ActivityWrapper w in getListSelection(treeListAct.SelectedItems))
                        {
                            // This parsing is done just to verify that they can be done and that the text in the box is valid
                            if (Settings.UseTimeXAxis)
                            {
                                w.TimeOffset = new TimeSpan(0, 0, (int)value);
                            }
                            else
                            {
                                w.DistanceOffset = value;
                            }
                        }
                        treeListAct.Refresh();
                        updateChart();
                    }
                    catch (Exception)
                    {
                        //Generic error message
                        new WarningDialog(Resources.NonNegativeNumber);
                    }
                }
                else
                {
                    valueOk = true; // cancelled
                }
            }
        }

        private void showChartToolsMenuItem_Click(object sender, EventArgs e)
        {
            Settings.ShowChartBar = !Settings.ShowChartBar;
            UpdateChartBar();
        }
        private void showToolBarMenuItem_Click(object sender, EventArgs e)
        {
            //PluginMain.Settings.ShowChartToolBar = !PluginMain.Settings.ShowChartToolBar;
            //ShowChartToolBar = PluginMain.Settings.ShowChartToolBar;
        }

        private void bannerXAxisMenuStrip_Opening(object sender, CancelEventArgs e)
        {
            xAxisTimeMenuItem.Checked = Settings.UseTimeXAxis;
            xAxisDistanceMenuItem.Checked = !Settings.UseTimeXAxis;
        }

        private void commonXAxisMenuItem_click(object sender, EventArgs e)
        {
            // Change the corresponding radio button. Its event handler will change the settings.
            if (sender == xAxisTimeMenuItem)
            {
                useTime.Checked = true;
            }
            else if (sender == xAxisDistanceMenuItem)
            {
                useDistance.Checked = true;
            }
        }
        
        private void bannerShowContextMenuStrip_Opening(object sender, CancelEventArgs e)
        {
            showHRMenuItem.Checked = Settings.ShowHeartRate;
            showPaceMenuItem.Checked = Settings.ShowPace;
            showSpeedMenuItem.Checked = Settings.ShowSpeed;
            showPowerMenuItem.Checked = Settings.ShowPower;
            showCadenceMenuItem.Checked = Settings.ShowCadence;
            showElevationMenuItem.Checked = Settings.ShowElevation;
            showTimeMenuItem.Checked = Settings.ShowTime;
            showDistanceMenuItem.Checked = Settings.ShowDistance;
        }

        private void showCommonYAxisMenuItem_click(object sender, EventArgs e)
        {
            // Change the corresponding check box. Its event handler will change the settings.
            if (sender == showHRMenuItem)
            {
                heartRate.Checked = !heartRate.Checked;
            }
            else if (sender == showPaceMenuItem)
            {
                pace.Checked = !pace.Checked;
            }
            else if (sender == showSpeedMenuItem)
            {
                speed.Checked = !speed.Checked;
            }
            else if (sender == showPowerMenuItem)
            {
                power.Checked = !power.Checked;
            }
            else if (sender == showCadenceMenuItem)
            {
                cadence.Checked = !cadence.Checked;
            }
            else if (sender == showElevationMenuItem)
            {
                elevation.Checked = !elevation.Checked;
            }
            else if (sender == showTimeMenuItem)
            {
                time.Checked = !time.Checked;
            }
            else if (sender == showDistanceMenuItem)
            {
                distance.Checked = !distance.Checked;
            }
        }

        private void bannerShowDiffContextMenuStrip_Opening(object sender, CancelEventArgs e)
        {
            showTimeDiffMenuItem.Checked = Settings.ShowDiffTime;
            showDistDiffMenuItem.Checked = Settings.ShowDiffDistance;
            showHRDiffMenuItem.Checked = Settings.ShowDiffHeartRate;
        }

        private void showDiffYAxisMenuItem_click(object sender, EventArgs e)
        {
            if (sender == showTimeDiffMenuItem)
                Settings.ShowDiffTime = !Settings.ShowDiffTime;
            else if (sender == showDistDiffMenuItem)
                Settings.ShowDiffDistance = !Settings.ShowDiffDistance;
            else if (sender == showHRDiffMenuItem)
                Settings.ShowDiffHeartRate = !Settings.ShowDiffHeartRate;
            updateChart();
        }


        private void treeListContextMenuStrip_Opening(object sender, CancelEventArgs e)
        {
            setRefTreeListMenuItem.Enabled = (treeListAct.SelectedItems.Count == 1);
            this.setRefTreeListMenuItem.Text = StringResources.SetRefActivity;
            if (CommonData.refActWrapper != null)
            {
                this.setRefTreeListMenuItem.Text += " (" + CommonData.refActWrapper.Activity.StartTime.ToLocalTime().ToString() + ")";
            }
        }

        private void tableSettingsMenuItem_Click(object sender, EventArgs e)
        {
#if !ST_2_1
#if ST_2_1
            ListSettings dialog = new ListSettings();
			dialog.ColumnsAvailable = OverlayColumnIds.ColumnDefs();
#else
            ListSettingsDialog dialog = new ListSettingsDialog();
            dialog.AvailableColumns = OverlayColumnIds.ColumnDefs(); // TrailResultColumnIds.ColumnDefs(m_controller.CurrentActivity);
#endif
            dialog.ThemeChanged(m_visualTheme);
            dialog.AllowFixedColumnSelect = false;
            dialog.SelectedColumns = Settings.TreeListActColumns; // PluginMain.Settings.ActivityPageColumns;
            dialog.NumFixedColumns = 3; // PluginMain.Settings.ActivityPageNumFixedColumns;

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                int numFixedColumns = dialog.NumFixedColumns;
                Settings.TreeListActColumns = dialog.SelectedColumns;
                RefreshColumns();
            }

#endif
        }

        private void allVisibleMenuItem_Click(object sender, EventArgs e)
        {
            foreach (ActivityWrapper wrapper in m_actWrappers)
            {
                treeListAct.SetChecked(wrapper, true);
            }
            treeView_CheckedChanged(sender, e);

        }

        private void noneVisibleMenuItem_Click(object sender, EventArgs e)
        {
            foreach (ActivityWrapper wrapper in m_actWrappers)
            {
                treeListAct.SetChecked(wrapper, false);
            }
            treeView_CheckedChanged(sender, e);
        }

        void summaryList_Click(object sender, System.EventArgs e)
        {
#if !ST_2_1
            //From Trails plugin
            if (sender is TreeList)
            {
                TreeList l = sender as TreeList;
                object row;
                TreeList.RowHitState hit;
                row = treeListAct.RowHitTest(((MouseEventArgs)e).Location, out hit);
                if (row != null && hit == TreeList.RowHitState.Row)
                {
                    ActivityWrapper utr = (ActivityWrapper)(row);
                    bool colorSelected = false;
                    if (hit != TreeList.RowHitState.PlusMinus)
                    {
                        int nStart = ((MouseEventArgs)e).X;
                        int spos = l.Location.X;// +l.Parent.Location.X;
                        for (int i = 0; i < l.Columns.Count; i++)
                        {
                            int epos = spos + l.Columns[i].Width;
                            if (nStart > spos && nStart < epos)
                            {
                                if (l.Columns[i].Id == OverlayColumnIds.Colour)
                                {
                                    colorSelected = true;
                                    break;
                                }
                            }

                            spos = epos;
                        }
                    }
                    if (colorSelected)
                    {
                        ColorSelectorPopup cs = new ColorSelectorPopup();
                        cs.Width = 70;
                        cs.ThemeChanged(m_visualTheme);
                        cs.DesktopLocation = ((Control)sender).PointToScreen(((MouseEventArgs)e).Location);
                        cs.Selected = utr.ActColor;
                        m_ColorSelectorResult = utr;
                        cs.ItemSelected += new ColorSelectorPopup.ItemSelectedEventHandler(cs_ItemSelected);
                        cs.Show();
                    }
                    else
                    {
                        bool isMatch = false;
                        foreach (ActivityWrapper t in getListSelection(this.treeListAct.CheckedElements))
                        {
                            if (t == utr)
                            {
                                isMatch = true;
                                break;
                            }
                        }
                        IList<TrailResult> aTr = new List<TrailResult>();
                        if (isMatch)
                        {
                            TrailResult tr = new TrailResult(utr);
                            aTr.Add(tr);
                        }
                        this.MarkTrack(TrailResultMarked.TrailResultMarkAll(aTr));
                    }
                }
            }
#endif
        }

        private ActivityWrapper m_ColorSelectorResult = null;
        void cs_ItemSelected(object sender, ColorSelectorPopup.ItemSelectedEventArgs e)
        {
            if (sender is ColorSelectorPopup && m_ColorSelectorResult != null)
            {
                ColorSelectorPopup cs = sender as ColorSelectorPopup;
                if (cs.Selected != m_ColorSelectorResult.ActColor)
                {
                    m_ColorSelectorResult.ActColor = cs.Selected;
                    this.RefreshPage();
                }
            }
            m_ColorSelectorResult = null;
        }
#if !ST_2_1
        void mapPoly_Click(object sender, MouseEventArgs e)
        {
            if (sender is TrailMapPolyline)
            {
                IList<TrailResult> result = new List<TrailResult> { (sender as TrailMapPolyline).TrailRes };
                this.EnsureVisible(result, true);
            }
        }
#endif
        private ITheme m_visualTheme =
#if ST_2_1
                Plugin.GetApplication().VisualTheme;
#else
 Plugin.GetApplication().SystemPreferences.VisualTheme;
#endif
        private CultureInfo m_culture =
#if ST_2_1
                new System.Globalization.CultureInfo("en");
#else
 Plugin.GetApplication().SystemPreferences.UICulture;
#endif

        private bool m_showPage = false;
        private ChartDataSeries m_lastSelectedSeries = null;

        private List<IActivity> m_activities = new List<IActivity>();
        private List<ActivityWrapper> m_actWrappers = new List<ActivityWrapper>();
        private IDictionary<ChartDataSeries, IActivity> m_series2activity = new Dictionary<ChartDataSeries, IActivity>();

        //bSelectingDataFlag and bSelectDataFlag are used to coordinate the chart 
        //click/select/selecting events to minimize 'movingAverage' and 'box' control flicker.
        //I'm sure there's a better way, but at this time this is all I've got.
        private bool m_bSelectingDataFlag = false;
        private bool m_bSelectDataFlag = false;

        private string saveImageProperties_fileName = "";

#if !ST_2_1
        private IDetailPage m_DetailPage = null;
        private IDailyActivityView m_view = null;
        private TrailPointsLayer m_layer = null;
        private bool m_boDetailPageExpanded = false;
        private bool m_firstZoom = true; //Force zoom first update - the page is not visible when activities are updated
#endif

        private void expandButton_Click(object sender, EventArgs e)
        {
#if !ST_2_1
            if (m_DetailPage != null)
            {
                m_boDetailPageExpanded = !m_boDetailPageExpanded;
                m_DetailPage.PageMaximized = m_boDetailPageExpanded;
            }
            if (m_boDetailPageExpanded)
            {
                this.expandButton.BackgroundImage = CommonResources.Images.View3PaneLowerLeft16;
            }
            else
            {
                this.expandButton.BackgroundImage = CommonResources.Images.View2PaneLowerHalf16;
            }
#endif
        }
    }

    static class CommonData
    {
        public static ActivityWrapper refActWrapper = null;
    }
}
