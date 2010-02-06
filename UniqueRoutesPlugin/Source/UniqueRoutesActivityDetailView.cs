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
        private IList<IActivity> similar;
        private IActivity activity;
        public IActivity Activity
        {
            set
            {
                this.activity = value;
                summaryLabel.Visible = false;
                summaryView.Visible = false;                        
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
            if (similar != null)
            {
                if (similar.Count > 1)
                {
                    DataTable table = new DataTable();
                    table.Columns.Add(CommonResources.Text.LabelDate);
                    table.Columns.Add(CommonResources.Text.LabelStartTime);
                    table.Columns.Add(UnitUtil.Time.LabelAxis);
                    table.Columns.Add(UnitUtil.Distance.LabelAxis);
                    table.Columns.Add(UnitUtil.PaceOrSpeed.LabelAxis(Settings.ShowPace));
                    table.Columns.Add(CommonResources.Text.LabelAvgHR + UnitUtil.HeartRate.LabelAbbr2);
                    foreach (IActivity activity in similar)
                    {
                        DataRow row = table.NewRow();
                        ActivityInfo info = ActivityInfoCache.Instance.GetInfo(activity);
                        row[0] = activity.StartTime.ToLocalTime().ToShortDateString();
                        row[1] = activity.StartTime.ToLocalTime().ToShortTimeString();
                        row[2] = UnitUtil.Time.ToString(info.Time);
                        row[3] = UnitUtil.Distance.ToString(info.DistanceMeters);
                        row[4] = UnitUtil.PaceOrSpeed.ToString(Settings.ShowPace, info.AverageSpeedMetersPerSecond);
                        row[5] = UnitUtil.HeartRate.ToString(info.AverageHeartRate);
                        table.Rows.Add(row);
                    }
                    summaryView.DataSource = table;
                    summaryLabel.Text = String.Format(Resources.FoundActivities,similar.Count - 1);
                    summaryView.Visible = true;
                    summaryLabel.Visible = true;
                    changeSettingsVisibility(true);
                }
                else
                {
                    summaryLabel.Text = Resources.DidNotFindAnyRoutes.Replace("\\n","\n");
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

        public void changeSettingsVisibility(bool visible)
        {
            label1.Visible = visible;
            selectedBox.Visible = visible;
            speedBox.Visible = visible;
            pluginBox.Visible = visible;
            sendLabel2.Visible = visible;
            doIt.Visible = visible;
            sendResultToLabel1.Visible = visible;
        }

        bool doUpdate;

        public UniqueRoutesActivityDetailView(IActivity activity)
        {
            InitializeComponent();
            correctLanguage();
            doUpdate = true;
            this.Activity = activity;
            this.Resize += new EventHandler(UniqueRoutesActivityDetailView_Resize);
            progressBar.Size = new Size(summaryView.Size.Width, progressBar.Height);
            speedBox.SelectedItem = CommonResources.Text.LabelPace;
            contextMenu.Click += new EventHandler(contextMenu_Click);
            Plugin.GetApplication().SystemPreferences.PropertyChanged += new PropertyChangedEventHandler(SystemPreferences_PropertyChanged);
            setSize();
            VisibleChanged += new EventHandler(UniqueRoutesActivityDetailView_VisibleChanged);
            if (Settings.accumulatedSummary != null)
            {
                pluginBox.Items.Add("Accumulated Summary");
            }
            if (Settings.highScore != null)
            {
                pluginBox.Items.Add("High Score");
            }
            if (Settings.overlay != null)
            {
                pluginBox.Items.Add("Overlay");
            }
            if (Settings.trimp != null)
            {
                pluginBox.Items.Add("TRIMP");
            }
            if (Settings.SelectAll)
            {
                selectedBox.SelectedItem = StringResources.All;
            }
            else
            {
                selectedBox.SelectedItem = StringResources.Selected;
            }
            if (Settings.SelectedPlugin != null &&
                    Settings.SelectedPlugin.Equals("Accumulated Summary") &&
                    Settings.accumulatedSummary != null)
            {
                pluginBox.SelectedItem = Settings.SelectedPlugin;
            }
            else if (Settings.SelectedPlugin != null &&
                        Settings.SelectedPlugin.Equals("High Score") &&
                         Settings.highScore != null)
            {
                pluginBox.SelectedItem = Settings.SelectedPlugin;
            }
            else if (Settings.SelectedPlugin != null &&
                Settings.SelectedPlugin.Equals("Overlay") &&
                Settings.overlay != null)
            {
                pluginBox.SelectedItem = Settings.SelectedPlugin;
            }
            else if (Settings.SelectedPlugin != null &&
                Settings.SelectedPlugin.Equals("TRIMP") &&
                  Settings.trimp != null)
            {
                pluginBox.SelectedItem = Settings.SelectedPlugin;
            }
            else if (Settings.accumulatedSummary != null)
            {
                pluginBox.SelectedItem = "Accumulated Summary";
            }
            else if (Settings.highScore != null)
            {
                pluginBox.SelectedItem = "High Score";
            }
            else if (Settings.overlay != null)
            {
                pluginBox.SelectedItem = "Overlay";
            }
            else if (Settings.trimp != null)
            {
                pluginBox.SelectedItem = "TRIMP";
            }
            else
            {
                pluginBox.SelectedItem = "";
                pluginBox.Enabled = false;
            }
            setCategoryLabel();
            doUpdate = false;
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
            sendResultToLabel1.Text = StringResources.Send;
            label1.Text = StringResources.Show;
            sendLabel2.Text = StringResources.ActivitiesTo;
            selectedBox.Items.Add(StringResources.All);
            selectedBox.Items.Add(StringResources.Selected);
            speedBox.Items.Add(CommonResources.Text.LabelPace);
            speedBox.Items.Add(CommonResources.Text.LabelSpeed);
            doIt.Text = Resources.DoIt;
            changeCategory.Text = StringResources.ChangeCategory;
            correctUI(new Control[] { selectedBox, sendLabel2, pluginBox, doIt });
            sendResultToLabel1.Location = new Point(selectedBox.Location.X - 5 - sendResultToLabel1.Size.Width,
                                        sendLabel2.Location.Y);
            label1.Location = new Point(speedBox.Location.X - 5 - label1.Size.Width,
                                    label1.Location.Y);
        }

        private void setCategoryLabel()
        {
            if (Settings.SelectedCategory == null)
            {
                categoryLabel.Text = Resources.IncludeAllActivitiesInSearch;
            }
            else
            {
                categoryLabel.Text = String.Format(Resources.IncludeOnlyCategory,
                    getCategory(Settings.SelectedCategory, null));
            }
        }

        private string getCategory(IActivityCategory iActivityCategory, string p)
        {
            if (iActivityCategory == null) return p;
            if (p == null) return getCategory(iActivityCategory.Parent,
                                        iActivityCategory.Name);
            return getCategory(iActivityCategory.Parent,
                                iActivityCategory.Name + ": " + p);
        }

        void UniqueRoutesActivityDetailView_VisibleChanged(object sender, EventArgs e)
        {
            calculate();
        }

        void contextMenu_Click(object sender, EventArgs e)
        {
            StringBuilder s = new StringBuilder();
            foreach (DataGridViewColumn column in summaryView.Columns)
            {
                s.Append(column.HeaderText + "\t");
            }
            s.Append("\n");
            foreach (DataGridViewRow row in summaryView.Rows)
            {
                foreach (DataGridViewCell cell in row.Cells)
                {
                    s.Append(cell.Value + "\t");
                }
                s.Append("\n");
            }
            Clipboard.SetText(s.ToString());
        }

        void SystemPreferences_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            setTable();
        }

        private void UniqueRoutesActivityDetailView_Resize(object sender, EventArgs e)
        {
            setSize();
        }

        private void setSize()
        {
            if (summaryView.Columns.Count > 0 && summaryView.Rows.Count > 0)
            {
                summaryView.Size = new Size(Size.Width - summaryView.Location.X - 20,
                       summaryView.Rows[0].Height * summaryView.Rows.Count
                       + summaryView.ColumnHeadersHeight);
                progressBar.Size = new Size(summaryView.Size.Width, progressBar.Height);
                int columnWidth = summaryView.Size.Width / summaryView.Columns.Count;
                foreach (DataGridViewColumn column in summaryView.Columns)
                {
                    column.Width = columnWidth;
                }
                summaryView.Size = new Size(Size.Width - summaryView.Location.X - 20,
                       summaryView.Rows[0].Height * summaryView.Rows.Count
                       + summaryView.ColumnHeadersHeight);                
            }
            changeCategory.Location = new Point(
                    categoryLabel.Location.X + categoryLabel.Width + 5,
                         changeCategory.Location.Y);
            Refresh();
        }

        private IActivity findActivity(string date, string starttime)
        {
            foreach (IActivity activity in similar)
            {
                String s1 = activity.StartTime.ToLocalTime().ToShortDateString();
                String s2 = activity.StartTime.ToLocalTime().ToShortTimeString();
                if (s1.Equals(date) && s2.Equals(starttime))
                    return activity;
            }
            return null;
        }

        private void highScoreButton_Click(object sender, EventArgs e)
        {
            IList<IActivity> list = new List<IActivity>();
            IList<int> seenRows = new List<int>();
            if (selectedBox.SelectedItem.Equals(StringResources.Selected))
            {
                foreach (DataGridViewCell cell in summaryView.SelectedCells)
                {
                    if (!seenRows.Contains(cell.RowIndex))
                    {
                        seenRows.Add(cell.RowIndex);
                        DataGridViewRow row = summaryView.Rows[cell.RowIndex];
                        list.Add(findActivity((string)row.Cells[0].Value,
                                              (string)row.Cells[1].Value));
                    }
                }
            }
            else
            {
                list = similar;
            }
            try
            {
                if (pluginBox.SelectedItem.Equals("Accumulated Summary"))
                {
                    Activator.CreateInstance(Settings.accumulatedSummary, new object[] { list });
                }
                else if (pluginBox.SelectedItem.Equals("High Score"))
                {
                    Activator.CreateInstance(Settings.highScore, new object[] { list, true, true });
                }
                else if (pluginBox.SelectedItem.Equals("Overlay"))
                {
                    Activator.CreateInstance(Settings.overlay, new object[] { list });
                }
                else if (pluginBox.SelectedItem.Equals("TRIMP"))
                {
                    Activator.CreateInstance(Settings.trimp, new object[] { list, true });
                }
            }
            catch (Exception ex)
            {
                new WarningDialog(String.Format(Resources.PluginApplicationError, Settings.SelectedPlugin, ex.ToString()));
            }
        }

        private void calculate_Click(object sender, EventArgs e)
        {
            calculate();
        }

        private void calculate()
        {
            if (activity != null && Visible && !doUpdate)
            {
                progressBar.Visible = true;
                summaryView.Visible = false;
                changeSettingsVisibility(false);
                similar = UniqueRoutes.findSimilarRoutes(activity, progressBar);
                determinePaceOrSpeed();
                progressBar.Visible = false;
                setTable();
                changeSettingsVisibility(similar.Count > 1);
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
            Settings.SelectAll = selectedBox.SelectedItem.Equals(StringResources.All);
        }
    }
}
