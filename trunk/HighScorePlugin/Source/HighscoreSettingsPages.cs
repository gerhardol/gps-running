using System;
using System.Collections.Generic;
using System.Text;

using ZoneFiveSoftware.Common.Visuals;
using ZoneFiveSoftware.Common.Visuals.Fitness;
using System.Collections;

namespace SportTracksHighScorePlugin.Source
{
    class HighScoreSettingsPages : IExtendSettingsPages
    {
        #region IExtendSettingsPages Members

        static List<ISettingsPage> list;

        public HighScoreSettingsPages()
        {
            list = new List<ISettingsPage>();
            list.Add(new HighScorePage());
        }

        public IList<ISettingsPage> SettingsPages
        {
            get
            {
                return list;
            }
        }

        #endregion
    }
}
