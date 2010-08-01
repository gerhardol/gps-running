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
using ZoneFiveSoftware.Common.Visuals.Fitness;
using ZoneFiveSoftware.Common.Data.Fitness;
using GpsRunningPlugin.Properties;
using GpsRunningPlugin.Util;

namespace GpsRunningPlugin.Source
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
            tree.Nodes.Add(StringResources.UseAllCategories);
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
            this.okButton.Text = ZoneFiveSoftware.Common.Visuals.CommonResources.Text.ActionOk;
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
            this.Text = StringResources.SelectCategory;
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
