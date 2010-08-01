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
    partial class UniqueRoutesSettingPageControl
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
            this.resetSettings = new ZoneFiveSoftware.Common.Visuals.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.hasDirectionBox = new System.Windows.Forms.CheckBox();
            this.percentageOff = new System.Windows.Forms.NumericUpDown();
            this.metricLabel = new System.Windows.Forms.Label();
            this.bandwidthBox = new ZoneFiveSoftware.Common.Visuals.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.endLabel = new System.Windows.Forms.Label();
            this.ignoreEndBox = new ZoneFiveSoftware.Common.Visuals.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.beginningLabel = new System.Windows.Forms.Label();
            this.ignoreBeginningBox = new ZoneFiveSoftware.Common.Visuals.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            ((System.ComponentModel.ISupportInitialize)(this.percentageOff)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // resetSettings
            // 
            this.resetSettings.Location = new System.Drawing.Point(3, 3);
            this.resetSettings.Name = "resetSettings";
            this.resetSettings.Size = new System.Drawing.Size(164, 23);
            this.resetSettings.TabIndex = 1;
            this.resetSettings.Text = "Reset all settings...";
            this.resetSettings.Click += new System.EventHandler(this.resetSettings_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(106, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Bandwidth:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(24, 41);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(142, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Allowed points outside band:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(52, 65);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(114, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Routes have direction:";
            // 
            // hasDirectionBox
            // 
            this.hasDirectionBox.AutoSize = true;
            this.hasDirectionBox.Location = new System.Drawing.Point(169, 65);
            this.hasDirectionBox.Name = "hasDirectionBox";
            this.hasDirectionBox.Size = new System.Drawing.Size(15, 14);
            this.hasDirectionBox.TabIndex = 5;
            this.hasDirectionBox.UseVisualStyleBackColor = true;
            // 
            // percentageOff
            // 
            this.percentageOff.Location = new System.Drawing.Point(169, 39);
            this.percentageOff.Name = "percentageOff";
            this.percentageOff.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.percentageOff.Size = new System.Drawing.Size(39, 20);
            this.percentageOff.TabIndex = 7;
            // 
            // metricLabel
            // 
            this.metricLabel.AutoSize = true;
            this.metricLabel.Location = new System.Drawing.Point(234, 16);
            this.metricLabel.Name = "metricLabel";
            this.metricLabel.Size = new System.Drawing.Size(38, 13);
            this.metricLabel.TabIndex = 9;
            this.metricLabel.Text = "meters";
            // 
            // bandwidthBox
            // 
            this.bandwidthBox.Location = new System.Drawing.Point(169, 13);
            this.bandwidthBox.Name = "bandwidthBox";
            this.bandwidthBox.Size = new System.Drawing.Size(59, 20);
            this.bandwidthBox.TabIndex = 8;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(214, 41);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(43, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "percent";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.endLabel);
            this.groupBox1.Controls.Add(this.ignoreEndBox);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.beginningLabel);
            this.groupBox1.Controls.Add(this.ignoreBeginningBox);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.metricLabel);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.bandwidthBox);
            this.groupBox1.Controls.Add(this.hasDirectionBox);
            this.groupBox1.Controls.Add(this.percentageOff);
            this.groupBox1.Location = new System.Drawing.Point(3, 32);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(327, 135);
            this.groupBox1.TabIndex = 11;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Settings";
            // 
            // endLabel
            // 
            this.endLabel.AutoSize = true;
            this.endLabel.Location = new System.Drawing.Point(234, 110);
            this.endLabel.Name = "endLabel";
            this.endLabel.Size = new System.Drawing.Size(38, 13);
            this.endLabel.TabIndex = 16;
            this.endLabel.Text = "meters";
            // 
            // ignoreEndBox
            // 
            this.ignoreEndBox.Location = new System.Drawing.Point(167, 107);
            this.ignoreEndBox.Name = "ignoreEndBox";
            this.ignoreEndBox.Size = new System.Drawing.Size(59, 20);
            this.ignoreEndBox.TabIndex = 15;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(66, 107);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(100, 13);
            this.label8.TabIndex = 14;
            this.label8.Text = "Ignore end of route:";
            // 
            // beginningLabel
            // 
            this.beginningLabel.AutoSize = true;
            this.beginningLabel.Location = new System.Drawing.Point(234, 87);
            this.beginningLabel.Name = "beginningLabel";
            this.beginningLabel.Size = new System.Drawing.Size(38, 13);
            this.beginningLabel.TabIndex = 13;
            this.beginningLabel.Text = "meters";
            // 
            // ignoreBeginningBox
            // 
            this.ignoreBeginningBox.Location = new System.Drawing.Point(167, 84);
            this.ignoreBeginningBox.Name = "ignoreBeginningBox";
            this.ignoreBeginningBox.Size = new System.Drawing.Size(59, 20);
            this.ignoreBeginningBox.TabIndex = 12;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(38, 87);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(128, 13);
            this.label5.TabIndex = 11;
            this.label5.Text = "Ignore beginning of route:";
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Location = new System.Drawing.Point(174, 8);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(156, 13);
            this.linkLabel1.TabIndex = 12;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "Unique Routes plugin webpage";
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // UniqueRoutesSettingPageControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.Controls.Add(this.linkLabel1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.resetSettings);
            this.Name = "UniqueRoutesSettingPageControl";
            this.Size = new System.Drawing.Size(337, 174);
            ((System.ComponentModel.ISupportInitialize)(this.percentageOff)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ZoneFiveSoftware.Common.Visuals.Button resetSettings;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox hasDirectionBox;
        private System.Windows.Forms.NumericUpDown percentageOff;
        private System.Windows.Forms.Label metricLabel;
        private ZoneFiveSoftware.Common.Visuals.TextBox bandwidthBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label beginningLabel;
        private ZoneFiveSoftware.Common.Visuals.TextBox ignoreBeginningBox;
        private System.Windows.Forms.Label endLabel;
        private ZoneFiveSoftware.Common.Visuals.TextBox ignoreEndBox;
        private System.Windows.Forms.Label label8;
    }
}
