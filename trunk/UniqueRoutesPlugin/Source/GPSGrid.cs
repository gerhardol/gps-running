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
using ZoneFiveSoftware.Common.Data.GPS;
using SportTracksUniqueRoutesPlugin.Source;
using ZoneFiveSoftware.Common.Data.Fitness;

namespace SportTracksUniqueRoutesPlugin.Source
{
    class GPSGrid
    {
        private readonly double Width;

        private IDictionary<int, IDictionary<int, IList<IGPSPoint>>> Grid;

        private readonly double Distance;

        public GPSGrid(IActivity activity)
        {
            Width = (Settings.Bandwidth) / (60 * 60 * 30.9);
            if (Width < 0.001) Width = 0.001;
            Distance = Settings.Bandwidth / 2;            
            Grid = new Dictionary<int, IDictionary<int, IList<IGPSPoint>>>();
            foreach (IGPSPoint point in activity.GPSRoute.GetValueEnumerator())
            {
                add(point);
            }
        }

        public void add(IGPSPoint point)
        {
            int x = (int)Math.Floor(point.LongitudeDegrees / Width);
            int y = (int)Math.Floor(point.LatitudeDegrees / Width);
            if (!Grid.ContainsKey(x))
            {
                Grid.Add(x, new Dictionary<int, IList<IGPSPoint>>());
            }
            if (!Grid[x].ContainsKey(y))
            {
                Grid[x].Add(y, new List<IGPSPoint>());
            }
            Grid[x][y].Add(point);
        }


        public IGPSPoint getClosests(IGPSPoint point)
        {
            int x = (int)Math.Floor(point.LongitudeDegrees / Width);
            int y = (int)Math.Floor(point.LatitudeDegrees / Width);
            for (int i = x - 1; i <= x + 1; i++)
            {
                if (Grid.ContainsKey(i))
                {
                    for (int j = y - 1; j <= y + 1; j++)
                    {
                        if (Grid[i].ContainsKey(j))
                        {
                            foreach (IGPSPoint pointInGrid in Grid[i][j])
                            {
                                if (point.DistanceMetersToPoint(pointInGrid) < Distance)
                                {
                                    return pointInGrid;
                                }
                            }
                        }
                    }
                }
            }

            return null;
        }
    }
}
