using System;
using System.Collections.Generic;
using System.Text;
using ZoneFiveSoftware.Common.Visuals;

namespace SportTracksOverlayPlugin.Source
{
    class OverlaySettingsPage: ISettingsPage
    {
        OverlaySettings control = null;

        #region ISettingsPage Members

        public Guid Id
        {
            get { return new Guid(SportTracksOverlayPlugin.Properties.Resources.Overlay); }
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
                control = new OverlaySettings();
            }
            return control;
        }

        public bool HidePage()
        {
            return true;
        }

        public string PageName
        {
            get { return "Overlay"; }
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
            get { return "Overlay"; }
        }

        public void UICultureChanged(System.Globalization.CultureInfo culture)
        {
        }

        #endregion

        #region INotifyPropertyChanged Members

        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;

        #endregion
    }
}
