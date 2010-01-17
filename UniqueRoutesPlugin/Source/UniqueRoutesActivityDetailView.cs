using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using ZoneFiveSoftware.Common.Data.Fitness;
using SportTracksUniqueRoutesPlugin.Source;
using SportTracksUniqueRoutesPlugin;
using System.Reflection.Emit;
using System.Reflection;
using System.IO;
using SportTracksUniqueRoutesPlugin.Properties;

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
                    table.Columns.Add(Resources.Date);
                    table.Columns.Add(Resources.StartTime);
                    table.Columns.Add(Resources.Time);
                    table.Columns.Add(Resources.Distance+ " (" + Settings.DistanceUnitShort + ")");
                    if (Settings.ShowPace)
                        table.Columns.Add(String.Format(Resources.Pace,Settings.DistanceUnitShort));
                    else
                        table.Columns.Add(String.Format(Resources.Speed,Settings.DistanceUnitShort));
                    table.Columns.Add(Resources.AvgHR);
                    foreach (IActivity activity in similar)
                    {
                        DataRow row = table.NewRow();
                        ActivityInfo info = ActivityInfoCache.Instance.GetInfo(activity);
                        row[0] = String.Format("{0:yyyy/MM/dd}", activity.StartTime.ToLocalTime());
                        row[1] = activity.StartTime.ToLocalTime().ToShortTimeString();
                        row[2] = info.Time.ToString();
                        row[3] = String.Format("{0:0.000}", Settings.convertFromDistance(info.DistanceMeters));
                        if (Settings.ShowPace)
                            row[4] = new TimeSpan(0, 0, (int)(1 / Settings.convertFromDistance(info.AverageSpeedMetersPerSecond))).ToString().Substring(3);
                        else
                            row[4] = String.Format("{0:0.0}", Settings.convertFromDistance(info.AverageSpeedMetersPerSecond * 60 * 60));
                        row[5] = Settings.present(info.AverageHeartRate, 1);
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
            Settings.dontSave = true;
            this.Activity = activity;
            this.Resize += new EventHandler(UniqueRoutesActivityDetailView_Resize);
            progressBar.Size = new Size(summaryView.Size.Width, progressBar.Height);
            speedBox.SelectedItem = Resources.Pace;
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
                selectedBox.SelectedItem = Resources.All;
            }
            else
            {
                selectedBox.SelectedItem = Resources.Selected;
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
            Settings.dontSave = false;
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
            sendResultToLabel1.Text = Resources.Send;
            label1.Text = Resources.Show;
            sendLabel2.Text = Resources.ActivitiesTo;
            selectedBox.Items.Add(Resources.All);
            selectedBox.Items.Add(Resources.Selected);
            speedBox.Items.Add(Resources.Pace);
            speedBox.Items.Add(Resources.Speed);
            doIt.Text = Resources.DoIt;
            changeCategory.Text = Resources.ChangeCategory;
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
                String s1 = String.Format("{0:yyyy/MM/dd}", activity.StartTime.ToLocalTime());
                String s2 = activity.StartTime.ToLocalTime().ToShortTimeString().ToString();
                if (s1.Equals(date) && s2.Equals(starttime))
                    return activity;
            }
            return null;
        }

        private void highScoreButton_Click(object sender, EventArgs e)
        {
            IList<IActivity> list = new List<IActivity>();
            IList<int> seenRows = new List<int>();
            if (selectedBox.SelectedItem.Equals(Resources.Selected))
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
            Settings.ShowPace = speedBox.SelectedItem.Equals(Resources.Pace);
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
    }
}
