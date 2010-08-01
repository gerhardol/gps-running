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
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox5.SuspendLayout();
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
            this.removeDistance.Location = new System.Drawing.Point(100, 65);
            this.removeDistance.Name = "removeDistance";
            this.removeDistance.Size = new System.Drawing.Size(111, 23);
            this.removeDistance.TabIndex = 6;
            this.removeDistance.Text = "Remove --->";
            this.removeDistance.Click += new System.EventHandler(this.removeDistance_Click);
            // 
            // distanceInputBox
            // 
            this.distanceInputBox.Location = new System.Drawing.Point(217, 21);
            this.distanceInputBox.Name = "distanceInputBox";
            this.distanceInputBox.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.distanceInputBox.Size = new System.Drawing.Size(71, 20);
            this.distanceInputBox.TabIndex = 2;
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
            this.addTime.Location = new System.Drawing.Point(100, 19);
            this.addTime.Name = "addTime";
            this.addTime.Size = new System.Drawing.Size(111, 23);
            this.addTime.TabIndex = 9;
            this.addTime.Text = "<--- Add";
            this.addTime.Click += new System.EventHandler(this.addTime_Click);
            // 
            // addDistance
            // 
            this.addDistance.Location = new System.Drawing.Point(100, 19);
            this.addDistance.Name = "addDistance";
            this.addDistance.Size = new System.Drawing.Size(111, 23);
            this.addDistance.TabIndex = 3;
            this.addDistance.Text = "<--- Add";
            this.addDistance.Click += new System.EventHandler(this.addDistance_Click);
            // 
            // removeTime
            // 
            this.removeTime.Location = new System.Drawing.Point(100, 65);
            this.removeTime.Name = "removeTime";
            this.removeTime.Size = new System.Drawing.Size(111, 23);
            this.removeTime.TabIndex = 11;
            this.removeTime.Text = "Remove --->";
            this.removeTime.Click += new System.EventHandler(this.removeTime_Click);
            // 
            // timeInputBox
            // 
            this.timeInputBox.Location = new System.Drawing.Point(218, 19);
            this.timeInputBox.Name = "timeInputBox";
            this.timeInputBox.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.timeInputBox.Size = new System.Drawing.Size(70, 20);
            this.timeInputBox.TabIndex = 8;
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
            this.resetDistances.Location = new System.Drawing.Point(289, 65);
            this.resetDistances.Name = "resetDistances";
            this.resetDistances.Size = new System.Drawing.Size(103, 23);
            this.resetDistances.TabIndex = 7;
            this.resetDistances.Text = "Reset...";
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
            this.resetTimes.Location = new System.Drawing.Point(289, 65);
            this.resetTimes.Name = "resetTimes";
            this.resetTimes.Size = new System.Drawing.Size(103, 23);
            this.resetTimes.TabIndex = 12;
            this.resetTimes.Text = "Reset...";
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
            this.resetElevations.Location = new System.Drawing.Point(289, 65);
            this.resetElevations.Name = "resetElevations";
            this.resetElevations.Size = new System.Drawing.Size(103, 23);
            this.resetElevations.TabIndex = 17;
            this.resetElevations.Text = "Reset...";
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
            this.elevationInputBox.Location = new System.Drawing.Point(217, 21);
            this.elevationInputBox.Name = "elevationInputBox";
            this.elevationInputBox.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.elevationInputBox.Size = new System.Drawing.Size(70, 20);
            this.elevationInputBox.TabIndex = 13;
            // 
            // removeElevation
            // 
            this.removeElevation.Location = new System.Drawing.Point(100, 65);
            this.removeElevation.Name = "removeElevation";
            this.removeElevation.Size = new System.Drawing.Size(111, 23);
            this.removeElevation.TabIndex = 16;
            this.removeElevation.Text = "Remove --->";
            this.removeElevation.Click += new System.EventHandler(this.removeElevation_Click);
            // 
            // addElevation
            // 
            this.addElevation.Location = new System.Drawing.Point(100, 19);
            this.addElevation.Name = "addElevation";
            this.addElevation.Size = new System.Drawing.Size(111, 23);
            this.addElevation.TabIndex = 14;
            this.addElevation.Text = "<--- Add";
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
            this.resetSettings.Location = new System.Drawing.Point(3, 3);
            this.resetSettings.Name = "resetSettings";
            this.resetSettings.Size = new System.Drawing.Size(188, 23);
            this.resetSettings.TabIndex = 1;
            this.resetSettings.Text = "Reset all settings...";
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
            this.resetPulseZone.Location = new System.Drawing.Point(289, 65);
            this.resetPulseZone.Name = "resetPulseZone";
            this.resetPulseZone.Size = new System.Drawing.Size(103, 23);
            this.resetPulseZone.TabIndex = 23;
            this.resetPulseZone.Text = "Reset...";
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
            this.maxPulseBox.Location = new System.Drawing.Point(217, 45);
            this.maxPulseBox.Name = "maxPulseBox";
            this.maxPulseBox.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.maxPulseBox.Size = new System.Drawing.Size(70, 20);
            this.maxPulseBox.TabIndex = 19;
            // 
            // minPulseBox
            // 
            this.minPulseBox.Location = new System.Drawing.Point(217, 19);
            this.minPulseBox.Name = "minPulseBox";
            this.minPulseBox.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.minPulseBox.Size = new System.Drawing.Size(70, 20);
            this.minPulseBox.TabIndex = 18;
            // 
            // removePulse
            // 
            this.removePulse.Location = new System.Drawing.Point(100, 65);
            this.removePulse.Name = "removePulse";
            this.removePulse.Size = new System.Drawing.Size(111, 23);
            this.removePulse.TabIndex = 22;
            this.removePulse.Text = "Remove --->";
            this.removePulse.Click += new System.EventHandler(this.removePulse_Click);
            // 
            // addPulse
            // 
            this.addPulse.Location = new System.Drawing.Point(100, 19);
            this.addPulse.Name = "addPulse";
            this.addPulse.Size = new System.Drawing.Size(111, 23);
            this.addPulse.TabIndex = 20;
            this.addPulse.Text = "<--- Add";
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
            this.resetPaceZone.Location = new System.Drawing.Point(289, 104);
            this.resetPaceZone.Name = "resetPaceZone";
            this.resetPaceZone.Size = new System.Drawing.Size(103, 23);
            this.resetPaceZone.TabIndex = 30;
            this.resetPaceZone.Text = "Reset...";
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
            this.maxSpeedBox.Location = new System.Drawing.Point(218, 47);
            this.maxSpeedBox.Name = "maxSpeedBox";
            this.maxSpeedBox.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.maxSpeedBox.Size = new System.Drawing.Size(70, 20);
            this.maxSpeedBox.TabIndex = 25;
            // 
            // minSpeedBox
            // 
            this.minSpeedBox.Location = new System.Drawing.Point(218, 21);
            this.minSpeedBox.Name = "minSpeedBox";
            this.minSpeedBox.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.minSpeedBox.Size = new System.Drawing.Size(70, 20);
            this.minSpeedBox.TabIndex = 24;
            // 
            // removePace
            // 
            this.removePace.Location = new System.Drawing.Point(113, 104);
            this.removePace.Name = "removePace";
            this.removePace.Size = new System.Drawing.Size(98, 23);
            this.removePace.TabIndex = 29;
            this.removePace.Text = "Remove --->";
            this.removePace.Click += new System.EventHandler(this.removePace_Click);
            // 
            // addPace
            // 
            this.addPace.Location = new System.Drawing.Point(113, 19);
            this.addPace.Name = "addPace";
            this.addPace.Size = new System.Drawing.Size(98, 23);
            this.addPace.TabIndex = 27;
            this.addPace.Text = "<--- Add";
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
            this.Name = "HighScoreSettingPageControl";
            this.Size = new System.Drawing.Size(408, 618);
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
    }
}
