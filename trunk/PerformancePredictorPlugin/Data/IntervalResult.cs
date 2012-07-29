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
using ZoneFiveSoftware.Common.Data.Fitness;

namespace GpsRunningPlugin.Source
{
    public class IntervalResultCache
    {
        public IntervalResultCache(double adistance, TimeSpan time)
        {
            double aseconds = time.TotalSeconds;
            mileSpeed = Predict.getTrainingSpeed(1609.344, adistance, time);
            k5Speed = Predict.getTrainingSpeed(5000, adistance, time);
            k10Speed = Predict.getTrainingSpeed(10000, adistance, time);
        }
        public double mileSpeed;
        public double k5Speed;
        public double k10Speed;

    }
    public class IntervalResult
    {
        private IActivity activity;
        private IntervalResultCache resultCache;

        public IActivity Activity
        {
            get
            {
                return activity;
            }
        }

        private double factor;
        public double Distance;
        public double OneMile
        {
            get
            {
                return factor * resultCache.mileSpeed;
            }
        }
        public double FiveKm
        {
            get
            {
                return factor * resultCache.k5Speed;
            }
        }
        public double TenKm
        {
            get
            {
                return factor * resultCache.k10Speed;
            }
        }

        public IntervalResult(IActivity activity, IntervalResultCache resultCache, double distance)
        {
            this.activity = activity;
            this.resultCache = resultCache;
            this.Distance = distance;
            this.factor = 1000.0 / distance;
        }
    }
}
