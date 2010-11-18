namespace GpsRunningPlugin.Util
{
    partial class InputDialog
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.inputDescriptionLabel = new System.Windows.Forms.Label();
            this.inputTextBox = new ZoneFiveSoftware.Common.Visuals.TextBox();
            this.okButton = new ZoneFiveSoftware.Common.Visuals.Button();
            this.cancelButton = new ZoneFiveSoftware.Common.Visuals.Button();
            this.SuspendLayout();
            // 
            // inputDescriptionLabel
            // 
            this.inputDescriptionLabel.AutoSize = true;
            this.inputDescriptionLabel.Location = new System.Drawing.Point(12, 9);
            this.inputDescriptionLabel.Name = "inputDescriptionLabel";
            this.inputDescriptionLabel.Size = new System.Drawing.Size(56, 13);
            this.inputDescriptionLabel.TabIndex = 5;
            this.inputDescriptionLabel.Text = "Enter XXX";
            // 
            // inputTextBox
            // 
            this.inputTextBox.AcceptsReturn = false;
            this.inputTextBox.AcceptsTab = false;
            this.inputTextBox.BackColor = System.Drawing.Color.White;
            this.inputTextBox.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(123)))), ((int)(((byte)(114)))), ((int)(((byte)(108)))));
            this.inputTextBox.ButtonImage = null;
            this.inputTextBox.Location = new System.Drawing.Point(15, 39);
            this.inputTextBox.MaxLength = 32767;
            this.inputTextBox.Multiline = false;
            this.inputTextBox.Name = "inputTextBox";
            this.inputTextBox.ReadOnly = false;
            this.inputTextBox.ReadOnlyColor = System.Drawing.SystemColors.Control;
            this.inputTextBox.ReadOnlyTextColor = System.Drawing.SystemColors.ControlLight;
            this.inputTextBox.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.inputTextBox.Size = new System.Drawing.Size(100, 19);
            this.inputTextBox.TabIndex = 4;
            this.inputTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
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
            this.okButton.Location = new System.Drawing.Point(15, 76);
            this.okButton.Name = "okButton";
            this.okButton.PushStyle = true;
            this.okButton.RightImage = null;
            this.okButton.Size = new System.Drawing.Size(75, 23);
            this.okButton.TabIndex = 3;
            this.okButton.Text = "Ok";
            this.okButton.TextAlign = System.Drawing.StringAlignment.Center;
            this.okButton.TextLeftMargin = 2;
            this.okButton.TextRightMargin = 2;
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.BackColor = System.Drawing.Color.Transparent;
            this.cancelButton.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(40)))), ((int)(((byte)(50)))), ((int)(((byte)(120)))));
            this.cancelButton.CenterImage = null;
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.HyperlinkStyle = false;
            this.cancelButton.ImageMargin = 2;
            this.cancelButton.LeftImage = null;
            this.cancelButton.Location = new System.Drawing.Point(109, 76);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.PushStyle = true;
            this.cancelButton.RightImage = null;
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 6;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.TextAlign = System.Drawing.StringAlignment.Center;
            this.cancelButton.TextLeftMargin = 2;
            this.cancelButton.TextRightMargin = 2;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // InputDialog
            // 
            this.AcceptButton = this.okButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.CancelButton = this.cancelButton;
            this.ClientSize = new System.Drawing.Size(214, 127);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.inputDescriptionLabel);
            this.Controls.Add(this.inputTextBox);
            this.Controls.Add(this.okButton);
            this.Name = "InputDialog";
            this.Text = "InputDialog";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label inputDescriptionLabel;
        private ZoneFiveSoftware.Common.Visuals.TextBox inputTextBox;
        private ZoneFiveSoftware.Common.Visuals.Button okButton;
        private ZoneFiveSoftware.Common.Visuals.Button cancelButton;
    }
}