/*
Copyright (C) 2010 Gerhard Olsson
Copyright (C) 2010 Peter Furucz

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

using System.Collections.Generic;
using ZoneFiveSoftware.Common.Data.Fitness;
using ZoneFiveSoftware.Common.Data.GPS;

namespace SportTracksUniqueRoutesPlugin.Source
{
    //Compatibility - namespace changed
    class UniqueRoutes
    {
        public static IList<IActivity> findSimilarRoutes(IActivity activity, System.Windows.Forms.ProgressBar progressBar)
        {
            return GpsRunningPlugin.Source.UniqueRoutes.findSimilarRoutes(activity, progressBar);
        }
    }
}

namespace SportTracksUniqueRoutesPlugin.Source
{
    //Compatibility - namespace changed
    class Settings
    {
        public static bool HasDirection
        {
            get { return GpsRunningPlugin.Source.Settings.HasDirection; }
            set { GpsRunningPlugin.Source.Settings.HasDirection = value; }
        }
    }
}

namespace UniqueRoutes.Export
{
    public static class UniqueRoutes
    {
        public static IList<IActivity> findSimilarRoutes(IActivity activity, System.Windows.Forms.ProgressBar progressBar)
        {
            return GpsRunningPlugin.Source.UniqueRoutes.findSimilarRoutes(activity, progressBar);
        }
    }
    class Settings
    {
        public static bool HasDirection
        {
            get { return GpsRunningPlugin.Source.Settings.HasDirection; }
            set { GpsRunningPlugin.Source.Settings.HasDirection = value; }
        }
    }
    public static class CommonStretches
    {
        public static IDictionary<IActivity, IList<double>> getCommonSpeed(IActivity refActivity, IList<IActivity> activities, bool useActive)
        {
            return GpsRunningPlugin.Source.CommonStretches.getCommonSpeed(refActivity.GPSRoute, activities, useActive);
        }
        public static IDictionary<IActivity, IList<double>> getCommonSpeed(IGPSRoute refRoute, IList<IActivity> activities, bool useActive)
        {
            return GpsRunningPlugin.Source.CommonStretches.getCommonSpeed(refRoute, activities, useActive);
        }

        public static IDictionary<IActivity, IList<double[,]>> findSimilarPoints(IActivity activity, IList<IActivity> activities)
        {
            IDictionary<IActivity, IList<double[,]>> results = new Dictionary<IActivity, IList<double[,]>>();
            IDictionary<IActivity, IList<GpsRunningPlugin.Source.PointInfo[]>> p = GpsRunningPlugin.Source.CommonStretches.findSimilarPoints(activity.GPSRoute, activity.Laps, activities);
            foreach(KeyValuePair<IActivity, IList<GpsRunningPlugin.Source.PointInfo[]>> kp in p)
            {
                results.Add(kp.Key, new List<double[,]>());
                foreach(GpsRunningPlugin.Source.PointInfo[] api in kp.Value)
                {
                    double[,] dpi = new double[2,4]{
                     {api[0].index, api[0].distance, api[0].time, api[0].restLap ? 1 : 0},
                     {api[1].index, api[1].distance, api[1].time, api[1].restLap ? 1 : 0}
                    };
                    results[kp.Key].Add(dpi);
                }
            }
            return results;
        }
    }
}
