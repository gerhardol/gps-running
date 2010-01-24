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
using ZoneFiveSoftware.Common.Visuals;

namespace SportTracksHighScorePlugin.Source
{
    class HighScoreActivityDetailPage : IActivityDetailPage
    {
        private IActivity activity = null;
        private HighScoreViewer control = null;

        #region IActivityDetailPage Members

        public IActivity Activity
        {
            set 
            {
                this.activity = value;
                if (value != null && control != null)
                    this.control.Activities = new IActivity[] { value };
            }
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
                IList<IActivity> list = new List<IActivity>();
                if (activity != null)
                    list.Add(activity);
                control = new HighScoreViewer(list, false, false);
            }
            return control;
        }

        public bool HidePage()
        {
            return true;
        }

        public string PageName
        {
            get { return Title; }
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
            RefreshPage();
        }

        public string Title
        {
            get { return "High Score"; }
        }

        public void UICultureChanged(System.Globalization.CultureInfo culture)
        {
            RefreshPage();
        }

        #endregion

        #region INotifyPropertyChanged Members

        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;

        #endregion
    }
}
