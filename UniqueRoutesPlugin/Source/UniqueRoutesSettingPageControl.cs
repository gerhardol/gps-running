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
using ZoneFiveSoftware.Common.Data.Fitness;
using System.Diagnostics;
using ZoneFiveSoftware.Common.Visuals;
using ZoneFiveSoftware.Common.Visuals.Fitness;
using ZoneFiveSoftware.Common.Data.GPS;
using ZoneFiveSoftware.Common.Data;
using ZoneFiveSoftware.Common.Data.Measurement;
using System.Collections;
using System.IO;
using System.Xml;
using GpsRunningPlugin.Properties;
using GpsRunningPlugin.Util;

namespace GpsRunningPlugin.Source
{
    public partial class UniqueRoutesSettingPageControl : UserControl
    {
        private bool m_showPage = false;
        private ITheme m_visualTheme;

        public UniqueRoutesSettingPageControl()
        {
            InitializeComponent();
            InitControls();
            bandwidthBox.LostFocus += new EventHandler(bandwidthBox_LostFocus);
            percentageOff.LostFocus += new EventHandler(percentageOff_LostFocus);
            hasDirectionBox.LostFocus += new EventHandler(hasDirectionBox_LostFocus);
            ignoreBeginningBox.LostFocus += new EventHandler(ignoreBeginningBox_LostFocus);
            ignoreEndBox.LostFocus += new EventHandler(ignoreEndBox_LostFocus);

            presentSettings();            
            Plugin.GetApplication().SystemPreferences.PropertyChanged += new PropertyChangedEventHandler(UniqueRoutesSettingPageControl_PropertyChanged);
        }

        void InitControls()
        {
            this.boxCategory.ButtonImage = Properties.Resources.DropDown;
        }

        public void ThemeChanged(ZoneFiveSoftware.Common.Visuals.ITheme visualTheme)
        {
            m_visualTheme = visualTheme;
            this.bandwidthBox.ThemeChanged(visualTheme);
            this.ignoreBeginningBox.ThemeChanged(visualTheme);
            this.ignoreEndBox.ThemeChanged(visualTheme);
            this.boxCategory.ThemeChanged(visualTheme);
        }

        public void UICultureChanged(System.Globalization.CultureInfo culture)
        {
            resetSettings.Text = StringResources.ResetAllSettings;
            linkLabelWebpage.Text = Resources.Webpage;
            //groupBox1.Text = StringResources.Settings;
            labelRadius.Text = Resources.Radius + ":";
            precedeControl(labelRadius, bandwidthBox);
            labelAllowPointsOutsideBand.Text = Resources.AllowPointsOutsideBand + ":";
            precedeControl(labelAllowPointsOutsideBand, percentageOff);
            labelRoutesHaveDirection.Text = Resources.RoutesHaveDirection + ":";
            precedeControl(labelRoutesHaveDirection, hasDirectionBox);
            labelIgnoreBeginningOfRoute.Text = Resources.IgnoreBeginningOfRoute + ":";
            precedeControl(labelIgnoreBeginningOfRoute, ignoreBeginningBox);
            labelIgnoreEndOfRoute.Text = Resources.IgnoreEndOfRoute + ":";
            precedeControl(labelIgnoreEndOfRoute, ignoreEndBox);
            //labelPercentOutsideUnit.Text = CommonResources.Text.LabelPercent;
            UniqueRoutesActivityDetailView.setCategoryLabel(this.categoryLabel, this.boxCategory, 0);
            this.boxCategory.Location = new Point(categoryLabel.Location.X + categoryLabel.Width + 3,
                this.boxCategory.Location.Y);
            this.boxCategory.Size = new Size(this.groupBox2.Size.Width - boxCategory.Location.X - 3,
                boxCategory.Size.Height);
        }

        public bool HidePage()
        {
            m_showPage = false;
            return true;
        }
        public void ShowPage(string bookmark)
        {
            m_showPage = true;
        }
        private void precedeControl(Control a, Control b)
        {
            a.Location = new Point(b.Location.X - a.Size.Width - 5, a.Location.Y);
        }

        private void presentSettings()
        {
            bandwidthBox.Text = UnitUtil.Elevation.ToString(Settings.Radius, "u");
            percentageOff.Value = (int)Math.Round(Settings.ErrorMargin * 100);
            hasDirectionBox.Checked = Settings.HasDirection;
            ignoreBeginningBox.Text = UnitUtil.Distance.ToString(Settings.IgnoreBeginning, "u");
            ignoreEndBox.Text = UnitUtil.Distance.ToString(Settings.IgnoreEnd, "u");

            //metricLabel.Text = UnitUtil.Elevation.Label;
            //beginningLabel.Text = UnitUtil.Distance.Label;
            //endLabel.Text = UnitUtil.Distance.Label;
        }

        void UniqueRoutesSettingPageControl_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (m_showPage)
            {
                presentSettings();
            }
        }
        
        void ignoreEndBox_LostFocus(object sender, EventArgs e)
        {
            try
            {
                double value = UnitUtil.Distance.Parse(ignoreEndBox.Text);
                if (double.IsNaN(value) || value < 0) { throw new Exception(); }
                Settings.IgnoreEnd = value;
                presentSettings();
            }
            catch (Exception)
            {
                ignoreEndBox.Text = UnitUtil.Distance.ToString(Settings.IgnoreEnd);
                new WarningDialog(Resources.EndMeterWarning);
            }
        }

        void ignoreBeginningBox_LostFocus(object sender, EventArgs e)
        {
            try
            {
                double value = UnitUtil.Distance.Parse(ignoreBeginningBox.Text);
                if (double.IsNaN(value) || value < 0) { throw new Exception(); }
                Settings.IgnoreBeginning = value;
                presentSettings();
            }
            catch (Exception)
            {
                ignoreBeginningBox.Text = UnitUtil.Distance.ToString(Settings.IgnoreBeginning);
                new WarningDialog(Resources.BeginningMeterWarning);
            }
        }

        private void hasDirectionBox_LostFocus(object sender, EventArgs e)
        {
            Settings.HasDirection = hasDirectionBox.Checked;
            presentSettings();
        }

        private void percentageOff_LostFocus(object sender, EventArgs e)
        {
            Settings.ErrorMargin = (double) percentageOff.Value / 100;
            presentSettings();
        }

        private void bandwidthBox_LostFocus(object sender, EventArgs e)
        {
            try
            {
                double value = UnitUtil.Elevation.Parse(bandwidthBox.Text);
                if (double.IsNaN(value) || value <= 0) { throw new Exception(); }
                Settings.Radius = value;
                presentSettings();
            }
            catch (Exception)
            {
                presentSettings();
                new WarningDialog(Resources.RadiusWarning);
            }
        }

        private void boxCategory_ButtonClicked(object sender, EventArgs e)
        {
            bool select = UniqueRoutesActivityDetailView.boxCategory_ButtonClickedCommon(categoryLabel, boxCategory,
                0, m_visualTheme);
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                ProcessStartInfo procStartInfo = new ProcessStartInfo("https://github.com/gerhardol/gps-running/wiki/UniqueRoutes");
                Process.Start(procStartInfo);
            }
            catch(Exception ex)
            {
                MessageDialog.Show("Exception encountered launching browser", "Launching other application" + ex,
                                MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
       }

        private void resetSettings_Click(object sender, EventArgs e)
        {
            presentSettings();
        }

    }
}
