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
#if !ST_2_1
using ZoneFiveSoftware.Common.Visuals.Forms;
#endif
using ZoneFiveSoftware.Common.Data.Fitness;
using SportTracksUniqueRoutesPlugin.Source;
using SportTracksUniqueRoutesPlugin;
using System.Reflection.Emit;
using System.Reflection;
using System.IO;
using SportTracksUniqueRoutesPlugin.Properties;
using SportTracksUniqueRoutesPlugin.Util;

namespace SportTracksUniqueRoutesPlugin.Source
{
    public partial class UniqueRoutesActivityDetailView : UserControl
    {
        private ITheme m_visualTheme;
        private IList<IActivity> similar;
        private IDictionary<string, string> similarToolTip;
        private IActivity activity;
        private IDictionary<ToolStripMenuItem, SendToPlugin> aSendToMenu = new Dictionary<ToolStripMenuItem, SendToPlugin>();

        bool doUpdate;

        public UniqueRoutesActivityDetailView(IActivity activity)
        {
            InitializeComponent();
            doUpdate = true;
            this.Activity = activity;

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
            copyTable.Image = ZoneFiveSoftware.Common.Visuals.CommonResources.Images.DocumentCopy16;
#if !ST_2_1
            sendToMenuItem.Image = ZoneFiveSoftware.Common.Visuals.CommonResources.Images.Analyze16;
#endif
            listSettingsMenuItem.Image = ZoneFiveSoftware.Common.Visuals.CommonResources.Images.Table16;
#if !ST_2_1
            this.listSettingsMenuItem.Click += new System.EventHandler(this.listSettingsToolStripMenuItem_Click);
#else
            //No listSetting dialog in ST2
            if (this.contextMenu.Items.Contains(this.listSettingsMenuItem))
            {
                this.contextMenu.Items.Remove(this.listSettingsMenuItem);
            }
#endif
            summaryList.ContextMenuStrip = contextMenu;
            summaryList.MouseDoubleClick += new MouseEventHandler(selectedRow_DoubleClick);
            speedBox.SelectedItem = CommonResources.Text.LabelPace;
            //TODO: ToolTip is not implemented for TreeList?
            //summaryList.MouseHover += new EventHandler(summaryView_CellToolTipTextNeeded);
            summaryList.ColumnClicked += new TreeList.ColumnEventHandler(summaryView_ColumnHeaderMouseClick);

            Plugin.GetApplication().SystemPreferences.PropertyChanged += new PropertyChangedEventHandler(SystemPreferences_PropertyChanged);
            setSize();
            VisibleChanged += new EventHandler(UniqueRoutesActivityDetailView_VisibleChanged);

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
                doIt.Enabled = false;
            }
            correctLanguage();
            doUpdate = false;
        }

        public IActivity Activity
        {
            set
            {
                this.activity = value;
                summaryLabel.Visible = false;
                summaryList.Visible = false;                        
                progressBar.Visible = false;
                doIt.Visible = false;
                changeSettingsVisibility(false);
                if (activity != null)
                {
                    calculate();
                }
            }
            get { return activity; }
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

                    //Debug info for stretches
                    IDictionary<IActivity, IList<double>> commonStretches = null;
                    IDictionary<IActivity, string> similarPoints = new Dictionary<IActivity, string>();
                    bool doGetcommonStretches = true;// Plugin.Verbose > 0;
                    if (doGetcommonStretches)
                    {
                        commonStretches = CommonStretches.getCommonSpeed(this.activity, similar, Settings.UseActive);
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
                    changeSettingsVisibility(true);
                }
                else
                {
                    summaryLabel.Text = Resources.DidNotFindAnyRoutes.Replace("\\n", Environment.NewLine);
                    summaryLabel.Visible = true;
                    changeSettingsVisibility(false);
                }
            }
            else
            {
                changeSettingsVisibility(false);
            }
            setSize();
        }
        private void RefreshColumns()
        {
            summaryList.Columns.Clear();
            ICollection<IListColumnDefinition> allCols = SummaryColumnIds.ColumnDefs(this.activity);
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

        private void changeSettingsVisibility(bool visible)
        {
            //OLD: No longer visible
            visible = false;

            labelShow.Visible = visible;
            selectedBox.Visible = visible;
            activeBox.Visible = visible;
            speedBox.Visible = visible;
            pluginBox.Visible = visible;
            sendLabel2.Visible = visible;
            labelLaps.Visible = visible;
            doIt.Visible = visible;
            sendResultToLabel1.Visible = visible;
        }

        public void ThemeChanged(ITheme visualTheme)
        {
            //RefreshPage();
            m_visualTheme = visualTheme;
            summaryList.ThemeChanged(visualTheme);
#if !ST_2_1
            this.splitContainer1.Panel1.BackColor = Plugin.GetApplication().SystemPreferences.VisualTheme.Control;
            this.splitContainer1.Panel2.BackColor = Plugin.GetApplication().SystemPreferences.VisualTheme.Control;
#endif
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

        private void correctLanguage()
        {
            //Some labels depends on the activity
            //summaryLabel, CommonStretches column, setTable()

            setCategoryLabel();
            changeCategory.Text = StringResources.ChangeCategory;
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
            activeBox.Items.Add(Resources .All);
            activeBox.Items.Add(StringResources.Active);
            speedBox.Items.Add(CommonResources.Text.LabelPace);
            speedBox.Items.Add(CommonResources.Text.LabelSpeed);
            doIt.Text = Resources.DoIt;
            correctUI(new Control[] { selectedBox, sendLabel2, pluginBox, doIt });
            sendResultToLabel1.Text = StringResources.Send;
            sendResultToLabel1.Location = new Point(selectedBox.Location.X - 5 - sendResultToLabel1.Size.Width,
                                        sendLabel2.Location.Y);
            labelShow.Location = new Point(activeBox.Location.X - 5 - labelShow.Size.Width,
                                    labelShow.Location.Y);
        }

        private void setCategoryLabel()
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
            changeCategory.Location = new Point(
                    summaryLabel.Location.X + summaryLabel.Width + 5,
                         changeCategory.Location.Y);
            categoryLabel.Location = new Point(
                   changeCategory.Location.X + changeCategory.Width + 5,
                        summaryLabel.Location.Y);
            Refresh();
        }

        private void calculate()
        {
            if (Visible && !doUpdate)
            {
                if (activity != null)
                {
                    summaryList.Visible = false;
                    summaryLabel.Visible = false;
                    changeSettingsVisibility(false);
                    categoryLabel.Visible = false;
                    changeCategory.Visible = false;
                    progressBar.Visible = true;
                    similar = UniqueRoutes.findSimilarRoutes(activity, progressBar);
                    determinePaceOrSpeed();
                    progressBar.Visible = false;
                    summaryLabel.Visible = true;
                    categoryLabel.Visible = true;
                    changeCategory.Visible = true;
                    setTable();
                    changeSettingsVisibility(similar.Count > 1);
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

        void UniqueRoutesActivityDetailView_VisibleChanged(object sender, EventArgs e)
        {
            calculate();
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

        //private void calculate_Click(object sender, EventArgs e)
        //{
        //    calculate();
        //}

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
            new CategorySelector();
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
