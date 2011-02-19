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
            this.summaryListLabel = new System.Windows.Forms.Label();
            this.summaryList = new ZoneFiveSoftware.Common.Visuals.TreeList();
            this.contextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.copyTable = new System.Windows.Forms.ToolStripMenuItem();
            this.listSettingsMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.activeMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sendToMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ctxMenuItemRefActivity = new System.Windows.Forms.ToolStripMenuItem();
            this.limitActivityMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.m_progressBar = new System.Windows.Forms.ProgressBar();
            this.btnRefresh = new ZoneFiveSoftware.Common.Visuals.Button();
            this.speedBox = new System.Windows.Forms.ComboBox();
            this.sendResultToLabel1 = new System.Windows.Forms.Label();
            this.pluginBox = new System.Windows.Forms.ComboBox();
            this.selectedBox = new System.Windows.Forms.ComboBox();
            this.sendLabel2 = new System.Windows.Forms.Label();
            this.labelShow = new System.Windows.Forms.Label();
            this.categoryLabel = new System.Windows.Forms.Label();
            this.infoIcon = new System.Windows.Forms.PictureBox();
            this.toolTipInfo = new System.Windows.Forms.ToolTip(this.components);
            this.boxCategory = new ZoneFiveSoftware.Common.Visuals.TextBox();
            this.activeBox = new System.Windows.Forms.ComboBox();
            this.labelLaps = new System.Windows.Forms.Label();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.contextMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.infoIcon)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // summaryListLabel
            // 
            this.summaryListLabel.AutoSize = true;
            this.summaryListLabel.Location = new System.Drawing.Point(4, 4);
            this.summaryListLabel.Name = "summaryListLabel";
            this.summaryListLabel.Size = new System.Drawing.Size(166, 13);
            this.summaryListLabel.TabIndex = 0;
            this.summaryListLabel.Text = "Found no activities on same route";
            this.summaryListLabel.Visible = false;
            // 
            // summaryList
            // 
            this.summaryList.AutoScroll = true;
            this.summaryList.AutoSize = true;
            this.summaryList.BackColor = System.Drawing.Color.Transparent;
            this.summaryList.Border = ZoneFiveSoftware.Common.Visuals.ControlBorder.Style.SmallRoundShadow;
            this.summaryList.CheckBoxes = false;
            this.summaryList.ContextMenuStrip = this.contextMenu;
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
            this.summaryList.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.selectedRow_DoubleClick);
            this.summaryList.MouseLeave += new System.EventHandler(this.summaryList_MouseLeave);
            this.summaryList.MouseMove += new System.Windows.Forms.MouseEventHandler(this.summaryList_MouseMove);
            this.summaryList.Click += new System.EventHandler(summaryList_Click);
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
            this.copyTable.Text = "<Copy table to clipboard";
            this.copyTable.Click += new System.EventHandler(this.copyTableMenu_Click);
            // 
            // listSettingsMenuItem
            // 
            this.listSettingsMenuItem.Name = "listSettingsMenuItem";
            this.listSettingsMenuItem.Size = new System.Drawing.Size(198, 22);
            this.listSettingsMenuItem.Text = "<List Settings...";
            // 
            // activeMenuItem
            // 
            this.activeMenuItem.Name = "activeMenuItem";
            this.activeMenuItem.Size = new System.Drawing.Size(198, 22);
            this.activeMenuItem.Text = "<Only active laps";
            this.activeMenuItem.Click += new System.EventHandler(this.activeMenuItem_Click);
            // 
            // sendToMenuItem
            // 
            this.sendToMenuItem.Name = "sendToMenuItem";
            this.sendToMenuItem.Size = new System.Drawing.Size(198, 22);
            this.sendToMenuItem.Text = "<Send to";
            // 
            // limitActivityMenuItem
            // 
            this.limitActivityMenuItem.Name = "limitActivityMenuItem";
            this.limitActivityMenuItem.Size = new System.Drawing.Size(198, 22);
            this.limitActivityMenuItem.Text = "<Limit selection to current activities...";
            this.limitActivityMenuItem.Click += new System.EventHandler(this.limitActivityMenuItem_Click);
            // 
            // ctxMenuItemRefActivity
            // 
            this.ctxMenuItemRefActivity.Name = "ctxMenuItemRefActivity";
            this.ctxMenuItemRefActivity.Size = new System.Drawing.Size(124, 22);
            this.ctxMenuItemRefActivity.Text = "<A&ctivit>";
            // 
            // progressBar
            // 
            this.m_progressBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_progressBar.Location = new System.Drawing.Point(0, 0);
            this.m_progressBar.Name = "progressBar";
            this.m_progressBar.Size = new System.Drawing.Size(391, 24);
            this.m_progressBar.TabIndex = 2;
            this.m_progressBar.Visible = false;
            // 
            // btnRefresh
            // 
            this.btnRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRefresh.BackColor = System.Drawing.Color.Transparent;
            this.btnRefresh.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(40)))), ((int)(((byte)(50)))), ((int)(((byte)(120)))));
            this.btnRefresh.CenterImage = null;
            this.btnRefresh.ContextMenuStrip = this.contextMenu;
            this.btnRefresh.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnRefresh.HyperlinkStyle = false;
            this.btnRefresh.ImageMargin = 2;
            this.btnRefresh.LeftImage = null;
            this.btnRefresh.Location = new System.Drawing.Point(368, 4);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.PushStyle = true;
            this.btnRefresh.RightImage = null;
            this.btnRefresh.Size = new System.Drawing.Size(20, 21);
            this.btnRefresh.TabIndex = 3;
            this.btnRefresh.Text = "Do it!";
            this.btnRefresh.TextAlign = System.Drawing.StringAlignment.Center;
            this.btnRefresh.TextLeftMargin = 2;
            this.btnRefresh.TextRightMargin = 2;
            this.btnRefresh.Click += new System.EventHandler(this.refreshButton_Click);
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
            this.categoryLabel.Location = new System.Drawing.Point(22, 7);
            this.categoryLabel.Name = "categoryLabel";
            this.categoryLabel.Size = new System.Drawing.Size(58, 13);
            this.categoryLabel.TabIndex = 11;
            this.categoryLabel.Text = "<Category:";
            // 
            // infoIcon
            // 
            this.infoIcon.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.infoIcon.Location = new System.Drawing.Point(6, 2);
            this.infoIcon.Name = "infoIcon";
            this.infoIcon.Size = new System.Drawing.Size(19, 20);
            this.infoIcon.TabIndex = 14;
            this.infoIcon.TabStop = false;
            // 
            // toolTipInfo
            // 
            this.toolTipInfo.AutoPopDelay = 25000;
            this.toolTipInfo.InitialDelay = 500;
            this.toolTipInfo.ReshowDelay = 100;
            // 
            // boxCategory
            // 
            this.boxCategory.AcceptsReturn = false;
            this.boxCategory.AcceptsTab = false;
            this.boxCategory.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.boxCategory.BackColor = System.Drawing.Color.White;
            this.boxCategory.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(123)))), ((int)(((byte)(114)))), ((int)(((byte)(108)))));
            this.boxCategory.ButtonImage = null;
            this.boxCategory.Location = new System.Drawing.Point(82, 3);
            this.boxCategory.MaxLength = 32767;
            this.boxCategory.Multiline = false;
            this.boxCategory.Name = "boxCategory";
            this.boxCategory.ReadOnly = true;
            this.boxCategory.ReadOnlyColor = System.Drawing.SystemColors.Control;
            this.boxCategory.ReadOnlyTextColor = System.Drawing.SystemColors.ControlLight;
            this.boxCategory.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.boxCategory.Size = new System.Drawing.Size(281, 21);
            this.boxCategory.TabIndex = 20;
            this.boxCategory.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.boxCategory.ButtonClick += new System.EventHandler(this.boxCategory_ButtonClicked);
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
            this.splitContainer1.Panel1.Controls.Add(this.categoryLabel);
            this.splitContainer1.Panel1.Controls.Add(this.btnRefresh);
            this.splitContainer1.Panel1.Controls.Add(this.boxCategory);
            this.splitContainer1.Panel1.Controls.Add(this.infoIcon);
            this.splitContainer1.Panel2.Controls.Add(this.m_progressBar);
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
            this.splitContainer1.Panel2.Controls.Add(this.summaryListLabel);
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
            ((System.ComponentModel.ISupportInitialize)(this.infoIcon)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label summaryListLabel;
        private ZoneFiveSoftware.Common.Visuals.TreeList summaryList;
        private System.Windows.Forms.ProgressBar m_progressBar;
        private ZoneFiveSoftware.Common.Visuals.Button btnRefresh;
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
        //private ZoneFiveSoftware.Common.Visuals.Button btnChangeCategory;
        private System.Windows.Forms.ComboBox activeBox;
        private System.Windows.Forms.Label labelLaps;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ToolStripMenuItem ctxMenuItemRefActivity;
        private System.Windows.Forms.PictureBox infoIcon;
        private System.Windows.Forms.ToolTip toolTipInfo;
        private ZoneFiveSoftware.Common.Visuals.TextBox boxCategory;
        private System.Windows.Forms.ToolStripMenuItem limitActivityMenuItem;
    }
}
