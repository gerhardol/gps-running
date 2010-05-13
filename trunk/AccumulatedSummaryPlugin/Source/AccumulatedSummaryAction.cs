/*
Copyright (C) 2007, 2008 Kristian Bisgaard Lassen
Copyright (C) 2010 Kristian Helkjaer Lassen
Copyright (C) 2010 Gerhard Olsson

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
using SportTracksAccumulatedSummaryPlugin.Properties;
#if !ST_2_1
using ZoneFiveSoftware.Common.Visuals.Fitness;
using ZoneFiveSoftware.Common.Visuals.Util;
#endif

namespace SportTracksAccumulatedSummaryPlugin.Source
{
    class AccumulatedSummaryAction: IAction
    {
#if !ST_2_1
        public AccumulatedSummaryAction(IDailyActivityView view)
        {
            this.dailyView = view;
        }
        public AccumulatedSummaryAction(IActivityReportsView view)
        {
            this.reportView = view;
        }
#endif
        public AccumulatedSummaryAction(IList<IActivity> activities)
        {
            this._activities = activities;
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
            get { return Properties.Resources.Image_16_AccumulatedSummary; }
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
            new AccumulatedSummaryView(activities);
        }

        public string Title
        {
            get 
            {
                if (activities.Count == 1) return Resources.AS1;
                return String.Format(Resources.AS2,activities.Count); 
            }
        }
        public bool Visible
        {
            get
            {
                return true;
            }
        }

        #endregion

#if !ST_2_1
        private IDailyActivityView dailyView = null;
        private IActivityReportsView reportView = null;
#endif
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
                        return CollectionUtils.GetItemsOfType<IActivity>(dailyView.SelectionProvider.SelectedItems);
                    }
                    else if (reportView != null)
                    {
                        return CollectionUtils.GetItemsOfType<IActivity>(reportView.SelectionProvider.SelectedItems);
                    }
                    else
                    {
                        return new List<IActivity>();
                    }
                }
#endif
                return _activities;
            }
        }
        private IList<IActivity> _activities = null;
        #region INotifyPropertyChanged Members

#pragma warning disable 67
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;

        #endregion
    }
}
