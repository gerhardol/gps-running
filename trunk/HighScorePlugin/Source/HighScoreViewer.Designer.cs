namespace SportTracksHighScorePlugin.Source
{
    partial class HighScoreViewer
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dataGrid = new System.Windows.Forms.DataGridView();
            this.contextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.label1 = new System.Windows.Forms.Label();
            this.boundsBox = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.imageBox = new System.Windows.Forms.ComboBox();
            this.domainBox = new System.Windows.Forms.ComboBox();
            this.paceBox = new System.Windows.Forms.ComboBox();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.label3 = new System.Windows.Forms.Label();
            this.viewBox = new System.Windows.Forms.ComboBox();
            this.chart = new ZoneFiveSoftware.Common.Visuals.Chart.ChartBase();
            this.Remarks = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid)).BeginInit();
            this.contextMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGrid
            // 
            this.dataGrid.AllowUserToAddRows = false;
            this.dataGrid.AllowUserToDeleteRows = false;
            this.dataGrid.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.dataGrid.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGrid.ContextMenuStrip = this.contextMenu;
            this.dataGrid.Location = new System.Drawing.Point(6, 54);
            this.dataGrid.Name = "dataGrid";
            this.dataGrid.ReadOnly = true;
            this.dataGrid.RowHeadersVisible = false;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.dataGrid.RowsDefaultCellStyle = dataGridViewCellStyle2;
            this.dataGrid.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.dataGrid.Size = new System.Drawing.Size(514, 380);
            this.dataGrid.TabIndex = 2;
            this.dataGrid.Visible = false;
            // 
            // contextMenu
            // 
            this.contextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1});
            this.contextMenu.Name = "contextMenu";
            this.contextMenu.Size = new System.Drawing.Size(197, 26);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(196, 22);
            this.toolStripMenuItem1.Text = "Copy table to clipboard";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(22, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(27, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Find";
            // 
            // boundsBox
            // 
            this.boundsBox.FormattingEnabled = true;
            this.boundsBox.Location = new System.Drawing.Point(55, 4);
            this.boundsBox.Name = "boundsBox";
            this.boundsBox.Size = new System.Drawing.Size(66, 21);
            this.boundsBox.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(196, 7);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "per specified";
            // 
            // imageBox
            // 
            this.imageBox.FormattingEnabled = true;
            this.imageBox.Location = new System.Drawing.Point(269, 4);
            this.imageBox.Name = "imageBox";
            this.imageBox.Size = new System.Drawing.Size(139, 21);
            this.imageBox.TabIndex = 7;
            // 
            // domainBox
            // 
            this.domainBox.FormattingEnabled = true;
            this.domainBox.Location = new System.Drawing.Point(127, 4);
            this.domainBox.Name = "domainBox";
            this.domainBox.Size = new System.Drawing.Size(63, 21);
            this.domainBox.TabIndex = 8;
            // 
            // paceBox
            // 
            this.paceBox.FormattingEnabled = true;
            this.paceBox.Location = new System.Drawing.Point(55, 27);
            this.paceBox.Name = "paceBox";
            this.paceBox.Size = new System.Drawing.Size(66, 21);
            this.paceBox.TabIndex = 10;
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(6, 54);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(514, 23);
            this.progressBar.TabIndex = 12;
            this.progressBar.Visible = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(15, 30);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(34, 13);
            this.label3.TabIndex = 14;
            this.label3.Text = "Show";
            // 
            // viewBox
            // 
            this.viewBox.FormattingEnabled = true;
            this.viewBox.Location = new System.Drawing.Point(127, 27);
            this.viewBox.Name = "viewBox";
            this.viewBox.Size = new System.Drawing.Size(63, 21);
            this.viewBox.TabIndex = 15;
            // 
            // chart
            // 
            this.chart.BackColor = System.Drawing.Color.White;
            this.chart.Border = ZoneFiveSoftware.Common.Visuals.ControlBorder.Style.SmallRoundShadow;
            this.chart.Location = new System.Drawing.Point(6, 54);
            this.chart.Name = "chart";
            this.chart.Size = new System.Drawing.Size(514, 381);
            this.chart.TabIndex = 16;
            // 
            // Remarks
            // 
            this.Remarks.AutoSize = true;
            this.Remarks.Location = new System.Drawing.Point(196, 30);
            this.Remarks.Name = "Remarks";
            this.Remarks.Size = new System.Drawing.Size(49, 13);
            this.Remarks.TabIndex = 17;
            this.Remarks.Text = "Remarks";
            // 
            // HighScoreViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.Controls.Add(this.Remarks);
            this.Controls.Add(this.viewBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.paceBox);
            this.Controls.Add(this.domainBox);
            this.Controls.Add(this.imageBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.boundsBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dataGrid);
            this.Controls.Add(this.chart);
            this.Name = "HighScoreViewer";
            this.Size = new System.Drawing.Size(524, 437);
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid)).EndInit();
            this.contextMenu.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGrid;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox boundsBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox imageBox;
        private System.Windows.Forms.ComboBox domainBox;
        private System.Windows.Forms.ComboBox paceBox;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox viewBox;
        private System.Windows.Forms.ContextMenuStrip contextMenu;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private ZoneFiveSoftware.Common.Visuals.Chart.ChartBase chart;
        private System.Windows.Forms.Label Remarks;
    }
}
