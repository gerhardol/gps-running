using System;
using System.Collections.Generic;
using System.Text;
using ZoneFiveSoftware.Common.Visuals;
using ZoneFiveSoftware.Common.Data.Fitness;
using SportTracksTRIMPPlugin.Properties;

namespace SportTracksTRIMPPlugin.Source
{
    class TRIMPAction: IAction
    {
        private IList<IActivity> activities;

        public TRIMPAction(IList<IActivity> activities)
        {
            this.activities = activities;
        }

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
            get { return Properties.Resources.Image_16_TRIMP; }
        }

        public void Refresh()
        {
        }

        public void Run(System.Drawing.Rectangle rectButton)
        {
            new TRIMPView(activities, true);
        }

        public string Title
        {
            get 
            {
                if (activities.Count == 1) return Resources.T1;
                return String.Format(Resources.T2, activities.Count); 
            }
        }

        #endregion

        #region INotifyPropertyChanged Members

        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;

        #endregion
    }
}
