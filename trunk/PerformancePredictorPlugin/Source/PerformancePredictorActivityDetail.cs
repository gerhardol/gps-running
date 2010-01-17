using System;
using System.Collections.Generic;
using System.Text;
using ZoneFiveSoftware.Common.Visuals.Fitness;

namespace SportTracksPerformancePredictorPlugin.Source
{
    class PerformancePredictorActivityDetail : IExtendActivityDetailPages
    {
        #region IExtendActivityDetailPages Members

        public IList<IActivityDetailPage> ActivityDetailPages
        {
            get { return new IActivityDetailPage[] { new PerformancePredictorActivityDetailPage() }; }
        }

        #endregion
    }
}
