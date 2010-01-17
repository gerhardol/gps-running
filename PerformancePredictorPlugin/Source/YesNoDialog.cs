using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace SportTracksPerformancePredictorPlugin.Source
{
    class YesNoDialog : Form
    {
        private Button yes;
        private Button no;
        private TextBox textBox1;
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
            this.yes = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.no = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // yes
            // 
            this.yes.Location = new System.Drawing.Point(54, 96);
            this.yes.Name = "yes";
            this.yes.Size = new System.Drawing.Size(75, 23);
            this.yes.TabIndex = 1;
            this.yes.Text = "Yes";
            this.yes.UseVisualStyleBackColor = true;
            this.yes.Click += new System.EventHandler(this.ok_Click);
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
            // no
            // 
            this.no.Location = new System.Drawing.Point(135, 96);
            this.no.Name = "no";
            this.no.Size = new System.Drawing.Size(75, 23);
            this.no.TabIndex = 3;
            this.no.Text = "No";
            this.no.UseVisualStyleBackColor = true;
            this.no.Click += new System.EventHandler(this.no_Click);
            // 
            // YesNoDialog
            // 
            this.ClientSize = new System.Drawing.Size(281, 131);
            this.Controls.Add(this.no);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.yes);
            this.Name = "YesNoDialog";
            this.Text = "Question?";
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
