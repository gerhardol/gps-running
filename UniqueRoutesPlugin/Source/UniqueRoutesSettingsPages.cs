using System;
using System.Collections.Generic;
using System.Text;

using ZoneFiveSoftware.Common.Visuals;
using ZoneFiveSoftware.Common.Visuals.Fitness;
using System.Collections;

namespace SportTracksUniqueRoutesPlugin.Source
{
    class UniqueRoutesSettingsPages : IExtendSettingsPages
    {
        #region IExtendSettingsPages Members

        static List<ISettingsPage> list;

        public UniqueRoutesSettingsPages()
        {
            list = new List<ISettingsPage>();
            list.Add(new UniqueRoutesPage());
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
