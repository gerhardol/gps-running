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
using ZoneFiveSoftware.Common.Visuals.Fitness;
using ZoneFiveSoftware.Common.Data.Fitness;
using System.Windows.Forms;
using ZoneFiveSoftware.Common.Visuals;
using System.Globalization;

namespace SportTracksTRIMPPlugin.Source
{
    class TRIMPActivityDetailPage : IActivityDetailPage
    {
        #region IActivityDetailPage Members
        private TRIMPView view;
        private IActivity activity;

        public IActivity Activity
        {
            set 
            {
                activity = value;
                if (view != null)
                {
                    if (value == null)
                    {
                        view.Activities = new List<IActivity>();
                    }
                    else
                    {
                        view.Activities = new IActivity[] { value };
                    }
                }
            }
        }

        public void RefreshPage()
        {
        }

        #endregion

        #region IDialogPage Members

        public Control CreatePageControl()
        {
            if (view == null)
            {
                if (activity == null)
                {
                    view = new TRIMPView(new List<IActivity>(), false);
                }
                else
                {
                    view = new TRIMPView(new IActivity[] { activity }, false);
                }
            }
            return view;
        }

        public bool HidePage()
        {
            return true;
        }

        public string PageName
        {
            get { return "TRIMP"; }
        }

        public void ShowPage(string bookmark)
        {
        }

        public IPageStatus Status
        {
            get { return null; }
        }

        public void ThemeChanged(ITheme visualTheme)
        {
        }

        public string Title
        {
            get { return "TRIMP"; }
        }

        public void UICultureChanged(CultureInfo culture)
        {           
        }

        #endregion

        #region INotifyPropertyChanged Members

        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;

        #endregion
    }
}
