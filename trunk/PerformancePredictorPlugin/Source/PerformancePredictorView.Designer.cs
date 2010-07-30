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
    partial class PerformancePredictorView
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
            this.daveCameron = new System.Windows.Forms.RadioButton();
            this.reigel = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.resultBox = new System.Windows.Forms.GroupBox();
            this.table = new System.Windows.Forms.RadioButton();
            this.chartButton = new System.Windows.Forms.RadioButton();
            this.dataGrid = new System.Windows.Forms.DataGridView();
            this.chart = new ZoneFiveSoftware.Common.Visuals.Chart.LineChart();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.training = new System.Windows.Forms.RadioButton();
            this.timePrediction = new System.Windows.Forms.RadioButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.speed = new System.Windows.Forms.RadioButton();
            this.pace = new System.Windows.Forms.RadioButton();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.groupBox1.SuspendLayout();
            this.resultBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // daveCameron
            // 
            this.daveCameron.AutoSize = true;
            this.daveCameron.Location = new System.Drawing.Point(6, 19);
            this.daveCameron.Name = "daveCameron";
            this.daveCameron.Size = new System.Drawing.Size(96, 17);
            this.daveCameron.TabIndex = 3;
            this.daveCameron.TabStop = true;
            this.daveCameron.Text = "Dave Cameron";
            this.daveCameron.UseVisualStyleBackColor = true;
            this.daveCameron.CheckedChanged += new System.EventHandler(this.daveCameron_CheckedChanged);
            // 
            // reigel
            // 
            this.reigel.AutoSize = true;
            this.reigel.Location = new System.Drawing.Point(6, 42);
            this.reigel.Name = "reigel";
            this.reigel.Size = new System.Drawing.Size(80, 17);
            this.reigel.TabIndex = 4;
            this.reigel.TabStop = true;
            this.reigel.Text = "Pete Riegel";
            this.reigel.UseVisualStyleBackColor = true;
            this.reigel.CheckedChanged += new System.EventHandler(this.reigel_CheckedChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.daveCameron);
            this.groupBox1.Controls.Add(this.reigel);
            this.groupBox1.Location = new System.Drawing.Point(4, 76);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(138, 68);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Prediction model";
            // 
            // resultBox
            // 
            this.resultBox.Controls.Add(this.table);
            this.resultBox.Controls.Add(this.chartButton);
            this.resultBox.Location = new System.Drawing.Point(4, 225);
            this.resultBox.Name = "resultBox";
            this.resultBox.Size = new System.Drawing.Size(138, 67);
            this.resultBox.TabIndex = 5;
            this.resultBox.TabStop = false;
            this.resultBox.Text = "Prediction results";
            // 
            // table
            // 
            this.table.AutoSize = true;
            this.table.Location = new System.Drawing.Point(6, 42);
            this.table.Name = "table";
            this.table.Size = new System.Drawing.Size(85, 17);
            this.table.TabIndex = 6;
            this.table.TabStop = true;
            this.table.Text = "View in table";
            this.table.UseVisualStyleBackColor = true;
            this.table.CheckedChanged += new System.EventHandler(this.table_CheckedChanged);
            // 
            // chartButton
            // 
            this.chartButton.AutoSize = true;
            this.chartButton.Location = new System.Drawing.Point(6, 19);
            this.chartButton.Name = "chartButton";
            this.chartButton.Size = new System.Drawing.Size(86, 17);
            this.chartButton.TabIndex = 0;
            this.chartButton.TabStop = true;
            this.chartButton.Text = "View in chart";
            this.chartButton.UseVisualStyleBackColor = true;
            this.chartButton.CheckedChanged += new System.EventHandler(this.chartButton_CheckedChanged);
            // 
            // dataGrid
            // 
            this.dataGrid.AllowUserToAddRows = false;
            this.dataGrid.AllowUserToDeleteRows = false;
            this.dataGrid.AllowUserToResizeRows = false;
            this.dataGrid.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGrid.Location = new System.Drawing.Point(0, 0);
            this.dataGrid.Name = "dataGrid";
            this.dataGrid.ReadOnly = true;
            this.dataGrid.RowHeadersVisible = false;
            this.dataGrid.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dataGrid.Size = new System.Drawing.Size(190, 298);
            this.dataGrid.TabIndex = 7;
            // 
            // chart
            // 
            this.chart.AutoSize = true;
            this.chart.BackColor = System.Drawing.Color.White;
            this.chart.Border = ZoneFiveSoftware.Common.Visuals.ControlBorder.Style.SmallRoundShadow;
            this.chart.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chart.Location = new System.Drawing.Point(0, 0);
            this.chart.Name = "chart";
            this.chart.Padding = new System.Windows.Forms.Padding(5);
            this.chart.Size = new System.Drawing.Size(190, 298);
            this.chart.TabIndex = 8;
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(148, 4);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(184, 23);
            this.progressBar.TabIndex = 9;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.training);
            this.groupBox3.Controls.Add(this.timePrediction);
            this.groupBox3.Location = new System.Drawing.Point(4, 4);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(138, 66);
            this.groupBox3.TabIndex = 10;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Settings";
            // 
            // training
            // 
            this.training.AutoSize = true;
            this.training.Location = new System.Drawing.Point(7, 43);
            this.training.Name = "training";
            this.training.Size = new System.Drawing.Size(63, 17);
            this.training.TabIndex = 1;
            this.training.TabStop = true;
            this.training.Text = "Training";
            this.training.UseVisualStyleBackColor = true;
            this.training.CheckedChanged += new System.EventHandler(this.training_CheckedChanged);
            // 
            // timePrediction
            // 
            this.timePrediction.AutoSize = true;
            this.timePrediction.Location = new System.Drawing.Point(7, 20);
            this.timePrediction.Name = "timePrediction";
            this.timePrediction.Size = new System.Drawing.Size(97, 17);
            this.timePrediction.TabIndex = 0;
            this.timePrediction.TabStop = true;
            this.timePrediction.Text = "Time prediction";
            this.timePrediction.UseVisualStyleBackColor = true;
            this.timePrediction.CheckedChanged += new System.EventHandler(this.timePrediction_CheckedChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.speed);
            this.groupBox2.Controls.Add(this.pace);
            this.groupBox2.Location = new System.Drawing.Point(4, 150);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(138, 69);
            this.groupBox2.TabIndex = 11;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Velocity";
            // 
            // speed
            // 
            this.speed.AutoSize = true;
            this.speed.Location = new System.Drawing.Point(7, 44);
            this.speed.Name = "speed";
            this.speed.Size = new System.Drawing.Size(84, 17);
            this.speed.TabIndex = 1;
            this.speed.TabStop = true;
            this.speed.Text = "Show speed";
            this.speed.UseVisualStyleBackColor = true;
            this.speed.CheckedChanged += new System.EventHandler(this.speed_CheckedChanged);
            // 
            // pace
            // 
            this.pace.AutoSize = true;
            this.pace.Location = new System.Drawing.Point(7, 20);
            this.pace.Name = "pace";
            this.pace.Size = new System.Drawing.Size(79, 17);
            this.pace.TabIndex = 0;
            this.pace.TabStop = true;
            this.pace.Text = "Show pace";
            this.pace.UseVisualStyleBackColor = true;
            this.pace.CheckedChanged += new System.EventHandler(this.pace_CheckedChanged);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.groupBox2);
            this.splitContainer1.Panel1.Controls.Add(this.groupBox3);
            this.splitContainer1.Panel1.Controls.Add(this.progressBar);
            this.splitContainer1.Panel1.Controls.Add(this.resultBox);
            this.splitContainer1.Panel1.Controls.Add(this.groupBox1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.chart);
            this.splitContainer1.Panel2.Controls.Add(this.dataGrid);
            this.splitContainer1.Size = new System.Drawing.Size(336, 298);
            this.splitContainer1.SplitterDistance = 145;
            this.splitContainer1.SplitterWidth = 1;
            this.splitContainer1.TabIndex = 12;
            // 
            // PerformancePredictorView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.Controls.Add(this.splitContainer1);
            this.Name = "PerformancePredictorView";
            this.Size = new System.Drawing.Size(336, 298);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.resultBox.ResumeLayout(false);
            this.resultBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RadioButton daveCameron;
        private System.Windows.Forms.RadioButton reigel;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox resultBox;
        private System.Windows.Forms.RadioButton table;
        private System.Windows.Forms.RadioButton chartButton;
        private System.Windows.Forms.DataGridView dataGrid;
        private ZoneFiveSoftware.Common.Visuals.Chart.LineChart chart;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.RadioButton training;
        private System.Windows.Forms.RadioButton timePrediction;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton speed;
        private System.Windows.Forms.RadioButton pace;
        private System.Windows.Forms.SplitContainer splitContainer1;


    }
}
