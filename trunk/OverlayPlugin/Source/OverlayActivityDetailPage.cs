using System;
using System.Collections.Generic;
using System.Text;
using ZoneFiveSoftware.Common.Visuals.Fitness;
using ZoneFiveSoftware.Common.Data.Fitness;
using ZoneFiveSoftware.Common.Visuals;
using System.Globalization;
using System.Windows.Forms;
using SportTracksOverlayPlugin.Properties;

namespace SportTracksOverlayPlugin.Source
{
    class OverlayActivityDetailPage : IActivityDetailPage
    {
        private IActivity activity = null;
        private OverlayView control = null;

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

        public Control CreatePageControl()
        {
            if (control == null)
            {
                IList<IActivity> list = new List<IActivity>();
                if (activity != null)
                    list.Add(activity);
                control = new OverlayView(list, false);
            }
            return control;
        }

        public bool HidePage()
        {
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
        }

        public IPageStatus Status
        {
            get
            {
                return null;
            }
        }

        public void ThemeChanged(ITheme visualTheme)
        {
            RefreshPage();
        }

        public string Title
        {
            get
            {
                return Resources.O1;
            }
        }

        public void UICultureChanged(CultureInfo culture)
        {
            RefreshPage();
        }

        #endregion

        #region INotifyPropertyChanged Members

        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;

        #endregion
    }
}
