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
    partial class PerformancePredictorControl
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
            this.daveCameronButton = new System.Windows.Forms.RadioButton();
            this.reigelButton = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.resultBox = new System.Windows.Forms.GroupBox();
            this.tableButton = new System.Windows.Forms.RadioButton();
            this.chartButton = new System.Windows.Forms.RadioButton();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.trainingButton = new System.Windows.Forms.RadioButton();
            this.timePredictionButton = new System.Windows.Forms.RadioButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.speedButton = new System.Windows.Forms.RadioButton();
            this.paceButton = new System.Windows.Forms.RadioButton();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.chkHighScoreBox = new System.Windows.Forms.CheckBox();
            this.trainingView = new TrainingView();
            this.predictorView = new PerformancePredictorView();

            this.groupBox1.SuspendLayout();
            this.resultBox.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // daveCameron
            // 
            this.daveCameronButton.AutoSize = true;
            this.daveCameronButton.Location = new System.Drawing.Point(6, 19);
            this.daveCameronButton.Name = "daveCameron";
            this.daveCameronButton.Size = new System.Drawing.Size(96, 17);
            this.daveCameronButton.TabIndex = 3;
            this.daveCameronButton.TabStop = true;
            this.daveCameronButton.Text = "Dave Cameron";
            this.daveCameronButton.UseVisualStyleBackColor = true;
            // 
            // reigel
            // 
            this.reigelButton.AutoSize = true;
            this.reigelButton.Location = new System.Drawing.Point(6, 42);
            this.reigelButton.Name = "reigel";
            this.reigelButton.Size = new System.Drawing.Size(80, 17);
            this.reigelButton.TabIndex = 4;
            this.reigelButton.TabStop = true;
            this.reigelButton.Text = "Pete Riegel";
            this.reigelButton.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.daveCameronButton);
            this.groupBox1.Controls.Add(this.reigelButton);
            this.groupBox1.Location = new System.Drawing.Point(4, 76);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(138, 68);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Prediction model";
            // 
            // resultBox
            // 
            this.resultBox.Controls.Add(this.tableButton);
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
            this.tableButton.AutoSize = true;
            this.tableButton.Location = new System.Drawing.Point(6, 42);
            this.tableButton.Name = "table";
            this.tableButton.Size = new System.Drawing.Size(85, 17);
            this.tableButton.TabIndex = 6;
            this.tableButton.TabStop = true;
            this.tableButton.Text = "View in table";
            this.tableButton.UseVisualStyleBackColor = true;
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
            this.groupBox3.Controls.Add(this.trainingButton);
            this.groupBox3.Controls.Add(this.timePredictionButton);
            this.groupBox3.Location = new System.Drawing.Point(4, 4);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(138, 66);
            this.groupBox3.TabIndex = 10;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Settings";
            // 
            // training
            // 
            this.trainingButton.AutoSize = true;
            this.trainingButton.Location = new System.Drawing.Point(7, 43);
            this.trainingButton.Name = "training";
            this.trainingButton.Size = new System.Drawing.Size(63, 17);
            this.trainingButton.TabIndex = 1;
            this.trainingButton.TabStop = true;
            this.trainingButton.Text = "Training";
            this.trainingButton.UseVisualStyleBackColor = true;
            // 
            // timePrediction
            // 
            this.timePredictionButton.AutoSize = true;
            this.timePredictionButton.Location = new System.Drawing.Point(7, 20);
            this.timePredictionButton.Name = "timePrediction";
            this.timePredictionButton.Size = new System.Drawing.Size(97, 17);
            this.timePredictionButton.TabIndex = 0;
            this.timePredictionButton.TabStop = true;
            this.timePredictionButton.Text = "Time prediction";
            this.timePredictionButton.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.speedButton);
            this.groupBox2.Controls.Add(this.paceButton);
            this.groupBox2.Location = new System.Drawing.Point(4, 150);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(138, 69);
            this.groupBox2.TabIndex = 11;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Velocity";
            // 
            // speed
            // 
            this.speedButton.AutoSize = true;
            this.speedButton.Location = new System.Drawing.Point(7, 44);
            this.speedButton.Name = "speed";
            this.speedButton.Size = new System.Drawing.Size(84, 17);
            this.speedButton.TabIndex = 1;
            this.speedButton.TabStop = true;
            this.speedButton.Text = "Show speed";
            this.speedButton.UseVisualStyleBackColor = true;
            // 
            // pace
            // 
            this.paceButton.AutoSize = true;
            this.paceButton.Location = new System.Drawing.Point(7, 20);
            this.paceButton.Name = "pace";
            this.paceButton.Size = new System.Drawing.Size(79, 17);
            this.paceButton.TabIndex = 0;
            this.paceButton.TabStop = true;
            this.paceButton.Text = "Show pace";
            this.paceButton.UseVisualStyleBackColor = true;
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
            this.splitContainer1.Panel1.Controls.Add(this.chkHighScoreBox);
            this.splitContainer1.Panel1.Controls.Add(this.groupBox2);
            this.splitContainer1.Panel1.Controls.Add(this.groupBox3);
            this.splitContainer1.Panel1.Controls.Add(this.resultBox);
            this.splitContainer1.Panel1.Controls.Add(this.groupBox1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.predictorView);
            this.splitContainer1.Panel2.Controls.Add(this.trainingView);
            this.splitContainer1.Panel2.Controls.Add(this.progressBar);
            this.splitContainer1.Size = new System.Drawing.Size(336, 316);
            this.splitContainer1.SplitterDistance = 145;
            this.splitContainer1.SplitterWidth = 1;
            this.splitContainer1.TabIndex = 12;
            // 
            // chkHighScore
            // 
            this.chkHighScoreBox.AutoSize = true;
            this.chkHighScoreBox.Enabled = false;
            this.chkHighScoreBox.Location = new System.Drawing.Point(11, 296);
            this.chkHighScoreBox.Name = "chkHighScore";
            this.chkHighScoreBox.Size = new System.Drawing.Size(79, 17);
            this.chkHighScoreBox.TabIndex = 12;
            this.chkHighScoreBox.Text = "High Score";
            this.chkHighScoreBox.UseVisualStyleBackColor = true;
            // 
            // predictorView
            // 
            this.predictorView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.predictorView.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.predictorView.Name = "predictorView";
            this.predictorView.Location = new System.Drawing.Point(0, 0);
            //this.predictorView.Size = new System.Drawing.Size(184, 23);
            this.predictorView.TabIndex = 9;
            // 
            // trainingView
            // 
            this.trainingView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.trainingView.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.trainingView.Name = "trainingView";
            this.trainingView.Location = new System.Drawing.Point(0, 0);
            //this.progressBar.Size = new System.Drawing.Size(184, 23);
            this.trainingView.TabIndex = 9;
            // 
            // PerformancePredictorControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.Controls.Add(this.splitContainer1);
            this.Name = "PerformancePredictorView";
            this.Size = new System.Drawing.Size(336, 316);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.resultBox.ResumeLayout(false);
            this.resultBox.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RadioButton daveCameronButton;
        private System.Windows.Forms.RadioButton reigelButton;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox resultBox;
        private System.Windows.Forms.RadioButton tableButton;
        private System.Windows.Forms.RadioButton chartButton;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.RadioButton trainingButton;
        private System.Windows.Forms.RadioButton timePredictionButton;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton speedButton;
        private System.Windows.Forms.RadioButton paceButton;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.CheckBox chkHighScoreBox;
        private TrainingView trainingView;
        private PerformancePredictorView predictorView;


    }
}
