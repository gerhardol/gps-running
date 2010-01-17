using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using ZoneFiveSoftware.Common.Visuals.Fitness;
using ZoneFiveSoftware.Common.Data.Fitness;
using SportTracksUniqueRoutesPlugin.Properties;

namespace SportTracksUniqueRoutesPlugin.Source
{
    class CategorySelector : Form
    {
        private TreeView tree;
        private Button okButton;
        private IDictionary<TreeNode, IActivityCategory> node2category;
        
        public CategorySelector()
        {
            InitializeComponent();
            node2category = new Dictionary<TreeNode, IActivityCategory>();
            tree.Nodes.Add(Resources.UseAllCategories);
            foreach (IActivityCategory category in Plugin.GetApplication().Logbook.ActivityCategories)
            {  
                addNode(category, null);
            }
            tree.NodeMouseDoubleClick += new TreeNodeMouseClickEventHandler(tree_NodeMouseClick);
            ShowDialog();
        }

        private void tree_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (node2category.ContainsKey(tree.SelectedNode))
                Settings.SelectedCategory = node2category[tree.SelectedNode];
            else
                Settings.SelectedCategory = null;
            Dispose();
        }

        private void addNode(IActivityCategory category, TreeNode parent)
        {
            TreeNode node = new TreeNode(category.Name);
            if (parent == null)
                tree.Nodes.Add(node);
            else
                parent.Nodes.Add(node);
            node2category.Add(node, category);
            foreach (IActivityCategory subcategory in category.SubCategories)
            {
                addNode(subcategory, node);
            }
        }

        private void InitializeComponent()
        {
            this.tree = new System.Windows.Forms.TreeView();
            this.okButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // tree
            // 
            this.tree.Location = new System.Drawing.Point(12, 12);
            this.tree.Name = "tree";
            this.tree.Size = new System.Drawing.Size(168, 170);
            this.tree.TabIndex = 0;
            // 
            // okButton
            // 
            this.okButton.Location = new System.Drawing.Point(12, 188);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(168, 23);
            this.okButton.TabIndex = 1;
            this.okButton.Text = Resources.Ok;
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // CategorySelector
            // 
            this.ClientSize = new System.Drawing.Size(192, 223);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.tree);
            this.MaximumSize = new System.Drawing.Size(200, 257);
            this.MinimumSize = new System.Drawing.Size(200, 257);
            this.Name = "CategorySelector";
            this.Text = Resources.SelectCategory;
            this.ResumeLayout(false);

        }

        private void okButton_Click(object sender, EventArgs e)
        {
            if (node2category.ContainsKey(tree.SelectedNode))
                Settings.SelectedCategory = node2category[tree.SelectedNode];
            else
                Settings.SelectedCategory = null;
            Dispose();
        }
    }
}
