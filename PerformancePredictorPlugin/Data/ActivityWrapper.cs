/*
Copyright (C) 2010 Staffan Nilsson

This library is free software; you can redistribute it and/or
modify it under the terms of the GNU Lesser General Public
License as published by the Free Software Foundation; either
version 3 of the License, or (at your option) any later version.

This library is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
Lesser General Public License for more details.

You should have received a copy of the GNU Lesser General Public
License along with this library. If not, see <http://www.gnu.org/licenses/>.
 */

using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using ZoneFiveSoftware.Common.Data.Fitness;

namespace GpsRunningPlugin.Source
{
    public class ActivityWrapper
    {
        private IActivity activity;
        private Color actColor;
        //private TimeSpan timeOffset;
        //private double distanceOffset;

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
            set
            {
                actColor = value;
            }
        }

        //public TimeSpan TimeOffset
        //{
        //    get
        //    {
        //        return timeOffset;
        //    }
        //    set
        //    {
        //        timeOffset = value;
        //    }
        //}

        //public double DistanceOffset
        //{
        //    get
        //    {
        //        return distanceOffset;
        //    }
        //    set
        //    {
        //        distanceOffset = value;
        //    }
        //}

        public ActivityWrapper()
        {
            activity = null;
            //timeOffset = new TimeSpan();
            //distanceOffset = 0;
            actColor = Color.Black;
        }

        public ActivityWrapper(IActivity activity, Color color):this()
        {
            this.activity = activity;
            this.actColor = color;
        }

    }
}
