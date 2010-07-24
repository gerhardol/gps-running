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
#if ST_2_1
                    activity.DataChanged += new ZoneFiveSoftware.Common.Data.NotifyDataChangedEventHandler(dataChanged);
#else
                    activity.PropertyChanged += new PropertyChangedEventHandler(Activity_PropertyChanged);
#endif
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
                    //Scale down the distances, so we get the high scores
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

#if ST_2_1
        private void dataChanged(object sender, ZoneFiveSoftware.Common.Data.NotifyDataChangedEventArgs e)
        {
            makeData();
        }
#else
        private void Activity_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            makeData();
        }
#endif
        private ChartDataSeries cameronSeries, riegelSeries;
        private DataTable cameronSet, riegelSet;

        private Form popupForm;

        public PerformancePredictorView(IActivity activity)
        {
            InitializeComponent(); 
            InitControls();

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
            chart.XAxis.Formatter = new Formatter.General(UnitUtil.Distance.DefaultDecimalPrecision);
            Activity = activity;
            //Remove this listener - let user explicitly update after changing settings, to avoid crashes
            //Settings.DistanceChanged += new PropertyChangedEventHandler(Settings_DistanceChanged);
        }

        public PerformancePredictorView(IList<IActivity> activities)
            : this((IActivity)null)
        {
            //Theme and Culture must be set manually
            this.ThemeChanged(
#if ST_2_1
                Plugin.GetApplication().VisualTheme
#else
                Plugin.GetApplication().SystemPreferences.VisualTheme
#endif
            );
            this.UICultureChanged(
#if ST_2_1
                new System.Globalization.CultureInfo("en")
#else
                Plugin.GetApplication().SystemPreferences.UICulture
#endif
            );
            popupForm = new Form();
            popupForm.Controls.Add(this);
            popupForm.Size = Settings.WindowSize;
            //Fill would be simpler here, but then edges are cut
            this.Size = new Size(Parent.Size.Width - 17, Parent.Size.Height - 38);
            this.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                    | System.Windows.Forms.AnchorStyles.Right | System.Windows.Forms.AnchorStyles.Bottom)));
            Parent.SizeChanged += new EventHandler(Parent_SizeChanged);
            dataGrid.CellContentDoubleClick += new DataGridViewCellEventHandler(selectedRow_DoubleClick);
            popupForm.StartPosition = FormStartPosition.CenterScreen;
            popupForm.Icon = Icon.FromHandle(Properties.Resources.Image_32_PerformancePredictor.GetHicon());
            popupForm.Show();

            if (activities.Count == 1) popupForm.Text = Resources.PPHS + " " + StringResources.ForOneActivity;
            else popupForm.Text = Resources.PPHS + " " + String.Format(StringResources.ForManyActivities, activities.Count);
            Activities = activities;
        }

        void InitControls()
        {
            trainingView = new TrainingView();
            trainingView.Location = chart.Location;
            trainingView.ThemeChanged(
#if ST_2_1
                Plugin.GetApplication().VisualTheme
#else
                Plugin.GetApplication().SystemPreferences.VisualTheme
#endif
                                     );
            trainingView.UICultureChanged(
#if ST_2_1
                new System.Globalization.CultureInfo("en")
#else
                Plugin.GetApplication().SystemPreferences.UICulture
#endif
                                         );
            this.splitContainer1.Panel2.Controls.Add(trainingView);
            trainingView.Dock = DockStyle.Fill;
            progressBar.Visible = false;
        }
        public void ThemeChanged(ITheme visualTheme)
        {
            //RefreshPage();
            //m_visualTheme = visualTheme;
            this.chart.ThemeChanged(visualTheme);
            //Set color for non ST controls
            this.dataGrid.BackgroundColor = visualTheme.Control;
            this.splitContainer1.Panel1.BackColor = visualTheme.Control;
            this.splitContainer1.Panel2.BackColor = visualTheme.Control;
            if (null != trainingView)
            {
                trainingView.ThemeChanged(visualTheme);
            }
        }
        public void UICultureChanged(System.Globalization.CultureInfo culture)
        {
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
            if (null != trainingView)
            {
                trainingView.UICultureChanged(culture);
            }
        }

        private void setSize()
        {
            if (dataGrid.Columns.Count > 0 && dataGrid.Rows.Count > 0)
            {
                foreach (DataGridViewColumn column in dataGrid.Columns)
                {
                    if (column.Name.Equals(ActivityIdColumn))
                    {
                        column.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                        column.Width = 0;
                        column.Visible = false;
                    }
                    else
                    {
                        column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        column.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                    }
                }
            }
        }

        private const string ActivityIdColumn = "ActivityId";
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

                if (info.DistanceMeters == 0 || info.Time.TotalSeconds == 0)
                {
                    setView();
                    return;
                }

                makeData(cameronSet, cameronSeries, Cameron,
                    info.DistanceMeters, info.Time.TotalMilliseconds / 1000);
                makeData(riegelSet, riegelSeries, Riegel,
                    info.DistanceMeters, info.Time.TotalMilliseconds / 1000);
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
            set.Columns.Add(ActivityIdColumn);
            series.Points.Clear();

            int index = 0;
            foreach (IList<Object> result in results)
            {
                IActivity foundActivity = (IActivity)result[0];
                double old_time = double.Parse(result[1].ToString());
                double meterStart = (double)result[2];
                double meterEnd = (double)result[3];
                double timeStart = 0;
                if (result.Count > 4) { timeStart = double.Parse(result[4].ToString()); }
                double old_dist = meterEnd - meterStart;
                double new_dist = old_dist * 100 / Settings.PercentOfDistance;
                double new_time = predict(new_dist, old_dist, old_time);
                float x = (float)UnitUtil.Distance.ConvertFrom(new_dist);
                if (!x.Equals(float.NaN) && series.Points.IndexOfKey(x) == -1)
                {
                    series.Points.Add(x, new PointF(x, (float)new_time));
                }

                //length is the distance HighScore tried to get a prediction  for, may differ to actual dist
                double length = Settings.Distances.Keys[index];
                DataRow row = set.NewRow();
                row[0] = UnitUtil.Distance.ToString(new_dist);
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
                row[6] = UnitUtil.Time.ToString(old_time);
                row[7] = UnitUtil.Distance.ToString(meterStart);
                row[8] = UnitUtil.Distance.ToString(old_dist);
                row[ActivityIdColumn] = foundActivity.ReferenceId;
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
            if (null != set || null != set.Columns)
            {
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

                foreach (double new_dist in Settings.Distances.Keys)
                {
                    double new_time = predict(new_dist, old_dist, old_time);
                    float x = (float)UnitUtil.Distance.ConvertFrom(new_dist);
                    if (!x.Equals(float.NaN) && series.Points.IndexOfKey(x) == -1)
                    {
                        series.Points.Add(x, new PointF(x, (float)new_time));
                    }

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
        }

        /**************************************************/

        void Parent_SizeChanged(object sender, EventArgs e)
        {
            if (popupForm != null)
            {
                Settings.WindowSize = popupForm.Size;
            }
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
        private void selectedRow_DoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            Guid view = new Guid("1dc82ca0-88aa-45a5-a6c6-c25f56ad1fc3");

            int rowIndex = e.RowIndex;
            if (rowIndex >= 0 && dataGrid.Columns[ActivityIdColumn] != null)
            {
                object id = dataGrid.Rows[rowIndex].Cells[ActivityIdColumn].Value;
                if (id != null)
                {
                    string bookmark = "id=" + id;
                    Plugin.GetApplication().ShowView(view, bookmark);
                }
            }
        }
    }

    public delegate double PredictTime(double new_dist, double old_dist, double old_time);
}
