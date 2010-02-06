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
using ZoneFiveSoftware.Common.Data.Fitness;
using ZoneFiveSoftware.Common.Visuals;
using ZoneFiveSoftware.Common.Visuals.Chart;
using ZoneFiveSoftware.Common.Data.Measurement;
using System.Reflection;
using SportTracksPerformancePredictorPlugin.Properties;
using SportTracksPerformancePredictorPlugin.Util;

namespace SportTracksPerformancePredictorPlugin.Source
{
    public partial class PerformancePredictorView : UserControl
    {
        private TrainingView trainingView;

        private IActivity activity;
        public IActivity Activity
        {
            get { return activity; }
            set
            {
                activity = value;
                if (activity != null)
                {
                    activity.DataChanged += new ZoneFiveSoftware.Common.Data.NotifyDataChangedEventHandler(dataChanged);
                }
                trainingView.Activity = value;
                makeData();                
            }
        }

        private IList<IList<Object>> results;
        private IList<IActivity> activities;
        public IList<IActivity> Activities
        {
            get { return activities; }
            set
            {
                activities = value;
                IList<double> partialDistances = new List<double>();
                foreach (double distance in Settings.Distances.Keys)
                {
                    partialDistances.Add(distance * Settings.PercentOfDistance / 100.0);
                }
                training.Checked = false;
                timePrediction.Checked = true;
                progressBar.Visible = true;
                progressBar.Minimum = 0;
                progressBar.Maximum = activities.Count;
                results = (IList<IList<Object>>)
                    Settings.highScore.GetMethod("getFastestTimesOfDistances").Invoke(null, 
                    new object[] { activities, partialDistances, progressBar });
                progressBar.Visible = false;
                makeData();
            }
        }

        private void dataChanged(object sender, ZoneFiveSoftware.Common.Data.NotifyDataChangedEventArgs e)
        {
            makeData();
        }

        private ChartDataSeries cameronSeries, riegelSeries;
        private DataTable cameronSet, riegelSet;

        private Form form;

        public PerformancePredictorView(IActivity activity)
        {
            InitializeComponent();
            groupBox3.Text = StringResources.Settings;
            groupBox1.Text = Resources.PredictionModel;
            groupBox2.Text = Resources.Velocity;
            resultBox.Text = Resources.PredictionResults;
            timePrediction.Text = Resources.TimePrediction;
            training.Text = StringResources.Training;
            pace.Text = CommonResources.Text.LabelPace;
            speed.Text = CommonResources.Text.LabelSpeed;
            chartButton.Text = Resources.ViewInChart;
            table.Text = Resources.ViewInTable;
            trainingView = new TrainingView();
            trainingView.Location = chart.Location;
            Controls.Add(trainingView);
            progressBar.Visible = false;
            Plugin.GetApplication().SystemPreferences.PropertyChanged += new PropertyChangedEventHandler(SystemPreferences_PropertyChanged);
            cameronSeries = new ChartDataSeries(chart, chart.YAxis);
            riegelSeries = new ChartDataSeries(chart, chart.YAxis);
            cameronSet = new DataTable();
            riegelSet = new DataTable();
            if (Parent != null)
            {
                Parent.Resize += new EventHandler(Parent_Resize);
            }
            Resize += new EventHandler(PerformancePredictorView_Resize);
            Settings settings = new Settings();
            switch (Settings.Model)
            {
                case PredictionModel.DAVE_CAMERON:
                    daveCameron.Checked = true;
                    trainingView.Predictor = Cameron;
                    break;
                case PredictionModel.PETE_RIEGEL:
                    reigel.Checked = true;
                    trainingView.Predictor = Riegel;
                    break;
            }
            chartButton.Checked = Settings.ShowChart;
            table.Checked = !Settings.ShowChart;
            timePrediction.Checked = Settings.ShowPrediction;
            training.Checked = !Settings.ShowPrediction;
            pace.Checked = Settings.ShowPace;
            speed.Checked = !Settings.ShowPace;
            setSize();
            chart.YAxis.Formatter = new Formatter.SecondsToTime();            
            Activity = activity;
            Settings.DistanceChanged += new PropertyChangedEventHandler(Settings_DistanceChanged);
        }

        public PerformancePredictorView(IList<IActivity> activities)
            :
            this((IActivity)null)
        {
            form = new Form();
            form.Controls.Add(this);
            form.Size = Settings.WindowSize;
            Parent.SizeChanged += new EventHandler(Parent_SizeChanged);
            form.StartPosition = FormStartPosition.CenterScreen;
            form.Icon = Icon.FromHandle(Properties.Resources.Image_32_PerformancePredictor.GetHicon());
            form.Show();
            if (activities.Count == 1) form.Text = Resources.PPHS + StringResources.ForOneActivity;
            else form.Text = Resources.PPHS + String.Format(StringResources.ForManyActivities, activities.Count);
            Activities = activities;
        }

        void Parent_SizeChanged(object sender, EventArgs e)
        {
            setSize();
        }

        private void Settings_DistanceChanged(object sender, PropertyChangedEventArgs e)
        {
            makeData();
        }

        private void SystemPreferences_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            makeData();
        }

        private void form_Resize(object sender, EventArgs e)
        {
            setSize();
        }

        private void PerformancePredictorView_Resize(object sender, EventArgs e)
        {
            setSize();
        }

        private void Parent_Resize(object sender, EventArgs e)
        {
            setSize();
        }

        private void setSize()
        {
            int rpch = 0, rpcw = 0;
            if (form != null)
            {
                if (null == Parent)
                {
                    //Should only occur when switching language
                    Size = new Size();
                }
                else
                {
                    Size = Parent.Size;
                }
                rpch = 40;
                rpcw = 10;
            }
            chart.Size = new Size(Size.Width - chart.Location.X - rpcw,
                Size.Height - chart.Location.Y - rpch);
            trainingView.Size = chart.Size;
            int columnWidth;
            if (activities != null && activities.Count > 0)
            {
                columnWidth = (int)Math.Floor((chart.Size.Width - 15)/ 10.0);
            }
            else
            {
                columnWidth = (int)Math.Floor((chart.Size.Width - 15) / 4.0);
            }
            foreach (DataGridViewColumn column in dataGrid.Columns)
            {
                column.Width = columnWidth;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
            int height = dataGrid.ColumnHeadersHeight;
            if (dataGrid.Rows.Count > 0)
            {
                height += dataGrid.Rows[0].Height * dataGrid.Rows.Count;
            }
            if (height > chart.Size.Height)
            {
                height = chart.Size.Height;
            }
            int width = 0;
            PropertyInfo propInfo = dataGrid.GetType().GetProperty("VerticalScrollBar",
                        BindingFlags.Instance | BindingFlags.NonPublic);
            if (propInfo != null)
            {
                ScrollBar propValue = propInfo.GetValue(dataGrid, null) as ScrollBar;
                if (propValue != null)
                {
                    if (propValue.Visible)
                    {
                        width += propValue.Width;
                    }
                }
            }
            dataGrid.Size = new Size(chart.Size.Width - 17+width,
                    height-3);
        }

        private void setView()
        {
            dataGrid.Visible = false;
            chart.Visible = false;
            trainingView.Visible = false;
            this.table.Enabled = false;
            chartButton.Enabled = false;
            if (activity == null)
            {
                timePrediction.Enabled = false;
                training.Enabled = false;
            }
            else
            {
                timePrediction.Enabled = true;
                training.Enabled = true;
            }
            if (activity == null && (activities == null || activities.Count == 0)) 
                return;
            if (!Settings.ShowPrediction && activities == null)
            {
                trainingView.Visible = true;
                return;
            }
            this.table.Enabled = true;
            chartButton.Enabled = true;
            DataTable table = null;
            ChartDataSeries series = null;
            switch (Settings.Model)
            {
                case PredictionModel.DAVE_CAMERON:
                    table = cameronSet;
                    series = cameronSeries;
                    break;
                case PredictionModel.PETE_RIEGEL:
                    table = riegelSet;
                    series = riegelSeries;
                    break;
            }
            if (table.Rows.Count == 0 || series.Points.Count == 0) return;
            dataGrid.DataSource = table;
            if (chart != null && !chart.IsDisposed)
            {
                chart.DataSeries.Clear();
                chart.DataSeries.Add(series);
                chart.AutozoomToData(true);
                chart.XAxis.Label = UnitUtil.Distance.LabelAxis;
                chart.YAxis.Label = UnitUtil.Time.LabelAxis;
            }
            if (!Settings.ShowChart)
            {
                dataGrid.Visible = true;
            }
            else
            {
                chart.Visible = true;
            }
            setSize();
        }

        private void makeData()
        {
            cameronSet.Clear(); cameronSet.Rows.Clear(); cameronSeries.Points.Clear();
            riegelSet.Clear(); riegelSet.Rows.Clear(); riegelSeries.Points.Clear();

            if (activity == null && (activities == null || activities.Count == 0))
            {
                setView();
                return;
            }

            if (activity == null)
            {
                makeData(cameronSet, cameronSeries, Cameron);
                makeData(riegelSet, riegelSeries, Riegel);
            }
            else
            {
                ActivityInfo info = ActivityInfoCache.Instance.GetInfo(activity);

                if (info.DistanceMetersMoving == 0 || info.Time.TotalSeconds == 0)
                {
                    setView();
                    return;
                }

                makeData(cameronSet, cameronSeries, Cameron,
                    info.DistanceMetersMoving, info.Time.TotalSeconds);
                makeData(riegelSet, riegelSeries, Riegel,
                    info.DistanceMetersMoving, info.Time.TotalSeconds);
            }
            setView();
        }

        PredictTime Cameron = delegate(double new_dist, double old_dist, double old_time)
                    {
                        double a = 13.49681 - (0.000030363 * old_dist)
                            + (835.7114 / Math.Pow(old_dist, 0.7905));
                        double b = 13.49681 - (0.000030363 * new_dist)
                            + (835.7114 / Math.Pow(new_dist, 0.7905));
                        double new_time = (old_time / old_dist) * (a / b) * new_dist;
                        return new_time;
                    };

        PredictTime Riegel = delegate(double new_dist, double old_dist, double old_time)
                    {
                        double new_time = old_time * Math.Pow(new_dist / old_dist, 1.06);
                        return new_time;
                    };

        private void makeData(DataTable set, ChartDataSeries series,
            PredictTime predict)
        {
            set.Clear();
            set.Columns.Clear();
            set.Columns.Add(UnitUtil.Distance.LabelAxis);
            set.Columns.Add(CommonResources.Text.LabelDistance);
            set.Columns.Add(Resources.PredictedTime, typeof(TimeSpan));
            if (Settings.ShowPace)
            {
                set.Columns.Add(UnitUtil.Pace.LabelAxis);
            }
            else
            {
                set.Columns.Add(UnitUtil.Speed.LabelAxis, typeof(double));
            }
            set.Columns.Add(Resources.UsedActivityStartDate);
            set.Columns.Add(Resources.UsedActivityStartTime);
            set.Columns.Add(Resources.UsedTimeOfActivity, typeof(TimeSpan));
            set.Columns.Add(Resources.StartOfPart + UnitUtil.Distance.LabelAbbr2);
            set.Columns.Add(Resources.UsedLengthOfActivity + UnitUtil.Distance.LabelAbbr2);
            series.Points.Clear();

            int index = 0;

            foreach (IList<Object> result in results)
            {
                IActivity foundActivity = (IActivity)result[0];
                int old_time = (int)result[1];
                double meterStart = (double)result[2];
                double meterEnd = (double)result[3];
                int timeStart = 0;
                if (result.Count > 4) { timeStart = (int)result[4]; }
                double old_dist = meterEnd - meterStart;
                double new_dist = old_dist * (100 / Settings.PercentOfDistance);
                double new_time = predict(new_dist, old_dist, old_time);
                series.Points.Add(index,
                    new PointF((float)UnitUtil.Distance.ConvertFrom(new_dist), (float)new_time));

                double length = Settings.Distances.Keys[index];
                DataRow row = set.NewRow();
                row[0] = UnitUtil.Distance.ToString(length);
                if (Settings.Distances[length].Values[0])
                {
                    row[1] = UnitUtil.Distance.ToString(length, "u");
                }
                else
                {
                    row[1] = UnitUtil.Distance.ToString(length, Settings.Distances[length].Keys[0], "u");
                }
                row[2] = UnitUtil.Time.ToString(new_time);
                double speed = new_dist / new_time;
                row[3] = UnitUtil.PaceOrSpeed.ToString(Settings.ShowPace, speed);
                row[4] = foundActivity.StartTime.ToLocalTime().ToShortDateString();
                row[5] = foundActivity.StartTime.AddSeconds(timeStart).ToLocalTime().ToShortTimeString();
                row[6] = new TimeSpan(0, 0, old_time);
                row[7] = UnitUtil.Distance.ToString(meterStart);
                row[8] = UnitUtil.Distance.ToString(old_dist);
                set.Rows.Add(row);
                index++;
            }
            for (int i = index; i < Settings.Distances.Count; i++)
            {
                DataRow row = set.NewRow();
                double key = Settings.Distances.Keys[i];
                Length.Units unit = Settings.Distances[key].Keys[0];
                row[0] = UnitUtil.Distance.ToString(key);
                if (Settings.Distances[key][unit])
                {
                    row[1] = UnitUtil.Distance.ToString(key, "u");
                }
                else
                {
                    row[1] = UnitUtil.Distance.ToString(key, unit, "u");
                }

                row[4] = Resources.NoSeedActivity;
                set.Rows.Add(row);
            }
        }

        private void makeData(DataTable set, ChartDataSeries series,
            PredictTime predict, double old_dist, double old_time)
        {
            set.Clear();
            set.Columns.Clear();
            set.Columns.Add(UnitUtil.Distance.LabelAxis);
            set.Columns.Add(CommonResources.Text.LabelDistance);
            set.Columns.Add(Resources.PredictedTime, typeof(TimeSpan));
            if (Settings.ShowPace)
            {
                set.Columns.Add(UnitUtil.Pace.LabelAxis);
            }
            else
            {
                set.Columns.Add(UnitUtil.Speed.LabelAxis, typeof(double));
            }
            series.Points.Clear();

            int index = 0;
            foreach (double new_dist in Settings.Distances.Keys)
            {
                double new_time = predict(new_dist, old_dist, old_time);
                series.Points.Add(index++,
                    new PointF((float)UnitUtil.Distance.ConvertFrom(new_dist), (float)new_time));

                double length = new_dist;
                DataRow row = set.NewRow();
                row[0] = UnitUtil.Distance.ToString(length);
                if (Settings.Distances[new_dist].Values[0])
                {
                    row[1] = UnitUtil.Distance.ToString(length, "u");
                }
                else
                {
                    row[1] = UnitUtil.Distance.ToString(length, Settings.Distances[new_dist].Keys[0], "u");
                }
                row[2] = UnitUtil.Time.ToString(new_time);
                double speed = new_dist / new_time;
                row[3] = UnitUtil.PaceOrSpeed.ToString(Settings.ShowPace, speed);
                set.Rows.Add(row);
            }
        }

        private void daveCameron_CheckedChanged(object sender, EventArgs e)
        {
            if (daveCameron.Checked)
            {
                Settings.Model = PredictionModel.DAVE_CAMERON;
                setView();
                trainingView.Predictor = Cameron;
            }
        }

        private void reigel_CheckedChanged(object sender, EventArgs e)
        {
            if (reigel.Checked)
            {
                Settings.Model = PredictionModel.PETE_RIEGEL;
                setView();
                trainingView.Predictor = Riegel;
            }
        }

        private void chartButton_CheckedChanged(object sender, EventArgs e)
        {
            if (chartButton.Checked)
            {
                Settings.ShowChart = true;
                updateVisibility();
            }
        }

        private void table_CheckedChanged(object sender, EventArgs e)
        {
            if (table.Checked)
            {
                Settings.ShowChart = false;
                updateVisibility();
            }
        }

        private void updateVisibility()
        {
            if (Settings.ShowChart)
            {
                dataGrid.Visible = false;
                chart.Visible = true;
            }
            else
            {
                dataGrid.Visible = true;
                chart.Visible = false;
            }
        }

        private void timePrediction_CheckedChanged(object sender, EventArgs e)
        {
            if (timePrediction.Checked && activities == null)
            {
                Settings.ShowPrediction = true;
                setView();
            }
        }

        private void training_CheckedChanged(object sender, EventArgs e)
        {
            if (training.Checked && activities == null)
            {
                Settings.ShowPrediction = false;
                setView();               
            }
        }

        private void pace_CheckedChanged(object sender, EventArgs e)
        {
            if (pace.Checked)
            {
                Settings.ShowPace = true;
                makeData();
                trainingView.setPages();
            }
        }

        private void speed_CheckedChanged(object sender, EventArgs e)
        {
            if (speed.Checked)
            {
                Settings.ShowPace = false;
                makeData();
                trainingView.setPages();
            }
        }

    }

    public delegate double PredictTime(double new_dist, double old_dist, double old_time);

}
