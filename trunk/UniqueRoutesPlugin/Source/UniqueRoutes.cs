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
using ZoneFiveSoftware.Common.Data.Fitness;
using ZoneFiveSoftware.Common.Data.GPS;
using SportTracksUniqueRoutesPlugin;
using SportTracksUniqueRoutesPlugin.Source;
using System.Windows.Forms;
using System.Collections;

namespace SportTracksUniqueRoutesPlugin.Source
{
    class UniqueRoutes
    {
        private UniqueRoutes() { }

        public static IList<IActivity> findSimilarRoutes(IActivity activity, System.Windows.Forms.ProgressBar progressBar)
        {
            IList<IActivity> activities = new List<IActivity>();
            if (activity == null || activity.GPSRoute == null || 
                !isAllowedActivity(activity))
                return activities;
            progressBar.Value = 0;
            progressBar.Minimum = 0;
            progressBar.Maximum = Plugin.GetApplication().Logbook.Activities.Count;
            GPSGrid grid = new GPSGrid(activity);
            IDictionary<IGPSPoint, int> positions = new Dictionary<IGPSPoint, int>();
            int index = 0;
            IDictionary<IActivity, IGPSPoint> beginningPoints = new Dictionary<IActivity, IGPSPoint>();
            IDictionary<IActivity, IGPSPoint> endPoints = new Dictionary<IActivity, IGPSPoint>();
            IDictionary<IActivity, ArrayList> routes = new Dictionary<IActivity, ArrayList>();
            IDictionary<IActivity, bool> hasSearchBeginAndEnd = new Dictionary<IActivity, bool>();
            foreach (IActivity otherActivity in Plugin.GetApplication().Logbook.Activities)
                hasSearchBeginAndEnd[otherActivity] = false;
            setBeginningAndEndPoints(activity, beginningPoints, endPoints, routes, hasSearchBeginAndEnd);
            if (!beginningPoints.ContainsKey(activity))
                return activities;
            foreach (IGPSPoint point in activity.GPSRoute.GetValueEnumerator())
                positions.Add(point, index++);
            foreach (IActivity otherActivity in Plugin.GetApplication().Logbook.Activities)
            {
                int pointsOutside = 0;
                bool inBand = false;
                int direction = 0;
                if (isAllowedActivity(otherActivity) && 
                    otherActivity.GPSRoute != null)
                {
                    setBeginningAndEndPoints(otherActivity, beginningPoints,
                        endPoints, routes, hasSearchBeginAndEnd);
                    if (beginningPoints.ContainsKey(otherActivity))
                    {
                        inBand = true;
                        index = 0;
                        bool seenStart = false;
                        IGPSPoint beginning = beginningPoints[otherActivity];
                        IGPSPoint end = endPoints[otherActivity];
                        foreach (IGPSPoint point in otherActivity.GPSRoute.GetValueEnumerator())
                        {
                            if (!seenStart && beginning == point)
                                seenStart = true;
                            if (seenStart)
                            {
                                IGPSPoint closests = grid.getClosests(point);
                                if (closests == null)
                                    pointsOutside++;
                                else
                                {
                                    bool inLowerHalf = positions[closests] < activity.GPSRoute.Count / 2;
                                    bool inOtherLowerHalf = index < otherActivity.GPSRoute.Count / 2;
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
                            if (seenStart && end == point)
                                break;
                            index++;
                        }
                        if (inBand)
                        {
                            pointsOutside = 0;
                            IGPSBounds otherBounds = GPSBounds.FromGPSRoute(otherActivity.GPSRoute);
                            GPSGrid otherGrid = new GPSGrid(otherActivity);
                            seenStart = false;
                            beginning = beginningPoints[activity];
                            end = endPoints[activity];
                            foreach (IGPSPoint point in activity.GPSRoute.GetValueEnumerator())
                            {
                                if (!seenStart && beginning == point)
                                    seenStart = true;
                                if (seenStart)
                                {
                                    IGPSPoint closests = otherGrid.getClosests(point);
                                    if (closests == null)
                                        pointsOutside++;
                                    if (pointsOutside / ((double)activity.GPSRoute.Count) > Settings.ErrorMargin)
                                    {
                                        inBand = false;
                                        break;
                                    }
                                }
                                if (seenStart && end == point)
                                    break;
                            }
                        }
                    }
                }
                if (inBand && (!Settings.HasDirection ||
                    (Settings.HasDirection && direction > 0)))
                    activities.Add(otherActivity);
                progressBar.Increment(1);
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
            IDictionary<IActivity, IGPSPoint> beginningPoints,
            IDictionary<IActivity, IGPSPoint> endPoints,
            IDictionary<IActivity, ArrayList> routes,
            IDictionary<IActivity, bool> hasSearchBeginAndEnd)
        {
            if (!hasSearchBeginAndEnd[activity])
            {
                double length = 0;
                IGPSPoint previous = null;
                ArrayList route = getRoute(activity, routes);
                int indexStart = 0;
                foreach (IGPSPoint point in route)
                {
                    if (length >= Settings.IgnoreBeginning)
                    {
                        beginningPoints[activity] = point;
                        break;
                    }
                    if (previous != null)
                        length += point.DistanceMetersToPoint(previous);
                    previous = point;
                    indexStart++;
                }

                length = 0;
                previous = null;
                int indexEnd = 0;
                for (int i = route.Count - 1; i >= 0; i--)
                {
                    IGPSPoint current = (IGPSPoint)route[i];
                    if (length >= Settings.IgnoreEnd)
                    {
                        endPoints[activity] = current;
                        indexEnd = i;
                        break;
                    }
                    if (previous != null)
                        length += previous.DistanceMetersToPoint(current);
                    previous = current;
                }
                if (indexEnd < indexStart)
                {
                    beginningPoints[activity] = null;
                    endPoints[activity] = null;
                }
                hasSearchBeginAndEnd[activity] = true;
            }
        }

        private static ArrayList getRoute(IActivity activity, IDictionary<IActivity, ArrayList> routes)
        {
            ArrayList route = null;
            if (!routes.ContainsKey(activity))
            {
                route = new ArrayList();
                foreach (IGPSPoint point in activity.GPSRoute.GetValueEnumerator())
                {
                    route.Add(point);
                }
            }
            else route = routes[activity];
            return route;
        }
    }
}
