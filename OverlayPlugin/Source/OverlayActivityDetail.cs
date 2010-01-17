using System;
using System.Collections.Generic;
using System.Text;
using ZoneFiveSoftware.Common.Visuals.Fitness;

namespace SportTracksOverlayPlugin.Source
{
    class OverlayActivityDetail : IExtendActivityDetailPages
    {
        #region IExtendActivityDetailPages Members

        public IList<IActivityDetailPage> ActivityDetailPages
        {
            get
            {
                return new IActivityDetailPage[] { new OverlayActivityDetailPage() };
            }
        }

        #endregion
    }
}
