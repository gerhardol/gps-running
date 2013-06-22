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
            this.temperatureLabel2 = new System.Windows.Forms.Label();
            this.temperatureLabel = new System.Windows.Forms.Label();
            this.temperatureList = new ZoneFiveSoftware.Common.Visuals.TreeList();
            this.weightTab = new System.Windows.Forms.TabPage();
            this.weightTableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.weightLabel2 = new System.Windows.Forms.Label();
            this.weightLabel = new System.Windows.Forms.Label();
            this.weightList = new ZoneFiveSoftware.Common.Visuals.TreeList();
            this.shoeTab = new System.Windows.Forms.TabPage();
            this.shoeTableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.shoeLabel2 = new System.Windows.Forms.Label();
            this.shoeLabel = new System.Windows.Forms.Label();
            this.shoeList = new ZoneFiveSoftware.Common.Visuals.TreeList();
            this.ageTab = new System.Windows.Forms.TabPage();
            this.ageTableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.ageLabel2 = new System.Windows.Forms.Label();
            this.ageLabel = new System.Windows.Forms.Label();
            this.ageList = new ZoneFiveSoftware.Common.Visuals.TreeList();
            this.utopiaTab = new System.Windows.Forms.TabPage();
            this.utopiaTableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.utopiaLabel2 = new System.Windows.Forms.Label();
            this.utopiaLabel = new System.Windows.Forms.Label();
            this.utopiaActualLabel = new System.Windows.Forms.Label();
            this.utopiaIdealLabel = new System.Windows.Forms.Label();
            this.utopiaTimeLabel = new System.Windows.Forms.Label();
            this.utopiaDistLabel = new System.Windows.Forms.Label();
            this.utopiaTempLabel = new System.Windows.Forms.Label();
            this.utopiaWeightLabel = new System.Windows.Forms.Label();
            this.utopiaShoeLabel = new System.Windows.Forms.Label();
            this.utopiaAgeLabel = new System.Windows.Forms.Label();
            this.timeBox = new ZoneFiveSoftware.Common.Visuals.TextBox();
            this.distBox = new ZoneFiveSoftware.Common.Visuals.TextBox();
            this.temperatureBox = new ZoneFiveSoftware.Common.Visuals.TextBox();
            this.weightBox = new ZoneFiveSoftware.Common.Visuals.TextBox();
            this.shoeBox = new ZoneFiveSoftware.Common.Visuals.TextBox();
            this.ageBox = new ZoneFiveSoftware.Common.Visuals.TextBox();
            this.temperatureBox2 = new ZoneFiveSoftware.Common.Visuals.TextBox();
            this.weightBox2 = new ZoneFiveSoftware.Common.Visuals.TextBox();
            this.shoeBox2 = new ZoneFiveSoftware.Common.Visuals.TextBox();
            this.ageBox2 = new ZoneFiveSoftware.Common.Visuals.TextBox();
            this.listMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.copyTableMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.temperatureTab.SuspendLayout();
            this.temperatureTableLayoutPanel.SuspendLayout();
            this.weightTab.SuspendLayout();
            this.weightTableLayoutPanel.SuspendLayout();
            this.shoeTab.SuspendLayout();
            this.shoeTableLayoutPanel.SuspendLayout();
            this.ageTab.SuspendLayout();
            this.ageTableLayoutPanel.SuspendLayout();
            this.utopiaTab.SuspendLayout();
            this.utopiaTableLayoutPanel.SuspendLayout();
            this.listMenu.SuspendLayout();
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
            this.tabControl1.Controls.Add(this.shoeTab);
            this.tabControl1.Controls.Add(this.ageTab);
            this.tabControl1.Controls.Add(this.utopiaTab);
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
            this.temperatureTableLayoutPanel.Controls.Add(this.temperatureLabel2, 0, 0);
            this.temperatureTableLayoutPanel.Controls.Add(this.temperatureLabel, 0, 1);
            this.temperatureTableLayoutPanel.Controls.Add(this.temperatureList, 0, 2);
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
            this.weightTableLayoutPanel.Controls.Add(this.weightLabel2, 0, 0);
            this.weightTableLayoutPanel.Controls.Add(this.weightLabel, 0, 1);
            this.weightTableLayoutPanel.Controls.Add(this.weightList, 0, 2);
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
            // shoeTab
            // 
            this.shoeTab.AutoScroll = true;
            this.shoeTab.Controls.Add(this.shoeTableLayoutPanel);
            this.shoeTab.Location = new System.Drawing.Point(4, 22);
            this.shoeTab.Name = "shoeTab";
            this.shoeTab.Size = new System.Drawing.Size(434, 211);
            this.shoeTab.TabIndex = 4;
            this.shoeTab.Text = "<shoe impact";
            this.shoeTab.UseVisualStyleBackColor = true;
            // 
            // shoeTableLayoutPanel
            // 
            this.shoeTableLayoutPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.shoeTableLayoutPanel.ColumnCount = 2;
            this.shoeTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.shoeTableLayoutPanel.Controls.Add(this.shoeLabel2, 0, 0);
            this.shoeTableLayoutPanel.Controls.Add(this.shoeLabel, 0, 1);
            this.shoeTableLayoutPanel.Controls.Add(this.shoeList, 0, 2);
            this.shoeTableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.shoeTableLayoutPanel.Location = new System.Drawing.Point(0, 0);
            this.shoeTableLayoutPanel.Name = "shoeTableLayoutPanel";
            this.shoeTableLayoutPanel.RowCount = 3;
            this.shoeTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 15F));
            this.shoeTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 15F));
            this.shoeTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.shoeTableLayoutPanel.Size = new System.Drawing.Size(434, 211);
            this.shoeTableLayoutPanel.TabIndex = 0;
            // 
            // shoeLabel2
            // 
            this.shoeLabel2.AutoSize = true;
            this.shoeLabel2.Location = new System.Drawing.Point(3, 0);
            this.shoeLabel2.Name = "shoeLabel2";
            this.shoeLabel2.Size = new System.Drawing.Size(265, 13);
            this.shoeLabel2.TabIndex = 7;
            this.shoeLabel2.Text = "<Weight per shoe";
            // 
            // shoeLabel
            // 
            this.shoeLabel.AutoSize = true;
            this.shoeLabel.Location = new System.Drawing.Point(3, 30);
            this.shoeLabel.Name = "shoeLabel";
            this.shoeLabel.Size = new System.Drawing.Size(98, 13);
            this.shoeLabel.TabIndex = 6;
            this.shoeLabel.Text = "<estimated shoe>";
            // 
            // shoeList
            // 
            this.shoeList.AutoScroll = true;
            this.shoeList.AutoSize = true;
            this.shoeList.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.shoeList.BackColor = System.Drawing.Color.Transparent;
            this.shoeList.Border = ZoneFiveSoftware.Common.Visuals.ControlBorder.Style.SmallRoundShadow;
            this.shoeList.CheckBoxes = false;
            this.shoeList.ContextMenuStrip = this.listMenu;
            this.shoeList.DefaultIndent = 15;
            this.shoeList.DefaultRowHeight = -1;
            this.shoeList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.shoeList.HeaderRowHeight = 21;
            this.shoeList.Location = new System.Drawing.Point(3, 46);
            this.shoeList.MultiSelect = false;
            this.shoeList.Name = "shoeList";
            this.shoeList.NumHeaderRows = ZoneFiveSoftware.Common.Visuals.TreeList.HeaderRows.Auto;
            this.shoeList.NumLockedColumns = 0;
            this.shoeList.RowAlternatingColors = true;
            this.shoeList.RowHotlightColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(51)))), ((int)(((byte)(153)))), ((int)(((byte)(255)))));
            this.shoeList.RowHotlightColorText = System.Drawing.SystemColors.HighlightText;
            this.shoeList.RowHotlightMouse = true;
            this.shoeList.RowSelectedColor = System.Drawing.SystemColors.Highlight;
            this.shoeList.RowSelectedColorText = System.Drawing.SystemColors.HighlightText;
            this.shoeList.RowSeparatorLines = true;
            this.shoeList.ShowLines = false;
            this.shoeList.ShowPlusMinus = false;
            this.shoeList.Size = new System.Drawing.Size(428, 162);
            this.shoeList.TabIndex = 8;
            // 
            // ageTab
            // 
            this.ageTab.AutoScroll = true;
            this.ageTab.Controls.Add(this.ageTableLayoutPanel);
            this.ageTab.Location = new System.Drawing.Point(4, 22);
            this.ageTab.Name = "ageTab";
            this.ageTab.Size = new System.Drawing.Size(434, 211);
            this.ageTab.TabIndex = 4;
            this.ageTab.Text = "<age impact";
            this.ageTab.UseVisualStyleBackColor = true;
            // 
            // ageTableLayoutPanel
            // 
            this.ageTableLayoutPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ageTableLayoutPanel.ColumnCount = 2;
            this.ageTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.ageTableLayoutPanel.Controls.Add(this.ageLabel2, 0, 0);
            this.ageTableLayoutPanel.Controls.Add(this.ageLabel, 0, 1);
            this.ageTableLayoutPanel.Controls.Add(this.ageList, 0, 2);
            this.ageTableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ageTableLayoutPanel.Location = new System.Drawing.Point(0, 0);
            this.ageTableLayoutPanel.Name = "ageTableLayoutPanel";
            this.ageTableLayoutPanel.RowCount = 3;
            this.ageTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 15F));
            this.ageTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 15F));
            this.ageTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.ageTableLayoutPanel.Size = new System.Drawing.Size(434, 211);
            this.ageTableLayoutPanel.TabIndex = 0;
            // 
            // ageLabel2
            // 
            this.ageLabel2.AutoSize = true;
            this.ageLabel2.Location = new System.Drawing.Point(3, 0);
            this.ageLabel2.Name = "ageLabel2";
            this.ageLabel2.Size = new System.Drawing.Size(265, 13);
            this.ageLabel2.TabIndex = 7;
            this.ageLabel2.Text = "<Ideal result is...";
            // 
            // ageLabel
            // 
            this.ageLabel.AutoSize = true;
            this.ageLabel.Location = new System.Drawing.Point(3, 30);
            this.ageLabel.Name = "ageLabel";
            this.ageLabel.Size = new System.Drawing.Size(98, 13);
            this.ageLabel.TabIndex = 6;
            this.ageLabel.Text = "<estimated age>";
            // 
            // ageList
            // 
            this.ageList.AutoScroll = true;
            this.ageList.AutoSize = true;
            this.ageList.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ageList.BackColor = System.Drawing.Color.Transparent;
            this.ageList.Border = ZoneFiveSoftware.Common.Visuals.ControlBorder.Style.SmallRoundShadow;
            this.ageList.CheckBoxes = false;
            this.ageList.ContextMenuStrip = this.listMenu;
            this.ageList.DefaultIndent = 15;
            this.ageList.DefaultRowHeight = -1;
            this.ageList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ageList.HeaderRowHeight = 21;
            this.ageList.Location = new System.Drawing.Point(3, 46);
            this.ageList.MultiSelect = false;
            this.ageList.Name = "ageList";
            this.ageList.NumHeaderRows = ZoneFiveSoftware.Common.Visuals.TreeList.HeaderRows.Auto;
            this.ageList.NumLockedColumns = 0;
            this.ageList.RowAlternatingColors = true;
            this.ageList.RowHotlightColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(51)))), ((int)(((byte)(153)))), ((int)(((byte)(255)))));
            this.ageList.RowHotlightColorText = System.Drawing.SystemColors.HighlightText;
            this.ageList.RowHotlightMouse = true;
            this.ageList.RowSelectedColor = System.Drawing.SystemColors.Highlight;
            this.ageList.RowSelectedColorText = System.Drawing.SystemColors.HighlightText;
            this.ageList.RowSeparatorLines = true;
            this.ageList.ShowLines = false;
            this.ageList.ShowPlusMinus = false;
            this.ageList.Size = new System.Drawing.Size(428, 162);
            this.ageList.TabIndex = 8;
            // 
            // utopiaTab
            // 
            this.utopiaTab.AutoScroll = true;
            this.utopiaTab.Controls.Add(this.utopiaTableLayoutPanel);
            this.utopiaTab.Location = new System.Drawing.Point(4, 22);
            this.utopiaTab.Name = "utopiaTab";
            this.utopiaTab.Size = new System.Drawing.Size(434, 211);
            this.utopiaTab.TabIndex = 4;
            this.utopiaTab.Text = "<utopia impact";
            this.utopiaTab.UseVisualStyleBackColor = true;
            // 
            // utopiaTableLayoutPanel
            // 
            this.utopiaTableLayoutPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.utopiaTableLayoutPanel.ColumnCount = 4;
            this.utopiaTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.utopiaTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.utopiaTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.utopiaTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.utopiaTableLayoutPanel.Controls.Add(this.utopiaLabel2, 0, 0);
            this.utopiaTableLayoutPanel.Controls.Add(this.utopiaLabel, 0, 1);
            this.utopiaTableLayoutPanel.SetColumnSpan(this.utopiaLabel, 4);
            this.utopiaTableLayoutPanel.SetColumnSpan(this.utopiaLabel2, 4);
            this.utopiaTableLayoutPanel.Controls.Add(this.utopiaActualLabel, 1, 2);
            this.utopiaTableLayoutPanel.Controls.Add(this.utopiaIdealLabel, 2, 2);
            this.utopiaTableLayoutPanel.Controls.Add(this.utopiaTimeLabel, 0, 3);
            this.utopiaTableLayoutPanel.Controls.Add(this.timeBox, 1, 3);
            this.utopiaTableLayoutPanel.Controls.Add(this.utopiaDistLabel, 0, 4);
            this.utopiaTableLayoutPanel.Controls.Add(this.distBox, 1, 4);
            this.utopiaTableLayoutPanel.Controls.Add(this.utopiaTempLabel, 0, 5);
            this.utopiaTableLayoutPanel.Controls.Add(this.temperatureBox, 1, 5);
            this.utopiaTableLayoutPanel.Controls.Add(this.temperatureBox2, 2, 5);
            this.utopiaTableLayoutPanel.Controls.Add(this.utopiaWeightLabel, 0, 6);
            this.utopiaTableLayoutPanel.Controls.Add(this.weightBox, 1, 6);
            this.utopiaTableLayoutPanel.Controls.Add(this.weightBox2, 2, 6);
            this.utopiaTableLayoutPanel.Controls.Add(this.utopiaShoeLabel, 0, 7);
            this.utopiaTableLayoutPanel.Controls.Add(this.shoeBox, 1, 7);
            this.utopiaTableLayoutPanel.Controls.Add(this.shoeBox2, 2, 7);
            this.utopiaTableLayoutPanel.Controls.Add(this.utopiaAgeLabel, 0, 8);
            this.utopiaTableLayoutPanel.Controls.Add(this.ageBox, 1, 8);
            this.utopiaTableLayoutPanel.Controls.Add(this.ageBox2, 2, 8);
            this.utopiaTableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.utopiaTableLayoutPanel.Location = new System.Drawing.Point(0, 0);
            this.utopiaTableLayoutPanel.Name = "utopiaTableLayoutPanel";
            this.utopiaTableLayoutPanel.RowCount = 10;
            this.utopiaTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 15F));
            this.utopiaTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 15F));
            this.utopiaTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 21F));
            this.utopiaTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 21F));
            this.utopiaTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 21F));
            this.utopiaTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 21F));
            this.utopiaTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 21F));
            this.utopiaTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 21F));
            this.utopiaTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 21F));
            this.utopiaTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.utopiaTableLayoutPanel.Size = new System.Drawing.Size(434, 211);
            this.utopiaTableLayoutPanel.TabIndex = 0;
            // 
            // utopiaLabel2
            // 
            this.utopiaLabel2.AutoSize = true;
            this.utopiaLabel2.Location = new System.Drawing.Point(3, 0);
            this.utopiaLabel2.Name = "utopiaLabel2";
            this.utopiaLabel2.Size = new System.Drawing.Size(265, 13);
            this.utopiaLabel2.TabIndex = 7;
            this.utopiaLabel2.Text = "<When everything is ideal...";
            // 
            // utopiaLabel
            // 
            this.utopiaLabel.AutoSize = true;
            this.utopiaLabel.Location = new System.Drawing.Point(3, 30);
            this.utopiaLabel.Name = "utopiaLabel";
            this.utopiaLabel.Size = new System.Drawing.Size(98, 13);
            this.utopiaLabel.TabIndex = 6;
            this.utopiaLabel.Text = "<estimated utopia>";
            // 
            // utopiaActualLabel
            // 
            this.utopiaActualLabel.AutoSize = true;
            this.utopiaActualLabel.Location = new System.Drawing.Point(3, 30);
            this.utopiaActualLabel.Name = "utopiaActualLabel";
            this.utopiaActualLabel.Size = new System.Drawing.Size(98, 13);
            this.utopiaActualLabel.TabIndex = 6;
            this.utopiaActualLabel.Text = "<Actual";
            // 
            // utopiaIdealLabel
            // 
            this.utopiaIdealLabel.AutoSize = true;
            this.utopiaIdealLabel.Location = new System.Drawing.Point(3, 30);
            this.utopiaIdealLabel.Name = "utopiaIdealLabel";
            this.utopiaIdealLabel.Size = new System.Drawing.Size(98, 13);
            this.utopiaIdealLabel.TabIndex = 6;
            this.utopiaIdealLabel.Text = "<Ideal";
            // 
            // utopiaTimeLabel
            // 
            this.utopiaTimeLabel.AutoSize = true;
            this.utopiaTimeLabel.Location = new System.Drawing.Point(3, 30);
            this.utopiaTimeLabel.Name = "utopiaTimeLabel";
            this.utopiaTimeLabel.Size = new System.Drawing.Size(98, 13);
            this.utopiaTimeLabel.TabIndex = 6;
            this.utopiaTimeLabel.Text = "<Time";
            // 
            // utopiaDistLabel
            // 
            this.utopiaDistLabel.AutoSize = true;
            this.utopiaDistLabel.Location = new System.Drawing.Point(3, 30);
            this.utopiaDistLabel.Name = "utopiaDistLabel";
            this.utopiaDistLabel.Size = new System.Drawing.Size(98, 13);
            this.utopiaDistLabel.TabIndex = 6;
            this.utopiaDistLabel.Text = "<Distance";
            // 
            // utopiaTempLabel
            // 
            this.utopiaTempLabel.AutoSize = true;
            this.utopiaTempLabel.Location = new System.Drawing.Point(3, 30);
            this.utopiaTempLabel.Name = "utopiaTempLabel";
            this.utopiaTempLabel.Size = new System.Drawing.Size(98, 13);
            this.utopiaTempLabel.TabIndex = 6;
            this.utopiaTempLabel.Text = "<Temperature";
            // 
            // utopiaWeightLabel
            // 
            this.utopiaWeightLabel.AutoSize = true;
            this.utopiaWeightLabel.Location = new System.Drawing.Point(3, 30);
            this.utopiaWeightLabel.Name = "utopiaWeightLabel";
            this.utopiaWeightLabel.Size = new System.Drawing.Size(98, 13);
            this.utopiaWeightLabel.TabIndex = 6;
            this.utopiaWeightLabel.Text = "<Weight";
            // 
            // utopiaShoeLabel
            // 
            this.utopiaShoeLabel.AutoSize = true;
            this.utopiaShoeLabel.Location = new System.Drawing.Point(3, 30);
            this.utopiaShoeLabel.Name = "utopiaShoeLabel";
            this.utopiaShoeLabel.Size = new System.Drawing.Size(98, 13);
            this.utopiaShoeLabel.TabIndex = 6;
            this.utopiaShoeLabel.Text = "<Shoe";
            // 
            // utopiaAgeLabel
            // 
            this.utopiaAgeLabel.AutoSize = true;
            this.utopiaAgeLabel.Location = new System.Drawing.Point(3, 30);
            this.utopiaAgeLabel.Name = "utopiaAgeLabel";
            this.utopiaAgeLabel.Size = new System.Drawing.Size(98, 13);
            this.utopiaAgeLabel.TabIndex = 6;
            this.utopiaAgeLabel.Text = "<Age";
            // 
            // timeBox
            // 
            this.timeBox.AcceptsReturn = false;
            this.timeBox.AcceptsTab = false;
            this.timeBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.timeBox.BackColor = System.Drawing.Color.White;
            this.timeBox.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(123)))), ((int)(((byte)(114)))), ((int)(((byte)(108)))));
            this.timeBox.ButtonImage = null;
            this.timeBox.Location = new System.Drawing.Point(331, 18);
            this.timeBox.MaxLength = 32767;
            this.timeBox.Multiline = false;
            this.timeBox.Name = "timeBox";
            this.timeBox.ReadOnly = false;
            this.timeBox.ReadOnlyColor = System.Drawing.SystemColors.Control;
            this.timeBox.ReadOnlyTextColor = System.Drawing.SystemColors.ControlLight;
            this.timeBox.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.timeBox.Size = new System.Drawing.Size(50, 19);
            this.timeBox.TabIndex = 1;
            this.timeBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.timeBox.LostFocus += timeBox_LostFocus;
            // 
            // distBox
            // 
            this.distBox.AcceptsReturn = false;
            this.distBox.AcceptsTab = false;
            this.distBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.distBox.BackColor = System.Drawing.Color.White;
            this.distBox.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(123)))), ((int)(((byte)(114)))), ((int)(((byte)(108)))));
            this.distBox.ButtonImage = null;
            this.distBox.Location = new System.Drawing.Point(331, 18);
            this.distBox.MaxLength = 32767;
            this.distBox.Multiline = false;
            this.distBox.Name = "distBox";
            this.distBox.ReadOnly = false;
            this.distBox.ReadOnlyColor = System.Drawing.SystemColors.Control;
            this.distBox.ReadOnlyTextColor = System.Drawing.SystemColors.ControlLight;
            this.distBox.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.distBox.Size = new System.Drawing.Size(50, 19);
            this.distBox.TabIndex = 1;
            this.distBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.distBox.LostFocus += distBox_LostFocus;
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
            // shoeBox
            // 
            this.shoeBox.AcceptsReturn = false;
            this.shoeBox.AcceptsTab = false;
            this.shoeBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.shoeBox.BackColor = System.Drawing.Color.White;
            this.shoeBox.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(123)))), ((int)(((byte)(114)))), ((int)(((byte)(108)))));
            this.shoeBox.ButtonImage = null;
            this.shoeBox.Location = new System.Drawing.Point(331, 18);
            this.shoeBox.MaxLength = 32767;
            this.shoeBox.Multiline = false;
            this.shoeBox.Name = "shoeBox";
            this.shoeBox.ReadOnly = false;
            this.shoeBox.ReadOnlyColor = System.Drawing.SystemColors.Control;
            this.shoeBox.ReadOnlyTextColor = System.Drawing.SystemColors.ControlLight;
            this.shoeBox.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.shoeBox.Size = new System.Drawing.Size(50, 19);
            this.shoeBox.TabIndex = 1;
            this.shoeBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.shoeBox.LostFocus += shoeBox_LostFocus;
            // 
            // ageBox
            // 
            this.ageBox.AcceptsReturn = false;
            this.ageBox.AcceptsTab = false;
            this.ageBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ageBox.BackColor = System.Drawing.Color.White;
            this.ageBox.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(123)))), ((int)(((byte)(114)))), ((int)(((byte)(108)))));
            this.ageBox.ButtonImage = null;
            this.ageBox.Location = new System.Drawing.Point(331, 18);
            this.ageBox.MaxLength = 32767;
            this.ageBox.Multiline = false;
            this.ageBox.Name = "ageBox";
            this.ageBox.ReadOnly = false;
            this.ageBox.ReadOnlyColor = System.Drawing.SystemColors.Control;
            this.ageBox.ReadOnlyTextColor = System.Drawing.SystemColors.ControlLight;
            this.ageBox.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.ageBox.Size = new System.Drawing.Size(50, 19);
            this.ageBox.TabIndex = 1;
            this.ageBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.ageBox.LostFocus += ageBox_LostFocus;
            // 
            // temperatureBox2
            // 
            this.temperatureBox2.AcceptsReturn = false;
            this.temperatureBox2.AcceptsTab = false;
            this.temperatureBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.temperatureBox2.BackColor = System.Drawing.Color.White;
            this.temperatureBox2.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(123)))), ((int)(((byte)(114)))), ((int)(((byte)(108)))));
            this.temperatureBox2.ButtonImage = null;
            this.temperatureBox2.Location = new System.Drawing.Point(331, 18);
            this.temperatureBox2.MaxLength = 32767;
            this.temperatureBox2.Multiline = false;
            this.temperatureBox2.Name = "temperatureBox2";
            this.temperatureBox2.ReadOnly = false;
            this.temperatureBox2.ReadOnlyColor = System.Drawing.SystemColors.Control;
            this.temperatureBox2.ReadOnlyTextColor = System.Drawing.SystemColors.ControlLight;
            this.temperatureBox2.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.temperatureBox2.Size = new System.Drawing.Size(50, 19);
            this.temperatureBox2.TabIndex = 1;
            this.temperatureBox2.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.temperatureBox2.LostFocus += temperatureBox2_LostFocus;
            // 
            // weightBox2
            // 
            this.weightBox2.AcceptsReturn = false;
            this.weightBox2.AcceptsTab = false;
            this.weightBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.weightBox2.BackColor = System.Drawing.Color.White;
            this.weightBox2.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(123)))), ((int)(((byte)(114)))), ((int)(((byte)(108)))));
            this.weightBox2.ButtonImage = null;
            this.weightBox2.Location = new System.Drawing.Point(331, 18);
            this.weightBox2.MaxLength = 32767;
            this.weightBox2.Multiline = false;
            this.weightBox2.Name = "weightBox2";
            this.weightBox2.ReadOnly = false;
            this.weightBox2.ReadOnlyColor = System.Drawing.SystemColors.Control;
            this.weightBox2.ReadOnlyTextColor = System.Drawing.SystemColors.ControlLight;
            this.weightBox2.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.weightBox2.Size = new System.Drawing.Size(50, 19);
            this.weightBox2.TabIndex = 1;
            this.weightBox2.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.weightBox2.LostFocus += weightBox2_LostFocus;
            // 
            // shoeBox2
            // 
            this.shoeBox2.AcceptsReturn = false;
            this.shoeBox2.AcceptsTab = false;
            this.shoeBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.shoeBox2.BackColor = System.Drawing.Color.White;
            this.shoeBox2.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(123)))), ((int)(((byte)(114)))), ((int)(((byte)(108)))));
            this.shoeBox2.ButtonImage = null;
            this.shoeBox2.Location = new System.Drawing.Point(331, 18);
            this.shoeBox2.MaxLength = 32767;
            this.shoeBox2.Multiline = false;
            this.shoeBox2.Name = "shoeBox2";
            this.shoeBox2.ReadOnly = false;
            this.shoeBox2.ReadOnlyColor = System.Drawing.SystemColors.Control;
            this.shoeBox2.ReadOnlyTextColor = System.Drawing.SystemColors.ControlLight;
            this.shoeBox2.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.shoeBox2.Size = new System.Drawing.Size(50, 19);
            this.shoeBox2.TabIndex = 1;
            this.shoeBox2.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.shoeBox2.LostFocus += shoeBox2_LostFocus;
            // 
            // ageBox2
            // 
            this.ageBox2.AcceptsReturn = false;
            this.ageBox2.AcceptsTab = false;
            this.ageBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ageBox2.BackColor = System.Drawing.Color.White;
            this.ageBox2.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(123)))), ((int)(((byte)(114)))), ((int)(((byte)(108)))));
            this.ageBox2.ButtonImage = null;
            this.ageBox2.Location = new System.Drawing.Point(331, 18);
            this.ageBox2.MaxLength = 32767;
            this.ageBox2.Multiline = false;
            this.ageBox2.Name = "ageBox2";
            this.ageBox2.ReadOnly = false;
            this.ageBox2.ReadOnlyColor = System.Drawing.SystemColors.Control;
            this.ageBox2.ReadOnlyTextColor = System.Drawing.SystemColors.ControlLight;
            this.ageBox2.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.ageBox2.Size = new System.Drawing.Size(50, 19);
            this.ageBox2.TabIndex = 1;
            this.ageBox2.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.ageBox2.LostFocus += ageBox2_LostFocus;
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
            this.weightTab.ResumeLayout(false);
            this.weightTableLayoutPanel.ResumeLayout(false);
            this.weightTableLayoutPanel.PerformLayout();
            this.shoeTab.ResumeLayout(false);
            this.shoeTableLayoutPanel.ResumeLayout(false);
            this.shoeTableLayoutPanel.PerformLayout();
            this.ageTab.ResumeLayout(false);
            this.ageTableLayoutPanel.ResumeLayout(false);
            this.ageTableLayoutPanel.PerformLayout();
            this.utopiaTab.ResumeLayout(false);
            this.utopiaTableLayoutPanel.ResumeLayout(false);
            this.utopiaTableLayoutPanel.PerformLayout();
            this.listMenu.ResumeLayout(false);
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

        private System.Windows.Forms.TabPage weightTab;
        private System.Windows.Forms.TableLayoutPanel weightTableLayoutPanel;
        private System.Windows.Forms.Label weightLabel2;
        private System.Windows.Forms.Label weightLabel;
        private ZoneFiveSoftware.Common.Visuals.TreeList weightList;

        private System.Windows.Forms.TabPage shoeTab;
        private System.Windows.Forms.TableLayoutPanel shoeTableLayoutPanel;
        private System.Windows.Forms.Label shoeLabel2;
        private System.Windows.Forms.Label shoeLabel;
        private ZoneFiveSoftware.Common.Visuals.TreeList shoeList;

        private System.Windows.Forms.TabPage ageTab;
        private System.Windows.Forms.TableLayoutPanel ageTableLayoutPanel;
        private System.Windows.Forms.Label ageLabel2;
        private System.Windows.Forms.Label ageLabel;
        private ZoneFiveSoftware.Common.Visuals.TreeList ageList;

        private System.Windows.Forms.TabPage utopiaTab;
        private System.Windows.Forms.TableLayoutPanel utopiaTableLayoutPanel;
        private System.Windows.Forms.Label utopiaLabel2;
        private System.Windows.Forms.Label utopiaLabel;
        private System.Windows.Forms.Label utopiaActualLabel;
        private System.Windows.Forms.Label utopiaIdealLabel;
        private System.Windows.Forms.Label utopiaTimeLabel;
        private System.Windows.Forms.Label utopiaDistLabel;
        private System.Windows.Forms.Label utopiaTempLabel;
        private System.Windows.Forms.Label utopiaWeightLabel;
        private System.Windows.Forms.Label utopiaShoeLabel;
        private System.Windows.Forms.Label utopiaAgeLabel;
        private ZoneFiveSoftware.Common.Visuals.TextBox timeBox;
        private ZoneFiveSoftware.Common.Visuals.TextBox distBox;
        private ZoneFiveSoftware.Common.Visuals.TextBox temperatureBox;
        private ZoneFiveSoftware.Common.Visuals.TextBox weightBox;
        private ZoneFiveSoftware.Common.Visuals.TextBox shoeBox;
        private ZoneFiveSoftware.Common.Visuals.TextBox ageBox;
        private ZoneFiveSoftware.Common.Visuals.TextBox temperatureBox2;
        private ZoneFiveSoftware.Common.Visuals.TextBox weightBox2;
        private ZoneFiveSoftware.Common.Visuals.TextBox shoeBox2;
        private ZoneFiveSoftware.Common.Visuals.TextBox ageBox2;
    }
}
