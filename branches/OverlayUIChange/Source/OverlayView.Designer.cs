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
            this.chart = new ZoneFiveSoftware.Common.Visuals.Chart.LineChart();
            this.labelActivity = new System.Windows.Forms.Label();
            this.heartRate = new System.Windows.Forms.CheckBox();
            this.pace = new System.Windows.Forms.CheckBox();
            this.speed = new System.Windows.Forms.CheckBox();
            this.useTime = new System.Windows.Forms.RadioButton();
            this.labelXaxis = new System.Windows.Forms.Label();
            this.useDistance = new System.Windows.Forms.RadioButton();
            this.labelYaxis = new System.Windows.Forms.Label();
            this.panelAct = new System.Windows.Forms.Panel();
            this.power = new System.Windows.Forms.CheckBox();
            this.cadence = new System.Windows.Forms.CheckBox();
            this.elevation = new System.Windows.Forms.CheckBox();
            this.categoryAverage = new System.Windows.Forms.CheckBox();
            this.movingAverage = new System.Windows.Forms.CheckBox();
            this.maBox = new ZoneFiveSoftware.Common.Visuals.TextBox();
            this.toolTipMAbox = new System.Windows.Forms.ToolTip(this.components);
            this.labelAOP = new System.Windows.Forms.Label();
            this.btnSaveImage = new ZoneFiveSoftware.Common.Visuals.Button();
            this.distance = new System.Windows.Forms.CheckBox();
            this.time = new System.Windows.Forms.CheckBox();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.splitContainer3 = new System.Windows.Forms.SplitContainer();
            this.panel1 = new ZoneFiveSoftware.Common.Visuals.Panel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.actionBanner1 = new ZoneFiveSoftware.Common.Visuals.ActionBanner();
            this.panel3 = new System.Windows.Forms.Panel();
            this.splitContainer4 = new System.Windows.Forms.SplitContainer();
            this.panel2 = new ZoneFiveSoftware.Common.Visuals.Panel();
            this.treeListAct = new ZoneFiveSoftware.Common.Visuals.TreeList();
            this.bannerContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.showMeanMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showRollingAverageMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.splitContainer3.Panel1.SuspendLayout();
            this.splitContainer3.Panel2.SuspendLayout();
            this.splitContainer3.SuspendLayout();
            this.panel1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.splitContainer4.Panel1.SuspendLayout();
            this.splitContainer4.Panel2.SuspendLayout();
            this.splitContainer4.SuspendLayout();
            this.panel2.SuspendLayout();
            this.bannerContextMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // chart
            // 
            this.chart.AutoScroll = true;
            this.chart.BackColor = System.Drawing.Color.White;
            this.chart.Border = ZoneFiveSoftware.Common.Visuals.ControlBorder.Style.SmallRoundShadow;
            this.chart.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chart.Location = new System.Drawing.Point(0, 0);
            this.chart.Margin = new System.Windows.Forms.Padding(0);
            this.chart.Name = "chart";
            this.chart.Padding = new System.Windows.Forms.Padding(5);
            this.chart.Size = new System.Drawing.Size(506, 135);
            this.chart.TabIndex = 12;
            // 
            // labelActivity
            // 
            this.labelActivity.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelActivity.AutoSize = true;
            this.labelActivity.Location = new System.Drawing.Point(3, 83);
            this.labelActivity.Name = "labelActivity";
            this.labelActivity.Size = new System.Drawing.Size(49, 13);
            this.labelActivity.TabIndex = 3;
            this.labelActivity.Text = "Activities";
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
            // panelAct
            // 
            this.panelAct.AutoScroll = true;
            this.panelAct.AutoSize = true;
            this.panelAct.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelAct.Location = new System.Drawing.Point(0, 0);
            this.panelAct.Name = "panelAct";
            this.panelAct.Size = new System.Drawing.Size(139, 36);
            this.panelAct.TabIndex = 12;
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
            // categoryAverage
            // 
            this.categoryAverage.AutoSize = true;
            this.categoryAverage.Location = new System.Drawing.Point(0, 3);
            this.categoryAverage.Name = "categoryAverage";
            this.categoryAverage.Size = new System.Drawing.Size(139, 17);
            this.categoryAverage.TabIndex = 9;
            this.categoryAverage.Text = global::GpsRunningPlugin.Properties.Resources.BCA;
            this.categoryAverage.UseVisualStyleBackColor = true;
            this.categoryAverage.CheckedChanged += new System.EventHandler(this.average_CheckedChanged);
            // 
            // movingAverage
            // 
            this.movingAverage.AutoSize = true;
            this.movingAverage.Location = new System.Drawing.Point(0, 26);
            this.movingAverage.Name = "movingAverage";
            this.movingAverage.Size = new System.Drawing.Size(126, 17);
            this.movingAverage.TabIndex = 10;
            this.movingAverage.Text = global::GpsRunningPlugin.Properties.Resources.BMA;
            this.movingAverage.UseVisualStyleBackColor = true;
            this.movingAverage.CheckedChanged += new System.EventHandler(this.movingAverage_CheckedChanged);
            // 
            // maBox
            // 
            this.maBox.AcceptsReturn = false;
            this.maBox.AcceptsTab = false;
            this.maBox.BackColor = System.Drawing.Color.White;
            this.maBox.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(123)))), ((int)(((byte)(114)))), ((int)(((byte)(108)))));
            this.maBox.ButtonImage = null;
            this.maBox.Location = new System.Drawing.Point(20, 62);
            this.maBox.MaxLength = 32767;
            this.maBox.Multiline = false;
            this.maBox.Name = "maBox";
            this.maBox.ReadOnly = false;
            this.maBox.ReadOnlyColor = System.Drawing.SystemColors.Control;
            this.maBox.ReadOnlyTextColor = System.Drawing.SystemColors.ControlLight;
            this.maBox.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.maBox.Size = new System.Drawing.Size(60, 20);
            this.maBox.TabIndex = 11;
            this.maBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            // 
            // labelAOP
            // 
            this.labelAOP.AutoSize = true;
            this.labelAOP.Location = new System.Drawing.Point(17, 46);
            this.labelAOP.Name = "labelAOP";
            this.labelAOP.Size = new System.Drawing.Size(103, 13);
            this.labelAOP.TabIndex = 21;
            this.labelAOP.Text = "Average over period";
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
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer2.Location = new System.Drawing.Point(3, 73);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.splitContainer3);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.BackColor = System.Drawing.Color.Transparent;
            this.splitContainer2.Panel2.Controls.Add(this.chart);
            this.splitContainer2.Size = new System.Drawing.Size(646, 135);
            this.splitContainer2.SplitterDistance = 139;
            this.splitContainer2.SplitterWidth = 1;
            this.splitContainer2.TabIndex = 0;
            // 
            // splitContainer3
            // 
            this.splitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer3.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer3.Location = new System.Drawing.Point(0, 0);
            this.splitContainer3.Name = "splitContainer3";
            this.splitContainer3.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer3.Panel1
            // 
            this.splitContainer3.Panel1.Controls.Add(this.maBox);
            this.splitContainer3.Panel1.Controls.Add(this.movingAverage);
            this.splitContainer3.Panel1.Controls.Add(this.categoryAverage);
            this.splitContainer3.Panel1.Controls.Add(this.labelAOP);
            this.splitContainer3.Panel1.Controls.Add(this.labelActivity);
            // 
            // splitContainer3.Panel2
            // 
            this.splitContainer3.Panel2.Controls.Add(this.panelAct);
            this.splitContainer3.Size = new System.Drawing.Size(139, 135);
            this.splitContainer3.SplitterDistance = 98;
            this.splitContainer3.SplitterWidth = 1;
            this.splitContainer3.TabIndex = 0;
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
            this.tableLayoutPanel1.Controls.Add(this.splitContainer2, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.actionBanner1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel3, 0, 1);
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
            this.treeListAct.CheckBoxes = false;
            this.treeListAct.DefaultIndent = 15;
            this.treeListAct.DefaultRowHeight = -1;
            this.treeListAct.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeListAct.HeaderRowHeight = 21;
            this.treeListAct.Location = new System.Drawing.Point(0, 0);
            this.treeListAct.MultiSelect = false;
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
            // bannerContextMenuStrip
            // 
            this.bannerContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.showMeanMenuItem,
            this.showRollingAverageMenuItem});
            this.bannerContextMenuStrip.Name = "bannerContextMenuStrip";
            this.bannerContextMenuStrip.Size = new System.Drawing.Size(158, 70);
            this.bannerContextMenuStrip.Opening += new System.ComponentModel.CancelEventHandler(this.bannerContextMenuStrip_Opening);
            // 
            // showMeanMenuItem
            // 
            this.showMeanMenuItem.Name = "showMeanMenuItem";
            this.showMeanMenuItem.Size = new System.Drawing.Size(157, 22);
            this.showMeanMenuItem.Text = "Mean";
            this.showMeanMenuItem.Click += new System.EventHandler(this.ShowMeanMenuItem_Click);
            // 
            // showRollingAverageMenuItem
            // 
            this.showRollingAverageMenuItem.Name = "showRollingAverageMenuItem";
            this.showRollingAverageMenuItem.Size = new System.Drawing.Size(157, 22);
            this.showRollingAverageMenuItem.Text = "Rolling Average";
            this.showRollingAverageMenuItem.Click += new System.EventHandler(this.rollingAverageToolStripMenuItem_Click);
            // 
            // OverlayView
            // 
            this.AutoSize = true;
            this.Controls.Add(this.splitContainer4);
            this.Name = "OverlayView";
            this.Size = new System.Drawing.Size(652, 285);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.ResumeLayout(false);
            this.splitContainer3.Panel1.ResumeLayout(false);
            this.splitContainer3.Panel1.PerformLayout();
            this.splitContainer3.Panel2.ResumeLayout(false);
            this.splitContainer3.Panel2.PerformLayout();
            this.splitContainer3.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.splitContainer4.Panel1.ResumeLayout(false);
            this.splitContainer4.Panel2.ResumeLayout(false);
            this.splitContainer4.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.bannerContextMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);

        }
        #endregion

        private ZoneFiveSoftware.Common.Visuals.Chart.LineChart chart;
        private System.Windows.Forms.Label labelXaxis;
        private System.Windows.Forms.Label labelYaxis;

        private System.Windows.Forms.Form popupForm;
        private System.Windows.Forms.Label labelAOP;
        private ZoneFiveSoftware.Common.Visuals.Button btnSaveImage;
        private System.Windows.Forms.Panel panelAct;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.SplitContainer splitContainer3;

        private System.Windows.Forms.RadioButton useTime;
        private System.Windows.Forms.RadioButton useDistance;
        private System.Windows.Forms.CheckBox heartRate;
        private System.Windows.Forms.CheckBox pace;
        private System.Windows.Forms.CheckBox speed;
        private System.Windows.Forms.CheckBox power;
        private System.Windows.Forms.CheckBox cadence;
        private System.Windows.Forms.CheckBox elevation;

        private System.Windows.Forms.CheckBox categoryAverage;
        private System.Windows.Forms.CheckBox movingAverage;
        private ZoneFiveSoftware.Common.Visuals.TextBox maBox;
        private System.Windows.Forms.ToolTip toolTipMAbox;
        private System.Windows.Forms.Label labelActivity;
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
    }
}
