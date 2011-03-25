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

#if !ST_2_1
        public PerformancePredictorControl(IDetailPage detailPage, IDailyActivityView view)
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
        }
        //popup dialog
        public PerformancePredictorControl(IDailyActivityView view)
            : this(true)
        {
            m_view = view;
            m_layer = TrailPointsLayer.Instance((IView)view);
            this.predictorView.InitControls(m_DetailPage, m_view, m_layer, this);
            this.trainingView.InitControls(m_DetailPage, m_view, m_layer, this);
        }
        public PerformancePredictorControl(IActivityReportsView view)
            : this(true)
        {
            m_layer = TrailPointsLayer.Instance((IView)view);
            this.predictorView.InitControls(m_DetailPage, m_view, m_layer, this);
            this.trainingView.InitControls(m_DetailPage, m_view, m_layer, this);
        }
        //UniqueRoutes sendto
        public PerformancePredictorControl(IList<IActivity> activities, IDailyActivityView view)
            : this(view)
        {
            this.Activities = activities;
        }
        public PerformancePredictorControl(IList<IActivity> activities, IActivityReportsView view)
            : this(view)
        {
            this.Activities = activities;
        }
#endif
        public PerformancePredictorControl()
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
        public PerformancePredictorControl(IList<IActivity> aAct, bool showDialog)
            : this(showDialog)
        {
            this.Activities = aAct;
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
                this.ShowPage("");
            }
        }

        void InitControls()
        {
            //Set control state before listeners are activated
            switch (Settings.Model)
            {
                default:
                case PredictionModel.DAVE_CAMERON:
                    daveCameronButton.Checked = true;
                    break;
                case PredictionModel.PETE_RIEGEL:
                    reigelButton.Checked = true;
                    break;
            }
            paceButton.Checked = Settings.ShowPace;
            speedButton.Checked = !Settings.ShowPace;

            setView();
            this.daveCameronButton.CheckedChanged += new System.EventHandler(this.daveCameron_CheckedChanged);
            this.reigelButton.CheckedChanged += new System.EventHandler(this.reigel_CheckedChanged);
            this.tableButton.CheckedChanged += new System.EventHandler(this.table_CheckedChanged);
            this.chartButton.CheckedChanged += new System.EventHandler(this.chartButton_CheckedChanged);
            this.trainingButton.CheckedChanged += new System.EventHandler(this.training_CheckedChanged);
            this.timePredictionButton.CheckedChanged += new System.EventHandler(this.timePrediction_CheckedChanged);
            this.speedButton.CheckedChanged += new System.EventHandler(this.speed_CheckedChanged);
            this.paceButton.CheckedChanged += new System.EventHandler(this.pace_CheckedChanged);
            this.chkHighScoreBox.CheckedChanged += new System.EventHandler(this.chkHighScore_CheckedChanged);
        }

        public void ThemeChanged(ITheme visualTheme)
        {
            //m_visualTheme = visualTheme;
            //Set color for non ST controls
            this.splitContainer1.Panel1.BackColor = visualTheme.Control;
            this.splitContainer1.Panel2.BackColor = visualTheme.Control;

            this.predictorView.ThemeChanged(visualTheme);
            this.trainingView.ThemeChanged(visualTheme);
        }

        public void UICultureChanged(System.Globalization.CultureInfo culture)
        {
            groupBox3.Text = StringResources.Settings;
            groupBox1.Text = Resources.PredictionModel;
            groupBox2.Text = Resources.Velocity;
            resultBox.Text = Resources.PredictionResults;
            timePredictionButton.Text = Resources.TimePrediction;
            trainingButton.Text = StringResources.Training;
            paceButton.Text = CommonResources.Text.LabelPace;
            speedButton.Text = CommonResources.Text.LabelSpeed;
            chartButton.Text = Resources.ViewInChart;
            tableButton.Text = Resources.ViewInTable;
            this.chkHighScoreBox.Text = Properties.Resources.HighScorePrediction;

            predictorView.UICultureChanged(culture);
            trainingView.UICultureChanged(culture);
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

                if (1 == m_activities.Count && m_activities[0] != null)
                {
                    if (m_lastActivity != m_activities[0])
                    {
                        m_lastActivity = m_activities[0];
                    }
                }
                else
                {
                    m_lastActivity = null;
                }

                //No settings for HS, separate check in makeData(), enabled in setView
                //For Activity page use Predict/Training by default for single activities
                //Enabling/disabling is done based on settings
                this.chkHighScoreBox.CheckedChanged -= new System.EventHandler(this.chkHighScore_CheckedChanged);
                if (Settings.HighScore != null && (m_activities.Count > 1 || m_popupForm != null))
                {
                    chkHighScoreBox.Checked = true;
                }
                else
                {
                    chkHighScoreBox.Checked = false;
                }
                this.chkHighScoreBox.CheckedChanged += new System.EventHandler(this.chkHighScore_CheckedChanged);


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
                            title = Resources.PPHS + " " + String.Format(StringResources.ForManyActivities, m_activities.Count);
                        }
                    }
                    m_popupForm.Text = title;
                }

                activateListeners();
                m_layer.ClearOverlays();
                setView();
            }
        }

        public IActivity SingleActivity
        {
            get
            {
                return m_lastActivity;
            }
        }

#if ST_2_1
        private void dataChanged(object sender, ZoneFiveSoftware.Common.Data.NotifyDataChangedEventArgs e)
        {
            makeData();
        }
#else
        private void Activity_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (m_showPage)
            {
                this.predictorView.RefreshData();
            }
        }
#endif

        public bool HidePage()
        {
            m_showPage = false;
            deactivateListeners();
            predictorView.HidePage();
            trainingView.HidePage();
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

        public Predict.PredictTime Predictor(PredictionModel model)
        {
            switch (model)
            {
                default:
                case PredictionModel.DAVE_CAMERON:
                    return Predict.Cameron;
                case PredictionModel.PETE_RIEGEL:
                    return Predict.Riegel;
            }
        }

        private void setView()
        {
            predictorView.HidePage();
            trainingView.HidePage();

            timePredictionButton.Enabled = false;
            trainingButton.Enabled = false;
            this.tableButton.Enabled = false;
            chartButton.Enabled = false;
            chkHighScoreBox.Enabled = false;

            this.tableButton.Enabled = true;
            this.chartButton.Enabled = true;

            this.trainingButton.CheckedChanged -= new System.EventHandler(this.training_CheckedChanged);
            this.timePredictionButton.CheckedChanged -= new System.EventHandler(this.timePrediction_CheckedChanged);
            if (m_activities.Count == 1)
            {
                timePredictionButton.Enabled = true;
                trainingButton.Enabled = true;

                timePredictionButton.Checked = Settings.ShowPrediction;
            }
            else
            {
                timePredictionButton.Checked = true;
            }

            trainingButton.Checked = !timePredictionButton.Checked;
            this.trainingButton.CheckedChanged += new System.EventHandler(this.training_CheckedChanged);
            this.timePredictionButton.CheckedChanged += new System.EventHandler(this.timePrediction_CheckedChanged);

            if (timePredictionButton.Checked)
            {
                chartButton.Checked = Settings.ShowChart;
                tableButton.Checked = !Settings.ShowChart;
                if (m_activities.Count == 1)
                {
                    //chkHighScore.Checked set in Activities (as it may clear selection)
                    if (Settings.HighScore != null)
                    {
                        chkHighScoreBox.Enabled = true;
                    }
                }
                if (m_showPage)
                {
                    predictorView.ShowPage("");
                }
            }
            else
            {
                this.tableButton.Enabled = false;
                this.chartButton.Enabled = false;
                if (this.chartButton.Checked)
                {
                    this.tableButton.CheckedChanged -= new System.EventHandler(this.table_CheckedChanged);
                    this.chartButton.Checked = false;
                    this.tableButton.Checked = true;
                    this.tableButton.CheckedChanged += new System.EventHandler(this.table_CheckedChanged);
                }
                if (m_showPage)
                {
                    trainingView.ShowPage("");
                }
            }
        }

        public bool ChkHighScore
        {
            get
            {
                return chkHighScoreBox.Checked;
            }
        }

        /**************************************************/

        //void Parent_SizeChanged(object sender, EventArgs e)
        //{
        //    if (popupForm != null)
        //    {
        //        Settings.WindowSize = popupForm.Size;
        //    }
        //    setSize();
        //}

        //private void Settings_DistanceChanged(object sender, PropertyChangedEventArgs e)
        //{
        //    makeData();
        //}

        //private void form_Resize(object sender, EventArgs e)
        //{
        //    setSize();
        //}

        //private void PerformancePredictorView_Resize(object sender, EventArgs e)
        //{
        //    setSize();
        //}

        //private void Parent_Resize(object sender, EventArgs e)
        //{
        //    setSize();
        //}
        
        private void daveCameron_CheckedChanged(object sender, EventArgs e)
        {
            if (m_showPage && daveCameronButton.Checked)
            {
                Settings.Model = PredictionModel.DAVE_CAMERON;
                //setView();
                predictorView.setData();
                trainingView.RefreshData();
            }
        }

        private void reigel_CheckedChanged(object sender, EventArgs e)
        {
            if (m_showPage && reigelButton.Checked)
            {
                Settings.Model = PredictionModel.PETE_RIEGEL;
                //setView();
                predictorView.setData();
                trainingView.RefreshData();
            }
        }

        private void chartButton_CheckedChanged(object sender, EventArgs e)
        {
            if (m_showPage && chartButton.Checked)
            {
                Settings.ShowChart = true;
                predictorView.updateChartVisibility();
                //No effect for Training
            }
        }

        private void table_CheckedChanged(object sender, EventArgs e)
        {
            if (m_showPage && tableButton.Checked)
            {
                Settings.ShowChart = false;
                predictorView.updateChartVisibility();
                //No effect for Training
            }
        }

        private void timePrediction_CheckedChanged(object sender, EventArgs e)
        {
            if (m_showPage && timePredictionButton.Checked && m_activities.Count == 1)
            {
                Settings.ShowPrediction = true;
                setView();
                //predictorView.setData();
            }
        }

        private void training_CheckedChanged(object sender, EventArgs e)
        {
            if (m_showPage && trainingButton.Checked && m_activities.Count == 1)
            {
                Settings.ShowPrediction = false;
                setView();
                //predictorView.setData();
            }
        }

        private void pace_CheckedChanged(object sender, EventArgs e)
        {
            if (m_showPage && paceButton.Checked)
            {
                Settings.ShowPace = true;
                predictorView.RefreshData();
                trainingView.RefreshData();
            }
        }

        private void speed_CheckedChanged(object sender, EventArgs e)
        {
            if (m_showPage && speedButton.Checked)
            {
                Settings.ShowPace = false;
                predictorView.RefreshData();
                trainingView.RefreshData();
            }
        }

        private void chkHighScore_CheckedChanged(object sender, EventArgs e)
        {
            if (m_showPage)
            {
                predictorView.RefreshData();
            }
        }
    }
}
