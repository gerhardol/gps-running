using System;
using System.Collections.Generic;
using System.Text;
using ZoneFiveSoftware.Common.Visuals;
using ZoneFiveSoftware.Common.Visuals.Fitness;
using ZoneFiveSoftware.Common.Data.Fitness;

namespace SportTracksOverlayPlugin.Source
{
    class OverlayEdit : IExtendActivityEditActions
    {

        #region IExtendActivityEditActions Members

        public IList<IAction> GetActions(IList<IActivity> activities)
        {
            if (activities.Count > 0)
                return new IAction[] { new OverlayAction(activities) };
            return new IAction[] { };
        }

        public IList<IAction> GetActions(IActivity activity)
        {
            return new IAction[] { new OverlayAction(new IActivity[] { activity }) };
        }

        #endregion
    }
}
