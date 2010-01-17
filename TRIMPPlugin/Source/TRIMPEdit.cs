using System;
using System.Collections.Generic;
using System.Text;
using ZoneFiveSoftware.Common.Visuals;
using ZoneFiveSoftware.Common.Visuals.Fitness;
using ZoneFiveSoftware.Common.Data.Fitness;

namespace SportTracksTRIMPPlugin.Source
{
    class TRIMPEdit : IExtendActivityEditActions
    {

        #region IExtendActivityEditActions Members

        public IList<IAction> GetActions(IList<IActivity> activities)
        {
            return new IAction[] { new TRIMPAction(activities) };
        }

        public IList<IAction> GetActions(IActivity activity)
        {
            return new IAction[] { new TRIMPAction(new IActivity[] { activity }) };
        }

        #endregion
    }
}
