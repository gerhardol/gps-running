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

namespace SportTracksTRIMPPlugin.Source
{
    partial class TRIMPSettings
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.numberOfZones = new System.Windows.Forms.NumericUpDown();
            this.maxHRLabel = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.Zone = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.HeartRate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Factor = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.startZone = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.resetSettings = new ZoneFiveSoftware.Common.Visuals.Button();
            this.chartBase = new ZoneFiveSoftware.Common.Visuals.Chart.LineChart();
            this.useMaxHR = new System.Windows.Forms.RadioButton();
            this.useHRReserve = new System.Windows.Forms.RadioButton();
            this.label4 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.numberOfZones)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.startZone)).BeginInit();
            this.SuspendLayout();
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Location = new System.Drawing.Point(140, 8);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(119, 13);
            this.linkLabel1.TabIndex = 0;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "TRIMP plugin webpage";
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // numberOfZones
            // 
            this.numberOfZones.Location = new System.Drawing.Point(105, 27);
            this.numberOfZones.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numberOfZones.Name = "numberOfZones";
            this.numberOfZones.Size = new System.Drawing.Size(38, 20);
            this.numberOfZones.TabIndex = 4;
            this.numberOfZones.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.numberOfZones.ValueChanged += new System.EventHandler(this.numberOfZones_ValueChanged);
            // 
            // maxHRLabel
            // 
            this.maxHRLabel.AutoSize = true;
            this.maxHRLabel.Location = new System.Drawing.Point(3, 76);
            this.maxHRLabel.Name = "maxHRLabel";
            this.maxHRLabel.Size = new System.Drawing.Size(35, 13);
            this.maxHRLabel.TabIndex = 0;
            this.maxHRLabel.Text = "label1";
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToResizeColumns = false;
            this.dataGridView1.AllowUserToResizeRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Zone,
            this.HeartRate,
            this.Factor});
            this.dataGridView1.Location = new System.Drawing.Point(6, 92);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.RowHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView1.RowHeadersVisible = false;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dataGridView1.RowsDefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridView1.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.dataGridView1.ShowEditingIcon = false;
            this.dataGridView1.Size = new System.Drawing.Size(386, 133);
            this.dataGridView1.TabIndex = 3;
            this.dataGridView1.TabStop = false;
            // 
            // Zone
            // 
            this.Zone.Frozen = true;
            this.Zone.HeaderText = "Zone (%max HR)";
            this.Zone.Name = "Zone";
            this.Zone.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Zone.Width = 120;
            // 
            // HeartRate
            // 
            this.HeartRate.Frozen = true;
            this.HeartRate.HeaderText = "Heart rate (BPM)";
            this.HeartRate.Name = "HeartRate";
            this.HeartRate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.HeartRate.Width = 120;
            // 
            // Factor
            // 
            this.Factor.HeaderText = "Factor";
            this.Factor.Name = "Factor";
            this.Factor.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(90, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Number of zones:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(44, 55);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(58, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Start zone:";
            // 
            // startZone
            // 
            this.startZone.Location = new System.Drawing.Point(105, 53);
            this.startZone.Name = "startZone";
            this.startZone.Size = new System.Drawing.Size(38, 20);
            this.startZone.TabIndex = 7;
            this.startZone.Value = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.startZone.ValueChanged += new System.EventHandler(this.startZone_ValueChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(149, 55);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(15, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "%";
            // 
            // resetSettings
            // 
            this.resetSettings.Location = new System.Drawing.Point(3, 3);
            this.resetSettings.Name = "resetSettings";
            this.resetSettings.Size = new System.Drawing.Size(131, 23);
            this.resetSettings.TabIndex = 9;
            this.resetSettings.Text = "Reset all settings...";
            this.resetSettings.Click += new System.EventHandler(this.resetSettings_Click_1);
            // 
            // chartBase
            // 
            this.chartBase.BackColor = System.Drawing.Color.White;
            this.chartBase.Border = ZoneFiveSoftware.Common.Visuals.ControlBorder.Style.SmallRoundShadow;
            this.chartBase.Location = new System.Drawing.Point(6, 231);
            this.chartBase.Name = "chartBase";
            this.chartBase.Size = new System.Drawing.Size(386, 225);
            this.chartBase.TabIndex = 10;
            // 
            // useMaxHR
            // 
            this.useMaxHR.AutoSize = true;
            this.useMaxHR.Location = new System.Drawing.Point(191, 27);
            this.useMaxHR.Name = "useMaxHR";
            this.useMaxHR.Size = new System.Drawing.Size(93, 17);
            this.useMaxHR.TabIndex = 11;
            this.useMaxHR.TabStop = true;
            this.useMaxHR.Text = "Max heart rate";
            this.useMaxHR.UseVisualStyleBackColor = true;
            this.useMaxHR.CheckedChanged += new System.EventHandler(this.useMaxHR_CheckedChanged);
            // 
            // useHRReserve
            // 
            this.useHRReserve.AutoSize = true;
            this.useHRReserve.Location = new System.Drawing.Point(191, 50);
            this.useHRReserve.Name = "useHRReserve";
            this.useHRReserve.Size = new System.Drawing.Size(202, 17);
            this.useHRReserve.TabIndex = 12;
            this.useHRReserve.TabStop = true;
            this.useHRReserve.Text = "Heart rate reserve (max HR - rest HR)";
            this.useHRReserve.UseVisualStyleBackColor = true;
            this.useHRReserve.CheckedChanged += new System.EventHandler(this.useHRReserve_CheckedChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(156, 29);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(29, 13);
            this.label4.TabIndex = 13;
            this.label4.Text = "Use:";
            // 
            // TRIMPSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label4);
            this.Controls.Add(this.useHRReserve);
            this.Controls.Add(this.useMaxHR);
            this.Controls.Add(this.chartBase);
            this.Controls.Add(this.resetSettings);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.startZone);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.numberOfZones);
            this.Controls.Add(this.maxHRLabel);
            this.Controls.Add(this.linkLabel1);
            this.Name = "TRIMPSettings";
            this.Size = new System.Drawing.Size(395, 466);
            ((System.ComponentModel.ISupportInitialize)(this.numberOfZones)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.startZone)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.NumericUpDown numberOfZones;
        private System.Windows.Forms.Label maxHRLabel;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown startZone;
        private System.Windows.Forms.Label label3;
        private ZoneFiveSoftware.Common.Visuals.Button resetSettings;
        private ZoneFiveSoftware.Common.Visuals.Chart.LineChart chartBase;
        private System.Windows.Forms.DataGridViewTextBoxColumn Zone;
        private System.Windows.Forms.DataGridViewTextBoxColumn HeartRate;
        private System.Windows.Forms.DataGridViewTextBoxColumn Factor;
        private System.Windows.Forms.RadioButton useMaxHR;
        private System.Windows.Forms.RadioButton useHRReserve;
        private System.Windows.Forms.Label label4;
    }
}
