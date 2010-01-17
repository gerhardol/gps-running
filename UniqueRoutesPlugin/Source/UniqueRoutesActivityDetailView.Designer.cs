namespace SportTracksUniqueRoutesPlugin.Source
{
    partial class UniqueRoutesActivityDetailView
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
            this.summaryLabel = new System.Windows.Forms.Label();
            this.summaryView = new System.Windows.Forms.DataGridView();
            this.contextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.copyTable = new System.Windows.Forms.ToolStripMenuItem();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.doIt = new System.Windows.Forms.Button();
            this.speedBox = new System.Windows.Forms.ComboBox();
            this.sendResultToLabel1 = new System.Windows.Forms.Label();
            this.pluginBox = new System.Windows.Forms.ComboBox();
            this.selectedBox = new System.Windows.Forms.ComboBox();
            this.sendLabel2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.categoryLabel = new System.Windows.Forms.Label();
            this.changeCategory = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.summaryView)).BeginInit();
            this.contextMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // summaryLabel
            // 
            this.summaryLabel.AutoSize = true;
            this.summaryLabel.Location = new System.Drawing.Point(4, 4);
            this.summaryLabel.Name = "summaryLabel";
            this.summaryLabel.Size = new System.Drawing.Size(35, 13);
            this.summaryLabel.TabIndex = 0;
            this.summaryLabel.Text = "label1";
            // 
            // summaryView
            // 
            this.summaryView.AllowUserToAddRows = false;
            this.summaryView.AllowUserToDeleteRows = false;
            this.summaryView.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.summaryView.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.summaryView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.summaryView.ContextMenuStrip = this.contextMenu;
            this.summaryView.Location = new System.Drawing.Point(4, 117);
            this.summaryView.Name = "summaryView";
            this.summaryView.ReadOnly = true;
            this.summaryView.RowHeadersVisible = false;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.summaryView.RowsDefaultCellStyle = dataGridViewCellStyle2;
            this.summaryView.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.summaryView.Size = new System.Drawing.Size(383, 101);
            this.summaryView.TabIndex = 1;
            // 
            // contextMenu
            // 
            this.contextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.copyTable});
            this.contextMenu.Name = "contextMenu";
            this.contextMenu.Size = new System.Drawing.Size(197, 26);
            // 
            // copyTable
            // 
            this.copyTable.Name = "copyTable";
            this.copyTable.Size = new System.Drawing.Size(196, 22);
            this.copyTable.Text = "Copy table to clipboard";
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(4, 4);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(100, 23);
            this.progressBar.TabIndex = 2;
            this.progressBar.Visible = false;
            // 
            // doIt
            // 
            this.doIt.Location = new System.Drawing.Point(331, 34);
            this.doIt.Name = "doIt";
            this.doIt.Size = new System.Drawing.Size(56, 23);
            this.doIt.TabIndex = 3;
            this.doIt.Text = "Do it!";
            this.doIt.UseVisualStyleBackColor = true;
            this.doIt.Visible = false;
            this.doIt.Click += new System.EventHandler(this.highScoreButton_Click);
            // 
            // speedBox
            // 
            this.speedBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.speedBox.FormattingEnabled = true;
            this.speedBox.Location = new System.Drawing.Point(51, 61);
            this.speedBox.Name = "speedBox";
            this.speedBox.Size = new System.Drawing.Size(68, 21);
            this.speedBox.TabIndex = 5;
            this.speedBox.Visible = false;
            this.speedBox.SelectedIndexChanged += new System.EventHandler(this.speedBox_SelectedIndexChanged);
            // 
            // sendResultToLabel1
            // 
            this.sendResultToLabel1.AutoSize = true;
            this.sendResultToLabel1.Location = new System.Drawing.Point(13, 37);
            this.sendResultToLabel1.Name = "sendResultToLabel1";
            this.sendResultToLabel1.Size = new System.Drawing.Size(32, 13);
            this.sendResultToLabel1.TabIndex = 6;
            this.sendResultToLabel1.Text = "Send";
            // 
            // pluginBox
            // 
            this.pluginBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.pluginBox.FormattingEnabled = true;
            this.pluginBox.Location = new System.Drawing.Point(191, 34);
            this.pluginBox.Name = "pluginBox";
            this.pluginBox.Size = new System.Drawing.Size(134, 21);
            this.pluginBox.TabIndex = 7;
            this.pluginBox.SelectedIndexChanged += new System.EventHandler(this.pluginBox_SelectedIndexChanged);
            // 
            // selectedBox
            // 
            this.selectedBox.DisplayMember = "all";
            this.selectedBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.selectedBox.FormattingEnabled = true;
            this.selectedBox.Location = new System.Drawing.Point(51, 34);
            this.selectedBox.Name = "selectedBox";
            this.selectedBox.Size = new System.Drawing.Size(68, 21);
            this.selectedBox.TabIndex = 8;
            this.selectedBox.SelectedIndexChanged += new System.EventHandler(this.selectedBox_SelectedIndexChanged);
            // 
            // sendLabel2
            // 
            this.sendLabel2.AutoSize = true;
            this.sendLabel2.Location = new System.Drawing.Point(125, 39);
            this.sendLabel2.Name = "sendLabel2";
            this.sendLabel2.Size = new System.Drawing.Size(60, 13);
            this.sendLabel2.TabIndex = 9;
            this.sendLabel2.Text = "activities to";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 64);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(34, 13);
            this.label1.TabIndex = 10;
            this.label1.Text = "Show";
            // 
            // categoryLabel
            // 
            this.categoryLabel.AutoSize = true;
            this.categoryLabel.Location = new System.Drawing.Point(5, 93);
            this.categoryLabel.Name = "categoryLabel";
            this.categoryLabel.Size = new System.Drawing.Size(97, 13);
            this.categoryLabel.TabIndex = 11;
            this.categoryLabel.Text = "Include activities in";
            // 
            // changeCategory
            // 
            this.changeCategory.Location = new System.Drawing.Point(108, 88);
            this.changeCategory.Name = "changeCategory";
            this.changeCategory.Size = new System.Drawing.Size(97, 23);
            this.changeCategory.TabIndex = 13;
            this.changeCategory.Text = "Change category";
            this.changeCategory.UseVisualStyleBackColor = true;
            this.changeCategory.Click += new System.EventHandler(this.changeCategory_Click);
            // 
            // UniqueRoutesActivityDetailView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.categoryLabel);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.changeCategory);
            this.Controls.Add(this.sendLabel2);
            this.Controls.Add(this.selectedBox);
            this.Controls.Add(this.pluginBox);
            this.Controls.Add(this.sendResultToLabel1);
            this.Controls.Add(this.doIt);
            this.Controls.Add(this.speedBox);
            this.Controls.Add(this.summaryView);
            this.Controls.Add(this.summaryLabel);
            this.Name = "UniqueRoutesActivityDetailView";
            this.Size = new System.Drawing.Size(391, 222);
            ((System.ComponentModel.ISupportInitialize)(this.summaryView)).EndInit();
            this.contextMenu.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label summaryLabel;
        private System.Windows.Forms.DataGridView summaryView;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Button doIt;
        private System.Windows.Forms.ComboBox speedBox;
        private System.Windows.Forms.ContextMenuStrip contextMenu;
        private System.Windows.Forms.ToolStripMenuItem copyTable;
        private System.Windows.Forms.Label sendResultToLabel1;
        private System.Windows.Forms.ComboBox pluginBox;
        private System.Windows.Forms.ComboBox selectedBox;
        private System.Windows.Forms.Label sendLabel2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label categoryLabel;
        private System.Windows.Forms.Button changeCategory;
    }
}
