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
using ZoneFiveSoftware.Common.Data.GPS;
using GpsRunningPlugin.Source;
using ZoneFiveSoftware.Common.Data.Fitness;

namespace GpsRunningPlugin.Source
{
    public class IndexDiffDist
    {
        public int Index, low, high;
        public double Diff;
        public double Dist;
        public IndexDiffDist() : this(-1,-1,-1, double.MaxValue,double.MaxValue)
        {
        }
        public IndexDiffDist(int Index, int low, int high, double Diff, double Dist)
        {
            this.Index = Index;
            this.low = low;
            this.high = high;
            this.Diff = Diff;
            this.Dist = Dist;
        }
    }

    class GPSGrid
    {
        private readonly double m_Width;
        private readonly double m_Distance;

        private IDictionary<int, IDictionary<int, IList<int>>> Grid;
        private readonly IGPSRoute Route;
        private IDistanceDataTrack Dist;

        public GPSGrid(IActivity activity)
            : this(activity, 1, false)
        { }
        public GPSGrid(IActivity activity, double BWidthFactor, bool isDist)
            : this(activity.GPSRoute, BWidthFactor, BWidthFactor, isDist)
        { }
        public GPSGrid(IGPSRoute route)
            : this(route, 1, 1, false)
        { }

        public GPSGrid(IGPSRoute route, double BWidthFactor, double DistFactor, bool isDist)
        {
            m_Width = BWidthFactor * Settings.Radius / (60 * 60 * 30.9);
            if (m_Width < 0.001) m_Width = 0.001;
            m_Distance = DistFactor * Settings.Radius;          
            Grid = new Dictionary<int, IDictionary<int, IList<int>>>();
            Route = route; //Just copy the reference
            if (isDist)
            {
                Dist = route.GetDistanceMetersTrack();
            }
            else
            {
                Dist = null;
            }
            for (int i = 0; i < route.Count; i++ )
            {
                add(i);
            }
        }

        private void add(int i)
        {
            int x = (int)Math.Floor(Route[i].Value.LongitudeDegrees / m_Width);
            int y = (int)Math.Floor(Route[i].Value.LatitudeDegrees / m_Width);
            if (!Grid.ContainsKey(x))
            {
                Grid.Add(x, new Dictionary<int, IList<int>>());
            }
            if (!Grid[x].ContainsKey(y))
            {
                Grid[x].Add(y, new List<int>());
            }
            Grid[x][y].Add(i);
        }

        //Get all points close to the asking point, but merge in "stretches" to let caller find best match
        public IList<IndexDiffDist> getAllCloseStretch(IGPSPoint point)
        {
            const int boxsize = 2;
            IList<IndexDiffDist> result = new List<IndexDiffDist>();
            int x = (int)Math.Floor(point.LongitudeDegrees / m_Width);
            int y = (int)Math.Floor(point.LatitudeDegrees / m_Width);
            for (int i = x - boxsize; i <= x + boxsize; i++)
            {
                if (Grid.ContainsKey(i))
                {
                    for (int j = y - boxsize; j <= y + boxsize; j++)
                    {
                        if (Grid[i].ContainsKey(j))
                        {
                            foreach (int p in Grid[i][j])
                            {
                                IGPSPoint pointInGrid = Route[p].Value;
                                double diffDist = point.DistanceMetersToPoint(pointInGrid);
                                if (diffDist < m_Distance)
                                {
                                    double totDist = double.MaxValue;
                                    if (null != Dist)
                                    {
                                        totDist = Dist[p].Value;
                                    }
                                    IndexDiffDist t = new IndexDiffDist(p, p, p, diffDist, totDist);
                                    result.Add(t);
                                }
                            }
                        }
                    }
                }
            }

            //Group the results with local minimums in "stretches"
            //Only directly adjacent stretches are merged now, so single points out of the box will split stretches
            if (result.Count > 0)
            {
                for (int i = 0; i < result.Count; i++)
                {
                    for (int j = 0; j < result.Count; j++)
                    {
                        if (i != j && result[i].Index > -1 && result[j].Index > -1)
                        {
                            int k = Math.Min(i, j);
                            int l = Math.Max(i, j);
                            //Try merge with lower
                            if ((result[k].high + 1 >= result[l].low && result[k].high <= result[l].high) ||
                                Math.Abs(result[k].Dist - result[l].Dist) < m_Distance)
                            {
                                int tmp;
                                if (result[k].Diff < result[l].Diff)
                                {
                                    //Use lower also at equal
                                    tmp = k;
                                    result[l].Index = -1;
                                }
                                else
                                {
                                    tmp = l;
                                    result[k].Index = -1;
                                }
                                result[tmp].low = Math.Min(result[i].low, result[j].low);
                                result[tmp].high = Math.Max(result[i].high, result[j].high);
                            }
                        }
                    }
                }
                for (int i = result.Count-1; i >= 0; i--)
                {
                    if (result[i].Index == -1)
                    {
                        result.RemoveAt(i);
                    }
                }
            }
            return result;
        }

        //Get a close enough point, not necessarily the closest or best match
        public int getClosePoint(IGPSPoint point)
        {
            IList<int> result = new List<int>();
            int x = (int)Math.Floor(point.LongitudeDegrees / m_Width);
            int y = (int)Math.Floor(point.LatitudeDegrees / m_Width);
            foreach (int i in new int[] { x, x - 1, x + 1 })
            {
                if (Grid.ContainsKey(i))
                {
                    foreach (int j in new int[] { y, y - 1, y + 1 })
                    {
                        if (Grid[i].ContainsKey(j))
                        {
                            foreach (int p in Grid[i][j])
                            {
                                IGPSPoint pointInGrid = Route[p].Value;
                                double diffDist = point.DistanceMetersToPoint(pointInGrid);
                                if (diffDist < m_Distance)
                                {
                                    return p;
                                }
                            }
                        }
                    }
                }
            }

            return -1;
        }
    }
}
