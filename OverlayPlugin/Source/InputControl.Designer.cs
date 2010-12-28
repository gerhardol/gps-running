/*
Copyright (C) 2010 Staffan Nilsson

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
    partial class InputControl
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
            this.panel1 = new ZoneFiveSoftware.Common.Visuals.Panel();
            this.okButton = new ZoneFiveSoftware.Common.Visuals.Button();
            this.inputTextBox = new ZoneFiveSoftware.Common.Visuals.TextBox();
            this.InputDescriptionLabel = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.BorderColor = System.Drawing.Color.Gray;
            this.panel1.Controls.Add(this.InputDescriptionLabel);
            this.panel1.Controls.Add(this.inputTextBox);
            this.panel1.Controls.Add(this.okButton);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.HeadingBackColor = System.Drawing.Color.LightBlue;
            this.panel1.HeadingFont = null;
            this.panel1.HeadingLeftMargin = 0;
            this.panel1.HeadingText = null;
            this.panel1.HeadingTextColor = System.Drawing.Color.Black;
            this.panel1.HeadingTopMargin = 3;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(278, 150);
            this.panel1.TabIndex = 0;
            // 
            // okButton
            // 
            this.okButton.BackColor = System.Drawing.Color.Transparent;
            this.okButton.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(40)))), ((int)(((byte)(50)))), ((int)(((byte)(120)))));
            this.okButton.CenterImage = null;
            this.okButton.DialogResult = System.Windows.Forms.DialogResult.None;
            this.okButton.HyperlinkStyle = false;
            this.okButton.ImageMargin = 2;
            this.okButton.LeftImage = null;
            this.okButton.Location = new System.Drawing.Point(17, 87);
            this.okButton.Name = "okButton";
            this.okButton.PushStyle = true;
            this.okButton.RightImage = null;
            this.okButton.Size = new System.Drawing.Size(75, 23);
            this.okButton.TabIndex = 0;
            this.okButton.Text = "Ok";
            this.okButton.TextAlign = System.Drawing.StringAlignment.Center;
            this.okButton.TextLeftMargin = 2;
            this.okButton.TextRightMargin = 2;
            // 
            // inputTextBox
            // 
            this.inputTextBox.AcceptsReturn = false;
            this.inputTextBox.AcceptsTab = false;
            this.inputTextBox.BackColor = System.Drawing.Color.White;
            this.inputTextBox.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(123)))), ((int)(((byte)(114)))), ((int)(((byte)(108)))));
            this.inputTextBox.ButtonImage = null;
            this.inputTextBox.Location = new System.Drawing.Point(17, 50);
            this.inputTextBox.MaxLength = 32767;
            this.inputTextBox.Multiline = false;
            this.inputTextBox.Name = "inputTextBox";
            this.inputTextBox.ReadOnly = false;
            this.inputTextBox.ReadOnlyColor = System.Drawing.SystemColors.Control;
            this.inputTextBox.ReadOnlyTextColor = System.Drawing.SystemColors.ControlLight;
            this.inputTextBox.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.inputTextBox.Size = new System.Drawing.Size(100, 19);
            this.inputTextBox.TabIndex = 1;
            this.inputTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            // 
            // InputDescriptionLabel
            // 
            this.InputDescriptionLabel.AutoSize = true;
            this.InputDescriptionLabel.Location = new System.Drawing.Point(14, 20);
            this.InputDescriptionLabel.Name = "InputDescriptionLabel";
            this.InputDescriptionLabel.Size = new System.Drawing.Size(56, 13);
            this.InputDescriptionLabel.TabIndex = 2;
            this.InputDescriptionLabel.Text = "<Enter value";
            // 
            // InputControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.Controls.Add(this.panel1);
            this.Name = "InputControl";
            this.Size = new System.Drawing.Size(278, 150);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private ZoneFiveSoftware.Common.Visuals.Panel panel1;
        private System.Windows.Forms.Label InputDescriptionLabel;
        private ZoneFiveSoftware.Common.Visuals.TextBox inputTextBox;
        private ZoneFiveSoftware.Common.Visuals.Button okButton;
    }
}
