using System;
using System.Collections.Generic;
using System.Text;

using ZoneFiveSoftware.Common.Visuals.Fitness;

namespace SportTracksUniqueRoutesPlugin.Source
{
    class UniqueRoutesActivityDetailPages : IExtendActivityDetailPages
    {
        #region IExtendActivityDetailPages Members

        public IList<IActivityDetailPage> ActivityDetailPages
        {
            get { return new IActivityDetailPage[] { new UniqueRoutesActivityDetailPage() }; }
        }

        #endregion
    }
}
