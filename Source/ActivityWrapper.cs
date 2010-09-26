using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using ZoneFiveSoftware.Common.Data.Fitness;

namespace GpsRunningPlugin.Source
{
    class ActivityWrapper
    {
        private IActivity activity;
        private Color actColor;

        public IActivity Activity
        {
            get
            {
                return activity;
            }
        }

        public Color ActColor
        {
            get
            {
                return actColor;
            }
        }

        public ActivityWrapper()
        {
            activity = null;
        }

        public ActivityWrapper(IActivity activity, Color color)
        {
            this.activity = activity;
            this.actColor = color;
        }

    }
}
