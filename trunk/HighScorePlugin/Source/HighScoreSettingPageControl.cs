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
using ZoneFiveSoftware.Common.Data.Measurement;
using System.Diagnostics;
using ZoneFiveSoftware.Common.Visuals;
using ZoneFiveSoftware.Common.Visuals.Fitness;
using ZoneFiveSoftware.Common.Data.GPS;
using ZoneFiveSoftware.Common.Data;
using System.Collections;
using System.IO;
using System.Xml;
using GpsRunningPlugin.Properties;
using GpsRunningPlugin.Util;

namespace GpsRunningPlugin.Source
{
    public partial class HighScoreSettingPageControl : UserControl
    {
        IList<IList<double>> speedZoneIndex;

        public HighScoreSettingPageControl()
        {
            InitializeComponent();
            paceTypeBox.DropDownStyle = ComboBoxStyle.DropDownList;
            paceTypeBox.SelectedIndexChanged += new EventHandler(paceTypeBox_SelectedIndexChanged);
            Plugin.GetApplication().SystemPreferences.PropertyChanged += new PropertyChangedEventHandler(HighScoreSettingPageControl_PropertyChanged);
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

        public void ThemeChanged(ZoneFiveSoftware.Common.Visuals.ITheme visualTheme)
        {
            distanceInputBox.ThemeChanged(visualTheme);
            timeInputBox.ThemeChanged(visualTheme);
            elevationInputBox.ThemeChanged(visualTheme);
            maxPulseBox.ThemeChanged(visualTheme);
            minPulseBox.ThemeChanged(visualTheme);
        }
        public void UICultureChanged(System.Globalization.CultureInfo culture)
        {
            resetSettings.Text = StringResources.ResetAllSettings;
            linkLabel1.Text = Resources.Webpage;
            ignoreManualBox.Text = Resources.IgnoreManualEnteredBox;
            addDistance.Text = "<-- " + ZoneFiveSoftware.Common.Visuals.CommonResources.Text.ActionAdd;
            removeDistance.Text = ZoneFiveSoftware.Common.Visuals.CommonResources.Text.ActionRemove + " -->";
            resetDistances.Text = StringResources.Reset;
            addTime.Text = "<-- " + ZoneFiveSoftware.Common.Visuals.CommonResources.Text.ActionAdd;
            removeTime.Text = ZoneFiveSoftware.Common.Visuals.CommonResources.Text.ActionRemove + " -->";
            resetTimes.Text = StringResources.Reset;
            addElevation.Text = "<-- " + ZoneFiveSoftware.Common.Visuals.CommonResources.Text.ActionAdd;
            removeElevation.Text = ZoneFiveSoftware.Common.Visuals.CommonResources.Text.ActionRemove + " -->";
            resetElevations.Text = StringResources.Reset;
            addPulse.Text = "<-- " + ZoneFiveSoftware.Common.Visuals.CommonResources.Text.ActionAdd;
            removePulse.Text = ZoneFiveSoftware.Common.Visuals.CommonResources.Text.ActionRemove + " -->";
            resetPulseZone.Text = StringResources.Reset;
            addPace.Text = "<-- " + ZoneFiveSoftware.Common.Visuals.CommonResources.Text.ActionAdd;
            removePace.Text = ZoneFiveSoftware.Common.Visuals.CommonResources.Text.ActionRemove + " -->";
            resetPaceZone.Text = StringResources.Reset;
            label1.Text = UnitUtil.HeartRate.LabelAbbr + " " + ZoneFiveSoftware.Common.Visuals.CommonResources.Text.LabelFrom.ToLower();
            label2.Text = UnitUtil.HeartRate.LabelAbbr + " " + ZoneFiveSoftware.Common.Visuals.CommonResources.Text.LabelTo.ToLower();
            label4.Text = "(" + StringResources.TimeFormat + ")";

            reset();
        }

        private void reset()
        {
            groupBox1.Text = UnitUtil.Distance.LabelAxis;
            groupBox2.Text = UnitUtil.Time.LabelAxis;
            groupBox3.Text = UnitUtil.Elevation.LabelAxis;
            groupBox4.Text = StringResources.HRZone + UnitUtil.HeartRate.LabelAbbr2;
            groupBox5.Text = StringResources.SpeedZone;

            String paceType = UnitUtil.Pace.Label;
            paceTypeBox.Items.Add(paceType);
            paceTypeBox.Items.Add(UnitUtil.Speed.LabelAbbr);
            paceTypeBox.SelectedItem = paceType;
            setPaceLabel();
            distanceLabel.Text = UnitUtil.Distance.LabelAbbr;
            correctUI(new Control[] { distanceInputBox, distanceLabel });
            elevationLabel.Text = UnitUtil.Elevation.LabelAbbr;
            correctUI(new Control[] { elevationInputBox, elevationLabel });
            populateLists();
            ignoreManualBox.Checked = Settings.IgnoreManualData;
        }

        void HighScoreSettingPageControl_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName.Equals("DistanceUnits")||e.PropertyName.Equals("ElevationUnits"))
            {
                reset();
            }
        }

        void paceTypeBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            setPaceLabel();
            populateSpeedList();
        }

        private void setPaceLabel()
        {
            bool isPace = UnitUtil.Pace.isLabelPace((String)paceTypeBox.SelectedItem);
            groupBox5.Text = StringResources.SpeedZone + UnitUtil.PaceOrSpeed.LabelAbbr2(isPace);
            paceLabelFrom.Text = UnitUtil.PaceOrSpeed.LabelAbbr(isPace) + " " + ZoneFiveSoftware.Common.Visuals.CommonResources.Text.LabelFrom.ToLower();
            paceLabelTo.Text = UnitUtil.PaceOrSpeed.LabelAbbr(isPace) + " " + ZoneFiveSoftware.Common.Visuals.CommonResources.Text.LabelTo.ToLower();

        }

        private void populateLists()
        {
            populateDistanceList();
            populateTimeList();
            populateElevationList();
            populatePulseList();
            populateSpeedList();
        }

        private void populateSpeedList()
        {
            speedZoneIndex = new List<IList<double>>();
            speedBox.Items.Clear();
            foreach (double min in Settings.speedZones.Keys)
                foreach (double max in Settings.speedZones[min].Keys)
                {
                    string from, to;
                    bool isPace = UnitUtil.Pace.isLabelPace((String)paceTypeBox.SelectedItem);
                    if (isPace)
                    {
                        from = UnitUtil.Pace.ToString(min);
                        to = UnitUtil.Pace.ToString(max);
                    }
                    else
                    {
                        from = UnitUtil.Speed.ToString(min);
                        to = UnitUtil.Speed.ToString(max);
                    }
                    speedBox.Items.Add(String.Format("{0} - {1}", from, to));
                    speedZoneIndex.Add(new double[] { min, max });
                }
        }

        private void populatePulseList()
        {
            pulseBox.Items.Clear();
            foreach (double min in Settings.pulseZones.Keys)
            {
                foreach (double max in Settings.pulseZones[min].Keys)
                {
                    pulseBox.Items.Add(String.Format("{0} - {1}", min, max));
                }
            }
        }
        
        private void populateTimeList()
        {
            timeBox.Items.Clear();
            foreach (TimeSpan time in Settings.times.Values)
            {

                timeBox.Items.Add(String.Format("{0}:{1}:{2}", padFront(time.Hours), padFront(time.Minutes), padFront(time.Seconds)));
            }
        }

        private String padFront(int p)
        {
            if (p < 10) return "0" + p;
            else return p.ToString();
        }

        private void populateDistanceList()
        {
            distanceBox.Items.Clear();
            foreach (double distance in Settings.distances.Keys)
            {
                distanceBox.Items.Add(UnitUtil.Distance.ToString(distance));
            }
        }

        private void populateElevationList()
        {
            elevationBox.Items.Clear();
            foreach (double elevation in Settings.elevations.Keys)
            {
                elevationBox.Items.Add(UnitUtil.Elevation.ToString(elevation));
            }
        }

        private void addDistance_Click(object sender, EventArgs e)
        {
            try
            {
                double distance = UnitUtil.Distance.Parse(distanceInputBox.Text);
                if (distance <= 0) { throw new Exception(); }
                if (!Settings.distances.ContainsKey(distance))
                {
                    Settings.distances.Add(distance, true);
                    populateDistanceList();
                }
            }
            catch (Exception) 
            {
                new WarningDialog(Resources.CannotAddToDistances);
            }
        }

        private void removeDistance_Click(object sender, EventArgs e)
        {
            if (distanceBox.SelectedIndex >= 0)
            {
                Settings.distances.RemoveAt(distanceBox.SelectedIndex);
                populateDistanceList();
            }
        }

        private void addTime_Click(object sender, EventArgs e)
        {
                try
                {
                    int seconds = (int)UnitUtil.Time.Parse(timeInputBox.Text);
                    if (seconds <= 0) { throw new Exception(); }
                    if (!Settings.times.ContainsKey(seconds))
                    {
                        Settings.times.Add(seconds, new TimeSpan(0, 0, seconds));
                        populateTimeList();
                    }
                }
                catch (Exception)
                {
                    new WarningDialog(Resources.CannotAddToTimes);
                }
        }

        private void removeTime_Click(object sender, EventArgs e)
        {
            if (timeBox.SelectedIndex >= 0)
            {
                Settings.times.RemoveAt(timeBox.SelectedIndex);
                populateTimeList();
            }
        }

        private void addElevation_Click(object sender, EventArgs e)
        {
            try
            {
                double elevation = UnitUtil.Elevation.Parse(elevationInputBox.Text);
                if (elevation <= 0) { throw new Exception(); }
                if (!Settings.elevations.ContainsKey(elevation))
                {
                    Settings.elevations.Add(elevation, true);
                    populateElevationList();
                }
            }
            catch (Exception)
            {
                new WarningDialog(Resources.CannotAddToElevations);
            }
        }

        private void resetSettings_Click(object sender, EventArgs e)
        {
            dialog = new YesNoDialog(Resources.ResetAllSettingsWarning);
            dialog.Disposed += new EventHandler(dialog_Disposed);
            dialog.ShowDialog();
        }

        YesNoDialog dialog;

        private void dialog_Disposed(object sender, EventArgs e)
        {
            if (dialog.answer)
            {
                Settings.defaults();
                populateLists();
            }
        }

        private void addPulse_Click(object sender, EventArgs e)
        {
            try
            {
                double min = UnitUtil.HeartRate.Parse(minPulseBox.Text);
                double max = UnitUtil.HeartRate.Parse(maxPulseBox.Text);
                if (min < 0 || max < 0 || min > max) throw new Exception();
                Settings.addPulse(min, max);
                populatePulseList();
            }
            catch (Exception)
            {
                new WarningDialog(Resources.CannotAddToHRZones);
            }
        }

        private void removePulse_Click(object sender, EventArgs e)
        {
            if (pulseBox.SelectedIndex >= 0)
            {
                String[] values = ((String)pulseBox.SelectedItem).Split('-');
                double min = UnitUtil.HeartRate.Parse(values[0]);
                double max = UnitUtil.HeartRate.Parse(values[1]);
                Settings.pulseZones[min].Remove(max);
                populatePulseList();
            }
        }

        private void addPace_Click(object sender, EventArgs e)
        {
            try
            {
                double min, max;
                bool isPace = UnitUtil.Pace.isLabelPace((String)paceTypeBox.SelectedItem);
                if (isPace)
                {
                    min = UnitUtil.Pace.Parse(maxSpeedBox.Text);
                    max = UnitUtil.Pace.Parse(minSpeedBox.Text);
                }
                else
                {
                    min = UnitUtil.Speed.Parse(minSpeedBox.Text);
                    max = UnitUtil.Speed.Parse(maxSpeedBox.Text);
                }
                if (min <= 0 || max <= 0 || max <= min) throw new Exception();
                Settings.addSpeed(min, max);
                populateSpeedList();
            }
            catch (Exception)
            {
                if (UnitUtil.Pace.isLabelPace((String)paceTypeBox.SelectedItem))
                    new WarningDialog(Resources.CannotAddToPaceZones);
                else
                    new WarningDialog(Resources.CannotAddToSpeedZones);
            }
        }

        private void removePace_Click(object sender, EventArgs e)
        {
            int index = speedBox.SelectedIndex;
            if (index >= 0)
            {
                Settings.speedZones[speedZoneIndex[index][0]].Remove(speedZoneIndex[index][1]);
                populateSpeedList();
            }
        }

        private void resetDistances_Click(object sender, EventArgs e)
        {
            dialog = new YesNoDialog(Resources.ResetDistancesWarning);
            dialog.Disposed += new EventHandler(dialog_Disposed_Distances);
            dialog.ShowDialog();
        }

        void dialog_Disposed_Distances(object sender, EventArgs e)
        {
            if (dialog.answer)
            {
                Settings.resetDistances();
                populateDistanceList();
            }
        }

        private void resetTimes_Click(object sender, EventArgs e)
        {
            dialog = new YesNoDialog(Resources.ResetTimesWarning);
            dialog.Disposed += new EventHandler(dialog_Disposed_times);
            dialog.ShowDialog();
        }

        void dialog_Disposed_times(object sender, EventArgs e)
        {
            if (dialog.answer)
            {
                Settings.resetTimes();
                populateTimeList();
            }
        }

        private void resetElevations_Click(object sender, EventArgs e)
        {
            dialog = new YesNoDialog(Resources.ResetElevationsWarning);
            dialog.Disposed += new EventHandler(dialog_Disposed_elevations);
            dialog.ShowDialog();
        }

        void dialog_Disposed_elevations(object sender, EventArgs e)
        {
            if (dialog.answer)
            {
                Settings.resetElevations();
                populateElevationList();
            }
        }

        private void resetPulseZone_Click(object sender, EventArgs e)
        {
            dialog = new YesNoDialog(Resources.ResetHRZonesWarning);
            dialog.Disposed += new EventHandler(dialog_Disposed_pulsezones);
            dialog.ShowDialog();
        }

        void dialog_Disposed_pulsezones(object sender, EventArgs e)
        {
            if (dialog.answer)
            {
                Settings.resetPulseZones();
                populatePulseList();
            }
        }

        private void resetPaceZone_Click(object sender, EventArgs e)
        {
            dialog = new YesNoDialog(Resources.ResetPaceZonesWarning);
            dialog.Disposed += new EventHandler(dialog_Disposed_pacezones);
            dialog.ShowDialog();
        }

        void dialog_Disposed_pacezones(object sender, EventArgs e)
        {
            if (dialog.answer)
            {
                Settings.resetSpeedZones();
                populateSpeedList();
            }
        }

        private void removeElevation_Click(object sender, EventArgs e)
        {
            if (elevationBox.SelectedIndex >= 0)
            {
                Settings.elevations.RemoveAt(elevationBox.SelectedIndex);
                populateElevationList();
            }
        }

        private void ignoreManualBox_CheckedChanged(object sender, EventArgs e)
        {
            Settings.IgnoreManualData = ignoreManualBox.Checked;
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(new ProcessStartInfo("IExplore",
                "http://code.google.com/p/gps-running/wiki/HighScore"));
        }

    }
}
