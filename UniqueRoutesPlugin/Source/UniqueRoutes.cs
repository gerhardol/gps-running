/*
Copyright (C) 2007, 2008 Kristian Bisgaard Lassen
Copyright (C) 2010 Kristian Helkjaer Lassen

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
using ZoneFiveSoftware.Common.Data;
using ZoneFiveSoftware.Common.Data.Fitness;
using ZoneFiveSoftware.Common.Data.GPS;
using GpsRunningPlugin;
using GpsRunningPlugin.Source;
using System.Windows.Forms;
using System.Collections;

namespace GpsRunningPlugin.Source
{
    class UniqueRoutes
    {
        private UniqueRoutes() { }

        //This method is used by DotRacing, Matrix
        //Other methods are not considered "stable"
        public static IList<IActivity> findSimilarRoutes(IActivity refActivity, System.Windows.Forms.ProgressBar progressBar)
        {
            return findSimilarRoutes(refActivity, null, true, progressBar);
        }

        public static IList<IActivity> findSimilarRoutes(IActivity refActivity, IList<IActivity> activities, System.Windows.Forms.ProgressBar progressBar)
        {
            return findSimilarRoutes(refActivity, activities, true, progressBar);
        }

        public static IList<IActivity> findSimilarRoutes(IGPSRoute refRoute, IList<IActivity> activities, System.Windows.Forms.ProgressBar progressBar)
        {
            return findSimilarRoutes(refRoute, activities, false, progressBar);
        }

        public static IList<IActivity> getBaseActivities()
        {
            IList<IActivity> activities = new List<IActivity>();
            foreach (IActivity otherActivity in Plugin.GetApplication().Logbook.Activities)
            {
                if (isAllowedActivity(otherActivity) &&
                    otherActivity.GPSRoute != null && otherActivity.GPSRoute.Count > 0)
                {
                    //Insert so the newest activity is inserted first
                    activities.Insert(0, otherActivity);
                }
            }
            return activities;
        }

        public static IList<IActivity> findSimilarRoutes(IActivity refActivity, IList<IActivity> activities, bool activityCompare, bool catCheck, System.Windows.Forms.ProgressBar progressBar)
        {
            if (refActivity == null ||
                refActivity.GPSRoute == null ||
                activityCompare && (false/*catCheck && !isAllowedActivity(refActivity)*/))
                return new List<IActivity>();
            return findSimilarRoutes(refActivity.GPSRoute, refActivity.ReferenceId, activities, activityCompare, progressBar);
        }

        private static IList<IActivity> findSimilarRoutes(IActivity refActivity, IList<IActivity> activities, bool activityCompare, System.Windows.Forms.ProgressBar progressBar)
        {
            return findSimilarRoutes(refActivity, activities, activityCompare, true, progressBar);
        }

        public static IList<IActivity> findSimilarRoutes(IGPSRoute refRoute, IList<IActivity> activities, bool activityCompare, System.Windows.Forms.ProgressBar progressBar)
        {
            return findSimilarRoutes(refRoute, "", activities, activityCompare, progressBar);
        }

        private static IList<IActivity> findSimilarRoutes(IGPSRoute refRoute, string refId, IList<IActivity> activities, bool activityCompare, System.Windows.Forms.ProgressBar progressBar)
        {
            if (progressBar == null)
            {
                progressBar = new System.Windows.Forms.ProgressBar();
            }
            IList<IActivity> result = new List<IActivity>();
            if (refRoute == null || refRoute.Count == 0)
                return result;
            if (activities == null)
            {
                activities = getBaseActivities();
            }
            progressBar.Value = 0;
            progressBar.Minimum = 0;
            progressBar.Maximum = activities.Count;

            GPSGrid refGrid = new GPSGrid(null, refRoute);
            IDictionary<string, int> beginningPoints = new Dictionary<string, int>();
            IDictionary<string, int> endPoints = new Dictionary<string, int>();

            setBeginningAndEndPoints(refId, refRoute, activityCompare, beginningPoints, endPoints);
            if (!beginningPoints.ContainsKey(refId) ||
                beginningPoints[refId] <= -1 ||
                      endPoints[refId] <= -1)
            {
                //The settings does not include any points
                return result;
            }
            foreach (IActivity otherActivity in activities)
            {
                if (otherActivity.GPSRoute != null && otherActivity.GPSRoute.Count > 0 &&
                    //Simple prune, eliminating routes not possibly common
                        otherActivity.GPSRoute[0].Value.DistanceMetersToPoint(refRoute[0].Value)
                        < otherActivity.GPSRoute.TotalDistanceMeters + refRoute.TotalDistanceMeters)
                {
                    setBeginningAndEndPoints(otherActivity, activityCompare, beginningPoints, endPoints);
                    //int noOfPoints = otherActivity.GPSRoute.Count;
                    if (beginningPoints[otherActivity.ReferenceId] > -1 && endPoints[otherActivity.ReferenceId] > -1)
                    {
                        int pointsOutside = 0;
                        bool inBand = true;
                        int direction = 0;
                        int prevMatch = -1;

                        //IGPSBounds otherBounds = GPSBounds.FromGPSRoute(otherActivity.GPSRoute);
                        GPSGrid otherGrid = new GPSGrid(refGrid, otherActivity);

                        //Check if the reference route fits in the other activity
                        for (int i = beginningPoints[refId]; i <= endPoints[refId]; i++)
                        {
                            IGPSPoint point = refRoute[i].Value;
                            int closests = otherGrid.getClosePoint(point);
                            if (closests < 0)
                            {
                                pointsOutside++;
                                if (pointsOutside / ((double)refRoute.Count) > Settings.ErrorMargin)
                                {
                                    inBand = false;
                                    break;
                                }
                            }
                            else if (Settings.HasDirection)
                            {
                                int currMatch = closests;// grid.getCloseSinglePoint(point);
                                if (currMatch > prevMatch)
                                {
                                    direction++;
                                }
                                else if (currMatch < prevMatch)
                                {
                                    direction--;
                                }
                                prevMatch = currMatch;

                                ////Find direction. Works well when the routes are not overlapping and have similar length
                                ////Use all matching points to get majority decision
                                ////All matching points from grid.getAllClose would be better but is significantly slower
                                //bool inOtherLowerHalf = i < otherActivity.GPSRoute.Count / 2;
                                //bool inLowerHalf = closests < refRoute.Count / 2;

                                //if ((inLowerHalf && inOtherLowerHalf) ||
                                //    (!inLowerHalf && !inOtherLowerHalf))
                                //    direction++;
                                //else
                                //    direction--;

                            }
                        }

                        //Check if the other activity matches the reference route 
                        //Only used when comparing activities
                        if (inBand && activityCompare)
                        {
                            pointsOutside = 0;
                            for (int i = beginningPoints[otherActivity.ReferenceId]; i <= endPoints[otherActivity.ReferenceId]; i++)
                            {
                                IGPSPoint point = otherActivity.GPSRoute[i].Value;
                                int closests = refGrid.getClosePoint(point);
                                if (closests < 0)
                                {
                                    pointsOutside++;
                                    if (pointsOutside / ((double)otherActivity.GPSRoute.Count) > Settings.ErrorMargin)
                                    {
                                        inBand = false;
                                        break;
                                    }
                                }
                            }
                        }
                        if (inBand && direction >= 0)
                        {
                            result.Add(otherActivity);
                        }
                    }
                }
                progressBar.Increment(1);
            }

            return result;
        }

        private static bool isAllowedActivity(IActivity activity)
        {
            return Settings.SelectedCategory == null ||
                isSubcategoryOf(Settings.SelectedCategory, activity.Category);
        }

        private static bool isSubcategoryOf(IActivityCategory iActivityCategory, IActivityCategory iActivityCategory_2)
        {
            if (iActivityCategory.Equals(iActivityCategory_2)) return true;
            if (iActivityCategory_2 == null) return false;
            return isSubcategoryOf(iActivityCategory, iActivityCategory_2.Parent);
        }

        private static void setBeginningAndEndPoints(IActivity activity, bool beginEndCheck,
            IDictionary<string, int> beginningPoints,
            IDictionary<string, int> endPoints)
        {
            setBeginningAndEndPoints(activity.ReferenceId, activity.GPSRoute, beginEndCheck, beginningPoints, endPoints);
        }
        private static void setBeginningAndEndPoints(string activity, IGPSRoute gpsroute, bool beginEndCheck,
            IDictionary<string, int> beginningPoints,
            IDictionary<string, int> endPoints)
        {
            double length = 0;
            //prune with some searches
            beginningPoints[activity] = 0;
            endPoints[activity] = gpsroute.Count - 1;
            if (beginEndCheck)
            {
                length = Settings.IgnoreBeginning + Settings.IgnoreEnd;
                if (length > gpsroute.TotalDistanceMeters)
                {
                    //not used at all
                    beginningPoints[activity] = -1;
                    endPoints[activity] = -1;
                    return;
                }
                if (length > 0)
                {
                    //Note: As we normally cover few points, this is faster than activity.GPSRoute.GetDistanceMetersTrack()
                    //(and the distance track must be scanned, there is no simple way to get the index for a distance)
                    //If the algoritm to calc dist is changed, this should be changed too
                    IGPSPoint previous;
                    if (Settings.IgnoreBeginning > 0)
                    {
                        length = 0;
                        previous = null;
                        for (int i = 0; i < gpsroute.Count; i++)
                        {
                            if (length >= Settings.IgnoreBeginning)
                            {
                                beginningPoints[activity] = i;
                                break;
                            }
                            IGPSPoint point = gpsroute[i].Value;
                            if (previous != null)
                                length += point.DistanceMetersToPoint(previous);
                            previous = point;
                        }
                    }
                    if (Settings.IgnoreEnd > 0)
                    {
                        length = 0;
                        previous = null;
                        for (int i = gpsroute.Count - 1; i >= 0; i--)
                        {
                            IGPSPoint current = gpsroute[i].Value;
                            if (length >= Settings.IgnoreEnd)
                            {
                                endPoints[activity] = i;
                                break;
                            }
                            if (previous != null)
                                length += previous.DistanceMetersToPoint(current);
                            previous = current;
                        }
                    }
                }
            }
        }
    }
}
