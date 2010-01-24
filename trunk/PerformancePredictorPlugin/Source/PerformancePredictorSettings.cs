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
using System.Diagnostics;
using ZoneFiveSoftware.Common.Data.Measurement;
using System.Globalization;
using SportTracksPerformancePredictorPlugin.Properties;

namespace SportTracksPerformancePredictorPlugin.Source
{
    public partial class PerformancePredictorSettings : UserControl
    {
        public PerformancePredictorSettings()
        {
            InitializeComponent();
            linkLabel1.Text = Resources.Webpage;
            resetSettings.Text = Resources.ResetAllSettings;
            groupBox1.Text = Resources.DistancesUsed;
            addDistance.Text = "<- " + Resources.AddDistance;
            removeDistance.Text = Resources.RemoveDistance + " ->";
            groupBox2.Text = Resources.HighScorePluginIntegration;
            label1.Text = Resources.Use;
            label2.Text = Resources.ProcDistUsed;
            foreach (Length.Units s in Enum.GetValues(typeof(Length.Units)))
            {
                unitBox.Items.Add(Settings.translateUnit(s));
            }
            unitBox.SelectedItem = Settings.DistanceUnit;
            updateList();
            numericUpDown1.Value = Settings.PercentOfDistance;
        }

        private void updateList()
        {
            distanceList.Items.Clear();
            foreach (double new_dist in Settings.Distances.Keys)
            {
                double length = new_dist;
                String unit = Settings.DistanceUnitShort;
                if (Settings.Distances[new_dist].Values[0])
                {
                    length = Length.Convert(length, Length.Units.Meter, Plugin.GetApplication().SystemPreferences.DistanceUnits);
                }
                else
                {
                    length = Length.Convert(length, Length.Units.Meter, Settings.Distances[new_dist].Keys[0]);
                    unit = Settings.Distances[new_dist].Keys[0].ToString();
                }
                distanceList.Items.Add(length + " " + unit);
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(new ProcessStartInfo("IExplore",
                "http://gpsrunning.nicolajsen.nl/?path=SportTracks/Performance+Predictor"));
        }

        private void resetSettings_Click(object sender, EventArgs e)
        {
            YesNoDialog dialog = new YesNoDialog(Resources.ResetAllSettingsWarning);
            dialog.ShowDialog();
            if (dialog.answer)
            {
                Settings.reset();
                Settings.save();
                updateList();
            }
        }

        private void addDistance_Click(object sender, EventArgs e)
        {
            try
            {
                double d = double.Parse(distanceBox.Text, NumberFormatInfo.CurrentInfo);
                if (d <= 0) throw new Exception();
                Length.Units unit = (Length.Units)Enum.Parse(typeof(Length.Units), (String)unitBox.SelectedItem);
                Settings.addDistance(d, unit, false);
                updateList();
                Settings.save();
            }
            catch (Exception)
            {
                new WarningDialog(Resources.PositiveNumber);
            }
        }

        private void removeDistance_Click(object sender, EventArgs e)
        {
            if (distanceList.SelectedIndex >= 0)
            {
                Settings.removeDistance(distanceList.SelectedIndex);
                updateList();
                Settings.save();
            }
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            Settings.PercentOfDistance = (int)numericUpDown1.Value;
        }
    }
}
