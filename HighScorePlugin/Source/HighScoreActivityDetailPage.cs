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
using System.ComponentModel;

using ZoneFiveSoftware.Common.Visuals.Fitness;
using ZoneFiveSoftware.Common.Data.Fitness;
using ZoneFiveSoftware.Common.Visuals;
#if !ST_2_1
using ZoneFiveSoftware.Common.Data;
using ZoneFiveSoftware.Common.Visuals.Util;
#endif

namespace GpsRunningPlugin.Source
{
    class HighScoreActivityDetailPage : 
#if ST_2_1
     IActivityDetailPage
#else
     IDetailPage
#endif
    {
        #region IActivityDetailPage Members
#if !ST_2_1
        public HighScoreActivityDetailPage(IDailyActivityView view)
        {
            this.view = view;
            view.SelectionProvider.SelectedItemsChanged += new EventHandler(OnViewSelectedItemsChanged);
        }

        private void OnViewSelectedItemsChanged(object sender, EventArgs e)
        {
            activities = CollectionUtils.GetAllContainedItemsOfType<IActivity>(view.SelectionProvider.SelectedItems);
            if ((control != null))
            {
                control.Activities = activities;
            }
        }
        public System.Guid Id { get { return new Guid("{0af379d0-5ebe-11df-a08a-0800200c9a66}"); } }

#else
        public IActivity Activity
        {
            set
            {
                if (null == value) { activities = new List<IActivity>(); }
                else { activities = new List<IActivity> { value }; }
                if ((control != null))
                {
                    control.Activities = activities;
                }
            }
        }
#endif
        public IList<string> MenuPath
        {
            get { return menuPath; }
            set { menuPath = value; OnPropertyChanged("MenuPath"); }
        }

        public bool MenuEnabled
        {
            get { return menuEnabled; }
            set { menuEnabled = value; OnPropertyChanged("MenuEnabled"); }
        }

        public bool MenuVisible
        {
            get { return menuVisible; }
            set { menuVisible = value; OnPropertyChanged("MenuVisible"); }
        }

        public bool PageMaximized
        {
            get { return pageMaximized; }
            set { pageMaximized = value; OnPropertyChanged("PageMaximized"); }
        }
        public void RefreshPage()
        {
            if (control != null)
            {
                control.Refresh();
            }
        }

        #endregion

        #region IDialogPage Members

        public System.Windows.Forms.Control CreatePageControl()
        {
            if (control == null)
            {
                control = new HighScoreViewer(this, view);
            }
            control.Activities = activities;
            return control;
        }

        public bool HidePage()
        {
            if (control != null) { return control.HidePage(); }
            return true;
        }

        public string PageName
        {
            get
            {
                return Title;
            }
        }

        public void ShowPage(string bookmark)
        {
            if (control != null) { control.ShowPage(bookmark); }
        }

        public IPageStatus Status
        {
            get { return null; }
        }

        public void ThemeChanged(ITheme visualTheme)
        {
            //			m_visualTheme = visualTheme;
            if (control != null)
            {
                control.ThemeChanged(visualTheme);
            }
        }

        public string Title
        {
            get { return Properties.Resources.ApplicationName; }
        }

        public void UICultureChanged(System.Globalization.CultureInfo culture)
        {
            if (control != null)
            {
                control.UICultureChanged(culture);
            }
        }

        #endregion

        #region INotifyPropertyChanged Members

#pragma warning disable 67
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;

        #endregion

#if !ST_2_1
        private IDailyActivityView view = null;
#endif
        private IList<IActivity> activities = new List<IActivity>();
        private HighScoreViewer control = null;
        private IList<string> menuPath = null;
        private bool menuEnabled = true;
        private bool menuVisible = true;
        private bool pageMaximized = false;

        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
