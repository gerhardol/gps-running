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

using ZoneFiveSoftware.Common.Data;
using ZoneFiveSoftware.Common.Data.Fitness;
using ZoneFiveSoftware.Common.Visuals;
using ZoneFiveSoftware.Common.Visuals.Fitness;
using ZoneFiveSoftware.Common.Visuals.Chart;
using ZoneFiveSoftware.Common.Visuals.Util;
using ZoneFiveSoftware.Common.Visuals.Mapping;
using GpsRunningPlugin.Properties;
using GpsRunningPlugin.Util;
using TrailsPlugin;
using TrailsPlugin.Data;
using TrailsPlugin.Utils;
using TrailsPlugin.UI.MapLayers;

namespace GpsRunningPlugin.Source
{
    public partial class HighScoreControl : UserControl
    {
        private HighScoreViewer hsViewer = null;

        internal void setViewer(HighScoreViewer hsViewer)
        {
            this.hsViewer = hsViewer;
        }

        internal HighScoreControl()
        {
            InitializeComponent();
            InitControls();

            domainBox.DropDownStyle = ComboBoxStyle.DropDownList;
            imageBox.DropDownStyle = ComboBoxStyle.DropDownList;
            boundsBox.DropDownStyle = ComboBoxStyle.DropDownList;

            domainBox.SelectedItem = Goal.translateToLanguage(Settings.Domain);
            imageBox.SelectedItem = Goal.translateToLanguage(Settings.Image);

            boundsBox.SelectedItem = Settings.UpperBound ? StringResources.Maximal : StringResources.Minimal;

            domainBox.SelectedIndexChanged += new EventHandler(domainBox_SelectedIndexChanged);
            imageBox.SelectedIndexChanged += new EventHandler(imageBox_SelectedIndexChanged);
            boundsBox.SelectedIndexChanged += new EventHandler(boundsBox_SelectedIndexChanged);
            minGradeBoxUpdate();
        }

        void InitControls()
        {
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
            this.minGradeBox.ThemeChanged(visualTheme);

            this.ControlPanel.BackColor = visualTheme.Control;
        }

        public void UICultureChanged(System.Globalization.CultureInfo culture)
        {
            label1.Text = StringResources.Find;
            label2.Text = StringResources.PerSpecified;
            minGradeLbl.Text = " " + ZoneFiveSoftware.Common.Visuals.CommonResources.Text.LabelGrade + ">";
            correctUI(new Control[] { boundsBox, domainBox, label2, imageBox, minGradeLbl, minGradeBox });

            label1.Location = new Point(boundsBox.Location.X - 5 - label1.Width, label1.Location.Y);
            label2.Location = new Point(label2.Location.X, label1.Location.Y);
            minGradeLbl.Location = new Point(minGradeLbl.Location.X, label1.Location.Y);
        }

        /***********************************************************/

        private void showResults(bool resetCache)
        {
            if(this.hsViewer != null)
            {
                this.hsViewer.showResults(resetCache);
            }
        }

        void imageBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            Settings.Image = (GoalParameter)Enum.Parse(typeof(GoalParameter), Goal.translateParameter((String)imageBox.SelectedItem), true);
            if (Settings.Domain.Equals(Settings.Image))
            {
                Settings.Domain = (Settings.Domain != GoalParameter.Distance) ? GoalParameter.Distance : GoalParameter.Time;
                domainBox.SelectedIndexChanged -= new EventHandler(domainBox_SelectedIndexChanged);
                domainBox.SelectedItem = Goal.translateToLanguage(Settings.Domain);
                domainBox.SelectedIndexChanged += new EventHandler(domainBox_SelectedIndexChanged);
            }
            showResults(false);
        }

        void domainBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            Settings.Domain = (GoalParameter)Enum.Parse(typeof(GoalParameter),
                        Goal.translateParameter((String)domainBox.SelectedItem), true);
            if (Settings.Domain.Equals(Settings.Image))
            {
                Settings.Image = (Settings.Domain != GoalParameter.Distance) ? GoalParameter.Distance : GoalParameter.Time;
                imageBox.SelectedIndexChanged -= new EventHandler(imageBox_SelectedIndexChanged);
                imageBox.SelectedItem = Goal.translateToLanguage(Settings.Image);
                imageBox.SelectedIndexChanged += new EventHandler(imageBox_SelectedIndexChanged);
            }
            showResults(false);
        }

        private void boundsBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            Settings.UpperBound = boundsBox.SelectedItem.Equals(StringResources.Maximal);
            showResults(false);
        }

        //Requires that the box is left, for instance tab or selecting other control (not just click)
        //MouseLeave or Key entered has other issues
        void minGradeBox_Leave(object sender, System.EventArgs e)
        {
            try
            {
                Settings.MinGrade = Double.Parse(minGradeBox.Text.Replace("%", "")) / 100.0;
            }
            catch (Exception)
            { }
            minGradeBoxUpdate();
            //Cache must be thrown away
            showResults(true);
        }

        private void minGradeBoxUpdate()
        {
            minGradeBox.Text = Settings.MinGrade.ToString("0.0 %");
        }
    }
}
