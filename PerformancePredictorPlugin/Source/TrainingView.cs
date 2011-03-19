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

namespace GpsRunningPlugin.Source
{
    public partial class TrainingView : UserControl
    {
        private IActivity m_activity;
        public IActivity Activity
        {
            get { return m_activity; }
            set
            {
                m_activity = value;
                if (value != null)
                {
                    setPages();
                }
            }
        }

        private PredictTime predictor;
        public PredictTime Predictor
        {
            get { return predictor; }
            set
            {
                predictor = value;
                if (m_activity != null && value != null)
                {
                    setPages();
                }
            }
        }

        public TrainingView()
        {
            InitializeComponent();
            InitControls();
            SizeChanged += new EventHandler(TrainingView_SizeChanged);            
        }

        void InitControls()
        {
            foreach (TabPage tab in this.tabControl1.TabPages)
            {
                foreach (Control grid0 in tab.Controls)
                {
                    if (grid0 is DataGridView)
                    {
                        DataGridView grid = (grid0 as DataGridView);
                        //This will disable gradient header, but make them more like ST controls
                        grid.EnableHeadersVisualStyles = false;
                        grid.AdvancedColumnHeadersBorderStyle.All = DataGridViewAdvancedCellBorderStyle.Outset;
                        grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                        grid.RowsDefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
                    }
                }
            }
            setSize();
        }
        private void SystemPreferences_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            setPages();
        }

#if ST_2_1
        private void Athlete_DataChanged(object sender, ZoneFiveSoftware.Common.Data.NotifyDataChangedEventArgs e)
        {
            setPages();
        }

        private void Logbook_DataChanged(object sender, ZoneFiveSoftware.Common.Data.NotifyDataChangedEventArgs e)
        {
            setPages();
        }
#else
        private void Athlete_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (m_showPage)
            {
                setPages();
            }
        }
        private void Logbook_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (m_showPage)
            {
                setPages();
            }
        }
#endif

        private void TrainingView_SizeChanged(object sender, EventArgs e)
        {
            if (m_showPage)
            {
                setSize();
            }
        }

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
                foreach (Control grid0 in tab.Controls)
                {
                    if (grid0 is DataGridView)
                    {
                        DataGridView grid = (grid0 as DataGridView);
                        grid.ForeColor = fColor;
                        grid.BackgroundColor = bColor;
                        grid.ColumnHeadersDefaultCellStyle.BackColor = visualTheme.SubHeader;
                    }
                }
            }
        }

        public void UICultureChanged(System.Globalization.CultureInfo culture)
        {
            this.trainingTab.Text = StringResources.Training;
            this.paceTempoTab.Text = Resources.PaceForTempoRuns;
            this.intervalTab.Text = Resources.IntervalSplitTimes;
            this.temperatureTab.Text = Resources.TemperatureImpact;
            this.weightTab.Text = Resources.WeighImpact;
            label1.Text = Resources.PaceRunNotification;
            label2.Text = String.Format(Resources.TemperatureNotification, UnitUtil.Temperature.ToString(16, "F0u"));
            interval2Label.Text = Resources.IntervalNotification;
            weightLabel2.Text = String.Format(Resources.WeightNotification, 2 + " " + StringResources.Seconds,
                UnitUtil.Distance.ToString(1000, "u"));
        }

        private bool m_showPage = false;

        public void ShowPage(string bookmark)
        {
            m_showPage = true;
            setPages();
            activateListeners();
        }

        public bool HidePage()
        {
            m_showPage = false;
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

        void popupForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.HidePage();
        }

        private void setSize()
        {
            foreach (TabPage tab in this.tabControl1.TabPages)
            {
                int offset = tabControl1.Location.X+9;
                foreach (Control grid0 in tab.Controls)
                {
                    if (grid0 is DataGridView)
                    {
                        DataGridView grid = (grid0 as DataGridView);
                        grid.Width = Size.Width - offset;
                    }
                }
            }
        }

        public void setPages()
        {
            if (m_showPage && m_activity != null && predictor != null)
            {
                setTraining();
                setPaceTempo();
                setInterval();
                setTemperature();
                setWeight();
                setSize();
            }
        }
        private void setWeight()
        {
            double weight = Plugin.GetApplication().Logbook.Athlete.InfoEntries.LastEntryAsOfDate(m_activity.StartTime).WeightKilograms;
            if (weight.Equals(double.NaN))
            {
                weightLabel.Text = Resources.SetWeight;
                weightLabel2.Visible = false;
                weightGrid.Visible = false;
                return;
            }
            weightLabel2.Visible = true;
            weightGrid.Visible = true;
            ActivityInfo info = ActivityInfoCache.Instance.GetInfo(m_activity);
            weightLabel.Text = Resources.ProjectedWeightImpact + " " +
                UnitUtil.Distance.ToString(info.DistanceMeters, "u");
            TimeSpan time = info.Time;
            DataTable set = new DataTable();
            set.Columns.Add(Resources.ProjectedWeight + UnitUtil.Weight.LabelAbbr2);
            set.Columns.Add(Resources.AdjustedVDOT);
            set.Columns.Add(Resources.EstimatedTime);
            if (Settings.ShowPace)
            {
                set.Columns.Add(Resources.EstimatedPace + UnitUtil.Pace.LabelAbbr2);
            }
            else
            {
                set.Columns.Add(Resources.EstimatedSpeed + UnitUtil.Speed.LabelAbbr2);
            }            
            double inc = 1.4;
            double vdot = getVdot(m_activity);
            for (int i = 0; i < 13; i++)
            {
                set.Rows.Add(getWeightRow(6 - i, vdot, weight, inc, time, info));
            }
            weightGrid.DataSource = set;            
        }

        private object[] getWeightRow(int p, double vdot, double weight, double inc,
            TimeSpan time, ActivityInfo info)
        {
            double projWeight = weight + p * inc;
            double projVdot = vdot * weight / projWeight;
            time = scaleTime(time, Math.Pow(vdot / projVdot, 0.83));

            double speed = info.DistanceMeters * 1000 / time.TotalMilliseconds;
            string str;
            str = UnitUtil.PaceOrSpeed.ToString(Settings.ShowPace, speed);

            return new object[]{ UnitUtil.Weight.ToString(projWeight), present(projVdot, 1), UnitUtil.Time.ToString(time), str };
        }
        private static string present(double p, int digits)
        {
            string pad = "";
            for (int i = 0; i < digits; i++)
            {
                pad += "0";
            }
            return String.Format("{0:0." + pad + "}", p);
        }

        private void setTemperature()
        {
            ActivityInfo info = ActivityInfoCache.Instance.GetInfo(m_activity);
            TimeSpan time = info.Time;
            temperatureLabel.Text = Resources.ProjectedTemperatureImpact+" "+UnitUtil.Distance.ToString(info.DistanceMeters,"u");
            double speed = info.DistanceMeters * 1000 / time.TotalMilliseconds;
            DataTable set = new DataTable();
            set.Columns.Add(CommonResources.Text.LabelTemperature + UnitUtil.Temperature.LabelAbbr2);
            set.Columns.Add(Resources.AdjustedTime);
            if (Settings.ShowPace)
            {
                set.Columns.Add(Resources.AdjustedPace + UnitUtil.Pace.LabelAbbr2);
            }
            else
            {
                set.Columns.Add(Resources.AdjustedSpeed + UnitUtil.Speed.LabelAbbr2);
            }
            float actualTemp = m_activity.Weather.TemperatureCelsius;
            if (!isValidtemperature(actualTemp)){actualTemp = 15;}
            double[] aTemperature = new double[] { 16, 18, 21, 24, 27, 29, 32, 35, 38 };
            for (int i = 0; i < aTemperature.Length; i++)
            {
                set.Rows.Add(getTemperatureRow(aTemperature[i], actualTemp, time, speed));
            }
            for (int i = 0; i < temperatureGrid.Rows.Count && i < aTemperature.Length; i++)
            {
                if (i == aTemperature.Length-1 || (i == 0 || actualTemp >= aTemperature[i - 1]) && (actualTemp < aTemperature[i]))
                {
                    temperatureGrid.Rows[i].DefaultCellStyle.ForeColor = Color.Gray;
                    break;
                }
            }
            temperatureGrid.DataSource = set;
        }

        private object[] getTemperatureRow(double temperature, float actual, TimeSpan time, double speed)
        {
            double f = getTemperatureFactor(temperature) / getTemperatureFactor(actual);
            speed = speed / f;
            time = scaleTime(time, f);

            return new object[] { 
                UnitUtil.Temperature.ToString(temperature, "F0"), 
                UnitUtil.Time.ToString(time), 
                UnitUtil.PaceOrSpeed.ToString(Settings.ShowPace, speed)
            };
        }

        private void setInterval()
        {
            DataTable set = new DataTable();
            set.Columns.Add(UnitUtil.Distance.LabelAxis);
            set.Columns.Add(Length.ToString(1, Length.Units.Mile, "F0u"));
            set.Columns.Add(Length.ToString(5, Length.Units.Kilometer, "F0u"));
            set.Columns.Add(Length.ToString(10, Length.Units.Kilometer, "F0u"));
            ActivityInfo info = ActivityInfoCache.Instance.GetInfo(m_activity);
            double distance = info.DistanceMeters;
            double seconds = info.Time.TotalSeconds;
            double mileSpeed = getTrainingSpeed(1609.344, distance, seconds);
            double k5Speed = getTrainingSpeed(5000, distance, seconds);
            double k10Speed = getTrainingSpeed(10000, distance, seconds);
            double[] distances = new double[] { 100, 200, 300, 400, 800, 1000, 1609.344 };
            for (int i = 0; i < distances.Length; i++)
            {
                set.Rows.Add(getIntervalRow(distances[i], mileSpeed, k5Speed, k10Speed));
            }

            intervalGrid.DataSource = set;
        }

        private object[] getIntervalRow(double p, double mileSpeed, double k5Speed, double k10Speed)
        {
            //The Speeds are passed here, they are scaled and presented as Pace
            //(there must be no unit!)
            double f = 1000.0/p;
            return new object[] {
                    UnitUtil.Distance.ToString(p),
                    UnitUtil.Pace.ToString(f*mileSpeed, "mm:ss"),
                    UnitUtil.Pace.ToString(f*k5Speed, "mm:ss"),
                    UnitUtil.Pace.ToString(f*k10Speed, "mm:ss")
            };
        }

        private void setPaceTempo()
        {
            paceTempoLabel.Text = String.Format(Resources.PaceForTempoRuns_label, getVdot(m_activity));
            DataTable set = new DataTable();
            set.Columns.Add(CommonResources.Text.LabelDuration + " (" + StringResources.MinutesShort + ")");
            set.Columns.Add(UnitUtil.PaceOrSpeed.LabelAxis(Settings.ShowPace));
            string[] durations = new string[] { "20", "25",  "30",  "35",  "40",  "45",  "50",    "55", "60" };
            double[] factors = new double[]    { 1,  1.012, 1.022, 1.027, 1.033, 1.038, 1.043, 1.04866, 1.055};
            double vdot = getVdot(m_activity);

            double speed = getTrainingSpeed(vdot, 0.93);
            for (int i = 0; i < durations.Length; i++)
            {
                DataRow row = set.NewRow();
                row[0] = durations[i];
                row[1] = UnitUtil.PaceOrSpeed.ToString(Settings.ShowPace, speed/factors[i]);
                set.Rows.Add(row);
            }
            paceTempoGrid.DataSource = set;
        }

        private TimeSpan scaleTime(TimeSpan pace, double p)
        {
            return new TimeSpan(0, 0, 0, 0, (int)Math.Round(pace.TotalMilliseconds * p));
        }

        private void setTraining()
        {
            double maxHr = Plugin.GetApplication().Logbook.Athlete.InfoEntries.LastEntryAsOfDate(DateTime.Now).MaximumHeartRatePerMinute;
            if (maxHr.Equals(double.NaN))
            {
                trainingLabel.Text = Resources.NoMaxHR;
                trainingGrid.Visible = false;
                return;
            }
            trainingGrid.Visible = true;
            DataTable set = new DataTable();
            set.Columns.Add(Resources.ZoneDistance);
            set.Columns.Add(CommonResources.Text.LabelPercentOfMax, typeof(double));
            set.Columns.Add(Resources.TrainRaceHR, typeof(double));
            if (Settings.ShowPace)
            {
                set.Columns.Add(UnitUtil.Pace.LabelAxis);
            }
            else
            {
                set.Columns.Add(UnitUtil.Speed.LabelAxis, typeof(double));
            }
            //addTrainingRow(set, percent);
            double vo2max = getVo2max(m_activity);
            double vdot = getVdot(m_activity);
            IList<String> zones = getZones();
            IList<double> percentages = getPercentages(vdot);
            IList<double> hrs = getHeartRates(percentages);
            IList<double> paces = getSpeeds(vdot, percentages);
            for (int i = 0; i < 15; i++)
            {
                DataRow row = set.NewRow();
                row[0] = zones[i];
                row[1] = (100*percentages[i]).ToString("F1");
                row[2] = UnitUtil.HeartRate.ToString(hrs[i]);
                row[3] = UnitUtil.PaceOrSpeed.ToString(Settings.ShowPace, paces[i]);
                set.Rows.Add(row);
            }
            trainingGrid.DataSource = set;
            trainingLabel.Text = String.Format(Resources.VO2MaxVDOT,
                100*vo2max, vdot);
        }

        private IList<string> getZones()
        {
            string[] result = new string[15];
            result[0] = Resources.Recovery;
            result[1] = Resources.EasyAerobicZone;
            result[2] = Resources.EasyAerobicZone;
            result[3] = Resources.EasyAerobicZone;
            result[4] = Resources.ModAerobicZone;
            result[5] = Resources.HighAerobicZone;
            result[6] = StringResources.Marathon;
            result[7] = "1/2 " + StringResources.Marathon;
            result[8] = "15 " + Length.LabelAbbr(Length.Units.Kilometer);
            result[9] = "12 " + Length.LabelAbbr(Length.Units.Kilometer);
            result[10] = "10 " + Length.LabelAbbr(Length.Units.Kilometer);
            result[11] = "8 " + Length.LabelAbbr(Length.Units.Kilometer);
            result[12] = "5 "  + Length.LabelAbbr(Length.Units.Kilometer);
            result[13] = "3 " + Length.LabelAbbr(Length.Units.Kilometer);
            result[14] = "1 " + Length.LabelAbbr(Length.Units.Mile);
            return result;
        }

        private IList<double> getSpeeds(double vdot, IList<double> percentages)
        {
            ActivityInfo info = ActivityInfoCache.Instance.GetInfo(m_activity);
            double seconds = info.Time.TotalSeconds;
            double distance = info.DistanceMeters;
            double[] result = new double[15];
            result[0] = getTrainingSpeed(vdot, percentages[0]);
            result[1] = getTrainingSpeed(vdot, percentages[1]);
            result[2] = getTrainingSpeed(vdot, percentages[2]);
            result[3] = getTrainingSpeed(vdot, percentages[3]);
            result[6] = getTrainingSpeed(42195, distance, seconds);
            result[4] = result[3] / (1 + (result[3] / result[6] - 1) / 6.0);
            result[5] = result[3] / (1 + (result[3] / result[6] - 1) / 3.0);
            result[7] = getTrainingSpeed(21097.5, distance, seconds);
            result[8] = getTrainingSpeed(15000, distance, seconds);
            result[9] = getTrainingSpeed(12000, distance, seconds);
            result[10] = getTrainingSpeed(10000, distance, seconds);
            result[11] = getTrainingSpeed(8000, distance, seconds);
            result[12] = getTrainingSpeed(5000, distance, seconds);
            result[13] = getTrainingSpeed(3000, distance, seconds);
            result[14] = getTrainingSpeed(1609.344, distance, seconds); ;
            return result;
        }

        private double getTrainingSpeed(double new_dist, double old_dist, double old_time)
        {
            return new_dist / Predictor(new_dist, old_dist, old_time);
        }
        //Get training speed from vdot
        private double getTrainingSpeed(double vdot, double percentZone)
        {
            return (29.54 + 5.000663 * (vdot * (percentZone - 0.05))
                - 0.007546 * Math.Pow(vdot * (percentZone - 0.05), 2)) / 60;
        }

        private IList<double> getHeartRates(IList<double> percentages)
        {
            IList<double> result = new List<double>();
            double maxHr = Plugin.GetApplication().Logbook.Athlete.InfoEntries.LastEntryAsOfDate(DateTime.Now).MaximumHeartRatePerMinute;
            foreach (double p in percentages)
            {
                result.Add(p * maxHr);
            }
            return result;
        }

        private double[] getPercentages(double vdot)
        {
            double[] result = new double[15];
            result[0] = 0.65;
            result[1] = 0.70;
            result[2] = 0.72;
            result[3] = 0.75;
            result[6] = 0.8 + 0.09 * (vdot - 30) / 55;
            result[4] = result[3] + (result[6] - result[3])/6.0;
            result[5] = result[3] + (result[6] - result[3])/3.0;
            result[7] = 0.84 + 0.08 * (vdot - 30) / 55;
            result[8] = 0.86 + 0.08 * (vdot - 30) / 55;
            result[9] = 0.87 + 0.08 * (vdot - 30) / 55;
            result[10] = 0.88 + 0.08 * (vdot - 30) / 55;
            result[11] = 0.9 + 0.08 * (vdot - 30) / 55;
            result[12] = 0.94 + 0.05 * (vdot - 30) / 55;
            result[13] = 0.98 + 0.02 * (vdot - 30) / 55;
            result[14] = 1;
            return result;
        }

        private double getVo2max(IActivity activity)
        {
            ActivityInfo info = ActivityInfoCache.Instance.GetInfo(activity);
            return 0.8 + 0.1894393 * Math.Exp(-0.012778 * info.Time.TotalMilliseconds / 60000)
                + 0.2989558 * Math.Exp(-0.1932605 * info.Time.TotalMilliseconds / 60000);
        }

        private double getVdot(IActivity activity)
        {
            ActivityInfo info = ActivityInfoCache.Instance.GetInfo(activity);
            return (-4.6 + 0.182258 * (info.DistanceMeters * 60000 / info.Time.TotalMilliseconds)
                + 0.000104 * Math.Pow(info.DistanceMeters * 60000 / info.Time.TotalMilliseconds, 2)) 
                / getVo2max(activity);
        }
        //Table from Kristian Bisgaard Lassen (unknown source)
        //Celcius factor
        //16 1
        //18 1.0075
        //21  1.015
        //24 1.0225
        //27 1.03 
        //29 1.0375
        //32 1.045
        //35 1.0525
        //38 1.06

        private bool isValidtemperature(double temperature)
        {
            if (double.IsNaN(temperature) || temperature <= 16 || temperature > 45)
            {
                return false;
            }
            return true;
        }
        private double getTemperatureFactor(double temperature)
        {
            if (!isValidtemperature(temperature))
            {
                //Outside range or invalid
                //Assume over 45 is invalid
                return 1;
            }
            else if (temperature < 20) { return 1.0075; }
            else if (temperature < 23) { return 1.015; }
            else if (temperature < 26) { return 1.0225; }
            else if (temperature < 28) { return 1.03; }
            else if (temperature < 31) { return 1.0375; }
            else if (temperature < 34) { return 1.045; }
            else if (temperature < 37) { return 1.0525; }
            return 1.06;
        }

        //Adapted from ApplyRoutes
        void tabControl1_DrawItem(object sender, DrawItemEventArgs e)
        {
            Brush backBrush;
            Brush foreBrush = new SolidBrush(m_visualTheme.ControlText);
            TabControl tc = sender as TabControl;
            if (tc == null) return;

            if (e.Index == tc.SelectedIndex)
            {
                foreBrush = new SolidBrush(m_visualTheme.ControlText);
                backBrush = new SolidBrush(m_visualTheme.Control);
            }
            else
            {
                foreBrush = new SolidBrush(m_visualTheme.SubHeaderText);
                backBrush = new SolidBrush(m_visualTheme.SubHeader);
            }

            string tabName = tc.TabPages[e.Index].Text;
            StringFormat sf = new StringFormat();
            sf.Alignment = StringAlignment.Center;
            e.Graphics.FillRectangle(backBrush, e.Bounds);
            Rectangle r = e.Bounds;
            r = new Rectangle(r.X, r.Y + 3, r.Width, r.Height - 3);
            e.Graphics.DrawString(tabName, e.Font, foreBrush, r, sf);

            //The right upper edge
            Brush background_brush = new SolidBrush(m_visualTheme.Control);//Backcolor of the form
            Rectangle LastTabRect = tc.GetTabRect(tc.TabPages.Count - 1);
            Rectangle rect = new Rectangle();
            rect.Location = new Point(LastTabRect.Right + this.Left, this.Top);
            rect.Size = new Size(this.Right - rect.Left, LastTabRect.Height);
            e.Graphics.FillRectangle(background_brush, rect);
            background_brush.Dispose();

            sf.Dispose();
            backBrush.Dispose();
            foreBrush.Dispose();

            e.DrawFocusRectangle();
        }

    }
}
