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
using ZoneFiveSoftware.Common.Visuals.Fitness;
using ZoneFiveSoftware.Common.Data.GPS;
using ZoneFiveSoftware.Common.Data;
using System.Collections;
using System.IO;
using System.Xml;
using SportTracksHighScorePlugin.Properties;

namespace SportTracksHighScorePlugin.Source
{
    public partial class HighScoreSettingPageControl : UserControl
    {
        IList<IList<double>> speedZoneIndex;

        public HighScoreSettingPageControl()
        {
            InitializeComponent();
            setLanguage();
            paceTypeBox.DropDownStyle = ComboBoxStyle.DropDownList;
            paceTypeBox.SelectedIndexChanged += new EventHandler(paceTypeBox_SelectedIndexChanged);
            reset();
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

        private void setLanguage()
        {
            resetSettings.Text = Resources.ResetAllSettings;
            linkLabel1.Text = Resources.Webpage;
            ignoreManualBox.Text = Resources.IgnoreManualEnteredBox;
            groupBox1.Text = Resources.Distances;
            groupBox2.Text = Resources.Times;
            groupBox3.Text = Resources.Elevations;
            groupBox4.Text = Resources.HRZones;
            groupBox5.Text = Resources.PaceZones;
            addDistance.Text = "<-- " + Resources.Add;
            removeDistance.Text = Resources.Remove + " -->";
            resetDistances.Text = Resources.Reset;
            addTime.Text = "<-- " + Resources.Add;
            removeTime.Text = Resources.Remove + " -->";
            resetTimes.Text = Resources.Reset;
            addElevation.Text = "<-- " + Resources.Add;
            removeElevation.Text = Resources.Remove + " -->";
            resetElevations.Text = Resources.Reset;
            addPulse.Text = "<-- " + Resources.Add;
            removePulse.Text = Resources.Remove + " -->";
            resetPulseZone.Text = Resources.Reset;
            addPace.Text = "<-- " + Resources.Add;
            removePace.Text = Resources.Remove + " -->";
            resetPaceZone.Text = Resources.Reset;
            label1.Text = Resources.BPMFrom;
            label2.Text = Resources.BPMTo;
            label4.Text = "(" + Resources.TimeFormat + ")";
        }

        private void reset()
        {
            String paceType = String.Format(Resources.PaceFormat,Settings.DistanceUnitShort);
            paceTypeBox.Items.Add(paceType);
            paceTypeBox.Items.Add(Settings.DistanceUnitShort + "/"+Time.Label(Time.TimeRange.Hour));
            paceTypeBox.SelectedItem = paceType;
            setPaceLabel();
            distanceLabel.Text = Settings.DistanceUnit;
            correctUI(new Control[] { distanceInputBox, distanceLabel });
            elevationLabel.Text = Settings.ElevationUnit;
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
            if (((String)paceTypeBox.SelectedItem).Contains("min/"))
            {
                paceLabelFrom.Text = Resources.PaceLabelFrom;
                paceLabelTo.Text = Resources.PaceLabelTo;
            }
            else
            {
                paceLabelFrom.Text = String.Format(Resources.SpeedLabelFrom, Settings.DistanceUnit);
                paceLabelTo.Text = String.Format(Resources.SpeedLabelTo, Settings.DistanceUnit);
            }
        }

        private void populateLists()
        {
            populateDistanceList();
            populateTimeList();
            populateElevationList();
            populatePulseList();
            populateSpeedList();
        }

        private String present(double d)
        {
            return string.Format("{0:0.000}", d);
        }

        private void populateSpeedList()
        {
            speedZoneIndex = new List<IList<double>>();
            speedBox.Items.Clear();
            foreach (double min in Settings.speedZones.Keys)
                foreach (double max in Settings.speedZones[min].Keys)
                {
                    if (((String)paceTypeBox.SelectedItem).Contains("min/"))
                    {
                        speedBox.Items.Insert(0, String.Format("{1} - {0}",
                            new TimeSpan(0, 0, (int)Math.Round(1.0/(HighScore.convertFromDistance(min)))).ToString().Substring(3),
                            new TimeSpan(0, 0, (int)Math.Round(1.0/(HighScore.convertFromDistance(max)))).ToString().Substring(3)));
                        speedZoneIndex.Insert(0, new double[] { min, max });
                    }
                    else
                    {
                        speedBox.Items.Add(String.Format("{1} - {0}", present(HighScore.convertFromDistance(3600*max)),
                            present(HighScore.convertFromDistance(3600*min))));
                        speedZoneIndex.Add(new double[] { min, max });
                    }                    
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
                distanceBox.Items.Add(present(HighScore.convertFromDistance(distance)));
            }
        }

        private void populateElevationList()
        {
            elevationBox.Items.Clear();
            foreach (double elevation in Settings.elevations.Keys)
            {
                elevationBox.Items.Add(present(HighScore.convertFromElevation(elevation)));
            }
        }

        private void addDistance_Click(object sender, EventArgs e)
        {
            try
            {
                double distance = double.Parse(distanceInputBox.Text);
                distance = convertToDistance(distance);
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
            String[] split = timeInputBox.Text.Split(':');
            if (split.Length == 3)
            {
                int hh, mm, ss;
                try
                {
                    hh = int.Parse(split[0]);
                    mm = int.Parse(split[1]);
                    ss = int.Parse(split[2]);
                    if (hh < 24 && hh >= 0 && mm < 60 && mm >= 0 && ss < 60 && ss >= 0)
                    {
                        int seconds = 3600 * hh + 60 * mm + ss;
                        if (!Settings.times.ContainsKey(seconds))
                        {
                            Settings.times.Add(seconds, new TimeSpan(hh, mm, ss));
                            populateTimeList();
                        }
                    }
                }
                catch (Exception) 
                {
                    new WarningDialog(Resources.CannotAddToTimes);
                }
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
                double elevation = double.Parse(elevationInputBox.Text);
                elevation = Settings.convertTo(elevation,Plugin.GetApplication().SystemPreferences.ElevationUnits);
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
                double min = Settings.parseDouble(minPulseBox.Text);
                double max = Settings.parseDouble(maxPulseBox.Text);
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
                double min = double.Parse(values[0]);
                double max = double.Parse(values[1]);
                Settings.pulseZones[min].Remove(max);
                populatePulseList();
            }
        }

        public static double convertToDistance(double p)
        {
            return Settings.convertTo(p,Plugin.GetApplication().SystemPreferences.DistanceUnits);
        }

        private void addPace_Click(object sender, EventArgs e)
        {
            try
            {
                double min, max;
                if (((String)paceTypeBox.SelectedItem).Contains("min/"))
                {
                    String[] pair = minSpeedBox.Text.Split(':');//min/?, ?/min, ?/min*min/sec 
                    max = 1.0 / (60 * int.Parse(pair[0]) + int.Parse(pair[1]));
                    max = convertToDistance(max);
                    pair = maxSpeedBox.Text.Split(':');
                    min = 1.0 / (60*int.Parse(pair[0]) + int.Parse(pair[1]));
                    min = convertToDistance(min);
                }
                else
                {
                    max = double.Parse(maxSpeedBox.Text);
                    max = convertToDistance(max)/3600.0;
                    min = double.Parse(minSpeedBox.Text);
                    min = convertToDistance(min)/3600.0;
                }
                if (min <= 0 || max <= 0 || max <= min) throw new Exception();
                Settings.addSpeed(min, max);
                populateSpeedList();
            }
            catch (Exception)
            {
                if (((String)paceTypeBox.SelectedItem).Contains("min/"))
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
