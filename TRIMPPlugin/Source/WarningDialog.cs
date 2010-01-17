using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using SportTracksTRIMPPlugin.Properties;

namespace SportTracksTRIMPPlugin.Source
{
    class WarningDialog : Form
    {
        private Button ok;
        private TextBox textBox1;
    
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
            this.ok = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // ok
            // 
            this.ok.Location = new System.Drawing.Point(102, 96);
            this.ok.Name = "ok";
            this.ok.Size = new System.Drawing.Size(75, 23);
            this.ok.TabIndex = 1;
            this.ok.Text = Resources.Ok;
            this.ok.UseVisualStyleBackColor = true;
            this.ok.Click += new System.EventHandler(this.ok_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(13, 13);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
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
            this.Text = Resources.Warning;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void ok_Click(object sender, EventArgs e)
        {
            Dispose();
        }
    }
}
