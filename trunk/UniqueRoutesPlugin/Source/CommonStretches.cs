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
    class CommonStretches
    {
        private CommonStretches() { }

        //xxx private static readonly int gpsPointMinStretch = 3; //Min stretches, ignore all w less points than this
        public static IDictionary<IActivity, IList<double>> getCommonSpeed(IActivity activity, IList<IActivity> activities, bool useActive)
        {
          IDictionary<IActivity, IList<IList<int>>> points = new Dictionary<IActivity, IList<IList<int>>>();
          IDictionary<IActivity, IList<double>> result = new Dictionary<IActivity, IList<double>>();
            points = findSimilarPoints(activity, activities);
                IDistanceDataTrack distRef = activity.GPSRoute.GetDistanceMetersTrack();
                foreach (IActivity otherActivity in activities)
                {
                    const int ExtraGridIndex = 2;
                    const float MaxDistDiffFactor = 0.1F;
                    double MinDistStretch = Settings.Bandwidth * 2;

                    IDistanceDataTrack dist = otherActivity.GPSRoute.GetDistanceMetersTrack();
                    double totTime = 0;
                    double totTimeRef = 0;
                    double totDist = 0;
                    double totDistRef = 0;

                    int startInd = -1;
                    int startIndRef = -1;
                    int prevInd = -1;
                    int prevIndRef = -1;
                    int totxxx = 0;

                    foreach (IList<int> i in points[otherActivity])
                    {
                        if (i[0] > 0)
                        {
                            if (startInd == -1)
                            {
                                startInd = i[0];
                                startIndRef = i[1];
                            }
                            prevInd = i[0];
                            prevIndRef = i[1];
                        }
                        else
                        {
                            if (startInd > -1 && startIndRef > -1 && prevInd > -1 && prevIndRef > -1){
                            //End - Update summary
                            totTime += otherActivity.GPSRoute[prevInd].ElapsedSeconds - otherActivity.GPSRoute[startInd].ElapsedSeconds;
                            totTimeRef += activity.GPSRoute[prevIndRef].ElapsedSeconds - activity.GPSRoute[startIndRef].ElapsedSeconds;
                            totDist += dist[prevInd].Value - dist[startInd].Value;
                            totDistRef += distRef[prevIndRef].Value - distRef[startIndRef].Value;
                            totxxx++;
                            }
                            else
                            {
                                startInd = startInd; //xxx debug
                            }

                        }
                    }
                    IList<double> s = new List<double>();
                    s.Add(totDist);
                    s.Add(totTime);
                    s.Add(totDistRef);
                    s.Add(totTimeRef);
                    s.Add(totxxx);
                    result.Add(otherActivity, s);
                }
          return result;
        }
        public static IDictionary<IActivity, IList<IList<int>>> findSimilarPoints(IActivity activity, IList<IActivity> activities)
        {
            GPSGrid grid = new GPSGrid(activity, 1, true);
            IDictionary<IActivity, IList<IList<int>>> result = new Dictionary<IActivity, IList<IList<int>>>();
            double cumulativeAverageDist = 0;
            int noCumAv = 0;
            foreach (IActivity otherActivity in activities)
            {
                const int ExtraGridIndex = 2;
                const float MaxDistDiffFactor = 0.1F;
                double MinDistStretch = Settings.Bandwidth * 2;

                result.Add(otherActivity, new List<IList<int>>());
                IDistanceDataTrack dist = otherActivity.GPSRoute.GetDistanceMetersTrack();
                int lastMatch = -1; //index of previous match
                int startMatch = -1;
                int lastMatchRef = -1;
                int startMatchRef = -1;
                IndexDiffDist lastIndex = new IndexDiffDist();
                for (int i = 0; i < otherActivity.GPSRoute.Count; i++)
                {
                    IList<IndexDiffDist> currIndex = new List<IndexDiffDist>();
                    IndexDiffDist closeIndex = null;
                    bool isEnd = true; //This is the end of a stretch, unless a matching point is found
                    if (activity.Equals(otherActivity))
                    {
                        //xxx
                        startMatchRef = startMatchRef;

                        if (otherActivity.GPSRoute.Count - 1 == i)
                        {
                            isEnd = true;
                        }
                    }
                    currIndex = grid.getAllCloseStretch(otherActivity.GPSRoute[i].Value);
                    if (currIndex.Count > 0)
                    {
                        IndexDiffDist tmpInd = null;
                        int prio = int.MaxValue;
                        foreach (IndexDiffDist IndDist in currIndex)
                        {
                            if (IndDist.Index > -1)
                            {
                                //Get the closest point - used in starts and could restart current stretch
                                if (closeIndex == null ||
                                    (Math.Abs(IndDist.Dist - dist[i].Value) < Settings.Bandwidth) ||
                                    (Math.Abs(IndDist.Dist - dist[i].Value - cumulativeAverageDist) <
                                Math.Abs(closeIndex.Dist - dist[i].Value - cumulativeAverageDist)))
                                {
                                    closeIndex = IndDist;
                                }

                                if (lastMatch >= 0)
                                {
                                    //Only check forward, i.e follow the same direction
                                    //Use a close enough stretch
                                    //This is the iffiest part of the algorithm matching stretches...
                                    if (IndDist.low >= lastIndex.low && IndDist.low <= lastIndex.high ||
                                         IndDist.Index >= lastIndex.low && IndDist.Index <= lastIndex.high)
                                    {
                                        //The grid overlaps the old grid
                                        if (prio > 0 || tmpInd == null || IndDist.Diff < tmpInd.Diff)
                                        {
                                            tmpInd = IndDist;
                                        }
                                        prio = 0;
                                    }
                                    else if (prio >= 10 &&
                                         IndDist.low >= lastIndex.low && IndDist.low <= lastIndex.high + ExtraGridIndex ||
                                         IndDist.Index >= lastIndex.low && IndDist.Index <= lastIndex.high + ExtraGridIndex)
                                    {
                                        //Grids are not overlapping, but adjacent points match
                                        if (prio > 10 || tmpInd == null || IndDist.Diff < tmpInd.Diff)
                                        {
                                            tmpInd = IndDist;
                                        }
                                        prio = 10;
                                    }
                                    else if (prio >= 20 &&
                                          Math.Abs(IndDist.Dist / lastIndex.Dist - 1) < MaxDistDiffFactor)
                                    {
                                        if (prio > 20 || tmpInd == null || IndDist.Diff < tmpInd.Diff)
                                        {
                                            tmpInd = IndDist;
                                        }
                                        prio = 20;
                                    }
                                }
                            }
                        }
                        //tmpInd is best match for thwe stretch, but closeIndex could be a better 
                        if (tmpInd == null ||
                            tmpInd != closeIndex && (
                            MinDistStretch < dist[i].Value - dist[startMatch].Value ||
                                    (Math.Abs(tmpInd.Dist - dist[i].Value) > Settings.Bandwidth) ||
                                    (Math.Abs(tmpInd.Dist - dist[i].Value - cumulativeAverageDist) >
                                Math.Abs(closeIndex.Dist - dist[i].Value - cumulativeAverageDist))))
                        {
                            tmpInd = closeIndex;
                        }
                        else
                        {
                            //The stretch continues
                            isEnd = false;
                        }
                        if (tmpInd != null)
                        {
                            lastIndex = tmpInd;
                            lastMatchRef = lastIndex.Index;
                            lastMatch = i;
                            cumulativeAverageDist += (lastIndex.Diff - dist[i].Value - cumulativeAverageDist) / ++noCumAv;

                            IList<int> s = new List<int>();
                            s.Add(i);
                            s.Add(lastMatchRef);

                            //xxx debug - but good to include distance
                            s.Add(startMatch);
                            s.Add(lastMatch);
                            s.Add(startMatchRef);
                            s.Add(lastMatchRef);
                            //s.Add((int)dist[startMatch].Value);
                            //s.Add((int)dist[lastMatch].Value);
                            //s.Add((int)lastIndex.Dist);
                            result[otherActivity].Add(s);
                        }

                    }
                    //Special check for last point investigated
                    if (otherActivity.GPSRoute.Count - 1 == i)
                    {
                        isEnd = true;
                    }
                    if (0 <= lastMatch && isEnd)
                    {
                        //ignore short stretches
                        if (lastMatch - startMatch > 1)
                        {
                            // end match
                            IList<int> s = new List<int>();
                            s.Add(-i);
                            s.Add(-1);
                            result[otherActivity].Add(s);
                        }
                        lastMatch = -1;
                    }
                    if (currIndex.Count > 0)
                    {
                        if (isEnd)
                        {
                            //start of new match - use best match to reference activity
                            startMatch = i;
                            lastIndex = closeIndex;
                            lastMatchRef = lastIndex.Index;
                            startMatchRef = lastIndex.Index;
                        }
                        lastMatch = i;
                    }
                }
            }

            return result;
        }
        public static IDictionary<IActivity, IList<IList<int>>> findSimilarPoints0(IActivity activity, IList<IActivity> activities)
        {
            GPSGrid grid = new GPSGrid(activity, 1, true);
            IDictionary<IActivity, IList<IList<int>>> result = new Dictionary<IActivity, IList<IList<int>>>();
            double cumulativeAverageDist = 0;
            int noCumAv = 0;
            foreach (IActivity otherActivity in activities)
            {
                        const int ExtraGridIndex = 2;
                        const float MaxDistDiffFactor = 0.1F;
                        double MinDistStretch = Settings.Bandwidth*2;

                result.Add(otherActivity, new List<IList<int>>());
                IDistanceDataTrack dist = otherActivity.GPSRoute.GetDistanceMetersTrack();
                int lastMatch = -1; //index of previous match
                int startMatch = -1;
                int lastMatchRef = -1;
                int startMatchRef = -1;
                IndexDiffDist lastIndex = new IndexDiffDist();
                for (int i = 0; i < otherActivity.GPSRoute.Count; i++)
                {
                    IList<IndexDiffDist> currIndex = new List<IndexDiffDist>();
                    IndexDiffDist closeIndex = null;
                    bool isEnd = true; //This is the end of a stretch, unless a matching point is found
                    if (activity.Equals(otherActivity))
                    {
                        //xxx
                        startMatchRef = startMatchRef;

                        if (otherActivity.GPSRoute.Count - 1 == i)
                        {
                            isEnd = true;
                        }
                    }
                    currIndex = grid.getAllCloseStretch(otherActivity.GPSRoute[i].Value);
                    if (currIndex.Count > 0)
                    {
                        IndexDiffDist tmpInd = null;
                        int prio = int.MaxValue;
                        foreach (IndexDiffDist IndDist in currIndex)
                        {
                            if (IndDist.Index > -1)
                            {
                                if ( closeIndex == null ||
                                    (Math.Abs(IndDist.Dist - dist[i].Value) < Settings.Bandwidth) || 
                                    (Math.Abs(IndDist.Dist - dist[i].Value - cumulativeAverageDist) <
                                Math.Abs(closeIndex.Dist - dist[i].Value - cumulativeAverageDist)))
                                {
                                    closeIndex = IndDist;
                                }

                                if (lastMatch >= 0)
                                {
                                    //Only check forward, i.e follow the same direction
                                    //Use a close enough stretch
                                    //This is the iffiest part of the algorithm matching stretches...
                                    if (IndDist.low >= lastIndex.low && IndDist.low <= lastIndex.high ||
                                         IndDist.Index >= lastIndex.low && IndDist.Index <= lastIndex.high)
                                    {
                                        //The grid overlaps the old grid
                                        if (prio > 0 || tmpInd == null || IndDist.Diff < tmpInd.Diff)
                                        {
                                            tmpInd = IndDist;
                                        }
                                        prio = 0;
                                    }
                                    else if (prio >= 10 &&
                                         IndDist.low >= lastIndex.low && IndDist.low <= lastIndex.high + ExtraGridIndex ||
                                         IndDist.Index >= lastIndex.low && IndDist.Index <= lastIndex.high + ExtraGridIndex)
                                    {
                                        //Grids are not overlapping, but adjacent points match
                                        if (prio > 10 || tmpInd == null || IndDist.Diff < tmpInd.Diff)
                                        {
                                            tmpInd = IndDist;
                                        }
                                        prio = 10;
                                    }
                                    else if (prio >= 20 &&
                                          Math.Abs(IndDist.Dist / lastIndex.Dist - 1) < MaxDistDiffFactor)
                                    {
                                        if (prio > 20 || tmpInd == null || IndDist.Diff < tmpInd.Diff)
                                        {
                                            tmpInd = IndDist;
                                        }
                                        prio = 20;
                                    }
                                }
                            }
                        }
                        //tmpInd is best match for thwe stretch, but closeIndex could be a better 
                        if (tmpInd == null ||
                            tmpInd != closeIndex &&(
                            MinDistStretch < dist[i].Value - dist[startMatch].Value ||
                                    (Math.Abs(tmpInd.Dist - dist[i].Value) > Settings.Bandwidth) ||
                                    (Math.Abs(tmpInd.Dist - dist[i].Value - cumulativeAverageDist) >
                                Math.Abs(closeIndex.Dist - dist[i].Value - cumulativeAverageDist))))
                        {
                            tmpInd = closeIndex;
                        }
                        else
                        {
                            //The stretch continues
                            isEnd = false;
                        }
                        if (tmpInd != null)
                        {
                            lastIndex = tmpInd;
                            lastMatchRef = lastIndex.Index;
                            lastMatch = i;
                            cumulativeAverageDist += (lastIndex.Diff - dist[i].Value - cumulativeAverageDist) / ++noCumAv;
                        }

                    }
                    //Special check for last point investigated
                    if (otherActivity.GPSRoute.Count - 1 == i)
                    {
                        isEnd = true;
                    }
                    if (0 <= lastMatch && isEnd)
                    {
                        //ignore short stretches
                        if (lastMatch - startMatch > 1)
                        {
                            // end match
                            IList<int> s = new List<int>();
                            s.Add(startMatch);
                            s.Add(lastMatch);
                            s.Add(startMatchRef);
                            s.Add(lastMatchRef);
                            s.Add((int)dist[startMatch].Value);
                            s.Add((int)dist[lastMatch].Value);
                            s.Add((int)lastIndex.Dist);
                            result[otherActivity].Add(s);
                        }
                        lastMatch = -1;
                    }
                    if (currIndex.Count > 0)
                    {
                        if (isEnd)
                        {
                            //start of new match - use best match to reference activity
                            startMatch = i;
                                     lastIndex = closeIndex;
                            lastMatchRef = lastIndex.Index;
                            startMatchRef = lastIndex.Index;
                        }
                        lastMatch = i;
                    }
                }
            }
                    
            return result;
        }
    }
}
