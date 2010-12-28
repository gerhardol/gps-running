/*
Copyright (C) 2008, 2009 Henrik Naess

This library is free software; you can redistribute it and/or
modify it under the terms of the GNU Lesser General Public
License as published by the Free Software Foundation; either
version 3 of the License, or (at your option) any later version.

This library is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU
Lesser General Public License for more details.

You should have received a copy of the GNU Lesser General Public
License along with this library. If not, see <http://www.gnu.org/licenses/>.
*/

using System;
using System.Collections.Generic;
using ZoneFiveSoftware.Common.Data;
using ZoneFiveSoftware.Common.Data.Fitness;
using ZoneFiveSoftware.Common.Data.GPS;
using ZoneFiveSoftware.Common.Visuals;

namespace GpsRunningPlugin
{
    public class ActivityCategoryContentProvider : IContentProvider
    {
        IList<object> list = new List<object>();
        public ActivityCategoryContentProvider(IList<object> list)
        {
            this.list = list;
        }

        public System.Collections.IList GetChildren(object parentElement)
        {
            System.Collections.IList result = new System.Collections.ArrayList();
            if (parentElement is IActivityCategory)
            {
                IActivityCategory parent = parentElement as IActivityCategory;
                foreach (IActivityCategory category in parent.SubCategories)
                {
                    result.Add(category);
                }
            }
            return result;
        }

        public System.Collections.IList GetElements(object inputElement)
        {
            System.Collections.IList result = new System.Collections.ArrayList();
            foreach (object obj in list)
            {
                result.Add(obj);
            }
            return result;
        }

        public object GetParent(object element)
        {
            object result = null;
            if (element is IActivityCategory)
            {
                return (element as IActivityCategory).Parent;
            }
            return result;
        }

        public bool HasChildren(object element)
        {
            if (element is IActivityCategory)
            {
                return (element as IActivityCategory).SubCategories.Count > 0;
            }
            return false;
        }

        public void InputChanged(object oldInput, object newInput)
        {
            list = newInput as IList<object>;
        }
    }

    /*    public class ActivityCategoryPopup
        {

            private void addNode(IActivityCategory category, System.Collections.IList parentCategories)
            {
                if (parentCategories == null)
                    if (category.SubCategories.Count > 0) parentCategories.Add(category);
                foreach (IActivityCategory subcategory in category.SubCategories)
                {
                    addNode(subcategory, parentCategories);
                }
            }

            private void boxCategory_ButtonClicked(object sender, EventArgs e)
            {
                TreeListPopup treeListPopup = new TreeListPopup();
                treeListPopup.ThemeChanged(m_visualTheme);
                treeListPopup.Tree.Columns.Add(new TreeList.Column());

                IList<object> list = new List<object>();
                //MatrixPlugin.GetApplication().Logbook.ActivityCategories;
                string useAllCat = Properties.Resources.UseAllCategories;
                foreach (IActivityCategory category in MatrixPlugin.GetApplication().Logbook.ActivityCategories)
                {
                    list.Add(category);
                }
                list.Add(useAllCat);
                treeListPopup.Tree.RowData = list;
                treeListPopup.Tree.ContentProvider = new ActivityCategoryContentProvider(list);
                //            treeListPopup.Tree.RowData = MatrixPlugin.GetApplication().Logbook.ActivityCategories;
                //          treeListPopup.Tree.ContentProvider = new ActivityCategoryContentProvider(MatrixPlugin.GetApplication().Logbook.ActivityCategories);
                treeListPopup.Tree.ShowPlusMinus = true;
                treeListPopup.FitContent = false;

                if (Settings.SelectedCategory != null)
                {
                    treeListPopup.Tree.Selected = new object[] { Settings.SelectedCategory };
                }
                System.Collections.IList parentCategories = new System.Collections.ArrayList();
                foreach (IActivityCategory category in MatrixPlugin.GetApplication().Logbook.ActivityCategories)
                {
                    addNode(category, parentCategories);
                }
                treeListPopup.Tree.Expanded = parentCategories;

                treeListPopup.ItemSelected += delegate(object sender2, TreeListPopup.ItemSelectedEventArgs e2)
                {
                    if (e2.Item is IActivityCategory)
                    {
                        Settings.SelectedCategory = (IActivityCategory)e2.Item;
                    }
                    else
                    {
                        Settings.SelectedCategory = null;
                    }
                    setCategoryLabel();
                    RefreshDataChanged("");
                };
                treeListPopup.Popup(this.boxCategory.Parent.RectangleToScreen(this.boxCategory.Bounds));
            }
        }
     * */
}