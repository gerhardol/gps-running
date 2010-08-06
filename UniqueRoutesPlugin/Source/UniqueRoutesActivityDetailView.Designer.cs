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
    partial class UniqueRoutesActivityDetailView
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
            this.summaryLabel = new System.Windows.Forms.Label();
            this.summaryList = new ZoneFiveSoftware.Common.Visuals.TreeList();
            this.contextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.copyTable = new System.Windows.Forms.ToolStripMenuItem();
            this.listSettingsMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.activeMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sendToMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ctxMenuItemRefActivity = new System.Windows.Forms.ToolStripMenuItem();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.btnDoIt = new ZoneFiveSoftware.Common.Visuals.Button();
            this.speedBox = new System.Windows.Forms.ComboBox();
            this.sendResultToLabel1 = new System.Windows.Forms.Label();
            this.pluginBox = new System.Windows.Forms.ComboBox();
            this.selectedBox = new System.Windows.Forms.ComboBox();
            this.sendLabel2 = new System.Windows.Forms.Label();
            this.labelShow = new System.Windows.Forms.Label();
            this.categoryLabel = new System.Windows.Forms.Label();
            this.btnChangeCategory = new ZoneFiveSoftware.Common.Visuals.Button();
            this.activeBox = new System.Windows.Forms.ComboBox();
            this.labelLaps = new System.Windows.Forms.Label();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.contextMenu.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // summaryLabel
            // 
            this.summaryLabel.AutoSize = true;
            this.summaryLabel.Location = new System.Drawing.Point(4, 4);
            this.summaryLabel.Name = "summaryLabel";
            this.summaryLabel.Size = new System.Drawing.Size(166, 13);
            this.summaryLabel.TabIndex = 0;
            this.summaryLabel.Text = "Found nn activities on same route";
            // 
            // summaryList
            // 
            this.summaryList.AutoScroll = true;
            this.summaryList.AutoSize = true;
            this.summaryList.BackColor = System.Drawing.Color.Transparent;
            this.summaryList.Border = ZoneFiveSoftware.Common.Visuals.ControlBorder.Style.SmallRoundShadow;
            this.summaryList.CheckBoxes = false;
            this.summaryList.DefaultIndent = 15;
            this.summaryList.DefaultRowHeight = -1;
            this.summaryList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.summaryList.HeaderRowHeight = 21;
            this.summaryList.Location = new System.Drawing.Point(0, 0);
            this.summaryList.MultiSelect = true;
            this.summaryList.Name = "summaryList";
            this.summaryList.NumHeaderRows = ZoneFiveSoftware.Common.Visuals.TreeList.HeaderRows.One;
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
            this.summaryList.Size = new System.Drawing.Size(391, 200);
            this.summaryList.TabIndex = 1;
            this.summaryList.ContextMenuStrip = contextMenu;
            this.summaryList.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(selectedRow_DoubleClick);
            this.summaryList.MouseMove += new System.Windows.Forms.MouseEventHandler(summaryList_MouseMove);
            this.summaryList.MouseLeave += new System.EventHandler(summaryList_MouseLeave);
            this.summaryListToolTipTimer.Tick += new System.EventHandler(ToolTipTimer_Tick);
            // 
            // contextMenu
            // 
            this.contextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.copyTable,
            this.listSettingsMenuItem,
            this.activeMenuItem,
            this.sendToMenuItem,
            this.ctxMenuItemRefActivity});
            this.contextMenu.Name = "contextMenu";
            this.contextMenu.Size = new System.Drawing.Size(199, 92);
            // 
            // copyTable
            // 
            this.copyTable.Name = "copyTable";
            this.copyTable.Size = new System.Drawing.Size(198, 22);
            this.copyTable.Text = "Copy table to clipboard";
            this.copyTable.Click += new System.EventHandler(this.copyTableMenu_Click);
            // 
            // listSettingsMenuItem
            // 
            this.listSettingsMenuItem.Name = "listSettingsMenuItem";
            this.listSettingsMenuItem.Size = new System.Drawing.Size(198, 22);
            this.listSettingsMenuItem.Text = "List Settings...";
            // 
            // activeMenuItem
            // 
            this.activeMenuItem.Name = "activeMenuItem";
            this.activeMenuItem.Size = new System.Drawing.Size(198, 22);
            this.activeMenuItem.Text = "Only active laps";
            this.activeMenuItem.Click += new System.EventHandler(this.activeMenuItem_Click);
            // 
            // sendToMenuItem
            // 
            this.sendToMenuItem.Name = "sendToMenuItem";
            this.sendToMenuItem.Size = new System.Drawing.Size(198, 22);
            this.sendToMenuItem.Text = "Send to";
            // 
            // ctxMenuItemRefActivity
            // 
            this.ctxMenuItemRefActivity.Name = "ctxMenuItemRefActivity";
            this.ctxMenuItemRefActivity.Size = new System.Drawing.Size(124, 22);
            this.ctxMenuItemRefActivity.Text = "<A&ctivit>";
            // 
            // progressBar
            // 
            this.progressBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBar.Location = new System.Drawing.Point(0, 0);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(391, 24);
            this.progressBar.TabIndex = 2;
            this.progressBar.Visible = false;
            // 
            // btnDoIt
            // 
            this.btnDoIt.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDoIt.BackColor = System.Drawing.Color.Transparent;
            this.btnDoIt.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(40)))), ((int)(((byte)(50)))), ((int)(((byte)(120)))));
            this.btnDoIt.CenterImage = null;
            this.btnDoIt.ContextMenuStrip = this.contextMenu;
            this.btnDoIt.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnDoIt.HyperlinkStyle = false;
            this.btnDoIt.ImageMargin = 2;
            this.btnDoIt.LeftImage = null;
            this.btnDoIt.Location = new System.Drawing.Point(368, 4);
            this.btnDoIt.Name = "btnDoIt";
            this.btnDoIt.PushStyle = true;
            this.btnDoIt.RightImage = null;
            this.btnDoIt.Size = new System.Drawing.Size(20, 21);
            this.btnDoIt.TabIndex = 3;
            this.btnDoIt.Text = "Do it!";
            this.btnDoIt.TextAlign = System.Drawing.StringAlignment.Center;
            this.btnDoIt.TextLeftMargin = 2;
            this.btnDoIt.TextRightMargin = 2;
            this.btnDoIt.Click += new System.EventHandler(this.refreshButton_Click);
            // 
            // speedBox
            // 
            this.speedBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.speedBox.FormattingEnabled = true;
            this.speedBox.Location = new System.Drawing.Point(178, 61);
            this.speedBox.Name = "speedBox";
            this.speedBox.Size = new System.Drawing.Size(68, 21);
            this.speedBox.TabIndex = 5;
            this.speedBox.Visible = false;
            this.speedBox.SelectedIndexChanged += new System.EventHandler(this.speedBox_SelectedIndexChanged);
            // 
            // sendResultToLabel1
            // 
            this.sendResultToLabel1.AutoSize = true;
            this.sendResultToLabel1.Location = new System.Drawing.Point(13, 37);
            this.sendResultToLabel1.Name = "sendResultToLabel1";
            this.sendResultToLabel1.Size = new System.Drawing.Size(32, 13);
            this.sendResultToLabel1.TabIndex = 6;
            this.sendResultToLabel1.Text = "Send";
            // 
            // pluginBox
            // 
            this.pluginBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.pluginBox.FormattingEnabled = true;
            this.pluginBox.Location = new System.Drawing.Point(191, 34);
            this.pluginBox.Name = "pluginBox";
            this.pluginBox.Size = new System.Drawing.Size(134, 21);
            this.pluginBox.TabIndex = 7;
            this.pluginBox.SelectedIndexChanged += new System.EventHandler(this.pluginBox_SelectedIndexChanged);
            // 
            // selectedBox
            // 
            this.selectedBox.DisplayMember = "all";
            this.selectedBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.selectedBox.FormattingEnabled = true;
            this.selectedBox.Location = new System.Drawing.Point(51, 34);
            this.selectedBox.Name = "selectedBox";
            this.selectedBox.Size = new System.Drawing.Size(68, 21);
            this.selectedBox.TabIndex = 8;
            this.selectedBox.SelectedIndexChanged += new System.EventHandler(this.selectedBox_SelectedIndexChanged);
            // 
            // sendLabel2
            // 
            this.sendLabel2.AutoSize = true;
            this.sendLabel2.Location = new System.Drawing.Point(125, 39);
            this.sendLabel2.Name = "sendLabel2";
            this.sendLabel2.Size = new System.Drawing.Size(60, 13);
            this.sendLabel2.TabIndex = 9;
            this.sendLabel2.Text = "activities to";
            // 
            // labelShow
            // 
            this.labelShow.AutoSize = true;
            this.labelShow.Location = new System.Drawing.Point(13, 64);
            this.labelShow.Name = "labelShow";
            this.labelShow.Size = new System.Drawing.Size(34, 13);
            this.labelShow.TabIndex = 10;
            this.labelShow.Text = "Show";
            this.labelShow.Visible = false;
            // 
            // categoryLabel
            // 
            this.categoryLabel.AutoSize = true;
            this.categoryLabel.Location = new System.Drawing.Point(269, 4);
            this.categoryLabel.Name = "categoryLabel";
            this.categoryLabel.Size = new System.Drawing.Size(97, 13);
            this.categoryLabel.TabIndex = 11;
            this.categoryLabel.Text = "Include activities in";
            // 
            // btnChangeCategory
            // 
            this.btnChangeCategory.BackColor = System.Drawing.Color.Transparent;
            this.btnChangeCategory.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(40)))), ((int)(((byte)(50)))), ((int)(((byte)(120)))));
            this.btnChangeCategory.CenterImage = null;
            this.btnChangeCategory.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnChangeCategory.HyperlinkStyle = false;
            this.btnChangeCategory.ImageMargin = 2;
            this.btnChangeCategory.LeftImage = null;
            this.btnChangeCategory.Location = new System.Drawing.Point(166, 0);
            this.btnChangeCategory.Name = "btnChangeCategory";
            this.btnChangeCategory.PushStyle = true;
            this.btnChangeCategory.RightImage = null;
            this.btnChangeCategory.Size = new System.Drawing.Size(97, 23);
            this.btnChangeCategory.TabIndex = 13;
            this.btnChangeCategory.Text = "Change category";
            this.btnChangeCategory.TextAlign = System.Drawing.StringAlignment.Center;
            this.btnChangeCategory.TextLeftMargin = 2;
            this.btnChangeCategory.TextRightMargin = 2;
            this.btnChangeCategory.Click += new System.EventHandler(this.changeCategory_Click);
            // 
            // activeBox
            // 
            this.activeBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.activeBox.FormattingEnabled = true;
            this.activeBox.Location = new System.Drawing.Point(51, 61);
            this.activeBox.Name = "activeBox";
            this.activeBox.Size = new System.Drawing.Size(68, 21);
            this.activeBox.TabIndex = 14;
            this.activeBox.Visible = false;
            this.activeBox.SelectedIndexChanged += new System.EventHandler(this.activeBox_SelectedIndexChanged);
            // 
            // labelLaps
            // 
            this.labelLaps.AutoSize = true;
            this.labelLaps.Location = new System.Drawing.Point(125, 64);
            this.labelLaps.Name = "labelLaps";
            this.labelLaps.Size = new System.Drawing.Size(30, 13);
            this.labelLaps.TabIndex = 15;
            this.labelLaps.Text = "Laps";
            this.labelLaps.Visible = false;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.progressBar);
            this.splitContainer1.Panel1.Controls.Add(this.btnDoIt);
            this.splitContainer1.Panel1.Controls.Add(this.summaryLabel);
            this.splitContainer1.Panel1.Controls.Add(this.btnChangeCategory);
            this.splitContainer1.Panel1.Controls.Add(this.categoryLabel);
            this.splitContainer1.Panel1.Controls.Add(this.sendLabel2);
            this.splitContainer1.Panel1.Controls.Add(this.pluginBox);
            this.splitContainer1.Panel1.Controls.Add(this.labelLaps);
            this.splitContainer1.Panel1.Controls.Add(this.activeBox);
            this.splitContainer1.Panel1.Controls.Add(this.labelShow);
            this.splitContainer1.Panel1.Controls.Add(this.selectedBox);
            this.splitContainer1.Panel1.Controls.Add(this.sendResultToLabel1);
            this.splitContainer1.Panel1.Controls.Add(this.speedBox);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.summaryList);
            this.splitContainer1.Size = new System.Drawing.Size(391, 228);
            this.splitContainer1.SplitterDistance = 27;
            this.splitContainer1.SplitterWidth = 1;
            this.splitContainer1.TabIndex = 16;
            // 
            // UniqueRoutesActivityDetailView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.Controls.Add(this.splitContainer1);
            this.Name = "UniqueRoutesActivityDetailView";
            this.Size = new System.Drawing.Size(391, 228);
            this.contextMenu.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label summaryLabel;
        private ZoneFiveSoftware.Common.Visuals.TreeList summaryList;
        private System.Windows.Forms.ProgressBar progressBar;
        private ZoneFiveSoftware.Common.Visuals.Button btnDoIt;
        private System.Windows.Forms.ComboBox speedBox;
        private System.Windows.Forms.ContextMenuStrip contextMenu;
        private System.Windows.Forms.ToolStripMenuItem copyTable;
        private System.Windows.Forms.ToolStripMenuItem listSettingsMenuItem;
        private System.Windows.Forms.ToolStripMenuItem activeMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sendToMenuItem;
        private System.Windows.Forms.Label sendResultToLabel1;
        private System.Windows.Forms.ComboBox pluginBox;
        private System.Windows.Forms.ComboBox selectedBox;
        private System.Windows.Forms.Label sendLabel2;
        private System.Windows.Forms.Label labelShow;
        private System.Windows.Forms.Label categoryLabel;
        private ZoneFiveSoftware.Common.Visuals.Button btnChangeCategory;
        private System.Windows.Forms.ComboBox activeBox;
        private System.Windows.Forms.Label labelLaps;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ToolStripMenuItem ctxMenuItemRefActivity;
    }
}
