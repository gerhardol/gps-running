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
using System.Collections;

using ZoneFiveSoftware.Common.Data;
using ZoneFiveSoftware.Common.Data.Fitness;
using ZoneFiveSoftware.Common.Visuals;
using ZoneFiveSoftware.Common.Visuals.Fitness;
using ZoneFiveSoftware.Common.Visuals.Chart;
using ZoneFiveSoftware.Common.Visuals.Util;
using ZoneFiveSoftware.Common.Visuals.Mapping;
using GpsRunningPlugin.Properties;
using GpsRunningPlugin.Util;
using TrailsPlugin;
using TrailsPlugin.Data;
using TrailsPlugin.Utils;
using TrailsPlugin.UI.MapLayers;

namespace GpsRunningPlugin.Source
{
    public partial class HighScoreViewer : UserControl
    {
        private readonly Form popupForm;
        private IDictionary<GoalParameter, IDictionary<GoalParameter, IDictionary<bool, IList<Result>>>> cachedResults;
        private String speedUnit;
#if ST_2_1
        //private object m_DetailPage = null;
#else
        private IDetailPage m_DetailPage = null;
        private IDailyActivityView m_view = null;
        private TrailPointsLayer m_layer = null;
        private IList<IActivity> m_activities = new List<IActivity>();
        IList<IValueRangeSeries<DateTime>> m_pauses = null;
        private bool m_showPage = false;

        //Activity page
        public HighScoreViewer(IDetailPage detailPage, IDailyActivityView view)
           : this()
        {
            m_DetailPage = detailPage;
            m_view = view;
            m_layer = TrailPointsLayer.Instance(m_view);
            if (m_DetailPage != null)
            {
                //expandButton.Visible = true;
            }
            if (Settings.ShowTrailsHint)
            {
                String oneTimeMessage = "The Trails plugin can be used as viewer for High Scores. Trails provides many more features, for instance graphs and top lists. Just select the HighScore trail. See the HighScore or Trails documentation for more information. This message will not be shown again.";
                DialogResult r = MessageDialog.Show(oneTimeMessage, "Trails Plugin", MessageBoxButtons.OK, MessageBoxIcon.Information);
                if (r == DialogResult.OK)
                {
                    Settings.ShowTrailsHint = false;
                }
            }
        }
        //popup dialog
        internal HighScoreViewer(IDailyActivityView view)
            : this(true)
        {
            m_view = view;
            m_layer = TrailPointsLayer.Instance((IView)view);
            this.ShowPage("");
        }
        internal HighScoreViewer(IActivityReportsView view)
            : this(true)
        {
            m_layer = TrailPointsLayer.Instance((IView)view);
            this.ShowPage("");
        }

        //UniqueRoutes sendto
        public HighScoreViewer(IList<IActivity> activities, IDailyActivityView view)
            : this(view)
        {
            this.Activities = activities;
        }
        public HighScoreViewer(IList<IActivity> activities, IActivityReportsView view)
            : this(view)
        {
            this.Activities = activities;
        }

        //Trails analyze
        public HighScoreViewer(IList<IActivity> activities, IList<IValueRangeSeries<DateTime>> pauses, IDailyActivityView view, System.Windows.Forms.ProgressBar progressBar)
            : this(view)
        {
            this.progressBar = progressBar;
            this.m_pauses = pauses;
            this.Activities = activities;
        }
#endif
        private HighScoreViewer()
        {
            InitializeComponent();
            InitControls();
            this.hsControl.setViewer(this);

            paceBox.DropDownStyle = ComboBoxStyle.DropDownList;
            viewBox.DropDownStyle = ComboBoxStyle.DropDownList;

            //speedUnit = getMostUsedSpeedUnit(m_activities);
            //paceBox.SelectedItem = speedUnit;
            if (Settings.ShowTable)
            {
                viewBox.SelectedItem = ZoneFiveSoftware.Common.Visuals.CommonResources.Text.LabelList;
            }
            else
            {
                viewBox.SelectedItem = StringResources.Graph;
            }

            paceBox.SelectedIndexChanged += new EventHandler(paceBox_SelectedIndexChanged);
            viewBox.SelectedIndexChanged += new EventHandler(viewBox_SelectedIndexChanged);
        }

        ////Compatibility with old UniqueRoutes send to
        //public HighScoreViewer(IList<IActivity> aAct, bool showDialog, bool dummy)
        //    : this(showDialog)
        //{
        //    this.Activities = aAct;
        //}

        //Create popup
        private HighScoreViewer(bool showDialog)
            : this()
        {
            if (showDialog)
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
                popupForm.Icon = Icon.FromHandle(Properties.Resources.Image_32_HighScore.GetHicon());
                this.Size = new Size(Parent.Size.Width - 17, Parent.Size.Height - 38);
                this.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right | System.Windows.Forms.AnchorStyles.Bottom)));
                popupForm.StartPosition = FormStartPosition.CenterScreen;
                popupForm.FormClosed += new FormClosedEventHandler(popupForm_FormClosed);
                popupForm.Resize += new EventHandler(popupForm_Resize);
                popupForm.Show();
            }
        }

        void InitControls()
        {
            paceBox.Items.Add(CommonResources.Text.LabelPace);
            paceBox.Items.Add(CommonResources.Text.LabelSpeed);
            viewBox.Items.Add(StringResources.Graph);
            viewBox.Items.Add(ZoneFiveSoftware.Common.Visuals.CommonResources.Text.LabelList);

            copyTableMenuItem.Image = ZoneFiveSoftware.Common.Visuals.CommonResources.Images.DocumentCopy16;
            summaryList.LabelProvider = new ResultLabelProvider();
            this.summaryListToolTipTimer.Tick += new System.EventHandler(ToolTipTimer_Tick);
        }

        public IList<IActivity> Activities
        {
            set
            {
                //Make sure activities is not null
                if (null == value) { m_activities.Clear(); }
                else { m_activities = value; }
                if (popupForm != null)
                {
                    if (m_activities.Count > 1)
                        popupForm.Text = Resources.HSV + " " + String.Format(StringResources.OfManyActivities, m_activities.Count);
                    else if (m_activities.Count == 1)
                        popupForm.Text = Resources.HSV + " " + StringResources.OfOneActivity;
                    else
                        popupForm.Text = Resources.HSV + " " + StringResources.OfNoActivities;
                }
                bool includeLocationAndDate = (m_activities.Count > 1);
                RefreshColumns(includeLocationAndDate);

                if (m_activities.Count > 0)
                {
                    speedUnit = getMostUsedSpeedUnit(m_activities);
                    paceBox.SelectedIndexChanged -= new EventHandler(paceBox_SelectedIndexChanged);
                    paceBox.SelectedItem = speedUnit;
                    paceBox.SelectedIndexChanged += new EventHandler(paceBox_SelectedIndexChanged);
                }
                showResults(true);
                if (m_layer != null)
                {
                    m_layer.ClearOverlays();
                }
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

        public void ThemeChanged(ITheme visualTheme)
        {
            this.hsControl.ThemeChanged(visualTheme);
            //RefreshPage();
            //m_visualTheme = visualTheme;
            this.summaryList.ThemeChanged(visualTheme);
            this.chart.ThemeChanged(visualTheme);

            this.splitContainer1.Panel1.BackColor = visualTheme.Control;
            this.splitContainer1.Panel2.BackColor = visualTheme.Control;
        }

        public void UICultureChanged(System.Globalization.CultureInfo culture)
        {
            this.hsControl.UICultureChanged(culture);
            Remarks.Text = "";
            label3.Text = ZoneFiveSoftware.Common.Visuals.CommonResources.Text.LabelShow;
            //correctUI(new Control[] { boundsBox, domainBox, label2, imageBox, minGradeLbl, minGradeBox });
            this.hsControl.Width = this.Width;

            copyTableMenuItem.Text = ZoneFiveSoftware.Common.Visuals.CommonResources.Text.ActionCopy;
            correctUI(new Control[] { paceBox, viewBox, Remarks });
            label3.Location = new Point(paceBox.Location.X - 5 - label3.Width, label3.Location.Y);
        }

        public bool HidePage()
        {
            m_showPage = false;
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
            //makeData();
            activateListeners();
            if (m_layer != null)
            {
                m_layer.ShowPage(bookmark);
            }
        }

        public void RefreshPage()
        {
            if (m_showPage)
            {
                showResults();
            }
        }

        private void activateListeners()
        {
            Plugin.GetApplication().SystemPreferences.PropertyChanged += new PropertyChangedEventHandler(SystemPreferences_PropertyChanged);
        }

        private void deactivateListeners()
        {
            Plugin.GetApplication().SystemPreferences.PropertyChanged -= new PropertyChangedEventHandler(SystemPreferences_PropertyChanged);
        }

        void popupForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.HidePage();
        }

        private void RefreshColumns(bool isMultiple)
        {
            summaryList.Columns.Clear();
            IList<string> cols;
            if (isMultiple)
            {
                cols = ResultColumnIds.LocAndDateColumns;
            }
            else
            {
                cols = ResultColumnIds.DefaultColumns;
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

        /***********************************************************/

        void SystemPreferences_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (this.InvokeRequired)
            {
                this.Invoke((PropertyChangedEventHandler)SystemPreferences_PropertyChanged, sender, e);
            }
            else
            {
                if (m_showPage && (e.PropertyName.Equals("DistanceUnits") || e.PropertyName.Equals("ElevationUnits")))
                {
                    showResults();
                }
            }
        }

        void viewBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            Settings.ShowTable = viewBox.SelectedItem.Equals(ZoneFiveSoftware.Common.Visuals.CommonResources.Text.LabelList);
            showResults();
        }

        public static string getMostUsedSpeedUnit(IList<IActivity> activities)
        {
            int speed = 0, pace = 0;
            foreach (IActivity activity in activities)
            {
                if (activity.Category.SpeedUnits == ZoneFiveSoftware.Common.Data.Measurement.Speed.Units.Pace)
                {
                    pace++;
                }
                else
                {
                    speed++;
                }
            }
            Settings.ShowPace = pace >= speed;
            if (speed > pace)
            {
                return CommonResources.Text.LabelSpeed;
            }
            return CommonResources.Text.LabelPace;
        }

        private IList<Goal> resetCachedResults()
        {
            cachedResults = new Dictionary<GoalParameter, IDictionary<GoalParameter, IDictionary<bool, IList<Result>>>>();
            IList<Goal> goals = Goal.generateAllGoals();
            foreach (Goal goal in goals)
            {
                if (!cachedResults.ContainsKey(goal.Domain))
                {
                    IDictionary<GoalParameter, IDictionary<bool, IList<Result>>> imageResultCache = new Dictionary<GoalParameter, IDictionary<bool, IList<Result>>>();
                    IDictionary<bool, IList<Result>> upperBoundResult = new Dictionary<bool, IList<Result>>();
                    upperBoundResult.Add(goal.UpperBound, null);
                    imageResultCache.Add(goal.Image, upperBoundResult);
                    cachedResults.Add(goal.Domain, imageResultCache);
                }
                if (!cachedResults[goal.Domain].ContainsKey(goal.Image))
                {
                    IDictionary<bool, IList<Result>> upperBoundResult = new Dictionary<bool, IList<Result>>();
                    upperBoundResult.Add(goal.UpperBound, null);
                    cachedResults[goal.Domain].Add(goal.Image, upperBoundResult);
                }
                if (!cachedResults[goal.Domain][goal.Image].ContainsKey(goal.UpperBound))
                {
                    cachedResults[goal.Domain][goal.Image].Add(goal.UpperBound, null);
                }
            }
            return goals;
        }

        private void preCalcCachedResults(IList<Goal> allGoals)
        {
            IList<Goal> goalsToCalc = new List<Goal>();
            //Precalculate results, it may take more time to calc the objects than to calc results
            foreach (Goal goal in allGoals)
            {
                if (Goal.IsZoneGoal(Settings.Image) ||
                    goal.Image == GoalParameter.Time ||
                    goal.Image == GoalParameter.Distance ||
                    goal.Image == GoalParameter.Elevation)
                {
                    goalsToCalc.Add(goal);
                }
            }
            if (goalsToCalc.Count > 0)
            {
                summaryList.Visible = false;
                IList<Result> results = HighScore.calculateActivities(m_activities, m_pauses, goalsToCalc, progressBar);
                foreach (Result r in results)
                {
                    if (cachedResults[r.Goal.Domain][r.Goal.Image][r.Goal.UpperBound] == null)
                    {
                        cachedResults[r.Goal.Domain][r.Goal.Image][r.Goal.UpperBound] = new List<Result>();
                    }
                    cachedResults[r.Goal.Domain][r.Goal.Image][r.Goal.UpperBound].Add(r);
                }
            }
        }

        void paceBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            speedUnit = (String)paceBox.SelectedItem;
            Settings.ShowPace = (String)paceBox.SelectedItem!=CommonResources.Text.LabelSpeed;
            bool includeLocationAndDate = (m_activities.Count > 1);
            RefreshColumns(includeLocationAndDate);
            showResults();
        }

        internal void showResults(bool resetCache)
        {
            if (resetCache)
            {
                IList<Goal> goals = resetCachedResults();
                //no precalc after reset
                //preCalcCachedResults(goals);
            }
            showResults();
        }

        private void showResults()
        {
            Remarks.Visible = false;
            summaryList.Visible = false;
            chart.Visible = false;
            if (Settings.Domain.Equals(Settings.Image))
            {
                Remarks.Text = Resources.NothingToDisplay;
                Remarks.Visible = true;
                return;
            }
            viewBox.SelectedIndexChanged -= new EventHandler(viewBox_SelectedIndexChanged);
            if (Goal.IsZoneGoal(Settings.Image))
            {
                viewBox.Enabled = false;
                viewBox.SelectedItem = ZoneFiveSoftware.Common.Visuals.CommonResources.Text.LabelList;
            }
            else
            {
                viewBox.Enabled = true;
                if (Settings.ShowTable)
                {
                    viewBox.SelectedItem = ZoneFiveSoftware.Common.Visuals.CommonResources.Text.LabelList;
                }
                else
                {
                    viewBox.SelectedItem = StringResources.Graph;
                }
            }
            viewBox.SelectedIndexChanged += new EventHandler(viewBox_SelectedIndexChanged);

            if (viewBox.SelectedItem.Equals(StringResources.Graph))
            {
                showGraph();                
            }
            else
            {
                showTable();                
            }
            showRemarks();            
        }

        private void showRemarks()
        {
            int manualEntered = 0, noStartTime = 0;
            foreach (IActivity activity in m_activities)
            {
                if (null != activity && activity.HasStartTime)
                {
                    if (Settings.IgnoreManualData)
                    {
                        if (activity.UseEnteredData)
                            manualEntered++;
                    }
                }
                else
                {
                    noStartTime++;
                }
            }
            if (manualEntered + noStartTime == 0)
            {
                Remarks.Text = "";
            }
            else
            {
                Remarks.Text = String.Format(Resources.IgnoredActivities, (manualEntered + noStartTime)) + ": ";
                bool first = true;
                if (noStartTime > 0)
                {
                    Remarks.Text += noStartTime + " " + Resources.HadNoStartTime;
                    first = false;
                }
                if (manualEntered > 0)
                {
                    if (!first)
                    {
                        Remarks.Text += " " + StringResources.And + " ";
                    }
                    Remarks.Text += manualEntered + " " + Resources.HadManualEnteredData;
                }
                Remarks.Visible = true;
            }
        }

        private void showTable()
        {
            IList<Result> results = null;
            if (cachedResults.ContainsKey(Settings.Domain) &&
                cachedResults[Settings.Domain].ContainsKey(Settings.Image) &&
                cachedResults[Settings.Domain][Settings.Image].ContainsKey(Settings.UpperBound))
            {
                results = cachedResults[Settings.Domain][Settings.Image][Settings.UpperBound];
            }
            if (results == null)
            {
                IList<Goal> goals = Goal.generateSettingsGoals();
                //Hide if visible
                summaryList.Visible = false;
                results = HighScore.calculateActivities(m_activities, m_pauses, goals, progressBar);
                cachedResults[Settings.Domain][Settings.Image][Settings.UpperBound] = results;
            }
            if (results != null && results.Count > 0)
            {
                if (progressBar != null)
                {
                    progressBar.Visible = false;
                }
                summaryList.RowData = results;
                summaryList.Visible = true;
            }
            else
            {
                Remarks.Text = Resources.NoResultsForSettings;
                Remarks.Visible = true;
            }
        }

        private void showGraph()
        {
            IList<Result> results = cachedResults[Settings.Domain][Settings.Image][Settings.UpperBound];
            if (!Goal.IsZoneGoal(Settings.Image))
            {
                // Graph can only be calculated for some X-axis
                chart.DataSeries.Clear();
                chart.XAxis.Label = Goal.getGoalParameterLabel(Settings.Image, speedUnit);
                chart.YAxis.Label = Goal.getGoalParameterLabel(Settings.Domain, speedUnit);
                if (results == null)
                {
                    IList<Goal> goals = Goal.generateSettingsGoals();
                    results = HighScore.calculateActivities(m_activities, m_pauses, goals, progressBar);
                    cachedResults[Settings.Domain][Settings.Image][Settings.UpperBound] = results;
                }
            }
            else
            {
                //No graph for zones
                results = null;
            }
            if (results == null || results.Count == 0)
            {
                Remarks.Text = Resources.NoResultsForSettings;
                Remarks.Visible = true;
            }
            else
            {
                ChartDataSeries series = new ChartDataSeries(chart, chart.YAxis);
                Goal.setAxisType(chart.XAxis, Settings.Image);
                Goal.setAxisType(chart.YAxis, Settings.Domain);
                foreach (Result result in results)
                {
                    float x = (float)result.getValue(Settings.Image, speedUnit);
                    float y = (float)result.getValue(Settings.Domain, speedUnit);
                    if (!x.Equals(float.NaN) && !float.IsInfinity(y) &&
                    series.Points.IndexOfKey(x) == -1)
                    {
                        series.Points.Add(x, new PointF(x, y));
                    }
                }
                chart.DataSeries.Add(series);
                chart.AutozoomToData(true);
                chart.Visible = true;
            }
        }

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
                if (row != null && hit == TreeList.RowHitState.Row && row is Result)
                {
                    Result result = (Result)row;
                    IActivity id = result.Activity;
                    if (id != null && result.Seconds > 0)
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
                        t.Add(new ValueRange<DateTime>(result.DateStart, result.DateEnd));
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
            if (row != null && hit == TreeList.RowHitState.Row && row is Result)
            {
                Result tr = (Result)row;
                string bookmark = "id=" + tr.Activity;
                Plugin.GetApplication().ShowView(view, bookmark);
            }
        }

        public static void copyTableMenu_Click(object sender, EventArgs e)
        {
            ToolStripDropDownItem d = sender as ToolStripDropDownItem;
            ToolStripMenuItem t = (ToolStripMenuItem)sender;
            ContextMenuStrip s = (ContextMenuStrip)t.Owner;
            TreeList list = (TreeList)s.SourceControl;
            list.CopyTextToClipboard(true, System.Globalization.CultureInfo.CurrentCulture.TextInfo.ListSeparator);
        }

        void popupForm_Resize(object sender, EventArgs e)
        {
            Settings.WindowSize = popupForm.Size;
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
                if (m_layer != null)
                {
                    m_layer.MarkedTrailRoutes = mresult;
                }
                //ST internal marking, use common marking
                if (atrST.Count > 0)
                {
                    //Only one activity, OK to merge selections on one track
                    TrailsItemTrackSelectionInfo result = TrailResultMarked.SelInfoUnion(atrST);
                    m_view.RouteSelectionProvider.SelectedItems = TrailsItemTrackSelectionInfo.SetAndAdjustFromSelectionToST(result);
                }

                //Zoom
                if (atr != null && atr.Count > 0 && this.m_layer != null)
                {
                    //It does not matter what layer is zoomed here
                    m_layer.DoZoom(GPS.GetBounds(atr[0].trailResult.GpsPoints(TrailResultMarked.SelInfoUnion(atr))));
                }
            }
#endif
        }

        // private member variables of the Control - initialization omitted
        ToolTip summaryListToolTip = new ToolTip();
        Timer summaryListToolTipTimer = new Timer();
        bool summaryListTooltipDisabled = false; // is set to true, whenever a tooltip would be annoying, e.g. while a context menu is shown
        Result summaryListLastEntryAtMouseMove = null;
        Point summaryListCursorLocationAtMouseMove;

        private void summaryList_MouseMove(object sender, MouseEventArgs e)
        {
            TreeList.RowHitState rowHitState;
            Result entry = (Result)summaryList.RowHitTest(e.Location, out rowHitState);
            if (entry == summaryListLastEntryAtMouseMove)
                return;
            else
                summaryListToolTip.Hide(summaryList);
            summaryListLastEntryAtMouseMove = entry;
            summaryListCursorLocationAtMouseMove = e.Location;

            if (entry != null)
                summaryListToolTipTimer.Start();
            else
                summaryListToolTipTimer.Stop();
        }

        private void summaryList_MouseLeave(object sender, EventArgs e)
        {
            summaryListToolTipTimer.Stop();
            summaryListToolTip.Hide(summaryList);
        }

        private void ToolTipTimer_Tick(object sender, EventArgs e)
        {
            summaryListToolTipTimer.Stop();

            if (summaryListLastEntryAtMouseMove != null &&
                summaryListCursorLocationAtMouseMove != null &&
                !summaryListTooltipDisabled)
            {
                string tt = StringResources.Goal + ": " + summaryListLastEntryAtMouseMove.Goal.ToString(speedUnit);
                summaryListToolTip.Show(tt,
                              summaryList,
                              new Point(summaryListCursorLocationAtMouseMove.X +
                                  Cursor.Current.Size.Width / 2,
                                        summaryListCursorLocationAtMouseMove.Y),
                              summaryListToolTip.AutoPopDelay);
            }
        }
    }
}
