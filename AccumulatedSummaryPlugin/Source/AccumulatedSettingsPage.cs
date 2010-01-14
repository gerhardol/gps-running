using System;
using System.Collections.Generic;
using System.Text;
using ZoneFiveSoftware.Common.Visuals.Fitness;
using ZoneFiveSoftware.Common.Visuals;

namespace SportTracksAccumulatedSummaryPlugin.Source
{
    class AccumulatedSettingsPage : IExtendSettingsPages
    {
        #region IExtendSettingsPages Members

        public IList<ISettingsPage> SettingsPages
        {
            get { return new ISettingsPage[] { new AccumulatedSummarySettingsPage() }; }
        }

        #endregion
    }
}
