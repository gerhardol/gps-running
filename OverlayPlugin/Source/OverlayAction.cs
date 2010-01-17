using System;
using System.Collections.Generic;
using System.Text;
using ZoneFiveSoftware.Common.Visuals;
using ZoneFiveSoftware.Common.Data.Fitness;
using SportTracksOverlayPlugin.Properties;

namespace SportTracksOverlayPlugin.Source
{
    class OverlayAction: IAction
    {
        private IList<IActivity> activities;

        public OverlayAction(IList<IActivity> activities)
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
            get { return Properties.Resources.Image_16_Overlay; }
        }

        public void Refresh()
        {
        }

        public void Run(System.Drawing.Rectangle rectButton)
        {
            new OverlayView(activities);
        }

        public string Title
        {
            get 
            {
                if (activities.Count == 1) return Resources.O1;
                return String.Format(Resources.O2,activities.Count); 
            }
        }

        #endregion

        #region INotifyPropertyChanged Members

        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;

        #endregion
    }
}
