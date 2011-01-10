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
    partial class OverlayView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OverlayView));
            this.xAxisTimeMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.chart = new ZoneFiveSoftware.Common.Visuals.Chart.LineChart();
            this.heartRate = new System.Windows.Forms.CheckBox();
            this.pace = new System.Windows.Forms.CheckBox();
            this.speed = new System.Windows.Forms.CheckBox();
            this.useTime = new System.Windows.Forms.RadioButton();
            this.labelXaxis = new System.Windows.Forms.Label();
            this.useDistance = new System.Windows.Forms.RadioButton();
            this.labelYaxis = new System.Windows.Forms.Label();
            this.power = new System.Windows.Forms.CheckBox();
            this.cadence = new System.Windows.Forms.CheckBox();
            this.elevation = new System.Windows.Forms.CheckBox();
            this.toolTipMAbox = new System.Windows.Forms.ToolTip(this.components);
            this.btnSaveImage = new ZoneFiveSoftware.Common.Visuals.Button();
            this.distance = new System.Windows.Forms.CheckBox();
            this.time = new System.Windows.Forms.CheckBox();
            this.panel1 = new ZoneFiveSoftware.Common.Visuals.Panel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.actionBanner1 = new ZoneFiveSoftware.Common.Visuals.ActionBanner();
            this.bannerContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.xAxisMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.bannerXAxisMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.xAxisDistanceMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.bannerShowContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.showHRMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showPaceMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showSpeedMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showPowerMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showCadenceMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showElevationMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showTimeMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showDistanceMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showDiffMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.bannerShowDiffContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.showTimeDiffMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showDistDiffMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showHRDiffMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showMeanMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showRollingAverageMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.setRollAvgWidthMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.offsetMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.setRefActMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.showChartToolsMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showToolBarMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.expandButton = new ZoneFiveSoftware.Common.Visuals.Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.chartBackgroundPanel = new System.Windows.Forms.Panel();
            this.splitContainer4 = new System.Windows.Forms.SplitContainer();
            this.panel2 = new ZoneFiveSoftware.Common.Visuals.Panel();
            this.treeListAct = new ZoneFiveSoftware.Common.Visuals.TreeList();
            this.treeListContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tableSettingsMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.setRefTreeListMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.visibleMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.visibleMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.allVisibleMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.noneVisibleMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.advancedMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.limitActivityMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.selectWithURMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.setOffsetWithURMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.actionBanner1.SuspendLayout();
            this.bannerContextMenuStrip.SuspendLayout();
            this.bannerXAxisMenuStrip.SuspendLayout();
            this.bannerShowContextMenuStrip.SuspendLayout();
            this.bannerShowDiffContextMenuStrip.SuspendLayout();
            this.panel3.SuspendLayout();
            this.chartBackgroundPanel.SuspendLayout();
            this.splitContainer4.Panel1.SuspendLayout();
            this.splitContainer4.Panel2.SuspendLayout();
            this.splitContainer4.SuspendLayout();
            this.panel2.SuspendLayout();
            this.treeListContextMenuStrip.SuspendLayout();
            this.visibleMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // xAxisTimeMenuItem
            // 
            this.xAxisTimeMenuItem.Name = "xAxisTimeMenuItem";
            this.xAxisTimeMenuItem.Size = new System.Drawing.Size(119, 22);
            this.xAxisTimeMenuItem.Text = "Time";
            this.xAxisTimeMenuItem.Click += new System.EventHandler(this.commonXAxisMenuItem_click);
            // 
            // chart
            // 
            this.chart.AutoScroll = true;
            this.chart.BackColor = System.Drawing.Color.White;
            this.chart.Border = ZoneFiveSoftware.Common.Visuals.ControlBorder.Style.SmallRoundShadow;
            this.chart.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chart.Location = new System.Drawing.Point(0, 0);
            this.chart.Name = "chart";
            this.chart.Padding = new System.Windows.Forms.Padding(5);
            this.chart.Size = new System.Drawing.Size(646, 135);
            this.chart.TabIndex = 12;
            // 
            // heartRate
            // 
            this.heartRate.AutoSize = true;
            this.heartRate.Location = new System.Drawing.Point(44, 18);
            this.heartRate.Name = "heartRate";
            this.heartRate.Size = new System.Drawing.Size(73, 17);
            this.heartRate.TabIndex = 3;
            this.heartRate.Text = "Heart rate";
            this.heartRate.UseVisualStyleBackColor = true;
            this.heartRate.CheckedChanged += new System.EventHandler(this.heartRate_CheckedChanged);
            // 
            // pace
            // 
            this.pace.AutoSize = true;
            this.pace.Location = new System.Drawing.Point(123, 18);
            this.pace.Name = "pace";
            this.pace.Size = new System.Drawing.Size(51, 17);
            this.pace.TabIndex = 4;
            this.pace.Text = "Pace";
            this.pace.UseVisualStyleBackColor = true;
            this.pace.CheckedChanged += new System.EventHandler(this.pace_CheckedChanged);
            // 
            // speed
            // 
            this.speed.AutoSize = true;
            this.speed.Location = new System.Drawing.Point(180, 19);
            this.speed.Name = "speed";
            this.speed.Size = new System.Drawing.Size(57, 17);
            this.speed.TabIndex = 5;
            this.speed.Text = "Speed";
            this.speed.UseVisualStyleBackColor = true;
            this.speed.CheckedChanged += new System.EventHandler(this.speed_CheckedChanged);
            // 
            // useTime
            // 
            this.useTime.AutoSize = true;
            this.useTime.Location = new System.Drawing.Point(44, 0);
            this.useTime.Name = "useTime";
            this.useTime.Size = new System.Drawing.Size(48, 17);
            this.useTime.TabIndex = 1;
            this.useTime.Text = "Time";
            this.useTime.UseVisualStyleBackColor = true;
            this.useTime.CheckedChanged += new System.EventHandler(this.useTime_CheckedChanged);
            // 
            // labelXaxis
            // 
            this.labelXaxis.AutoSize = true;
            this.labelXaxis.Location = new System.Drawing.Point(0, 0);
            this.labelXaxis.Name = "labelXaxis";
            this.labelXaxis.Size = new System.Drawing.Size(38, 13);
            this.labelXaxis.TabIndex = 9;
            this.labelXaxis.Text = "X axis:";
            // 
            // useDistance
            // 
            this.useDistance.AutoSize = true;
            this.useDistance.Location = new System.Drawing.Point(98, 0);
            this.useDistance.Name = "useDistance";
            this.useDistance.Size = new System.Drawing.Size(67, 17);
            this.useDistance.TabIndex = 2;
            this.useDistance.Text = "Distance";
            this.useDistance.UseVisualStyleBackColor = true;
            this.useDistance.CheckedChanged += new System.EventHandler(this.useDistance_CheckedChanged);
            // 
            // labelYaxis
            // 
            this.labelYaxis.AutoSize = true;
            this.labelYaxis.Location = new System.Drawing.Point(0, 19);
            this.labelYaxis.Name = "labelYaxis";
            this.labelYaxis.Size = new System.Drawing.Size(38, 13);
            this.labelYaxis.TabIndex = 11;
            this.labelYaxis.Text = "Y axis:";
            // 
            // power
            // 
            this.power.AutoSize = true;
            this.power.Location = new System.Drawing.Point(243, 19);
            this.power.Name = "power";
            this.power.Size = new System.Drawing.Size(56, 17);
            this.power.TabIndex = 6;
            this.power.Text = "Power";
            this.power.UseVisualStyleBackColor = true;
            this.power.CheckedChanged += new System.EventHandler(this.power_CheckedChanged);
            // 
            // cadence
            // 
            this.cadence.AutoSize = true;
            this.cadence.Location = new System.Drawing.Point(305, 19);
            this.cadence.Name = "cadence";
            this.cadence.Size = new System.Drawing.Size(69, 17);
            this.cadence.TabIndex = 7;
            this.cadence.Text = "Cadence";
            this.cadence.UseVisualStyleBackColor = true;
            this.cadence.CheckedChanged += new System.EventHandler(this.cadence_CheckedChanged);
            // 
            // elevation
            // 
            this.elevation.AutoSize = true;
            this.elevation.Location = new System.Drawing.Point(380, 19);
            this.elevation.Name = "elevation";
            this.elevation.Size = new System.Drawing.Size(70, 17);
            this.elevation.TabIndex = 8;
            this.elevation.Text = "Elevation";
            this.elevation.UseVisualStyleBackColor = true;
            this.elevation.CheckedChanged += new System.EventHandler(this.elevation_CheckedChanged);
            // 
            // btnSaveImage
            // 
            this.btnSaveImage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSaveImage.AutoSize = true;
            this.btnSaveImage.BackColor = System.Drawing.Color.Transparent;
            this.btnSaveImage.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnSaveImage.BackgroundImage")));
            this.btnSaveImage.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnSaveImage.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(40)))), ((int)(((byte)(50)))), ((int)(((byte)(120)))));
            this.btnSaveImage.CenterImage = null;
            this.btnSaveImage.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnSaveImage.HyperlinkStyle = false;
            this.btnSaveImage.ImageMargin = 2;
            this.btnSaveImage.LeftImage = null;
            this.btnSaveImage.Location = new System.Drawing.Point(620, 20);
            this.btnSaveImage.Name = "btnSaveImage";
            this.btnSaveImage.PushStyle = true;
            this.btnSaveImage.RightImage = null;
            this.btnSaveImage.Size = new System.Drawing.Size(23, 23);
            this.btnSaveImage.TabIndex = 23;
            this.btnSaveImage.TextAlign = System.Drawing.StringAlignment.Center;
            this.btnSaveImage.TextLeftMargin = 2;
            this.btnSaveImage.TextRightMargin = 2;
            this.btnSaveImage.Click += new System.EventHandler(this.btnSaveImage_Click);
            // 
            // distance
            // 
            this.distance.AutoSize = true;
            this.distance.Location = new System.Drawing.Point(511, 19);
            this.distance.Name = "distance";
            this.distance.Size = new System.Drawing.Size(68, 17);
            this.distance.TabIndex = 25;
            this.distance.Text = "Distance";
            this.distance.UseVisualStyleBackColor = true;
            this.distance.CheckedChanged += new System.EventHandler(this.distance_CheckedChanged);
            // 
            // time
            // 
            this.time.AutoSize = true;
            this.time.Location = new System.Drawing.Point(456, 19);
            this.time.Name = "time";
            this.time.Size = new System.Drawing.Size(49, 17);
            this.time.TabIndex = 24;
            this.time.Text = "Time";
            this.time.UseVisualStyleBackColor = true;
            this.time.CheckedChanged += new System.EventHandler(this.time_CheckedChanged);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.BorderColor = System.Drawing.Color.Gray;
            this.panel1.Controls.Add(this.tableLayoutPanel1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.HeadingBackColor = System.Drawing.Color.LightBlue;
            this.panel1.HeadingFont = null;
            this.panel1.HeadingLeftMargin = 0;
            this.panel1.HeadingText = null;
            this.panel1.HeadingTextColor = System.Drawing.Color.Black;
            this.panel1.HeadingTopMargin = 3;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(652, 211);
            this.panel1.TabIndex = 25;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.actionBanner1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel3, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.chartBackgroundPanel, 0, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(652, 211);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // actionBanner1
            // 
            this.actionBanner1.BackColor = System.Drawing.Color.Transparent;
            this.actionBanner1.ContextMenuStrip = this.bannerContextMenuStrip;
            this.actionBanner1.Controls.Add(this.expandButton);
            this.actionBanner1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.actionBanner1.HasMenuButton = true;
            this.actionBanner1.Location = new System.Drawing.Point(0, 0);
            this.actionBanner1.Margin = new System.Windows.Forms.Padding(0);
            this.actionBanner1.Name = "actionBanner1";
            this.actionBanner1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.actionBanner1.Size = new System.Drawing.Size(652, 20);
            this.actionBanner1.Style = ZoneFiveSoftware.Common.Visuals.ActionBanner.BannerStyle.Header2;
            this.actionBanner1.TabIndex = 0;
            this.actionBanner1.Tag = "";
            this.actionBanner1.UseStyleFont = true;
            // 
            // bannerContextMenuStrip
            // 
            this.bannerContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.xAxisMenuItem,
            this.showMenuItem,
            this.showDiffMenuItem,
            this.showMeanMenuItem,
            this.showRollingAverageMenuItem,
            this.toolStripSeparator1,
            this.setRollAvgWidthMenuItem,
            this.offsetMenuItem,
            this.setRefActMenuItem,
            this.toolStripSeparator2,
            this.showChartToolsMenuItem,
            this.showToolBarMenuItem});
            this.bannerContextMenuStrip.Name = "bannerContextMenuStrip";
            this.bannerContextMenuStrip.ShowCheckMargin = true;
            this.bannerContextMenuStrip.ShowImageMargin = false;
            this.bannerContextMenuStrip.Size = new System.Drawing.Size(212, 236);
            this.bannerContextMenuStrip.Opening += new System.ComponentModel.CancelEventHandler(this.bannerContextMenuStrip_Opening);
            // 
            // xAxisMenuItem
            // 
            this.xAxisMenuItem.DropDown = this.bannerXAxisMenuStrip;
            this.xAxisMenuItem.Name = "xAxisMenuItem";
            this.xAxisMenuItem.Size = new System.Drawing.Size(211, 22);
            this.xAxisMenuItem.Text = "X Axis";
            // 
            // bannerXAxisMenuStrip
            // 
            this.bannerXAxisMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.xAxisTimeMenuItem,
            this.xAxisDistanceMenuItem});
            this.bannerXAxisMenuStrip.Name = "bannerXAxisMenuStrip";
            this.bannerXAxisMenuStrip.OwnerItem = this.xAxisMenuItem;
            this.bannerXAxisMenuStrip.Size = new System.Drawing.Size(120, 48);
            this.bannerXAxisMenuStrip.Opening += new System.ComponentModel.CancelEventHandler(this.bannerXAxisMenuStrip_Opening);
            // 
            // xAxisDistanceMenuItem
            // 
            this.xAxisDistanceMenuItem.Name = "xAxisDistanceMenuItem";
            this.xAxisDistanceMenuItem.Size = new System.Drawing.Size(119, 22);
            this.xAxisDistanceMenuItem.Text = "Distance";
            this.xAxisDistanceMenuItem.Click += new System.EventHandler(this.commonXAxisMenuItem_click);
            // 
            // showMenuItem
            // 
            this.showMenuItem.DropDown = this.bannerShowContextMenuStrip;
            this.showMenuItem.Name = "showMenuItem";
            this.showMenuItem.Size = new System.Drawing.Size(211, 22);
            this.showMenuItem.Text = "Show";
            // 
            // bannerShowContextMenuStrip
            // 
            this.bannerShowContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.showHRMenuItem,
            this.showPaceMenuItem,
            this.showSpeedMenuItem,
            this.showPowerMenuItem,
            this.showCadenceMenuItem,
            this.showElevationMenuItem,
            this.showTimeMenuItem,
            this.showDistanceMenuItem});
            this.bannerShowContextMenuStrip.Name = "bannerShowContextMenuStrip";
            this.bannerShowContextMenuStrip.OwnerItem = this.showMenuItem;
            this.bannerShowContextMenuStrip.Size = new System.Drawing.Size(155, 180);
            this.bannerShowContextMenuStrip.Opening += new System.ComponentModel.CancelEventHandler(this.bannerShowContextMenuStrip_Opening);
            // 
            // showHRMenuItem
            // 
            this.showHRMenuItem.Name = "showHRMenuItem";
            this.showHRMenuItem.Size = new System.Drawing.Size(154, 22);
            this.showHRMenuItem.Text = "Show HR";
            this.showHRMenuItem.Click += new System.EventHandler(this.showCommonYAxisMenuItem_click);
            // 
            // showPaceMenuItem
            // 
            this.showPaceMenuItem.Name = "showPaceMenuItem";
            this.showPaceMenuItem.Size = new System.Drawing.Size(154, 22);
            this.showPaceMenuItem.Text = "Show Pace";
            this.showPaceMenuItem.Click += new System.EventHandler(this.showCommonYAxisMenuItem_click);
            // 
            // showSpeedMenuItem
            // 
            this.showSpeedMenuItem.Name = "showSpeedMenuItem";
            this.showSpeedMenuItem.Size = new System.Drawing.Size(154, 22);
            this.showSpeedMenuItem.Text = "Show Speed";
            this.showSpeedMenuItem.Click += new System.EventHandler(this.showCommonYAxisMenuItem_click);
            // 
            // showPowerMenuItem
            // 
            this.showPowerMenuItem.Name = "showPowerMenuItem";
            this.showPowerMenuItem.Size = new System.Drawing.Size(154, 22);
            this.showPowerMenuItem.Text = "Show Power";
            this.showPowerMenuItem.Click += new System.EventHandler(this.showCommonYAxisMenuItem_click);
            // 
            // showCadenceMenuItem
            // 
            this.showCadenceMenuItem.Name = "showCadenceMenuItem";
            this.showCadenceMenuItem.Size = new System.Drawing.Size(154, 22);
            this.showCadenceMenuItem.Text = "Show Cadence";
            this.showCadenceMenuItem.Click += new System.EventHandler(this.showCommonYAxisMenuItem_click);
            // 
            // showElevationMenuItem
            // 
            this.showElevationMenuItem.Name = "showElevationMenuItem";
            this.showElevationMenuItem.Size = new System.Drawing.Size(154, 22);
            this.showElevationMenuItem.Text = "Show Elevation";
            this.showElevationMenuItem.Click += new System.EventHandler(this.showCommonYAxisMenuItem_click);
            // 
            // showTimeMenuItem
            // 
            this.showTimeMenuItem.Name = "showTimeMenuItem";
            this.showTimeMenuItem.Size = new System.Drawing.Size(154, 22);
            this.showTimeMenuItem.Text = "Show Time";
            this.showTimeMenuItem.Click += new System.EventHandler(this.showCommonYAxisMenuItem_click);
            // 
            // showDistanceMenuItem
            // 
            this.showDistanceMenuItem.Name = "showDistanceMenuItem";
            this.showDistanceMenuItem.Size = new System.Drawing.Size(154, 22);
            this.showDistanceMenuItem.Text = "Show Distance";
            this.showDistanceMenuItem.Click += new System.EventHandler(this.showCommonYAxisMenuItem_click);
            // 
            // showDiffMenuItem
            // 
            this.showDiffMenuItem.DropDown = this.bannerShowDiffContextMenuStrip;
            this.showDiffMenuItem.Name = "showDiffMenuItem";
            this.showDiffMenuItem.Size = new System.Drawing.Size(211, 22);
            this.showDiffMenuItem.Text = "Show Diff";
            // 
            // bannerShowDiffContextMenuStrip
            // 
            this.bannerShowDiffContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.showTimeDiffMenuItem,
            this.showDistDiffMenuItem,
            this.showHRDiffMenuItem});
            this.bannerShowDiffContextMenuStrip.Name = "bannerShowDiffContextMenuStrip";
            this.bannerShowDiffContextMenuStrip.OwnerItem = this.showDiffMenuItem;
            this.bannerShowDiffContextMenuStrip.Size = new System.Drawing.Size(174, 70);
            this.bannerShowDiffContextMenuStrip.Opening += new System.ComponentModel.CancelEventHandler(this.bannerShowDiffContextMenuStrip_Opening);
            // 
            // showTimeDiffMenuItem
            // 
            this.showTimeDiffMenuItem.Name = "showTimeDiffMenuItem";
            this.showTimeDiffMenuItem.Size = new System.Drawing.Size(173, 22);
            this.showTimeDiffMenuItem.Text = "Show Time Diff";
            this.showTimeDiffMenuItem.Click += new System.EventHandler(this.showDiffYAxisMenuItem_click);
            // 
            // showDistDiffMenuItem
            // 
            this.showDistDiffMenuItem.Name = "showDistDiffMenuItem";
            this.showDistDiffMenuItem.Size = new System.Drawing.Size(173, 22);
            this.showDistDiffMenuItem.Text = "Show Distance Diff";
            this.showDistDiffMenuItem.Click += new System.EventHandler(this.showDiffYAxisMenuItem_click);
            // 
            // showHRDiffMenuItem
            // 
            this.showHRDiffMenuItem.Name = "showHRDiffMenuItem";
            this.showHRDiffMenuItem.Size = new System.Drawing.Size(173, 22);
            this.showHRDiffMenuItem.Text = "Show HR Diff";
            this.showHRDiffMenuItem.Click += new System.EventHandler(this.showDiffYAxisMenuItem_click);
            // 
            // showMeanMenuItem
            // 
            this.showMeanMenuItem.Name = "showMeanMenuItem";
            this.showMeanMenuItem.Size = new System.Drawing.Size(211, 22);
            this.showMeanMenuItem.Text = "Mean";
            this.showMeanMenuItem.Click += new System.EventHandler(this.ShowMeanMenuItem_Click);
            // 
            // showRollingAverageMenuItem
            // 
            this.showRollingAverageMenuItem.Name = "showRollingAverageMenuItem";
            this.showRollingAverageMenuItem.Size = new System.Drawing.Size(211, 22);
            this.showRollingAverageMenuItem.Text = "Rolling Average";
            this.showRollingAverageMenuItem.Click += new System.EventHandler(this.rollingAverageToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(208, 6);
            // 
            // setRollAvgWidthMenuItem
            // 
            this.setRollAvgWidthMenuItem.Name = "setRollAvgWidthMenuItem";
            this.setRollAvgWidthMenuItem.Size = new System.Drawing.Size(211, 22);
            this.setRollAvgWidthMenuItem.Text = "Set moving average width";
            this.setRollAvgWidthMenuItem.Click += new System.EventHandler(this.setRollAvgWidthMenuItem_Click);
            // 
            // offsetMenuItem
            // 
            this.offsetMenuItem.Name = "offsetMenuItem";
            this.offsetMenuItem.Size = new System.Drawing.Size(211, 22);
            this.offsetMenuItem.Text = "Set activity offset";
            this.offsetMenuItem.Click += new System.EventHandler(this.offsetMenuItem_Click);
            // 
            // setRefActMenuItem
            // 
            this.setRefActMenuItem.Name = "setRefActMenuItem";
            this.setRefActMenuItem.Size = new System.Drawing.Size(211, 22);
            this.setRefActMenuItem.Text = "Set reference activity";
            this.setRefActMenuItem.Click += new System.EventHandler(this.setRefActMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(208, 6);
            // 
            // showChartToolsMenuItem
            // 
            this.showChartToolsMenuItem.Name = "showChartToolsMenuItem";
            this.showChartToolsMenuItem.Size = new System.Drawing.Size(211, 22);
            this.showChartToolsMenuItem.Text = "< showChartTools";
            this.showChartToolsMenuItem.Click += new System.EventHandler(this.showChartToolsMenuItem_Click);
            // 
            // showToolBarMenuItem
            // 
            this.showToolBarMenuItem.Name = "showToolBarMenuItem";
            this.showToolBarMenuItem.Size = new System.Drawing.Size(211, 22);
            this.showToolBarMenuItem.Text = "< showToolBar";
            this.showToolBarMenuItem.Click += new System.EventHandler(this.showToolBarMenuItem_Click);
            // 
            // expandButton
            // 
            this.expandButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.expandButton.BackColor = System.Drawing.Color.Transparent;
            this.expandButton.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(40)))), ((int)(((byte)(50)))), ((int)(((byte)(120)))));
            this.expandButton.CenterImage = null;
            this.expandButton.DialogResult = System.Windows.Forms.DialogResult.None;
            this.expandButton.HyperlinkStyle = false;
            this.expandButton.ImageMargin = 2;
            this.expandButton.LeftImage = null;
            this.expandButton.Location = new System.Drawing.Point(602, 2);
            this.expandButton.Name = "expandButton";
            this.expandButton.PushStyle = true;
            this.expandButton.RightImage = null;
            this.expandButton.Size = new System.Drawing.Size(17, 17);
            this.expandButton.TabIndex = 1;
            this.expandButton.TextAlign = System.Drawing.StringAlignment.Center;
            this.expandButton.TextLeftMargin = 2;
            this.expandButton.TextRightMargin = 2;
            this.expandButton.Click += new System.EventHandler(this.expandButton_Click);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.distance);
            this.panel3.Controls.Add(this.btnSaveImage);
            this.panel3.Controls.Add(this.time);
            this.panel3.Controls.Add(this.labelXaxis);
            this.panel3.Controls.Add(this.elevation);
            this.panel3.Controls.Add(this.heartRate);
            this.panel3.Controls.Add(this.cadence);
            this.panel3.Controls.Add(this.pace);
            this.panel3.Controls.Add(this.power);
            this.panel3.Controls.Add(this.speed);
            this.panel3.Controls.Add(this.labelYaxis);
            this.panel3.Controls.Add(this.useTime);
            this.panel3.Controls.Add(this.useDistance);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(3, 23);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(646, 44);
            this.panel3.TabIndex = 1;
            // 
            // chartBackgroundPanel
            // 
            this.chartBackgroundPanel.Controls.Add(this.chart);
            this.chartBackgroundPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chartBackgroundPanel.Location = new System.Drawing.Point(3, 73);
            this.chartBackgroundPanel.Name = "chartBackgroundPanel";
            this.chartBackgroundPanel.Size = new System.Drawing.Size(646, 135);
            this.chartBackgroundPanel.TabIndex = 2;
            // 
            // splitContainer4
            // 
            this.splitContainer4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer4.Location = new System.Drawing.Point(0, 0);
            this.splitContainer4.Margin = new System.Windows.Forms.Padding(0);
            this.splitContainer4.Name = "splitContainer4";
            this.splitContainer4.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer4.Panel1
            // 
            this.splitContainer4.Panel1.Controls.Add(this.panel2);
            // 
            // splitContainer4.Panel2
            // 
            this.splitContainer4.Panel2.Controls.Add(this.panel1);
            this.splitContainer4.Size = new System.Drawing.Size(652, 285);
            this.splitContainer4.SplitterDistance = 70;
            this.splitContainer4.TabIndex = 26;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Transparent;
            this.panel2.BorderColor = System.Drawing.Color.Gray;
            this.panel2.Controls.Add(this.treeListAct);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.HeadingBackColor = System.Drawing.Color.LightBlue;
            this.panel2.HeadingFont = null;
            this.panel2.HeadingLeftMargin = 0;
            this.panel2.HeadingText = null;
            this.panel2.HeadingTextColor = System.Drawing.Color.Black;
            this.panel2.HeadingTopMargin = 3;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(652, 70);
            this.panel2.TabIndex = 0;
            // 
            // treeListAct
            // 
            this.treeListAct.BackColor = System.Drawing.Color.Transparent;
            this.treeListAct.Border = ZoneFiveSoftware.Common.Visuals.ControlBorder.Style.SmallRoundShadow;
            this.treeListAct.CheckBoxes = true;
            this.treeListAct.DefaultIndent = 15;
            this.treeListAct.DefaultRowHeight = -1;
            this.treeListAct.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeListAct.HeaderRowHeight = 21;
            this.treeListAct.Location = new System.Drawing.Point(0, 0);
            this.treeListAct.MultiSelect = true;
            this.treeListAct.Name = "treeListAct";
            this.treeListAct.NumHeaderRows = ZoneFiveSoftware.Common.Visuals.TreeList.HeaderRows.Auto;
            this.treeListAct.NumLockedColumns = 0;
            this.treeListAct.RowAlternatingColors = true;
            this.treeListAct.RowHotlightColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(51)))), ((int)(((byte)(153)))), ((int)(((byte)(255)))));
            this.treeListAct.RowHotlightColorText = System.Drawing.SystemColors.HighlightText;
            this.treeListAct.RowHotlightMouse = true;
            this.treeListAct.RowSelectedColor = System.Drawing.SystemColors.Highlight;
            this.treeListAct.RowSelectedColorText = System.Drawing.SystemColors.HighlightText;
            this.treeListAct.RowSeparatorLines = true;
            this.treeListAct.ShowLines = false;
            this.treeListAct.ShowPlusMinus = false;
            this.treeListAct.Size = new System.Drawing.Size(652, 70);
            this.treeListAct.TabIndex = 0;
            // 
            // treeListContextMenuStrip
            // 
            this.treeListContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tableSettingsMenuItem,
            this.setRefTreeListMenuItem,
            this.visibleMenuItem,
            this.advancedMenuItem});
            this.treeListContextMenuStrip.Name = "treeListContextMenuStrip";
            this.treeListContextMenuStrip.Size = new System.Drawing.Size(184, 92);
            this.treeListContextMenuStrip.Opening += new System.ComponentModel.CancelEventHandler(this.treeListContextMenuStrip_Opening);
            // 
            // tableSettingsMenuItem
            // 
            this.tableSettingsMenuItem.Name = "tableSettingsMenuItem";
            this.tableSettingsMenuItem.Size = new System.Drawing.Size(183, 22);
            this.tableSettingsMenuItem.Text = "Table settings";
            this.tableSettingsMenuItem.Click += new System.EventHandler(this.tableSettingsMenuItem_Click);
            // 
            // setRefTreeListMenuItem
            // 
            this.setRefTreeListMenuItem.Name = "setRefTreeListMenuItem";
            this.setRefTreeListMenuItem.Size = new System.Drawing.Size(183, 22);
            this.setRefTreeListMenuItem.Text = "Set reference activity";
            this.setRefTreeListMenuItem.Click += new System.EventHandler(this.setRefActMenuItem_Click);
            // 
            // visibleMenuItem
            // 
            this.visibleMenuItem.DropDown = this.visibleMenuStrip;
            this.visibleMenuItem.Name = "visibleMenuItem";
            this.visibleMenuItem.Size = new System.Drawing.Size(183, 22);
            this.visibleMenuItem.Text = "Visible";
            // 
            // visibleMenuStrip
            // 
            this.visibleMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.allVisibleMenuItem,
            this.noneVisibleMenuItem});
            this.visibleMenuStrip.Name = "visibleMenuStrip";
            this.visibleMenuStrip.Size = new System.Drawing.Size(104, 48);
            // 
            // allVisibleMenuItem
            // 
            this.allVisibleMenuItem.Name = "allVisibleMenuItem";
            this.allVisibleMenuItem.Size = new System.Drawing.Size(103, 22);
            this.allVisibleMenuItem.Text = "All";
            this.allVisibleMenuItem.Click += new System.EventHandler(this.allVisibleMenuItem_Click);
            // 
            // noneVisibleMenuItem
            // 
            this.noneVisibleMenuItem.Name = "noneVisibleMenuItem";
            this.noneVisibleMenuItem.Size = new System.Drawing.Size(103, 22);
            this.noneVisibleMenuItem.Text = "None";
            this.noneVisibleMenuItem.Click += new System.EventHandler(this.noneVisibleMenuItem_Click);
            // 
            // advancedMenuItem
            // 
            this.advancedMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.limitActivityMenuItem,
            this.selectWithURMenuItem,
            this.setOffsetWithURMenuItem});
            this.advancedMenuItem.Name = "advancedMenuItem";
            this.advancedMenuItem.Size = new System.Drawing.Size(183, 22);
            this.advancedMenuItem.Text = "<Advanced>";
            // 
            // limitActivityMenuItem
            // 
            this.limitActivityMenuItem.Name = "limitActivityMenuItem";
            this.limitActivityMenuItem.Size = new System.Drawing.Size(288, 22);
            this.limitActivityMenuItem.Text = "<Limit selection to current activities...";
            this.limitActivityMenuItem.Click += new System.EventHandler(this.limitActivityMenuItem_Click);
            // 
            // selectWithURMenuItem
            // 
            this.selectWithURMenuItem.Name = "selectWithURMenuItem";
            this.selectWithURMenuItem.Size = new System.Drawing.Size(288, 22);
            this.selectWithURMenuItem.Text = "<Select with UR to current activities...";
            this.selectWithURMenuItem.Click += new System.EventHandler(this.selectWithURMenuItem_Click);
            // 
            // setOffsetWithURMenuItem
            // 
            this.setOffsetWithURMenuItem.Name = "setOffsetWithURMenuItem";
            this.setOffsetWithURMenuItem.Size = new System.Drawing.Size(288, 22);
            this.setOffsetWithURMenuItem.Text = "<Set offset with UR to current activities...";
            this.setOffsetWithURMenuItem.Click += new System.EventHandler(this.setOffsetWithURMenuItem_Click);
            // 
            // OverlayView
            // 
            this.AutoSize = true;
            this.Controls.Add(this.splitContainer4);
            this.Name = "OverlayView";
            this.Size = new System.Drawing.Size(652, 285);
            this.panel1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.actionBanner1.ResumeLayout(false);
            this.bannerContextMenuStrip.ResumeLayout(false);
            this.bannerXAxisMenuStrip.ResumeLayout(false);
            this.bannerShowContextMenuStrip.ResumeLayout(false);
            this.bannerShowDiffContextMenuStrip.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.chartBackgroundPanel.ResumeLayout(false);
            this.splitContainer4.Panel1.ResumeLayout(false);
            this.splitContainer4.Panel2.ResumeLayout(false);
            this.splitContainer4.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.treeListContextMenuStrip.ResumeLayout(false);
            this.visibleMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);

        }
        #endregion

        private ZoneFiveSoftware.Common.Visuals.Chart.LineChart chart;
        private System.Windows.Forms.Label labelXaxis;
        private System.Windows.Forms.Label labelYaxis;

        private System.Windows.Forms.Form popupForm;
        private ZoneFiveSoftware.Common.Visuals.Button btnSaveImage;

        private System.Windows.Forms.RadioButton useTime;
        private System.Windows.Forms.RadioButton useDistance;
        private System.Windows.Forms.CheckBox heartRate;
        private System.Windows.Forms.CheckBox pace;
        private System.Windows.Forms.CheckBox speed;
        private System.Windows.Forms.CheckBox power;
        private System.Windows.Forms.CheckBox cadence;
        private System.Windows.Forms.CheckBox elevation;
        private System.Windows.Forms.ToolTip toolTipMAbox;
        private System.Windows.Forms.CheckBox time;
        private System.Windows.Forms.CheckBox distance;
        private ZoneFiveSoftware.Common.Visuals.Panel panel1;
        private ZoneFiveSoftware.Common.Visuals.ActionBanner actionBanner1;
        private System.Windows.Forms.SplitContainer splitContainer4;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private ZoneFiveSoftware.Common.Visuals.Panel panel2;
        private ZoneFiveSoftware.Common.Visuals.TreeList treeListAct;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.ContextMenuStrip bannerContextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem showMeanMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showRollingAverageMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ContextMenuStrip treeListContextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem tableSettingsMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem setRefActMenuItem;
        private System.Windows.Forms.ToolStripMenuItem setRefTreeListMenuItem;
        private System.Windows.Forms.ToolStripMenuItem advancedMenuItem;
        private System.Windows.Forms.ToolStripMenuItem limitActivityMenuItem;
        private System.Windows.Forms.ToolStripMenuItem selectWithURMenuItem;
        private System.Windows.Forms.ToolStripMenuItem setOffsetWithURMenuItem;
        private System.Windows.Forms.ContextMenuStrip bannerShowContextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem showHRMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showPaceMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showSpeedMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showPowerMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showCadenceMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showElevationMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showTimeMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showDistanceMenuItem;
        private System.Windows.Forms.ContextMenuStrip bannerShowDiffContextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem showTimeDiffMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showDistDiffMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showHRDiffMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showDiffMenuItem;
        private System.Windows.Forms.Panel chartBackgroundPanel;
        private System.Windows.Forms.ToolStripMenuItem setRollAvgWidthMenuItem;
        private System.Windows.Forms.ToolStripMenuItem offsetMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showChartToolsMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showToolBarMenuItem;
        private ZoneFiveSoftware.Common.Visuals.Button expandButton;
        private System.Windows.Forms.ToolStripMenuItem xAxisMenuItem;
        private System.Windows.Forms.ContextMenuStrip bannerXAxisMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem xAxisDistanceMenuItem;
        private System.Windows.Forms.ToolStripMenuItem xAxisTimeMenuItem;
        private System.Windows.Forms.ToolStripMenuItem visibleMenuItem;
        private System.Windows.Forms.ContextMenuStrip visibleMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem allVisibleMenuItem;
        private System.Windows.Forms.ToolStripMenuItem noneVisibleMenuItem;
    }
}
