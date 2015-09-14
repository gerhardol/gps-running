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
    partial class HighScoreControl
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
            this.label1 = new System.Windows.Forms.Label();
            this.boundsBox = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.imageBox = new System.Windows.Forms.ComboBox();
            this.domainBox = new System.Windows.Forms.ComboBox();
            this.minGradeLbl = new System.Windows.Forms.Label();
            this.minGradeBox = new ZoneFiveSoftware.Common.Visuals.TextBox();
            this.ControlPanel = new System.Windows.Forms.Panel();
            this.ControlPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(22, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(27, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Find";
            // 
            // boundsBox
            // 
            this.boundsBox.FormattingEnabled = true;
            this.boundsBox.Location = new System.Drawing.Point(55, 3);
            this.boundsBox.Name = "boundsBox";
            this.boundsBox.Size = new System.Drawing.Size(72, 21);
            this.boundsBox.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(196, 6);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "per specified";
            // 
            // imageBox
            // 
            this.imageBox.FormattingEnabled = true;
            this.imageBox.Location = new System.Drawing.Point(269, 3);
            this.imageBox.Name = "imageBox";
            this.imageBox.Size = new System.Drawing.Size(139, 21);
            this.imageBox.TabIndex = 7;
            // 
            // domainBox
            // 
            this.domainBox.FormattingEnabled = true;
            this.domainBox.Location = new System.Drawing.Point(133, 3);
            this.domainBox.Name = "domainBox";
            this.domainBox.Size = new System.Drawing.Size(69, 21);
            this.domainBox.TabIndex = 8;
            // 
            // minGradeLbl
            // 
            this.minGradeLbl.AutoSize = true;
            this.minGradeLbl.Location = new System.Drawing.Point(414, 6);
            this.minGradeLbl.Name = "minGradeLbl";
            this.minGradeLbl.Size = new System.Drawing.Size(48, 13);
            this.minGradeLbl.TabIndex = 18;
            this.minGradeLbl.Text = "<Grade>";
            // 
            // minGradeBox
            // 
            this.minGradeBox.AcceptsReturn = false;
            this.minGradeBox.AcceptsTab = false;
            this.minGradeBox.BackColor = System.Drawing.Color.White;
            this.minGradeBox.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(123)))), ((int)(((byte)(114)))), ((int)(((byte)(108)))));
            this.minGradeBox.ButtonImage = null;
            this.minGradeBox.Location = new System.Drawing.Point(456, 3);
            this.minGradeBox.MaxLength = 32767;
            this.minGradeBox.Multiline = false;
            this.minGradeBox.Name = "minGradeBox";
            this.minGradeBox.ReadOnly = false;
            this.minGradeBox.ReadOnlyColor = System.Drawing.SystemColors.Control;
            this.minGradeBox.ReadOnlyTextColor = System.Drawing.SystemColors.ControlLight;
            this.minGradeBox.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.minGradeBox.Size = new System.Drawing.Size(50, 20);
            this.minGradeBox.TabIndex = 19;
            this.minGradeBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.minGradeBox.Leave += new System.EventHandler(this.minGradeBox_Leave);
            // 
            // ControlPanel
            // 
            this.ControlPanel.BackColor = System.Drawing.Color.Transparent;
            this.ControlPanel.Controls.Add(this.minGradeBox);
            this.ControlPanel.Controls.Add(this.minGradeLbl);
            this.ControlPanel.Controls.Add(this.domainBox);
            this.ControlPanel.Controls.Add(this.imageBox);
            this.ControlPanel.Controls.Add(this.label2);
            this.ControlPanel.Controls.Add(this.boundsBox);
            this.ControlPanel.Controls.Add(this.label1);
            this.ControlPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ControlPanel.Location = new System.Drawing.Point(0, 0);
            this.ControlPanel.Name = "ControlPanel";
            this.ControlPanel.Size = new System.Drawing.Size(495, 25);
            this.ControlPanel.TabIndex = 0;
            // 
            // HighScoreControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.AutoSize = true;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.ControlPanel);
            this.Name = "HighScoreControl";
            this.Size = new System.Drawing.Size(495, 25);
            this.ControlPanel.ResumeLayout(false);
            this.ControlPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox boundsBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox imageBox;
        private System.Windows.Forms.ComboBox domainBox;
        private ZoneFiveSoftware.Common.Visuals.TextBox minGradeBox;
        private System.Windows.Forms.Label minGradeLbl;
        private System.Windows.Forms.Panel ControlPanel;
    }
}
