using System;
using System.Collections.Generic;
using System.Text;
using ZoneFiveSoftware.Common.Visuals.Fitness;
using ZoneFiveSoftware.Common.Data.Fitness;
using ZoneFiveSoftware.Common.Visuals;

namespace SportTracksHighScorePlugin.Source
{
    class HighScoreActivityEdit : IExtendActivityEditActions
    {

        #region IExtendActivityEditActions Members

        public IList<IAction> GetActions(IList<IActivity> activities)
        {
            return new IAction[] { new HighScoreAction(activities) };
        }

        public IList<IAction> GetActions(IActivity activity)
        {
            return new IAction[] { new HighScoreAction(new IActivity[] { activity }) };
        }

        #endregion
    }
}
