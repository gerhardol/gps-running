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
using ZoneFiveSoftware.Common.Visuals.Fitness;
using ZoneFiveSoftware.Common.Data.GPS;
#if !ST_2_1
using ZoneFiveSoftware.Common.Data;
using ZoneFiveSoftware.Common.Visuals.Forms;
using ZoneFiveSoftware.Common.Visuals.Util;
using ZoneFiveSoftware.Common.Visuals.Mapping;
#endif
using ZoneFiveSoftware.Common.Data.Fitness;
using GpsRunningPlugin.Source;
using GpsRunningPlugin;
using System.Reflection.Emit;
using System.Reflection;
using System.IO;
using GpsRunningPlugin.Properties;
using GpsRunningPlugin.Util;
using TrailsPlugin.Data;
using TrailsPlugin.UI.MapLayers;

namespace GpsRunningPlugin.Source
{
    public partial class UniqueRoutesActivityDetailView : UserControl
    {
        private ITheme m_visualTheme;
        private System.Globalization.CultureInfo m_culture;
        private IList<IActivity> similar;
        private IDictionary<string, string> similarToolTip;
        private IActivity refActivity = null;
        private IGPSRoute urRoute = new GPSRoute();
        private IList<IActivity> selectedActivities = new List<IActivity>();
        private IDictionary<ToolStripMenuItem, SendToPlugin> aSendToMenu = new Dictionary<ToolStripMenuItem, SendToPlugin>();
        private bool _showPage = false;
        private bool _needsRecalculation = true;
        private bool _allowActivityUpdate = true; //Ignore "automatic" activity update
        private IDictionary<IActivity, IList<PointInfo[]>> m_commonStretches = null;

#if ST_2_1
        private object m_DetailPage = null;
#else
        private IDetailPage m_DetailPage = null;
        private IDailyActivityView m_view = null;
        private TrailPointsLayer m_layer = null;

        public UniqueRoutesActivityDetailView(IDetailPage detailPage, IDailyActivityView view)
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
#endif
        public UniqueRoutesActivityDetailView()
        {
            InitializeComponent();
            InitControls();

            this.Resize += new EventHandler(UniqueRoutesActivityDetailView_Resize);
            activeBox.Visible = true;
            if (Settings.UseActive)
            {
                activeBox.SelectedItem = StringResources.Active;
            }
            else
            {
                activeBox.SelectedItem = Resources.All;
            }
            activeMenuItem.CheckState = Settings.UseActive ? CheckState.Checked : CheckState.Unchecked;
            speedBox.SelectedItem = CommonResources.Text.LabelPace;
            summaryList.ColumnClicked += new TreeList.ColumnEventHandler(summaryView_ColumnHeaderMouseClick);

            Plugin.GetApplication().SystemPreferences.PropertyChanged += new PropertyChangedEventHandler(SystemPreferences_PropertyChanged);
            setSize();

            bool isPluginMatch = false;
            foreach (SendToPlugin p in Settings.aSendToPlugin)
            {
                System.Windows.Forms.ToolStripMenuItem sendMenuItem;
                sendMenuItem = new System.Windows.Forms.ToolStripMenuItem();
                sendMenuItem.Name = p.id;
                sendMenuItem.Size = new System.Drawing.Size(198, 22);
                sendMenuItem.Text = p.name;
                if (p.pType != null)
                {
                    sendMenuItem.Click += new System.EventHandler(this.sendActivityButton_Click);
                    pluginBox.Items.Add(p.name);
                    sendMenuItem.Enabled = true;
                    if (!isPluginMatch)
                    {
                        pluginBox.SelectedItem = Settings.SelectedPlugin;
                    }
                    if (Settings.SelectedPlugin != null &&
                            Settings.SelectedPlugin.Equals(p.name))
                    {
                        //Match in settings, no more checks
                        isPluginMatch = true;
                    }
                }
                else
                {
                    if (p.id == "TRIMP")
                    {
                        sendMenuItem.Visible = false;
                    }
                    sendMenuItem.Enabled = false;
                }
                aSendToMenu[sendMenuItem] = p;
                this.sendToMenuItem.DropDownItems.Add(sendMenuItem);
            }
            this.sendToMenuItem.DropDownItems.Add(this.limitActivityMenuItem);

            if (Settings.SelectAll)
            {
                selectedBox.SelectedItem = Resources.All;
            }
            else
            {
                selectedBox.SelectedItem = StringResources.Selected;
            }
            //pluginBox
            if (!isPluginMatch)
            {
                pluginBox.SelectedItem = "";
                pluginBox.Enabled = false;
            }
        }

        void InitControls()
        {
            this.infoIcon.Image = ZoneFiveSoftware.Common.Visuals.CommonResources.Images.Information16;
            this.boxCategory.ButtonImage = Properties.Resources.DropDown;
            copyTable.Image = ZoneFiveSoftware.Common.Visuals.CommonResources.Images.DocumentCopy16;
#if !ST_2_1
            sendToMenuItem.Image = ZoneFiveSoftware.Common.Visuals.CommonResources.Images.Analyze16;
#endif
            listSettingsMenuItem.Image = ZoneFiveSoftware.Common.Visuals.CommonResources.Images.Table16;
            btnRefresh.CenterImage = ZoneFiveSoftware.Common.Visuals.CommonResources.Images.Refresh16;
#if !ST_2_1
            this.listSettingsMenuItem.Click += new System.EventHandler(this.listSettingsToolStripMenuItem_Click);
#else
            //No listSetting dialog in ST2
            if (this.contextMenu.Items.Contains(this.listSettingsMenuItem))
            {
                this.contextMenu.Items.Remove(this.listSettingsMenuItem);
            }
#endif
            this.summaryListToolTipTimer.Tick += new System.EventHandler(ToolTipTimer_Tick);
            summaryList.LabelProvider = new ActivityLabelProvider();
#if !ST_2_1
            this.summaryList.SelectedItemsChanged += new System.EventHandler(summaryList_SelectedItemsChanged);
#endif
        }

        public void ThemeChanged(ITheme visualTheme)
        {
            m_visualTheme = visualTheme;
            summaryList.ThemeChanged(visualTheme);
            this.splitContainer1.Panel1.BackColor = visualTheme.Control;
            this.splitContainer1.Panel2.BackColor = visualTheme.Control;
            this.infoIcon.BackColor = visualTheme.Control;
            this.boxCategory.ThemeChanged(visualTheme);
        }

        public void UICultureChanged(System.Globalization.CultureInfo culture)
        {
            //Some labels depends on the activity
            //summaryLabel, CommonStretches column, setTable()
            m_culture = culture;

            setCategoryLabel();
            this.categoryLabel.Text = ZoneFiveSoftware.Common.Visuals.CommonResources.Text.LabelCategory + ":";
            //btnChangeCategory.Text = StringResources.ChangeCategory;
            copyTable.Text = ZoneFiveSoftware.Common.Visuals.CommonResources.Text.ActionCopy;
            RefreshColumns();
            this.limitActivityMenuItem.Text = Properties.Resources.UI_Activity_List_LimitSelection;
            this.ctxMenuItemRefActivity.Text = StringResources.SetRefActivity;
            listSettingsMenuItem.Text = StringResources.ListSettings;
#if ST_2_1
            sendToMenuItem.Text = CommonResources.Text.LabelActivity;
#else
            sendToMenuItem.Text = CommonResources.Text.ActionAnalyze;
#endif
            this.activeMenuItem.Text = Resources.ctxActiveLaps;

            //No longer visible
            labelShow.Text = ZoneFiveSoftware.Common.Visuals.CommonResources.Text.LabelShow;
            sendLabel2.Text = Resources.ActivitiesTo;
            selectedBox.Items.Add(Resources.All);
            selectedBox.Items.Add(StringResources.Selected);
            labelLaps.Text = CommonResources.Text.LabelSplits;
            activeBox.Items.Add(Resources.All);
            activeBox.Items.Add(StringResources.Active);
            speedBox.Items.Add(CommonResources.Text.LabelPace);
            speedBox.Items.Add(CommonResources.Text.LabelSpeed);
            btnRefresh.Text = "";
            //correctUI(new Control[] { selectedBox, sendLabel2, pluginBox, btnDoIt });
            //sendResultToLabel1.Text = StringResources.Send;
            //sendResultToLabel1.Location = new Point(selectedBox.Location.X - 5 - sendResultToLabel1.Size.Width,
            //                            sendLabel2.Location.Y);
            //labelShow.Location = new Point(activeBox.Location.X - 5 - labelShow.Size.Width,
            //                        labelShow.Location.Y);
            this.boxCategory.Location = new Point(categoryLabel.Location.X + categoryLabel.Width + 3,
                this.boxCategory.Location.Y);
            this.boxCategory.Size = new Size(btnRefresh.Location.X - boxCategory.Location.X - 6,
                boxCategory.Size.Height);

        }
        
        public IList<IActivity> Activities
        {
            set
            {
                if (_allowActivityUpdate)
                {
                    selectedActivities = new List<IActivity>();
                    foreach (IActivity t in value)
                    {
                        if (t.GPSRoute != null && t.GPSRoute.Count > 1)
                        {
                            selectedActivities.Add(t);
                        }
                    }
                    if (selectedActivities.Count == 0)
                    {
                        this.refActivity = null;
                        summaryList.Visible = false;
                        summaryListLabel.Text = Resources.NoGpsActivitiesSelected;
                        summaryListLabel.Visible = true;
                    }
                    else
                    {
                        bool isMatch = false;
                        if (this.refActivity != null)
                        {
                            foreach (IActivity t in selectedActivities)
                            {
                                if (t.Equals(this.refActivity))
                                {
                                    isMatch = true;
                                    break;
                                }
                            }
                        }
                        if (!isMatch)
                        {
                            this.refActivity = selectedActivities[0];
                        }
                    }
                    _needsRecalculation = true;
                    calculate();
                }
            }
            get { return selectedActivities; }
        }

        public bool HidePage()
        {
            _showPage = false;
#if !ST_2_1
            if (m_layer != null)
            {
                m_layer.ShowPage = _showPage;
            }
#endif
            return true;
        }
        public void ShowPage(string bookmark)
        {
            _showPage = true;
            calculate();
#if !ST_2_1
            if (m_layer != null)
            {
                m_layer.ShowPage = _showPage;
            }
#endif
        }

        private void RefreshColumns()
        {
            summaryList.Columns.Clear();
            ICollection<IListColumnDefinition> allCols = SummaryColumnIds.ColumnDefs(this.refActivity);
            //Permanent fields
            foreach (IListColumnDefinition columnDef in SummaryColumnIds.PermanentMultiColumnDefs())
            {
                TreeList.Column column = new TreeList.Column(
                    columnDef.Id,
                    columnDef.Text(columnDef.Id),
                    columnDef.Width,
                    columnDef.Align
                );
                summaryList.Columns.Add(column);
            }
            foreach (string id in Settings.ActivityPageColumns)
            {
                foreach (ListColumnDefinition columnDef in allCols)
                {
                    if (columnDef.Id == id)
                    {
                        TreeList.Column col = new TreeList.Column(
                            columnDef.Id,
                            columnDef.Text(columnDef.Id),
                            columnDef.Width,
                            columnDef.Align
                        );
                        col.CanSelect = columnDef.CanSelect;
                        summaryList.Columns.Add(col);
                        break;
                    }
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

        private void setCategoryLabel()
        {
            if (selectedActivities.Count > 1)
            {
                this.boxCategory.Text = string.Format(Resources.LimitingToSelected, selectedActivities.Count);
                //categoryLabel.Text = string.Format(Resources.LimitingToSelected, selectedActivities.Count);
            }
            else
            {
                if (Settings.SelectedCategory == null)
                {
                    this.boxCategory.Text = Util.StringResources.UseAllCategories;
                    //categoryLabel.Text = Resources.IncludeAllActivitiesInSearch;
                }
                else
                {
                    this.boxCategory.Text = Settings.printFullCategoryPath(Settings.SelectedCategory);
                    //categoryLabel.Text = String.Format("{0}",//Resources.IncludeOnlyCategory,
                    //    Settings.printFullCategoryPath(Settings.SelectedCategory));
                }
            }
#if !ST_2_1
            if (null != m_view && m_view.RouteSelectionProvider.SelectedItems.Count > 0)
            {
                //TODO: Special info for selection?
                //categoryLabel.Text += " Using selected points";
            }
#endif
            //btnChangeCategory.Location = new Point(
            //        summaryLabel.Location.X + summaryLabel.Width + 5,
            //             btnChangeCategory.Location.Y);
            //int btnOffset = btnChangeCategory.Visible ? btnChangeCategory.Width + 5 : 0;
            //categoryLabel.Location = new Point(
            //       btnChangeCategory.Location.X + btnOffset, summaryLabel.Location.Y);
        }

        private void summaryView_Sort()
        {
            summaryList.SetSortIndicator(Settings.SummaryViewSortColumn,
                Settings.SummaryViewSortDirection == ListSortDirection.Ascending);
            List<UniqueRoutesResult> list = (List<UniqueRoutesResult>)summaryList.RowData;
            list.Sort();
            summaryList.RowData = list;
        }

        private void setSize()
        {
            setCategoryLabel();
            Refresh();
        }

        private void setTable()
        {
            summaryList.Columns.Clear();
            summaryList.RowData = null;
            if (similar != null)
            {
                if (similar.Count > 1)
                {
                    IList<UniqueRoutesResult> result = new List<UniqueRoutesResult>();
                    similarToolTip = new Dictionary<string, string>();
                    RefreshColumns();

                    IDictionary<IActivity, PointInfo[]> commonStretches = null;
                    bool doGetcommonStretches = true;// Plugin.Verbose > 0;
                    if (doGetcommonStretches)
                    {
                        //reset calculations
                        m_commonStretches = null;
                        commonStretches = CommonStretches.getCommonSpeed(SimilarPoints, similar, Settings.UseActive);
                    }
                    foreach (IActivity activity in similar)
                    {
                        string commonText = null;
                        if (commonStretches != null)
                        {
                            if (commonStretches[activity][0].index > 0)
                            {
                                commonText =
                                    UnitUtil.PaceOrSpeed.ToString(Settings.ShowPace,
                                    commonStretches[activity][0].distance / commonStretches[activity][0].time, "u") +
                                    " (" + UnitUtil.PaceOrSpeed.ToString(Settings.ShowPace,
                                    commonStretches[activity][1].distance / commonStretches[activity][1].time) + ")" +
                                    " " + commonStretches[activity][0].index + " " + Resources.Sections + " " +
                                    UnitUtil.Distance.ToString(commonStretches[activity][0].distance, "u") + " " +
                                    UnitUtil.Time.ToString(commonStretches[activity][0].time) +
                                    " (" + UnitUtil.Distance.ToString(commonStretches[activity][1].distance, "u") + " " +
                                    " " + UnitUtil.Time.ToString(commonStretches[activity][1].time) + ")";
                                if (Plugin.Verbose > 0)
                                {
                                    //Extra info, also for additional info
                                    IItemTrackSelectionInfo[] ii = CommonStretches.getSelInfo(
                                        new DateTime[] { activity.StartTime, refActivity.StartTime },
                                        SimilarPoints[activity], Settings.UseActive);
                                    foreach (ValueRange<DateTime> d in ii[0].MarkedTimes)
                                    {
                                        commonText += System.Environment.NewLine + d;
                                    }
                                    commonText += System.Environment.NewLine;
                                    foreach (ValueRange<DateTime> d in ii[1].MarkedTimes)
                                    {
                                        commonText += System.Environment.NewLine + d;
                                    }
                                }
                            }
                            else
                            {
                                commonText = commonStretches[activity][0].index + " " + Resources.Sections;
                            }
                        }
                        result.Add(new UniqueRoutesResult(activity, commonText));
                        similarToolTip[activity.ReferenceId] = Resources.CommonStretches + ": " + commonText;
                    }
                    summaryList.RowData = result;
                    //summaryLabel.Text = String.Format(Resources.FoundActivities, similar.Count - 1);
                    toolTipInfo.SetToolTip(infoIcon, String.Format(Resources.FoundActivities, similar.Count - 1));
                    summaryView_Sort();
                    summaryList.Visible = true;
                    //summaryLabel.Visible = true;
                }
                else
                {
                    toolTipInfo.SetToolTip(infoIcon, Resources.DidNotFindAnyRoutes.Replace("\\n", Environment.NewLine));
                    summaryListLabel.Text = Resources.DidNotFindAnyRoutes.Replace("\\n", Environment.NewLine);
                    summaryListLabel.Visible = true;
                }
            }
            setSize();
        }

#if !ST_2_1
        IGPSRoute getRoute(IActivity activity, IList<IItemTrackSelectionInfo> selectGPS)
        {
            IGPSRoute urRoute = new GPSRoute();
            foreach (IItemTrackSelectionInfo item in selectGPS)
            {
                IValueRange<DateTime> ti = item.SelectedTime;
                DateTime s0, s1;
                if (null != ti)
                {
                    s0 = ti.Lower;
                    s1 = ti.Upper;
                }
                else
                {
                    IValueRange<double> di = item.SelectedDistance;
                    if (di == null) { continue; }
                    IDistanceDataTrack dt = activity.GPSRoute.GetDistanceMetersTrack();
                    s0 = dt.GetTimeAtDistanceMeters(di.Lower);
                    s1 = dt.GetTimeAtDistanceMeters(di.Upper);
                }

                for (int i = 0; i < activity.GPSRoute.Count; i++)
                {
                    if (activity.GPSRoute[i].ElapsedSeconds < s0.Subtract(activity.GPSRoute.StartTime).TotalSeconds) { continue; }
                    urRoute.Add(activity.GPSRoute.StartTime.AddSeconds(activity.GPSRoute[i].ElapsedSeconds), activity.GPSRoute[i].Value);
                    if (activity.GPSRoute[i].ElapsedSeconds > s1.Subtract(activity.GPSRoute.StartTime).TotalSeconds) { break; }
                }
            }
            return urRoute;
        }
#endif
        private void calculate()
        {
            calculate(true);
        }
        private void calculate(bool useSelection)
        {
            if (_showPage && _needsRecalculation)
            {
                if (refActivity != null)
                {
                    summaryList.Visible = false;
                    summaryLabel.Visible = false;
                    summaryListLabel.Visible = false;
                    categoryLabel.Visible = false;
                    //btnChangeCategory.Visible = false;
                    boxCategory.Visible = true;
                    progressBar.BringToFront(); //Kept at back to work with designer...
                    progressBar.Visible = true;
                    urRoute.Clear();
#if !ST_2_1
                    IList<IItemTrackSelectionInfo> selectedGPS = m_view.RouteSelectionProvider.SelectedItems;
                    if (useSelection && selectedGPS.Count > 0)
                    {
                        urRoute = getRoute(refActivity, selectedGPS);
                    }
#endif
                    IList<IActivity> activities;

                    if (selectedActivities.Count > 1)
                    {
                        activities = selectedActivities;
                    }
                    else
                    {
                        activities = UniqueRoutes.getBaseActivities();
                        //btnChangeCategory.Visible = true;
                        boxCategory.Visible = true;
                    }
                    if (urRoute.Count > 0)
                    {
                        similar = UniqueRoutes.findSimilarRoutes(urRoute, activities, false, progressBar);
                    }
                    else
                    {
                        similar = UniqueRoutes.findSimilarRoutes(refActivity, activities, true, progressBar);
                    }
                    progressBar.Visible = false;
                    categoryLabel.Visible = true;
                    determinePaceOrSpeed();

                    setTable();
                    
                    //Add the activities to the menu
                    IList<IActivity> remaining = new List<IActivity>();
                    foreach (IActivity t in selectedActivities)
                    {
                        remaining.Add(t);
                    }
                    this.ctxMenuItemRefActivity.DropDownItems.Clear();
                    foreach (IActivity act in similar)
                    {
                        string tt = act.StartTime + " " + act.Name + " " + UnitUtil.Distance.ToString(act.TotalDistanceMetersEntered, "u") + " " + UnitUtil.Time.ToString(act.TotalTimeEntered, "u");
                        ToolStripMenuItem childMenuItem = new ToolStripMenuItem(tt);
                        childMenuItem.Tag = act;
                        if (act.Equals(refActivity) && urRoute.Count == 0)
                        {
                            childMenuItem.Checked = true;
                        }
                        else
                        {
                            childMenuItem.Checked = false;
                        }
                        this.ctxMenuItemRefActivity.DropDownItems.Add(childMenuItem);
                        childMenuItem.Click += new System.EventHandler(this.ctxRefActivityItemActivities_Click);
                        remaining.Remove(act);
                    }
                    if (urRoute.Count > 0)
                    {
                        string tt = urRoute.StartTime + " " + urRoute.TotalDistanceMeters + " " + urRoute.TotalElapsedSeconds;
                        ToolStripMenuItem childMenuItem = new ToolStripMenuItem(tt);
                        childMenuItem.Checked = true;
                        childMenuItem.Enabled = false;
                        this.ctxMenuItemRefActivity.DropDownItems.Add(childMenuItem);
                    }
                    if (remaining.Count > 0)
                    {
                        ToolStripSeparator separator = new ToolStripSeparator();
                        this.ctxMenuItemRefActivity.DropDownItems.Add(separator);
                        foreach (IActivity act in remaining)
                        {
                            string tt = act.StartTime + " " + act.Name + " " +UnitUtil.Distance.ToString(act.TotalDistanceMetersEntered, "u") + " " + UnitUtil.Time.ToString(act.TotalTimeEntered, "u");
                            ToolStripMenuItem childMenuItem = new ToolStripMenuItem(tt);
                            childMenuItem.Tag = act;
                            if (act.Equals(refActivity) && urRoute.Count == 0)
                            {
                                childMenuItem.Checked = true;
                            }
                            else
                            {
                                childMenuItem.Checked = false;
                            }
                            this.ctxMenuItemRefActivity.DropDownItems.Add(childMenuItem);
                            childMenuItem.Click += new System.EventHandler(this.ctxRefActivityItemActivities_Click);
                        }
                    }
                }
                else
                {
                    setSize();
                }
            }
        }

        private void determinePaceOrSpeed()
        {
        //    int pace = 0, speed = 0;
        //    foreach (IActivity activity in similar)
        //    {
        //        if (activity.Category.SpeedUnits.ToString().ToLower().Equals("speed"))
        //            speed++;
        //        else
        //            pace++;
        //    }
        //    Settings.ShowPace = pace >= speed;
            if (refActivity != null)
            {
                Settings.ShowPace = (refActivity.Category.SpeedUnits == ZoneFiveSoftware.Common.Data.Measurement.Speed.Units.Pace) ?
                    true : false;
            }
        }

        //Some views like mapping is only working in single view - there are likely better tests
        public bool isSingleView
        {
            get
            {
#if !ST_2_1
                if (CollectionUtils.GetSingleItemOfType<IActivity>(m_view.SelectionProvider.SelectedItems) == null)
                {
                    return false;
                }
#endif
                return true;
            }
        }
        
        //This works slightly different to i.e. Trails, as it marks the reference if possible
        public void MarkRef(IItemTrackSelectionInfo res)
        {
#if !ST_2_1
            if (_showPage)
            {
                if (m_view != null &&
                    m_view.RouteSelectionProvider != null)
                {
                        m_view.RouteSelectionProvider.SelectedItems = new IItemTrackSelectionInfo[] {
                            res };
                }
            }
#endif
        }
        public void MarkTrack(IList<TrailResultMarked> atr)
        {
#if !ST_2_1
            if (_showPage)
            {
                //if (m_view != null &&
                //    m_view.RouteSelectionProvider != null &&
                //    isSingleView == true)
                //{
                //    //if (!markChart)
                //    //{
                //    //    m_view.RouteSelectionProvider.SelectedItemsChanged -= new EventHandler(RouteSelectionProvider_SelectedItemsChanged);
                //    //}
                //    if (atr.Count > 0)
                //    {
                //        //Only one activity, OK to merge selections on one track
                //        TrailsItemTrackSelectionInfo r = TrailResultMarked.SelInfoUnion(atr);
                //        r.Activity = refActivity;
                //        m_view.RouteSelectionProvider.SelectedItems = new IItemTrackSelectionInfo[] { r };
                //        m_layer.ZoomRoute = atr[0].trailResult.GpsPoints(r);

                //    }
                //    //if (!markChart)
                //    //{
                //    //    m_view.RouteSelectionProvider.SelectedItemsChanged += new EventHandler(RouteSelectionProvider_SelectedItemsChanged);
                //    //}
                //}
                //else
                {
                    IDictionary<string, MapPolyline> result = new Dictionary<string, MapPolyline>();
                    foreach (TrailResultMarked trm in atr)
                    {
                        foreach (TrailMapPolyline m in TrailMapPolyline.GetTrailMapPolyline(trm.trailResult, trm.selInfo))
                        {
                            m.Click += new MouseEventHandler(mapPoly_Click);
                            result.Add(m.key, m);
                        }
                    }
                    m_layer.MarkedTrailRoutes = result;
                }
            }
#endif
        }

        public void EnsureVisible(IList<TrailResult> atr, bool chart)
        {
            if (atr != null && atr.Count > 0 && atr[0].Activity!=null)
            {
                foreach (UniqueRoutesResult urr in (IList<UniqueRoutesResult>)summaryList.RowData)
                {
                    if (atr[0].Activity == urr.Activity)
                    {
                        this.summaryList.EnsureVisible(urr);
                    }
                }
            }
        }

        private IDictionary<IActivity, IList<PointInfo[]>> SimilarPoints
        {
            get
            {
                if (m_commonStretches == null)
                {
                    if (urRoute != null && urRoute.Count > 0)
                    {
                        m_commonStretches = CommonStretches.findSimilarPoints(urRoute, similar);
                    }
                    else if (refActivity != null)
                    {
                        m_commonStretches = CommonStretches.findSimilarPoints(refActivity, similar);
                    }
                    else
                    {
                        m_commonStretches = new Dictionary<IActivity, IList<PointInfo[]>>();
                    }
                }
                return m_commonStretches;
            }
        }

        /*******************************************************************************/
        //Event handlers

        void mapPoly_Click(object sender, MouseEventArgs e)
        {
            if (sender is TrailMapPolyline)
            {
                IList<TrailResult> result = new List<TrailResult> { (sender as TrailMapPolyline).TrailRes };
                this.EnsureVisible(result, true);
            }
        }

        void copyTableMenu_Click(object sender, EventArgs e)
        {
            summaryList.CopyTextToClipboard(true, System.Globalization.CultureInfo.CurrentCulture.TextInfo.ListSeparator);
        }

        void SystemPreferences_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            setTable();
        }

        private void UniqueRoutesActivityDetailView_Resize(object sender, EventArgs e)
        {
            setSize();
        }

        private void ctxRefActivityItemActivities_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem sourceMenuItem = (ToolStripMenuItem)sender;
            if (sourceMenuItem != null && sourceMenuItem.Tag != null)
            {
                refActivity = (IActivity)(sourceMenuItem.Tag);
                calculate(false); //update, without using selected points
            }
        }
        private void refreshButton_Click(object sender, EventArgs e)
        {
            _needsRecalculation = true;
            calculate();
        }

        private void sendActivityButton_Click(object sender, EventArgs e)
        {
            SendToPlugin sendToPlugin = null;
            IList<IActivity> list = new List<IActivity>();
            if (summaryList.Selected.Count > 0)
            //if (selectedBox.SelectedItem.Equals(StringResources.Selected))
            {
                foreach (UniqueRoutesResult sel in summaryList.Selected)
                {
                    list.Add(sel.Activity);
                }
            }
            else
            {
                list = similar;
            }

            string pluginName = Settings.SelectedPlugin;
            try
            {
                if (sender is ToolStripMenuItem)
                {
                    sendToPlugin = aSendToMenu[sender as ToolStripMenuItem];
                    pluginName = sendToPlugin.name;
                }
                else
                {
                    //Honor selected box
                    if (!selectedBox.SelectedItem.Equals(StringResources.Selected))
                    {
                        list = similar;
                    }

                    foreach (SendToPlugin p in Settings.aSendToPlugin)
                    {
                        if (pluginBox.SelectedItem.Equals(p.name))
                        {
                            sendToPlugin = p;
                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                new WarningDialog(String.Format(Resources.PluginApplicationError, pluginName, ex.ToString()) + " other");
            }

            try
            {
                if (null != sendToPlugin && null != sendToPlugin.pType)
                {
                    object[] par = sendToPlugin.par;
                    //all plugins have activities as first par
                    par[0] = list;
#if !ST_2_1
                    par[1] = m_view;
#endif
                    Activator.CreateInstance(sendToPlugin.pType, par);
                }
            }
            catch (Exception ex)
            {
                new WarningDialog(String.Format(Resources.PluginApplicationError, Settings.SelectedPlugin, ex.ToString()));
            }
        }

        private void activeBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            Settings.UseActive = activeBox.SelectedItem.Equals(StringResources.Active);
            activeMenuItem.CheckState = Settings.UseActive ? CheckState.Checked : CheckState.Unchecked;
            setTable();
        }

        private void speedBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            Settings.ShowPace = speedBox.SelectedItem.Equals(CommonResources.Text.LabelPace);
            setTable();
        }

        //private void changeCategory_Click(object sender, EventArgs e)
        //{
        //    CategorySelector cs = new CategorySelector(m_visualTheme, m_culture);
        //    cs.ShowDialog();
        //    setCategoryLabel();
        //    _needsRecalculation = true;
        //    calculate();
        //}
        private void addNode(IActivityCategory category, System.Collections.IList parentCategories)
        {
            if (parentCategories != null)
            {
                if (category.SubCategories.Count > 0) parentCategories.Add(category);
            }
            foreach (IActivityCategory subcategory in category.SubCategories)
            {
                addNode(subcategory, parentCategories);
            }
        }

        private void boxCategory_ButtonClicked(object sender, EventArgs e)
        {
            TreeListPopup treeListPopup = new TreeListPopup();
            treeListPopup.ThemeChanged(m_visualTheme);
            treeListPopup.Tree.Columns.Add(new TreeList.Column());

            IList<object> list = new List<object>();
            list.Add(Util.StringResources.UseAllCategories);
            foreach (IActivityCategory category in Plugin.GetApplication().Logbook.ActivityCategories)
            {
                list.Add(category);
            }

            treeListPopup.Tree.RowData = list;
            treeListPopup.Tree.ContentProvider = new ActivityCategoryContentProvider(list);
            treeListPopup.Tree.ShowPlusMinus = true;
            treeListPopup.FitContent = false;

            if (Settings.SelectedCategory != null)
            {
                treeListPopup.Tree.Selected = new object[] { Settings.SelectedCategory };
            }
            //Expand by default
            System.Collections.IList parentCategories = new System.Collections.ArrayList();
            foreach (IActivityCategory category in Plugin.GetApplication().Logbook.ActivityCategories)
            {
                addNode(category, parentCategories);
            }
            treeListPopup.Tree.Expanded = parentCategories;

            treeListPopup.ItemSelected += delegate(object sender2, TreeListPopup.ItemSelectedEventArgs e2)
            {
                if (e2.Item is IActivityCategory)
                {
                    Settings.SelectedCategory = (IActivityCategory)e2.Item;
                }
                else
                {
                    Settings.SelectedCategory = null;
                }
                setCategoryLabel();
                _needsRecalculation = true;
                calculate();
            };
            treeListPopup.Popup(this.boxCategory.Parent.RectangleToScreen(this.boxCategory.Bounds));
        }

        private void pluginBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            Settings.SelectedPlugin = (string)pluginBox.SelectedItem;
        }

        private void selectedBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            Settings.SelectAll = selectedBox.SelectedItem.Equals(Resources.All);
        }
        private void selectedRow_DoubleClick(object sender, MouseEventArgs e)
        {
            object row;
            TreeList.RowHitState dummy;
            row = summaryList.RowHitTest(e.Location, out dummy);
            if (row != null)
            {
                string bookmark = "id=" + ((UniqueRoutesResult)row).Activity;
                Plugin.GetApplication().ShowView(GUIDs.OpenView, bookmark);
            }
        }

        private void summaryView_ColumnHeaderMouseClick(object sender, TreeList.ColumnEventArgs e)
        {
            if (Settings.SummaryViewSortColumn == e.Column.Id)
            {
                Settings.SummaryViewSortDirection = Settings.SummaryViewSortDirection == ListSortDirection.Ascending ?
                       ListSortDirection.Descending : ListSortDirection.Ascending;
            }
            Settings.SummaryViewSortColumn = e.Column.Id;
            summaryView_Sort();
        }
#if !ST_2_1
        private void listSettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ListSettingsDialog dialog = new ListSettingsDialog();
            dialog.AvailableColumns = SummaryColumnIds.ColumnDefs(null);
            dialog.ThemeChanged(m_visualTheme);
            dialog.AllowFixedColumnSelect = false;
            dialog.SelectedColumns = Settings.ActivityPageColumns;

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                Settings.ActivityPageColumns = dialog.SelectedColumns;
                RefreshColumns();
            }
        }

        public static IList<UniqueRoutesResult> getListSelection(System.Collections.IList tlist)
        {
            IList<UniqueRoutesResult> aTr = new List<UniqueRoutesResult>();
            if (tlist != null)
            {
                foreach (object t in tlist)
                {
                    if (t != null)
                    {
                        aTr.Add(((UniqueRoutesResult)t));
                    }
                }
            }
            return aTr;
        }
        void summaryList_SelectedItemsChanged(object sender, System.EventArgs e)
        {
            IList<UniqueRoutesResult> results = getListSelection(this.summaryList.SelectedItems);
            IDictionary<string, MapPolyline> routes = new Dictionary<string, MapPolyline>();
            foreach (UniqueRoutesResult ur in results)
            {
                //Possibly limit no of Trails shown, it slows down (show complete Activities?)
                TrailResult tr = new TrailResult(ur);
                TrailMapPolyline m = new TrailMapPolyline(tr);
                m.Click += new MouseEventHandler(mapPoly_Click);
                routes.Add(m.key, m);
            }
            m_layer.TrailRoutes = routes;
        }
#endif

        void summaryList_Click(object sender, System.EventArgs e)
        {
#if !ST_2_1
            object row;
            TreeList.RowHitState hit;
            row = summaryList.RowHitTest(((MouseEventArgs)e).Location, out hit);
            if (row != null && hit == TreeList.RowHitState.Row)
            {
                UniqueRoutesResult utr = (UniqueRoutesResult)(row);
                bool isMatch = false;
                foreach (UniqueRoutesResult t in getListSelection(this.summaryList.SelectedItems))
                {
                    if (t == utr)
                    {
                        isMatch = true;
                        break;
                    }
                }
                IList<TrailResultMarked> aTrm = new List<TrailResultMarked>();
                if (isMatch && refActivity != null)
                {
                    IDictionary<IActivity, IList<PointInfo[]>> commonStretches = SimilarPoints;
                    if (commonStretches.Count > 0 &&
                        commonStretches.ContainsKey(utr.Activity) &&
                        commonStretches[utr.Activity] != null &&
                        commonStretches[utr.Activity].Count > 0)
                    {
                        TrailResult tr = new TrailResult(utr);
                        IItemTrackSelectionInfo[] i = CommonStretches.getSelInfo(new DateTime[] { utr.Activity.StartTime, refActivity.StartTime },
                                                commonStretches[utr.Activity], true);
                        ((TrailsItemTrackSelectionInfo)i[0]).Activity = utr.Activity;
                        ((TrailsItemTrackSelectionInfo)i[1]).Activity = refActivity;
                        aTrm.Add(new TrailResultMarked(tr, i[0].MarkedTimes));
                        this.MarkRef(i[1]);
                    }
                }
                this.MarkTrack(aTrm);
            }
#endif
        }

        private void activeMenuItem_Click(object sender, EventArgs e)
        {
            Settings.UseActive = !Settings.UseActive;
            activeMenuItem.CheckState = Settings.UseActive ? CheckState.Checked : CheckState.Unchecked;
            setTable();
        }

        void limitActivityMenuItem_Click(object sender, System.EventArgs e)
        {
#if !ST_2_1
            if (this.summaryList.SelectedItems != null && this.summaryList.SelectedItems.Count > 0)
            {
                IList<IActivity> aAct = new List<IActivity>();
                foreach (UniqueRoutesResult tr in this.summaryList.SelectedItems)
                {
                    aAct.Add(tr.Activity);
                }
                _allowActivityUpdate = false;
                m_view.SelectionProvider.SelectedItems = (List<IActivity>)aAct;
                _allowActivityUpdate = true;
            }
#endif
        }

        //ToolTip as used in DataGridView, not implemented for TreeList
        //private void summaryView_CellToolTipTextNeeded(object sender, EventArgs e)
        //{
        //    object row;
        //    TreeList.RowHitState dummy;
        //    row = summaryList.RowHitTest(e.Location, out dummy);
        //    if (row != null)
        //    {
        //        summaryList.Controls.
        //           row.ToolTipText = similarToolTip[((UniqueRoutesResult)row).Activity.ReferenceId];
        //    }
        //}

        //ToolTip support in TreeList - courtsey of omb

        // private member variables of the Control - initialization omitted
        ToolTip summaryListToolTip = new ToolTip();
        Timer summaryListToolTipTimer = new Timer();
        bool summaryListTooltipDisabled = false; // is set to true, whenever a tooltip would be annoying, e.g. while a context menu is shown
        UniqueRoutesResult summaryListLastEntryAtMouseMove = null;
        Point summaryListCursorLocationAtMouseMove;

        private void summaryList_MouseMove(object sender, MouseEventArgs e)
        {
           TreeList.RowHitState rowHitState;
           UniqueRoutesResult entry = (UniqueRoutesResult)summaryList.RowHitTest(e.Location, out rowHitState);
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
                string tt = similarToolTip[summaryListLastEntryAtMouseMove.Activity.ReferenceId];
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
