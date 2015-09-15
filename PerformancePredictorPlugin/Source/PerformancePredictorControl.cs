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
    public partial class PerformancePredictorControl : UserControl
    {
        private IActivity m_lastActivity = null;
#if ST_2_1
        private const object m_DetailPage = null;
#else
        private IDetailPage m_DetailPage = null;
        private IDailyActivityView m_view = null;
        private TrailPointsLayer m_layer = null;
#endif
        private Form m_popupForm = null;

        private bool m_showPage = false;
        private IList<IActivity> m_activities = new List<IActivity>();
        //override used part
        private TimeSpan? m_time = null;
        private double? m_distance = null;
        private bool m_insertedSources = false;

#if !ST_2_1
        //Activity page
        internal PerformancePredictorControl(IDetailPage detailPage, IDailyActivityView view)
            : this()
        {
            m_DetailPage = detailPage;
            m_view = view;
            m_layer = TrailPointsLayer.Instance(m_view);
            if (m_DetailPage != null)
            {
                //expandButton.Visible = true;
            }
            this.predictorView.InitControls(m_DetailPage, m_view, m_layer, this);
            this.trainingView.InitControls(m_DetailPage, m_view, m_layer, this);
            this.extrapolateView.InitControls(m_DetailPage, m_view, m_layer, this);
        }

        //Analyze popup dialog
        internal PerformancePredictorControl(IDailyActivityView view)
            : this(true)
        {
            m_view = view;
            m_layer = TrailPointsLayer.Instance((IView)view);
            this.predictorView.InitControls(m_DetailPage, m_view, m_layer, this);
            this.trainingView.InitControls(m_DetailPage, m_view, m_layer, this);
            this.extrapolateView.InitControls(m_DetailPage, m_view, m_layer, this);
        }
        internal PerformancePredictorControl(IActivityReportsView view)
            : this(true)
        {
            m_layer = TrailPointsLayer.Instance((IView)view);
            this.predictorView.InitControls(m_DetailPage, m_view, m_layer, this);
            this.trainingView.InitControls(m_DetailPage, m_view, m_layer, this);
            this.extrapolateView.InitControls(m_DetailPage, m_view, m_layer, this);
        }

        //UniqueRoutes sendto
        public PerformancePredictorControl(IList<IActivity> activities, IDailyActivityView view)
            : this(view)
        {
            this.Activities = activities;
            ShowPage("");
        }
        public PerformancePredictorControl(IList<IActivity> activities, IActivityReportsView view)
            : this(view)
        {
            this.Activities = activities;
            ShowPage("");
        }

        //Trails sendto
        internal PerformancePredictorControl(IList<IActivity> activities, IList<IItemTrackSelectionInfo> selections, IDailyActivityView view, System.Windows.Forms.ProgressBar progressBar)
            : this(view)
        {
            this.m_insertedSources = true;
            this.Activities = activities;
            IList<IItemTrackSelectionInfo> sels = TrailsItemTrackSelectionInfo.SetAndAdjustFromSelectionFromST(selections, activities);
            IList<TimePredictionSource> source = new List<TimePredictionSource>();
            foreach(IItemTrackSelectionInfo t in sels)
            {
                source.Add(new TimePredictionSource(t));
            }
            this.predictorView.SetCalculation(source);
            //Set the time / distance displayed
            this.predictorView.setData();
            ShowPage("");
        }
#endif
        private PerformancePredictorControl()
        {
            InitializeComponent();
            InitControls();
        }

        private PerformancePredictorControl(bool showDialog)
            : this()
        {
            if (showDialog)
            {
                //Theme and Culture must be set manually
                this.ThemeChanged(
#if ST_2_1
Plugin.GetApplication().VisualTheme);
#else
Plugin.GetApplication().SystemPreferences.VisualTheme);
#endif
                this.UICultureChanged(
#if ST_2_1
new System.Globalization.CultureInfo("en"));
#else
Plugin.GetApplication().SystemPreferences.UICulture);
#endif
                m_popupForm = new Form();
                m_popupForm.Controls.Add(this);
                m_popupForm.Size = Settings.WindowSize;
                //Fill would be simpler here, but then edges are cut
                this.Size = new Size(Parent.Size.Width - 17, Parent.Size.Height - 38);
                this.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right | System.Windows.Forms.AnchorStyles.Bottom)));
                //Parent.SizeChanged += new EventHandler(Parent_SizeChanged);

                m_popupForm.StartPosition = FormStartPosition.CenterScreen;
                m_popupForm.Icon = Icon.FromHandle(Properties.Resources.Image_32_PerformancePredictor.GetHicon());
                m_popupForm.FormClosed += new FormClosedEventHandler(popupForm_FormClosed);
                m_popupForm.Show();
            }
        }

        void InitControls()
        {
            this.modelComboBox.ButtonImage = Properties.Resources.DropDown;
            syncToolBarToState();

            UpdateToolBar();
            int i = this.bannerContextMenuStrip.Items.IndexOf(this.modelMenuItem);
            foreach (PredictionModel m in PredictionModelUtil.List)
            {
                System.Windows.Forms.ToolStripMenuItem menuItem = new ToolStripMenuItem();
                menuItem.Name = m.ToString();
                menuItem.Size = new System.Drawing.Size(211, 22);
                menuItem.Text = menuItem.Name; //Set in UICultureChanged
                menuItem.Tag = m;
                menuItem.Click += new System.EventHandler(predictModelMenuItem_Click);
                this.bannerContextMenuStrip.Items.Insert(++i, menuItem );
            }
        }

        private static ITheme m_visualTheme;
        public void ThemeChanged(ITheme visualTheme)
        {
            m_visualTheme = visualTheme;
 
            //Set color for non ST controls
            this.splitContainer1.Panel1.BackColor = visualTheme.Control;
            this.splitContainer1.Panel2.BackColor = visualTheme.Control;
            this.distanceTextBox.ThemeChanged(visualTheme);
            this.timeTextBox.ThemeChanged(visualTheme);
            this.modelComboBox.ThemeChanged(visualTheme);

            this.actionBanner1.ThemeChanged(visualTheme);
            this.predictorView.ThemeChanged(visualTheme);
            this.trainingView.ThemeChanged(visualTheme);
            this.extrapolateView.ThemeChanged(visualTheme);
        }

        public void UICultureChanged(System.Globalization.CultureInfo culture)
        {
            settingsBox.Text = StringResources.Settings;
            settingsMenuItem.Text = settingsBox.Text;
            timePredictionButton.Text = Resources.TimePrediction;
            timePredictionMenuItem.Text = timePredictionButton.Text;
            trainingButton.Text = StringResources.Training;
            trainingMenuItem.Text = trainingButton.Text;
            extrapolateButton.Text = Properties.Resources.Extrapolate;
            extrapolateMenuItem.Text = extrapolateButton.Text;

            modelBox.Text = Resources.PredictionModel;
            this.modelMenuItem.Text = modelBox.Text;
            foreach (ToolStripItem t in this.bannerContextMenuStrip.Items)
            {
                if (t.Tag is PredictionModel)
                {
                    PredictionModel m = (PredictionModel)t.Tag;
                    t.Text = PredictionModelUtil.Name(m);
                }
            }
            
            velocityBox.Text = Resources.Velocity;
            velocityMenuItem.Text = velocityBox.Text;
            paceButton.Text = CommonResources.Text.LabelPace;
            paceMenuItem.Text = paceButton.Text;
            speedButton.Text = CommonResources.Text.LabelSpeed;
            speedMenuItem.Text = speedButton.Text;

            resultBox.Text = Resources.PredictionResults;
            resultMenuItem.Text = resultBox.Text;
            tableButton.Text = Resources.ViewInTable;
            tableMenuItem.Text = tableButton.Text;
            chartButton.Text = Resources.ViewInChart;
            chartMenuItem.Text = chartButton.Text;

            this.chkHighScoreBox.Text = Properties.Resources.HighScorePrediction;
            this.chkHighScoreMenuItem.Text = this.chkHighScoreBox.Text;
            showToolBarMenuItem.Text = StringResources.Menu_ShowToolBar;
            this.overrideGroupBox.Text =Resources.Override;

            predictorView.UICultureChanged(culture);
            trainingView.UICultureChanged(culture);
            extrapolateView.UICultureChanged(culture);
        }

        public IList<IActivity> Activities
        {
            get { return m_activities; }
            set
            {
                //Make sure activities is not null
                deactivateListeners();
                if (null == value) { m_activities.Clear(); }
                else { m_activities = value; }
                //Reset "special selection"
                m_time = null;
                m_distance = null;

                if (1 == m_activities.Count && m_activities[0] != null)
                {
                    m_lastActivity = m_activities[0];
                }
                else
                {
                    m_lastActivity = null;
                }

                //title cant be set directly on activity page
                if (null != m_popupForm)
                {
                    string title = Resources.ApplicationName;
                    if (m_activities.Count > 0)
                    {
                        if (m_activities.Count == 1)
                        {
                            title += " " + StringResources.ForOneActivity;
                        }
                        else
                        {
                            //TODO: Trails can have many "activities" but only one used activity
                            title += " " + String.Format(StringResources.ForManyActivities, m_activities.Count);
                        }
                    }
                    m_popupForm.Text = title;
                }

                this.predictorView.ClearCalculations();

                activateListeners();
                if (m_layer != null)
                {
                    m_layer.ClearOverlays();
                }
                setView();
                syncToolBarToState();
            }
        }

        //For extrapolate/training view, where certain data is dependent on an activity, nothing major if it is changed
        public IActivity SingleActivity
        {
            get
            {
                if (m_lastActivity != null)
                    {
                    return m_lastActivity;
                }
                else if (m_activities.Count > 0)
                {
                    return m_activities[0];
                }
                else if (Plugin.GetApplication().Logbook.Activities.Count > 0)
                {
                    //The last - maybe latest
                    return Plugin.GetApplication().Logbook.Activities[Plugin.GetApplication().Logbook.Activities.Count-1];
                }

                return null;
            }
        }

        internal TimeSpan Time
        {
            get
            {
                if (this.m_time != null)
                {
                    return (TimeSpan)this.m_time;
                }
                else if(predictorView.Time > TimeSpan.Zero)
                {
                    return predictorView.Time;
                }
                else if (this.SingleActivity != null)
                {
                    ActivityInfo info = ActivityInfoCache.Instance.GetInfo(this.SingleActivity);
                    return info.Time;
                }
                return TimeSpan.FromSeconds(0);
            }
            set
            {
                if (value.TotalSeconds > 0 && !this.m_time.Equals(value))
                {
                    if (this.m_distance == null)
                    {
                        this.m_distance = this.Distance;
                    }
                    this.m_time = value;
                    this.setView();
                    syncToolBarToState();
                }
            }
        }

        internal double Distance
        {
            get
            {
                if (this.m_distance != null)
                {
                    return (double)this.m_distance;
                }
                else if (predictorView.Distance > 0)
                {
                    return predictorView.Distance;
                }
                else if (this.SingleActivity != null)
                {
                    ActivityInfo info = ActivityInfoCache.Instance.GetInfo(this.SingleActivity);
                    return info.DistanceMeters;
                }
                return 0;
            }
            set
            {
                if (!double.IsNaN(value) && value > 0)
                {
                    if (this.m_time == null)
                    {
                        //Setting will recalc, need to set current value..
                        this.m_time = this.Time;
                    }
                    this.m_distance = value;
                    this.setView();
                    syncToolBarToState();
                }
            }
        }

        internal bool IsOverridden { get { return (this.m_time != null || this.m_distance != null); } }

        internal bool IsExternalSource { get { return m_insertedSources; } }
#if ST_2_1
        private void dataChanged(object sender, ZoneFiveSoftware.Common.Data.NotifyDataChangedEventArgs e)
#else
        private void Activity_PropertyChanged(object sender, PropertyChangedEventArgs e)
#endif
        {
            if (this.InvokeRequired)
            {
                this.Invoke((System.ComponentModel.PropertyChangedEventHandler)Activity_PropertyChanged, sender, e);
            }
            else
            {
                if (m_showPage)
                {
                    this.predictorView.RefreshData();
                }
            }
        }

        public bool HidePage()
        {
            m_showPage = false;
            deactivateListeners();
            predictorView.HidePage();
            trainingView.HidePage();
            extrapolateView.HidePage();
            if (m_layer != null)
            {
                m_layer.HidePage();
            }
            return true;
        }

        public void ShowPage(string bookmark)
        {
            m_showPage = true;
            //Show views active
            setView();
            syncToolBarToState();
            activateListeners();
            if (m_layer != null)
            {
                m_layer.ShowPage(bookmark);
            }
        }

        private void activateListeners()
        {
            if (m_showPage && m_lastActivity != null)
            {
#if ST_2_1
                m_lastActivity.DataChanged += new ZoneFiveSoftware.Common.Data.NotifyDataChangedEventHandler(dataChanged);
#else
                m_lastActivity.PropertyChanged += new PropertyChangedEventHandler(Activity_PropertyChanged);
#endif
            }
        }

        private void deactivateListeners()
        {
            if (m_lastActivity != null)
            {
#if ST_2_1
                m_lastActivity.DataChanged -= new ZoneFiveSoftware.Common.Data.NotifyDataChangedEventHandler(dataChanged);
#else
                m_lastActivity.PropertyChanged -= new PropertyChangedEventHandler(Activity_PropertyChanged);
#endif
            }
        }

        void popupForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.HidePage();
        }

        private void setView()
        {
            bool show = this.m_showPage;
            this.m_showPage = false;
            this.predictorView.HidePage();
            this.trainingView.HidePage();
            this.extrapolateView.HidePage();
            this.m_showPage = show;

            this.tableButton.Enabled = false;
            this.tableButton.Checked = true;
            this.chkHighScoreBox.Enabled = false;

            if (Settings.PredictionView == PredictionView.TimePrediction)
            {
                this.timePredictionButton.Checked = true;
                this.actionBanner1.Text = Properties.Resources.TimePrediction;
                this.chkHighScoreBox.Enabled = Settings.HighScore != null && !this.IsOverridden && !this.IsExternalSource;
                this.tableButton.Enabled = true;
                this.tableButton.Checked = !Settings.ShowChart;
                if (this.m_showPage)
                {
                    this.predictorView.ShowPage("");
                }
            }
            else if (Settings.PredictionView == PredictionView.Training)
                {
                    this.actionBanner1.Text = StringResources.Training;
                    this.trainingButton.Checked = true;
                    if (this.m_showPage)
                    {
                        this.trainingView.ShowPage("");
                    }
                }
            else if (Settings.PredictionView == PredictionView.Extrapolate)
            {
                this.actionBanner1.Text = Properties.Resources.Extrapolate;
                this.extrapolateButton.Checked = true;
                if (this.m_showPage)
                {
                    this.extrapolateView.ShowPage("");
                }
            }
            else
            {
                this.actionBanner1.Text = "Unknown";
            }

            //Dependent
            chartButton.Checked = !tableButton.Checked;
            chartButton.Enabled = tableButton.Enabled;
        }

        private void UpdateToolBar()
        {
             
            if (Settings.ShowToolBar)
            {
                this.splitContainer1.SplitterDistance = 145;
            }
            else
            {
                this.splitContainer1.SplitterDistance = 0;
            }
        }

        private void syncToolBarToState()
        {
            timePredictionButton.Checked = (Settings.PredictionView == PredictionView.TimePrediction);
            trainingButton.Checked = (Settings.PredictionView == PredictionView.Training);
            extrapolateButton.Checked = (Settings.PredictionView == PredictionView.Extrapolate);

            this.modelComboBox.Text = PredictionModelUtil.Name(Settings.Model);
            if (this.Time.TotalSeconds > 0)
            {
                this.timeTextBox.Text = UnitUtil.Time.ToString(this.Time, "u");
            }
            else
            {
                this.timeTextBox.Text = "";
            }
            if (this.Distance > 0)
            {
                this.distanceTextBox.Text = UnitUtil.Distance.ToString(this.Distance, "u");
            }
            else
            {
                this.distanceTextBox.Text = "";
            }
            if (!this.IsOverridden)
            {
                this.timeTextBox.Font = new System.Drawing.Font(this.timeTextBox.Font, FontStyle.Italic);
                this.distanceTextBox.Font = new System.Drawing.Font(this.timeTextBox.Font, FontStyle.Italic);
            }
            else
            {
                this.timeTextBox.Font = new System.Drawing.Font(this.timeTextBox.Font, FontStyle.Regular);
                this.distanceTextBox.Font = new System.Drawing.Font(this.timeTextBox.Font, FontStyle.Regular);
            }

            paceButton.Checked = Settings.ShowPace;
            speedButton.Checked = !Settings.ShowPace;

            tableButton.Checked = !Settings.ShowChart;
            chartButton.Checked = !tableButton.Checked;
            chartButton.Enabled = tableButton.Enabled;
        }

        public bool ChkHighScore
        {
            get
            {
                return chkHighScoreBox.Checked;
            }
        }

        /**************************************************/

        private void timePrediction_Click(object sender, EventArgs e)
        {
            if (m_showPage && (
                sender is RadioButton && timePredictionButton.Checked ||
                sender is ToolStripMenuItem && !timePredictionMenuItem.Checked))
            {
                Settings.PredictionView = PredictionView.TimePrediction;
                syncToolBarToState();
                setView();
            }
        }

        private void training_Click(object sender, EventArgs e)
        {
            if (m_showPage && (
                sender is RadioButton && trainingButton.Checked ||
                sender is ToolStripMenuItem && !trainingMenuItem.Checked))
            {
                Settings.PredictionView = PredictionView.Training;
                syncToolBarToState();
                setView();
            }
        }

        private void extrapolate_Click(object sender, EventArgs e)
        {
            if (m_showPage && (
                sender is RadioButton && extrapolateButton.Checked ||
                sender is ToolStripMenuItem && !extrapolateMenuItem.Checked))
            {
                Settings.PredictionView = PredictionView.Extrapolate;
                syncToolBarToState();
                setView();
            }
        }

        private void predict_Click(PredictionModel pred)
        {
            Settings.Model = pred;
            syncToolBarToState();
            //setView();
            predictorView.setData();
            trainingView.RefreshData();
            extrapolateView.RefreshData();
        }

        private void predictModelMenuItem_Click(object sender, EventArgs e)
        {
            if (m_showPage && (
                        sender is ToolStripMenuItem && !((ToolStripMenuItem)sender).Checked) &&
                ((ToolStripMenuItem)sender).Tag is PredictionModel)
            {
                predict_Click((PredictionModel)((ToolStripMenuItem)sender).Tag);
            }
        }

        private void bannerContextMenuStrip_Opening(object sender, CancelEventArgs e)
        {
            timePredictionMenuItem.Checked = timePredictionButton.Checked;
            timePredictionMenuItem.Enabled = timePredictionButton.Enabled;
            trainingMenuItem.Checked = trainingButton.Checked;
            trainingMenuItem.Enabled = trainingButton.Enabled;
            extrapolateMenuItem.Checked = extrapolateButton.Checked;
            extrapolateMenuItem.Enabled = extrapolateButton.Enabled;

            foreach (ToolStripItem t in this.bannerContextMenuStrip.Items)
            {
                if (t.Tag is PredictionModel)
                {
                    PredictionModel m = (PredictionModel)t.Tag;
                    ((ToolStripMenuItem)t).Checked = (Settings.Model == m);
                }
            }

            paceMenuItem.Checked = paceButton.Checked;
            paceMenuItem.Enabled = paceButton.Enabled;
            speedMenuItem.Checked = speedButton.Checked;
            speedMenuItem.Enabled = speedButton.Enabled;

            tableMenuItem.Checked = tableButton.Checked;
            tableMenuItem.Enabled = tableButton.Enabled;
            chartMenuItem.Checked = chartButton.Checked;
            chartMenuItem.Enabled = chartButton.Enabled;

            chkHighScoreMenuItem.Checked = chkHighScoreBox.Checked;
            chkHighScoreMenuItem.Enabled = chkHighScoreBox.Enabled;

            showToolBarMenuItem.Checked = Settings.ShowToolBar;
        }

        private void table_Click(object sender, EventArgs e)
        {
            if (m_showPage && (
                sender is RadioButton && tableButton.Checked ||
                sender is ToolStripMenuItem && !tableMenuItem.Checked))
            {
                Settings.ShowChart = false;
                syncToolBarToState();
                predictorView.setData();
                //No effect for Training
            }
        }

        private void chart_Click(object sender, EventArgs e)
        {
            if (m_showPage && (
                sender is RadioButton && chartButton.Checked ||
                sender is ToolStripMenuItem && !chartMenuItem.Checked))
            {
                Settings.ShowChart = true;
                syncToolBarToState();
                predictorView.setData();
                //No effect for Training
            }
        }

        private void pace_Click(object sender, EventArgs e)
        {
            if (m_showPage && (
                sender is RadioButton && paceButton.Checked ||
                sender is ToolStripMenuItem && !paceMenuItem.Checked))
            {
                Settings.ShowPace = true;
                syncToolBarToState();
                predictorView.RefreshData();
            }
        }

        private void speed_Click(object sender, EventArgs e)
        {
            if (m_showPage && (
                sender is RadioButton && speedButton.Checked ||
                sender is ToolStripMenuItem && !speedMenuItem.Checked))
            {
                Settings.ShowPace = false;
                syncToolBarToState();
                predictorView.RefreshData();
            }
        }

        private void chkHighScore_Click(object sender, EventArgs e)
        {
            if (m_showPage)
            {
                if (sender is ToolStripMenuItem)
                {
                    //Not needed if sender is CheckBox
                    //menuitem synced to the checkbox
                    this.chkHighScoreBox.Checked = !this.chkHighScoreBox.Checked;
                }
                setView();
                predictorView.RefreshData();
            }
        }

        void actionBanner1_MenuClicked(object sender, System.EventArgs e)
        {
            //actionBanner1.ContextMenuStrip.Width = 100;
            actionBanner1.ContextMenuStrip.Show(actionBanner1.Parent.PointToScreen(new System.Drawing.Point(actionBanner1.Right - actionBanner1.ContextMenuStrip.Width - 2,
                actionBanner1.Bottom + 1)));
        }

        private void showToolBarMenuItem_Click(object sender, EventArgs e)
        {
            Settings.ShowToolBar = !Settings.ShowToolBar;
            UpdateToolBar();
        }

        public static void copyTableMenu_Click(object sender, EventArgs e)
        {
            ToolStripDropDownItem d = sender as ToolStripDropDownItem;
            ToolStripMenuItem t = (ToolStripMenuItem)sender;
            ContextMenuStrip s = (ContextMenuStrip)t.Owner;
            TreeList list = (TreeList)s.SourceControl;
            list.CopyTextToClipboard(true, System.Globalization.CultureInfo.CurrentCulture.TextInfo.ListSeparator);
        }

        class PredictionModelLabelProvider : TreeList.ILabelProvider
        {
            public Image GetImage(object element, TreeList.Column column)
            {
                PredictionModel t = (PredictionModel)element;
                PredictionModelUtil.ChartColors c = PredictionModelUtil.Colors(t);
                Bitmap i = new System.Drawing.Bitmap(6,6);
                Graphics g = Graphics.FromImage(i);
                g.DrawRectangle(new Pen(c.LineNormal, 3), new Rectangle(1, 1, 3, 3));
                return i;
            }
            public string GetText(object element, TreeList.Column column)
            {
                PredictionModel t = (PredictionModel)element;
                return PredictionModelUtil.Name(t);
            }
        }

        private void modelComboBox_ButtonClicked(object sender, EventArgs e)
        {
            TreeListPopup treeListPopup = new TreeListPopup();
            treeListPopup.ThemeChanged(m_visualTheme);
            treeListPopup.Tree.Columns.Add(new TreeList.Column());

            treeListPopup.Tree.RowData = PredictionModelUtil.List;
            treeListPopup.Tree.LabelProvider = new PredictionModelLabelProvider();

            if (PredictionModelUtil.List.Contains(Settings.Model))
            {
                treeListPopup.Tree.Selected = new object[] { Settings.Model };
            }
            treeListPopup.ItemSelected += delegate(object sender2, TreeListPopup.ItemSelectedEventArgs e2)
            {
                try
                {
                    Settings.Model = ((PredictionModel)(e2).Item);
                    modelComboBox.Text = PredictionModelUtil.Name(Settings.Model);
                    predict_Click(Settings.Model);
                }
                catch (KeyNotFoundException)
                {
                    //MessageDialog.Show("Settings (position group) for Matrix was changed, please redo your actions");
                }
            };
            treeListPopup.Popup(this.modelComboBox.Parent.RectangleToScreen(this.modelComboBox.Bounds));
        }

        void timeTextBox_LostFocus(object sender, System.EventArgs e)
        {
            if (m_showPage && !string.IsNullOrEmpty(this.timeTextBox.Text))
            {
               string s = UnitUtil.Time.ToString(this.Time, "u");
               if (!s.Equals(this.timeTextBox.Text))
               {
                   double time = UnitUtil.Time.Parse(this.timeTextBox.Text);
                   if (!double.IsNaN(time))
                   {
                       this.Time = TimeSpan.FromSeconds(time);
                   }
               }
            }
        }

        void distanceTextBox_LostFocus(object sender, System.EventArgs e)
        {
            if (m_showPage && !string.IsNullOrEmpty(this.distanceTextBox.Text))
            {
                //This is called at view changes. Compare if changed
                //IsNan, 0: check in assignment
                string s = UnitUtil.Distance.ToString(this.Distance, "u");
                if (!s.Equals(this.distanceTextBox.Text))
                {
                    this.Distance = UnitUtil.Distance.Parse(this.distanceTextBox.Text);
                }
            }
        }

        //Adapted from ApplyRoutes
        public static void tabControl1_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (m_visualTheme != null && sender is TabControl)
            {
                Brush backBrush;
                Brush foreBrush = new SolidBrush(m_visualTheme.ControlText);
                TabControl tc = sender as TabControl;
                if (tc == null) return;

                if (e.Index == tc.SelectedIndex)
                {
                    foreBrush = new SolidBrush(m_visualTheme.ControlText);
                    backBrush = new SolidBrush(m_visualTheme.Control);
                }
                else
                {
                    foreBrush = new SolidBrush(m_visualTheme.SubHeaderText);
                    backBrush = new SolidBrush(m_visualTheme.SubHeader);
                }

                string tabName = tc.TabPages[e.Index].Text;
                StringFormat sf = new StringFormat();
                sf.Alignment = StringAlignment.Center;
                e.Graphics.FillRectangle(backBrush, e.Bounds);
                Rectangle r = e.Bounds;
                r = new Rectangle(r.X, r.Y + 3, r.Width, r.Height - 3);
                e.Graphics.DrawString(tabName, e.Font, foreBrush, r, sf);

                //The right upper edge
                Brush background_brush = new SolidBrush(m_visualTheme.Control);//Backcolor of the form
                Rectangle LastTabRect = tc.GetTabRect(tc.TabPages.Count - 1);
                Rectangle rect = new Rectangle();
                rect.Location = new Point(LastTabRect.Right + tc.Left, tc.Top);
                rect.Size = new Size(tc.Right - rect.Left, LastTabRect.Height);
                //                rect.Location = new Point(LastTabRect.Right + this.Left, this.Top);
                //                rect.Size = new Size(this.Right - rect.Left, LastTabRect.Height);
                e.Graphics.FillRectangle(background_brush, rect);
                background_brush.Dispose();

                sf.Dispose();
                backBrush.Dispose();
                foreBrush.Dispose();

                e.DrawFocusRectangle();
            }
        }
    }
}
