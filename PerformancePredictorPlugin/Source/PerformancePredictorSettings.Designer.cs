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
    partial class PerformancePredictorSettings
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
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.resetSettings = new ZoneFiveSoftware.Common.Visuals.Button();
            this.distancesGroupBox = new System.Windows.Forms.GroupBox();
            this.removeDistance = new ZoneFiveSoftware.Common.Visuals.Button();
            this.unitBox = new System.Windows.Forms.ComboBox();
            this.distanceBox = new ZoneFiveSoftware.Common.Visuals.TextBox();
            this.addDistance = new ZoneFiveSoftware.Common.Visuals.Button();
            this.distanceList = new System.Windows.Forms.ListBox();
            this.hsGroupBox = new System.Windows.Forms.GroupBox();
            this.hsPercentLabel2 = new System.Windows.Forms.Label();
            this.hsPercentUpDown = new System.Windows.Forms.NumericUpDown();
            this.hsPercentLabel1 = new System.Windows.Forms.Label();
            this.idealGroupBox = new System.Windows.Forms.GroupBox();
            this.bmiBox = new ZoneFiveSoftware.Common.Visuals.TextBox();
            this.bmiLabel = new System.Windows.Forms.Label();
            this.shoeBox = new ZoneFiveSoftware.Common.Visuals.TextBox();
            this.shoeLabel = new System.Windows.Forms.Label();
            this.modelGroupBox = new System.Windows.Forms.GroupBox();
            this.riegelFatigueFactorBox = new ZoneFiveSoftware.Common.Visuals.TextBox();
            this.riegelFatigueFactorLabel = new System.Windows.Forms.Label();
            this.elinderBreakEvenBox = new ZoneFiveSoftware.Common.Visuals.TextBox();
            this.elinderBreakEvenLabel = new System.Windows.Forms.Label();
            this.predictionGroupBox = new System.Windows.Forms.GroupBox();
            this.minPercentLabel2 = new System.Windows.Forms.Label();
            this.minPercentUpDown = new System.Windows.Forms.NumericUpDown();
            this.minPercentLabel1 = new System.Windows.Forms.Label();
            this.distancesGroupBox.SuspendLayout();
            this.hsGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.hsPercentUpDown)).BeginInit();
            this.idealGroupBox.SuspendLayout();
            this.modelGroupBox.SuspendLayout();
            this.predictionGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.minPercentUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Location = new System.Drawing.Point(3, 0);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(190, 13);
            this.linkLabel1.TabIndex = 0;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "Performance Predictor plugin webpage";
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
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
            this.resetSettings.Location = new System.Drawing.Point(6, 16);
            this.resetSettings.Name = "resetSettings";
            this.resetSettings.PushStyle = true;
            this.resetSettings.RightImage = null;
            this.resetSettings.Size = new System.Drawing.Size(187, 23);
            this.resetSettings.TabIndex = 1;
            this.resetSettings.Text = "<Reset all settings...";
            this.resetSettings.TextAlign = System.Drawing.StringAlignment.Center;
            this.resetSettings.TextLeftMargin = 2;
            this.resetSettings.TextRightMargin = 2;
            this.resetSettings.Click += new System.EventHandler(this.resetSettings_Click);
            // 
            // distancesGroupBox
            // 
            this.distancesGroupBox.Controls.Add(this.removeDistance);
            this.distancesGroupBox.Controls.Add(this.unitBox);
            this.distancesGroupBox.Controls.Add(this.distanceBox);
            this.distancesGroupBox.Controls.Add(this.addDistance);
            this.distancesGroupBox.Controls.Add(this.distanceList);
            this.distancesGroupBox.Location = new System.Drawing.Point(6, 45);
            this.distancesGroupBox.Name = "distancesGroupBox";
            this.distancesGroupBox.Size = new System.Drawing.Size(326, 161);
            this.distancesGroupBox.TabIndex = 2;
            this.distancesGroupBox.TabStop = false;
            this.distancesGroupBox.Text = "<Distances used in models";
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
            this.removeDistance.Location = new System.Drawing.Point(132, 130);
            this.removeDistance.Name = "removeDistance";
            this.removeDistance.PushStyle = true;
            this.removeDistance.RightImage = null;
            this.removeDistance.Size = new System.Drawing.Size(186, 23);
            this.removeDistance.TabIndex = 4;
            this.removeDistance.Text = "<Remove distance -->";
            this.removeDistance.TextAlign = System.Drawing.StringAlignment.Center;
            this.removeDistance.TextLeftMargin = 2;
            this.removeDistance.TextRightMargin = 2;
            this.removeDistance.Click += new System.EventHandler(this.removeDistance_Click);
            // 
            // unitBox
            // 
            this.unitBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.unitBox.FormattingEnabled = true;
            this.unitBox.Location = new System.Drawing.Point(220, 21);
            this.unitBox.Name = "unitBox";
            this.unitBox.Size = new System.Drawing.Size(98, 21);
            this.unitBox.TabIndex = 3;
            // 
            // distanceBox
            // 
            this.distanceBox.AcceptsReturn = false;
            this.distanceBox.AcceptsTab = false;
            this.distanceBox.BackColor = System.Drawing.Color.White;
            this.distanceBox.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(123)))), ((int)(((byte)(114)))), ((int)(((byte)(108)))));
            this.distanceBox.ButtonImage = null;
            this.distanceBox.Location = new System.Drawing.Point(132, 21);
            this.distanceBox.MaxLength = 32767;
            this.distanceBox.Multiline = false;
            this.distanceBox.Name = "distanceBox";
            this.distanceBox.ReadOnly = false;
            this.distanceBox.ReadOnlyColor = System.Drawing.SystemColors.Control;
            this.distanceBox.ReadOnlyTextColor = System.Drawing.SystemColors.ControlLight;
            this.distanceBox.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.distanceBox.Size = new System.Drawing.Size(82, 20);
            this.distanceBox.TabIndex = 2;
            this.distanceBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
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
            this.addDistance.Location = new System.Drawing.Point(132, 47);
            this.addDistance.Name = "addDistance";
            this.addDistance.PushStyle = true;
            this.addDistance.RightImage = null;
            this.addDistance.Size = new System.Drawing.Size(186, 23);
            this.addDistance.TabIndex = 1;
            this.addDistance.Text = "<<-- Add distance";
            this.addDistance.TextAlign = System.Drawing.StringAlignment.Center;
            this.addDistance.TextLeftMargin = 2;
            this.addDistance.TextRightMargin = 2;
            this.addDistance.Click += new System.EventHandler(this.addDistance_Click);
            // 
            // distanceList
            // 
            this.distanceList.FormattingEnabled = true;
            this.distanceList.Location = new System.Drawing.Point(6, 19);
            this.distanceList.Name = "distanceList";
            this.distanceList.Size = new System.Drawing.Size(120, 134);
            this.distanceList.TabIndex = 0;
            // 
            // predictionGroupBox
            // 
            this.predictionGroupBox.Controls.Add(this.minPercentLabel2);
            this.predictionGroupBox.Controls.Add(this.minPercentUpDown);
            this.predictionGroupBox.Controls.Add(this.minPercentLabel1);
            this.predictionGroupBox.Location = new System.Drawing.Point(6, 212);
            this.predictionGroupBox.Name = "predictionGroupBox";
            this.predictionGroupBox.Size = new System.Drawing.Size(326, 48);
            this.predictionGroupBox.TabIndex = 4;
            this.predictionGroupBox.TabStop = false;
            // 
            // minPercentLabel1
            // 
            this.minPercentLabel1.AutoSize = true;
            this.minPercentLabel1.Location = new System.Drawing.Point(6, 21);
            this.minPercentLabel1.Name = "minPercentLabel1";
            this.minPercentLabel1.Size = new System.Drawing.Size(54, 13);
            this.minPercentLabel1.TabIndex = 0;
            this.minPercentLabel1.Text = "<Minimum";
            // 
            // minPercentUpDown
            // 
            this.minPercentUpDown.Location = new System.Drawing.Point(66, 19);
            this.minPercentUpDown.Maximum = new decimal(new int[] {
            99,
            0,
            0,
            0});
            this.minPercentUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.minPercentUpDown.Name = "minPercentUpDown";
            this.minPercentUpDown.Size = new System.Drawing.Size(36, 20);
            this.minPercentUpDown.TabIndex = 1;
            this.minPercentUpDown.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.minPercentUpDown.Value = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.minPercentUpDown.ValueChanged += new System.EventHandler(this.minPercentUpDown_ValueChanged);
            // 
            // minPercentLabel2
            // 
            this.minPercentLabel2.AutoSize = true;
            this.minPercentLabel2.Location = new System.Drawing.Point(108, 21);
            this.minPercentLabel2.Name = "minPercentLabel2";
            this.minPercentLabel2.Size = new System.Drawing.Size(145, 13);
            this.minPercentLabel2.TabIndex = 2;
            this.minPercentLabel2.Text = "<% of distance to predict time";
            // 
            // hsGroupBox
            // 
            this.hsGroupBox.Controls.Add(this.hsPercentLabel2);
            this.hsGroupBox.Controls.Add(this.hsPercentUpDown);
            this.hsGroupBox.Controls.Add(this.hsPercentLabel1);
            this.hsGroupBox.Location = new System.Drawing.Point(6, 266);
            this.hsGroupBox.Name = "hsGroupBox";
            this.hsGroupBox.Size = new System.Drawing.Size(326, 48);
            this.hsGroupBox.TabIndex = 3;
            this.hsGroupBox.TabStop = false;
            this.hsGroupBox.Text = "<High Score plugin integration";
            // 
            // hsPercentLabel2
            // 
            this.hsPercentLabel2.AutoSize = true;
            this.hsPercentLabel2.Location = new System.Drawing.Point(76, 21);
            this.hsPercentLabel2.Name = "hsPercentLabel2";
            this.hsPercentLabel2.Size = new System.Drawing.Size(145, 13);
            this.hsPercentLabel2.TabIndex = 2;
            this.hsPercentLabel2.Text = "<% of distance to predict time";
            // 
            // hsPercentUpDown
            // 
            this.hsPercentUpDown.Location = new System.Drawing.Point(38, 19);
            this.hsPercentUpDown.Maximum = new decimal(new int[] {
            99,
            0,
            0,
            0});
            this.hsPercentUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.hsPercentUpDown.Name = "hsPercentUpDown";
            this.hsPercentUpDown.Size = new System.Drawing.Size(36, 20);
            this.hsPercentUpDown.TabIndex = 1;
            this.hsPercentUpDown.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.hsPercentUpDown.Value = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.hsPercentUpDown.ValueChanged += new System.EventHandler(this.hsPercentUpDown_ValueChanged);
            // 
            // hsPercentLabel1
            // 
            this.hsPercentLabel1.AutoSize = true;
            this.hsPercentLabel1.Location = new System.Drawing.Point(6, 21);
            this.hsPercentLabel1.Name = "hsPercentLabel1";
            this.hsPercentLabel1.Size = new System.Drawing.Size(32, 13);
            this.hsPercentLabel1.TabIndex = 0;
            this.hsPercentLabel1.Text = "<Use";
            // 
            // idealGroupBox
            // 
            this.idealGroupBox.Controls.Add(this.bmiBox);
            this.idealGroupBox.Controls.Add(this.bmiLabel);
            this.idealGroupBox.Controls.Add(this.shoeBox);
            this.idealGroupBox.Controls.Add(this.shoeLabel);
            this.idealGroupBox.Location = new System.Drawing.Point(3, 396);
            this.idealGroupBox.Name = "idealGroupBox";
            this.idealGroupBox.Size = new System.Drawing.Size(326, 71);
            this.idealGroupBox.TabIndex = 4;
            this.idealGroupBox.TabStop = false;
            this.idealGroupBox.Text = "<ideal";
            // 
            // bmiBox
            // 
            this.bmiBox.AcceptsReturn = false;
            this.bmiBox.AcceptsTab = false;
            this.bmiBox.BackColor = System.Drawing.Color.White;
            this.bmiBox.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(123)))), ((int)(((byte)(114)))), ((int)(((byte)(108)))));
            this.bmiBox.ButtonImage = null;
            this.bmiBox.Location = new System.Drawing.Point(54, 45);
            this.bmiBox.MaxLength = 32767;
            this.bmiBox.Multiline = false;
            this.bmiBox.Name = "bmiBox";
            this.bmiBox.ReadOnly = false;
            this.bmiBox.ReadOnlyColor = System.Drawing.SystemColors.Control;
            this.bmiBox.ReadOnlyTextColor = System.Drawing.SystemColors.ControlLight;
            this.bmiBox.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.bmiBox.Size = new System.Drawing.Size(82, 20);
            this.bmiBox.TabIndex = 5;
            this.bmiBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            // 
            // bmiLabel
            // 
            this.bmiLabel.AutoSize = true;
            this.bmiLabel.Location = new System.Drawing.Point(6, 48);
            this.bmiLabel.Name = "bmiLabel";
            this.bmiLabel.Size = new System.Drawing.Size(32, 13);
            this.bmiLabel.TabIndex = 4;
            this.bmiLabel.Text = "<BMI";
            // 
            // shoeBox
            // 
            this.shoeBox.AcceptsReturn = false;
            this.shoeBox.AcceptsTab = false;
            this.shoeBox.BackColor = System.Drawing.Color.White;
            this.shoeBox.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(123)))), ((int)(((byte)(114)))), ((int)(((byte)(108)))));
            this.shoeBox.ButtonImage = null;
            this.shoeBox.Location = new System.Drawing.Point(54, 19);
            this.shoeBox.MaxLength = 32767;
            this.shoeBox.Multiline = false;
            this.shoeBox.Name = "shoeBox";
            this.shoeBox.ReadOnly = false;
            this.shoeBox.ReadOnlyColor = System.Drawing.SystemColors.Control;
            this.shoeBox.ReadOnlyTextColor = System.Drawing.SystemColors.ControlLight;
            this.shoeBox.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.shoeBox.Size = new System.Drawing.Size(82, 20);
            this.shoeBox.TabIndex = 3;
            this.shoeBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            // 
            // shoeLabel
            // 
            this.shoeLabel.AutoSize = true;
            this.shoeLabel.Location = new System.Drawing.Point(6, 21);
            this.shoeLabel.Name = "shoeLabel";
            this.shoeLabel.Size = new System.Drawing.Size(38, 13);
            this.shoeLabel.TabIndex = 0;
            this.shoeLabel.Text = "<Shoe";
            // 
            // modelGroupBox
            // 
            this.modelGroupBox.Controls.Add(this.riegelFatigueFactorBox);
            this.modelGroupBox.Controls.Add(this.riegelFatigueFactorLabel);
            this.modelGroupBox.Controls.Add(this.elinderBreakEvenBox);
            this.modelGroupBox.Controls.Add(this.elinderBreakEvenLabel);
            this.modelGroupBox.Location = new System.Drawing.Point(3, 320);
            this.modelGroupBox.Name = "modelGroupBox";
            this.modelGroupBox.Size = new System.Drawing.Size(326, 71);
            this.modelGroupBox.TabIndex = 6;
            this.modelGroupBox.TabStop = false;
            this.modelGroupBox.Text = "<prediction model";
            // 
            // riegelFatigueFactorBox
            // 
            this.riegelFatigueFactorBox.AcceptsReturn = false;
            this.riegelFatigueFactorBox.AcceptsTab = false;
            this.riegelFatigueFactorBox.BackColor = System.Drawing.Color.White;
            this.riegelFatigueFactorBox.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(123)))), ((int)(((byte)(114)))), ((int)(((byte)(108)))));
            this.riegelFatigueFactorBox.ButtonImage = null;
            this.riegelFatigueFactorBox.Location = new System.Drawing.Point(132, 45);
            this.riegelFatigueFactorBox.MaxLength = 32767;
            this.riegelFatigueFactorBox.Multiline = false;
            this.riegelFatigueFactorBox.Name = "riegelFatigueFactorBox";
            this.riegelFatigueFactorBox.ReadOnly = false;
            this.riegelFatigueFactorBox.ReadOnlyColor = System.Drawing.SystemColors.Control;
            this.riegelFatigueFactorBox.ReadOnlyTextColor = System.Drawing.SystemColors.ControlLight;
            this.riegelFatigueFactorBox.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.riegelFatigueFactorBox.Size = new System.Drawing.Size(82, 20);
            this.riegelFatigueFactorBox.TabIndex = 5;
            this.riegelFatigueFactorBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            // 
            // riegelFatigueFactorLabel
            // 
            this.riegelFatigueFactorLabel.AutoSize = true;
            this.riegelFatigueFactorLabel.Location = new System.Drawing.Point(6, 48);
            this.riegelFatigueFactorLabel.Name = "riegelFatigueFactorLabel";
            this.riegelFatigueFactorLabel.Size = new System.Drawing.Size(111, 13);
            this.riegelFatigueFactorLabel.TabIndex = 4;
            this.riegelFatigueFactorLabel.Text = "<Riegel FatigueFactor";
            // 
            // elinderBreakEvenBox
            // 
            this.elinderBreakEvenBox.AcceptsReturn = false;
            this.elinderBreakEvenBox.AcceptsTab = false;
            this.elinderBreakEvenBox.BackColor = System.Drawing.Color.White;
            this.elinderBreakEvenBox.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(123)))), ((int)(((byte)(114)))), ((int)(((byte)(108)))));
            this.elinderBreakEvenBox.ButtonImage = null;
            this.elinderBreakEvenBox.Location = new System.Drawing.Point(132, 19);
            this.elinderBreakEvenBox.MaxLength = 32767;
            this.elinderBreakEvenBox.Multiline = false;
            this.elinderBreakEvenBox.Name = "elinderBreakEvenBox";
            this.elinderBreakEvenBox.ReadOnly = false;
            this.elinderBreakEvenBox.ReadOnlyColor = System.Drawing.SystemColors.Control;
            this.elinderBreakEvenBox.ReadOnlyTextColor = System.Drawing.SystemColors.ControlLight;
            this.elinderBreakEvenBox.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.elinderBreakEvenBox.Size = new System.Drawing.Size(82, 20);
            this.elinderBreakEvenBox.TabIndex = 3;
            this.elinderBreakEvenBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            // 
            // elinderBreakEvenLabel
            // 
            this.elinderBreakEvenLabel.AutoSize = true;
            this.elinderBreakEvenLabel.Location = new System.Drawing.Point(6, 21);
            this.elinderBreakEvenLabel.Name = "elinderBreakEvenLabel";
            this.elinderBreakEvenLabel.Size = new System.Drawing.Size(101, 13);
            this.elinderBreakEvenLabel.TabIndex = 0;
            this.elinderBreakEvenLabel.Text = "<Elinder BreakEven";
            // 
            // PerformancePredictorSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.predictionGroupBox);
            this.Controls.Add(this.modelGroupBox);
            this.Controls.Add(this.idealGroupBox);
            this.Controls.Add(this.hsGroupBox);
            this.Controls.Add(this.distancesGroupBox);
            this.Controls.Add(this.resetSettings);
            this.Controls.Add(this.linkLabel1);
            this.Name = "PerformancePredictorSettings";
            this.Size = new System.Drawing.Size(342, 470);
            this.distancesGroupBox.ResumeLayout(false);
            this.hsGroupBox.ResumeLayout(false);
            this.hsGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.hsPercentUpDown)).EndInit();
            this.idealGroupBox.ResumeLayout(false);
            this.idealGroupBox.PerformLayout();
            this.modelGroupBox.ResumeLayout(false);
            this.modelGroupBox.PerformLayout();
            this.predictionGroupBox.ResumeLayout(false);
            this.predictionGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.minPercentUpDown)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.LinkLabel linkLabel1;
        private ZoneFiveSoftware.Common.Visuals.Button resetSettings;
        private System.Windows.Forms.GroupBox distancesGroupBox;
        private ZoneFiveSoftware.Common.Visuals.Button removeDistance;
        private System.Windows.Forms.ComboBox unitBox;
        private ZoneFiveSoftware.Common.Visuals.TextBox distanceBox;
        private ZoneFiveSoftware.Common.Visuals.Button addDistance;
        private System.Windows.Forms.ListBox distanceList;
        private System.Windows.Forms.GroupBox hsGroupBox;
        private System.Windows.Forms.Label hsPercentLabel1;
        private System.Windows.Forms.NumericUpDown hsPercentUpDown;
        private System.Windows.Forms.Label hsPercentLabel2;
        private System.Windows.Forms.GroupBox idealGroupBox;
        private ZoneFiveSoftware.Common.Visuals.TextBox bmiBox;
        private System.Windows.Forms.Label bmiLabel;
        private ZoneFiveSoftware.Common.Visuals.TextBox shoeBox;
        private System.Windows.Forms.Label shoeLabel;
        private System.Windows.Forms.GroupBox modelGroupBox;
        private ZoneFiveSoftware.Common.Visuals.TextBox riegelFatigueFactorBox;
        private System.Windows.Forms.Label riegelFatigueFactorLabel;
        private ZoneFiveSoftware.Common.Visuals.TextBox elinderBreakEvenBox;
        private System.Windows.Forms.Label elinderBreakEvenLabel;
        private System.Windows.Forms.GroupBox predictionGroupBox;
        private System.Windows.Forms.Label minPercentLabel2;
        private System.Windows.Forms.NumericUpDown minPercentUpDown;
        private System.Windows.Forms.Label minPercentLabel1;
    }
}
