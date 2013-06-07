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
    partial class ExtrapolateView
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.temperatureTab = new System.Windows.Forms.TabPage();
            this.temperatureTableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.temperatureBox = new ZoneFiveSoftware.Common.Visuals.TextBox();
            this.temperatureLabel2 = new System.Windows.Forms.Label();
            this.temperatureLabel = new System.Windows.Forms.Label();
            this.temperatureList = new ZoneFiveSoftware.Common.Visuals.TreeList();
            this.listMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.copyTableMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.weightTab = new System.Windows.Forms.TabPage();
            this.weightTableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.weightBox = new ZoneFiveSoftware.Common.Visuals.TextBox();
            this.weightLabel2 = new System.Windows.Forms.Label();
            this.weightLabel = new System.Windows.Forms.Label();
            this.weightList = new ZoneFiveSoftware.Common.Visuals.TreeList();
            this.panel1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.temperatureTab.SuspendLayout();
            this.temperatureTableLayoutPanel.SuspendLayout();
            this.listMenu.SuspendLayout();
            this.weightTab.SuspendLayout();
            this.weightTableLayoutPanel.SuspendLayout();
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
            this.panel1.Size = new System.Drawing.Size(442, 237);
            this.panel1.TabIndex = 1;
            // 
            // tabControl1
            // 
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
            this.tabControl1.Size = new System.Drawing.Size(442, 237);
            this.tabControl1.TabIndex = 0;
            this.tabControl1.DrawItem += new System.Windows.Forms.DrawItemEventHandler(PerformancePredictorControl.tabControl1_DrawItem);
            // 
            // temperatureTab
            // 
            this.temperatureTab.AutoScroll = true;
            this.temperatureTab.Controls.Add(this.temperatureTableLayoutPanel);
            this.temperatureTab.Location = new System.Drawing.Point(4, 22);
            this.temperatureTab.Name = "temperatureTab";
            this.temperatureTab.Size = new System.Drawing.Size(434, 211);
            this.temperatureTab.TabIndex = 3;
            this.temperatureTab.Text = "<Temperature impact";
            this.temperatureTab.UseVisualStyleBackColor = true;
            // 
            // temperatureTableLayoutPanel
            // 
            this.temperatureTableLayoutPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.temperatureTableLayoutPanel.ColumnCount = 2;
            this.temperatureTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.temperatureTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 55F));
            this.temperatureTableLayoutPanel.Controls.Add(this.temperatureBox, 1, 0);
            this.temperatureTableLayoutPanel.Controls.Add(this.temperatureLabel2, 0, 0);
            this.temperatureTableLayoutPanel.Controls.Add(this.temperatureLabel, 0, 1);
            this.temperatureTableLayoutPanel.Controls.Add(this.temperatureList, 0, 2);
            this.temperatureTableLayoutPanel.SetColumnSpan(this.temperatureList, 2);
            this.temperatureTableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.temperatureTableLayoutPanel.Location = new System.Drawing.Point(0, 0);
            this.temperatureTableLayoutPanel.Name = "temperatureTableLayoutPanel";
            this.temperatureTableLayoutPanel.RowCount = 3;
            this.temperatureTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 15F));
            this.temperatureTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 15F));
            this.temperatureTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.temperatureTableLayoutPanel.Size = new System.Drawing.Size(434, 211);
            this.temperatureTableLayoutPanel.TabIndex = 0;
            // 
            // temperatureBox
            // 
            this.temperatureBox.AcceptsReturn = false;
            this.temperatureBox.AcceptsTab = false;
            this.temperatureBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.temperatureBox.BackColor = System.Drawing.Color.White;
            this.temperatureBox.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(123)))), ((int)(((byte)(114)))), ((int)(((byte)(108)))));
            this.temperatureBox.ButtonImage = null;
            this.temperatureBox.Location = new System.Drawing.Point(331, 18);
            this.temperatureBox.MaxLength = 32767;
            this.temperatureBox.Multiline = false;
            this.temperatureBox.Name = "temperatureBox";
            this.temperatureBox.ReadOnly = false;
            this.temperatureBox.ReadOnlyColor = System.Drawing.SystemColors.Control;
            this.temperatureBox.ReadOnlyTextColor = System.Drawing.SystemColors.ControlLight;
            this.temperatureBox.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.temperatureBox.Size = new System.Drawing.Size(50, 19);
            this.temperatureBox.TabIndex = 1;
            this.temperatureBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.temperatureBox.LostFocus += temperatureBox_LostFocus;
            // 
            // temperatureLabel2
            // 
            this.temperatureLabel2.AutoSize = true;
            this.temperatureLabel2.Location = new System.Drawing.Point(3, 0);
            this.temperatureLabel2.Name = "temperatureLabel2";
            this.temperatureLabel2.Size = new System.Drawing.Size(272, 13);
            this.temperatureLabel2.TabIndex = 4;
            this.temperatureLabel2.Text = "<Performance is not adversely affected at 16° C or lower";
            // 
            // temperatureLabel
            // 
            this.temperatureLabel.AutoSize = true;
            this.temperatureLabel.Location = new System.Drawing.Point(3, 30);
            this.temperatureLabel.Name = "temperatureLabel";
            this.temperatureLabel.Size = new System.Drawing.Size(95, 13);
            this.temperatureLabel.TabIndex = 3;
            this.temperatureLabel.Text = "<temperatureLabel";
            // 
            // temperatureList
            // 
            this.temperatureList.AutoScroll = true;
            this.temperatureList.AutoSize = true;
            this.temperatureList.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.temperatureList.BackColor = System.Drawing.Color.Transparent;
            this.temperatureList.Border = ZoneFiveSoftware.Common.Visuals.ControlBorder.Style.SmallRoundShadow;
            this.temperatureList.CheckBoxes = false;
            this.temperatureList.ContextMenuStrip = this.listMenu;
            this.temperatureList.DefaultIndent = 15;
            this.temperatureList.DefaultRowHeight = -1;
            this.temperatureList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.temperatureList.HeaderRowHeight = 21;
            this.temperatureList.Location = new System.Drawing.Point(3, 46);
            this.temperatureList.MultiSelect = false;
            this.temperatureList.Name = "temperatureList";
            this.temperatureList.NumHeaderRows = ZoneFiveSoftware.Common.Visuals.TreeList.HeaderRows.Auto;
            this.temperatureList.NumLockedColumns = 0;
            this.temperatureList.RowAlternatingColors = true;
            this.temperatureList.RowHotlightColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(51)))), ((int)(((byte)(153)))), ((int)(((byte)(255)))));
            this.temperatureList.RowHotlightColorText = System.Drawing.SystemColors.HighlightText;
            this.temperatureList.RowHotlightMouse = true;
            this.temperatureList.RowSelectedColor = System.Drawing.SystemColors.Highlight;
            this.temperatureList.RowSelectedColorText = System.Drawing.SystemColors.HighlightText;
            this.temperatureList.RowSeparatorLines = true;
            this.temperatureList.ShowLines = false;
            this.temperatureList.ShowPlusMinus = false;
            this.temperatureList.Size = new System.Drawing.Size(428, 162);
            this.temperatureList.TabIndex = 5;
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
            // 
            // weightTab
            // 
            this.weightTab.AutoScroll = true;
            this.weightTab.Controls.Add(this.weightTableLayoutPanel);
            this.weightTab.Location = new System.Drawing.Point(4, 22);
            this.weightTab.Name = "weightTab";
            this.weightTab.Size = new System.Drawing.Size(434, 211);
            this.weightTab.TabIndex = 4;
            this.weightTab.Text = "<Weight impact";
            this.weightTab.UseVisualStyleBackColor = true;
            // 
            // weightTableLayoutPanel
            // 
            this.weightTableLayoutPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.weightTableLayoutPanel.ColumnCount = 2;
            this.weightTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.weightTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 55F));
            this.weightTableLayoutPanel.Controls.Add(this.weightBox, 1, 0);
            this.weightTableLayoutPanel.Controls.Add(this.weightLabel2, 0, 0);
            this.weightTableLayoutPanel.Controls.Add(this.weightLabel, 0, 1);
            this.weightTableLayoutPanel.Controls.Add(this.weightList, 0, 2);
            this.weightTableLayoutPanel.SetColumnSpan(this.weightList, 2);
            this.weightTableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.weightTableLayoutPanel.Location = new System.Drawing.Point(0, 0);
            this.weightTableLayoutPanel.Name = "weightTableLayoutPanel";
            this.weightTableLayoutPanel.RowCount = 3;
            this.weightTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 15F));
            this.weightTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 15F));
            this.weightTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.weightTableLayoutPanel.Size = new System.Drawing.Size(434, 211);
            this.weightTableLayoutPanel.TabIndex = 0;
            // 
            // weightBox
            // 
            this.weightBox.AcceptsReturn = false;
            this.weightBox.AcceptsTab = false;
            this.weightBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.weightBox.BackColor = System.Drawing.Color.White;
            this.weightBox.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(123)))), ((int)(((byte)(114)))), ((int)(((byte)(108)))));
            this.weightBox.ButtonImage = null;
            this.weightBox.Location = new System.Drawing.Point(331, 18);
            this.weightBox.MaxLength = 32767;
            this.weightBox.Multiline = false;
            this.weightBox.Name = "weightBox";
            this.weightBox.ReadOnly = false;
            this.weightBox.ReadOnlyColor = System.Drawing.SystemColors.Control;
            this.weightBox.ReadOnlyTextColor = System.Drawing.SystemColors.ControlLight;
            this.weightBox.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.weightBox.Size = new System.Drawing.Size(50, 19);
            this.weightBox.TabIndex = 1;
            this.weightBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.weightBox.LostFocus += weightBox_LostFocus;
            // 
            // weightLabel2
            // 
            this.weightLabel2.AutoSize = true;
            this.weightLabel2.Location = new System.Drawing.Point(3, 0);
            this.weightLabel2.Name = "weightLabel2";
            this.weightLabel2.Size = new System.Drawing.Size(265, 13);
            this.weightLabel2.TabIndex = 7;
            this.weightLabel2.Text = "<Estimated times and paces are +/- 2 seconds per mile";
            // 
            // weightLabel
            // 
            this.weightLabel.AutoSize = true;
            this.weightLabel.Location = new System.Drawing.Point(3, 30);
            this.weightLabel.Name = "weightLabel";
            this.weightLabel.Size = new System.Drawing.Size(98, 13);
            this.weightLabel.TabIndex = 6;
            this.weightLabel.Text = "<estimated weight>";
            // 
            // weightList
            // 
            this.weightList.AutoScroll = true;
            this.weightList.AutoSize = true;
            this.weightList.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.weightList.BackColor = System.Drawing.Color.Transparent;
            this.weightList.Border = ZoneFiveSoftware.Common.Visuals.ControlBorder.Style.SmallRoundShadow;
            this.weightList.CheckBoxes = false;
            this.weightList.ContextMenuStrip = this.listMenu;
            this.weightList.DefaultIndent = 15;
            this.weightList.DefaultRowHeight = -1;
            this.weightList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.weightList.HeaderRowHeight = 21;
            this.weightList.Location = new System.Drawing.Point(3, 46);
            this.weightList.MultiSelect = false;
            this.weightList.Name = "weightList";
            this.weightList.NumHeaderRows = ZoneFiveSoftware.Common.Visuals.TreeList.HeaderRows.Auto;
            this.weightList.NumLockedColumns = 0;
            this.weightList.RowAlternatingColors = true;
            this.weightList.RowHotlightColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(51)))), ((int)(((byte)(153)))), ((int)(((byte)(255)))));
            this.weightList.RowHotlightColorText = System.Drawing.SystemColors.HighlightText;
            this.weightList.RowHotlightMouse = true;
            this.weightList.RowSelectedColor = System.Drawing.SystemColors.Highlight;
            this.weightList.RowSelectedColorText = System.Drawing.SystemColors.HighlightText;
            this.weightList.RowSeparatorLines = true;
            this.weightList.ShowLines = false;
            this.weightList.ShowPlusMinus = false;
            this.weightList.Size = new System.Drawing.Size(428, 162);
            this.weightList.TabIndex = 8;
            // 
            // ExtrapolateView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.AutoSize = true;
            this.Controls.Add(this.panel1);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "ExtrapolateView";
            this.Size = new System.Drawing.Size(442, 237);
            this.panel1.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.temperatureTab.ResumeLayout(false);
            this.temperatureTableLayoutPanel.ResumeLayout(false);
            this.temperatureTableLayoutPanel.PerformLayout();
            this.listMenu.ResumeLayout(false);
            this.weightTab.ResumeLayout(false);
            this.weightTableLayoutPanel.ResumeLayout(false);
            this.weightTableLayoutPanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ContextMenuStrip listMenu;
        private System.Windows.Forms.ToolStripMenuItem copyTableMenuItem;

        private System.Windows.Forms.TabControl tabControl1;

        private System.Windows.Forms.TabPage temperatureTab;
        private System.Windows.Forms.TableLayoutPanel temperatureTableLayoutPanel;
        private System.Windows.Forms.Label temperatureLabel;
        private System.Windows.Forms.Label temperatureLabel2;
        private ZoneFiveSoftware.Common.Visuals.TreeList temperatureList;
        private ZoneFiveSoftware.Common.Visuals.TextBox temperatureBox;
        
        private System.Windows.Forms.TabPage weightTab;
        private System.Windows.Forms.TableLayoutPanel weightTableLayoutPanel;
        private System.Windows.Forms.Label weightLabel2;
        private System.Windows.Forms.Label weightLabel;
        private ZoneFiveSoftware.Common.Visuals.TreeList weightList;
        private ZoneFiveSoftware.Common.Visuals.TextBox weightBox;

    }
}
