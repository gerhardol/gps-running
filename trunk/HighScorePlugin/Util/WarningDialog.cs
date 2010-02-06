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

using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using ZoneFiveSoftware.Common.Visuals;

namespace SportTracksHighScorePlugin.Util
{
    class WarningDialog : Form
    {
        private ZoneFiveSoftware.Common.Visuals.Button ok;
        private ZoneFiveSoftware.Common.Visuals.TextBox textBox1;
    
        public WarningDialog(String message)
        {
            InitializeComponent();
            this.textBox1.Text = message;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            ShowDialog();
        }

        private void InitializeComponent()
        {
            this.ok = new ZoneFiveSoftware.Common.Visuals.Button();
            this.textBox1 = new ZoneFiveSoftware.Common.Visuals.TextBox();
            this.SuspendLayout();
            // 
            // ok
            // 
            this.ok.Location = new System.Drawing.Point(102, 96);
            this.ok.Name = "ok";
            this.ok.Size = new System.Drawing.Size(75, 23);
            this.ok.TabIndex = 1;
            this.ok.Text = CommonResources.Text.ActionOk;
            //this.ok.UseVisualStyleBackColor = true;
            this.ok.Click += new System.EventHandler(this.ok_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(13, 13);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            //this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox1.Size = new System.Drawing.Size(256, 77);
            this.textBox1.TabIndex = 2;
            // 
            // WarningDialog
            // 
            this.ClientSize = new System.Drawing.Size(281, 131);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.ok);
            this.Name = "WarningDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.WindowsDefaultBounds;
            this.Text = StringResources.Warning;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void ok_Click(object sender, EventArgs e)
        {
            Dispose();
        }
    }
}
