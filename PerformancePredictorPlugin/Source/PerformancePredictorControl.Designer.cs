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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PerformancePredictorControl));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.trainingView = new TrainingView();
            this.predictorView = new TimePredictionView();
            this.extrapolateView = new ExtrapolateView();
            this.actionBanner1 = new ZoneFiveSoftware.Common.Visuals.ActionBanner();

            this.bannerContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.settingsMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.timePredictionMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.trainingMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.extrapolateMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.modelMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.daveCameronMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.peteRiegelMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.velocityMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.paceMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.speedMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.resultMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tableMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.chartMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.chkHighScoreMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.showToolBarMenuItem = new System.Windows.Forms.ToolStripMenuItem();

            this.settingsBox = new System.Windows.Forms.GroupBox();
            this.trainingButton = new System.Windows.Forms.RadioButton();
            this.timePredictionButton = new System.Windows.Forms.RadioButton();
            this.extrapolateButton = new System.Windows.Forms.RadioButton();
            this.modelBox = new System.Windows.Forms.GroupBox();
            this.daveCameronButton = new System.Windows.Forms.RadioButton();
            this.peteRiegelButton = new System.Windows.Forms.RadioButton();
            this.resultBox = new System.Windows.Forms.GroupBox();
            this.tableButton = new System.Windows.Forms.RadioButton();
            this.chartButton = new System.Windows.Forms.RadioButton();
            this.velocityBox = new System.Windows.Forms.GroupBox();
            this.speedButton = new System.Windows.Forms.RadioButton();
            this.paceButton = new System.Windows.Forms.RadioButton();
            this.chkHighScoreBox = new System.Windows.Forms.CheckBox();

            this.tableLayoutPanel1.SuspendLayout();
            this.actionBanner1.SuspendLayout();
            this.bannerContextMenuStrip.SuspendLayout();
            this.modelBox.SuspendLayout();
            this.resultBox.SuspendLayout();
            this.settingsBox.SuspendLayout();
            this.velocityBox.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.actionBanner1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.splitContainer1, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(652, 211);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // actionBanner1
            // 
            this.actionBanner1.BackColor = System.Drawing.Color.Transparent;
            this.actionBanner1.ContextMenuStrip = this.bannerContextMenuStrip;
            this.actionBanner1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.actionBanner1.HasMenuButton = true;
            this.actionBanner1.Location = new System.Drawing.Point(0, 0);
            this.actionBanner1.Margin = new System.Windows.Forms.Padding(0);
            this.actionBanner1.Name = "actionBanner1";
            this.actionBanner1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.actionBanner1.Size = new System.Drawing.Size(652, 20);
            this.actionBanner1.Style = ZoneFiveSoftware.Common.Visuals.ActionBanner.BannerStyle.Header1;
            this.actionBanner1.TabIndex = 0;
            this.actionBanner1.Tag = "";
            this.actionBanner1.UseStyleFont = true;
            // 
            // bannerContextMenuStrip
            // 
            this.bannerContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[]
            {
                this.settingsMenuItem,
                this.timePredictionMenuItem,
                this.trainingMenuItem,
                this.extrapolateMenuItem,
                this.toolStripSeparator1,
                this.modelMenuItem,
                this.daveCameronMenuItem,
                this.peteRiegelMenuItem,
                this.toolStripSeparator2,
                this.resultMenuItem,
                this.tableMenuItem,
                this.chartMenuItem,
                this.toolStripSeparator3,
                this.velocityMenuItem,
                this.paceMenuItem,
                this.speedMenuItem,
                this.toolStripSeparator4,
                this.chkHighScoreMenuItem,
                this.toolStripSeparator5,
                this.showToolBarMenuItem
            });
            this.bannerContextMenuStrip.Name = "bannerContextMenuStrip";
            this.bannerContextMenuStrip.ShowCheckMargin = true;
            this.bannerContextMenuStrip.ShowImageMargin = false;
            this.bannerContextMenuStrip.Size = new System.Drawing.Size(212, 236);
            this.actionBanner1.MenuClicked += actionBanner1_MenuClicked;
            // 
            // settingsMenuItem
            // 
            this.settingsMenuItem.Enabled = false;
            this.settingsMenuItem.Name = "settingsMenuItem";
            this.settingsMenuItem.Size = new System.Drawing.Size(154, 22);
            this.settingsMenuItem.Text = "<settingsMenuItem";
            // 
            // timePrectionMenuItem
            // 
            this.timePredictionMenuItem.Name = "timePrectionMenuItem";
            this.timePredictionMenuItem.Size = new System.Drawing.Size(154, 22);
            this.timePredictionMenuItem.Text = "<timePrectionMenuItem";
            this.timePredictionMenuItem.Click += new System.EventHandler(this.timePrediction_Click);
            // 
            // trainingMenuItem
            // 
            this.trainingMenuItem.Name = "trainingMenuItem";
            this.trainingMenuItem.Size = new System.Drawing.Size(154, 22);
            this.trainingMenuItem.Text = "<trainingMenuItem";
            this.trainingMenuItem.Click += new System.EventHandler(this.training_Click);
            // 
            // extrapolateMenuItem
            // 
            this.extrapolateMenuItem.Name = "extrapolateMenuItem";
            this.extrapolateMenuItem.Size = new System.Drawing.Size(154, 22);
            this.extrapolateMenuItem.Text = "<extrapolateMenuItem";
            this.extrapolateMenuItem.Click += new System.EventHandler(this.extrapolate_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(208, 6);
            // 
            // modelMenuItem
            // 
            this.modelMenuItem.Enabled = false;
            this.modelMenuItem.Name = "modelMenuItem";
            this.modelMenuItem.Size = new System.Drawing.Size(154, 22);
            this.modelMenuItem.Text = "<modelMenuItem";
            // 
            // daveCameronMenuItem
            // 
            this.daveCameronMenuItem.Name = "daveCameronMenuItem";
            this.daveCameronMenuItem.Size = new System.Drawing.Size(154, 22);
            this.daveCameronMenuItem.Text = "<daveCameronMenuItem";
            this.daveCameronMenuItem.Click += new System.EventHandler(this.daveCameron_Click);
            // 
            // peteRiegelMenuItem
            // 
            this.peteRiegelMenuItem.Name = "peteRiegelMenuItem";
            this.peteRiegelMenuItem.Size = new System.Drawing.Size(154, 22);
            this.peteRiegelMenuItem.Text = "<peteRiegelMenuItem";
            this.peteRiegelMenuItem.Click += new System.EventHandler(this.peteRiegel_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(208, 6);
            // 
            // velocityMenuItem
            // 
            this.velocityMenuItem.Enabled = false;
            this.velocityMenuItem.Name = "velocityMenuItem";
            this.velocityMenuItem.Size = new System.Drawing.Size(154, 22);
            this.velocityMenuItem.Text = "<velocityMenuItem";
            // 
            // paceMenuItem
            // 
            this.paceMenuItem.Name = "paceMenuItem";
            this.paceMenuItem.Size = new System.Drawing.Size(154, 22);
            this.paceMenuItem.Text = "<paceMenuItem";
            this.paceMenuItem.Click += new System.EventHandler(this.pace_Click);
            // 
            // speedMenuItem
            // 
            this.speedMenuItem.Name = "speedMenuItem";
            this.speedMenuItem.Size = new System.Drawing.Size(154, 22);
            this.speedMenuItem.Text = "<speedMenuItem";
            this.speedMenuItem.Click += new System.EventHandler(this.speed_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(208, 6);
            // 
            // resultMenuItem
            // 
            this.resultMenuItem.Enabled = false;
            this.resultMenuItem.Name = "resultMenuItem";
            this.resultMenuItem.Size = new System.Drawing.Size(154, 22);
            this.resultMenuItem.Text = "<resultMenuItem";
            // 
            // tableMenuItem
            // 
            this.tableMenuItem.Name = "tableMenuItem";
            this.tableMenuItem.Size = new System.Drawing.Size(154, 22);
            this.tableMenuItem.Text = "<tableMenuItem";
            this.tableMenuItem.Click += new System.EventHandler(this.table_Click);
            // 
            // chartMenuItem
            // 
            this.chartMenuItem.Name = "chartMenuItem";
            this.chartMenuItem.Size = new System.Drawing.Size(154, 22);
            this.chartMenuItem.Text = "<chartMenuItem";
            this.chartMenuItem.Click += new System.EventHandler(this.chart_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator2";
            this.toolStripSeparator4.Size = new System.Drawing.Size(208, 6);
            // 
            // chkHighScoreMenuItem
            // 
            this.chkHighScoreMenuItem.Name = "chkHighScoreMenuItem";
            this.chkHighScoreMenuItem.Size = new System.Drawing.Size(154, 22);
            this.chkHighScoreMenuItem.Text = "<chkHighScoreMenuItem";
            this.chkHighScoreMenuItem.Click += new System.EventHandler(this.chkHighScore_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(208, 6);
            // 
            // showChartToolsMenuItem
            // 
            this.showToolBarMenuItem.Name = "showChartToolsMenuItem";
            this.showToolBarMenuItem.Size = new System.Drawing.Size(211, 22);
            this.showToolBarMenuItem.Text = "< showChartTools";
            this.showToolBarMenuItem.Click += new System.EventHandler(this.showToolBarMenuItem_Click);
            // 
            // daveCameronButton
            // 
            this.daveCameronButton.AutoSize = true;
            this.daveCameronButton.Location = new System.Drawing.Point(6, 19);
            this.daveCameronButton.Name = "daveCameronButton";
            this.daveCameronButton.Size = new System.Drawing.Size(96, 17);
            this.daveCameronButton.TabIndex = 3;
            this.daveCameronButton.TabStop = true;
            this.daveCameronButton.Text = "<Dave Cameron";
            this.daveCameronButton.UseVisualStyleBackColor = true;
            this.daveCameronButton.Click += new System.EventHandler(this.daveCameron_Click);
            // 
            // peteRiegelButton
            // 
            this.peteRiegelButton.AutoSize = true;
            this.peteRiegelButton.Location = new System.Drawing.Point(6, 42);
            this.peteRiegelButton.Name = "peteRiegelButton";
            this.peteRiegelButton.Size = new System.Drawing.Size(80, 17);
            this.peteRiegelButton.TabIndex = 4;
            this.peteRiegelButton.TabStop = true;
            this.peteRiegelButton.Text = "<Pete Riegel";
            this.peteRiegelButton.UseVisualStyleBackColor = true;
            this.peteRiegelButton.Click += new System.EventHandler(this.peteRiegel_Click);
            // 
            // modelBox
            // 
            this.modelBox.Controls.Add(this.daveCameronButton);
            this.modelBox.Controls.Add(this.peteRiegelButton);
            this.modelBox.Location = new System.Drawing.Point(4, 99);
            this.modelBox.Name = "modelBox";
            this.modelBox.Size = new System.Drawing.Size(138, 68);
            this.modelBox.TabIndex = 5;
            this.modelBox.TabStop = false;
            this.modelBox.Text = "<Prediction model";
            // 
            // resultBox
            // 
            this.resultBox.Controls.Add(this.tableButton);
            this.resultBox.Controls.Add(this.chartButton);
            this.resultBox.Location = new System.Drawing.Point(4, 248);
            this.resultBox.Name = "resultBox";
            this.resultBox.Size = new System.Drawing.Size(138, 67);
            this.resultBox.TabIndex = 5;
            this.resultBox.TabStop = false;
            this.resultBox.Text = "<Prediction results";
            // 
            // tableButton
            // 
            this.tableButton.AutoSize = true;
            this.tableButton.Location = new System.Drawing.Point(6, 19);
            this.tableButton.Name = "table";
            this.tableButton.Size = new System.Drawing.Size(85, 17);
            this.tableButton.TabIndex = 6;
            this.tableButton.TabStop = true;
            this.tableButton.Text = "<View in table";
            this.tableButton.UseVisualStyleBackColor = true;
            this.tableButton.Click += new System.EventHandler(this.table_Click);
            // 
            // chartButton
            // 
            this.chartButton.AutoSize = true;
            this.chartButton.Location = new System.Drawing.Point(6, 42);
            this.chartButton.Name = "chartButton";
            this.chartButton.Size = new System.Drawing.Size(86, 17);
            this.chartButton.TabIndex = 0;
            this.chartButton.TabStop = true;
            this.chartButton.Text = "<View in chart";
            this.chartButton.UseVisualStyleBackColor = true;
            this.chartButton.Click += new System.EventHandler(this.chart_Click);
            // 
            // settingsBox
            // 
            this.settingsBox.Controls.Add(this.trainingButton);
            this.settingsBox.Controls.Add(this.timePredictionButton);
            this.settingsBox.Controls.Add(this.extrapolateButton);
            this.settingsBox.Location = new System.Drawing.Point(4, 4);
            this.settingsBox.Name = "settingsBox";
            this.settingsBox.Size = new System.Drawing.Size(138, 89);
            this.settingsBox.TabIndex = 10;
            this.settingsBox.TabStop = false;
            this.settingsBox.Text = "<Settings";
            // 
            // training
            // 
            this.trainingButton.AutoSize = true;
            this.trainingButton.Location = new System.Drawing.Point(7, 43);
            this.trainingButton.Name = "training";
            this.trainingButton.Size = new System.Drawing.Size(63, 17);
            this.trainingButton.TabIndex = 1;
            this.trainingButton.TabStop = true;
            this.trainingButton.Text = "<Training";
            this.trainingButton.UseVisualStyleBackColor = true;
            this.trainingButton.Click += new System.EventHandler(this.training_Click);
            // 
            // timePredictionButton
            // 
            this.timePredictionButton.AutoSize = true;
            this.timePredictionButton.Location = new System.Drawing.Point(7, 20);
            this.timePredictionButton.Name = "timePredictionButton";
            this.timePredictionButton.Size = new System.Drawing.Size(97, 17);
            this.timePredictionButton.TabIndex = 0;
            this.timePredictionButton.TabStop = true;
            this.timePredictionButton.Text = "<Time prediction";
            this.timePredictionButton.UseVisualStyleBackColor = true;
            this.timePredictionButton.Click += new System.EventHandler(this.timePrediction_Click);
            // 
            // extrapolateButton
            // 
            this.extrapolateButton.AutoSize = true;
            this.extrapolateButton.Location = new System.Drawing.Point(7, 66);
            this.extrapolateButton.Name = "extrapolateButton";
            this.extrapolateButton.Size = new System.Drawing.Size(97, 17);
            this.extrapolateButton.TabIndex = 1;
            this.extrapolateButton.TabStop = true;
            this.extrapolateButton.Text = "<extrapolateButton";
            this.extrapolateButton.UseVisualStyleBackColor = true;
            this.extrapolateButton.Click += new System.EventHandler(this.extrapolate_Click);
            // 
            // velocityBox
            // 
            this.velocityBox.Controls.Add(this.speedButton);
            this.velocityBox.Controls.Add(this.paceButton);
            this.velocityBox.Location = new System.Drawing.Point(4, 173);
            this.velocityBox.Name = "velocityBox";
            this.velocityBox.Size = new System.Drawing.Size(138, 69);
            this.velocityBox.TabIndex = 11;
            this.velocityBox.TabStop = false;
            this.velocityBox.Text = "<Velocity";
            // 
            // speedButton
            // 
            this.speedButton.AutoSize = true;
            this.speedButton.Location = new System.Drawing.Point(7, 44);
            this.speedButton.Name = "speedButton";
            this.speedButton.Size = new System.Drawing.Size(84, 17);
            this.speedButton.TabIndex = 1;
            this.speedButton.TabStop = true;
            this.speedButton.Text = "<Show speed";
            this.speedButton.UseVisualStyleBackColor = true;
            this.speedButton.Click += new System.EventHandler(this.speed_Click);
            // 
            // paceButton
            // 
            this.paceButton.AutoSize = true;
            this.paceButton.Location = new System.Drawing.Point(7, 20);
            this.paceButton.Name = "paceButton";
            this.paceButton.Size = new System.Drawing.Size(79, 17);
            this.paceButton.TabIndex = 0;
            this.paceButton.TabStop = true;
            this.paceButton.Text = "<Show pace";
            this.paceButton.UseVisualStyleBackColor = true;
            this.paceButton.Click += new System.EventHandler(this.pace_Click);
            // 
            // chkHighScoreBox
            // 
            this.chkHighScoreBox.AutoSize = true;
            this.chkHighScoreBox.Enabled = false;
            this.chkHighScoreBox.Location = new System.Drawing.Point(11, 319);
            this.chkHighScoreBox.Name = "chkHighScoreBox";
            this.chkHighScoreBox.Size = new System.Drawing.Size(79, 17);
            this.chkHighScoreBox.TabIndex = 12;
            this.chkHighScoreBox.Text = "<High Score";
            this.chkHighScoreBox.UseVisualStyleBackColor = true;
            this.chkHighScoreBox.Click += new System.EventHandler(this.chkHighScore_Click);
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
            this.splitContainer1.Panel1.Controls.Add(this.velocityBox);
            this.splitContainer1.Panel1.Controls.Add(this.settingsBox);
            this.splitContainer1.Panel1.Controls.Add(this.resultBox);
            this.splitContainer1.Panel1.Controls.Add(this.modelBox);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.predictorView);
            this.splitContainer1.Panel2.Controls.Add(this.trainingView);
            this.splitContainer1.Size = new System.Drawing.Size(336, 316);
            this.splitContainer1.Panel1MinSize = 0;
            this.splitContainer1.SplitterDistance = 145;
            this.splitContainer1.SplitterWidth = 1;
            this.splitContainer1.TabIndex = 12;
            // 
            // PerformancePredictorControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "PerformancePredictorView";
            this.Size = new System.Drawing.Size(336, 216);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.actionBanner1.ResumeLayout(false);
            this.bannerContextMenuStrip.ResumeLayout(false);
            this.settingsBox.ResumeLayout(false);
            this.settingsBox.PerformLayout();
            this.modelBox.ResumeLayout(false);
            this.modelBox.PerformLayout();
            this.resultBox.ResumeLayout(false);
            this.resultBox.PerformLayout();
            this.velocityBox.ResumeLayout(false);
            this.velocityBox.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private ZoneFiveSoftware.Common.Visuals.ActionBanner actionBanner1;
        private TrainingView trainingView;
        private TimePredictionView predictorView;
        private ExtrapolateView extrapolateView;

        private System.Windows.Forms.ContextMenuStrip bannerContextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem settingsMenuItem;
        private System.Windows.Forms.ToolStripMenuItem timePredictionMenuItem;
        private System.Windows.Forms.ToolStripMenuItem trainingMenuItem;
        private System.Windows.Forms.ToolStripMenuItem extrapolateMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem modelMenuItem;
        private System.Windows.Forms.ToolStripMenuItem daveCameronMenuItem;
        private System.Windows.Forms.ToolStripMenuItem peteRiegelMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem resultMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tableMenuItem;
        private System.Windows.Forms.ToolStripMenuItem chartMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem velocityMenuItem;
        private System.Windows.Forms.ToolStripMenuItem speedMenuItem;
        private System.Windows.Forms.ToolStripMenuItem paceMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripMenuItem chkHighScoreMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripMenuItem showToolBarMenuItem;

        private System.Windows.Forms.GroupBox settingsBox;
        private System.Windows.Forms.RadioButton timePredictionButton;
        private System.Windows.Forms.RadioButton trainingButton;
        private System.Windows.Forms.RadioButton extrapolateButton;
        private System.Windows.Forms.GroupBox modelBox;
        private System.Windows.Forms.RadioButton daveCameronButton;
        private System.Windows.Forms.RadioButton peteRiegelButton;
        private System.Windows.Forms.GroupBox resultBox;
        private System.Windows.Forms.RadioButton tableButton;
        private System.Windows.Forms.RadioButton chartButton;
        private System.Windows.Forms.GroupBox velocityBox;
        private System.Windows.Forms.RadioButton speedButton;
        private System.Windows.Forms.RadioButton paceButton;
        private System.Windows.Forms.CheckBox chkHighScoreBox;




    }
}
