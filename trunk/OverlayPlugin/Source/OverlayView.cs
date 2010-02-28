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
using ZoneFiveSoftware.Common.Visuals;
using ZoneFiveSoftware.Common.Visuals.Chart;
using ZoneFiveSoftware.Common.Data;
using ZoneFiveSoftware.Common.Data.Fitness;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Collections;
using System.Reflection;
using ZoneFiveSoftware.Common.Data.Measurement;
using ZoneFiveSoftware.Common.Visuals.Fitness;
using ZoneFiveSoftware.Common.Data.Algorithm;
using SportTracksOverlayPlugin.Properties;
using SportTracksOverlayPlugin.Util;

namespace SportTracksOverlayPlugin.Source
{
    class OverlayView : UserControl
    {
        private LineChart chart;
        private Label labelXaxis;
        private Label labelYaxis;

        private RadioButton useTime;
        private RadioButton useDistance;
        private CheckBox heartRate;
        private CheckBox pace;
        private CheckBox speed;
        private CheckBox power;
        private CheckBox cadence;
        private CheckBox elevation;

        private CheckBox categoryAverage;
        private CheckBox movingAverage;
        private Label movingAverageLabel;
        private System.Windows.Forms.TextBox maBox;
        private ToolTip toolTipMAbox;
        private System.ComponentModel.IContainer components;
        private CheckBox lastChecked;

        private List<IActivity> activities;
        private System.Windows.Forms.Panel panelAct;
        private Label labelActivity;
        private IDictionary<ChartDataSeries, IActivity> series2activity;

        private IDictionary<IActivity, IList<double>> actOffsets;
        private IDictionary<System.Windows.Forms.TextBox, IActivity> actBoxes;
        private IList<System.Windows.Forms.TextBox> actTextBoxes;
        private IDictionary<ChartDataSeries, System.Windows.Forms.TextBox> series2actBoxes;

        private IList<bool> checks;
        private IDictionary<CheckBox, int> boxes;
        private IList<CheckBox> checkBoxes;
        private IDictionary<ChartDataSeries, CheckBox> series2boxes;

        public IList<IActivity> Activities
        {
            set
            {
                if (activities != null)
                {
                    foreach (IActivity activity in activities)
                    {
                        activity.DataChanged -= new NotifyDataChangedEventHandler(activity_DataChanged);
                    }
                }
                activities = new List<IActivity>();
                foreach (IActivity activity in value)
                {
                    activities.Add(activity);
                    activity.DataChanged += new NotifyDataChangedEventHandler(activity_DataChanged);
                }
                //Temporary
                if (Plugin.Verbose > 100 && Settings.uniqueRoutes != null)
                {
                    MethodInfo methodInfo = Settings.uniqueRoutes.GetMethod("findSimilarStretch");
                    IDictionary<IActivity, IList<IList<int>>> result = 
                        (IDictionary<IActivity, IList<IList<int>>>)methodInfo.Invoke(this, new object[] { activities[0], activities });

                }
                activities.Sort(new ActivityDateComparer());

                nextIndex = 0;
                int x = 0;
                int y = 0;
                int index = 0;

                actOffsets = new Dictionary<IActivity, IList<double>>();
                actBoxes = new Dictionary<System.Windows.Forms.TextBox, IActivity>();
                actTextBoxes = new List<System.Windows.Forms.TextBox>();
                checks = new List<bool>();
                boxes = new Dictionary<CheckBox, int>();
                checkBoxes = new List<CheckBox>();

                foreach (IActivity activity in activities)
                {
                    x = 0;
                    IList<double> s = new List<double>();
                    s.Add(0);
                    s.Add(0);
                    actOffsets.Add(activity, s);//Get from UR integration

                    System.Windows.Forms.TextBox offBox = new System.Windows.Forms.TextBox();
                    actTextBoxes.Add(offBox);
                    offBox.Size = new Size(30, 20);
                    offBox.LostFocus += new EventHandler(offBox_LostFocus);
                    offBox.Location = new Point(x, y);
                    panelAct.Controls.Add(offBox);
                    offBox.Name = "offsetBox";
                    actBoxes.Add(offBox, activity);
                    if (Plugin.Verbose > 0)
                    {
                        offBox.Visible = true;
                        x += 35;
                    }
                    else
                    {
                        offBox.Visible = false;
                    }

                    CheckBox box = new CheckBox();
                    checkBoxes.Add(box);
                    box.Checked = true;
                    box.Text = activity.StartTime.ToLocalTime().ToString();
                    box.Size = new Size(155, box.Height);
                    box.ForeColor = newColor();
                    //box.CheckAlign = ContentAlignment.MiddleLeft;
                    box.CheckedChanged += new EventHandler(box_CheckedChanged);
                    checks.Add(true);
                    panelAct.Controls.Add(box);
                    box.Location = new Point(x, y);
                    boxes.Add(box, index);

                    index++;
                    y += 25;
                }
                updateChart();
            }
            get
            {
                return activities;
            }
        }

        private class ActivityDateComparer : Comparer<IActivity>
        {
            public override int Compare(IActivity x, IActivity y)
            {
                return x.StartTime.CompareTo(y.StartTime);
            }
        }

        private Form form;

        private bool dontUpdate;

        public OverlayView(IList<IActivity> activities)
            : this(activities, true) { }

        public OverlayView(IList<IActivity> activities, bool showDialog)
        {
            InitializeComponent();

            labelActivity.Text = StringResources.Activities;
            labelXaxis.Text = StringResources.XAxis + ":";
            labelYaxis.Text = StringResources.YAxis + ":";
            int max = Math.Max(labelXaxis.Location.X + labelXaxis.Size.Width,
                                labelYaxis.Location.X + labelYaxis.Size.Width) + 5;
            useTime.Location = new Point(max, labelXaxis.Location.Y);
            correctUI(new Control[] { useTime, useDistance });
            heartRate.Location = new Point(max, labelYaxis.Location.Y);
            correctUI(new Control[] { heartRate, pace, speed, power, cadence, elevation });
            chart.Location = new Point(Math.Max(Math.Max(categoryAverage.Location.X + categoryAverage.Size.Width,
                                                         movingAverage.Location.X + movingAverage.Size.Width),
                                                panelAct.Location.X + panelAct.Size.Width), chart.Location.Y);
            dontUpdate = true;
            series2activity = new Dictionary<ChartDataSeries, IActivity>();
            series2actBoxes = new Dictionary<ChartDataSeries, System.Windows.Forms.TextBox>();
            series2boxes = new Dictionary<ChartDataSeries, CheckBox>();
            SizeChanged += new EventHandler(OverlayView_SizeChanged);
            Activities = activities;

            useTime.Text = CommonResources.Text.LabelTime;
            useDistance.Text = CommonResources.Text.LabelDistance;
            heartRate.Text = CommonResources.Text.LabelHeartRate;
            pace.Text = CommonResources.Text.LabelPace;
            speed.Text = CommonResources.Text.LabelSpeed;
            power.Text = CommonResources.Text.LabelPower;
            cadence.Text = CommonResources.Text.LabelCadence;
            elevation.Text = CommonResources.Text.LabelElevation;
            categoryAverage.Text = Resources.BCA;
            movingAverage.Text = Resources.BMA;

            heartRate.Checked = Settings.ShowHeartRate;
            pace.Checked = Settings.ShowPace;
            speed.Checked = Settings.ShowSpeed;
            power.Checked = Settings.ShowPower;
            cadence.Checked = Settings.ShowCadence;
            elevation.Checked = Settings.ShowElevation;

            categoryAverage.Checked = Settings.ShowCategoryAverage;
            movingAverage.Checked = Settings.ShowMovingAverage;
            toolTipMAbox.SetToolTip(maBox, Resources.MAToolTip);
            maBox.LostFocus += new EventHandler(maBox_LostFocus);
            if (Settings.ShowTime)
            {
                maBox.Text = UnitUtil.Time.ToString(Settings.MovingAverageTime, "mm:ss");
            }
            else
            {
                maBox.Text = UnitUtil.Elevation.ToString(Settings.MovingAverageLength);
            }
            updateMovingAverage();
            if (Settings.ShowTime)
            {
                useTime.Checked = true;
                useDistance.Checked = false;
            }
            else
            {
                useDistance.Checked = true;
                useTime.Checked = false;
            }
            chart.SelectData += new ChartBase.SelectDataHandler(chart_SelectData);
            chart.Click += new EventHandler(chart_Click);
            dontUpdate = false;
            updateLabels();
            updateChart();
            if (showDialog)
            {
                form = new Form();
                form.Controls.Add(this);
                form.Size = Settings.WindowSize;
                form.SizeChanged += new EventHandler(form_SizeChanged);
                setSize();
                if (activities.Count == 1)
                    form.Text = Resources.O1;
                else
                    form.Text = String.Format(Resources.O2, activities.Count);
                form.Icon = Icon.FromHandle(Properties.Resources.Image_32_Overlay.GetHicon());
                Parent.SizeChanged += new EventHandler(Parent_SizeChanged);
                form.StartPosition = FormStartPosition.CenterScreen;
                form.ShowDialog();
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
            if (Settings.ShowCategoryAverage && activities.Count > 1)
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
            if (Settings.ShowTime)
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
            series2boxes.Add(average, movingAverage);
            series2activity.Add(average, series2activity[series]);
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
            int index = 0;
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
                if (seen > 1)
                {
                    average.Points.Add(index++, new PointF(x, y / seen));
                }
            }
            series2boxes.Add(average, categoryAverage);
            return average;
        }

        private void updateOffBoxLabel(System.Windows.Forms.TextBox box)
        {
           if (Settings.ShowTime)
            { 
               box.Text = UnitUtil.Time.ToString(actOffsets[actBoxes[box]][0], "mm:ss");
            }
            else
            {
                box.Text = UnitUtil.Distance.ToString(actOffsets[actBoxes[box]][1], "F3"); 
            }
        }
        private void updateLabels()
        {
            foreach (System.Windows.Forms.TextBox box in actTextBoxes)
            {
                updateOffBoxLabel(box);
            }
         }
        private void updateChart()
        {
            if (dontUpdate) return;
            chart.DataSeries.Clear();
            chart.YAxisRight.Clear();
            series2activity.Clear();
            series2actBoxes.Clear();
            series2boxes.Clear();
            bool useRight = false;
            if (Settings.ShowTime)
            {
                chart.XAxis.Formatter = new Formatter.SecondsToTime();
                chart.XAxis.Label = UnitUtil.Time.LabelAxis;
            }
            else
            {
                chart.XAxis.Formatter = new Formatter.General();
                chart.XAxis.Label = UnitUtil.Distance.LabelAxis; ;
            }
            if (Settings.ShowHeartRate)
            {
                nextIndex = 0;
                useRight = true;
                chart.YAxis.Formatter = new Formatter.General();
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
                axis.Formatter = new Formatter.General();
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
                axis.Formatter = new Formatter.General();
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
                axis.Formatter = new Formatter.General();
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
                axis.Formatter = new Formatter.General();
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
            chart.AutozoomToData(true);
            chart.Refresh();
        }

        private IList<ChartDataSeries> buildSeries(
            Interpolator interpolator, CanInterpolater canInterpolator, IAxis axis,
            GetDataSeries getDataSeriess)
        {
            IList<ChartDataSeries> list = new List<ChartDataSeries>();
            int index = 0;            
            foreach (IActivity activity in activities)
            {
                if (checks[index])
                {
                    double offset;
                    if (Settings.ShowTime)
                    {
                        offset = actOffsets[activity][0];
                    }
                    else
                    {
                        offset = actOffsets[activity][1];
                    }
                    ChartDataSeries series = getDataSeries(
                        interpolator, 
                        canInterpolator, 
                        ActivityInfoCache.Instance.GetInfo(activity),
                        axis,
                        getDataSeriess,
                        offset);
                    series2actBoxes.Add(series, actTextBoxes[index]);
                    series2boxes.Add(series, checkBoxes[index]);
                    series2activity.Add(series, activity);
                    list.Add(series);
                }
                else
                {
                    newColor();
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
                newColor();
                return series;
            }
            IValueRangeSeries<DateTime> pauses = info.NonMovingTimes;
            bool first = true;
            float priorElapsed = float.NaN;
            INumericTimeDataSeries data = getDataSeries(info);
            foreach (ITimeValueEntry<float> entry in data)
            {
                DateTime entryTime = data.EntryDateTime(entry);
                float elapsed =
                    (float)DateTimeRangeSeries.TimeNotPaused(info.Activity.StartTime, entryTime,
                                                            pauses).TotalSeconds;
                if (elapsed != priorElapsed)
                {
                    float x = float.NaN;
                    if (Settings.ShowTime)
                    {
                        x = (float)(elapsed+offset);
                    }
                    else
                    {
                        ITimeValueEntry<float> entryMoving = info.MovingDistanceMetersTrack.GetInterpolatedValue(info.Activity.StartTime.AddSeconds(elapsed));
                        if (entryMoving != null && (first || (!first && entryMoving.Value > 0)))
                        {
                            x = (float)UnitUtil.Distance.ConvertFrom(entryMoving.Value + offset);
                        }
                    }
                    float y = (float)interpolator(entry.Value);
                    if (!x.Equals(float.NaN))
                    {
                        series.Points.Add(series.Points.Count,
                            new PointF(x, y));
                    }
                }
                priorElapsed = elapsed;
                first = false;
            }
            series.LineColor = newColor();
            return series;
        }

        private void setSize()
        {
            if (Parent != null)
            {
                if (form != null)
                {
                    Size = new Size(form.Size.Width - 10,
                                    form.Size.Height - 10);
                    Settings.WindowSize =
                        new Size(
                        Parent.Size.Width,
                        Parent.Size.Height);
                }
                else
                {
                    Size = new Size(Parent.Size.Width,
                                   Parent.Size.Height);
                }
            }
            chart.Size = new Size(
                Size.Width - chart.Location.X,
                Size.Height - chart.Location.Y -(form == null ? 0 : 30));
            panelAct.Size = new Size(panelAct.Width, chart.Height);
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.chart = new ZoneFiveSoftware.Common.Visuals.Chart.LineChart();
            this.labelActivity = new System.Windows.Forms.Label();
            this.heartRate = new System.Windows.Forms.CheckBox();
            this.pace = new System.Windows.Forms.CheckBox();
            this.speed = new System.Windows.Forms.CheckBox();
            this.useTime = new System.Windows.Forms.RadioButton();
            this.labelXaxis = new System.Windows.Forms.Label();
            this.useDistance = new System.Windows.Forms.RadioButton();
            this.labelYaxis = new System.Windows.Forms.Label();
            this.panelAct = new System.Windows.Forms.Panel();
            this.power = new System.Windows.Forms.CheckBox();
            this.cadence = new System.Windows.Forms.CheckBox();
            this.elevation = new System.Windows.Forms.CheckBox();
            this.categoryAverage = new System.Windows.Forms.CheckBox();
            this.movingAverage = new System.Windows.Forms.CheckBox();
            this.movingAverageLabel = new System.Windows.Forms.Label();
            this.maBox = new System.Windows.Forms.TextBox();
            this.toolTipMAbox = new System.Windows.Forms.ToolTip(this.components);
            this.SuspendLayout();
            // 
            // chart
            // 
            this.chart.BackColor = System.Drawing.Color.White;
            this.chart.Border = ZoneFiveSoftware.Common.Visuals.ControlBorder.Style.SmallRoundShadow;
            this.chart.Location = new System.Drawing.Point(142, 45);
            this.chart.Name = "chart";
            this.chart.Size = new System.Drawing.Size(308, 235);
            this.chart.TabIndex = 1;
            // 
            // labelActivity
            // 
            this.labelActivity.AutoSize = true;
            this.labelActivity.Location = new System.Drawing.Point(0, 114);
            this.labelActivity.Name = "labelActivity";
            this.labelActivity.Size = new System.Drawing.Size(49, 13);
            this.labelActivity.TabIndex = 3;
            this.labelActivity.Text = "Activities";
            // 
            // heartRate
            // 
            this.heartRate.AutoSize = true;
            this.heartRate.Location = new System.Drawing.Point(46, 21);
            this.heartRate.Name = "heartRate";
            this.heartRate.Size = new System.Drawing.Size(73, 17);
            this.heartRate.TabIndex = 5;
            this.heartRate.Text = "Heart rate";
            this.heartRate.UseVisualStyleBackColor = true;
            this.heartRate.CheckedChanged += new System.EventHandler(this.heartRate_CheckedChanged);
            // 
            // pace
            // 
            this.pace.AutoSize = true;
            this.pace.Location = new System.Drawing.Point(125, 21);
            this.pace.Name = "pace";
            this.pace.Size = new System.Drawing.Size(51, 17);
            this.pace.TabIndex = 6;
            this.pace.Text = "Pace";
            this.pace.UseVisualStyleBackColor = true;
            this.pace.CheckedChanged += new System.EventHandler(this.pace_CheckedChanged);
            // 
            // speed
            // 
            this.speed.AutoSize = true;
            this.speed.Location = new System.Drawing.Point(182, 22);
            this.speed.Name = "speed";
            this.speed.Size = new System.Drawing.Size(57, 17);
            this.speed.TabIndex = 7;
            this.speed.Text = "Speed";
            this.speed.UseVisualStyleBackColor = true;
            this.speed.CheckedChanged += new System.EventHandler(this.speed_CheckedChanged);
            // 
            // useTime
            // 
            this.useTime.AutoSize = true;
            this.useTime.Location = new System.Drawing.Point(46, 3);
            this.useTime.Name = "useTime";
            this.useTime.Size = new System.Drawing.Size(48, 17);
            this.useTime.TabIndex = 8;
            this.useTime.Text = "Time";
            this.useTime.UseVisualStyleBackColor = true;
            this.useTime.CheckedChanged += new System.EventHandler(this.useTime_CheckedChanged);
            // 
            // labelXaxis
            // 
            this.labelXaxis.AutoSize = true;
            this.labelXaxis.Location = new System.Drawing.Point(2, 3);
            this.labelXaxis.Name = "labelXaxis";
            this.labelXaxis.Size = new System.Drawing.Size(38, 13);
            this.labelXaxis.TabIndex = 9;
            this.labelXaxis.Text = "X axis:";
            // 
            // useDistance
            // 
            this.useDistance.AutoSize = true;
            this.useDistance.Location = new System.Drawing.Point(100, 3);
            this.useDistance.Name = "useDistance";
            this.useDistance.Size = new System.Drawing.Size(67, 17);
            this.useDistance.TabIndex = 10;
            this.useDistance.Text = "Distance";
            this.useDistance.UseVisualStyleBackColor = true;
            this.useDistance.CheckedChanged += new System.EventHandler(this.useDistance_CheckedChanged);
            // 
            // labelYaxis
            // 
            this.labelYaxis.AutoSize = true;
            this.labelYaxis.Location = new System.Drawing.Point(2, 22);
            this.labelYaxis.Name = "labelYaxis";
            this.labelYaxis.Size = new System.Drawing.Size(38, 13);
            this.labelYaxis.TabIndex = 11;
            this.labelYaxis.Text = "Y axis:";
            // 
            // panelAct
            // 
            this.panelAct.AutoScroll = true;
            this.panelAct.AutoSize = true;
            this.panelAct.Location = new System.Drawing.Point(3, 130);
            this.panelAct.Name = "panelAct";
            this.panelAct.Size = new System.Drawing.Size(138, 147);
            this.panelAct.TabIndex = 12;
            // 
            // power
            // 
            this.power.AutoSize = true;
            this.power.Location = new System.Drawing.Point(245, 22);
            this.power.Name = "power";
            this.power.Size = new System.Drawing.Size(56, 17);
            this.power.TabIndex = 13;
            this.power.Text = "Power";
            this.power.UseVisualStyleBackColor = true;
            this.power.CheckedChanged += new System.EventHandler(this.power_CheckedChanged);
            // 
            // cadence
            // 
            this.cadence.AutoSize = true;
            this.cadence.Location = new System.Drawing.Point(307, 22);
            this.cadence.Name = "cadence";
            this.cadence.Size = new System.Drawing.Size(69, 17);
            this.cadence.TabIndex = 14;
            this.cadence.Text = "Cadence";
            this.cadence.UseVisualStyleBackColor = true;
            this.cadence.CheckedChanged += new System.EventHandler(this.cadence_CheckedChanged);
            // 
            // elevation
            // 
            this.elevation.AutoSize = true;
            this.elevation.Location = new System.Drawing.Point(382, 22);
            this.elevation.Name = "elevation";
            this.elevation.Size = new System.Drawing.Size(70, 17);
            this.elevation.TabIndex = 15;
            this.elevation.Text = "Elevation";
            this.elevation.UseVisualStyleBackColor = true;
            this.elevation.CheckedChanged += new System.EventHandler(this.elevation_CheckedChanged);
            // 
            // categoryAverage
            // 
            this.categoryAverage.AutoSize = true;
            this.categoryAverage.Location = new System.Drawing.Point(3, 44);
            this.categoryAverage.Name = "categoryAverage";
            this.categoryAverage.Size = new System.Drawing.Size(139, 17);
            this.categoryAverage.TabIndex = 16;
            this.categoryAverage.Text = global::SportTracksOverlayPlugin.Properties.Resources.BCA;
            this.categoryAverage.UseVisualStyleBackColor = true;
            this.categoryAverage.CheckedChanged += new System.EventHandler(this.average_CheckedChanged);
            // 
            // movingAverage
            // 
            this.movingAverage.AutoSize = true;
            this.movingAverage.Location = new System.Drawing.Point(3, 67);
            this.movingAverage.Name = "movingAverage";
            this.movingAverage.Size = new System.Drawing.Size(126, 17);
            this.movingAverage.TabIndex = 18;
            this.movingAverage.Text = global::SportTracksOverlayPlugin.Properties.Resources.BMA;
            this.movingAverage.UseVisualStyleBackColor = true;
            this.movingAverage.CheckedChanged += new System.EventHandler(this.movingAverage_CheckedChanged);
            // 
            // movingAverageLabel
            // 
            this.movingAverageLabel.AutoSize = true;
            this.movingAverageLabel.Location = new System.Drawing.Point(70, 94);
            this.movingAverageLabel.Name = "movingAverageLabel";
            this.movingAverageLabel.Size = new System.Drawing.Size(0, 13);
            this.movingAverageLabel.TabIndex = 19;
            // 
            // maBox
            // 
            this.maBox.Location = new System.Drawing.Point(4, 91);
            this.maBox.Name = "maBox";
            this.maBox.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.maBox.Size = new System.Drawing.Size(60, 20);
            this.maBox.TabIndex = 20;
            // 
            // OverlayView
            // 
            this.Controls.Add(this.maBox);
            this.Controls.Add(this.movingAverageLabel);
            this.Controls.Add(this.movingAverage);
            this.Controls.Add(this.chart);
            this.Controls.Add(this.categoryAverage);
            this.Controls.Add(this.elevation);
            this.Controls.Add(this.cadence);
            this.Controls.Add(this.power);
            this.Controls.Add(this.panelAct);
            this.Controls.Add(this.labelYaxis);
            this.Controls.Add(this.useDistance);
            this.Controls.Add(this.labelXaxis);
            this.Controls.Add(this.useTime);
            this.Controls.Add(this.speed);
            this.Controls.Add(this.pace);
            this.Controls.Add(this.heartRate);
            this.Controls.Add(this.labelActivity);
            this.Name = "OverlayView";
            this.Size = new System.Drawing.Size(450, 282);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void form_SizeChanged(object sender, EventArgs e)
        {
            setSize();
        }

        private void OverlayView_SizeChanged(object sender, EventArgs e)
        {
            setSize();
        }

        private void activity_DataChanged(object sender, NotifyDataChangedEventArgs e)
        {
            updateChart();
        }

        private void offBox_LostFocus(object sender, EventArgs e)
        {
            System.Windows.Forms.TextBox box = (System.Windows.Forms.TextBox)sender;
            try
            {
                //Recalculate other offset here?
                if (Settings.ShowTime)
                {
                    actOffsets[actBoxes[box]][0] = UnitUtil.Time.Parse(box.Text);
                }
                else
                {
                    actOffsets[actBoxes[box]][1] = UnitUtil.Distance.Parse(box.Text);
                }
            }
            catch
            {
                //Other warning here?
                new WarningDialog(Resources.NonNegativeNumber);
            }
            updateOffBoxLabel(box);
        }

        private void box_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox box = (CheckBox)sender;
            checks[boxes[box]] = box.Checked;
            updateChart();
        }

        private void maBox_LostFocus(object sender, EventArgs e)
        {
            try
            {
                if (Settings.ShowTime)
                {
                    double value = UnitUtil.Time.Parse(maBox.Text);
                    if (value < 0) { throw new Exception(); }
                    Settings.MovingAverageTime = value;
                    maBox.Text = UnitUtil.Time.ToString(value);
                }
                else
                {
                    double value = UnitUtil.Elevation.Parse(maBox.Text);
                    if (value < 0) { throw new Exception(); }
                    Settings.MovingAverageLength = value;
                    maBox.Text = UnitUtil.Distance.ToString(value);
                }
                updateChart();
            }
            catch (Exception)
            {
                if (Settings.ShowTime)
                {
                    maBox.Text = UnitUtil.Time.ToString(Settings.MovingAverageTime);
                }
                else
                {
                    maBox.Text = UnitUtil.Distance.ToString(Settings.MovingAverageLength);
                }
                new WarningDialog(Resources.NonNegativeNumber);
            }
        }

        void chart_Click(object sender, EventArgs e)
        {
            if (lastChecked != null)
            {
                lastChecked.Font = new Font(lastChecked.Font, FontStyle.Regular);
                if (lastChecked == movingAverage)
                {
                    lastChecked.ForeColor = Color.Black;
                }
                lastChecked = null;
            }
        }

        void chart_SelectData(object sender, ChartBase.SelectDataEventArgs e)
        {
            if (e != null && e.DataSeries != null)
            {
                if (lastChecked != null)
                {
                    lastChecked.Font = new Font(lastChecked.Font, FontStyle.Regular);
                    if (lastChecked == movingAverage)
                    {
                        lastChecked.ForeColor = Color.Black;
                    }
                }
                if (series2boxes.ContainsKey(e.DataSeries))
                {
                    CheckBox box = series2boxes[e.DataSeries];
                    lastChecked = box;
                    if (box == movingAverage)
                    {
                        box.ForeColor = getColor(activities.IndexOf(series2activity[e.DataSeries]) % 10);
                    }
                    box.Font = new Font(box.Font, FontStyle.Bold);
                }
            }
        }

        private void Parent_SizeChanged(object sender, EventArgs e)
        {
            setSize();
        }

        private void useTime_CheckedChanged(object sender, EventArgs e)
        {
            if (!Settings.ShowTime)
            {
                Settings.ShowTime = true;
                updateLabels();
                updateChart();
                updateMovingAverage();
            }
        }

        private void useDistance_CheckedChanged(object sender, EventArgs e)
        {
            if (Settings.ShowTime)
            {
                Settings.ShowTime = false;
                updateLabels();
                updateChart();
                updateMovingAverage();
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

        private void average_CheckedChanged(object sender, EventArgs e)
        {
            Settings.ShowCategoryAverage = categoryAverage.Checked;
            updateChart();
        }

        private void updateMovingAverage()
        {
            Settings.ShowMovingAverage = movingAverage.Checked;
            if (Settings.ShowTime)
            {
                string sec = Time.Label(Time.TimeRange.Second);
                //ST will not give labels in 2.1
                if (sec == null || sec.Equals("")) { sec = "s"; }
                movingAverageLabel.Text = sec;
                maBox.Text = UnitUtil.Time.ToString(Settings.MovingAverageTime);
            }
            else
            {
                movingAverageLabel.Text = UnitUtil.Elevation.LabelAbbr;
                maBox.Text = UnitUtil.Elevation.ToString(Settings.MovingAverageLength);
            }
            maBox.Enabled = Settings.ShowMovingAverage;
        }

        private void movingAverage_CheckedChanged(object sender, EventArgs e)
        {
            updateMovingAverage();
            updateChart();
        }
    }
}
