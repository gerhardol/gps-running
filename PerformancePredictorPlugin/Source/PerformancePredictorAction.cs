using System;
using System.Collections.Generic;
using System.Text;
using ZoneFiveSoftware.Common.Data.Fitness;
using ZoneFiveSoftware.Common.Visuals;
using SportTracksPerformancePredictorPlugin.Properties;

namespace SportTracksPerformancePredictorPlugin.Source
{
    class PerformancePredictorAction : IAction
    {
        private IList<IActivity> activities;

        public PerformancePredictorAction(IList<IActivity> activities)
        {
            this.activities = activities;
        }

        #region IAction Members

        public bool Enabled
        {
            get { return Settings.highScore != null; }
        }

        public bool HasMenuArrow
        {
            get { return false; }
        }

        public System.Drawing.Image Image
        {
            get { return Properties.Resources.Image_16_PerformancePredictor; }
        }

        public void Refresh()
        {
        }

        public void Run(System.Drawing.Rectangle rectButton)
        {
            new PerformancePredictorView(activities);
        }

        public string Title
        {
            get
            {
                if (activities.Count == 1) return Resources.PPHS1;
                return String.Format(Resources.PPHS2,activities.Count);
            }
        }

        #endregion

        #region INotifyPropertyChanged Members

        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;

        #endregion
    }
}
