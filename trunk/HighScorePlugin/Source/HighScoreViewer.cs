using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using ZoneFiveSoftware.Common.Data.Fitness;
using System.Collections;
using ZoneFiveSoftware.Common.Visuals.Fitness;
using ZoneFiveSoftware.Common.Visuals.Chart;
using SportTracksHighScorePlugin.Properties;

namespace SportTracksHighScorePlugin.Source
{
    public partial class HighScoreViewer : UserControl
    {
        private readonly Form form;
        
        private readonly bool includeLocationAndDate, showDialog;

        private IList<IActivity> activities;
        public IList<IActivity> Activities
        {
            set
            {
                activities = value;
                if (form != null)
                {
                    if (activities.Count > 1)
                        form.Text = String.Format(Resources.HSV1,activities.Count);
                    else if (activities.Count == 1)
                        form.Text = Resources.HSV2;
                    else
                        form.Text = Resources.HSV3;
                }
                resetCachedResults();
                showResults();
            }
        }

        private GoalParameter domain, image;

        private bool upperBound;

        private IDictionary<GoalParameter, IDictionary<GoalParameter, IDictionary<bool, DataTable>>> cachedTables;

        private IDictionary<DataTable, String> tableFormat;

        private IDictionary<GoalParameter, IDictionary<GoalParameter, IDictionary<bool, IList<Result>>>> cachedResults;

        private String speedUnit;

        public HighScoreViewer(IList<IActivity> activities, bool includeLocationAndDate, bool showDialog)
        {
            InitializeComponent();

            convertLanguage();

            Plugin.GetApplication().SystemPreferences.PropertyChanged += new PropertyChangedEventHandler(SystemPreferences_PropertyChanged);
            
            this.showDialog = showDialog;
            this.includeLocationAndDate = includeLocationAndDate;

            Remarks.Text = "";
            domainBox.DropDownStyle = ComboBoxStyle.DropDownList;
            imageBox.DropDownStyle = ComboBoxStyle.DropDownList;
            boundsBox.DropDownStyle = ComboBoxStyle.DropDownList;
            paceBox.DropDownStyle = ComboBoxStyle.DropDownList;
            viewBox.DropDownStyle = ComboBoxStyle.DropDownList;

            domainBox.SelectedItem = translateToLanguage(Settings.Domain);
            imageBox.SelectedItem = translateToLanguage(Settings.Image);
            
            boundsBox.SelectedItem = Settings.UpperBound ? Resources.LowerCaseMaximal : Resources.LowerCaseMinimal;
            speedUnit = getMostUsedSpeedUnit(activities);
            paceBox.SelectedItem = speedUnit;
            if (Settings.ShowTable)
                viewBox.SelectedItem = Resources.LowerCaseTable;
            else
                viewBox.SelectedItem = Resources.LowerCaseGraph;

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

            if (showDialog)
            {
                form = new Form();
                form.Controls.Add(this);
                form.Size = Settings.WindowSize;
                form.Icon = Icon.FromHandle(Properties.Resources.Image_32_HighScore.GetHicon());
                Parent.SizeChanged += new EventHandler(SizeChanged_handler);
                form.StartPosition = FormStartPosition.CenterScreen;
                progressBar.Size = new Size(Size.Width - 20, progressBar.Height);
                form.Show();
            }
            else
            {
                SizeChanged += new EventHandler(SizeChanged_handler);
            }           
            Activities = activities;
        }

        private string translateToLanguage(GoalParameter goalParameter)
        {
            if (goalParameter.Equals(GoalParameter.PulseZone))
                return Resources.HRZone;
            if (goalParameter.Equals(GoalParameter.SpeedZone))
                return Resources.SpeedZone;
            if (goalParameter.Equals(GoalParameter.PulseZoneSpeedZone))
                return Resources.HRAndSpeedZones;
            if (goalParameter.Equals(GoalParameter.Distance))
                return Resources.DomDistance;
            if (goalParameter.Equals(GoalParameter.Elevation))
                return Resources.DomElevation;
            if (goalParameter.Equals(GoalParameter.Time))
                return Resources.DomTime;
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

        private void convertLanguage()
        {
            paceBox.Items.Add(Resources.LowerCasePace);
            paceBox.Items.Add(Resources.LowerCaseSpeed);
            viewBox.Items.Add(Resources.LowerCaseGraph);
            viewBox.Items.Add(Resources.LowerCaseTable);
            label1.Text = Resources.Find;
            label2.Text = Resources.PerSpecified;
            label3.Text = Resources.Show;
            correctUI(new Control[] { boundsBox, domainBox, label2, imageBox });
            label1.Location = new Point(boundsBox.Location.X - 5 - label1.Width, label1.Location.Y);
            correctUI(new Control[] { paceBox, viewBox, Remarks });
            label3.Location = new Point(paceBox.Location.X - 5 - label3.Width, label3.Location.Y);
            boundsBox.Items.Add(Resources.LowerCaseMinimal);
            boundsBox.Items.Add(Resources.LowerCaseMaximal);
            domainBox.Items.Add(Resources.DomDistance);
            domainBox.Items.Add(Resources.DomElevation);
            domainBox.Items.Add(Resources.DomTime);
            imageBox.Items.Add(Resources.DomDistance);
            imageBox.Items.Add(Resources.DomElevation);
            imageBox.Items.Add(Resources.DomTime);
            imageBox.Items.Add(Resources.HRZone);
            imageBox.Items.Add(Resources.HRAndSpeedZones);
            toolStripMenuItem1.Text = Resources.CopyTable;
            label2.Location = new Point(label2.Location.X, label1.Location.Y);
        }

        void contextMenu_Click(object sender, EventArgs e)
        {
            StringBuilder s = new StringBuilder();
            foreach (DataGridViewColumn column in dataGrid.Columns)
            {
                s.Append(column.HeaderText + "\t");
            }
            s.Append(Resources.Goal);
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
            String dMetric = Settings.DistanceUnitShort;
            String eMetric = Settings.ElevationUnitShort;
            switch (gp)
            {
                case GoalParameter.Distance:
                    return Resources.Distance+" (" +dMetric + ")";
                case GoalParameter.Time:
                    return Resources.TimeHours;
                case GoalParameter.Elevation:
                    return Resources.Elevation+" (" + eMetric + ")";
                case GoalParameter.PulseZone:
                    return Resources.HR;
                case GoalParameter.SpeedZone:
                    if (speedUnit.Equals(Resources.LowerCaseSpeed)) return String.Format(Resources.Speed2,dMetric);
                    else return String.Format(Resources.Pace2,dMetric);
                case GoalParameter.PulseZoneSpeedZone:
                    return Resources.HR;
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
                    return HighScore.convertFromDistance(result.Meters);
                case GoalParameter.Time:
                    return result.Seconds / 60.0;
                case GoalParameter.Elevation:
                    return HighScore.convertFromElevation(result.Elevations);
                case GoalParameter.PulseZone:
                    return result.AveragePulse;
                case GoalParameter.SpeedZone:
                    if (speedUnit.Equals(Resources.LowerCaseSpeed)) return HighScore.convertSpeed(result);
                    else return HighScore.convertPace(result)/60;
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
                if (activity.Category.SpeedUnits.ToString().ToLower().Equals("speed"))
                    speed++;
                else pace++;
            }
            if (speed >= pace) return Resources.LowerCaseSpeed;
            return Resources.LowerCasePace;
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
            if (str.Equals(Resources.DomDistance)) return "distance";
            if (str.Equals(Resources.DomElevation)) return "elevation";
            if (str.Equals(Resources.DomTime)) return "time";
            if (str.Equals(Resources.HRZone)) return "pulsezone";
            if (str.Equals(Resources.SpeedZone)) return "speedzone";
            if (str.Equals(Resources.HRAndSpeedZones)) return "pulsezonespeedzone";
            return null;
        }

        private void setSettings()
        {
            Settings.Domain = (GoalParameter)Enum.Parse(typeof(GoalParameter), 
                        translateParameter((String)domainBox.SelectedItem), true);
            Settings.UpperBound = boundsBox.SelectedItem.Equals(Resources.LowerCaseMaximal);
            Settings.Image = (GoalParameter)Enum.Parse(typeof(GoalParameter), translateParameter((String)imageBox.SelectedItem), true);
            domain = Settings.Domain;
            image = Settings.Image;
            upperBound = Settings.UpperBound;
            Settings.ShowTable = viewBox.SelectedItem.Equals(Resources.LowerCaseTable);
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
            if (viewBox.SelectedItem.Equals(Resources.LowerCaseGraph))
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
                if (activity.HasStartTime)
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
                        Remarks.Text += " " + Resources.And + " ";
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
                dataGrid.Visible = true;
                dataGrid.DataSource = table;
                dataGrid.ShowCellToolTips = true;
                foreach (DataGridViewColumn column in dataGrid.Columns)
                    column.SortMode = DataGridViewColumnSortMode.NotSortable;
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
            chart.DataSeries.Clear();
            chart.XAxis.Label = getLabel(image);
            chart.YAxis.Label = getLabel(domain);
            IList<Result> results = cachedResults[domain][image][upperBound];
            if (results == null)
            {
                IList<Goal> goals = HighScore.generateGoals();
                progressBar.Visible = true;
                results = HighScore.calculate(activities, goals, progressBar);
                progressBar.Visible = false;
            }
            if (results == null || 
                !(image == GoalParameter.Distance ||
                image == GoalParameter.Time ||
                image == GoalParameter.Elevation))
            {
                Remarks.Text = Resources.NoResultsForSettings;
                Remarks.Visible = true;
            }
            else if (
                image == GoalParameter.Distance ||
                image == GoalParameter.Time ||
                image == GoalParameter.Elevation)
            {
                ChartDataSeries series = new ChartDataSeries(chart, chart.YAxis);
                setAxisType(chart.XAxis, image);
                setAxisType(chart.YAxis, domain);
                int index = 0;
                foreach (Result result in results)
                {
                    if (result != null)
                    {
                        series.Points.Add(
                            index++,
                            new PointF((float)getValue(result, image),
                                       (float)getValue(result, domain)));
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
                case GoalParameter.SpeedZone:
                    ArrayList categories = new ArrayList();
                    ArrayList keys = new ArrayList();
                    int index = 0;
                    foreach (double from in Settings.speedZones.Keys)
                    {
                        foreach (double to in Settings.speedZones[from].Keys)
                        {
                            categories.Add(HighScore.present(from, 1) + "-" +
                                HighScore.present(to, 1));
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
                e.ToolTipText = Resources.Goal+": " + goal.ToString(speedUnit);
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

        private void setSize()
        {
            if (showDialog && Parent != null)
            {
                Size = new Size(Parent.Size.Width - 10, //paceBox.Location.X + paceBox.Width),
                                Parent.Size.Height - 10);
                progressBar.Size = new Size(Size.Width, progressBar.Height);
                Settings.WindowSize = new Size(Parent.Size.Width, Parent.Size.Height);
            }
            if (dataGrid.Columns.Count > 0 && dataGrid.Rows.Count > 0)
            {
                int columnWidth = 
                    ((Size.Width - dataGrid.Location.X - 10)
                    / dataGrid.Columns.Count);
                foreach (DataGridViewColumn column in dataGrid.Columns)
                {
                    column.Width = columnWidth;
                }
                dataGrid.Size = new Size(
                    Size.Width - dataGrid.Location.X - 15,
                    dataGrid.Rows[0].Height * dataGrid.Rows.Count
                    + dataGrid.ColumnHeadersHeight + 2);
            }
            chart.Size = new Size(Size.Width - chart.Location.X - 10, 
                Size.Height - chart.Location.Y - 30);
        }
    }
}
