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

namespace GpsRunningPlugin.Source
{
    partial class HighScoreSettingPageControl
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.distanceBox = new System.Windows.Forms.ListBox();
            this.removeDistance = new ZoneFiveSoftware.Common.Visuals.Button();
            this.distanceInputBox = new ZoneFiveSoftware.Common.Visuals.TextBox();
            this.timeBox = new System.Windows.Forms.ListBox();
            this.addTime = new ZoneFiveSoftware.Common.Visuals.Button();
            this.addDistance = new ZoneFiveSoftware.Common.Visuals.Button();
            this.removeTime = new ZoneFiveSoftware.Common.Visuals.Button();
            this.timeInputBox = new ZoneFiveSoftware.Common.Visuals.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.resetDistances = new ZoneFiveSoftware.Common.Visuals.Button();
            this.distanceLabel = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.resetTimes = new ZoneFiveSoftware.Common.Visuals.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.resetElevations = new ZoneFiveSoftware.Common.Visuals.Button();
            this.elevationLabel = new System.Windows.Forms.Label();
            this.elevationInputBox = new ZoneFiveSoftware.Common.Visuals.TextBox();
            this.removeElevation = new ZoneFiveSoftware.Common.Visuals.Button();
            this.addElevation = new ZoneFiveSoftware.Common.Visuals.Button();
            this.elevationBox = new System.Windows.Forms.ListBox();
            this.resetSettings = new ZoneFiveSoftware.Common.Visuals.Button();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.resetPulseZone = new ZoneFiveSoftware.Common.Visuals.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.maxPulseBox = new ZoneFiveSoftware.Common.Visuals.TextBox();
            this.minPulseBox = new ZoneFiveSoftware.Common.Visuals.TextBox();
            this.removePulse = new ZoneFiveSoftware.Common.Visuals.Button();
            this.addPulse = new ZoneFiveSoftware.Common.Visuals.Button();
            this.pulseBox = new System.Windows.Forms.ListBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.resetPaceZone = new ZoneFiveSoftware.Common.Visuals.Button();
            this.paceTypeBox = new System.Windows.Forms.ComboBox();
            this.paceLabelTo = new System.Windows.Forms.Label();
            this.paceLabelFrom = new System.Windows.Forms.Label();
            this.maxSpeedBox = new ZoneFiveSoftware.Common.Visuals.TextBox();
            this.minSpeedBox = new ZoneFiveSoftware.Common.Visuals.TextBox();
            this.removePace = new ZoneFiveSoftware.Common.Visuals.Button();
            this.addPace = new ZoneFiveSoftware.Common.Visuals.Button();
            this.speedBox = new System.Windows.Forms.ListBox();
            this.ignoreManualBox = new System.Windows.Forms.CheckBox();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.highScoreControl1 = new HighScoreControl();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.SuspendLayout();
            // 
            // distanceBox
            // 
            this.distanceBox.FormattingEnabled = true;
            this.distanceBox.Location = new System.Drawing.Point(6, 19);
            this.distanceBox.Name = "distanceBox";
            this.distanceBox.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.distanceBox.Size = new System.Drawing.Size(88, 69);
            this.distanceBox.TabIndex = 5;
            // 
            // removeDistance
            // 
            this.removeDistance.BackColor = System.Drawing.Color.Transparent;
            this.removeDistance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(40)))), ((int)(((byte)(50)))), ((int)(((byte)(120)))));
            this.removeDistance.CenterImage = null;
            this.removeDistance.DialogResult = System.Windows.Forms.DialogResult.None;
            this.removeDistance.HyperlinkStyle = false;
            this.removeDistance.ImageMargin = 2;
            this.removeDistance.LeftImage = null;
            this.removeDistance.Location = new System.Drawing.Point(100, 65);
            this.removeDistance.Name = "removeDistance";
            this.removeDistance.PushStyle = true;
            this.removeDistance.RightImage = null;
            this.removeDistance.Size = new System.Drawing.Size(111, 23);
            this.removeDistance.TabIndex = 6;
            this.removeDistance.Text = "Remove --->";
            this.removeDistance.TextAlign = System.Drawing.StringAlignment.Center;
            this.removeDistance.TextLeftMargin = 2;
            this.removeDistance.TextRightMargin = 2;
            this.removeDistance.Click += new System.EventHandler(this.removeDistance_Click);
            // 
            // distanceInputBox
            // 
            this.distanceInputBox.AcceptsReturn = false;
            this.distanceInputBox.AcceptsTab = false;
            this.distanceInputBox.BackColor = System.Drawing.Color.White;
            this.distanceInputBox.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(123)))), ((int)(((byte)(114)))), ((int)(((byte)(108)))));
            this.distanceInputBox.ButtonImage = null;
            this.distanceInputBox.Location = new System.Drawing.Point(217, 21);
            this.distanceInputBox.MaxLength = 32767;
            this.distanceInputBox.Multiline = false;
            this.distanceInputBox.Name = "distanceInputBox";
            this.distanceInputBox.ReadOnly = false;
            this.distanceInputBox.ReadOnlyColor = System.Drawing.SystemColors.Control;
            this.distanceInputBox.ReadOnlyTextColor = System.Drawing.SystemColors.ControlLight;
            this.distanceInputBox.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.distanceInputBox.Size = new System.Drawing.Size(71, 20);
            this.distanceInputBox.TabIndex = 2;
            this.distanceInputBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            // 
            // timeBox
            // 
            this.timeBox.FormattingEnabled = true;
            this.timeBox.Location = new System.Drawing.Point(6, 19);
            this.timeBox.Name = "timeBox";
            this.timeBox.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.timeBox.Size = new System.Drawing.Size(88, 69);
            this.timeBox.TabIndex = 10;
            // 
            // addTime
            // 
            this.addTime.BackColor = System.Drawing.Color.Transparent;
            this.addTime.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(40)))), ((int)(((byte)(50)))), ((int)(((byte)(120)))));
            this.addTime.CenterImage = null;
            this.addTime.DialogResult = System.Windows.Forms.DialogResult.None;
            this.addTime.HyperlinkStyle = false;
            this.addTime.ImageMargin = 2;
            this.addTime.LeftImage = null;
            this.addTime.Location = new System.Drawing.Point(100, 19);
            this.addTime.Name = "addTime";
            this.addTime.PushStyle = true;
            this.addTime.RightImage = null;
            this.addTime.Size = new System.Drawing.Size(111, 23);
            this.addTime.TabIndex = 9;
            this.addTime.Text = "<--- Add";
            this.addTime.TextAlign = System.Drawing.StringAlignment.Center;
            this.addTime.TextLeftMargin = 2;
            this.addTime.TextRightMargin = 2;
            this.addTime.Click += new System.EventHandler(this.addTime_Click);
            // 
            // addDistance
            // 
            this.addDistance.BackColor = System.Drawing.Color.Transparent;
            this.addDistance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(40)))), ((int)(((byte)(50)))), ((int)(((byte)(120)))));
            this.addDistance.CenterImage = null;
            this.addDistance.DialogResult = System.Windows.Forms.DialogResult.None;
            this.addDistance.HyperlinkStyle = false;
            this.addDistance.ImageMargin = 2;
            this.addDistance.LeftImage = null;
            this.addDistance.Location = new System.Drawing.Point(100, 19);
            this.addDistance.Name = "addDistance";
            this.addDistance.PushStyle = true;
            this.addDistance.RightImage = null;
            this.addDistance.Size = new System.Drawing.Size(111, 23);
            this.addDistance.TabIndex = 3;
            this.addDistance.Text = "<--- Add";
            this.addDistance.TextAlign = System.Drawing.StringAlignment.Center;
            this.addDistance.TextLeftMargin = 2;
            this.addDistance.TextRightMargin = 2;
            this.addDistance.Click += new System.EventHandler(this.addDistance_Click);
            // 
            // removeTime
            // 
            this.removeTime.BackColor = System.Drawing.Color.Transparent;
            this.removeTime.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(40)))), ((int)(((byte)(50)))), ((int)(((byte)(120)))));
            this.removeTime.CenterImage = null;
            this.removeTime.DialogResult = System.Windows.Forms.DialogResult.None;
            this.removeTime.HyperlinkStyle = false;
            this.removeTime.ImageMargin = 2;
            this.removeTime.LeftImage = null;
            this.removeTime.Location = new System.Drawing.Point(100, 65);
            this.removeTime.Name = "removeTime";
            this.removeTime.PushStyle = true;
            this.removeTime.RightImage = null;
            this.removeTime.Size = new System.Drawing.Size(111, 23);
            this.removeTime.TabIndex = 11;
            this.removeTime.Text = "Remove --->";
            this.removeTime.TextAlign = System.Drawing.StringAlignment.Center;
            this.removeTime.TextLeftMargin = 2;
            this.removeTime.TextRightMargin = 2;
            this.removeTime.Click += new System.EventHandler(this.removeTime_Click);
            // 
            // timeInputBox
            // 
            this.timeInputBox.AcceptsReturn = false;
            this.timeInputBox.AcceptsTab = false;
            this.timeInputBox.BackColor = System.Drawing.Color.White;
            this.timeInputBox.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(123)))), ((int)(((byte)(114)))), ((int)(((byte)(108)))));
            this.timeInputBox.ButtonImage = null;
            this.timeInputBox.Location = new System.Drawing.Point(218, 19);
            this.timeInputBox.MaxLength = 32767;
            this.timeInputBox.Multiline = false;
            this.timeInputBox.Name = "timeInputBox";
            this.timeInputBox.ReadOnly = false;
            this.timeInputBox.ReadOnlyColor = System.Drawing.SystemColors.Control;
            this.timeInputBox.ReadOnlyTextColor = System.Drawing.SystemColors.ControlLight;
            this.timeInputBox.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.timeInputBox.Size = new System.Drawing.Size(70, 20);
            this.timeInputBox.TabIndex = 8;
            this.timeInputBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(291, 22);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(57, 13);
            this.label4.TabIndex = 17;
            this.label4.Text = "(hh:mm:ss)";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.resetDistances);
            this.groupBox1.Controls.Add(this.distanceLabel);
            this.groupBox1.Controls.Add(this.distanceBox);
            this.groupBox1.Controls.Add(this.addDistance);
            this.groupBox1.Controls.Add(this.removeDistance);
            this.groupBox1.Controls.Add(this.distanceInputBox);
            this.groupBox1.Location = new System.Drawing.Point(3, 55);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(398, 100);
            this.groupBox1.TabIndex = 18;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Distance";
            // 
            // resetDistances
            // 
            this.resetDistances.BackColor = System.Drawing.Color.Transparent;
            this.resetDistances.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(40)))), ((int)(((byte)(50)))), ((int)(((byte)(120)))));
            this.resetDistances.CenterImage = null;
            this.resetDistances.DialogResult = System.Windows.Forms.DialogResult.None;
            this.resetDistances.HyperlinkStyle = false;
            this.resetDistances.ImageMargin = 2;
            this.resetDistances.LeftImage = null;
            this.resetDistances.Location = new System.Drawing.Point(289, 65);
            this.resetDistances.Name = "resetDistances";
            this.resetDistances.PushStyle = true;
            this.resetDistances.RightImage = null;
            this.resetDistances.Size = new System.Drawing.Size(103, 23);
            this.resetDistances.TabIndex = 7;
            this.resetDistances.Text = "Reset...";
            this.resetDistances.TextAlign = System.Drawing.StringAlignment.Center;
            this.resetDistances.TextLeftMargin = 2;
            this.resetDistances.TextRightMargin = 2;
            this.resetDistances.Click += new System.EventHandler(this.resetDistances_Click);
            // 
            // distanceLabel
            // 
            this.distanceLabel.AutoSize = true;
            this.distanceLabel.Location = new System.Drawing.Point(255, 24);
            this.distanceLabel.Name = "distanceLabel";
            this.distanceLabel.Size = new System.Drawing.Size(0, 13);
            this.distanceLabel.TabIndex = 18;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.resetTimes);
            this.groupBox2.Controls.Add(this.timeBox);
            this.groupBox2.Controls.Add(this.addTime);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.removeTime);
            this.groupBox2.Controls.Add(this.timeInputBox);
            this.groupBox2.Location = new System.Drawing.Point(3, 161);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(398, 100);
            this.groupBox2.TabIndex = 19;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Time";
            // 
            // resetTimes
            // 
            this.resetTimes.BackColor = System.Drawing.Color.Transparent;
            this.resetTimes.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(40)))), ((int)(((byte)(50)))), ((int)(((byte)(120)))));
            this.resetTimes.CenterImage = null;
            this.resetTimes.DialogResult = System.Windows.Forms.DialogResult.None;
            this.resetTimes.HyperlinkStyle = false;
            this.resetTimes.ImageMargin = 2;
            this.resetTimes.LeftImage = null;
            this.resetTimes.Location = new System.Drawing.Point(289, 65);
            this.resetTimes.Name = "resetTimes";
            this.resetTimes.PushStyle = true;
            this.resetTimes.RightImage = null;
            this.resetTimes.Size = new System.Drawing.Size(103, 23);
            this.resetTimes.TabIndex = 12;
            this.resetTimes.Text = "Reset...";
            this.resetTimes.TextAlign = System.Drawing.StringAlignment.Center;
            this.resetTimes.TextLeftMargin = 2;
            this.resetTimes.TextRightMargin = 2;
            this.resetTimes.Click += new System.EventHandler(this.resetTimes_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.resetElevations);
            this.groupBox3.Controls.Add(this.elevationLabel);
            this.groupBox3.Controls.Add(this.elevationInputBox);
            this.groupBox3.Controls.Add(this.removeElevation);
            this.groupBox3.Controls.Add(this.addElevation);
            this.groupBox3.Controls.Add(this.elevationBox);
            this.groupBox3.Location = new System.Drawing.Point(3, 267);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(398, 100);
            this.groupBox3.TabIndex = 20;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Elevation";
            // 
            // resetElevations
            // 
            this.resetElevations.BackColor = System.Drawing.Color.Transparent;
            this.resetElevations.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(40)))), ((int)(((byte)(50)))), ((int)(((byte)(120)))));
            this.resetElevations.CenterImage = null;
            this.resetElevations.DialogResult = System.Windows.Forms.DialogResult.None;
            this.resetElevations.HyperlinkStyle = false;
            this.resetElevations.ImageMargin = 2;
            this.resetElevations.LeftImage = null;
            this.resetElevations.Location = new System.Drawing.Point(289, 65);
            this.resetElevations.Name = "resetElevations";
            this.resetElevations.PushStyle = true;
            this.resetElevations.RightImage = null;
            this.resetElevations.Size = new System.Drawing.Size(103, 23);
            this.resetElevations.TabIndex = 17;
            this.resetElevations.Text = "Reset...";
            this.resetElevations.TextAlign = System.Drawing.StringAlignment.Center;
            this.resetElevations.TextLeftMargin = 2;
            this.resetElevations.TextRightMargin = 2;
            this.resetElevations.Click += new System.EventHandler(this.resetElevations_Click);
            // 
            // elevationLabel
            // 
            this.elevationLabel.AutoSize = true;
            this.elevationLabel.Location = new System.Drawing.Point(255, 24);
            this.elevationLabel.Name = "elevationLabel";
            this.elevationLabel.Size = new System.Drawing.Size(0, 13);
            this.elevationLabel.TabIndex = 18;
            // 
            // elevationInputBox
            // 
            this.elevationInputBox.AcceptsReturn = false;
            this.elevationInputBox.AcceptsTab = false;
            this.elevationInputBox.BackColor = System.Drawing.Color.White;
            this.elevationInputBox.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(123)))), ((int)(((byte)(114)))), ((int)(((byte)(108)))));
            this.elevationInputBox.ButtonImage = null;
            this.elevationInputBox.Location = new System.Drawing.Point(217, 21);
            this.elevationInputBox.MaxLength = 32767;
            this.elevationInputBox.Multiline = false;
            this.elevationInputBox.Name = "elevationInputBox";
            this.elevationInputBox.ReadOnly = false;
            this.elevationInputBox.ReadOnlyColor = System.Drawing.SystemColors.Control;
            this.elevationInputBox.ReadOnlyTextColor = System.Drawing.SystemColors.ControlLight;
            this.elevationInputBox.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.elevationInputBox.Size = new System.Drawing.Size(70, 20);
            this.elevationInputBox.TabIndex = 13;
            this.elevationInputBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            // 
            // removeElevation
            // 
            this.removeElevation.BackColor = System.Drawing.Color.Transparent;
            this.removeElevation.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(40)))), ((int)(((byte)(50)))), ((int)(((byte)(120)))));
            this.removeElevation.CenterImage = null;
            this.removeElevation.DialogResult = System.Windows.Forms.DialogResult.None;
            this.removeElevation.HyperlinkStyle = false;
            this.removeElevation.ImageMargin = 2;
            this.removeElevation.LeftImage = null;
            this.removeElevation.Location = new System.Drawing.Point(100, 65);
            this.removeElevation.Name = "removeElevation";
            this.removeElevation.PushStyle = true;
            this.removeElevation.RightImage = null;
            this.removeElevation.Size = new System.Drawing.Size(111, 23);
            this.removeElevation.TabIndex = 16;
            this.removeElevation.Text = "Remove --->";
            this.removeElevation.TextAlign = System.Drawing.StringAlignment.Center;
            this.removeElevation.TextLeftMargin = 2;
            this.removeElevation.TextRightMargin = 2;
            this.removeElevation.Click += new System.EventHandler(this.removeElevation_Click);
            // 
            // addElevation
            // 
            this.addElevation.BackColor = System.Drawing.Color.Transparent;
            this.addElevation.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(40)))), ((int)(((byte)(50)))), ((int)(((byte)(120)))));
            this.addElevation.CenterImage = null;
            this.addElevation.DialogResult = System.Windows.Forms.DialogResult.None;
            this.addElevation.HyperlinkStyle = false;
            this.addElevation.ImageMargin = 2;
            this.addElevation.LeftImage = null;
            this.addElevation.Location = new System.Drawing.Point(100, 19);
            this.addElevation.Name = "addElevation";
            this.addElevation.PushStyle = true;
            this.addElevation.RightImage = null;
            this.addElevation.Size = new System.Drawing.Size(111, 23);
            this.addElevation.TabIndex = 14;
            this.addElevation.Text = "<--- Add";
            this.addElevation.TextAlign = System.Drawing.StringAlignment.Center;
            this.addElevation.TextLeftMargin = 2;
            this.addElevation.TextRightMargin = 2;
            this.addElevation.Click += new System.EventHandler(this.addElevation_Click);
            // 
            // elevationBox
            // 
            this.elevationBox.FormattingEnabled = true;
            this.elevationBox.Location = new System.Drawing.Point(6, 19);
            this.elevationBox.Name = "elevationBox";
            this.elevationBox.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.elevationBox.Size = new System.Drawing.Size(88, 69);
            this.elevationBox.TabIndex = 15;
            // 
            // resetSettings
            // 
            this.resetSettings.BackColor = System.Drawing.Color.Transparent;
            this.resetSettings.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(40)))), ((int)(((byte)(50)))), ((int)(((byte)(120)))));
            this.resetSettings.CenterImage = null;
            this.resetSettings.DialogResult = System.Windows.Forms.DialogResult.None;
            this.resetSettings.HyperlinkStyle = false;
            this.resetSettings.ImageMargin = 2;
            this.resetSettings.LeftImage = null;
            this.resetSettings.Location = new System.Drawing.Point(3, 3);
            this.resetSettings.Name = "resetSettings";
            this.resetSettings.PushStyle = true;
            this.resetSettings.RightImage = null;
            this.resetSettings.Size = new System.Drawing.Size(188, 23);
            this.resetSettings.TabIndex = 1;
            this.resetSettings.Text = "Reset all settings...";
            this.resetSettings.TextAlign = System.Drawing.StringAlignment.Center;
            this.resetSettings.TextLeftMargin = 2;
            this.resetSettings.TextRightMargin = 2;
            this.resetSettings.Click += new System.EventHandler(this.resetSettings_Click);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.resetPulseZone);
            this.groupBox4.Controls.Add(this.label2);
            this.groupBox4.Controls.Add(this.label1);
            this.groupBox4.Controls.Add(this.maxPulseBox);
            this.groupBox4.Controls.Add(this.minPulseBox);
            this.groupBox4.Controls.Add(this.removePulse);
            this.groupBox4.Controls.Add(this.addPulse);
            this.groupBox4.Controls.Add(this.pulseBox);
            this.groupBox4.Location = new System.Drawing.Point(3, 373);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(398, 100);
            this.groupBox4.TabIndex = 21;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Heart rate zone";
            // 
            // resetPulseZone
            // 
            this.resetPulseZone.BackColor = System.Drawing.Color.Transparent;
            this.resetPulseZone.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(40)))), ((int)(((byte)(50)))), ((int)(((byte)(120)))));
            this.resetPulseZone.CenterImage = null;
            this.resetPulseZone.DialogResult = System.Windows.Forms.DialogResult.None;
            this.resetPulseZone.HyperlinkStyle = false;
            this.resetPulseZone.ImageMargin = 2;
            this.resetPulseZone.LeftImage = null;
            this.resetPulseZone.Location = new System.Drawing.Point(289, 65);
            this.resetPulseZone.Name = "resetPulseZone";
            this.resetPulseZone.PushStyle = true;
            this.resetPulseZone.RightImage = null;
            this.resetPulseZone.Size = new System.Drawing.Size(103, 23);
            this.resetPulseZone.TabIndex = 23;
            this.resetPulseZone.Text = "Reset...";
            this.resetPulseZone.TextAlign = System.Drawing.StringAlignment.Center;
            this.resetPulseZone.TextLeftMargin = 2;
            this.resetPulseZone.TextRightMargin = 2;
            this.resetPulseZone.Click += new System.EventHandler(this.resetPulseZone_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(290, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(42, 13);
            this.label2.TabIndex = 21;
            this.label2.Text = "BPM to";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(290, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 13);
            this.label1.TabIndex = 20;
            this.label1.Text = "BPM from";
            // 
            // maxPulseBox
            // 
            this.maxPulseBox.AcceptsReturn = false;
            this.maxPulseBox.AcceptsTab = false;
            this.maxPulseBox.BackColor = System.Drawing.Color.White;
            this.maxPulseBox.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(123)))), ((int)(((byte)(114)))), ((int)(((byte)(108)))));
            this.maxPulseBox.ButtonImage = null;
            this.maxPulseBox.Location = new System.Drawing.Point(217, 45);
            this.maxPulseBox.MaxLength = 32767;
            this.maxPulseBox.Multiline = false;
            this.maxPulseBox.Name = "maxPulseBox";
            this.maxPulseBox.ReadOnly = false;
            this.maxPulseBox.ReadOnlyColor = System.Drawing.SystemColors.Control;
            this.maxPulseBox.ReadOnlyTextColor = System.Drawing.SystemColors.ControlLight;
            this.maxPulseBox.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.maxPulseBox.Size = new System.Drawing.Size(70, 20);
            this.maxPulseBox.TabIndex = 19;
            this.maxPulseBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            // 
            // minPulseBox
            // 
            this.minPulseBox.AcceptsReturn = false;
            this.minPulseBox.AcceptsTab = false;
            this.minPulseBox.BackColor = System.Drawing.Color.White;
            this.minPulseBox.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(123)))), ((int)(((byte)(114)))), ((int)(((byte)(108)))));
            this.minPulseBox.ButtonImage = null;
            this.minPulseBox.Location = new System.Drawing.Point(217, 19);
            this.minPulseBox.MaxLength = 32767;
            this.minPulseBox.Multiline = false;
            this.minPulseBox.Name = "minPulseBox";
            this.minPulseBox.ReadOnly = false;
            this.minPulseBox.ReadOnlyColor = System.Drawing.SystemColors.Control;
            this.minPulseBox.ReadOnlyTextColor = System.Drawing.SystemColors.ControlLight;
            this.minPulseBox.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.minPulseBox.Size = new System.Drawing.Size(70, 20);
            this.minPulseBox.TabIndex = 18;
            this.minPulseBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            // 
            // removePulse
            // 
            this.removePulse.BackColor = System.Drawing.Color.Transparent;
            this.removePulse.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(40)))), ((int)(((byte)(50)))), ((int)(((byte)(120)))));
            this.removePulse.CenterImage = null;
            this.removePulse.DialogResult = System.Windows.Forms.DialogResult.None;
            this.removePulse.HyperlinkStyle = false;
            this.removePulse.ImageMargin = 2;
            this.removePulse.LeftImage = null;
            this.removePulse.Location = new System.Drawing.Point(100, 65);
            this.removePulse.Name = "removePulse";
            this.removePulse.PushStyle = true;
            this.removePulse.RightImage = null;
            this.removePulse.Size = new System.Drawing.Size(111, 23);
            this.removePulse.TabIndex = 22;
            this.removePulse.Text = "Remove --->";
            this.removePulse.TextAlign = System.Drawing.StringAlignment.Center;
            this.removePulse.TextLeftMargin = 2;
            this.removePulse.TextRightMargin = 2;
            this.removePulse.Click += new System.EventHandler(this.removePulse_Click);
            // 
            // addPulse
            // 
            this.addPulse.BackColor = System.Drawing.Color.Transparent;
            this.addPulse.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(40)))), ((int)(((byte)(50)))), ((int)(((byte)(120)))));
            this.addPulse.CenterImage = null;
            this.addPulse.DialogResult = System.Windows.Forms.DialogResult.None;
            this.addPulse.HyperlinkStyle = false;
            this.addPulse.ImageMargin = 2;
            this.addPulse.LeftImage = null;
            this.addPulse.Location = new System.Drawing.Point(100, 19);
            this.addPulse.Name = "addPulse";
            this.addPulse.PushStyle = true;
            this.addPulse.RightImage = null;
            this.addPulse.Size = new System.Drawing.Size(111, 23);
            this.addPulse.TabIndex = 20;
            this.addPulse.Text = "<--- Add";
            this.addPulse.TextAlign = System.Drawing.StringAlignment.Center;
            this.addPulse.TextLeftMargin = 2;
            this.addPulse.TextRightMargin = 2;
            this.addPulse.Click += new System.EventHandler(this.addPulse_Click);
            // 
            // pulseBox
            // 
            this.pulseBox.FormattingEnabled = true;
            this.pulseBox.Location = new System.Drawing.Point(6, 19);
            this.pulseBox.Name = "pulseBox";
            this.pulseBox.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.pulseBox.Size = new System.Drawing.Size(88, 69);
            this.pulseBox.TabIndex = 21;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.resetPaceZone);
            this.groupBox5.Controls.Add(this.paceTypeBox);
            this.groupBox5.Controls.Add(this.paceLabelTo);
            this.groupBox5.Controls.Add(this.paceLabelFrom);
            this.groupBox5.Controls.Add(this.maxSpeedBox);
            this.groupBox5.Controls.Add(this.minSpeedBox);
            this.groupBox5.Controls.Add(this.removePace);
            this.groupBox5.Controls.Add(this.addPace);
            this.groupBox5.Controls.Add(this.speedBox);
            this.groupBox5.Location = new System.Drawing.Point(3, 479);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(398, 135);
            this.groupBox5.TabIndex = 22;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Pace zone";
            // 
            // resetPaceZone
            // 
            this.resetPaceZone.BackColor = System.Drawing.Color.Transparent;
            this.resetPaceZone.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(40)))), ((int)(((byte)(50)))), ((int)(((byte)(120)))));
            this.resetPaceZone.CenterImage = null;
            this.resetPaceZone.DialogResult = System.Windows.Forms.DialogResult.None;
            this.resetPaceZone.HyperlinkStyle = false;
            this.resetPaceZone.ImageMargin = 2;
            this.resetPaceZone.LeftImage = null;
            this.resetPaceZone.Location = new System.Drawing.Point(289, 104);
            this.resetPaceZone.Name = "resetPaceZone";
            this.resetPaceZone.PushStyle = true;
            this.resetPaceZone.RightImage = null;
            this.resetPaceZone.Size = new System.Drawing.Size(103, 23);
            this.resetPaceZone.TabIndex = 30;
            this.resetPaceZone.Text = "Reset...";
            this.resetPaceZone.TextAlign = System.Drawing.StringAlignment.Center;
            this.resetPaceZone.TextLeftMargin = 2;
            this.resetPaceZone.TextRightMargin = 2;
            this.resetPaceZone.Click += new System.EventHandler(this.resetPaceZone_Click);
            // 
            // paceTypeBox
            // 
            this.paceTypeBox.FormattingEnabled = true;
            this.paceTypeBox.Location = new System.Drawing.Point(218, 73);
            this.paceTypeBox.Name = "paceTypeBox";
            this.paceTypeBox.Size = new System.Drawing.Size(94, 21);
            this.paceTypeBox.TabIndex = 26;
            // 
            // paceLabelTo
            // 
            this.paceLabelTo.AutoSize = true;
            this.paceLabelTo.Location = new System.Drawing.Point(295, 50);
            this.paceLabelTo.Name = "paceLabelTo";
            this.paceLabelTo.Size = new System.Drawing.Size(20, 13);
            this.paceLabelTo.TabIndex = 21;
            this.paceLabelTo.Text = "To";
            // 
            // paceLabelFrom
            // 
            this.paceLabelFrom.AutoSize = true;
            this.paceLabelFrom.Location = new System.Drawing.Point(295, 24);
            this.paceLabelFrom.Name = "paceLabelFrom";
            this.paceLabelFrom.Size = new System.Drawing.Size(30, 13);
            this.paceLabelFrom.TabIndex = 20;
            this.paceLabelFrom.Text = "From";
            // 
            // maxSpeedBox
            // 
            this.maxSpeedBox.AcceptsReturn = false;
            this.maxSpeedBox.AcceptsTab = false;
            this.maxSpeedBox.BackColor = System.Drawing.Color.White;
            this.maxSpeedBox.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(123)))), ((int)(((byte)(114)))), ((int)(((byte)(108)))));
            this.maxSpeedBox.ButtonImage = null;
            this.maxSpeedBox.Location = new System.Drawing.Point(218, 47);
            this.maxSpeedBox.MaxLength = 32767;
            this.maxSpeedBox.Multiline = false;
            this.maxSpeedBox.Name = "maxSpeedBox";
            this.maxSpeedBox.ReadOnly = false;
            this.maxSpeedBox.ReadOnlyColor = System.Drawing.SystemColors.Control;
            this.maxSpeedBox.ReadOnlyTextColor = System.Drawing.SystemColors.ControlLight;
            this.maxSpeedBox.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.maxSpeedBox.Size = new System.Drawing.Size(70, 20);
            this.maxSpeedBox.TabIndex = 25;
            this.maxSpeedBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            // 
            // minSpeedBox
            // 
            this.minSpeedBox.AcceptsReturn = false;
            this.minSpeedBox.AcceptsTab = false;
            this.minSpeedBox.BackColor = System.Drawing.Color.White;
            this.minSpeedBox.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(123)))), ((int)(((byte)(114)))), ((int)(((byte)(108)))));
            this.minSpeedBox.ButtonImage = null;
            this.minSpeedBox.Location = new System.Drawing.Point(218, 21);
            this.minSpeedBox.MaxLength = 32767;
            this.minSpeedBox.Multiline = false;
            this.minSpeedBox.Name = "minSpeedBox";
            this.minSpeedBox.ReadOnly = false;
            this.minSpeedBox.ReadOnlyColor = System.Drawing.SystemColors.Control;
            this.minSpeedBox.ReadOnlyTextColor = System.Drawing.SystemColors.ControlLight;
            this.minSpeedBox.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.minSpeedBox.Size = new System.Drawing.Size(70, 20);
            this.minSpeedBox.TabIndex = 24;
            this.minSpeedBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            // 
            // removePace
            // 
            this.removePace.BackColor = System.Drawing.Color.Transparent;
            this.removePace.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(40)))), ((int)(((byte)(50)))), ((int)(((byte)(120)))));
            this.removePace.CenterImage = null;
            this.removePace.DialogResult = System.Windows.Forms.DialogResult.None;
            this.removePace.HyperlinkStyle = false;
            this.removePace.ImageMargin = 2;
            this.removePace.LeftImage = null;
            this.removePace.Location = new System.Drawing.Point(113, 104);
            this.removePace.Name = "removePace";
            this.removePace.PushStyle = true;
            this.removePace.RightImage = null;
            this.removePace.Size = new System.Drawing.Size(98, 23);
            this.removePace.TabIndex = 29;
            this.removePace.Text = "Remove --->";
            this.removePace.TextAlign = System.Drawing.StringAlignment.Center;
            this.removePace.TextLeftMargin = 2;
            this.removePace.TextRightMargin = 2;
            this.removePace.Click += new System.EventHandler(this.removePace_Click);
            // 
            // addPace
            // 
            this.addPace.BackColor = System.Drawing.Color.Transparent;
            this.addPace.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(40)))), ((int)(((byte)(50)))), ((int)(((byte)(120)))));
            this.addPace.CenterImage = null;
            this.addPace.DialogResult = System.Windows.Forms.DialogResult.None;
            this.addPace.HyperlinkStyle = false;
            this.addPace.ImageMargin = 2;
            this.addPace.LeftImage = null;
            this.addPace.Location = new System.Drawing.Point(113, 19);
            this.addPace.Name = "addPace";
            this.addPace.PushStyle = true;
            this.addPace.RightImage = null;
            this.addPace.Size = new System.Drawing.Size(98, 23);
            this.addPace.TabIndex = 27;
            this.addPace.Text = "<--- Add";
            this.addPace.TextAlign = System.Drawing.StringAlignment.Center;
            this.addPace.TextLeftMargin = 2;
            this.addPace.TextRightMargin = 2;
            this.addPace.Click += new System.EventHandler(this.addPace_Click);
            // 
            // speedBox
            // 
            this.speedBox.FormattingEnabled = true;
            this.speedBox.Location = new System.Drawing.Point(6, 19);
            this.speedBox.Name = "speedBox";
            this.speedBox.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.speedBox.Size = new System.Drawing.Size(101, 108);
            this.speedBox.TabIndex = 28;
            // 
            // ignoreManualBox
            // 
            this.ignoreManualBox.AutoSize = true;
            this.ignoreManualBox.Checked = true;
            this.ignoreManualBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ignoreManualBox.Location = new System.Drawing.Point(9, 32);
            this.ignoreManualBox.Name = "ignoreManualBox";
            this.ignoreManualBox.Size = new System.Drawing.Size(222, 17);
            this.ignoreManualBox.TabIndex = 23;
            this.ignoreManualBox.Text = "Ignore activities with manual entered data";
            this.ignoreManualBox.UseVisualStyleBackColor = true;
            this.ignoreManualBox.CheckedChanged += new System.EventHandler(this.ignoreManualBox_CheckedChanged);
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Location = new System.Drawing.Point(197, 8);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(138, 13);
            this.linkLabel1.TabIndex = 24;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "High Score plugin webpage";
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.highScoreControl1);
            this.groupBox6.Location = new System.Drawing.Point(3, 620);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(504, 45);
            this.groupBox6.TabIndex = 26;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Settings";
            // 
            // highScoreControl1
            // 
            this.highScoreControl1.AutoScroll = true;
            this.highScoreControl1.AutoSize = true;
            this.highScoreControl1.BackColor = System.Drawing.Color.Transparent;
            this.highScoreControl1.Location = new System.Drawing.Point(0,15);
            this.highScoreControl1.Name = "highScoreControl1";
            this.highScoreControl1.Size = new System.Drawing.Size(495, 25);
            this.highScoreControl1.TabIndex = 25;
            // 
            // HighScoreSettingPageControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.Controls.Add(this.linkLabel1);
            this.Controls.Add(this.ignoreManualBox);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.resetSettings);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox6);
            this.Name = "HighScoreSettingPageControl";
            this.Size = new System.Drawing.Size(524, 666);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox distanceBox;
        private ZoneFiveSoftware.Common.Visuals.Button removeDistance;
        private ZoneFiveSoftware.Common.Visuals.TextBox distanceInputBox;
        private System.Windows.Forms.ListBox timeBox;
        private ZoneFiveSoftware.Common.Visuals.Button addTime;
        private ZoneFiveSoftware.Common.Visuals.Button addDistance;
        private ZoneFiveSoftware.Common.Visuals.Button removeTime;
        private ZoneFiveSoftware.Common.Visuals.TextBox timeInputBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private ZoneFiveSoftware.Common.Visuals.Button removeElevation;
        private ZoneFiveSoftware.Common.Visuals.Button addElevation;
        private System.Windows.Forms.ListBox elevationBox;
        private ZoneFiveSoftware.Common.Visuals.TextBox elevationInputBox;
        private ZoneFiveSoftware.Common.Visuals.Button resetSettings;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private ZoneFiveSoftware.Common.Visuals.TextBox maxPulseBox;
        private ZoneFiveSoftware.Common.Visuals.TextBox minPulseBox;
        private ZoneFiveSoftware.Common.Visuals.Button removePulse;
        private ZoneFiveSoftware.Common.Visuals.Button addPulse;
        private System.Windows.Forms.ListBox pulseBox;
        private System.Windows.Forms.GroupBox groupBox5;
        private ZoneFiveSoftware.Common.Visuals.TextBox maxSpeedBox;
        private ZoneFiveSoftware.Common.Visuals.TextBox minSpeedBox;
        private ZoneFiveSoftware.Common.Visuals.Button removePace;
        private ZoneFiveSoftware.Common.Visuals.Button addPace;
        private System.Windows.Forms.ListBox speedBox;
        private System.Windows.Forms.Label paceLabelTo;
        private System.Windows.Forms.Label paceLabelFrom;
        private System.Windows.Forms.ComboBox paceTypeBox;
        private System.Windows.Forms.Label distanceLabel;
        private System.Windows.Forms.Label elevationLabel;
        private ZoneFiveSoftware.Common.Visuals.Button resetDistances;
        private ZoneFiveSoftware.Common.Visuals.Button resetTimes;
        private ZoneFiveSoftware.Common.Visuals.Button resetElevations;
        private ZoneFiveSoftware.Common.Visuals.Button resetPulseZone;
        private ZoneFiveSoftware.Common.Visuals.Button resetPaceZone;
        private System.Windows.Forms.CheckBox ignoreManualBox;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private HighScoreControl highScoreControl1;
        private System.Windows.Forms.GroupBox groupBox6;
    }
}
