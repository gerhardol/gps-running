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
using SportTracksTRIMPPlugin.Properties;

namespace SportTracksTRIMPPlugin.Source
{
    class YesNoDialog : Form
    {
        private ZoneFiveSoftware.Common.Visuals.Button yes;
        private ZoneFiveSoftware.Common.Visuals.Button no;
        private ZoneFiveSoftware.Common.Visuals.TextBox textBox1;
        public bool answer;
    
        public YesNoDialog(String message)
        {
            InitializeComponent();
            this.textBox1.Text = message;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
        }

        private void InitializeComponent()
        {
            this.yes = new ZoneFiveSoftware.Common.Visuals.Button();
            this.textBox1 = new ZoneFiveSoftware.Common.Visuals.TextBox();
            this.no = new ZoneFiveSoftware.Common.Visuals.Button();
            this.SuspendLayout();
            // 
            // yes
            // 
            this.yes.Location = new System.Drawing.Point(54, 96);
            this.yes.Name = "yes";
            this.yes.Size = new System.Drawing.Size(75, 23);
            this.yes.TabIndex = 1;
            this.yes.Text = CommonResources.Text.ActionYes;
            //this.yes.UseVisualStyleBackColor = true;
            this.yes.Click += new System.EventHandler(this.ok_Click);
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
            // no
            // 
            this.no.Location = new System.Drawing.Point(135, 96);
            this.no.Name = "no";
            this.no.Size = new System.Drawing.Size(75, 23);
            this.no.TabIndex = 3;
            this.no.Text = CommonResources.Text.ActionNo;
            //this.no.UseVisualStyleBackColor = true;
            this.no.Click += new System.EventHandler(this.no_Click);
            // 
            // YesNoDialog
            // 
            this.ClientSize = new System.Drawing.Size(281, 131);
            this.Controls.Add(this.no);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.yes);
            this.Name = "YesNoDialog";
            this.Text = Resources.Question;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void ok_Click(object sender, EventArgs e)
        {
            answer = true;
            Dispose();
        }

        private void no_Click(object sender, EventArgs e)
        {
            answer = false;
            Dispose();
        }
    }
}
