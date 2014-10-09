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
            this.bandwidthBox = new ZoneFiveSoftware.Common.Visuals.TextBox();
            this.labelPercentOutsideUnit = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.ignoreEndBox = new ZoneFiveSoftware.Common.Visuals.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.ignoreBeginningBox = new ZoneFiveSoftware.Common.Visuals.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.categoryLabel = new System.Windows.Forms.Label();
            this.boxCategory = new ZoneFiveSoftware.Common.Visuals.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.percentageOff)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
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
            this.resetSettings.Size = new System.Drawing.Size(164, 23);
            this.resetSettings.TabIndex = 1;
            this.resetSettings.Text = "Reset all settings...";
            this.resetSettings.TextAlign = System.Drawing.StringAlignment.Center;
            this.resetSettings.TextLeftMargin = 2;
            this.resetSettings.TextRightMargin = 2;
            this.resetSettings.Click += new System.EventHandler(this.resetSettings_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(174, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Bandwidth:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(92, 41);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(142, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Allowed points outside band:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(120, 65);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(114, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Routes have direction:";
            // 
            // hasDirectionBox
            // 
            this.hasDirectionBox.AutoSize = true;
            this.hasDirectionBox.Location = new System.Drawing.Point(237, 65);
            this.hasDirectionBox.Name = "hasDirectionBox";
            this.hasDirectionBox.Size = new System.Drawing.Size(15, 14);
            this.hasDirectionBox.TabIndex = 5;
            this.hasDirectionBox.UseVisualStyleBackColor = true;
            // 
            // percentageOff
            // 
            this.percentageOff.Location = new System.Drawing.Point(237, 39);
            this.percentageOff.Name = "percentageOff";
            this.percentageOff.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.percentageOff.Size = new System.Drawing.Size(39, 20);
            this.percentageOff.TabIndex = 7;
            // 
            // bandwidthBox
            // 
            this.bandwidthBox.AcceptsReturn = false;
            this.bandwidthBox.AcceptsTab = false;
            this.bandwidthBox.BackColor = System.Drawing.Color.White;
            this.bandwidthBox.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(123)))), ((int)(((byte)(114)))), ((int)(((byte)(108)))));
            this.bandwidthBox.ButtonImage = null;
            this.bandwidthBox.Location = new System.Drawing.Point(237, 13);
            this.bandwidthBox.MaxLength = 32767;
            this.bandwidthBox.Multiline = false;
            this.bandwidthBox.Name = "bandwidthBox";
            this.bandwidthBox.ReadOnly = false;
            this.bandwidthBox.ReadOnlyColor = System.Drawing.SystemColors.Control;
            this.bandwidthBox.ReadOnlyTextColor = System.Drawing.SystemColors.ControlLight;
            this.bandwidthBox.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.bandwidthBox.Size = new System.Drawing.Size(59, 20);
            this.bandwidthBox.TabIndex = 8;
            this.bandwidthBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            // 
            // labelPercentOutsideUnit
            // 
            this.labelPercentOutsideUnit.AutoSize = true;
            this.labelPercentOutsideUnit.Location = new System.Drawing.Point(280, 42);
            this.labelPercentOutsideUnit.Name = "labelPercentOutsideUnit";
            this.labelPercentOutsideUnit.Size = new System.Drawing.Size(15, 13);
            this.labelPercentOutsideUnit.TabIndex = 10;
            this.labelPercentOutsideUnit.Text = "%";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.ignoreEndBox);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.ignoreBeginningBox);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.labelPercentOutsideUnit);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.bandwidthBox);
            this.groupBox1.Controls.Add(this.hasDirectionBox);
            this.groupBox1.Controls.Add(this.percentageOff);
            this.groupBox1.Location = new System.Drawing.Point(3, 32);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(309, 135);
            this.groupBox1.TabIndex = 11;
            this.groupBox1.TabStop = false;
            // 
            // ignoreEndBox
            // 
            this.ignoreEndBox.AcceptsReturn = false;
            this.ignoreEndBox.AcceptsTab = false;
            this.ignoreEndBox.BackColor = System.Drawing.Color.White;
            this.ignoreEndBox.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(123)))), ((int)(((byte)(114)))), ((int)(((byte)(108)))));
            this.ignoreEndBox.ButtonImage = null;
            this.ignoreEndBox.Location = new System.Drawing.Point(237, 107);
            this.ignoreEndBox.MaxLength = 32767;
            this.ignoreEndBox.Multiline = false;
            this.ignoreEndBox.Name = "ignoreEndBox";
            this.ignoreEndBox.ReadOnly = false;
            this.ignoreEndBox.ReadOnlyColor = System.Drawing.SystemColors.Control;
            this.ignoreEndBox.ReadOnlyTextColor = System.Drawing.SystemColors.ControlLight;
            this.ignoreEndBox.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.ignoreEndBox.Size = new System.Drawing.Size(59, 20);
            this.ignoreEndBox.TabIndex = 15;
            this.ignoreEndBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(134, 110);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(100, 13);
            this.label8.TabIndex = 14;
            this.label8.Text = "Ignore end of route:";
            // 
            // ignoreBeginningBox
            // 
            this.ignoreBeginningBox.AcceptsReturn = false;
            this.ignoreBeginningBox.AcceptsTab = false;
            this.ignoreBeginningBox.BackColor = System.Drawing.Color.White;
            this.ignoreBeginningBox.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(123)))), ((int)(((byte)(114)))), ((int)(((byte)(108)))));
            this.ignoreBeginningBox.ButtonImage = null;
            this.ignoreBeginningBox.Location = new System.Drawing.Point(237, 84);
            this.ignoreBeginningBox.MaxLength = 32767;
            this.ignoreBeginningBox.Multiline = false;
            this.ignoreBeginningBox.Name = "ignoreBeginningBox";
            this.ignoreBeginningBox.ReadOnly = false;
            this.ignoreBeginningBox.ReadOnlyColor = System.Drawing.SystemColors.Control;
            this.ignoreBeginningBox.ReadOnlyTextColor = System.Drawing.SystemColors.ControlLight;
            this.ignoreBeginningBox.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.ignoreBeginningBox.Size = new System.Drawing.Size(59, 20);
            this.ignoreBeginningBox.TabIndex = 12;
            this.ignoreBeginningBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(106, 87);
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
            // categoryLabel
            // 
            this.categoryLabel.AutoSize = true;
            this.categoryLabel.Location = new System.Drawing.Point(6, 13);
            this.categoryLabel.Name = "categoryLabel";
            this.categoryLabel.Size = new System.Drawing.Size(58, 13);
            this.categoryLabel.TabIndex = 21;
            this.categoryLabel.Text = "<Category:";
            // 
            // boxCategory
            // 
            this.boxCategory.AcceptsReturn = false;
            this.boxCategory.AcceptsTab = false;
            this.boxCategory.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.boxCategory.BackColor = System.Drawing.Color.White;
            this.boxCategory.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(123)))), ((int)(((byte)(114)))), ((int)(((byte)(108)))));
            this.boxCategory.ButtonImage = null;
            this.boxCategory.Location = new System.Drawing.Point(65, 12);
            this.boxCategory.MaxLength = 32767;
            this.boxCategory.Multiline = false;
            this.boxCategory.Name = "boxCategory";
            this.boxCategory.ReadOnly = true;
            this.boxCategory.ReadOnlyColor = System.Drawing.SystemColors.Control;
            this.boxCategory.ReadOnlyTextColor = System.Drawing.SystemColors.ControlLight;
            this.boxCategory.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.boxCategory.Size = new System.Drawing.Size(244, 21);
            this.boxCategory.TabIndex = 22;
            this.boxCategory.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.boxCategory.ButtonClick += new System.EventHandler(this.boxCategory_ButtonClicked);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.boxCategory);
            this.groupBox2.Controls.Add(this.categoryLabel);
            this.groupBox2.Location = new System.Drawing.Point(3, 173);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(309, 39);
            this.groupBox2.TabIndex = 23;
            this.groupBox2.TabStop = false;
            // 
            // UniqueRoutesSettingPageControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.linkLabel1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.resetSettings);
            this.Name = "UniqueRoutesSettingPageControl";
            this.Size = new System.Drawing.Size(399, 251);
            ((System.ComponentModel.ISupportInitialize)(this.percentageOff)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
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
        private ZoneFiveSoftware.Common.Visuals.TextBox bandwidthBox;
        private System.Windows.Forms.Label labelPercentOutsideUnit;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.Label label5;
        private ZoneFiveSoftware.Common.Visuals.TextBox ignoreBeginningBox;
        private ZoneFiveSoftware.Common.Visuals.TextBox ignoreEndBox;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label categoryLabel;
        private ZoneFiveSoftware.Common.Visuals.TextBox boxCategory;
        private System.Windows.Forms.GroupBox groupBox2;
    }
}
