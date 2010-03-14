/*
Copyright (C) 2010 Gerhard Olsson

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
    class UniqueStretches
    {
        private UniqueStretches() { }
        private static UniqueModel uniqueModel = UniqueModel.GPS;

        private static readonly int gpsPointMinStretch = 3; //Min stretches, ignore all w less points than this
        public static IDictionary<IActivity, IList<IList<int>>> findSimilarStretch(IActivity activity, IList<IActivity> activities)
        {
            GPSGrid grid = new GPSGrid(activity, true);
            IDictionary<IActivity, IList<IList<int>>> result = new Dictionary<IActivity, IList<IList<int>>>();
            foreach (IActivity otherActivity in activities)
            {
                result.Add(otherActivity, new List<IList<int>>());
                IDistanceDataTrack dist = otherActivity.GPSRoute.GetDistanceMetersTrack();
                int lastMatch = -1; //index of previous match
                int startMatch = -1;
                int startMatchRef = -1;
                IndexDiffDist currMatch = new IndexDiffDist();
                for (int i = 0; i < otherActivity.GPSRoute.Count; i++)
                {
                    IList<IndexDiffDist> curr = new List<IndexDiffDist>();
                    curr = grid.getAllCloseStretch(otherActivity.GPSRoute[i].Value);
                    bool isEnd = true; //Assume this is end, if lastMatch is set
                    if (curr.Count > 0)
                    {
                       if (lastMatch >= 0)
                        {
                            foreach (IndexDiffDist IndDist in curr)
                            {
                                //Only check forward, i.e follow the same direction
                                if(IndDist.low >= currMatch.low && IndDist.low <= currMatch.high)
                                {
                                    lastMatch = i;
                                    currMatch = IndDist;
                                    isEnd = false;
                                    break;
                                }
                            }
                       }
                    }
                    //Special checks for first/last point investigated
                    if (0 == i)
                    {
                        isEnd = false;
                    } else if (otherActivity.GPSRoute.Count - 1 == i)
                        {
                        isEnd = true;
                    }
                    if (0 <= lastMatch && isEnd)
                    {
                        if (lastMatch - startMatch <= gpsPointMinStretch)
                        {
                            //ignore this short stretch - could be distance based too
                        }
                        else
                        {
                            // end match
                            IList<int> s = new List<int>();
                            s.Add(startMatch);
                            s.Add(lastMatch);
                            s.Add(startMatchRef);
                            s.Add(currMatch.Index);
                            result[otherActivity].Add(s);
                        }
                        lastMatch = -1;
                    }
                    if (curr.Count > 0)
                    {
                        if (isEnd)
                        {
                            //start of new match
                            startMatch = i;
                            currMatch = curr[0];
                            foreach (IndexDiffDist IndDist in curr)
                            {
                                if (Math.Abs(IndDist.Dist - dist[i].Value) < Math.Abs(currMatch.Dist - dist[i].Value))
                                {
                                    currMatch = IndDist;
                                }
                            }
                            startMatchRef = currMatch.Index;
                        }
                        lastMatch = i;
                    }
                }
            }
                    
            return result;
        }
}
