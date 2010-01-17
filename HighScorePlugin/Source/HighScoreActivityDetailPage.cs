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
