using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using ZoneFiveSoftware.Common.Data.Fitness;
using System.Reflection;
using ZoneFiveSoftware.Common.Data.Measurement;
using SportTracksPerformancePredictorPlugin.Properties;

namespace SportTracksPerformancePredictorPlugin.Source
{
    public partial class TrainingView : UserControl
    {
        private IActivity activity;
        public IActivity Activity
        {
            get { return activity; }
            set
            {
                activity = value;
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
                if (activity != null && value != null)
                {
                    setPages();
                }
            }
        }

        public TrainingView()
        {
            InitializeComponent();
            tabControl1.TabPages[0].Text = Resources.Training;
            tabControl1.TabPages[1].Text = Resources.PaceForTempoRuns;
            tabControl1.TabPages[2].Text = Resources.IntervalSplitTimes;
            tabControl1.TabPages[3].Text = Resources.TemperatureImpact;
            tabControl1.TabPages[4].Text = Resources.WeighImpact;
            label1.Text = Resources.PaceRunNotification;
            label2.Text = Resources.TemperatureNotification;
            weightLabel2.Text = String.Format(Resources.WeightNotification,Settings.present(Length.Convert(1,Length.Units.Kilometer,Plugin.GetApplication().SystemPreferences.DistanceUnits),3)+" "+Settings.DistanceUnit);
            SizeChanged += new EventHandler(TrainingView_SizeChanged);            
            setSize();
            Plugin.GetApplication().Logbook.DataChanged += new ZoneFiveSoftware.Common.Data.NotifyDataChangedEventHandler(Logbook_DataChanged);
            Plugin.GetApplication().Logbook.Athlete.DataChanged += new ZoneFiveSoftware.Common.Data.NotifyDataChangedEventHandler(Athlete_DataChanged);
            Plugin.GetApplication().SystemPreferences.PropertyChanged += new PropertyChangedEventHandler(SystemPreferences_PropertyChanged);
        }

        private void SystemPreferences_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            setPages();
        }

        private void Athlete_DataChanged(object sender, ZoneFiveSoftware.Common.Data.NotifyDataChangedEventArgs e)
        {
            setPages();
        }

        private void Logbook_DataChanged(object sender, ZoneFiveSoftware.Common.Data.NotifyDataChangedEventArgs e)
        {
            setPages();
        }

        private void TrainingView_SizeChanged(object sender, EventArgs e)
        {
            setSize();
        }

        private void sizeColumnsAndGrid(DataGridView dataGrid, double columnCount)
        {
            int newDataGridWidth = Size.Width;
            int newDataGridHeight = Size.Height - dataGrid.Location.Y-25;
            int columnWidth;
            columnWidth = (int)Math.Floor(newDataGridWidth/columnCount);
            foreach (DataGridViewColumn column in dataGrid.Columns)
            {
                column.Width = columnWidth;
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
            int height = dataGrid.ColumnHeadersHeight;
            if (dataGrid.Rows.Count > 0)
            {
                height += dataGrid.Rows[0].Height * dataGrid.Rows.Count;
                if (dataGrid == weightGrid)
                {
                    height += 10;
                }
            }
            if (height > newDataGridHeight)
            {
                height = newDataGridHeight;
            }
            dataGrid.Size = new Size(Size.Width-15,
                    height);           
        }

        private void setSize()
        {
            tabControl1.Size = Size;
            sizeColumnsAndGrid(trainingGrid, 4);
            sizeColumnsAndGrid(paceTempoGrid, 2);
            sizeColumnsAndGrid(intervalGrid, 4);
            sizeColumnsAndGrid(temperatureGrid, 3);
            sizeColumnsAndGrid(weightGrid, 4);
            if (temperatureGrid.Rows.Count > 0)
            {
                temperatureGrid.Rows[0].DefaultCellStyle.ForeColor = Color.OrangeRed;
            }
        }

        public void setPages()
        {
            if (activity != null && predictor != null)
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
            double weight = Plugin.GetApplication().Logbook.Athlete.InfoEntries.LastEntryAsOfDate(activity.StartTime).WeightKilograms;
            if (weight.Equals(double.NaN))
            {
                weightLabel.Text = Resources.SetWeight;
                weightLabel2.Visible = false;
                weightGrid.Visible = false;
                return;
            }
            weightLabel2.Visible = true;
            weightGrid.Visible = true;
            ActivityInfo info = ActivityInfoCache.Instance.GetInfo(activity);
            weightLabel.Text =String.Format(Resources.ProjectedWeightImpact,
                Length.Convert(info.DistanceMetersMoving,Length.Units.Meter,Plugin.GetApplication().SystemPreferences.DistanceUnits),
                Settings.DistanceUnitShort);
            TimeSpan time = info.Time;
            TimeSpan pace = new TimeSpan(0, 0, (int)Math.Round(time.TotalSeconds
                / Length.Convert(info.DistanceMetersMoving, Length.Units.Meter, Plugin.GetApplication().SystemPreferences.DistanceUnits))); 
            DataTable set = new DataTable();
            set.Columns.Add(Resources.ProjectedWeight);
            set.Columns.Add(Resources.AdjustedVDOT);
            set.Columns.Add(Resources.EstimatedTime);
            if (Settings.ShowPace)
            {
                set.Columns.Add(String.Format(Resources.EstimatedPace,Settings.DistanceUnitShort));
            }
            else
            {
                set.Columns.Add(String.Format(Resources.EstimatedSpeed,Settings.DistanceUnitShort),typeof(double));
            }            
            double inc = 1.4;
            double vdot = getVdot(activity);
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
            double projVdot = weight * vdot / projWeight;
            time = scalePace(time, Math.Pow(vdot / projVdot, 0.83));
            TimeSpan pace = new TimeSpan(0, 0, (int)Math.Round(time.TotalSeconds
                / Length.Convert(info.DistanceMetersMoving, Length.Units.Meter, Plugin.GetApplication().SystemPreferences.DistanceUnits)));
            if (Settings.ShowPace)
            {
                return new object[]{
                    Settings.present(projWeight, 1),
                    Settings.present(projVdot, 1),
                    Settings.present(time),
                    Settings.present(pace)
                };
            }
            else
            {
                return new object[]{
                    Settings.present(projWeight, 1),
                    Settings.present(projVdot, 1),
                    Settings.present(time),
                    Settings.present(1/pace.TotalHours, 2)
                };
            }
        }

        private void setTemperature()
        {
            ActivityInfo info = ActivityInfoCache.Instance.GetInfo(activity);
            TimeSpan time = info.Time;
            temperatureLabel.Text = String.Format(Resources.ProjectedTemperatureImpact,
                Length.Convert(info.DistanceMetersMoving,Length.Units.Meter,Plugin.GetApplication().SystemPreferences.DistanceUnits),
                Settings.DistanceUnitShort);
            TimeSpan pace = new TimeSpan(0, 0, (int)Math.Round(time.TotalSeconds
                / Length.Convert(info.DistanceMetersMoving, Length.Units.Meter, Plugin.GetApplication().SystemPreferences.DistanceUnits)));
            DataTable set = new DataTable();
            set.Columns.Add(Resources.Temperature);
            set.Columns.Add(Resources.AdjustedTime);
            if (Settings.ShowPace)
            {
                set.Columns.Add(String.Format(Resources.AdjustedPace,Settings.DistanceUnitShort));
            }
            else
            {
                string s1 = Resources.AdjustedSpeed;
                string s2 = Settings.DistanceUnitShort;
                string s3 = string.Format("Bijgestelde snelheid ({0}/u)", "km");
                set.Columns.Add(String.Format(Resources.AdjustedSpeed,Settings.DistanceUnitShort), typeof(double));
            }
            set.Rows.Add(getTemperatureRow("16° C", time, pace, 1));
            set.Rows.Add(getTemperatureRow("18° C", time, pace, 1.0075));
            set.Rows.Add(getTemperatureRow("21° C", time, pace, 1.015));
            set.Rows.Add(getTemperatureRow("24° C", time, pace, 1.0225));
            set.Rows.Add(getTemperatureRow("27° C", time, pace, 1.03));
            set.Rows.Add(getTemperatureRow("29° C", time, pace, 1.0375));
            set.Rows.Add(getTemperatureRow("32° C", time, pace, 1.045));
            set.Rows.Add(getTemperatureRow("35° C", time, pace, 1.0525));
            set.Rows.Add(getTemperatureRow("38° C", time, pace, 1.06));
            temperatureGrid.DataSource = set;
            temperatureGrid.Rows[0].DefaultCellStyle.ForeColor = Color.Gray;
        }

        private object[] getTemperatureRow(string p, TimeSpan time, TimeSpan pace, double f)
        {
            time = scalePace(time, f);
            pace = scalePace(pace, f);
            if (Settings.ShowPace)
            {
                return new object[]{
                    p,
                    Settings.present(time),
                    Settings.present(pace)
                };
            }
            else
            {
                return new object[]{
                    p,
                    Settings.present(time),
                    Settings.present(1/pace.TotalHours, 2)
                };
            }
        }

        private void setInterval()
        {
            DataTable set = new DataTable();
            set.Columns.Add(Resources.DistanceMeters, typeof(double));
            set.Columns.Add(Resources.MilePace.Substring(0, Resources.MilePace.IndexOf('/')) + ")");
            set.Columns.Add(Resources.k5Pace.Substring(0, Resources.k5Pace.IndexOf('/')) + ")");
            set.Columns.Add(Resources.k10Pace.Substring(0, Resources.k10Pace.IndexOf('/')) + ")");            
            ActivityInfo info = ActivityInfoCache.Instance.GetInfo(activity);
            double distance = info.DistanceMetersMoving;
            double seconds = info.Time.TotalSeconds;
            TimeSpan milePace = getTrainingPace(1609.344, distance, seconds);
            TimeSpan k5Pace = getTrainingPace(5000, distance, seconds);
            TimeSpan k10Pace = getTrainingPace(10000, distance, seconds);
            set.Rows.Add(getIntervalRow(100, milePace, k5Pace, k10Pace));
            set.Rows.Add(getIntervalRow(200, milePace, k5Pace, k10Pace));
            set.Rows.Add(getIntervalRow(300, milePace, k5Pace, k10Pace));
            set.Rows.Add(getIntervalRow(400, milePace, k5Pace, k10Pace));
            set.Rows.Add(getIntervalRow(600, milePace, k5Pace, k10Pace));
            set.Rows.Add(getIntervalRow(1000, milePace, k5Pace, k10Pace));
            set.Rows.Add(getIntervalRow(1200, milePace, k5Pace, k10Pace));
            set.Rows.Add(getIntervalRow(1609.344,milePace, k5Pace, k10Pace));
            intervalGrid.DataSource = set;
        }

        private object[] getIntervalRow(double p, TimeSpan milePace, 
            TimeSpan k5Pace, TimeSpan k10Pace)
        {
            milePace = scalePace(milePace, p / 1000.0);
            k5Pace = scalePace(k5Pace, p / 1000.0);
            k10Pace = scalePace(k10Pace, p / 1000.0);
            return new object[] {
                    p,
                    Settings.present(milePace),
                    Settings.present(k5Pace),
                    Settings.present(k10Pace)};
        }

        private void setPaceTempo()
        {
            paceTempoLabel.Text = String.Format(Resources.PaceForTempoRuns_label, getVdot(activity));
            DataTable set = new DataTable();
            set.Columns.Add(Resources.Duration);
            if (Settings.ShowPace)
            {
                set.Columns.Add(String.Format(Resources.Pace,Settings.DistanceUnitShort));
            }
            else
            {
                set.Columns.Add(String.Format(Resources.Speed,Settings.DistanceUnitShort), typeof(double));
            }
            string[] durations = new string[] { "20 " + Resources.ShortLowerCaseMinutes, "25 " + Resources.ShortLowerCaseMinutes,
                "30 "+Resources.ShortLowerCaseMinutes, "35 "+Resources.ShortLowerCaseMinutes, 
                "40 "+Resources.ShortLowerCaseMinutes, "45 "+Resources.ShortLowerCaseMinutes, 
                "50 "+Resources.ShortLowerCaseMinutes, "55 "+Resources.ShortLowerCaseMinutes, 
                "60 "+Resources.ShortLowerCaseMinutes };
            double vdot = getVdot(activity);
            TimeSpan pace = getTrainingPace(vdot,0.93);
            TimeSpan[] paces = new TimeSpan[] {
                pace,
                scalePace(pace,1.012),
                scalePace(pace,1.022),
                scalePace(pace,1.027),
                scalePace(pace,1.033),
                scalePace(pace,1.038),
                scalePace(pace,1.043),
                scalePace(pace,1.04866),
                scalePace(pace,1.055)};
            for (int i = 0; i < durations.Length; i++)
            {
                DataRow row = set.NewRow();
                row[0] = durations[i];
                if (Settings.ShowPace)
                {
                    row[1] = Settings.present(paces[i]);
                }
                else
                {
                    row[1] = Settings.present(1 / paces[i].TotalHours, 2);
                }
                set.Rows.Add(row);
            }
            paceTempoGrid.DataSource = set;
        }

        private TimeSpan scalePace(TimeSpan pace, double p)
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
            set.Columns.Add(Resources.PercentMaxHR, typeof(double));
            set.Columns.Add(Resources.TrainRaceHR, typeof(double));
            if (Settings.ShowPace)
            {
                set.Columns.Add(String.Format(Resources.Pace,Settings.DistanceUnitShort));
            }
            else
            {
                set.Columns.Add(String.Format(Resources.Speed,Settings.DistanceUnitShort), typeof(double));
            }
            //addTrainingRow(set, percent);
            double vo2max = getVo2max(activity);
            double vdot = getVdot(activity);
            IList<String> zones = getZones();
            IList<double> percentages = getPercentages(vdot);
            IList<double> hrs = getHeartRates(percentages);
            IList<TimeSpan> paces = getPaces(vdot, percentages);
            for (int i = 0; i < 15; i++)
            {
                DataRow row = set.NewRow();
                row[0] = zones[i];
                row[1] = Settings.present(100*percentages[i],1);
                row[2] = Settings.present(hrs[i],1);
                if (Settings.ShowPace)
                {
                    row[3] = Settings.present(paces[i]);
                }
                else
                {
                    row[3] = Settings.present(1 / paces[i].TotalHours, 2);
                }
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
            result[6] = Resources.Marathon;
            result[7] = "1/2 "+Resources.Marathon;
            result[8] = "15"+Resources.ShortLowerCaseKilometer;
            result[9] = "12" + Resources.ShortLowerCaseKilometer;
            result[10] = "10" + Resources.ShortLowerCaseKilometer;
            result[11] = "8" + Resources.ShortLowerCaseKilometer;
            result[12] = "5" + Resources.ShortLowerCaseKilometer;
            result[13] = "3" + Resources.ShortLowerCaseKilometer;
            result[14] = Resources.Mile;
            return result;
        }

        private IList<TimeSpan> getPaces(double vdot, IList<double> percentages)
        {
            ActivityInfo info = ActivityInfoCache.Instance.GetInfo(activity);
            double seconds = info.Time.TotalSeconds;
            double distance = info.DistanceMetersMoving;
            TimeSpan[] result = new TimeSpan[15];
            result[0] = getTrainingPace(vdot,percentages[0]);
            result[1] = getTrainingPace(vdot, percentages[1]);
            result[2] = getTrainingPace(vdot, percentages[2]);
            result[3] = getTrainingPace(vdot, percentages[3]);
            result[6] = getTrainingPace(42195, distance, seconds); 
            result[4] = result[3].Add(new TimeSpan(0, 0,
                (int)Math.Round((result[6].TotalSeconds - result[3].TotalSeconds) / 3.0)));
            result[5] = result[3].Add(new TimeSpan(0,0,
                (int)Math.Round((result[6].TotalSeconds - result[3].TotalSeconds) / 6.0)));
            result[7] = getTrainingPace(21097.5, distance, seconds);
            result[8] = getTrainingPace(15000, distance, seconds);
            result[9] = getTrainingPace(12000, distance, seconds);
            result[10] = getTrainingPace(10000, distance, seconds);
            result[11] = getTrainingPace(8000, distance, seconds);
            result[12] = getTrainingPace(5000, distance, seconds);
            result[13] = getTrainingPace(3000, distance, seconds);
            result[14] = getTrainingPace(1609.344, distance, seconds); ;
            return result;
        }

        private TimeSpan getTrainingPace(double new_dist, double old_dist, double old_time)
        {
            return new TimeSpan(0, 0, (int)Math.Round(Predictor(new_dist, old_dist, old_time) 
                / Length.Convert(new_dist,Length.Units.Meter,Plugin.GetApplication().SystemPreferences.DistanceUnits)));
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
            result[4] = result[3] + (result[6] - result[3])/3.0;
            result[5] = result[3] + (result[6] - result[3])/6.0;
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

        private TimeSpan getTrainingPace(double vdot,double percent)
        {
            double factor = Length.Convert(1, 
                Plugin.GetApplication().SystemPreferences.DistanceUnits, Length.Units.Meter);
            double days =(1/(29.54+5.000663*(vdot*(percent-0.05))
                -0.007546*Math.Pow(vdot*(percent-0.05),2))) * factor / 1440;
            DateTime time = new DateTime();
            time = time.AddDays(days);
            return time - new DateTime();
        }

        private double getVo2max(IActivity activity)
        {
            ActivityInfo info = ActivityInfoCache.Instance.GetInfo(activity);
            return 0.8 + 0.1894393 * Math.Exp(-0.012778 * info.Time.TotalMinutes)
                + 0.2989558 * Math.Exp(-0.1932605 * info.Time.TotalMinutes);
        }

        private double getVdot(IActivity activity)
        {
            ActivityInfo info = ActivityInfoCache.Instance.GetInfo(activity);
            return (-4.6 + 0.182258 * (info.DistanceMetersMoving / info.Time.TotalMinutes)
                + 0.000104 * Math.Pow(info.DistanceMetersMoving / info.Time.TotalMinutes, 2)) 
                / getVo2max(activity);
        }
    }
}
