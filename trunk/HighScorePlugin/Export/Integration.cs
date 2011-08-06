/*
Copyright (C) 2011 Gerhard Olsson

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
using ZoneFiveSoftware.Common.Data;
using ZoneFiveSoftware.Common.Data.Fitness;
using ZoneFiveSoftware.Common.Data.GPS;
using ZoneFiveSoftware.Common.Visuals.Fitness;
using TrailsPlugin.Data;
using GpsRunningPlugin.Source;

namespace HighScore.Export
{
    public static class HighScore
    {
        //Compability
        public static IList<IList<Object>> getFastestTimesOfDistances(IList<IActivity> activities, IList<double> distances, System.Windows.Forms.ProgressBar progress)
        {
            return GpsRunningPlugin.Source.HighScore.getFastestTimesOfDistances(activities, distances, progress);
        }

        public static IList<IList<Object>> getResults(IList<IActivity> activities, System.Windows.Forms.ProgressBar progress)
        {
            IList<Goal> goals = GpsRunningPlugin.Source.HighScore.generateGoals();

            Result[] results = GpsRunningPlugin.Source.HighScore.calculate2(activities, goals, progress);
            IList<IList<Object>> objects = new List<IList<Object>>();
            foreach (Result result in results)
            {
                if (result != null)
                {
                    TrailsItemTrackSelectionInfo res = new TrailsItemTrackSelectionInfo();
                    res.MarkedTimes = new ValueRangeSeries<DateTime> { new ValueRange<DateTime>(result.DateStart, result.DateEnd) };
                    string tt = GpsRunningPlugin.Util.StringResources.Goal + ": " + result.Goal.ToString(GpsRunningPlugin.Source.HighScoreViewer.getMostUsedSpeedUnit(activities));
                    IList<Object> s = new List<Object>();
                    s.Add(result.Activity);
                    s.Add(res);
                    s.Add(tt);
                    objects.Add(s);
                }
                else
                {
                    objects.Add(null);
                }
            }
            return objects;
        }
    }
}
