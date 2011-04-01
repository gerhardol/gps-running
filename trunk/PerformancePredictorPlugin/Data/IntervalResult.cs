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
using ZoneFiveSoftware.Common.Data.Measurement;

namespace GpsRunningPlugin.Source
{
    public class IntervalResult
    {
        private IActivity activity;

        public IActivity Activity
        {
            get
            {
                return activity;
            }
        }

        private bool isCalc = false;
        static double mileSpeed;
        static double k5Speed;
        static double k10Speed;
        private double factor;

        public double Distance;
        public double OneMile
        {
            get
            {
                return factor*mileSpeed;
            }
        }
        public double FiveKm
        {
            get
            {
                return factor*k5Speed;
            }
        }
        public double TenKm
        {
            get
            {
                return factor*k10Speed;
            }
        }

        public IntervalResult(IActivity activity, double distance, double seconds)
        {
            this.activity = activity;
            this.Distance = distance;
            factor = 1000.0 / distance;
            if (!isCalc)
            {
                ActivityInfo info = ActivityInfoCache.Instance.GetInfo(activity);
                double adistance = info.DistanceMeters;
                double aseconds = info.Time.TotalSeconds;
                mileSpeed = TrainingView.getTrainingSpeed(1609.344, adistance, aseconds);
                k5Speed = TrainingView.getTrainingSpeed(5000, adistance, aseconds);
                k10Speed = TrainingView.getTrainingSpeed(10000, adistance, aseconds);
                isCalc = true;
            }
        }
    }
}
