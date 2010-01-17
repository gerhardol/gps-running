using System;
using System.Collections.Generic;
using System.Text;
using ZoneFiveSoftware.Common.Visuals.Fitness;
using ZoneFiveSoftware.Common.Data.Fitness;
using System.Windows.Forms;
using ZoneFiveSoftware.Common.Visuals;
using System.Globalization;

namespace SportTracksPerformancePredictorPlugin.Source
{
    class PerformancePredictorActivityDetailPage : IActivityDetailPage
    {
        private IActivity activity = null;
        private PerformancePredictorView control = null;

        #region IActivityDetailPage Members

        public IActivity Activity
        {
            set
            {
                this.activity = value;
                if (control != null)
                    this.control.Activity = value;
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
                control = new PerformancePredictorView(activity);
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
            get { return "Performance Predictor"; }
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