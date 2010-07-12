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

namespace SportTracksPerformancePredictorPlugin.Source
{
    partial class TrainingView
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.trainingTab = new System.Windows.Forms.TabPage();
            this.trainingLabel = new System.Windows.Forms.Label();
            this.trainingGrid = new System.Windows.Forms.DataGridView();
            this.paceTempoTab = new System.Windows.Forms.TabPage();
            this.paceTempoGrid = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.paceTempoLabel = new System.Windows.Forms.Label();
            this.intervalTab = new System.Windows.Forms.TabPage();
            this.interval2Label = new System.Windows.Forms.Label();
            this.interval1Label = new System.Windows.Forms.Label();
            this.intervalGrid = new System.Windows.Forms.DataGridView();
            this.temperatureTab = new System.Windows.Forms.TabPage();
            this.label2 = new System.Windows.Forms.Label();
            this.temperatureLabel = new System.Windows.Forms.Label();
            this.temperatureGrid = new System.Windows.Forms.DataGridView();
            this.weightTab = new System.Windows.Forms.TabPage();
            this.weightLabel2 = new System.Windows.Forms.Label();
            this.weightLabel = new System.Windows.Forms.Label();
            this.weightGrid = new System.Windows.Forms.DataGridView();
            this.tabControl1.SuspendLayout();
            this.trainingTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trainingGrid)).BeginInit();
            this.paceTempoTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.paceTempoGrid)).BeginInit();
            this.intervalTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.intervalGrid)).BeginInit();
            this.temperatureTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.temperatureGrid)).BeginInit();
            this.weightTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.weightGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.trainingTab);
            this.tabControl1.Controls.Add(this.paceTempoTab);
            this.tabControl1.Controls.Add(this.intervalTab);
            this.tabControl1.Controls.Add(this.temperatureTab);
            this.tabControl1.Controls.Add(this.weightTab);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.HotTrack = true;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.Padding = new System.Drawing.Point(0, 0);
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(442, 137);
            this.tabControl1.TabIndex = 0;
            // 
            // trainingTab
            // 
            this.trainingTab.AutoScroll = true;
            this.trainingTab.Controls.Add(this.trainingLabel);
            this.trainingTab.Controls.Add(this.trainingGrid);
            this.trainingTab.Location = new System.Drawing.Point(4, 22);
            this.trainingTab.Name = "trainingTab";
            this.trainingTab.Padding = new System.Windows.Forms.Padding(3);
            this.trainingTab.Size = new System.Drawing.Size(434, 111);
            this.trainingTab.TabIndex = 0;
            this.trainingTab.Text = "Training";
            this.trainingTab.UseVisualStyleBackColor = true;
            // 
            // trainingLabel
            // 
            this.trainingLabel.AutoSize = true;
            this.trainingLabel.Location = new System.Drawing.Point(7, 7);
            this.trainingLabel.Name = "trainingLabel";
            this.trainingLabel.Size = new System.Drawing.Size(35, 13);
            this.trainingLabel.TabIndex = 1;
            this.trainingLabel.Text = "label1";
            // 
            // trainingGrid
            // 
            this.trainingGrid.AllowUserToAddRows = false;
            this.trainingGrid.AllowUserToDeleteRows = false;
            this.trainingGrid.AllowUserToResizeRows = false;
            this.trainingGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.trainingGrid.BackgroundColor = System.Drawing.SystemColors.Control;
            this.trainingGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.trainingGrid.Location = new System.Drawing.Point(0, 30);
            this.trainingGrid.Name = "trainingGrid";
            this.trainingGrid.RowHeadersVisible = false;
            this.trainingGrid.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.trainingGrid.Size = new System.Drawing.Size(430, 81);
            this.trainingGrid.TabIndex = 0;
            // 
            // paceTempoTab
            // 
            this.paceTempoTab.AutoScroll = true;
            this.paceTempoTab.Controls.Add(this.paceTempoGrid);
            this.paceTempoTab.Controls.Add(this.label1);
            this.paceTempoTab.Controls.Add(this.paceTempoLabel);
            this.paceTempoTab.Location = new System.Drawing.Point(4, 22);
            this.paceTempoTab.Name = "paceTempoTab";
            this.paceTempoTab.Padding = new System.Windows.Forms.Padding(3);
            this.paceTempoTab.Size = new System.Drawing.Size(434, 111);
            this.paceTempoTab.TabIndex = 1;
            this.paceTempoTab.Text = "Pace for tempo runs";
            this.paceTempoTab.UseVisualStyleBackColor = true;
            // 
            // paceTempoGrid
            // 
            this.paceTempoGrid.AllowUserToAddRows = false;
            this.paceTempoGrid.AllowUserToDeleteRows = false;
            this.paceTempoGrid.AllowUserToResizeRows = false;
            this.paceTempoGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.paceTempoGrid.BackgroundColor = System.Drawing.SystemColors.Control;
            this.paceTempoGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.paceTempoGrid.Location = new System.Drawing.Point(0, 30);
            this.paceTempoGrid.Name = "paceTempoGrid";
            this.paceTempoGrid.ReadOnly = true;
            this.paceTempoGrid.RowHeadersVisible = false;
            this.paceTempoGrid.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.paceTempoGrid.Size = new System.Drawing.Size(502, 51);
            this.paceTempoGrid.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(460, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "20 min run is at lactate threshold pace - pace of longer runs adjusted to maintai" +
                "n proper intensity.";
            // 
            // paceTempoLabel
            // 
            this.paceTempoLabel.AutoSize = true;
            this.paceTempoLabel.Location = new System.Drawing.Point(6, 3);
            this.paceTempoLabel.Name = "paceTempoLabel";
            this.paceTempoLabel.Size = new System.Drawing.Size(35, 13);
            this.paceTempoLabel.TabIndex = 0;
            this.paceTempoLabel.Text = "label1";
            // 
            // intervalTab
            // 
            this.intervalTab.AutoScroll = true;
            this.intervalTab.Controls.Add(this.interval2Label);
            this.intervalTab.Controls.Add(this.interval1Label);
            this.intervalTab.Controls.Add(this.intervalGrid);
            this.intervalTab.Location = new System.Drawing.Point(4, 22);
            this.intervalTab.Name = "intervalTab";
            this.intervalTab.Size = new System.Drawing.Size(434, 111);
            this.intervalTab.TabIndex = 2;
            this.intervalTab.Text = "Interval split times";
            this.intervalTab.UseVisualStyleBackColor = true;
            // 
            // interval2Label
            // 
            this.interval2Label.AutoSize = true;
            this.interval2Label.Location = new System.Drawing.Point(3, 14);
            this.interval2Label.Name = "interval2Label";
            this.interval2Label.Size = new System.Drawing.Size(148, 13);
            this.interval2Label.TabIndex = 5;
            this.interval2Label.Text = "Suggested short interval pace";
            // 
            // interval1Label
            // 
            this.interval1Label.AutoSize = true;
            this.interval1Label.Location = new System.Drawing.Point(3, 6);
            this.interval1Label.Name = "interval1Label";
            this.interval1Label.Size = new System.Drawing.Size(0, 13);
            this.interval1Label.TabIndex = 4;
            // 
            // intervalGrid
            // 
            this.intervalGrid.AllowUserToAddRows = false;
            this.intervalGrid.AllowUserToDeleteRows = false;
            this.intervalGrid.AllowUserToResizeRows = false;
            this.intervalGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.intervalGrid.BackgroundColor = System.Drawing.SystemColors.Control;
            this.intervalGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.intervalGrid.Location = new System.Drawing.Point(0, 30);
            this.intervalGrid.Name = "intervalGrid";
            this.intervalGrid.ReadOnly = true;
            this.intervalGrid.RowHeadersVisible = false;
            this.intervalGrid.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.intervalGrid.Size = new System.Drawing.Size(430, 81);
            this.intervalGrid.TabIndex = 3;
            // 
            // temperatureTab
            // 
            this.temperatureTab.AutoScroll = true;
            this.temperatureTab.Controls.Add(this.label2);
            this.temperatureTab.Controls.Add(this.temperatureLabel);
            this.temperatureTab.Controls.Add(this.temperatureGrid);
            this.temperatureTab.Location = new System.Drawing.Point(4, 22);
            this.temperatureTab.Name = "temperatureTab";
            this.temperatureTab.Size = new System.Drawing.Size(434, 111);
            this.temperatureTab.TabIndex = 3;
            this.temperatureTab.Text = "Temperature impact";
            this.temperatureTab.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(266, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Performance is not adversely affected at 16° C or lower";
            // 
            // temperatureLabel
            // 
            this.temperatureLabel.AutoSize = true;
            this.temperatureLabel.Location = new System.Drawing.Point(6, 3);
            this.temperatureLabel.Name = "temperatureLabel";
            this.temperatureLabel.Size = new System.Drawing.Size(89, 13);
            this.temperatureLabel.TabIndex = 3;
            this.temperatureLabel.Text = "temperatureLabel";
            // 
            // temperatureGrid
            // 
            this.temperatureGrid.AllowUserToAddRows = false;
            this.temperatureGrid.AllowUserToDeleteRows = false;
            this.temperatureGrid.AllowUserToResizeRows = false;
            this.temperatureGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.temperatureGrid.BackgroundColor = System.Drawing.SystemColors.Control;
            this.temperatureGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.temperatureGrid.Location = new System.Drawing.Point(0, 30);
            this.temperatureGrid.Name = "temperatureGrid";
            this.temperatureGrid.ReadOnly = true;
            this.temperatureGrid.RowHeadersVisible = false;
            this.temperatureGrid.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.temperatureGrid.Size = new System.Drawing.Size(430, 81);
            this.temperatureGrid.TabIndex = 2;
            // 
            // weightTab
            // 
            this.weightTab.AutoScroll = true;
            this.weightTab.Controls.Add(this.weightLabel2);
            this.weightTab.Controls.Add(this.weightLabel);
            this.weightTab.Controls.Add(this.weightGrid);
            this.weightTab.Location = new System.Drawing.Point(4, 22);
            this.weightTab.Name = "weightTab";
            this.weightTab.Size = new System.Drawing.Size(434, 111);
            this.weightTab.TabIndex = 4;
            this.weightTab.Text = "Weight impact";
            this.weightTab.UseVisualStyleBackColor = true;
            // 
            // weightLabel2
            // 
            this.weightLabel2.AutoSize = true;
            this.weightLabel2.Location = new System.Drawing.Point(6, 15);
            this.weightLabel2.Name = "weightLabel2";
            this.weightLabel2.Size = new System.Drawing.Size(259, 13);
            this.weightLabel2.TabIndex = 7;
            this.weightLabel2.Text = "Estimated times and paces are +/- 2 seconds per mile";
            // 
            // weightLabel
            // 
            this.weightLabel.AutoSize = true;
            this.weightLabel.Location = new System.Drawing.Point(6, 2);
            this.weightLabel.Name = "weightLabel";
            this.weightLabel.Size = new System.Drawing.Size(35, 13);
            this.weightLabel.TabIndex = 6;
            this.weightLabel.Text = "label4";
            // 
            // weightGrid
            // 
            this.weightGrid.AllowUserToAddRows = false;
            this.weightGrid.AllowUserToDeleteRows = false;
            this.weightGrid.AllowUserToResizeRows = false;
            this.weightGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.weightGrid.BackgroundColor = System.Drawing.SystemColors.Control;
            this.weightGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.weightGrid.Location = new System.Drawing.Point(0, 30);
            this.weightGrid.Margin = new System.Windows.Forms.Padding(0, 3, 3, 3);
            this.weightGrid.Name = "weightGrid";
            this.weightGrid.ReadOnly = true;
            this.weightGrid.RowHeadersVisible = false;
            this.weightGrid.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.weightGrid.Size = new System.Drawing.Size(435, 85);
            this.weightGrid.TabIndex = 5;
            // 
            // TrainingView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.AutoSize = true;
            this.Controls.Add(this.tabControl1);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "TrainingView";
            this.Size = new System.Drawing.Size(442, 137);
            this.tabControl1.ResumeLayout(false);
            this.trainingTab.ResumeLayout(false);
            this.trainingTab.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trainingGrid)).EndInit();
            this.paceTempoTab.ResumeLayout(false);
            this.paceTempoTab.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.paceTempoGrid)).EndInit();
            this.intervalTab.ResumeLayout(false);
            this.intervalTab.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.intervalGrid)).EndInit();
            this.temperatureTab.ResumeLayout(false);
            this.temperatureTab.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.temperatureGrid)).EndInit();
            this.weightTab.ResumeLayout(false);
            this.weightTab.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.weightGrid)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage trainingTab;
        private System.Windows.Forms.TabPage paceTempoTab;
        private System.Windows.Forms.TabPage intervalTab;
        private System.Windows.Forms.TabPage temperatureTab;
        private System.Windows.Forms.TabPage weightTab;
        private System.Windows.Forms.DataGridView trainingGrid;
        private System.Windows.Forms.Label trainingLabel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label paceTempoLabel;
        private System.Windows.Forms.DataGridView paceTempoGrid;
        private System.Windows.Forms.DataGridView intervalGrid;
        private System.Windows.Forms.Label temperatureLabel;
        private System.Windows.Forms.DataGridView temperatureGrid;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label weightLabel2;
        private System.Windows.Forms.Label weightLabel;
        private System.Windows.Forms.DataGridView weightGrid;
        private System.Windows.Forms.Label interval2Label;
        private System.Windows.Forms.Label interval1Label;

    }
}
