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
#endif

using GpsRunningPlugin.Properties;
using GpsRunningPlugin.Util;

namespace GpsRunningPlugin.Source
{
    public partial class OverlayView : UserControl
    {
        //A wrapper for popupforms - could be called from IAction
        public OverlayView(IList<IActivity> activities, bool showDialog)
            : this()
        {
            if (showDialog)
            {
                //Theme and Culture must be set manually
                this.ThemeChanged(m_visualTheme);
                this.UICultureChanged(m_culture);
            }
            this.Activities = activities;
            if (showDialog)
            {
                _showPage = true;
            }
            RefreshPage();
            if (showDialog)
            {
                this.ShowDialog();
            }
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

            categoryAverage.Checked = Settings.ShowCategoryAverage;
            movingAverage.Checked = Settings.ShowMovingAverage;
            toolTipMAbox.SetToolTip(maBox, Resources.MAToolTip);
            maBox.LostFocus += new EventHandler(maBox_LostFocus);
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
            chart.SelectingData += new ChartBase.SelectDataHandler(chart_SelectingData);
            chart.Click += new EventHandler(chart_Click);
        }

        public void InitControls()
        {
            Font fCategory = categoryAverage.Font;
            Font fMoving = movingAverage.Font;
            categoryAverage.Font = new Font(categoryAverage.Font, FontStyle.Bold);
            movingAverage.Font = new Font(movingAverage.Font, FontStyle.Bold);

            //chart.Location = new Point(Math.Max(Math.Max(categoryAverage.Location.X + categoryAverage.Size.Width,
            //                                             movingAverage.Location.X + movingAverage.Size.Width),
            //                                    panelAct.Location.X + panelAct.Size.Width), chart.Location.Y);
            categoryAverage.Font = fCategory;
            movingAverage.Font = fMoving;

            series2boxes = new Dictionary<ChartDataSeries, CheckBox>();
            SizeChanged += new EventHandler(OverlayView_SizeChanged);
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
            if (activities.Count == 1)
                popupForm.Text = Resources.O1;
            else
                popupForm.Text = String.Format(Resources.O2, activities.Count);
            popupForm.Icon = Icon.FromHandle(Properties.Resources.Image_32_Overlay.GetHicon());
            Parent.SizeChanged += new EventHandler(Parent_SizeChanged);
            popupForm.StartPosition = FormStartPosition.CenterScreen;
            popupForm.ShowDialog();
        }

        public IList<IActivity> Activities
        {
            get
            {
                return activities;
            }
            set
            {
                if (activities != null)
                {
                    foreach (IActivity activity in activities)
                    {
#if ST_2_1
                        activity.DataChanged -= new NotifyDataChangedEventHandler(activity_DataChanged);
#else
                        activity.PropertyChanged -= new PropertyChangedEventHandler(Activity_PropertyChanged);
#endif
                    }
                }
                activities.Clear();
                foreach (IActivity activity in value)
                {
                    activities.Add(activity);
#if ST_2_1
                    activity.DataChanged += new NotifyDataChangedEventHandler(activity_DataChanged);
#else
                    activity.PropertyChanged += new PropertyChangedEventHandler(Activity_PropertyChanged);
#endif

                }
                RefreshPage();
            }
        }
        public void RefreshPage()
        {
            if (_showPage)
            {
                updateActivities();
                updateLabels();
                updateChart();
            }
        }
        private void updateActivities()
        {
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

            actOffsets.Clear();
            actBoxes.Clear();
            actTextBoxes.Clear();
            checks.Clear();
            boxes.Clear();
            checkBoxes.Clear();
            lastChecked.Clear();
            panelAct.Controls.Clear();

            foreach (IActivity activity in activities)
            {
                x = 0;
                IList<double> s = new List<double>();
                s.Add(0);
                s.Add(0);
                actOffsets.Add(activity, s);//TODO: Get from UR integration

                ZoneFiveSoftware.Common.Visuals.TextBox offBox = new ZoneFiveSoftware.Common.Visuals.TextBox();
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
                box.AutoSize = true;
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
        }

        private class ActivityDateComparer : Comparer<IActivity>
        {
            public override int Compare(IActivity x, IActivity y)
            {
                return x.StartTime.CompareTo(y.StartTime);
            }
        }

        public bool HidePage()
        {
            _showPage = false;
            return true;
        }
        public void ShowPage(string bookmark)
        {
            bool changed = !_showPage;
            _showPage = true;
            if (changed) { RefreshPage(); }
        }
        public void ThemeChanged(ITheme visualTheme)
        {
            m_visualTheme = visualTheme;

            this.chart.ThemeChanged(visualTheme);
            this.maBox.ThemeChanged(visualTheme);
            //Non ST controls
            this.panelAct.BackColor = visualTheme.Control;
            this.splitContainer1.Panel1.BackColor = visualTheme.Control;
            this.splitContainer1.Panel2.BackColor = visualTheme.Control;
            this.splitContainer2.Panel1.BackColor = visualTheme.Control;
            this.splitContainer2.Panel2.BackColor = visualTheme.Control;
            this.splitContainer3.Panel1.BackColor = visualTheme.Control;
            this.splitContainer3.Panel2.BackColor = visualTheme.Control;
        }

        public void UICultureChanged(CultureInfo culture)
        {
            m_culture = culture;
            labelActivity.Text = StringResources.Activities;
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
            categoryAverage.Text = Resources.BCA;
            movingAverage.Text = Resources.BMA;
            labelAOP.Text = Resources.AOP;

            int max = Math.Max(labelXaxis.Location.X + labelXaxis.Size.Width,
                                labelYaxis.Location.X + labelYaxis.Size.Width) + 5;
            useTime.Location = new Point(max, labelXaxis.Location.Y);
            correctUI(new Control[] { useTime, useDistance });
            heartRate.Location = new Point(max, labelYaxis.Location.Y);
            correctUI(new Control[] { heartRate, pace, speed, power, cadence, elevation });

            updateLabels();
            RefreshPage();
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
            series2boxes.Add(average, categoryAverage);
            return average;
        }

        private void updateOffBoxLabel(ZoneFiveSoftware.Common.Visuals.TextBox box)
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
            if (null != actTextBoxes)
            {
                foreach (ZoneFiveSoftware.Common.Visuals.TextBox box in actTextBoxes)
                {
                    if (null != box) { updateOffBoxLabel(box); }
                }
            }
        }
        private void updateChart()
        {
            //TODO: add show working
            chart.Visible = false;
            chart.UseWaitCursor = true;
            this.splitContainer2.Panel2.BackgroundImage = ZoneFiveSoftware.Common.Visuals.CommonResources.Images.Hourglass16;
            this.splitContainer2.Panel2.BackgroundImageLayout = ImageLayout.Center;
            chart.BeginUpdate();
            chart.AutozoomToData(false);
            ResetLastSelectedBoxFonts();
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
			//chart.AutozoomToData is the slowest part of this plugin
            chart.AutozoomToData(true);
            chart.Refresh();
            chart.EndUpdate();
            chart.UseWaitCursor = false;
            chart.Visible = true;
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
            bool first = true;
            float priorElapsed = float.NaN;
            //This should be retrieved per activity, if that is changed in ST
            bool includeStopped = false;
#if ST_2_1
            // If UseEnteredData is set, exclude Stopped
            if (info.Activity.UseEnteredData == false && info.Time.Equals(info.ActualTrackTime))
            {
                includeStopped = true;
            }
#else
            includeStopped = Plugin.GetApplication().SystemPreferences.AnalysisSettings.IncludeStopped;
#endif
            IValueRangeSeries<DateTime> pauses;
            if (includeStopped)
            {
                pauses = info.Activity.TimerPauses;
            }
            else
            {
                pauses = info.NonMovingTimes;
            }
            INumericTimeDataSeries data = getDataSeries(info);
            foreach (ITimeValueEntry<float> entry in data)
            {
                DateTime entryTime = data.EntryDateTime(entry);
                float elapsed =
                    (float)DateTimeRangeSeries.TimeNotPaused(info.Activity.StartTime, entryTime,
                                                            pauses).TotalSeconds;
				if ( elapsed != priorElapsed )
				{
					float x = float.NaN;
					if ( Settings.ShowTime )
					{
						x = (float)( elapsed + offset );
					}
					else
					{
                        ITimeValueEntry<float> entryMoving;
                        if (includeStopped)
                        {
                            entryMoving = info.ActualDistanceMetersTrack.GetInterpolatedValue(info.Activity.StartTime.AddSeconds(entry.ElapsedSeconds));
                        }
                        else
                        {
                            entryMoving = info.MovingDistanceMetersTrack.GetInterpolatedValue(info.Activity.StartTime.AddSeconds(entry.ElapsedSeconds));
                        }
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
            series.LineColor = newColor();
            return series;
        }

        private void form_SizeChanged(object sender, EventArgs e)
        {
            if (popupForm != null)
            {
                Settings.WindowSize = popupForm.Size;
            }
            OverlayView_SizeChanged(sender, e);
        }

        private void OverlayView_SizeChanged(object sender, EventArgs e)
        {
            setSize();
        }

#if ST_2_1
        private void activity_DataChanged(object sender, NotifyDataChangedEventArgs e)
        {
            updateChart();
        }
#else
        private void Activity_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            updateChart();
        }
#endif
        private void offBox_LostFocus(object sender, EventArgs e)
        {
            ZoneFiveSoftware.Common.Visuals.TextBox box = (ZoneFiveSoftware.Common.Visuals.TextBox)sender;
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
                }
                else
                {
                    double value = UnitUtil.Distance.Parse(maBox.Text);
                    if (value < 0) { throw new Exception(); }
                    Settings.MovingAverageLength = value;
                }
                updateChart();
            }
            catch (Exception)
            {
                //Generic error message
                new WarningDialog(Resources.NonNegativeNumber);
            }
            updateMovingAverage();
        }

		void ResetLastSelectedBoxFonts()
		{
			if ( null != lastChecked && lastChecked.Count > 0 )
			{
				for ( int i = 0; i < lastChecked.Count; i++ )
				{
					lastChecked[i].Font = new Font( lastChecked[i].Font, FontStyle.Regular );
					if ( lastChecked[i] == movingAverage )
					{
						lastChecked[i].ForeColor = Color.Black;
					}
					lastChecked[i].Refresh();
				}
				lastChecked.Clear();
			}
		}

        void chart_Click(object sender, EventArgs e)
        {
			bSelectDataFlag = false;

			if ( bSelectingDataFlag )
			{
				bSelectingDataFlag = false;
				return;
			}
			ResetLastSelectedBoxFonts();

        }

		void chart_SelectingData(object sender, ChartBase.SelectDataEventArgs e)
		{
			if ( ( lastSelectedSeries != null ) && ( lastSelectedSeries != e.DataSeries ) )
				ResetLastSelectedBoxFonts();
			lastSelectedSeries = e.DataSeries;
			bSelectingDataFlag = true;
		}

        void chart_SelectData(object sender, ChartBase.SelectDataEventArgs e)
        {
            if (e != null && e.DataSeries != null)
            {
				bSelectingDataFlag = false;
                if (series2boxes.ContainsKey(e.DataSeries))
                {				
					CheckBox box = series2boxes[e.DataSeries];
					lastChecked.Add( box );
                    if (box == movingAverage)
                    {
                        box.ForeColor = getColor(activities.IndexOf(series2activity[e.DataSeries]) % 10);
						box.Font = new Font( box.Font, FontStyle.Bold );

						box = checkBoxes[activities.IndexOf( series2activity[e.DataSeries] )];
						lastChecked.Add( box );
                    }
                    box.Font = new Font(box.Font, FontStyle.Bold);
					panelAct.ScrollControlIntoView( box );

					if ( bSelectDataFlag )
						chart_SelectingData( sender, e );
					bSelectDataFlag = true;

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
				//string sec = "mm:ss";
				maBox.Text = UnitUtil.Time.ToString( Settings.MovingAverageTime, "mm:ss" );
            }
            else
            {
				maBox.Text = UnitUtil.Distance.ToString( Settings.MovingAverageLength, "u" );
			}
            maBox.Enabled = Settings.ShowMovingAverage;
        }

        private void movingAverage_CheckedChanged(object sender, EventArgs e)
        {
            updateMovingAverage();
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
                    (MessageBox.Show(String.Format(SaveImageResources.FileAlreadyExists, siiPage.FileName),
                                        SaveImageResources.SaveImage, MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes))
					chart.SaveImage( siiPage.ImageSize, siiPage.FileName, siiPage.ImageFormat ); 
#else
                chart.SaveImage(siiPage.ImageSizes[siiPage.ImageSize], siiPage.FileName, siiPage.ImageFormat); 
#endif
            }
		}


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

        private bool _showPage = false;
        private IList<CheckBox> lastChecked = new List<CheckBox>();
        private ChartDataSeries lastSelectedSeries = null;

        private List<IActivity> activities = new List<IActivity>();
        private IDictionary<ChartDataSeries, IActivity> series2activity = new Dictionary<ChartDataSeries, IActivity>();

        private IDictionary<IActivity, IList<double>> actOffsets = new Dictionary<IActivity, IList<double>>();
        private IDictionary<ZoneFiveSoftware.Common.Visuals.TextBox, IActivity> actBoxes = new Dictionary<ZoneFiveSoftware.Common.Visuals.TextBox, IActivity>();
        private IList<ZoneFiveSoftware.Common.Visuals.TextBox> actTextBoxes = new List<ZoneFiveSoftware.Common.Visuals.TextBox>();
        private IDictionary<ChartDataSeries, ZoneFiveSoftware.Common.Visuals.TextBox> series2actBoxes = new Dictionary<ChartDataSeries, ZoneFiveSoftware.Common.Visuals.TextBox>();

        private IList<bool> checks = new List<bool>();
        private IDictionary<CheckBox, int> boxes = new Dictionary<CheckBox, int>();
        private IList<CheckBox> checkBoxes = new List<CheckBox>();
        private IDictionary<ChartDataSeries, CheckBox> series2boxes;

        //bSelectingDataFlag and bSelectDataFlag are used to coordinate the chart 
        //click/select/selecting events to minimize 'movingAverage' and 'box' control flicker.
        //I'm sure there's a better way, but at this time this is all I've got.
        private bool bSelectingDataFlag = false;
        private bool bSelectDataFlag = false;

        private string saveImageProperties_fileName = "";
    }
}
