using System;
using System.Collections.Generic;
using System.Text;
using ZoneFiveSoftware.Common.Visuals;
using ZoneFiveSoftware.Common.Visuals.Fitness;
using ZoneFiveSoftware.Common.Data.Fitness;

namespace SportTracksAccumulatedSummaryPlugin.Source
{
    class AccumulatedSummaryEdit : IExtendActivityEditActions
    {

        #region IExtendActivityEditActions Members

        public IList<IAction> GetActions(IList<IActivity> activities)
        {
            return new IAction[] { new AccumulatedSummaryAction(activities) };
        }

        public IList<IAction> GetActions(IActivity activity)
        {
            return new IAction[] { new AccumulatedSummaryAction(new IActivity[] { activity }) };
        }

        #endregion
    }
}
