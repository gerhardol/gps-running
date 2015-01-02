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
        internal PerformancePredictorControl(IList<IActivity> activities, IDailyActivityView view, TimeSpan time, double distance, System.Windows.Forms.ProgressBar progressBar)
            : this(view)
        {
            this.Activities = activities;
            this.m_time = time;
            this.m_distance = distance;
            ShowPage("");
        }
#endif
        private PerformancePredictorControl()
        {
            InitializeComponent();
            InitControls();

            //if (Parent != null)
            //{
            //    Parent.Resize += new EventHandler(Parent_Resize);
            //}
            //Resize += new EventHandler(PerformancePredictorView_Resize);
            //Settings settings = new Settings();
        }

        //Compatibility with old UniqueRoutes send to
        //public PerformancePredictorControl(IList<IActivity> aAct, bool showDialog)
        //    : this(showDialog)
        //{
        //    this.Activities = aAct;
        //}

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
            //menu set separately
            syncToolBarToState();
            syncMenuToState();

            UpdateToolBar();
            //Correct possibly misaligned settings
            //setView();
        }

        private static ITheme m_visualTheme;
        public void ThemeChanged(ITheme visualTheme)
        {
            m_visualTheme = visualTheme;
 
            //Set color for non ST controls
            this.splitContainer1.Panel1.BackColor = visualTheme.Control;
            this.splitContainer1.Panel2.BackColor = visualTheme.Control;
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
            this.daveCameronMenuItem.Text = PredictionModelUtil.Name(PredictionModel.DAVE_CAMERON);
            this.peteRiegelMenuItem.Text = PredictionModelUtil.Name(PredictionModel.PETE_RIEGEL);
            this.wavaMenuItem.Text = PredictionModelUtil.Name(PredictionModel.WAVA);
            
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

                //No settings for HS, separate check in makeData(), enabled in setView
                //For Activity page use Predict/Training by default for single activities
                //Enabling/disabling is done based on settings
                if (Settings.HighScore != null && (m_activities.Count > 1/* || m_popupForm != null*/))
                {
                    chkHighScoreBox.Checked = true;
                }
                else
                {
                    chkHighScoreBox.Checked = false;
                }

                //if (m_activities.Count != 1 || (m_activities.Count == 1 && null != m_activities[0]))
                //{
                //    trainingView.Activity = null;
                //}
                //predictorView.Activities = m_activities;

                //title cant be set directly on activity page
                if (null != m_popupForm)
                {
                    string title = Resources.PPHS;
                    if (m_activities.Count > 0)
                    {
                        if (m_activities.Count == 1)
                        {
                            title = Resources.PPHS + " " + StringResources.ForOneActivity;
                        }
                        else
                        {
                            //TODO: Trails can have many "activities" but only one used activity
                            title = Resources.PPHS + " " + String.Format(StringResources.ForManyActivities, m_activities.Count);
                        }
                    }
                    m_popupForm.Text = title;
                }

                activateListeners();
                if (m_layer != null)
                {
                    m_layer.ClearOverlays();
                }
                setView();
            }
        }

        //For extrapolate view, where a single activity is extrapolated
        public IActivity SingleActivity
        {
            get
            {
                if (!this.ChkHighScore)
                {
                    return m_lastActivity;
                }
                return null;
            }
        }

        //For training view, where certain data is depenedent on an activity, nothing major if it is changed
        public IActivity FirstActivity
        {
            get
            {
                if (m_activities.Count > 0 && m_activities[0] != null && !this.ChkHighScore)
                {
                    return m_activities[0];
                }
                return null;
            }
        }

        //The following are a little more permissive than SingleActivity, but seem to be OK
        internal ActivityInfo SingleInfo { get { return ActivityInfoCache.Instance.GetInfo(this.m_lastActivity); } }
        internal TimeSpan Time
        {
            get
            {
                if (this.m_time != null)
                {
                    return (TimeSpan)this.m_time;
                }
                else if (this.m_lastActivity != null)
                {
                    return this.SingleInfo.Time;
                }
                return TimeSpan.FromSeconds(0);
            }
            set
            {
                this.m_time = value;
                this.setView();
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
                else if (this.m_lastActivity != null)
                {
                    return this.SingleInfo.DistanceMeters;
                }
                return 0;
            }
            set
            {
                this.m_distance = value;
                this.setView();
            }
        }
        internal bool IsPartial { get { return (this.m_time != null && this.m_distance != null); } }

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
            this.predictorView.HidePage();
            this.trainingView.HidePage();
            this.extrapolateView.HidePage();

            //Disable all but timePredictionButton (always active)
            //timePredictionButton.Enabled = true;
            this.trainingButton.Enabled = false;
            this.extrapolateButton.Enabled = false;
            this.timePredictionButton.Checked = false;
            this.trainingButton.Checked = false;
            this.extrapolateButton.Checked = false;

            this.tableButton.Enabled = false;
            this.tableButton.Checked = true;
            this.chkHighScoreBox.Enabled = false;

            if (this.SingleActivity != null && !this.ChkHighScore)
            {
                this.trainingButton.Enabled = true;
                this.extrapolateButton.Enabled = true;

                this.timePredictionButton.Checked = Settings.PredictionView == PredictionView.TimePrediction;
            }
            else if (this.FirstActivity != null && !this.ChkHighScore)
            {
                //timePredictionButton.Enabled = true;
                this.trainingButton.Enabled = true;

                this.timePredictionButton.Checked = Settings.PredictionView == PredictionView.TimePrediction ||
                    Settings.PredictionView == PredictionView.Extrapolate;
            }
            else
            {
                this.timePredictionButton.Checked = true;
            }

            if (timePredictionButton.Checked)
            {
                this.actionBanner1.Text = Properties.Resources.TimePrediction;
                this.chkHighScoreBox.Enabled |= Settings.HighScore != null;
                this.tableButton.Enabled = true;
                this.tableButton.Checked = !Settings.ShowChart;
                if (this.m_showPage)
                {
                    this.predictorView.ShowPage("");
                }
            }
            else
            {
                if (this.trainingButton.Enabled && Settings.PredictionView == PredictionView.Training)
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
            }

            //Dependent
            chartButton.Checked = !tableButton.Checked;
            chartButton.Enabled = tableButton.Enabled;

            syncMenuToState();
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

            paceButton.Checked = Settings.ShowPace;
            speedButton.Checked = !Settings.ShowPace;

            tableButton.Checked = !Settings.ShowChart;
            chartButton.Checked = !tableButton.Checked;
            chartButton.Enabled = tableButton.Enabled;
        }

        private void syncMenuToState()
        {
            timePredictionMenuItem.Checked = timePredictionButton.Checked;
            timePredictionMenuItem.Enabled = timePredictionButton.Enabled;
            trainingMenuItem.Checked = trainingButton.Checked;
            trainingMenuItem.Enabled = trainingButton.Enabled;
            extrapolateMenuItem.Checked = extrapolateButton.Checked;
            extrapolateMenuItem.Enabled = extrapolateButton.Enabled;

            daveCameronMenuItem.Checked = Settings.Model == PredictionModel.DAVE_CAMERON;
            peteRiegelMenuItem.Checked = Settings.Model == PredictionModel.PETE_RIEGEL;
            wavaMenuItem.Checked = Settings.Model == PredictionModel.WAVA;

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
                syncMenuToState();
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
                syncMenuToState();
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
                syncMenuToState();
                setView();
            }
        }

        private void predict_Click(PredictionModel pred)
        {
            Settings.Model = pred;
            syncToolBarToState();
            syncMenuToState();
            //setView();
            predictorView.setData();
            trainingView.RefreshData();
            extrapolateView.RefreshData();
        }

        private void daveCameron_Click(object sender, EventArgs e)
        {
            if (m_showPage && (
                sender is ToolStripMenuItem && !daveCameronMenuItem.Checked))
            {
                predict_Click(PredictionModel.DAVE_CAMERON);
            }
        }

        private void peteRiegel_Click(object sender, EventArgs e)
        {
            if (m_showPage && (
                sender is ToolStripMenuItem && !peteRiegelMenuItem.Checked))
            {
                predict_Click(PredictionModel.PETE_RIEGEL);
            }
        }

        private void wava_Click(object sender, EventArgs e)
        {
            if (m_showPage && (
                sender is ToolStripMenuItem && !wavaMenuItem.Checked))
            {
                predict_Click(PredictionModel.WAVA);
            }
        }

        private void table_Click(object sender, EventArgs e)
        {
            if (m_showPage && (
                sender is RadioButton && tableButton.Checked ||
                sender is ToolStripMenuItem && !tableMenuItem.Checked))
            {
                Settings.ShowChart = false;
                syncToolBarToState();
                syncMenuToState();
                predictorView.updateChartVisibility();
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
                syncMenuToState();
                predictorView.updateChartVisibility();
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
                syncMenuToState();
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
                syncMenuToState();
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
