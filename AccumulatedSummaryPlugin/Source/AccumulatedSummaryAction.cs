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
using GpsRunningPlugin.Properties;
#if !ST_2_1
using ZoneFiveSoftware.Common.Data;
using ZoneFiveSoftware.Common.Visuals.Fitness;
using ZoneFiveSoftware.Common.Visuals.Util;
#endif

namespace GpsRunningPlugin.Source
{
    class AccumulatedSummaryAction: IAction
    {
#if !ST_2_1
        public AccumulatedSummaryAction(IDailyActivityView view)
        {
            this.m_dailyView = view;
        }
        public AccumulatedSummaryAction(IActivityReportsView view)
        {
            this.m_reportView = view;
        }
#else
        public AccumulatedSummaryAction(IList<IActivity> activities)
        {
            this.activities = activities;
        }
#endif

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
                return String.Format(Resources.AS2, activities.Count); 
            }
        }
        private bool firstRun = true;
        public bool Visible
        {
            get
            {
                //Analyze menu must be Visible at first call, otherwise it is hidden
                //Could be done with listeners too
                if (true == firstRun) { firstRun = false; return true; }
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
        private IDailyActivityView m_dailyView = null;
        private IActivityReportsView m_reportView = null;
#endif
        private IList<IActivity> m_activities = null;
        private IList<IActivity> activities
        {
            get
            {
#if !ST_2_1
                //activities are set either directly or by selection,
                //not by more than one view
                if (m_activities == null)
                {
                    if (m_dailyView != null)
                    {
                        return CollectionUtils.GetAllContainedItemsOfType<IActivity>(m_dailyView.SelectionProvider.SelectedItems); 
                    }
                    else if (m_reportView != null)
                    {
                        return CollectionUtils.GetAllContainedItemsOfType<IActivity>(m_reportView.SelectionProvider.SelectedItems);
                    }
                    else
                    {
                        return new List<IActivity>();
                    }
                }
#endif
                return m_activities;
            }
            set
            {
                m_activities = value;
            }
        }
    }
}
