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

namespace SportTracksPerformancePredictorPlugin.Source
{
    partial class PerformancePredictorSettings
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
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.resetSettings = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.removeDistance = new System.Windows.Forms.Button();
            this.unitBox = new System.Windows.Forms.ComboBox();
            this.distanceBox = new System.Windows.Forms.TextBox();
            this.addDistance = new System.Windows.Forms.Button();
            this.distanceList = new System.Windows.Forms.ListBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            this.SuspendLayout();
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Location = new System.Drawing.Point(3, 0);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(190, 13);
            this.linkLabel1.TabIndex = 0;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "Performance Predictor plugin webpage";
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // resetSettings
            // 
            this.resetSettings.Location = new System.Drawing.Point(6, 16);
            this.resetSettings.Name = "resetSettings";
            this.resetSettings.Size = new System.Drawing.Size(187, 23);
            this.resetSettings.TabIndex = 1;
            this.resetSettings.Text = "Reset all settings...";
            this.resetSettings.UseVisualStyleBackColor = true;
            this.resetSettings.Click += new System.EventHandler(this.resetSettings_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.removeDistance);
            this.groupBox1.Controls.Add(this.unitBox);
            this.groupBox1.Controls.Add(this.distanceBox);
            this.groupBox1.Controls.Add(this.addDistance);
            this.groupBox1.Controls.Add(this.distanceList);
            this.groupBox1.Location = new System.Drawing.Point(6, 45);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(326, 162);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Distances used in models";
            // 
            // removeDistance
            // 
            this.removeDistance.Location = new System.Drawing.Point(132, 130);
            this.removeDistance.Name = "removeDistance";
            this.removeDistance.Size = new System.Drawing.Size(186, 23);
            this.removeDistance.TabIndex = 4;
            this.removeDistance.Text = "Remove distance -->";
            this.removeDistance.UseVisualStyleBackColor = true;
            this.removeDistance.Click += new System.EventHandler(this.removeDistance_Click);
            // 
            // unitBox
            // 
            this.unitBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.unitBox.FormattingEnabled = true;
            this.unitBox.Location = new System.Drawing.Point(220, 21);
            this.unitBox.Name = "unitBox";
            this.unitBox.Size = new System.Drawing.Size(98, 21);
            this.unitBox.TabIndex = 3;
            // 
            // distanceBox
            // 
            this.distanceBox.Location = new System.Drawing.Point(132, 21);
            this.distanceBox.Name = "distanceBox";
            this.distanceBox.Size = new System.Drawing.Size(82, 20);
            this.distanceBox.TabIndex = 2;
            // 
            // addDistance
            // 
            this.addDistance.Location = new System.Drawing.Point(132, 47);
            this.addDistance.Name = "addDistance";
            this.addDistance.Size = new System.Drawing.Size(186, 23);
            this.addDistance.TabIndex = 1;
            this.addDistance.Text = "<-- Add distance";
            this.addDistance.UseVisualStyleBackColor = true;
            this.addDistance.Click += new System.EventHandler(this.addDistance_Click);
            // 
            // distanceList
            // 
            this.distanceList.FormattingEnabled = true;
            this.distanceList.Location = new System.Drawing.Point(6, 19);
            this.distanceList.Name = "distanceList";
            this.distanceList.Size = new System.Drawing.Size(120, 134);
            this.distanceList.TabIndex = 0;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.numericUpDown1);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Location = new System.Drawing.Point(6, 213);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(326, 48);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "High Score plugin integration";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(76, 21);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(139, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "% of distance to predict time";
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Location = new System.Drawing.Point(38, 19);
            this.numericUpDown1.Maximum = new decimal(new int[] {
            99,
            0,
            0,
            0});
            this.numericUpDown1.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(32, 20);
            this.numericUpDown1.TabIndex = 1;
            this.numericUpDown1.Value = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.numericUpDown1.ValueChanged += new System.EventHandler(this.numericUpDown1_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(26, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Use";
            // 
            // PerformancePredictorSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.resetSettings);
            this.Controls.Add(this.linkLabel1);
            this.Name = "PerformancePredictorSettings";
            this.Size = new System.Drawing.Size(342, 268);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.Button resetSettings;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button removeDistance;
        private System.Windows.Forms.ComboBox unitBox;
        private System.Windows.Forms.TextBox distanceBox;
        private System.Windows.Forms.Button addDistance;
        private System.Windows.Forms.ListBox distanceList;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.Label label2;
    }
}
