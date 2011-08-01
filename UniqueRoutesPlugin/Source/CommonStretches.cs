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
using ZoneFiveSoftware.Common.Visuals.Fitness;
using GpsRunningPlugin;
using GpsRunningPlugin.Source;
using System.Windows.Forms;
using System.Collections;
using TrailsPlugin.Data;

namespace GpsRunningPlugin.Source
{
    public class PointInfo 
    {
        public PointInfo(int index) : this (index, -1, -1, false)
        {
        }
        public PointInfo(int index, double distance, double time, bool restLap)
        {
            this.index = index;
            this.distance = distance;
            this.time = time;
            this.restLap = restLap;
        }
        //Temporary (?) debuginfo
        public PointInfo(int index, double distance, double time, bool restLap, string debugInfo) : this (index, distance, time, restLap)
        {
            this.debugInfo = debugInfo;
        }
        public int index =0;
        public double distance=0;
        public double time=0;
        public bool restLap=false;
        public string debugInfo = "";
    }
    public static class CommonStretches
    {
        //Note: Using IValueRangeSeries will automerge reference results. Ref may not have same no of stretches
        public static IItemTrackSelectionInfo[] getSelInfo(DateTime[] st, IList<PointInfo[]> pi, bool useActive)
        {
            PointInfo[] pi2;
            return getSelInfo(st, pi, out pi2, useActive);
        }
        public static IItemTrackSelectionInfo[] getSelInfo(DateTime[] st, IList<PointInfo[]> pi, out PointInfo[] pi2, bool useActive)
        {
            //Data.TrailsItemTrackSelectionInfo result = new Data.TrailsItemTrackSelectionInfo();
            TrailsItemTrackSelectionInfo[] res = new TrailsItemTrackSelectionInfo[]{ new TrailsItemTrackSelectionInfo(), new TrailsItemTrackSelectionInfo() };
            res[0].MarkedTimes = new ValueRangeSeries<DateTime>();
            res[0].MarkedDistances = new ValueRangeSeries<double>();
            res[1].MarkedTimes = new ValueRangeSeries<DateTime>();
            res[1].MarkedDistances = new ValueRangeSeries<double>();
            pi2 = new PointInfo[2] { new PointInfo(0,0,0,false), new PointInfo(0,0,0,false) };

            PointInfo[] startIndex = null;
            PointInfo[] prevIndex = null;
            foreach (PointInfo[] i in pi)
            {
                //TODO: Rest/lap transitions incorrect
                if (i[0].index >= 0 && (!useActive || !(i[0].restLap || i[1].restLap)))
                {
                    if (startIndex == null || startIndex[0].index < 0)
                    {
                        startIndex = i;
                    }
                    prevIndex = i;
                }
                else
                {
                    if (startIndex != null && prevIndex != null && 
                    startIndex[0].index > -1 && prevIndex[0].index > -1)
                    {
                        const int minStretchLength = 3;
                        //End - Update summary
                        //Ignore single/double point matches and "reverse" reference matches
                        //Set both MarkedTimes, MarkedDistances for convenience
                        if (prevIndex[0].index - startIndex[0].index >= minStretchLength &&
                            startIndex[1].index <= prevIndex[1].index)
                        {
                            for (int j = 0; j <= 1; j++)
                            {
                                pi2[j].index++;
                                pi2[j].time += prevIndex[j].time - startIndex[j].time;
                                pi2[j].distance += prevIndex[j].distance - startIndex[j].distance;
                                res[j].MarkedTimes.Add(new ValueRange<DateTime>(
                                    st[j].AddSeconds(startIndex[j].time),
                                    st[j].AddSeconds(prevIndex[j].time)));
                                res[j].MarkedDistances.Add(new ValueRange<double>(
                                    startIndex[j].distance, prevIndex[j].distance));
                            }
                        }
                    }
                    startIndex = null;
                    prevIndex = null;
                }
            }
            return res;
        }
        public static IDictionary<IActivity, PointInfo[]> getCommonSpeed(IActivity activity, IList<IActivity> activities, bool useActive)
        {
            return getCommonSpeed(activity.GPSRoute, activities, useActive);
        }
        public static IDictionary<IActivity, PointInfo[]> getCommonSpeed(IGPSRoute refRoute, IList<IActivity> activities, bool useActive)
        {
            IDictionary<IActivity, IList<PointInfo[]>> points = new Dictionary<IActivity, IList<PointInfo[]>>();
            points = findSimilarPoints(refRoute, activities, null);
            return getCommonSpeed(points, activities, useActive, null);
        }

        public static IDictionary<IActivity, PointInfo[]> getCommonSpeed(IDictionary<IActivity, IList<PointInfo[]>> points, IList<IActivity> activities, bool useActive, System.Windows.Forms.ProgressBar progressBar)
        {
            IDictionary<IActivity, PointInfo[]> result = new Dictionary<IActivity, PointInfo[]>();
            if (progressBar != null)
            {
                progressBar.Value = 0;
                progressBar.Minimum = 0;
                progressBar.Maximum = activities.Count;
            }
            foreach (IActivity otherActivity in activities)
            {
                //double MinDistStretch = Settings.Radius*2*2;
                //totDist, totTime, totDistRef, totTimeRef, noStretches
                PointInfo[] res;

                if (points.ContainsKey(otherActivity))
                {
                    IItemTrackSelectionInfo[] i = getSelInfo(new DateTime[] { otherActivity.StartTime, otherActivity.StartTime },
                        points[otherActivity], out res, useActive);

                    result.Add(otherActivity, res);
                }
                if (progressBar != null)
                {
                    progressBar.Increment(1);
                }
            }
            return result;
        }
        public static IDictionary<IActivity, IList<PointInfo[]>> findSimilarPoints(IActivity activity, IList<IActivity> activities, System.Windows.Forms.ProgressBar progressBar)
        {
            return findSimilarPoints(activity.GPSRoute, activity.Laps, activities, progressBar);
        }
        public static IDictionary<IActivity, IList<PointInfo[]>> findSimilarPoints(IGPSRoute refRoute, IList<IActivity> activities, System.Windows.Forms.ProgressBar progressBar)
        {
            return findSimilarPoints(refRoute, null, activities, progressBar);
        }

        public static IDictionary<IActivity, IList<PointInfo[]>> findSimilarPoints(IGPSRoute refRoute, IActivityLaps refLaps, IList<IActivity> activities, System.Windows.Forms.ProgressBar progressBar)
        {
            GPSGrid refGrid = new GPSGrid(null, refRoute, 1F, 0.5F, true);
            IDictionary<IActivity, IList<PointInfo[]>> result = new Dictionary<IActivity, IList<PointInfo[]>>();
            double cumulativeAverageDist = 0;
            int noCumAv = 0;
            if (activities != null)
            {
                if (progressBar != null)
                {
                    progressBar.Value = 0;
                    progressBar.Minimum = 0;
                    progressBar.Maximum = activities.Count;
                }
                foreach (IActivity otherActivity in activities)
                {
                    const int ExtraGridIndex = 2;
                    const float MaxDistDiffFactor = 0.1F;
                    double minDistStretch = Settings.Radius * 2;

                    result.Add(otherActivity, new List<PointInfo[]>());
                    IDistanceDataTrack distTrack = otherActivity.GPSRoute.GetDistanceMetersTrack();
                    int lastMatch = -1; //index of previous match
                    int startMatch = -1;
                    IndexDiffDist lastMatchRef = null;
                    IndexDiffDist startMatchRef = null;
                    IndexDiffDist lastIndex = null;
                    for (int i = 0; i < otherActivity.GPSRoute.Count; i++)
                    {
                        IList<IndexDiffDist> currIndex = new List<IndexDiffDist>();
                        IndexDiffDist closeIndex = null; //Closest to current point
                        IndexDiffDist nextIndex = null; //The best match in this stretch
                        bool isEnd = true; //This is the end of a stretch, unless a matching point is found
                        currIndex = refGrid.getAllCloseStretch(otherActivity.GPSRoute[i].Value);
                        if (currIndex.Count > 0)
                        {
                            int prio = int.MaxValue;
                            foreach (IndexDiffDist IndDist in currIndex)
                            {
                                if (IndDist.Index > -1)
                                {
                                    //Get the closest good enough point - used at start and could restart current stretch
                                    if (closeIndex == null ||
                                        //Close match in distance
                                        (Math.Abs(IndDist.Dist - distTrack[i].Value) < 3 * Settings.Radius) ||
                                        //Close to other matches
                                        (Math.Abs(IndDist.Dist - distTrack[i].Value - cumulativeAverageDist) <
                                    Math.Abs(closeIndex.Dist - distTrack[i].Value - cumulativeAverageDist)))
                                    {
                                        closeIndex = IndDist;
                                    }

                                    //next in relation to previous match
                                    if (lastMatch > -1 && lastIndex != null)
                                    {
                                        //Only check forward, i.e follow the same direction
                                        //Use a close enough stretch
                                        //This is the iffiest part of the algorithm matching stretches...
                                        if ((IndDist.low >= lastIndex.low && IndDist.low <= lastIndex.high ||
                                             IndDist.Index >= lastIndex.low && IndDist.Index <= lastIndex.high) &&
                                            IndDist.Index >= lastIndex.Index)
                                        {
                                            //The grid overlaps the old grid
                                            //The matching index may be lower then this is a restart (this or prev match should then be dropped)
                                            if ((prio > 0 && (nextIndex == null || IndDist.Diff < nextIndex.Diff) && IndDist.Index >= lastIndex.Index))
                                            {
                                                nextIndex = IndDist;
                                                prio = 0;
                                            }
                                            else if (prio >= 0 && (IndDist.Index > lastIndex.Index && (nextIndex == null || nextIndex.Index <= lastIndex.Index)))
                                            {
                                                nextIndex = IndDist;
                                                prio = 0;
                                            }
                                            else if (IndDist.Index == lastIndex.Index && (nextIndex == null || nextIndex.Index < lastIndex.Index))
                                            {
                                                nextIndex = IndDist;
                                                prio = 0;
                                            }
                                            else if (prio > 10)
                                            {
                                                nextIndex = IndDist;
                                                prio = 10;
                                            }
                                            else if (nextIndex == null)
                                            {
                                                nextIndex = IndDist;
                                                prio = 10;
                                            }
                                            //else - not better
                                        }
                                        else if ((
                                             IndDist.low >= lastIndex.low && IndDist.low <= lastIndex.high + ExtraGridIndex ||
                                             IndDist.Index >= lastIndex.low && IndDist.Index <= lastIndex.high + ExtraGridIndex))
                                        {
                                            //Grids are not overlapping, but adjacent points match forward
                                            if (prio > 20 && IndDist.Index > lastIndex.Index)
                                            {
                                                nextIndex = IndDist;
                                                prio = 20;
                                            }
                                            else if (prio >= 20)
                                            {
                                                nextIndex = IndDist;
                                                prio = 20;
                                            }
                                        }
                                        else if ((
                                              Math.Abs(IndDist.Dist / lastIndex.Dist - 1) < MaxDistDiffFactor) &&
                                            (prio > 20))
                                        {
                                            nextIndex = IndDist;
                                            prio = 30;
                                        }
                                    }
                                }
                            }

                            //Update if close enough
                            if (closeIndex != null)
                            {
                                //TODO: This algorithm needs to prio correct direction better
                                //nextInd is best match for the stretch, but closeIndex could be better 
                                if (nextIndex == null ||
                                    (!nextIndex.Equals(closeIndex)) && (
                                    //back match - prefer closer
                                    lastIndex.Index > nextIndex.Index ||
                                    prio > 10 && (minDistStretch < distTrack[i].Value - distTrack[startMatch].Value ||
                                            (Math.Abs(nextIndex.Dist - distTrack[i].Value) > Settings.Radius) ||
                                            (Math.Abs(nextIndex.Dist - distTrack[i].Value - cumulativeAverageDist) >
                                        Math.Abs(closeIndex.Dist - distTrack[i].Value - cumulativeAverageDist)))))
                                {
                                    //switch to closeIndex and ignore nextIndex
                                    nextIndex = closeIndex;
                                    //start of new stretch - use best match to reference activity
                                    startMatch = i;
                                    lastIndex = closeIndex;
                                    startMatchRef = closeIndex;
                                }
                                else
                                {
                                    //The stretch continues
                                    isEnd = false;
                                    cumulativeAverageDist += (lastIndex.Diff - distTrack[i].Value - cumulativeAverageDist) / ++noCumAv;
                                }
                            }
                        }
                        if (isEnd && lastMatch >= 0)
                        {
                            // end match
                            PointInfo[] s = new PointInfo[2];
                            s[0] = new PointInfo(-i - 1);
                            s[1] = new PointInfo(-lastMatchRef.Index - 1);
                            result[otherActivity].Add(s);
                        }
                        if (nextIndex != null)
                        {
                            lastIndex = nextIndex;
                            lastMatchRef = lastIndex;
                            lastMatch = i;

                            PointInfo[] s = new PointInfo[2];
                            s[0] = new PointInfo(i, distTrack[i].Value, otherActivity.GPSRoute[i].ElapsedSeconds,
                                getRestLap(i, otherActivity),
                                startMatch + " " + lastMatch + " " + startMatchRef + " " + lastMatchRef);
                            s[1] = new PointInfo(lastIndex.Index, lastIndex.Dist, refRoute[lastIndex.Index].ElapsedSeconds,
                                getRestLap(lastIndex.Index, refRoute, refLaps));
                            result[otherActivity].Add(s);
                        }
                        else
                        {
                            lastMatch = -1;
                        }
                    }
                    //add end marker if needed
                    if (lastMatch > -1)
                    {
                        PointInfo[] s = new PointInfo[2];
                        s[0] = new PointInfo(-otherActivity.GPSRoute.Count);
                        s[1] = new PointInfo(-1);
                        result[otherActivity].Add(s);
                    }
                    if (progressBar != null)
                    {
                        progressBar.Increment(1);
                    }
                }
            }

            return result;
        }
        static bool getRestLap(int index, IActivity activity)
        {
            return getRestLap(index, activity.GPSRoute, activity.Laps);
        }
        static bool getRestLap(int index, IGPSRoute route, IActivityLaps laps)
        {
            bool restLap = false;
            if (laps != null && laps.Count > 1)
            {
                restLap = laps[laps.Count - 1].Rest;
                for (int i = 0; i < laps.Count - 1; i++)
                {
                    //End time is starttime for next lap
                    //Go from first to second last to find where it fits (default is last, no end time there)
                    if (0 > route.EntryDateTime(route[index]).CompareTo(laps[i + 1].StartTime))
                    {
                        //time was in previous lap
                        restLap = laps[i].Rest;
                        break;
                    }
                }
            }
            return restLap;
        }
    }
}
