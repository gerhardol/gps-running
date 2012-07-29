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
using ZoneFiveSoftware.Common.Data.Algorithm;
using TrailsPlugin.Data;
using GpsRunningPlugin.Source;


namespace PerformancePredictor.Export
{
    public static class PerformancePredictor
    {
        /// <summary>
        /// Popup from time/distance. Activities included for meta data.
        /// </summary>
        /// <param name="activities"></param>
        /// <param name="view"></param>
        /// <param name="time"></param>
        /// <param name="distance"></param>
        /// <param name="progressBar"></param>
        public static void PerformancePredictorPopup(IList<IActivity> activities, IDailyActivityView view, TimeSpan time, double distance, System.Windows.Forms.ProgressBar progressBar)
        {
            new PerformancePredictorControl(activities, view, time, distance, progressBar);
        }

        /// <summary>
        /// Calculate PP externally - incomplete
        /// </summary>
        /// <param name="activities"></param>
        /// <param name="parts"></param>
        /// <param name="progressBar"></param>
        /// <returns></returns>
        public static IList<IList<Object>> getResults(IList<IActivity> activities, IItemTrackSelectionInfo parts, System.Windows.Forms.ProgressBar progressBar)
        {
            IList<TimePredictionResult> results = null;
            if (activities != null && activities.Count > 0)
            {
                if (activities.Count > 1 ||
                (activities.Count == 1 && (Settings.HighScore != null)))
                {
                    //Predict using one or many activities (check done that HS enabled prior)
                    results = TimePredictionView.getResults(null, Predict.Predictor(Settings.Model), activities, progressBar);
                }
                else if (activities[0] != null)
                {
                    //TODO: Calculate time/distance. Should be example in Trails
                    TimeSpan time = TimeSpan.FromSeconds(1);
                    double dist = 1;
                    if (dist > 0 && time.TotalSeconds > 0)
                    {
                        results = TimePredictionView.getResults(null, Predict.Predictor(Settings.Model), activities, dist, time, progressBar);
                    }
                }
            }
            IList<IList<Object>> objects = new List<IList<Object>>();
            if (results == null)
            {
                foreach (TimePredictionResult result in results)
                {
                    if (result != null)
                    {
                        TrailsItemTrackSelectionInfo res = new TrailsItemTrackSelectionInfo();
                        DateTime endTime = DateTimeRangeSeries.AddTimeAndPauses(result.StartDate, result.UsedTime, result.Activity.TimerPauses); //TODO: pauses from marked time
                        res.MarkedTimes = new ValueRangeSeries<DateTime> { new ValueRange<DateTime>(result.StartDate, endTime) };
                        IList<Object> s = new List<Object>();
                        s.Add(result.Activity);
                        s.Add(res);
                        s.Add(result.DistanceNominal);
                        s.Add(result.Distance);
                        objects.Add(s);
                    }
                    else
                    {
                        objects.Add(null);
                    }
                }
            }
            return objects;
        }
    }
}
