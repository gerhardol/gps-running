using System;
using System.Collections.Generic;
using System.Text;
using ZoneFiveSoftware.Common.Visuals.Fitness;
using ZoneFiveSoftware.Common.Visuals;

namespace SportTracksTRIMPPlugin.Source
{
    class TRIMPPage : IExtendSettingsPages
    {
        #region IExtendSettingsPages Members

        public IList<ISettingsPage> SettingsPages
        {
            get { return new ISettingsPage[] { new TRIMPSettingsPage() }; }
        }

        #endregion
    }
}
