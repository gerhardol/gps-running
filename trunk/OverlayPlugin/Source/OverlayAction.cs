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
using ZoneFiveSoftware.Common.Visuals;
using ZoneFiveSoftware.Common.Data.Fitness;
using SportTracksOverlayPlugin.Properties;
#if !ST_2_1
using ZoneFiveSoftware.Common.Data;
using ZoneFiveSoftware.Common.Visuals.Fitness;
using ZoneFiveSoftware.Common.Visuals.Util;
#endif

namespace SportTracksOverlayPlugin.Source
{
    class OverlayAction: IAction
    {
#if !ST_2_1
        public OverlayAction(IDailyActivityView view)
        {
            this.dailyView = view;
        }
        public OverlayAction(IActivityReportsView view)
        {
            this.reportView = view;
        }
#endif

        public OverlayAction(IList<IActivity> activities)
        {
            this.activities = activities;
        }

        #region IAction Members

        public bool Enabled
        {
            get { return activities.Count > 0; }
        }

        public bool HasMenuArrow
        {
            get { return false; }
        }

        public System.Drawing.Image Image
        {
            get { return Properties.Resources.Image_16_Overlay; }
        }

        public IList<string> MenuPath
        {
            get
            {
                return new List<string>();
            }
        }
        public void Refresh()
        {
        }

        public void Run(System.Drawing.Rectangle rectButton)
        {
#if OVERLAY_REDESIGN
            new OverlayView2(activities);
#else            
            new OverlayView(activities);
#endif
        }

        public string Title
        {
            get 
            {
                if (activities.Count == 1) return Resources.O1;
                return String.Format(Resources.O2,activities.Count); 
            }
        }
        public bool Visible
        {
            get
            {
                if (activities.Count == 0) return false;
                return true;
            }
        }

        #endregion

        #region INotifyPropertyChanged Members

#pragma warning disable 67
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;

        #endregion
#if !ST_2_1
        IList<ItemType> GetAllContainedItems<ItemType>(ISelectionProvider selectionProvider)
        {
            List<ItemType> items = new List<ItemType>();
            foreach (ItemType item in CollectionUtils.GetItemsOfType<ItemType>(selectionProvider.SelectedItems))
            {
                if (!items.Contains(item)) items.Add(item);
            }
            AddGroupItems<ItemType>(CollectionUtils.GetItemsOfType<IGroupedItem<ItemType>>(
                                    selectionProvider.SelectedItems), items);
            return items;
        }

        void AddGroupItems<ItemType>(IList<IGroupedItem<ItemType>> groups, IList<ItemType> allItems)
        {
            foreach (IGroupedItem<ItemType> group in groups)
            {
                foreach (ItemType item in group.Items)
                {
                    if (!allItems.Contains(item)) allItems.Add(item);
                }
                AddGroupItems(group.SubGroups, allItems);
            }
        }
#endif
#if !ST_2_1
        private IDailyActivityView dailyView = null;
        private IActivityReportsView reportView = null;
#endif
        private IList<IActivity> _activities = null;
        private IList<IActivity> activities
        {
            get
            {
#if !ST_2_1
                //activities are set either directly or by selection,
                //not by more than one
                if (_activities == null)
                {
                    if (dailyView != null)
                    {
                        return GetAllContainedItems<IActivity>(dailyView.SelectionProvider);
                    }
                    else if (reportView != null)
                    {
                        return GetAllContainedItems<IActivity>(reportView.SelectionProvider);
                    }
                    else
                    {
                        return new List<IActivity>();
                    }
                }
#endif
                return _activities;
            }
            set
            {
                _activities = value;
            }
        }
    }
}
