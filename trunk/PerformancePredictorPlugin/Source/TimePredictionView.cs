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
    public partial class TimePredictionView : UserControl
    {
#if ST_2_1
        private const object m_DetailPage = null;
#else
        //private IDetailPage m_DetailPage = null;
        private IDailyActivityView m_view = null;
        private TrailPointsLayer m_layer = null;
#endif
        private PerformancePredictorControl m_ppcontrol = null;
        private bool m_showPage = false;

        private class PredictorData
        {
            public bool isData;
            public IList<TimePredictionResult> result;
            public ChartDataSeries series;
        }
        private IDictionary<PredictionModel, PredictorData> m_predictorData = new Dictionary<PredictionModel, PredictorData>();

        public TimePredictionView()
        {
            InitializeComponent();

            chart.YAxis.Formatter = new Formatter.SecondsToTime();
            chart.XAxis.Formatter = new Formatter.General(UnitUtil.Distance.DefaultDecimalPrecision);
        }

        public void InitControls(IDetailPage detailPage, IDailyActivityView view, TrailPointsLayer layer, PerformancePredictorControl ppControl)
        {
#if !ST_2_1
            //m_DetailPage = detailPage;
            m_view = view;
            m_layer = layer;
#endif
            m_ppcontrol = ppControl;

            copyTableMenuItem.Image = ZoneFiveSoftware.Common.Visuals.CommonResources.Images.DocumentCopy16;
            summaryList.NumHeaderRows = TreeList.HeaderRows.Two;
            summaryList.LabelProvider = new ResultLabelProvider();
        }

        public void ThemeChanged(ITheme visualTheme)
        {
            //m_visualTheme = visualTheme;
            summaryList.ThemeChanged(visualTheme);
            this.chart.ThemeChanged(visualTheme);
        }

        public void UICultureChanged(System.Globalization.CultureInfo culture)
        {
            chart.XAxis.Label = UnitUtil.Distance.LabelAxis;
            chart.YAxis.Label = UnitUtil.Time.LabelAxis;
            lblHighScoreRequired.Text = Properties.Resources.HighScoreRequired;
            copyTableMenuItem.Text = ZoneFiveSoftware.Common.Visuals.CommonResources.Text.ActionCopy;
        }

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

        public void RefreshData()
        {
            if (m_showPage)
            {
                //Reset settings
                foreach (PredictorData p in m_predictorData.Values)
                {
                    p.isData = false;
                    p.result.Clear();
                    p.series.Points.Clear();
                }

                setData();
            }
        }

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

            //Get the (cached?) list/chart
            makeData(Settings.Model);
            if (chart != null)
            {
                chart.DataSeries.Clear();
                chart.DataSeries.Add(m_predictorData[Settings.Model].series);
                chart.AutozoomToData(true);
            }
            m_showPage = showPage;
            updateChartVisibility();
        }

        private void RefreshColumns(bool isHighScore)
        {
            summaryList.Columns.Clear();
            IList<string> cols;
            if (isHighScore)
            {
                cols = ResultColumnIds.TimePredictHsColumns;
            }
            else
            {
                cols = ResultColumnIds.TimePredictSingleColumns;
            }
            foreach (string id in cols)
            {
                foreach (IListColumnDefinition columnDef in ResultColumnIds.ColumnDefs())
                {
                    if (columnDef.Id == id)
                    {
                        TreeList.Column column = new TreeList.Column(
                            columnDef.Id,
                            columnDef.Text(columnDef.Id),
                            columnDef.Width,
                            columnDef.Align
                        );
                        summaryList.Columns.Add(column);
                        break;
                    }
                }
            }
            //summaryList.NumLockedColumns = Data.Settings.ActivityPageNumFixedColumns;
        }

        private void makeData(PredictionModel model)
        {
            if (!m_predictorData.ContainsKey(model))
            {
                PredictorData p = new PredictorData();
                p.result = new List<TimePredictionResult>();
                p.series = new ChartDataSeries(chart, chart.YAxis);
                m_predictorData.Add(model, p);
            }
            if (!m_predictorData[model].isData)
            {
                m_predictorData[model].isData = true;
                if (m_ppcontrol.Activities.Count > 1 ||
                    (m_ppcontrol.Activities.Count == 1 && m_ppcontrol.ChkHighScore))
                {
                    //Predict using one or many activities (check done that HS enabled prior)
                    makeData(m_predictorData[model].series, m_predictorData[model].result, Predict.Predictor(model));
                }
                else if (m_ppcontrol.SingleActivity != null)
                {
                    //Predict and training info
                    ActivityInfo info = ActivityInfoCache.Instance.GetInfo(m_ppcontrol.SingleActivity);

                    if (info.DistanceMeters > 0 && info.Time.TotalSeconds > 0)
                    {
                        makeData(m_predictorData[model].series, m_predictorData[model].result, Predict.Predictor(model),
                            info.DistanceMeters, info.Time.TotalSeconds);
                    }
                }
                //else: no activity selected
            }
            summaryList.RowData = m_predictorData[model].result;
        }

        private void makeData(ChartDataSeries series, IList<TimePredictionResult> reslist,
            Predict.PredictTime predict)
        {
            RefreshColumns(true);
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
                progressBar.Visible = true;
                progressBar.BringToFront();
                progressBar.Size = new Size(this.Width, progressBar.Height);
                progressBar.Minimum = 0;
                progressBar.Maximum = m_ppcontrol.Activities.Count;
                results = (IList<IList<Object>>)
                    Settings.HighScore.GetMethod("getFastestTimesOfDistances").Invoke(null,
                    new object[] { m_ppcontrol.Activities, partialDistances, progressBar });
                progressBar.Visible = false;
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
                Length.Units unitNominal;
                if (Settings.Distances[length].Values[0])
                {
                    unitNominal = UnitUtil.Distance.Unit;
                }
                else
                {
                    unitNominal = Settings.Distances[length].Keys[0];
                }
                double speed = new_dist / new_time;
                reslist.Add(new TimePredictionResult(foundActivity, new_dist, length, unitNominal, new_time, old_dist, old_time, meterStart, timeStart));
                index++;
            }
            for (int i = index; i < Settings.Distances.Count; i++)
            {
                double new_dist = Settings.Distances.Keys[i];
                double length = new_dist;
                Length.Units unitNominal;
                if (Settings.Distances[length].Values[0])
                {
                    unitNominal = UnitUtil.Distance.Unit;
                }
                else
                {
                    unitNominal = Settings.Distances[length].Keys[0];
                }
                reslist.Add(new TimePredictionResult(new_dist, length, unitNominal));
            }
        }

        private void makeData(ChartDataSeries series, IList<TimePredictionResult> reslist,
            Predict.PredictTime predict, double old_dist, double old_time)
        {
            RefreshColumns(false);
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
                    Length.Units unitNominal;
                    if (Settings.Distances[length].Values[0])
                    {
                        unitNominal = UnitUtil.Distance.Unit;
                    }
                    else
                    {
                        unitNominal = Settings.Distances[length].Keys[0];
                    }
                    double speed = new_dist / new_time;
                    //row[ActivityIdColumn] = "";
                    reslist.Add(new TimePredictionResult(m_ppcontrol.SingleActivity, new_dist, length, unitNominal, new_time));
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
                    chart.Visible = false;
                    summaryList.Visible = true;
                }
                else
                {
                    lblHighScoreRequired.Visible = false;
                    summaryList.Visible = !Settings.ShowChart;
                    chart.Visible = false;
                }
            }
        }
        /**************************************************/

        void summaryList_Click(object sender, System.EventArgs e)
        {
            //SelectTrack, for ST3
            if (sender is TreeList)
            {
                TreeList l = sender as TreeList;
                TreeList.RowHitState hit;
                object row;
                //Note: As ST scrolls before Location is recorded, incorrect row may be selected...
                row = summaryList.RowHitTest(((MouseEventArgs)e).Location, out hit);
                if (row != null && hit == TreeList.RowHitState.Row && row is TimePredictionResult)
                {
                    TimePredictionResult result = (TimePredictionResult)row;
                    IActivity id = result.Activity;
                    if (id != null && result.UsedTime > 0)
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
                        IValueRangeSeries<DateTime> t = new ValueRangeSeries<DateTime>();
                        t.Add(new ValueRange<DateTime>(result.StartUsedTime, result.StartUsedTime.AddSeconds(result.UsedTime)));
                        IList<TrailResultMarked> aTrm = new List<TrailResultMarked>();
                        aTrm.Add(new TrailResultMarked(
                            new TrailResult(new ActivityWrapper(id, Plugin.GetApplication().SystemPreferences.RouteSettings.RouteSelectedColor)),
                            t));
                        this.MarkTrack(aTrm);
                    }
                }
            }
        }

        private void selectedRow_DoubleClick(object sender, MouseEventArgs e)
        {
            Guid view = GUIDs.DailyActivityView;

            object row;
            TreeList.RowHitState hit;
            row = summaryList.RowHitTest(e.Location, out hit);
            if (row != null  && hit == TreeList.RowHitState.Row && row is TimePredictionResult)
                {
                    TimePredictionResult tr = (TimePredictionResult)row;
                string bookmark = "id=" + tr.Activity;
                Plugin.GetApplication().ShowView(view, bookmark);
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
