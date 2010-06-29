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
using SportTracksUniqueRoutesPlugin;
using SportTracksUniqueRoutesPlugin.Source;
using System.Windows.Forms;
using System.Collections;

namespace SportTracksUniqueRoutesPlugin.Source
{
    public enum UniqueModel
    {
        GPS, ELEVATION
    }
    class UniqueRoutes
    {
        private UniqueRoutes() { }
        private static UniqueModel uniqueModel = UniqueModel.GPS;

        public static IList<IActivity> findSimilarRoutes(IActivity activity, System.Windows.Forms.ProgressBar progressBar)
        {
            IList<IActivity> activities = new List<IActivity>();
            if (activity == null || 
                uniqueModel == UniqueModel.ELEVATION && activity.ElevationMetersTrack == null || 
                activity.GPSRoute == null || //GPS, ELEVATION
                !isAllowedActivity(activity))
                return activities;
            progressBar.Value = 0;
            progressBar.Minimum = 0;
            progressBar.Maximum = Plugin.GetApplication().Logbook.Activities.Count;
            if (uniqueModel == UniqueModel.ELEVATION)
            {
                //Not implemented
            } else {
            GPSGrid grid = new GPSGrid(activity);
            IDictionary<IActivity, int> beginningPoints = new Dictionary<IActivity, int>();
            IDictionary<IActivity, int> endPoints = new Dictionary<IActivity, int>();

            setBeginningAndEndPoints(activity, beginningPoints, endPoints);
            if (!beginningPoints.ContainsKey(activity) ||
                beginningPoints[activity] <= -1 ||
                        endPoints[activity] <= -1)
            {
                //The settings does not include any points
                return activities;
            }
            foreach (IActivity otherActivity in Plugin.GetApplication().Logbook.Activities)
            {
                int pointsOutside = 0;
                bool inBand = false;
                int direction = 0;
                if (isAllowedActivity(otherActivity) &&
                    otherActivity.GPSRoute != null && otherActivity.GPSRoute.Count >0)
                {
                    setBeginningAndEndPoints(otherActivity, beginningPoints, endPoints);
                    int noOfPoints = otherActivity.GPSRoute.Count;
                    if (beginningPoints[otherActivity] > -1 && 
                        endPoints[otherActivity] > -1 &&
                        otherActivity.GPSRoute[0].Value.DistanceMetersToPoint(activity.GPSRoute[0].Value)
                        < otherActivity.GPSRoute.TotalDistanceMeters + activity.GPSRoute.TotalDistanceMeters)
                    {
                        inBand = true;
                        for (int i = beginningPoints[otherActivity]; i <= endPoints[otherActivity]; i++)
                        {
                            IGPSPoint point = otherActivity.GPSRoute[i].Value;
                            int closests = grid.getClosePoint(point);
                            if (closests < 0)
                            {
                                pointsOutside++;
                            }
                            else
                            {
                                //Find direction. Works well when the routes are not overlapping and have similar length
                                //Use all matching points to get majority decision
                                //All matching points from grid.getAllClose would be better but is significantly slower
                                bool inOtherLowerHalf = i < otherActivity.GPSRoute.Count / 2;
                                
                                    bool inLowerHalf = closests < activity.GPSRoute.Count / 2;
                                    if ((inLowerHalf && inOtherLowerHalf) ||
                                        (!inLowerHalf && !inOtherLowerHalf))
                                        direction++;
                                    else
                                        direction--;
                                
                            }
                            if (pointsOutside / ((double)otherActivity.GPSRoute.Count) > Settings.ErrorMargin)
                            {
                                inBand = false;
                                break;
                            }
                        }
                        if (inBand)
                        {
                            pointsOutside = 0;
                            IGPSBounds otherBounds = GPSBounds.FromGPSRoute(otherActivity.GPSRoute);
                            GPSGrid otherGrid = new GPSGrid(otherActivity);
                            for (int i = beginningPoints[activity]; i <= endPoints[activity]; i++)
                            {
                                IGPSPoint point = activity.GPSRoute[i].Value;
                                int closests = otherGrid.getClosePoint(point);
                                if (closests < 0)
                                {
                                    pointsOutside++;
                                }
                                if (pointsOutside / ((double)activity.GPSRoute.Count) > Settings.ErrorMargin)
                                {
                                    inBand = false;
                                    break;
                                }
                            }
                        }
                    }
                }
                if (inBand && (!Settings.HasDirection ||
                    (Settings.HasDirection && direction > 0)))
                    activities.Add(otherActivity);
                progressBar.Increment(1);
            }
            }

            return activities;
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

        private static void setBeginningAndEndPoints(IActivity activity,
            IDictionary<IActivity, int> beginningPoints,
            IDictionary<IActivity, int> endPoints)
        {
            double length = 0;
            //prune with some searches
            beginningPoints[activity] = 0;
            endPoints[activity] = activity.GPSRoute.Count - 1;
            length = Settings.IgnoreBeginning + Settings.IgnoreEnd;
            if (length > activity.GPSRoute.TotalDistanceMeters)
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
                    for (int i = 0; i < activity.GPSRoute.Count; i++)
                    {
                        if (length >= Settings.IgnoreBeginning)
                        {
                            beginningPoints[activity] = i;
                            break;
                        }
                        IGPSPoint point = activity.GPSRoute[i].Value;
                        if (previous != null)
                            length += point.DistanceMetersToPoint(previous);
                        previous = point;
                    }
                }
                if (Settings.IgnoreEnd > 0)
                {
                    length = 0;
                    previous = null;
                    for (int i = activity.GPSRoute.Count - 1; i >= 0; i--)
                    {
                        IGPSPoint current = activity.GPSRoute[i].Value;
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
