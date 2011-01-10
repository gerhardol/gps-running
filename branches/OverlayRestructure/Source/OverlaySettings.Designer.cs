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
    partial class OverlaySettings
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
            this.lblUniqueRoutes = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Location = new System.Drawing.Point(3, 40);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(121, 13);
            this.linkLabel1.TabIndex = 0;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "Overlay plugin webpage";
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // lblUniqueRoutes
            // 
            this.lblUniqueRoutes.AutoSize = true;
            this.lblUniqueRoutes.Location = new System.Drawing.Point(3, 14);
            this.lblUniqueRoutes.Name = "lblUniqueRoutes";
            this.lblUniqueRoutes.Size = new System.Drawing.Size(93, 26);
            this.lblUniqueRoutes.TabIndex = 1;
            this.lblUniqueRoutes.Text = "<UR placeholder>\r\nline2";
            // 
            // OverlaySettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.linkLabel1);
            this.Controls.Add(this.lblUniqueRoutes);
            this.Name = "OverlaySettings";
            this.Size = new System.Drawing.Size(130, 61);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.Label lblUniqueRoutes;
    }
}
