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
        private TimeSpan timeOffset;
        private double distanceOffset;

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

        public TimeSpan TimeOffset
        {
            get
            {
                return timeOffset;
            }
            set
            {
                timeOffset = value;
            }
        }

        public double DistanceOffset
        {
            get
            {
                return distanceOffset;
            }
            set
            {
                distanceOffset = value;
            }
        }

        public ActivityWrapper()
        {
            activity = null;
            timeOffset = new TimeSpan();
            distanceOffset = 0;
            actColor = Color.Black;
        }

        public ActivityWrapper(IActivity activity, Color color):this()
        {
            this.activity = activity;
            this.actColor = color;
        }

    }
}
