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
using ZoneFiveSoftware.Common.Visuals.Fitness;
using ZoneFiveSoftware.Common.Data.GPS;
using ZoneFiveSoftware.Common.Data;
using System.Collections;
using System.IO;
using System.Xml;
using SportTracksUniqueRoutesPlugin.Properties;

namespace SportTracksUniqueRoutesPlugin.Source
{
    public partial class UniqueRoutesSettingPageControl : UserControl
    {
        public UniqueRoutesSettingPageControl()
        {
            InitializeComponent();
            updateLanguage();
            bandwidthBox.LostFocus += new EventHandler(bandwidthBox_LostFocus);
            percentageOff.LostFocus += new EventHandler(percentageOff_LostFocus);
            hasDirectionBox.LostFocus += new EventHandler(hasDirectionBox_LostFocus);
            ignoreBeginningBox.LostFocus += new EventHandler(ignoreBeginningBox_LostFocus);
            ignoreEndBox.LostFocus += new EventHandler(ignoreEndBox_LostFocus);            
            presentSettings();            
            Plugin.GetApplication().SystemPreferences.PropertyChanged += new PropertyChangedEventHandler(UniqueRoutesSettingPageControl_PropertyChanged);
        }

        private void precedeControl(Control a, Control b)
        {
            a.Location = new Point(b.Location.X - a.Size.Width - 5, a.Location.Y);
        }

        private void updateLanguage()
        {
            resetSettings.Text = Resources.ResetAllSettings;
            linkLabel1.Text = Resources.Webpage;
            groupBox1.Text = Resources.Settings;
            label1.Text = Resources.Bandwidth + ":";
            precedeControl(label1, bandwidthBox);
            label2.Text = Resources.AllowPointsOutsideBand + ":";
            precedeControl(label2, percentageOff);
            label3.Text = Resources.RoutesHaveDirection + ":";
            precedeControl(label3, hasDirectionBox);
            label5.Text = Resources.IgnoreBeginningOfRoute + ":";
            precedeControl(label5, ignoreBeginningBox);
            label8.Text = Resources.IgnoreEndOfRoute + ":";
            precedeControl(label8, ignoreEndBox);
            metricLabel.Text = Resources.Meters;
            label4.Text = Resources.Percent;
            beginningLabel.Text = Resources.Meters;
            endLabel.Text = Resources.Meters;
        }

        void UniqueRoutesSettingPageControl_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            presentSettings();
        }
        
        void ignoreEndBox_LostFocus(object sender, EventArgs e)
        {
            try
            {
                Settings.IgnoreEnd = Settings.convertToDistance
                    (Settings.parseDouble(ignoreEndBox.Text));
            }
            catch (Exception)
            {
                new WarningDialog(Resources.EndMeterWarning);
                bandwidthBox.Text = Settings.Bandwidth.ToString();
            }
        }

        void ignoreBeginningBox_LostFocus(object sender, EventArgs e)
        {
            try
            {
                Settings.IgnoreBeginning = Settings.convertToDistance
                    (Settings.parseDouble(ignoreBeginningBox.Text));
            }
            catch (Exception)
            {
                new WarningDialog(Resources.BeginningMeterWarning);
                bandwidthBox.Text = Settings.Bandwidth.ToString();
            }
        }

        private void presentSettings()
        {
            bandwidthBox.Text = Settings.Bandwidth.ToString();
            percentageOff.Value = (int)Math.Round(Settings.ErrorMargin * 100);
            hasDirectionBox.Checked = Settings.HasDirection;
            ignoreBeginningBox.Text = Settings.present(Settings.convertFromDistance(Settings.IgnoreBeginning));
            ignoreEndBox.Text = Settings.present(Settings.convertFromDistance(Settings.IgnoreEnd));
            beginningLabel.Text = Settings.DistanceUnit;
            endLabel.Text = Settings.DistanceUnit;
        }

        private void hasDirectionBox_LostFocus(object sender, EventArgs e)
        {
            Settings.HasDirection = hasDirectionBox.Checked;
        }

        private void percentageOff_LostFocus(object sender, EventArgs e)
        {
            Settings.ErrorMargin = (double) percentageOff.Value / 100;
        }

        private void bandwidthBox_LostFocus(object sender, EventArgs e)
        {
            try
            {
                Settings.Bandwidth = int.Parse(bandwidthBox.Text);
            }
            catch (Exception)
            {
                new WarningDialog(Resources.BandwidthWarning);
                bandwidthBox.Text = Settings.Bandwidth.ToString();
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(new ProcessStartInfo("IExplore",
                "http://gpsrunning.nicolajsen.nl/?path=SportTracks%2FUnique%20Routes"));
        }

        private void resetSettings_Click(object sender, EventArgs e)
        {
            Settings.reset();
            Settings.save();
            presentSettings();
        }

    }
}
