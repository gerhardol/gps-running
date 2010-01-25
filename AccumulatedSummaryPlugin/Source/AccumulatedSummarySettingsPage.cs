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

namespace SportTracksAccumulatedSummaryPlugin.Source
{
    class AccumulatedSummarySettingsPage: ISettingsPage
    {
        AccumulatedSummarySettings control = null;

        #region ISettingsPage Members

        public Guid Id
        {
            get
            {
                Plugin plugin = new SportTracksAccumulatedSummaryPlugin.Plugin();
                return plugin.Id;
            }
        }

        public IList<ISettingsPage> SubPages
        {
            get { return new List<ISettingsPage>(); }
        }

        #endregion

        #region IDialogPage Members

        public System.Windows.Forms.Control CreatePageControl()
        {
            if (control == null)
            {
                control = new AccumulatedSummarySettings();
            }
            return control;
        }

        public bool HidePage()
        {
            return true;
        }

        public string PageName
        {
            get { return "Accumulated Summary"; }
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
            get { return "Accumulated Summary"; }
        }

        public void UICultureChanged(System.Globalization.CultureInfo culture)
        {
        }

        #endregion

        #region INotifyPropertyChanged Members

#pragma warning disable 67
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;

        #endregion
    }
}
