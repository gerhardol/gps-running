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

using ZoneFiveSoftware.Common.Data.Fitness;
using ZoneFiveSoftware.Common.Visuals;
using ZoneFiveSoftware.Common.Visuals.Fitness;
using ZoneFiveSoftware.Common.Visuals.Chart;
using GpsRunningPlugin.Properties;
using GpsRunningPlugin.Util;

namespace GpsRunningPlugin.Source
{
    public partial class HighScoreViewer : UserControl
    {
        private readonly Form popupForm;
        private bool includeLocationAndDate = false;
        private readonly bool showDialog = false;
        private GoalParameter domain, image;
        private bool upperBound;
        private IDictionary<GoalParameter, IDictionary<GoalParameter, IDictionary<bool, DataTable>>> cachedTables;
        private IDictionary<DataTable, String> tableFormat;
        private IDictionary<GoalParameter, IDictionary<GoalParameter, IDictionary<bool, IList<Result>>>> cachedResults;
        private String speedUnit;
#if ST_2_1
        private object m_DetailPage = null;
#else
        private IDetailPage m_DetailPage = null;
        private IDailyActivityView m_view = null;

        public HighScoreViewer(IDetailPage detailPage, IDailyActivityView view)
           : this()
        {
            m_DetailPage = detailPage;
            m_view = view;
            if (m_DetailPage != null)
            {
                //expandButton.Visible = true;
            }
        }
        //popup dialog
        public HighScoreViewer(IDailyActivityView view)
            : this(true)
        {
            //m_layer = TrailPointsLayer.Instance((IView)view);
        }
        public HighScoreViewer(IActivityReportsView view)
            : this(true)
        {
            //m_layer = TrailPointsLayer.Instance((IView)view);
        }
#endif
        public HighScoreViewer()
        {
            InitializeComponent();
            InitControls();

            Plugin.GetApplication().SystemPreferences.PropertyChanged += new PropertyChangedEventHandler(SystemPreferences_PropertyChanged);
            dataGrid.CellContentDoubleClick += new DataGridViewCellEventHandler(selectedRow_DoubleClick);

            domainBox.DropDownStyle = ComboBoxStyle.DropDownList;
            imageBox.DropDownStyle = ComboBoxStyle.DropDownList;
            boundsBox.DropDownStyle = ComboBoxStyle.DropDownList;
            paceBox.DropDownStyle = ComboBoxStyle.DropDownList;
            viewBox.DropDownStyle = ComboBoxStyle.DropDownList;

            domainBox.SelectedItem = translateToLanguage(Settings.Domain);
            imageBox.SelectedItem = translateToLanguage(Settings.Image);

            boundsBox.SelectedItem = Settings.UpperBound ? StringResources.Maximal : StringResources.Minimal;
            speedUnit = getMostUsedSpeedUnit(activities);
            paceBox.SelectedItem = speedUnit;
            if (Settings.ShowTable)
                viewBox.SelectedItem = ZoneFiveSoftware.Common.Visuals.CommonResources.Text.LabelList;
            else
                viewBox.SelectedItem = StringResources.Graph;

            domain = Settings.Domain;
            image = Settings.Image;
            upperBound = Settings.UpperBound;

            domainBox.SelectedIndexChanged += new EventHandler(domainBox_SelectedIndexChanged);
            imageBox.SelectedIndexChanged += new EventHandler(imageBox_SelectedIndexChanged);
            boundsBox.SelectedIndexChanged += new EventHandler(boundsBox_SelectedIndexChanged);
            paceBox.SelectedIndexChanged += new EventHandler(paceBox_SelectedIndexChanged);
            viewBox.SelectedIndexChanged += new EventHandler(viewBox_SelectedIndexChanged);

            dataGrid.CellToolTipTextNeeded += new DataGridViewCellToolTipTextNeededEventHandler(dataGrid_CellToolTipTextNeeded);
            contextMenu.Click += new EventHandler(contextMenu_Click);
        }

        public HighScoreViewer(bool showDialog)
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
                Parent.SizeChanged += new EventHandler(Parent_SizeChanged);
                popupForm.StartPosition = FormStartPosition.CenterScreen;
                popupForm.Show();
            }
            else
            {
                SizeChanged += new EventHandler(SizeChanged_handler);
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
            toolStripMenuCopy.Image = ZoneFiveSoftware.Common.Visuals.CommonResources.Images.DocumentCopy16;

            //This will disable gradient header, but make them more like ST controls
            //(required for setting ColumnHeadersDefaultCellStyle.BackColor)
            this.dataGrid.EnableHeadersVisualStyles = false;
            this.dataGrid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.dataGrid.RowsDefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.dataGrid.AdvancedColumnHeadersBorderStyle.All = DataGridViewAdvancedCellBorderStyle.Outset;
        }

        private IList<IActivity> activities = new List<IActivity>();
        public IList<IActivity> Activities
        {
            set
            {
                //Make sure activities is not null
                if (null == value) { activities.Clear(); }
                else { activities = value; }
                if (popupForm != null)
                {
                    if (activities.Count > 1)
                        popupForm.Text = Resources.HSV + " " + String.Format(StringResources.OfManyActivities, activities.Count);
                    else if (activities.Count == 1)
                        popupForm.Text = Resources.HSV + " " + StringResources.OfOneActivity;
                    else
                        popupForm.Text = Resources.HSV + " " + StringResources.OfNoActivities;
                }
                this.includeLocationAndDate = (activities.Count > 1);
                resetCachedResults();
                if (activities.Count > 0)
                {

                    showResults();
                }
            }
        }

        private string translateToLanguage(GoalParameter goalParameter)
        {
            if (goalParameter.Equals(GoalParameter.PulseZone))
                return StringResources.HRZone;
            if (goalParameter.Equals(GoalParameter.SpeedZone))
                return StringResources.SpeedZone;
            if (goalParameter.Equals(GoalParameter.PulseZoneSpeedZone))
                return Resources.HRAndSpeedZones;
            if (goalParameter.Equals(GoalParameter.Distance))
                return CommonResources.Text.LabelDistance;
            if (goalParameter.Equals(GoalParameter.Elevation))
                return CommonResources.Text.LabelElevation;
            if (goalParameter.Equals(GoalParameter.Time))
                return CommonResources.Text.LabelTime;
            return null;
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
            this.chart.ThemeChanged(visualTheme);

            this.splitContainer1.Panel1.BackColor = visualTheme.Control;
            this.splitContainer1.Panel2.BackColor = visualTheme.Control;
            this.dataGrid.BackgroundColor = visualTheme.Control;
            this.dataGrid.GridColor = visualTheme.Border;
            this.dataGrid.DefaultCellStyle.BackColor = visualTheme.Window;
            this.dataGrid.ColumnHeadersDefaultCellStyle.BackColor = visualTheme.SubHeader;
        }

        public void UICultureChanged(System.Globalization.CultureInfo culture)
        {
            Remarks.Text = "";
            label1.Text = StringResources.Find;
            label2.Text = StringResources.PerSpecified;
            label3.Text = ZoneFiveSoftware.Common.Visuals.CommonResources.Text.LabelShow;
            correctUI(new Control[] { boundsBox, domainBox, label2, imageBox });
            label1.Location = new Point(boundsBox.Location.X - 5 - label1.Width, label1.Location.Y);
            correctUI(new Control[] { paceBox, viewBox, Remarks });
            label3.Location = new Point(paceBox.Location.X - 5 - label3.Width, label3.Location.Y);
            toolStripMenuCopy.Text = ZoneFiveSoftware.Common.Visuals.CommonResources.Text.ActionCopy;
            label2.Location = new Point(label2.Location.X, label1.Location.Y);
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
//            if (changed) { makeData(); }
        }
        private void setSize()
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
                    }
                }
            }
        }

        /***********************************************************/

        void contextMenu_Click(object sender, EventArgs e)
        {
            StringBuilder s = new StringBuilder();
            foreach (DataGridViewColumn column in dataGrid.Columns)
            {
                s.Append(column.HeaderText + "\t");
            }
            s.Append(StringResources.Goal);
            s.Append("\n");
            int rowIndex = 0;
            foreach (DataGridViewRow row in dataGrid.Rows)
            {
                foreach (DataGridViewCell cell in row.Cells)
                {
                    s.Append(cell.Value + "\t");
                }
                Goal goal = getGoalFromTable(rowIndex);
                if (goal != null)
                    s.Append(getGoalFromTable(rowIndex).ToString(speedUnit));
                s.Append("\n");
                rowIndex++;
            }
            Clipboard.SetText(s.ToString());
        }

        void SystemPreferences_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName.Equals("DistanceUnits") || e.PropertyName.Equals("ElevationUnits"))
            {
                resetCachedTables();
                showResults();
            }
        }

        private String getLabel(GoalParameter gp)
        {
            switch (gp)
            {
                case GoalParameter.Distance:
                    return UnitUtil.Distance.LabelAxis;
                case GoalParameter.Time:
                    return UnitUtil.Time.LabelAxis;
                case GoalParameter.Elevation:
                    return UnitUtil.Elevation.LabelAxis;
                case GoalParameter.PulseZone:
                    return UnitUtil.HeartRate.LabelAxis;
                case GoalParameter.SpeedZone:
                    if (speedUnit.Equals(CommonResources.Text.LabelPace)) return UnitUtil.Pace.LabelAxis;
                    return UnitUtil.Speed.LabelAxis;
                case GoalParameter.PulseZoneSpeedZone:
                    return UnitUtil.HeartRate.LabelAxis;
            }
            throw new Exception();
        }

        void viewBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            setSettings();
            showResults();
        }

        private double getValue(Result result, GoalParameter gp)
        {
            switch (gp)
            {
                case GoalParameter.Distance:
                    return UnitUtil.Distance.ConvertFrom(result.Meters);
                case GoalParameter.Time:
                    return result.Seconds;
                case GoalParameter.Elevation:
                    return UnitUtil.Elevation.ConvertFrom(result.Elevations);
                case GoalParameter.PulseZone:
                    return result.AveragePulse;
                case GoalParameter.SpeedZone:
                    double speed = result.Meters / result.Seconds;
                    return UnitUtil.PaceOrSpeed.ConvertFrom(speedUnit.Equals(CommonResources.Text.LabelPace), speed);
                case GoalParameter.PulseZoneSpeedZone:
                    return result.AveragePulse;
            }
            throw new Exception();
        }

        private string getMostUsedSpeedUnit(IList<IActivity> activities)
        {
            int speed = 0, pace = 0;
            foreach (IActivity activity in activities)
            {
                if (activity.Category.SpeedUnits.ToString().ToLower().Equals(CommonResources.Text.LabelPace))
                    pace++;
                else speed++;
            }
            if (speed >= pace) return CommonResources.Text.LabelSpeed;
            return CommonResources.Text.LabelPace;
        }

        private void resetCachedTables()
        {
            foreach (GoalParameter domain in Enum.GetValues(typeof(GoalParameter)))
            {
                if (cachedTables[domain] != null)
                {
                    foreach (GoalParameter image in Enum.GetValues(typeof(GoalParameter)))
                    {
                        if (cachedTables[domain][image] != null)
                        {
                            cachedTables[domain][image][true] = null;
                            cachedTables[domain][image][false] = null;
                        }
                    }
                }
            }
        }

        private void resetCachedResults()
        {
            cachedTables = new Dictionary<GoalParameter, IDictionary<GoalParameter, IDictionary<bool, DataTable>>>();
            tableFormat = new Dictionary<DataTable, String>();
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
                        HighScore.generateGoals(domain, image, true, goals);
                        HighScore.generateGoals(domain, image, false, goals);
                    }
                    upperBoundResult.Add(true, null);
                    upperBoundResult.Add(false, null);
                    imageResultCache.Add(image, upperBoundResult);
                  }
                  cachedTables.Add(domain, imageTableCache);
                  cachedResults.Add(domain, imageResultCache);
            }
            progressBar.Minimum = 0;
            progressBar.Maximum = activities.Count;
            progressBar.Value = 0;
            progressBar.Visible = true;
            IList<Result> results = HighScore.calculate(activities, goals, progressBar);
            progressBar.Visible = false;
            foreach (GoalParameter domain in Enum.GetValues(typeof(GoalParameter)))
                foreach (GoalParameter image in Enum.GetValues(typeof(GoalParameter)))
                {
                    if (domain != image &&
                        (domain == GoalParameter.Distance ||
                         domain == GoalParameter.Time ||
                         domain == GoalParameter.Elevation))
                    {
                        cachedResults[domain][image][true] = filter(results, goals, domain, image, true);
                        cachedTables[domain][image][true] = HighScore.generateTable(cachedResults[domain][image][true],
                            speedUnit, includeLocationAndDate, domain, image, true);
                        tableFormat[cachedTables[domain][image][true]] = speedUnit;
                        cachedResults[domain][image][false] = filter(results, goals, domain, image, false);
                        cachedTables[domain][image][false] = HighScore.generateTable(cachedResults[domain][image][false], 
                            speedUnit, includeLocationAndDate, domain, image, false);
                        tableFormat[cachedTables[domain][image][false]] = speedUnit;
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

        private String translateParameter(String str)
        {
            if (str.Equals(CommonResources.Text.LabelDistance)) return "distance";
            if (str.Equals(CommonResources.Text.LabelElevation)) return "elevation";
            if (str.Equals(CommonResources.Text.LabelTime)) return "time";
            if (str.Equals(StringResources.HRZone)) return "pulsezone";
            if (str.Equals(StringResources.SpeedZone)) return "speedzone";
            if (str.Equals(Resources.HRAndSpeedZones)) return "pulsezonespeedzone";
            return null;
        }

        private void setSettings()
        {
            Settings.Domain = (GoalParameter)Enum.Parse(typeof(GoalParameter), 
                        translateParameter((String)domainBox.SelectedItem), true);
            Settings.UpperBound = boundsBox.SelectedItem.Equals(StringResources.Maximal);
            Settings.Image = (GoalParameter)Enum.Parse(typeof(GoalParameter), translateParameter((String)imageBox.SelectedItem), true);
            domain = Settings.Domain;
            image = Settings.Image;
            upperBound = Settings.UpperBound;
            Settings.ShowTable = viewBox.SelectedItem.Equals(ZoneFiveSoftware.Common.Visuals.CommonResources.Text.LabelList);
        }

        private void showResults()
        {
            Remarks.Visible = false;
            dataGrid.Visible = false;
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
            foreach (IActivity activity in activities)
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
            DataTable table = cachedTables[domain][image][upperBound];
            if (table == null)
            {
                IList<Result> results = cachedResults[domain][image][upperBound];
                if (results == null)
                {
                    IList<Goal> goals = HighScore.generateGoals();
                    progressBar.Visible = true;
                    results = HighScore.calculate(activities, goals, progressBar);
                    progressBar.Visible = false;
                }
                table = HighScore.generateTable(results, speedUnit, includeLocationAndDate,
                    Settings.Domain, Settings.Image, Settings.UpperBound);
                cachedTables[domain][image][upperBound] = table;
                tableFormat[table] = speedUnit;
                cachedResults[domain][image][upperBound] = results;
            }
            else if (!tableFormat[table].Equals(speedUnit))
            {
                table = HighScore.generateTable(cachedResults[domain][image][upperBound], speedUnit, includeLocationAndDate,
                    Settings.Domain, Settings.Image, Settings.UpperBound);
                cachedTables[domain][image][upperBound] = table;
                tableFormat[table] = speedUnit;
            }
            if (table.Rows.Count > 0)
            {
                dataGrid.DataSource = table;
                dataGrid.ShowCellToolTips = true;
                foreach (DataGridViewColumn column in dataGrid.Columns)
                {
                    column.SortMode = DataGridViewColumnSortMode.NotSortable;
                }
                dataGrid.Visible = true;
                setSize();
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
                chart.XAxis.Label = getLabel(image);
                chart.YAxis.Label = getLabel(domain);
                results = cachedResults[domain][image][upperBound];
                if (results == null)
                {
                    IList<Goal> goals = HighScore.generateGoals();
                    progressBar.Visible = true;
                    results = HighScore.calculate(activities, goals, progressBar);
                    progressBar.Visible = false;
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
                setAxisType(chart.XAxis, image);
                setAxisType(chart.YAxis, domain);
                foreach (Result result in results)
                {
                    if (result != null)
                    {
                        float x = (float)getValue(result, image);
                        float y = (float)getValue(result, domain);
                        if (!x.Equals(float.NaN) && !float.IsInfinity(y) &&
                        series.Points.IndexOfKey(x) == -1)
                        {
                            series.Points.Add(x, new PointF(x, y));
                        }
                    }
                }
                chart.DataSeries.Add(series);
                chart.AutozoomToData(true);
                setSize();
                chart.Visible = true;
            }
        }

        private void setAxisType(IAxis axis, GoalParameter goal)
        {
            switch (goal)
            {
                case GoalParameter.Time:
                    axis.Formatter = new Formatter.SecondsToTime(); return;
                case GoalParameter.Distance:
                    axis.Formatter = new Formatter.General(UnitUtil.Distance.DefaultDecimalPrecision); return;
                case GoalParameter.Elevation:
                    axis.Formatter = new Formatter.General(UnitUtil.Elevation.DefaultDecimalPrecision); return;
                case GoalParameter.SpeedZone:
                    //TBD: This is likely not used
                    ArrayList categories = new ArrayList();
                    ArrayList keys = new ArrayList();
                    int index = 0;
                    foreach (double from in Settings.speedZones.Keys)
                    {
                        foreach (double to in Settings.speedZones[from].Keys)
                        {
                            categories.Add(UnitUtil.Speed.ToString(from) + "-" + UnitUtil.Speed.ToString(to));
                            keys.Add(index++);
                        }
                    }
                    axis.Formatter = new Formatter.Category(categories, keys);
                    return;
                default:
                    axis.Formatter = new Formatter.General(); return;
            }
        }

        private void dataGrid_CellToolTipTextNeeded(object sender, DataGridViewCellToolTipTextNeededEventArgs e)
        {
            Goal goal = getGoalFromTable(e.RowIndex);
            if (goal != null)
                e.ToolTipText = StringResources.Goal + ": " + goal.ToString(speedUnit);
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

        void SizeChanged_handler(object sender, EventArgs e)
        {
            setSize();
        }
        void Parent_SizeChanged(object sender, EventArgs e)
        {
            if (popupForm != null)
            {
                Settings.WindowSize = popupForm.Size;
            }
            SizeChanged_handler(sender, e);
        }

        private void selectedRow_DoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            Guid view = new Guid("1dc82ca0-88aa-45a5-a6c6-c25f56ad1fc3");

            int rowIndex = e.RowIndex;
            if (rowIndex >= 0 && dataGrid.Columns[ActivityIdColumn] != null)
            {
                object id = dataGrid.Rows[rowIndex].Cells[ActivityIdColumn].Value;
                if (id != null)
                {
                    string bookmark = "id=" + id;
                    Plugin.GetApplication().ShowView(view, bookmark);
                }
            }
        }

        public const string ActivityIdColumn = "ActivityId";
    }
}
