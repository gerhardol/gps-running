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
        //Create grid in which gps points are put. Each grid size is the configured radius
        //As a point can be located close to the border of each cell, cells close by must be checked too
        //Decreasing the cell size and including more cells decreases speed 
        private readonly double m_Distance;
        private readonly double m_latWidth;
        private readonly double m_lngWidth;

        private IDictionary<int, IDictionary<int, IList<int>>> m_Grid;
        private readonly IGPSRoute m_Route;
        private IDistanceDataTrack m_Dist;

        public GPSGrid(GPSGrid refGrid, IActivity activity)
            : this(refGrid, activity, 1, false)
        { }
        public GPSGrid(GPSGrid refGrid, IActivity activity, double BWidthFactor, bool isDist)
            : this(refGrid, activity.GPSRoute, BWidthFactor, BWidthFactor, isDist)
        { }
        public GPSGrid(GPSGrid refGrid, IGPSRoute route)
            : this(refGrid, route, 1, 1, false)
        { }

        public GPSGrid(GPSGrid refGrid, IGPSRoute route, double BWidthFactor, double DistFactor, bool isDist)
        {
            m_Distance = DistFactor * Settings.Radius;
            m_Grid = new Dictionary<int, IDictionary<int, IList<int>>>();
            m_Route = route; //Just copy the reference
            if (refGrid == null)
            {
                //Set grid size from aprox distance for reference
                //See Trails plugin, TrailsGPSLocation.getGPSBounds()
                m_latWidth = BWidthFactor * Settings.Radius / 110574 * 1.005F;
                m_lngWidth = BWidthFactor * Settings.Radius / 111132 / Math.Cos(m_Route[0].Value.LongitudeDegrees * Math.PI / 180);
            }
            else
            {
                m_latWidth = refGrid.m_latWidth;
                m_lngWidth = refGrid.m_lngWidth;
            }
            if (isDist)
            {
                m_Dist = m_Route.GetDistanceMetersTrack();
            }
            else
            {
                m_Dist = null;
            }
            for (int i = 0; i < m_Route.Count; i++ )
            {
                addGrid(i);
            }
        }

        private void addGrid(int i)
        {
            int x = (int)Math.Floor(m_Route[i].Value.LongitudeDegrees / m_lngWidth);
            int y = (int)Math.Floor(m_Route[i].Value.LatitudeDegrees / m_latWidth);
            if (!m_Grid.ContainsKey(x))
            {
                m_Grid.Add(x, new Dictionary<int, IList<int>>());
            }
            if (!m_Grid[x].ContainsKey(y))
            {
                m_Grid[x].Add(y, new List<int>());
            }
            m_Grid[x][y].Add(i);
        }

        //Get all points close to the asking point, but merge in "stretches" to let caller find best match
        public IList<IndexDiffDist> getAllCloseStretch(IGPSPoint point)
        {
            const int boxsize = 2;
            IList<IndexDiffDist> result = new List<IndexDiffDist>();
            int x = (int)Math.Floor(point.LongitudeDegrees / m_lngWidth);
            int y = (int)Math.Floor(point.LatitudeDegrees / m_latWidth);
            for (int i = x - boxsize; i <= x + boxsize; i++)
            {
                if (m_Grid.ContainsKey(i))
                {
                    for (int j = y - boxsize; j <= y + boxsize; j++)
                    {
                        if (m_Grid[i].ContainsKey(j))
                        {
                            foreach (int p in m_Grid[i][j])
                            {
                                IGPSPoint pointInGrid = m_Route[p].Value;
                                double diffDist = point.DistanceMetersToPoint(pointInGrid);
                                if (diffDist < m_Distance)
                                {
                                    double totDist = double.MaxValue;
                                    if (null != m_Dist)
                                    {
                                        totDist = m_Dist[p].Value;
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
            int x = (int)Math.Floor(point.LongitudeDegrees / m_lngWidth);
            int y = (int)Math.Floor(point.LatitudeDegrees / m_latWidth);
            foreach (int i in new int[] { x, x - 1, x + 1 })
            {
                if (m_Grid.ContainsKey(i))
                {
                    foreach (int j in new int[] { y, y - 1, y + 1 })
                    {
                        if (m_Grid[i].ContainsKey(j))
                        {
                            foreach (int p in m_Grid[i][j])
                            {
                                IGPSPoint pointInGrid = m_Route[p].Value;
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
