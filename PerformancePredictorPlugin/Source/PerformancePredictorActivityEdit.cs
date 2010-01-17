using System;
using System.Collections.Generic;
using System.Text;
using ZoneFiveSoftware.Common.Visuals.Fitness;
using ZoneFiveSoftware.Common.Data.Fitness;
using ZoneFiveSoftware.Common.Visuals;

namespace SportTracksPerformancePredictorPlugin.Source
{
    class PerformancePredictorActivityEdit : IExtendActivityEditActions
    {
        #region IExtendActivityEditActions Members

        public IList<IAction> GetActions(IList<IActivity> activities)
        {
            return new IAction[] { new PerformancePredictorAction(activities) };
        }

        public IList<IAction> GetActions(IActivity activity)
        {
            return new IAction[] { new PerformancePredictorAction(new IActivity[] { activity }) };
        }

        #endregion
    }
}
