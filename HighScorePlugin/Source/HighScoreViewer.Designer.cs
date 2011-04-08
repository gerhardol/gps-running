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
    partial class HighScoreViewer
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
            this.summaryList = new ZoneFiveSoftware.Common.Visuals.TreeList();
            this.listMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.copyTableMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.label1 = new System.Windows.Forms.Label();
            this.boundsBox = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.imageBox = new System.Windows.Forms.ComboBox();
            this.domainBox = new System.Windows.Forms.ComboBox();
            this.paceBox = new System.Windows.Forms.ComboBox();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.label3 = new System.Windows.Forms.Label();
            this.viewBox = new System.Windows.Forms.ComboBox();
            this.chart = new ZoneFiveSoftware.Common.Visuals.Chart.ChartBase();
            this.Remarks = new System.Windows.Forms.Label();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.minGradeLbl = new System.Windows.Forms.Label();
            this.minGradeBox = new ZoneFiveSoftware.Common.Visuals.TextBox();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // summaryList
            // 
            this.summaryList.AutoScroll = true;
            this.summaryList.AutoSize = true;
            this.summaryList.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.summaryList.BackColor = System.Drawing.Color.Transparent;
            this.summaryList.Border = ZoneFiveSoftware.Common.Visuals.ControlBorder.Style.SmallRoundShadow;
            this.summaryList.CheckBoxes = false;
            this.summaryList.ContextMenuStrip = this.listMenu;
            this.summaryList.DefaultIndent = 15;
            this.summaryList.DefaultRowHeight = -1;
            this.summaryList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.summaryList.HeaderRowHeight = 21;
            this.summaryList.Location = new System.Drawing.Point(0, 0);
            this.summaryList.Margin = new System.Windows.Forms.Padding(0);
            this.summaryList.MultiSelect = false;
            this.summaryList.Name = "summaryList";
            this.summaryList.NumHeaderRows = ZoneFiveSoftware.Common.Visuals.TreeList.HeaderRows.Auto;
            this.summaryList.NumLockedColumns = 0;
            this.summaryList.RowAlternatingColors = true;
            this.summaryList.RowHotlightColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(51)))), ((int)(((byte)(153)))), ((int)(((byte)(255)))));
            this.summaryList.RowHotlightColorText = System.Drawing.SystemColors.HighlightText;
            this.summaryList.RowHotlightMouse = true;
            this.summaryList.RowSelectedColor = System.Drawing.SystemColors.Highlight;
            this.summaryList.RowSelectedColorText = System.Drawing.SystemColors.HighlightText;
            this.summaryList.RowSeparatorLines = true;
            this.summaryList.ShowLines = false;
            this.summaryList.ShowPlusMinus = false;
            this.summaryList.Size = new System.Drawing.Size(710, 27);
            this.summaryList.TabIndex = 11;
            this.summaryList.Click += new System.EventHandler(this.summaryList_Click);
            this.summaryList.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.selectedRow_DoubleClick);
            this.summaryList.MouseLeave += new System.EventHandler(this.summaryList_MouseLeave);
            this.summaryList.MouseMove += new System.Windows.Forms.MouseEventHandler(this.summaryList_MouseMove);
            // 
            // listMenu
            // 
            this.listMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.copyTableMenuItem});
            this.listMenu.Name = "listContextMenuStrip";
            this.listMenu.Size = new System.Drawing.Size(207, 26);
            // 
            // copyTableMenuItem
            // 
            this.copyTableMenuItem.Name = "copyTableMenuItem";
            this.copyTableMenuItem.Size = new System.Drawing.Size(206, 22);
            this.copyTableMenuItem.Text = "<Copy table to clipboard";
            this.copyTableMenuItem.Click += new System.EventHandler(HighScoreViewer.copyTableMenu_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(22, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(27, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Find";
            // 
            // boundsBox
            // 
            this.boundsBox.FormattingEnabled = true;
            this.boundsBox.Location = new System.Drawing.Point(55, 4);
            this.boundsBox.Name = "boundsBox";
            this.boundsBox.Size = new System.Drawing.Size(66, 21);
            this.boundsBox.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(196, 7);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "per specified";
            // 
            // imageBox
            // 
            this.imageBox.FormattingEnabled = true;
            this.imageBox.Location = new System.Drawing.Point(269, 4);
            this.imageBox.Name = "imageBox";
            this.imageBox.Size = new System.Drawing.Size(139, 21);
            this.imageBox.TabIndex = 7;
            // 
            // domainBox
            // 
            this.domainBox.FormattingEnabled = true;
            this.domainBox.Location = new System.Drawing.Point(127, 4);
            this.domainBox.Name = "domainBox";
            this.domainBox.Size = new System.Drawing.Size(63, 21);
            this.domainBox.TabIndex = 8;
            // 
            // minGradeLbl
            // 
            this.minGradeLbl.AutoSize = true;
            this.minGradeLbl.Location = new System.Drawing.Point(414, 7);
            this.minGradeLbl.Name = "minGradeLbl";
            this.minGradeLbl.Size = new System.Drawing.Size(50, 13);
            this.minGradeLbl.TabIndex = 18;
            this.minGradeLbl.Text = "<Grade>";
            // 
            // minGradeBox
            // 
            this.minGradeBox.Location = new System.Drawing.Point(456, 4);
            this.minGradeBox.Name = "minGradeBox";
            this.minGradeBox.Size = new System.Drawing.Size(50, 20);
            this.minGradeBox.TabIndex = 19;
            // 
            // paceBox
            // 
            this.paceBox.FormattingEnabled = true;
            this.paceBox.Location = new System.Drawing.Point(55, 27);
            this.paceBox.Name = "paceBox";
            this.paceBox.Size = new System.Drawing.Size(66, 21);
            this.paceBox.TabIndex = 10;
            // 
            // progressBar
            // 
            this.progressBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBar.Location = new System.Drawing.Point(6, 54);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(478, 23);
            this.progressBar.TabIndex = 12;
            this.progressBar.Visible = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(15, 30);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(34, 13);
            this.label3.TabIndex = 14;
            this.label3.Text = "Show";
            // 
            // viewBox
            // 
            this.viewBox.FormattingEnabled = true;
            this.viewBox.Location = new System.Drawing.Point(127, 27);
            this.viewBox.Name = "viewBox";
            this.viewBox.Size = new System.Drawing.Size(63, 21);
            this.viewBox.TabIndex = 15;
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
            this.chart.Size = new System.Drawing.Size(490, 65);
            this.chart.TabIndex = 16;
            // 
            // Remarks
            // 
            this.Remarks.AutoSize = true;
            this.Remarks.Location = new System.Drawing.Point(196, 30);
            this.Remarks.Name = "Remarks";
            this.Remarks.Size = new System.Drawing.Size(49, 13);
            this.Remarks.TabIndex = 17;
            this.Remarks.Text = "Remarks";
            // 
            // splitContainer1
            // 
            this.splitContainer1.BackColor = System.Drawing.Color.Transparent;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.minGradeBox);
            this.splitContainer1.Panel1.Controls.Add(this.minGradeLbl);
            this.splitContainer1.Panel1.Controls.Add(this.Remarks);
            this.splitContainer1.Panel1.Controls.Add(this.viewBox);
            this.splitContainer1.Panel1.Controls.Add(this.label3);
            this.splitContainer1.Panel1.Controls.Add(this.progressBar);
            this.splitContainer1.Panel1.Controls.Add(this.paceBox);
            this.splitContainer1.Panel1.Controls.Add(this.domainBox);
            this.splitContainer1.Panel1.Controls.Add(this.imageBox);
            this.splitContainer1.Panel1.Controls.Add(this.label2);
            this.splitContainer1.Panel1.Controls.Add(this.boundsBox);
            this.splitContainer1.Panel1.Controls.Add(this.label1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.AutoScroll = true;
            this.splitContainer1.Panel2.Controls.Add(this.summaryList);
            this.splitContainer1.Panel2.Controls.Add(this.chart);
            this.splitContainer1.Size = new System.Drawing.Size(490, 116);
            this.splitContainer1.SplitterWidth = 1;
            this.splitContainer1.TabIndex = 18;
            // 
            // HighScoreViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.AutoSize = true;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.splitContainer1);
            this.Name = "HighScoreViewer";
            this.Size = new System.Drawing.Size(490, 116);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private ZoneFiveSoftware.Common.Visuals.TreeList summaryList;
        private System.Windows.Forms.ContextMenuStrip listMenu;
        private System.Windows.Forms.ToolStripMenuItem copyTableMenuItem;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox boundsBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox imageBox;
        private System.Windows.Forms.ComboBox domainBox;
        private ZoneFiveSoftware.Common.Visuals.TextBox minGradeBox;
        private System.Windows.Forms.Label minGradeLbl;
        private System.Windows.Forms.ComboBox paceBox;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox viewBox;
        private ZoneFiveSoftware.Common.Visuals.Chart.ChartBase chart;
        private System.Windows.Forms.Label Remarks;
        private System.Windows.Forms.SplitContainer splitContainer1;
    }
}
