using System;
using System.Collections.Generic;
using System.Text;
using ZoneFiveSoftware.Common.Visuals;
using ZoneFiveSoftware.Common.Data.Fitness;
using SportTracksAccumulatedSummaryPlugin.Properties;

namespace SportTracksAccumulatedSummaryPlugin.Source
{
    class AccumulatedSummaryAction: IAction
    {
        private IList<IActivity> activities;

        public AccumulatedSummaryAction(IList<IActivity> activities)
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
            get { return Properties.Resources.Image_16_AccumulatedSummary; }
        }

        public void Refresh()
        {
        }

        public void Run(System.Drawing.Rectangle rectButton)
        {
            new AccumulatedSummaryView(activities);
        }

        public string Title
        {
            get 
            {
                if (activities.Count == 1) return Resources.AS1;
                return String.Format(Resources.AS2,activities.Count); 
            }
        }

        #endregion

        #region INotifyPropertyChanged Members

        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;

        #endregion
    }
}
