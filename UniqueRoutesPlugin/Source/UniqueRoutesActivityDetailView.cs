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
#if !ST_2_1
using ZoneFiveSoftware.Common.Data;
using ZoneFiveSoftware.Common.Data.GPS;
using ZoneFiveSoftware.Common.Visuals.Forms;
#endif
using ZoneFiveSoftware.Common.Data.Fitness;
using GpsRunningPlugin.Source;
using GpsRunningPlugin;
using System.Reflection.Emit;
using System.Reflection;
using System.IO;
using GpsRunningPlugin.Properties;
using GpsRunningPlugin.Util;

namespace GpsRunningPlugin.Source
{
    public partial class UniqueRoutesActivityDetailView : UserControl
    {
        private ITheme m_visualTheme;
        private System.Globalization.CultureInfo m_culture;
        private IList<IActivity> similar;
        private IDictionary<string, string> similarToolTip;
        private IActivity refActivity = null;
        private IList<IActivity> selectedActivities = new List<IActivity>();
        private IDictionary<ToolStripMenuItem, SendToPlugin> aSendToMenu = new Dictionary<ToolStripMenuItem, SendToPlugin>();

#if !ST_2_1
        private IDailyActivityView m_view = null;
        public UniqueRoutesActivityDetailView(IDailyActivityView view)
            : this()
        {
            m_view = view;
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
            //TODO: ToolTip is not implemented for TreeList?
            //summaryList.MouseHover += new EventHandler(summaryView_CellToolTipTextNeeded);
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
                sendMenuItem.Click += new System.EventHandler(this.sendActivityButton_Click);
                sendMenuItem.Enabled = false;
                if (p.pType != null)
                {
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
                aSendToMenu[sendMenuItem] = p;
                this.sendToMenuItem.DropDownItems.Add(sendMenuItem);
            }

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
            this.ctxMenuItemRefActivity.Text = Properties.Resources.ctxReferenceActivity;
            copyTable.Image = ZoneFiveSoftware.Common.Visuals.CommonResources.Images.DocumentCopy16;
#if !ST_2_1
            sendToMenuItem.Image = ZoneFiveSoftware.Common.Visuals.CommonResources.Images.Analyze16;
#endif
            listSettingsMenuItem.Image = ZoneFiveSoftware.Common.Visuals.CommonResources.Images.Table16;
            btnDoIt.CenterImage = ZoneFiveSoftware.Common.Visuals.CommonResources.Images.Refresh16;
#if !ST_2_1
            this.listSettingsMenuItem.Click += new System.EventHandler(this.listSettingsToolStripMenuItem_Click);
#else
            //No listSetting dialog in ST2
            if (this.contextMenu.Items.Contains(this.listSettingsMenuItem))
            {
                this.contextMenu.Items.Remove(this.listSettingsMenuItem);
            }
#endif
            progressBar.BringToFront(); //Kept at back to work with designer...

            summaryList.ContextMenuStrip = contextMenu;
            summaryList.MouseDoubleClick += new MouseEventHandler(selectedRow_DoubleClick);
        }
        public IList<IActivity> Activities
        {
            set
            {
                selectedActivities = value;
                if (value == null || value.Count == 0)
                {
                    this.refActivity = null;
                }
                else
                {
                    bool isMatch = false;
                    if (this.refActivity != null)
                    {
                        foreach (IActivity t in value)
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
                        this.refActivity = value[0];
                    }
                }

                calculate();
            }
            get { return selectedActivities; }
        }

        private bool _showPage = false;
        public bool HidePage()
        {
            _showPage = false;
            return true;
        }
        public void ShowPage(string bookmark)
        {
            bool changed = (_showPage != true);
            _showPage = true;
            if (changed) { calculate(); }
        }

        private void setTable()
        {
            setTable(null);
        }

        private void setTable(IActivity urActivity)
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

                    //Debug info for stretches
                    IDictionary<IActivity, IList<double>> commonStretches = null;
                    IDictionary<IActivity, string> similarPoints = new Dictionary<IActivity, string>();
                    bool doGetcommonStretches = true;// Plugin.Verbose > 0;
                    if (doGetcommonStretches && urActivity != null)
                    {
                        commonStretches = CommonStretches.getCommonSpeed(urActivity, similar, Settings.UseActive);
                        //similarPoints = CommonStretches.findSimilarDebug(this.activity, similar, Settings.UseActive);
                    }
                    foreach (IActivity activity in similar)
                    {
                        string commonText = null;
                        if (commonStretches != null)
                        {
                            commonText = 
                                UnitUtil.PaceOrSpeed.ToString(Settings.ShowPace,
                                commonStretches[activity][0] / commonStretches[activity][1], "u") +
                                " (" + UnitUtil.PaceOrSpeed.ToString(Settings.ShowPace,
                                commonStretches[activity][2] / commonStretches[activity][3]) + ")" +
                                " " + commonStretches[activity][4] + " "+ Resources.Sections + " " +
                                UnitUtil.Distance.ToString(commonStretches[activity][0], "u") + " " +
                                UnitUtil.Time.ToString(commonStretches[activity][1]) +
                                " (" + UnitUtil.Distance.ToString(commonStretches[activity][2], "u") + " " +
                                " " + UnitUtil.Time.ToString(commonStretches[activity][3]) + ")";
                        }
                        result.Add(new UniqueRoutesResult(activity, commonText));
                        similarToolTip[activity.ReferenceId] = Resources.CommonStretches + ": " + commonText;
                    }
                    summaryList.RowData = result;
                    summaryLabel.Text = String.Format(Resources.FoundActivities, similar.Count - 1);
                    summaryView_Sort();
                    summaryList.Visible = true;
                    summaryLabel.Visible = true;
                }
                else
                {
                    summaryLabel.Text = Resources.DidNotFindAnyRoutes.Replace("\\n", Environment.NewLine);
                    summaryLabel.Visible = true;
                }
            }
            setSize();
        }
        private void RefreshColumns()
        {
            summaryList.Columns.Clear();
            ICollection<IListColumnDefinition> allCols = SummaryColumnIds.ColumnDefs(this.refActivity);
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

        public void ThemeChanged(ITheme visualTheme)
        {
            m_visualTheme = visualTheme;
            summaryList.ThemeChanged(visualTheme);
            this.splitContainer1.Panel1.BackColor = visualTheme.Control;
            this.splitContainer1.Panel2.BackColor = visualTheme.Control;
        }
        public void UICultureChanged(System.Globalization.CultureInfo culture)
        {
            //Some labels depends on the activity
            //summaryLabel, CommonStretches column, setTable()
            m_culture = culture;

            setCategoryLabel();
            btnChangeCategory.Text = StringResources.ChangeCategory;
            copyTable.Text = ZoneFiveSoftware.Common.Visuals.CommonResources.Text.ActionCopy;
            RefreshColumns();
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
            btnDoIt.Text = "";
            //correctUI(new Control[] { selectedBox, sendLabel2, pluginBox, btnDoIt });
            sendResultToLabel1.Text = StringResources.Send;
            sendResultToLabel1.Location = new Point(selectedBox.Location.X - 5 - sendResultToLabel1.Size.Width,
                                        sendLabel2.Location.Y);
            labelShow.Location = new Point(activeBox.Location.X - 5 - labelShow.Size.Width,
                                    labelShow.Location.Y);
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
                categoryLabel.Text = string.Format(Resources.LimitingToSelected, selectedActivities.Count);
            }
            else
            {
                if (Settings.SelectedCategory == null)
                {
                    categoryLabel.Text = Resources.IncludeAllActivitiesInSearch;
                }
                else
                {
                    categoryLabel.Text = String.Format("{0}",//Resources.IncludeOnlyCategory,
                        Settings.printFullCategoryPath(Settings.SelectedCategory));
                }
            }
#if !ST_2_1
            if (null != m_view && m_view.RouteSelectionProvider.SelectedItems.Count > 0)
            {
                //TODO: Special info for selection?
                //categoryLabel.Text += " Using selected points";
            }
#endif
            btnChangeCategory.Location = new Point(
                    summaryLabel.Location.X + summaryLabel.Width + 5,
                         btnChangeCategory.Location.Y);
            int btnOffset = btnChangeCategory.Visible ? btnChangeCategory.Width + 5 : 0;
            categoryLabel.Location = new Point(
                   btnChangeCategory.Location.X + btnOffset, summaryLabel.Location.Y);
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

#if !ST_2_1
        IActivity getActivity(IActivity activity, IList<IItemTrackSelectionInfo> selectGPS)
        {
            IGPSRoute urRoute = new GPSRoute();
            foreach (IItemTrackSelectionInfo item in selectGPS)
            {
                IValueRange<DateTime> ti = item.SelectedTime;
                DateTime s0, s1;
                if (null != ti)
                {
                    s0=ti.Lower;
                    s1=ti.Upper;
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
            IActivity result = activity;
            if (urRoute.Count > 0)
            {
                result = Plugin.GetApplication().Logbook.Activities.Add(urRoute.StartTime);
                result.GPSRoute = urRoute;
            }
            return result;
        }
#endif
        private void calculate()
        {
            calculate(true);
        }
        private void calculate(bool useSelection)
        {
            if (_showPage)
            {
                if (refActivity != null)
                {
                    summaryList.Visible = false;
                    summaryLabel.Visible = false;
                    categoryLabel.Visible = false;
                    btnChangeCategory.Visible = false;
                    progressBar.Visible = true;
                    IActivity urActivity = refActivity;
#if !ST_2_1
                    int countGPS = 0;
                    IList<IItemTrackSelectionInfo> selectedGPS = m_view.RouteSelectionProvider.SelectedItems;
                    countGPS = selectedGPS.Count;
                    if (useSelection && countGPS > 0)
                    {
                        urActivity = getActivity(urActivity, selectedGPS);
                    }
#endif
                    IList<IActivity> activities;

                    if (selectedActivities.Count >1)
                    {
                        activities = selectedActivities;
                    }
                    else
                    {
                        activities = UniqueRoutes.getBaseActivities();
                        btnChangeCategory.Visible = true;
                    }
                    similar = UniqueRoutes.findSimilarRoutes(urActivity, activities, urActivity.Equals(refActivity), progressBar);
                    categoryLabel.Visible = true;
                    determinePaceOrSpeed();
                    progressBar.Visible = false;

                    summaryLabel.Visible = true;
                    setTable(urActivity);
                    //Add the activities to the menu
                    this.ctxMenuItemRefActivity.DropDownItems.Clear();
                    foreach (IActivity act in similar)
                    {
                        string tt = act.StartTime + " " + act.Name + act.TotalDistanceMetersEntered + " " + act.TotalTimeEntered;
                        ToolStripMenuItem childMenuItem = new ToolStripMenuItem(tt);
                        childMenuItem.Tag = act.ReferenceId;
                        if (act.Equals(urActivity))
                        {
                            childMenuItem.Checked = true;
                            if (!urActivity.Equals(refActivity))
                            {
                                //Temporary activity, to be removed
                                childMenuItem.Enabled = false;

                            }
                        }
                        else
                        {
                            childMenuItem.Checked = false;
                        }
                        this.ctxMenuItemRefActivity.DropDownItems.Add(childMenuItem);
                        childMenuItem.Click += new System.EventHandler(this.ctxRefActivityItemActivities_Click);
                    }
#if !ST_2_1
                    if (!urActivity.Equals(refActivity))
                    {
                        //Delete temporary activity
                        Plugin.GetApplication().Logbook.Activities.Remove(urActivity);
                    }
#endif
                }
                else
                {
                    setSize();
                }
            }
        }

        private void determinePaceOrSpeed()
        {
            int pace = 0, speed = 0;
            foreach (IActivity activity in similar)
            {
                if (activity.Category.SpeedUnits.ToString().ToLower().Equals("speed"))
                    speed++;
                else
                    pace++;
            }
            Settings.ShowPace = pace >= speed;
        }

        /*******************************************************************************/
        //Event handlers

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
                string refId = (string)sourceMenuItem.Tag;
                foreach (IActivity act in similar)
                {
                    if (refId.Equals(act.ReferenceId))
                    {
                        refActivity = act;
                    }
                }

                calculate(false); //update, without using selected points
            }
        }
        private void refreshButton_Click(object sender, EventArgs e)
        {
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

            try
            {
                if (sender is ToolStripMenuItem)
                {
                    sendToPlugin = aSendToMenu[sender as ToolStripMenuItem];
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
                new WarningDialog(String.Format(Resources.PluginApplicationError, Settings.SelectedPlugin, ex.ToString())+ " other");
            }
               
            try
            {
                if (null != sendToPlugin && null != sendToPlugin.pType)
                {
                    object[] par = sendToPlugin.par;
                    //all plugins have activities as first par
                    par[0] = list;
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

        private void changeCategory_Click(object sender, EventArgs e)
        {
            CategorySelector cs = new CategorySelector(m_visualTheme, m_culture);
            cs.ShowDialog();
            setCategoryLabel();
            calculate();
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
            Guid view = new Guid("1dc82ca0-88aa-45a5-a6c6-c25f56ad1fc3");

            object row;
            TreeList.RowHitState dummy;
            row = summaryList.RowHitTest(e.Location, out dummy);
            if (row != null)
            {
                     string bookmark = "id=" + ((UniqueRoutesResult)row).Activity;
                    Plugin.GetApplication().ShowView(view, bookmark);
            }
        }
        //ToolTip unused
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
#endif
        private void activeMenuItem_Click(object sender, EventArgs e)
        {
            Settings.UseActive = !Settings.UseActive;
            activeMenuItem.CheckState = Settings.UseActive ? CheckState.Checked : CheckState.Unchecked;
            setTable();
        }
    }
}
