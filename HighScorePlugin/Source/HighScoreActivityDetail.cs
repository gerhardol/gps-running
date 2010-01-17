using System;
using System.Collections.Generic;
using System.Text;

using ZoneFiveSoftware.Common.Visuals.Fitness;

namespace SportTracksHighScorePlugin.Source
{
    class HighScoreActivityDetailPages : IExtendActivityDetailPages
    {
        #region IExtendActivityDetailPages Members

        public IList<IActivityDetailPage> ActivityDetailPages
        {
            get { return new IActivityDetailPage[] { new HighScoreActivityDetailPage() }; }
        }

        #endregion
    }
}
