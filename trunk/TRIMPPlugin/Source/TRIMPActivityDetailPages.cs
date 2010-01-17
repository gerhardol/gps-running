using System;
using System.Collections.Generic;
using System.Text;
using ZoneFiveSoftware.Common.Visuals.Fitness;

namespace SportTracksTRIMPPlugin.Source
{
    class TRIMPActivityDetailPages : IExtendActivityDetailPages
    {

        #region IExtendActivityDetailPages Members

        public IList<IActivityDetailPage> ActivityDetailPages
        {
            get { return new IActivityDetailPage[] { new TRIMPActivityDetailPage() }; }
        }

        #endregion
    }
}
