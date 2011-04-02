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
            this.panel1 = new System.Windows.Forms.Panel();
            this.listMenu = new System.Windows.Forms.ContextMenuStrip();
            this.copyTableMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.trainingTab = new System.Windows.Forms.TabPage();
            this.trainingLabel = new System.Windows.Forms.Label();
            this.trainingList = new ZoneFiveSoftware.Common.Visuals.TreeList();
            this.paceTempoTab = new System.Windows.Forms.TabPage();
            this.paceTempoLabel = new System.Windows.Forms.Label();
            this.paceTempoList = new ZoneFiveSoftware.Common.Visuals.TreeList();
            this.paceTempoLabel2 = new System.Windows.Forms.Label();
            this.intervalTab = new System.Windows.Forms.TabPage();
            this.intervalLabel = new System.Windows.Forms.Label();
            this.intervalList = new ZoneFiveSoftware.Common.Visuals.TreeList();
            this.temperatureTab = new System.Windows.Forms.TabPage();
            this.temperatureLabel2 = new System.Windows.Forms.Label();
            this.temperatureLabel = new System.Windows.Forms.Label();
            this.temperatureList = new ZoneFiveSoftware.Common.Visuals.TreeList();
            this.weightTab = new System.Windows.Forms.TabPage();
            this.weightLabel2 = new System.Windows.Forms.Label();
            this.weightLabel = new System.Windows.Forms.Label();
            this.weightList = new ZoneFiveSoftware.Common.Visuals.TreeList();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel5 = new System.Windows.Forms.TableLayoutPanel();
            this.panel1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.trainingTab.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            this.tableLayoutPanel5.SuspendLayout();
            this.paceTempoTab.SuspendLayout();
            this.intervalTab.SuspendLayout();
            this.temperatureTab.SuspendLayout();
            this.weightTab.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.AutoSize = true;
            this.panel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panel1.Controls.Add(this.tabControl1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(400, 300);
            this.panel1.TabIndex = 1;
            // 
            // listMenu
            // 
            this.listMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.copyTableMenuItem});
            this.listMenu.Name = "listContextMenuStrip";
            this.listMenu.Size = new System.Drawing.Size(199, 48);
            // 
            // copyTableMenuItem
            // 
            this.copyTableMenuItem.Name = "copyTableMenuItem";
            this.copyTableMenuItem.Size = new System.Drawing.Size(198, 22);
            this.copyTableMenuItem.Text = "<Copy table to clipboard";
            this.copyTableMenuItem.Click += new System.EventHandler(this.copyTableMenu_Click);
            // 
            // tabControl1
            //
            this.tabControl1.Controls.Add(this.trainingTab);
            this.tabControl1.Controls.Add(this.paceTempoTab);
            this.tabControl1.Controls.Add(this.intervalTab);
            this.tabControl1.Controls.Add(this.temperatureTab);
            this.tabControl1.Controls.Add(this.weightTab);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.DrawMode = System.Windows.Forms.TabDrawMode.OwnerDrawFixed;
            this.tabControl1.HotTrack = true;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.Padding = new System.Drawing.Point(0, 0);
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(242, 137);
            this.tabControl1.TabIndex = 0;
            this.tabControl1.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.tabControl1_DrawItem);
            // 
            // trainingTab
            // 
            this.trainingTab.AutoScroll = true;
            this.trainingTab.Controls.Add(this.tableLayoutPanel1);
            this.trainingTab.Location = new System.Drawing.Point(4, 22);
            this.trainingTab.Name = "trainingTab";
            this.trainingTab.Padding = new System.Windows.Forms.Padding(3);
            this.trainingTab.Size = new System.Drawing.Size(234, 111);
            this.trainingTab.TabIndex = 0;
            this.trainingTab.Text = "Training";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.trainingLabel, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.trainingList, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(652, 211);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // trainingLabel
            // 
            this.trainingLabel.AutoSize = true;
            this.trainingLabel.Location = new System.Drawing.Point(3, 16);
            this.trainingLabel.Name = "trainingLabel";
            this.trainingLabel.Size = new System.Drawing.Size(50, 13);
            this.trainingLabel.TabIndex = 1;
            this.trainingLabel.Text = "VO2 max";
            // 
            // trainingList
            // 
            this.trainingList.AutoScroll = true;
            this.trainingList.AutoSize = true;
            this.trainingList.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.trainingList.ContextMenuStrip = this.listMenu;
            this.trainingList.Dock = System.Windows.Forms.DockStyle.Fill;
            // 
            // paceTempoTab
            // 
            this.paceTempoTab.AutoScroll = true;
            this.paceTempoTab.Controls.Add(this.tableLayoutPanel2);
            this.paceTempoTab.Location = new System.Drawing.Point(4, 22);
            this.paceTempoTab.Name = "paceTempoTab";
            this.paceTempoTab.Padding = new System.Windows.Forms.Padding(3);
            this.paceTempoTab.Size = new System.Drawing.Size(235, 111);
            this.paceTempoTab.TabIndex = 1;
            this.paceTempoTab.Text = "Pace for tempo runs";
            this.paceTempoTab.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Controls.Add(this.paceTempoLabel2, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.paceTempoLabel, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.paceTempoList, 0, 1);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.Size = new System.Drawing.Size(652, 211);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // paceTempoLabel
            // 
            this.paceTempoLabel.AutoSize = true;
            this.paceTempoLabel.Location = new System.Drawing.Point(3, 3);
            this.paceTempoLabel.Name = "paceTempoLabel";
            this.paceTempoLabel.Size = new System.Drawing.Size(390, 26);
            this.paceTempoLabel.TabIndex = 0;
            this.paceTempoLabel.Text = "Pace for tempo runs of 20 to 60 minutes at VDOT {0:0.0}. 20 min run is at lactate" +
                " \r\nthreshold pace - pace of longer runs adjusted to maintain proper intensity.";
            // 
            // paceTempoLabel2
            // 
            this.paceTempoLabel2.AutoSize = true;
            this.paceTempoLabel2.Location = new System.Drawing.Point(3, 2);
            this.paceTempoLabel2.Name = "label1";
            this.paceTempoLabel2.Size = new System.Drawing.Size(368, 13);
            this.paceTempoLabel2.TabIndex = 1;
            this.paceTempoLabel2.Text = "20 min run is at lactate threshold pace - pace of longer runs adjusted to >>>>";
            this.paceTempoLabel2.Visible = true;
            // 
            // paceTempoList
            // 
            this.paceTempoList.AutoScroll = true;
            this.paceTempoList.AutoSize = true;
            this.paceTempoList.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.paceTempoList.ContextMenuStrip = this.listMenu;
            this.paceTempoList.Dock = System.Windows.Forms.DockStyle.Fill;
            // 
            // intervalTab
            // 
            this.intervalTab.AutoScroll = true;
            this.intervalTab.Controls.Add(this.tableLayoutPanel3);
            this.intervalTab.Location = new System.Drawing.Point(4, 22);
            this.intervalTab.Name = "intervalTab";
            this.intervalTab.Size = new System.Drawing.Size(235, 111);
            this.intervalTab.TabIndex = 2;
            this.intervalTab.Text = "Interval split times";
            this.intervalTab.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 1;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Controls.Add(this.intervalLabel, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.intervalList, 0, 1);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 2;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel3.Size = new System.Drawing.Size(652, 211);
            this.tableLayoutPanel3.TabIndex = 0;
            // 
            // intervalLabel
            // 
            this.intervalLabel.AutoSize = true;
            this.intervalLabel.Location = new System.Drawing.Point(3, 16);
            this.intervalLabel.Name = "intervalLabel2";
            this.intervalLabel.Size = new System.Drawing.Size(148, 13);
            this.intervalLabel.TabIndex = 5;
            this.intervalLabel.Text = "Suggested short interval pace";
            // 
            // intervalList
            // 
            this.intervalList.AutoScroll = true;
            this.intervalList.AutoSize = true;
            this.intervalList.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.intervalList.ContextMenuStrip = this.listMenu;
            this.intervalList.Dock = System.Windows.Forms.DockStyle.Fill;
            // 
            // temperatureTab
            // 
            this.temperatureTab.AutoScroll = true;
            this.temperatureTab.Controls.Add(this.tableLayoutPanel4);
            this.temperatureTab.Location = new System.Drawing.Point(4, 22);
            this.temperatureTab.Name = "temperatureTab";
            this.temperatureTab.Size = new System.Drawing.Size(235, 111);
            this.temperatureTab.TabIndex = 3;
            this.temperatureTab.Text = "Temperature impact";
            this.temperatureTab.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.ColumnCount = 1;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.Controls.Add(this.temperatureLabel2, 0, 0);
            this.tableLayoutPanel4.Controls.Add(this.temperatureLabel, 0, 0);
            this.tableLayoutPanel4.Controls.Add(this.temperatureList, 0, 1);
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 2;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel4.Size = new System.Drawing.Size(652, 211);
            this.tableLayoutPanel4.TabIndex = 0;
            // 
            // label2
            // 
            this.temperatureLabel2.AutoSize = true;
            this.temperatureLabel2.Location = new System.Drawing.Point(3, 3);
            this.temperatureLabel2.Name = "label2";
            this.temperatureLabel2.Size = new System.Drawing.Size(266, 13);
            this.temperatureLabel2.TabIndex = 4;
            this.temperatureLabel2.Text = "Performance is not adversely affected at 16° C or lower";
            // 
            // temperatureLabel
            // 
            this.temperatureLabel.AutoSize = true;
            this.temperatureLabel.Location = new System.Drawing.Point(3, 2);
            this.temperatureLabel.Name = "temperatureLabel";
            this.temperatureLabel.Size = new System.Drawing.Size(89, 13);
            this.temperatureLabel.TabIndex = 3;
            this.temperatureLabel.Text = "temperatureLabel";
            // 
            // temperatureList
            // 
            this.temperatureList.AutoScroll = true;
            this.temperatureList.AutoSize = true;
            this.temperatureList.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.temperatureList.ContextMenuStrip = this.listMenu;
            this.temperatureList.Dock = System.Windows.Forms.DockStyle.Fill;
            // 
            // weightTab
            // 
            this.weightTab.AutoScroll = true;
            this.weightTab.Controls.Add(this.tableLayoutPanel5);
            this.weightTab.Location = new System.Drawing.Point(4, 22);
            this.weightTab.Name = "weightTab";
            this.weightTab.Size = new System.Drawing.Size(235, 111);
            this.weightTab.TabIndex = 4;
            this.weightTab.Text = "Weight impact";
            this.weightTab.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel5
            // 
            this.tableLayoutPanel5.ColumnCount = 1;
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel5.Controls.Add(this.weightLabel2, 0, 0);
            this.tableLayoutPanel5.Controls.Add(this.weightLabel, 0, 0);
            this.tableLayoutPanel5.Controls.Add(this.weightList, 0, 1);
            this.tableLayoutPanel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel5.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel5.Name = "tableLayoutPanel5";
            this.tableLayoutPanel5.RowCount = 2;
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel5.Size = new System.Drawing.Size(652, 211);
            this.tableLayoutPanel5.TabIndex = 0;
            // 
            // weightLabel2
            // 
            this.weightLabel2.AutoSize = true;
            this.weightLabel2.Location = new System.Drawing.Point(3, 2);
            this.weightLabel2.Name = "weightLabel2";
            this.weightLabel2.Size = new System.Drawing.Size(259, 13);
            this.weightLabel2.TabIndex = 7;
            this.weightLabel2.Text = "Estimated times and paces are +/- 2 seconds per mile";
            // 
            // weightLabel
            // 
            this.weightLabel.AutoSize = true;
            this.weightLabel.Location = new System.Drawing.Point(3, 3);
            this.weightLabel.Name = "weightLabel";
            this.weightLabel.Size = new System.Drawing.Size(92, 13);
            this.weightLabel.TabIndex = 6;
            this.weightLabel.Text = "estimated weight>";
            // 
            // weightList
            // 
            this.weightList.AutoScroll = true;
            this.weightList.AutoSize = true;
            this.weightList.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.weightList.ContextMenuStrip = this.listMenu;
            this.weightList.Dock = System.Windows.Forms.DockStyle.Fill;
            // 
            // TrainingView
            //
            this.AutoScroll = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.Controls.Add(this.panel1);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "TrainingView";
            this.Size = new System.Drawing.Size(242, 137);
            this.tabControl1.ResumeLayout(false);
            this.trainingTab.ResumeLayout(false);
            this.trainingTab.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel4.ResumeLayout(false);
            this.tableLayoutPanel5.ResumeLayout(false);
            this.paceTempoTab.ResumeLayout(false);
            this.paceTempoTab.PerformLayout();
            this.intervalTab.ResumeLayout(false);
            this.intervalTab.PerformLayout();
            this.temperatureTab.ResumeLayout(false);
            this.temperatureTab.PerformLayout();
            this.weightTab.ResumeLayout(false);
            this.weightTab.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ContextMenuStrip listMenu;
        private System.Windows.Forms.ToolStripMenuItem copyTableMenuItem;

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage trainingTab;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label trainingLabel;
        private ZoneFiveSoftware.Common.Visuals.TreeList trainingList;

        private System.Windows.Forms.TabPage paceTempoTab;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Label paceTempoLabel;
        private System.Windows.Forms.Label paceTempoLabel2;
        private ZoneFiveSoftware.Common.Visuals.TreeList paceTempoList;

        private System.Windows.Forms.TabPage intervalTab;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.Label intervalLabel;
        private ZoneFiveSoftware.Common.Visuals.TreeList intervalList;
        
        private System.Windows.Forms.TabPage temperatureTab;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.Label temperatureLabel;
        private System.Windows.Forms.Label temperatureLabel2;
        private ZoneFiveSoftware.Common.Visuals.TreeList temperatureList;
        
        private System.Windows.Forms.TabPage weightTab;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel5;
        private System.Windows.Forms.Label weightLabel2;
        private System.Windows.Forms.Label weightLabel;
        private ZoneFiveSoftware.Common.Visuals.TreeList weightList;

    }
}
