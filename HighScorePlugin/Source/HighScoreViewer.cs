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
        private readonly bool showDialog = false;
        private GoalParameter domain, image;
        private bool upperBound;
        private IDictionary<GoalParameter, IDictionary<GoalParameter, IDictionary<bool, IList<Result>>>> cachedResults;
        private String speedUnit;
#if ST_2_1
        //private object m_DetailPage = null;
#else
        private IDetailPage m_DetailPage = null;
        private IDailyActivityView m_view = null;
        private TrailPointsLayer m_layer = null;
        private IList<IActivity> m_activities = new List<IActivity>();
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
        }
        //popup dialog
        public HighScoreViewer(IDailyActivityView view)
            : this(true)
        {
            m_view = view;
            m_layer = TrailPointsLayer.Instance((IView)view);
        }
        public HighScoreViewer(IActivityReportsView view)
            : this(true)
        {
            m_layer = TrailPointsLayer.Instance((IView)view);
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
#endif
        private HighScoreViewer()
        {
            InitializeComponent();
            InitControls();

            domainBox.DropDownStyle = ComboBoxStyle.DropDownList;
            imageBox.DropDownStyle = ComboBoxStyle.DropDownList;
            boundsBox.DropDownStyle = ComboBoxStyle.DropDownList;
            paceBox.DropDownStyle = ComboBoxStyle.DropDownList;
            viewBox.DropDownStyle = ComboBoxStyle.DropDownList;

            domainBox.SelectedItem = Goal.translateToLanguage(Settings.Domain);
            imageBox.SelectedItem = Goal.translateToLanguage(Settings.Image);

            boundsBox.SelectedItem = Settings.UpperBound ? StringResources.Maximal : StringResources.Minimal;
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

            domain = Settings.Domain;
            image = Settings.Image;
            upperBound = Settings.UpperBound;

            domainBox.SelectedIndexChanged += new EventHandler(domainBox_SelectedIndexChanged);
            imageBox.SelectedIndexChanged += new EventHandler(imageBox_SelectedIndexChanged);
            boundsBox.SelectedIndexChanged += new EventHandler(boundsBox_SelectedIndexChanged);
            paceBox.SelectedIndexChanged += new EventHandler(paceBox_SelectedIndexChanged);
            viewBox.SelectedIndexChanged += new EventHandler(viewBox_SelectedIndexChanged);
            minGradeBoxUpdate();
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
            this.showDialog = showDialog;

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
                popupForm.Show();
            }
        }

        void InitControls()
        {
            paceBox.Items.Add(CommonResources.Text.LabelPace);
            paceBox.Items.Add(CommonResources.Text.LabelSpeed);
            viewBox.Items.Add(StringResources.Graph);
            viewBox.Items.Add(ZoneFiveSoftware.Common.Visuals.CommonResources.Text.LabelList);
            boundsBox.Items.Add(StringResources.Minimal);
            boundsBox.Items.Add(StringResources.Maximal);
            domainBox.Items.Add(CommonResources.Text.LabelDistance);
            domainBox.Items.Add(CommonResources.Text.LabelElevation);
            domainBox.Items.Add(CommonResources.Text.LabelTime);
            imageBox.Items.Add(CommonResources.Text.LabelDistance);
            imageBox.Items.Add(CommonResources.Text.LabelElevation);
            imageBox.Items.Add(CommonResources.Text.LabelTime);
            imageBox.Items.Add(StringResources.HRZone);
            imageBox.Items.Add(Resources.HRAndSpeedZones);

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
                resetCachedResults();
                if (m_activities.Count > 0)
                {
                    speedUnit = getMostUsedSpeedUnit(m_activities);
                    paceBox.SelectedItem = speedUnit;
                    showResults();
                }
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
            //RefreshPage();
            //m_visualTheme = visualTheme;
            this.summaryList.ThemeChanged(visualTheme);
            this.chart.ThemeChanged(visualTheme);
            this.minGradeBox.ThemeChanged(visualTheme);

            this.splitContainer1.Panel1.BackColor = visualTheme.Control;
            this.splitContainer1.Panel2.BackColor = visualTheme.Control;
        }

        public void UICultureChanged(System.Globalization.CultureInfo culture)
        {
            Remarks.Text = "";
            label1.Text = StringResources.Find;
            label2.Text = StringResources.PerSpecified;
            label3.Text = ZoneFiveSoftware.Common.Visuals.CommonResources.Text.LabelShow;
            minGradeLbl.Text = " " + ZoneFiveSoftware.Common.Visuals.CommonResources.Text.LabelGrade + ">";
            correctUI(new Control[] { boundsBox, domainBox, label2, imageBox, minGradeLbl, minGradeBox });

            copyTableMenuItem.Text = ZoneFiveSoftware.Common.Visuals.CommonResources.Text.ActionCopy;
            label1.Location = new Point(boundsBox.Location.X - 5 - label1.Width, label1.Location.Y);
            correctUI(new Control[] { paceBox, viewBox, Remarks });
            label3.Location = new Point(paceBox.Location.X - 5 - label3.Width, label3.Location.Y);
            label2.Location = new Point(label2.Location.X, label1.Location.Y);
            minGradeLbl.Location = new Point(minGradeLbl.Location.X, label1.Location.Y);
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
            setSettings();
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

        private void resetCachedResults()
        {
            cachedResults = new Dictionary<GoalParameter, IDictionary<GoalParameter, IDictionary<bool, IList<Result>>>>();
            IList<Goal> goals = new List<Goal>();
            foreach (GoalParameter domain in Enum.GetValues(typeof(GoalParameter)))
            {
                IDictionary<GoalParameter, IDictionary<bool, DataTable>> imageTableCache = new Dictionary<GoalParameter, IDictionary<bool, DataTable>>();
                IDictionary<GoalParameter, IDictionary<bool, IList<Result>>> imageResultCache = new Dictionary<GoalParameter, IDictionary<bool, IList<Result>>>();
                
                foreach (GoalParameter image in Enum.GetValues(typeof(GoalParameter)))
                {
                    IDictionary<bool, DataTable> upperBoundTable = new Dictionary<bool, DataTable>();
                    upperBoundTable.Add(true, null);
                    upperBoundTable.Add(false, null);
                    imageTableCache.Add(image, upperBoundTable);
                    IDictionary<bool, IList<Result>> upperBoundResult = new Dictionary<bool, IList<Result>>();
                    if (image != domain &&
                        (domain == GoalParameter.Distance ||
                         domain == GoalParameter.Time ||
                         domain == GoalParameter.Elevation))
                    {
                        Goal.generateGoals(domain, image, true, goals);
                        Goal.generateGoals(domain, image, false, goals);
                    }
                    upperBoundResult.Add(true, null);
                    upperBoundResult.Add(false, null);
                    imageResultCache.Add(image, upperBoundResult);
                  }
                  cachedResults.Add(domain, imageResultCache);
            }
            
            //Precalculate results, it takes more time to calc the objects than to calc results
            summaryList.Visible = false;
            IList<Result> results = HighScore.calculateInternal(m_activities, goals, progressBar);
            summaryList.Visible = true;
            foreach (GoalParameter domain in Enum.GetValues(typeof(GoalParameter)))
            {
                foreach (GoalParameter image in Enum.GetValues(typeof(GoalParameter)))
                {
                    if (domain != image &&
                        (domain == GoalParameter.Distance ||
                         domain == GoalParameter.Time ||
                         domain == GoalParameter.Elevation))
                    {
                        cachedResults[domain][image][true] = filter(results, goals, domain, image, true);
                        cachedResults[domain][image][false] = filter(results, goals, domain, image, false);
                    }
                }
            }
             
        }

        private IList<Result> filter(IList<Result> results, IList<Goal> goals, 
            GoalParameter domain, GoalParameter image, bool p)
        {
            IList<Result> list = new List<Result>();
            for (int i = 0; i < results.Count; i++)
            {
                if (goals[i].Domain.Equals(domain) && goals[i].Image.Equals(image) &&
                    goals[i].UpperBound == p)
                    list.Add(results[i]);
            }
            return list;
        }

        void paceBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            speedUnit = (String)paceBox.SelectedItem;
            Settings.ShowPace=(String)paceBox.SelectedItem!=CommonResources.Text.LabelSpeed;
            bool includeLocationAndDate = (m_activities.Count > 1);
            RefreshColumns(includeLocationAndDate);
            showResults();
        }

        void imageBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            setSettings();
            showResults();
        }

        void domainBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            setSettings();
            showResults();
        }

        private void boundsBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            setSettings();
            showResults();
        }

        //Requires that the box is left, for instance tab or selecting other control (not just click)
        //MouseLeave or Key entered has other issues
        void minGradeBox_Leave(object sender, System.EventArgs e)
        {
            try
            {
                Settings.MinGrade = Double.Parse(minGradeBox.Text.Replace("%", "")) / 100.0;
            }
            catch (Exception)
            { }
            minGradeBoxUpdate();
            //Cache must be thrown away
            //DateTime t1 = DateTime.Now;
            resetCachedResults();
            //DateTime t2 = DateTime.Now;
            showResults();
            //DateTime t3 = DateTime.Now;
            //copyTableMenuItem.Text = "xxx " + t1 + " " + t2 + " " + t3 + " " + (t2 - t1).TotalSeconds + " " + (t3 - t2).TotalSeconds;
        }

        private void minGradeBoxUpdate()
        {
            minGradeBox.Text = Settings.MinGrade.ToString("0.0 %");
        }

        private void setSettings()
        {
            Settings.Domain = (GoalParameter)Enum.Parse(typeof(GoalParameter), 
                        Goal.translateParameter((String)domainBox.SelectedItem), true);
            Settings.UpperBound = boundsBox.SelectedItem.Equals(StringResources.Maximal);
            Settings.Image = (GoalParameter)Enum.Parse(typeof(GoalParameter), Goal.translateParameter((String)imageBox.SelectedItem), true);
            domain = Settings.Domain;
            image = Settings.Image;
            upperBound = Settings.UpperBound;
            Settings.ShowTable = viewBox.SelectedItem.Equals(ZoneFiveSoftware.Common.Visuals.CommonResources.Text.LabelList);
        }

        private void showResults()
        {
            Remarks.Visible = false;
            summaryList.Visible = false;
            chart.Visible = false;
            if (domain.Equals(image))
            {
                Remarks.Text = Resources.NothingToDisplay;
                Remarks.Visible = true;
                return;
            }
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
            IList<Result> results = cachedResults[domain][image][upperBound];
            if (results == null)
            {
                IList<Goal> goals = Goal.generateSettingsGoals();
                summaryList.Visible = false;
                results = HighScore.calculateInternal(m_activities, goals, progressBar);
                summaryList.Visible = true;
                cachedResults[domain][image][upperBound] = results;
            }
            if (results.Count > 0)
            {
                IList<Result> result2 = new List<Result>();
                progressBar.Visible = false;
                foreach (Result r in results)
                {
                    if (r != null)
                    {
                        result2.Add(r);
                    }
                }
                summaryList.RowData = result2;
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
            IList<Result> results = null;
            if ((image == GoalParameter.Distance ||
               image == GoalParameter.Time ||
               image == GoalParameter.Elevation))
            {
                // Graph can only be calculated for some X-axis
                chart.DataSeries.Clear();
                chart.XAxis.Label = Goal.getGoalParameterLabel(image, speedUnit);
                chart.YAxis.Label = Goal.getGoalParameterLabel(domain, speedUnit);
                results = cachedResults[domain][image][upperBound];
                if (results == null)
                {
                    IList<Goal> goals = Goal.generateSettingsGoals();
                    summaryList.Visible = false;
                    results = HighScore.calculateInternal(m_activities, goals, progressBar);
                    summaryList.Visible = true;
                }
            }
            if (results == null)
            {
                Remarks.Text = Resources.NoResultsForSettings;
                Remarks.Visible = true;
            }
            else
            {
                ChartDataSeries series = new ChartDataSeries(chart, chart.YAxis);
                Goal.setAxisType(chart.XAxis, image);
                Goal.setAxisType(chart.YAxis, domain);
                foreach (Result result in results)
                {
                    if (result != null)
                    {
                        float x = (float)result.getValue(image, speedUnit);
                        float y = (float)result.getValue(domain, speedUnit);
                        if (!x.Equals(float.NaN) && !float.IsInfinity(y) &&
                        series.Points.IndexOfKey(x) == -1)
                        {
                            series.Points.Add(x, new PointF(x, y));
                        }
                    }
                }
                chart.DataSeries.Add(series);
                chart.AutozoomToData(true);
                chart.Visible = true;
            }
        }

        private Goal getGoalFromTable(int rowIndex)
        {
            int index = 0;
            IList<Result> results = cachedResults[domain][image][upperBound];
            foreach (Result result in results)
            {
                if (index == rowIndex && result != null)
                {
                    return result.Goal;
                }
                else if (result != null)
                {
                    index++;
                }
            }
            return null;
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
                    m_view.RouteSelectionProvider.SelectedItems = TrailsItemTrackSelectionInfo.SetAndAdjustFromSelection(new IItemTrackSelectionInfo[] { result }, null, false);
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
