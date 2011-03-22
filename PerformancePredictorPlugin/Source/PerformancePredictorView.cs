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
using System.Reflection;

using ZoneFiveSoftware.Common.Data;
using ZoneFiveSoftware.Common.Data.Fitness;
using ZoneFiveSoftware.Common.Visuals;
using ZoneFiveSoftware.Common.Visuals.Chart;
using ZoneFiveSoftware.Common.Visuals.Util;
using ZoneFiveSoftware.Common.Visuals.Fitness;
using ZoneFiveSoftware.Common.Data.Measurement;
using ZoneFiveSoftware.Common.Visuals.Mapping;
using GpsRunningPlugin.Properties;
using GpsRunningPlugin.Util;
using TrailsPlugin;
using TrailsPlugin.Data;
using TrailsPlugin.Utils;
using TrailsPlugin.UI.MapLayers;

namespace GpsRunningPlugin.Source
{
    public partial class PerformancePredictorView : UserControl
    {
#if ST_2_1
        private const object m_DetailPage = null;
#else
        //private IDetailPage m_DetailPage = null;
        private IDailyActivityView m_view = null;
        private TrailPointsLayer m_layer = null;
#endif
        private PerformancePredictorControl m_ppcontrol = null;
        private System.Windows.Forms.ProgressBar m_progressBar;
        private bool m_showPage = false;

        private class PredictorData
        {
            public bool isData;
            public ChartDataSeries series;
            public DataTable set;
        }
        private IDictionary<PredictionModel, PredictorData> m_predictorData = new Dictionary<PredictionModel, PredictorData>()
        {
            { PredictionModel.DAVE_CAMERON, new PredictorData() },
            { PredictionModel.PETE_RIEGEL, new PredictorData() }
        };

        public PerformancePredictorView()
        {
            InitializeComponent();

            setSize();
            chart.YAxis.Formatter = new Formatter.SecondsToTime();
            chart.XAxis.Formatter = new Formatter.General(UnitUtil.Distance.DefaultDecimalPrecision);
        }

        public void InitControls(IDetailPage detailPage, IDailyActivityView view, TrailPointsLayer layer, PerformancePredictorControl ppControl, System.Windows.Forms.ProgressBar progressBar)
        {
#if !ST_2_1
            //m_DetailPage = detailPage;
            m_view = view;
            m_layer = layer;
#endif
            m_ppcontrol = ppControl;
            m_progressBar = progressBar;

            foreach (PredictorData p in m_predictorData.Values)
            {
                p.isData = false;
                p.series = new ChartDataSeries(chart, chart.YAxis);
                p.set = new DataTable();
            }

            dataGrid.CellDoubleClick += new DataGridViewCellEventHandler(selectedRow_DoubleClick);
            dataGrid.CellMouseClick += new DataGridViewCellMouseEventHandler(dataGrid_CellMouseClick); 
            this.dataGrid.EnableHeadersVisualStyles = false;
            this.dataGrid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.dataGrid.RowsDefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.dataGrid.AdvancedColumnHeadersBorderStyle.All = DataGridViewAdvancedCellBorderStyle.Outset;
        }

        public void ThemeChanged(ITheme visualTheme)
        {
            //m_visualTheme = visualTheme;
            this.chart.ThemeChanged(visualTheme);
            //Set color for non ST controls
            this.dataGrid.BackgroundColor = visualTheme.Control;
            this.dataGrid.GridColor = visualTheme.Border;
            this.dataGrid.DefaultCellStyle.BackColor = visualTheme.Window;
            this.dataGrid.ColumnHeadersDefaultCellStyle.BackColor = visualTheme.SubHeader;
        }

        public void UICultureChanged(System.Globalization.CultureInfo culture)
        {
            chart.XAxis.Label = UnitUtil.Distance.LabelAxis;
            chart.YAxis.Label = UnitUtil.Time.LabelAxis;
            lblHighScoreRequired.Text = Properties.Resources.HighScoreRequired;
        }

        //private IList<IActivity> activities = new List<IActivity>();
        //public IList<IActivity> Activities
        //{
        //    get { return activities; }
        //    set
        //    {
        //        bool showPage = m_showPage;
        //        m_showPage = false;
        //        deactivateListeners();

        //        //Make sure activities is not null
        //        if (null == value) { activities.Clear(); }
        //        else { activities = value; }

        //        m_showPage = showPage;
        //        //Reset settings
        //        activateListeners();

        //        RefreshData();
        //    }
        //}

        public bool HidePage()
        {
            m_showPage = false;
            this.Visible = false;
            deactivateListeners();
            if (m_layer != null)
            {
                m_layer.ClearOverlays();
                m_layer.HidePage();
            }
            return true;
        }
        public void ShowPage(string bookmark)
        {
            m_showPage = true;
            this.Visible = true;
            activateListeners();
            RefreshData();
            if (m_layer != null)
            {
                m_layer.ShowPage(bookmark);
            }
        }

        private void activateListeners()
        {
            if (m_showPage)
            {
                Plugin.GetApplication().SystemPreferences.PropertyChanged += new PropertyChangedEventHandler(SystemPreferences_PropertyChanged);
            }
        }

        private void deactivateListeners()
        {
            Plugin.GetApplication().SystemPreferences.PropertyChanged -= new PropertyChangedEventHandler(SystemPreferences_PropertyChanged);
        }

        public void setSize()
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
                        column.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                        //column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    }
                }
            }
        }

        private const string ActivityIdColumn = "ActivityId";

        public void RefreshData()
        {
            if (m_showPage)
            {
                //Reset settings
                foreach (PredictorData p in m_predictorData.Values)
                {
                    p.isData = false;
                    p.series.Points.Clear();
                    p.set.Clear();
                    p.set.Rows.Clear();
                }

                setData();
                setSize();
            }
        }

        //#if ST_2_1
        //        private void dataChanged(object sender, ZoneFiveSoftware.Common.Data.NotifyDataChangedEventArgs e)
        //        {
        //            if (m_showPage)
        //            {
        //                makeData();
        //            }
        //        }
        //#else
        //        private void Activity_PropertyChanged(object sender, PropertyChangedEventArgs e)
        //        {
        //            if (m_showPage)
        //            {
        //                makeData();
        //            }
        //        }
        //#endif
        private void SystemPreferences_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (m_showPage)
            {
                RefreshData();
            }
        }

        public void setData()
        {
            bool showPage = m_showPage;
            m_showPage = false;

            makeData(Settings.Model);
            dataGrid.DataSource = m_predictorData[Settings.Model].set;
            if (chart != null && !chart.IsDisposed)
            {
                chart.DataSeries.Clear();
                chart.DataSeries.Add(m_predictorData[Settings.Model].series);
                chart.AutozoomToData(true);
            }
            m_showPage = showPage;
            updateChartVisibility();
        }

        private void makeData(PredictionModel model)
        {
            if (!m_predictorData[model].isData)
            {
                m_predictorData[model].isData = true;
                if (m_ppcontrol.Activities.Count > 1 ||
                    (m_ppcontrol.Activities.Count == 1 && m_ppcontrol.ChkHighScore))
                {
                    //Predict using one or many activities (check done that HS enabled prior)
                    makeData(m_predictorData[model].set, m_predictorData[model].series, m_ppcontrol.Predictor(model));
                }
                else if (m_ppcontrol.SingleActivity != null)
                {
                    //Predict and training info
                    ActivityInfo info = ActivityInfoCache.Instance.GetInfo(m_ppcontrol.SingleActivity);

                    if (info.DistanceMeters > 0 && info.Time.TotalSeconds > 0)
                    {
                        makeData(m_predictorData[model].set, m_predictorData[model].series, m_ppcontrol.Predictor(model),
                            info.DistanceMeters, info.Time.TotalSeconds);
                    }
                }
                //else: no activity selected
            }
        }

        private void makeData(DataTable set, ChartDataSeries series,
            Predict.PredictTime predict)
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

            IList<IList<Object>> results = new List<IList<Object>>();
            if (Settings.HighScore != null)
            {
                IList<double> partialDistances = new List<double>();
                foreach (double distance in Settings.Distances.Keys)
                {
                    //Scale down the distances, so we get the high scores
                    partialDistances.Add(distance * Settings.PercentOfDistance / 100.0);
                }
                m_progressBar.Visible = true;
                m_progressBar.Minimum = 0;
                m_progressBar.Maximum = m_ppcontrol.Activities.Count;
                results = (IList<IList<Object>>)
                    Settings.HighScore.GetMethod("getFastestTimesOfDistances").Invoke(null,
                    new object[] { m_ppcontrol.Activities, partialDistances, m_progressBar });
                m_progressBar.Visible = false;
            }

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
                row[ActivityIdColumn] = "";
                set.Rows.Add(row);
            }
        }

        private void makeData(DataTable set, ChartDataSeries series,
            Predict.PredictTime predict, double old_dist, double old_time)
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
                //set.Columns.Add(ActivityIdColumn);

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
                    //row[ActivityIdColumn] = "";
                    set.Rows.Add(row);
                }
            }
        }

        public void updateChartVisibility()
        {
            if (m_showPage)
            {
                //Note: Should possibly be checking for data in table/chart
                if (Settings.HighScore == null &&
                    (m_ppcontrol.Activities.Count > 1 || m_ppcontrol.ChkHighScore))
                {
                    lblHighScoreRequired.Visible = true;
                    dataGrid.Visible = false;
                    chart.Visible = false;
                }
                else
                {
                    lblHighScoreRequired.Visible = false;
                    dataGrid.Visible = !Settings.ShowChart;
                    chart.Visible = Settings.ShowChart;
                }
            }
        }
        /**************************************************/

        private void selectedRow_DoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int rowIndex = e.RowIndex;
            if (rowIndex >= 0 && dataGrid.Columns[ActivityIdColumn] != null)
            {
                object id = dataGrid.Rows[rowIndex].Cells[ActivityIdColumn].Value;
                if (id != null)
                {
                    string bookmark = "id=" + id;
                    Plugin.GetApplication().ShowView(GpsRunningPlugin.GUIDs.OpenView, bookmark);
                }
            }
        }

        //Maphandling copy&paste from Overlay/UniqueRoutes
        void dataGrid_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            int rowIndex = e.RowIndex;
            if (rowIndex >= 0 && dataGrid.Columns[ActivityIdColumn] != null)
            {
                string actid = (string)dataGrid.Rows[rowIndex].Cells[ActivityIdColumn].Value;

                if (actid != null && actid != "")
                {
                    IActivity id = null;
                    foreach (IActivity act in m_ppcontrol.Activities)
                    {
                        if (act.ReferenceId == actid)
                        {
                            id = act;
                        }
                    }
                    if (id != null)
                    {
                        if (m_showPage && isSingleView != true)
                        {
                            IDictionary<string, MapPolyline> routes = new Dictionary<string, MapPolyline>();
                            TrailMapPolyline m = new TrailMapPolyline(
                                new TrailResult(new ActivityWrapper(id, Plugin.GetApplication().SystemPreferences.RouteSettings.RouteColor)));
                            routes.Add(m.key, m);
                            if (m_layer != null)
                            {
                                m_layer.TrailRoutes = routes;
                            }
                        }
                        IValueRangeSeries<double> t = new ValueRangeSeries<double>();
                        t.Add(new ValueRange<double>(
                            UnitUtil.Distance.Parse((string)dataGrid.Rows[rowIndex].Cells[7].Value),
                            UnitUtil.Distance.Parse((string)dataGrid.Rows[rowIndex].Cells[7].Value) +
                            UnitUtil.Distance.Parse((string)dataGrid.Rows[rowIndex].Cells[8].Value)));
                        IList<TrailResultMarked> aTrm = new List<TrailResultMarked>();
                        aTrm.Add(new TrailResultMarked(
                            new TrailResult(new ActivityWrapper(id, Plugin.GetApplication().SystemPreferences.RouteSettings.RouteSelectedColor)),
                            t));
                        this.MarkTrack(aTrm);
                    }
                }
            }
        }

        //Some views like mapping is only working in single view - there are likely better tests
        public bool isSingleView
        {
            get
            {
#if !ST_2_1
                if (m_view != null && CollectionUtils.GetSingleItemOfType<IActivity>(m_view.SelectionProvider.SelectedItems) == null)
                {
                    return false;
                }
#endif
                return true;
            }
        }

        public void MarkTrack(IList<TrailResultMarked> atr)
        {
#if !ST_2_1
            if (m_showPage)
            {
                if (m_view != null &&
                    m_view.RouteSelectionProvider != null)
                {
                    //For activities drawn by default, use common marking
                    IList<TrailResultMarked> atr2 = new List<TrailResultMarked>();
                    foreach (TrailResultMarked trm in atr)
                    {
                        if (trm.trailResult.Activity == m_ppcontrol.SingleActivity)
                        {
                            atr2.Add(trm);
                        }
                    }
                    //Only one activity, OK to merge selections on one track
                    TrailsItemTrackSelectionInfo result = TrailResultMarked.SelInfoUnion(atr2);
                    m_view.RouteSelectionProvider.SelectedItems = new IItemTrackSelectionInfo[] { result };
                    if (atr != null && atr.Count > 0)
                    {
                        m_layer.DoZoom(GPS.GetBounds(atr[0].trailResult.GpsPoints(result)));
                    }
                }
                IDictionary<string, MapPolyline> mresult = new Dictionary<string, MapPolyline>();
                foreach (TrailResultMarked trm in atr)
                {
                    foreach (TrailMapPolyline m in TrailMapPolyline.GetTrailMapPolyline(trm.trailResult, trm.selInfo))
                    {
                        if (trm.trailResult.Activity != m_ppcontrol.SingleActivity)
                        {
                            //m.Click += new MouseEventHandler(mapPoly_Click);
                            if (!mresult.ContainsKey(m.key))
                            {
                                mresult.Add(m.key, m);
                            }
                        }
                    }
                }
                m_layer.MarkedTrailRoutes = mresult;
            }
#endif
        }
    }
}
