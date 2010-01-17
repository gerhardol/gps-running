using System;
using System.Collections.Generic;
using System.Text;
using ZoneFiveSoftware.Common.Visuals;
using SportTracksUniqueRoutesPlugin.Properties;

namespace SportTracksUniqueRoutesPlugin.Source
{
    class UniqueRoutesPage : ISettingsPage
    {
        private UniqueRoutesSettingPageControl control = null;

        #region ISettingsPage Members

        public Guid Id
        {
            get { return new Guid(Resources.UniqueRoutesGuid); }
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
                control = new UniqueRoutesSettingPageControl();
            }
            return control;
        }

        public bool HidePage()
        {
            return true;
        }

        public string PageName
        {
            get { return "Unique Routes"; }
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
            if (control != null)
            {
                //control.ThemeChanged(visualTheme);
            }
        }

        public string Title
        {
            get { return "Unique Routes"; }
        }

        public void UICultureChanged(System.Globalization.CultureInfo culture)
        {
            if (control != null)
            {
                
            }
        }

        #endregion

        #region INotifyPropertyChanged Members

        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;

        #endregion
    }
}
