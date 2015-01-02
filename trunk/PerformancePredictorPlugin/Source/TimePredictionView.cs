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
        }
        private IDictionary<PredictionModel, PredictorData> m_predictorData = new Dictionary<PredictionModel, PredictorData>();

        public TimePredictionView()
        {
            InitializeComponent();
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
            IAxis axis = new RightVerticalAxis(chart);
            chart.YAxisRight.Add(axis);
        }

        public void ThemeChanged(ITheme visualTheme)
        {
            //m_visualTheme = visualTheme;
            summaryList.ThemeChanged(visualTheme);
            this.chart.ThemeChanged(visualTheme);
        }

        public void UICultureChanged(System.Globalization.CultureInfo culture)
        {
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
                }

                setData();
            }
        }

        private void SystemPreferences_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (this.InvokeRequired)
            {
                this.Invoke((PropertyChangedEventHandler)SystemPreferences_PropertyChanged, sender, e);
            }
            else
            {
                if (m_showPage)
                {
                    RefreshData();
                }
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
                chart.YAxis.Formatter = new Formatter.SecondsToTime();
                chart.XAxis.Formatter = new Formatter.General(UnitUtil.Distance.DefaultDecimalPrecision);
                chart.XAxis.Label = UnitUtil.Distance.LabelAxis;
                chart.YAxis.Label = UnitUtil.Time.LabelAxis;

                chart.YAxisRight[0].Label = UnitUtil.PaceOrSpeed.LabelAxis(Settings.ShowPace);
                chart.YAxisRight[0].Formatter = new Formatter.General(UnitUtil.PaceOrSpeed.DefaultDecimalPrecision(Settings.ShowPace));

                chart.DataSeries.Clear();
                ChartDataSeries tseries = new ChartDataSeries(chart, chart.YAxis);
                TimePredictionResultUtil.getTimeSeries(m_predictorData[Settings.Model].result, tseries);
                chart.DataSeries.Add(tseries);

                ChartDataSeries pseries = new ChartDataSeries(chart, chart.YAxisRight[0]);
                TimePredictionResultUtil.getSpeedSeries(m_predictorData[Settings.Model].result, pseries, Settings.ShowPace);
                chart.DataSeries.Add(pseries);

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

        //public IList<TimePredictionResult> getResults()
        //{
        //    PredictionModel model = Settings.Model;
        //    makeData(model);
        //    return m_predictorData[model].result;
        //}

        private void makeData(PredictionModel model)
        {
            if (!m_predictorData.ContainsKey(model))
            {
                PredictorData p = new PredictorData();
                p.result = new List<TimePredictionResult>();
                m_predictorData.Add(model, p);
            }
            if (!m_predictorData[model].isData)
            {
                m_predictorData[model].isData = true;

                if (!m_ppcontrol.IsPartial && (m_ppcontrol.Activities.Count > 1 ||
                    (m_ppcontrol.Activities.Count == 1 && m_ppcontrol.ChkHighScore)))
                {
                    RefreshColumns(true);
                    Predict.SetAgeSexFromActivity(m_ppcontrol.Activities[0]);
                    //Predict using one or many activities (check done that HS enabled prior)
                    //makeData(m_predictorData[model].series, m_predictorData[model].result, Predict.Predictor(model));
                    m_predictorData[model].result = getResults(Predict.Predictor(model), m_ppcontrol.Activities, progressBar);
                }
                else if (m_ppcontrol.IsPartial || m_ppcontrol.SingleActivity != null)
                {
                    RefreshColumns(false);
                    IList<IActivity> activities;
                    if (m_ppcontrol.IsPartial)
                    {
                        activities = m_ppcontrol.Activities;
                    }
                    else
                    {
                        activities = new List<IActivity> { m_ppcontrol.SingleActivity };
                    }

                    if (m_ppcontrol.Distance > 0 && m_ppcontrol.Time.TotalSeconds > 0)
                    {
                        //Predict
                        Predict.SetAgeSexFromActivity(activities[0]);
                        m_predictorData[model].result = getResults(Predict.Predictor(model), activities,
                            //makeData(m_predictorData[model].series, m_predictorData[model].result, Predict.Predictor(model),
                            m_ppcontrol.Distance, m_ppcontrol.Time, null);
                    }
                }
                //else: no activity selected
            }
            summaryList.RowData = m_predictorData[model].result;
        }

        public static IList<TimePredictionResult> getResults(Predict.PredictTime predict, IList<IActivity> activities, System.Windows.Forms.ProgressBar progressBar)
        {
            IList<TimePredictionResult> reslist = new List<TimePredictionResult>();
            IList<IList<Object>> results = new List<IList<Object>>();
            if (Settings.HighScore != null)
            {
                IList<double> partialDistances = new List<double>();
                foreach (double distance in Settings.Distances.Keys)
                {
                    //Scale down the distances, so we get the high scores
                    partialDistances.Add(distance * Settings.PercentOfDistance / 100.0);
                }
                //chart.Visible = false;
                //summaryList.Visible = false;
                //this.lblHighScoreRequired.Visible = false;
                if (progressBar != null)
                {
                    progressBar.Visible = true;
                    progressBar.BringToFront();
                    //progressBar.Size = new Size(this.Width, progressBar.Height);
                    progressBar.Minimum = 0;
                    progressBar.Value = 0;
                    progressBar.Maximum = activities.Count;
                }
                results = (IList<IList<Object>>)
                    Settings.HighScore.GetMethod("getFastestTimesOfDistances").Invoke(null,
                    new object[] { activities, partialDistances, progressBar });
                if (progressBar != null)
                {
                    progressBar.Visible = false;
                }
            }

            int index = 0;
            foreach (IList<Object> result in results)
            {
                IActivity foundActivity = (IActivity)result[0];
                TimeSpan old_time = TimeSpan.FromSeconds(UnitUtil.Time.Parse(result[1].ToString()));
                double meterStart = (double)result[2];
                double old_dist = (double)result[3];
                double timeStart = 0;
                if (result.Count > 4) { timeStart = double.Parse(result[4].ToString()); }
                double meterEnd = meterStart + old_dist;
                double new_dist = old_dist * 100 / Settings.PercentOfDistance;
                double new_time = predict(new_dist, old_dist, old_time);

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
            return reslist;
        }

        public static IList<TimePredictionResult> getResults(Predict.PredictTime predict, IList<IActivity> activities, double old_dist, TimeSpan old_time, System.Windows.Forms.ProgressBar progressBar)
        {
            IList<TimePredictionResult> reslist = new List<TimePredictionResult>();
            foreach (double new_dist in Settings.Distances.Keys)
            {
                double new_time = predict(new_dist, old_dist, old_time);

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
                //row[ActivityIdColumn] = "";
                if (activities != null && activities.Count > 0)
                {
                    reslist.Add(new TimePredictionResult(activities[0], new_dist, length, unitNominal, new_time));
                }
            }
            return reslist;
        }

        public void updateChartVisibility()
        {
            if (m_showPage)
            {
                //Note: Should possibly be checking for data in table/chart
                if (Settings.HighScore == null &&
                    (m_ppcontrol.Activities.Count > 1 /*|| m_ppcontrol.ChkHighScore*/))
                {
                    lblHighScoreRequired.Visible = true;
                    chart.Visible = false;
                    summaryList.Visible = false;
                }
                else
                {
                    lblHighScoreRequired.Visible = false;
                    summaryList.Visible = !Settings.ShowChart;
                    chart.Visible = Settings.ShowChart;
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
                    if (id != null)
                    {
                        if (m_showPage)
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
                        DateTime endTime;
                        if (result.UsedTime.TotalSeconds > 0)
                        {
                            endTime = result.StartUsedTime.Add(result.UsedTime);
                        }
                        else
                        {
                            endTime = ActivityInfoCache.Instance.GetInfo(id).ActualTrackEnd;
                        }
                        t.Add(new ValueRange<DateTime>(result.StartUsedTime, endTime));
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

        //Try to find if ST is mapping a certain activity
        public bool ViewSingleActivity(IActivity activity)
        {
            return activity == CollectionUtils.GetSingleItemOfType<IActivity>(m_view.SelectionProvider.SelectedItems);
        }

        //Adapted from Trails, ActivityDetailPageControl
        public void MarkTrack(IList<TrailResultMarked> atr)
        {
#if !ST_2_1
            if (m_showPage)
            {
                IList<TrailResultMarked> atrST = new List<TrailResultMarked>();
                IDictionary<string, MapPolyline> mresult = new Dictionary<string, MapPolyline>();
                foreach (TrailResultMarked trm in atr)
                {
                    if (m_view != null &&
                      //m_view.RouteSelectionProvider != null &&
                      //trm.trailResult.Activity == m_ppcontrol.SingleActivity)
                      ViewSingleActivity(trm.trailResult.Activity))
                    {
                        //Use ST standard display of track where possible
                        atrST.Add(trm);
                    }
                    else
                    {
                        //Trails internal display of tracks
                        foreach (TrailMapPolyline m in TrailMapPolyline.GetTrailMapPolyline(trm.trailResult, trm.selInfo))
                        {
                            if (!mresult.ContainsKey(m.key))
                            {
                                //m.Click += new MouseEventHandler(mapPoly_Click);
                                mresult.Add(m.key, m);
                            }
                        }
                    }
                }
                //Trails track display update
                m_layer.MarkedTrailRoutes = mresult;

                //ST internal marking, use common marking
                if (atrST.Count > 0)
                {
                    //Only one activity, OK to merge selections on one track
                    TrailsItemTrackSelectionInfo result = TrailResultMarked.SelInfoUnion(atrST);
                    m_view.RouteSelectionProvider.SelectedItems = TrailsItemTrackSelectionInfo.SetAndAdjustFromSelection(new IItemTrackSelectionInfo[] { result }, null, false);
                }

                //Zoom
                if (atr != null && atr.Count > 0)
                {
                    //It does not matter what layer is zoomed here
                    m_layer.DoZoom(GPS.GetBounds(atr[0].trailResult.GpsPoints(TrailResultMarked.SelInfoUnion(atr))));
                }
            }
#endif
        }
    }
}
