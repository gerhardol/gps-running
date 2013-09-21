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
    ///Draft for a public interface for export for other plugins
    public interface IPerformancePredictorExport
    {
        void PerformancePredictorPopup(IList<IActivity> activities, IDailyActivityView view, TimeSpan time, double distance, System.Windows.Forms.ProgressBar progressBar);

        //public class PerformancePredictorFieldArgs
        //{
        //    public PerformancePredictorFieldArgs(IActivity activity, double time, double distance)
        //    {
        //        this.activity = activity;
        //        this.time = time;
        //        this.distance = distance;
        //    }
        //    public IActivity activity;
        //    public double time, distance;
        //}

        //public class PerformancePredictorResult
        //{
        //    public class Predicted
        //    {
        //        public double new_dist, new_time;
        //        public Predicted(double new_dist, double new_time)
        //        {
        //            this.new_dist = new_dist;
        //            this.new_time = new_time;
        //        }
        //    }
        //    public PerformancePredictorResult(IList<Object> o)
        //    {
        //        this.vo2max = (double)o[0];
        //        this.vdot = (double)o[1];
        //        this.predicted = new List<Predicted>();
        //        for (int i = 2; i < o.Count - 1; i += 2)
        //        {
        //            predicted.Add(new Predicted((double)o[i], (double)o[i + 1]));
        //        }
        //    }
        //    public double vo2max, vdot;
        //    public IList<Predicted> predicted;
        //}

        ///// <summary>
        ///// Calculate PP externally
        ///// </summary>
        ///// <param name="activities"></param>
        ///// <param name="progressBar"></param>
        ///// <returns></returns>
        //public static IList<PerformancePredictorResult> getResults(IList<PerformancePredictorFieldArgs> activities, IList<double> predictDistances, System.Windows.Forms.ProgressBar progressBar);
    }

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
        /// Calculate PP externally
        /// </summary>
        /// <param name="activities"></param>
        /// <param name="parts"></param>
        /// <param name="progressBar"></param>
        /// <returns></returns>
        public static IList<IList<Object>> getResults(IList<IActivity> activities, IList<double> times, IList<double> distances, IList<double> predictDistances, IList<double> old_times2, System.Windows.Forms.ProgressBar progressBar)
        {
            IList<IList<Object>> objects = new List<IList<Object>>();
            if (activities != null && activities.Count > 0 &&
                activities.Count == times.Count &&
                activities.Count == distances.Count)
            {
                for (int i = 0; i < activities.Count; i++)
                {
                    IActivity activity = activities[i];
                    double old_time = times[i];
                    double old_dist = distances[i];
                    double old_time2 = old_times2[i];

                    IList<Object> s = new List<Object>();
                    double vo2max = Predict.getVo2max(old_time);
                    s.Add(vo2max);
                    double vdot = Predict.getVdot(old_time, old_dist);
                    s.Add(vdot);

                    foreach (double predDist in predictDistances)
                    {
                        double new_time = double.NaN;
                        if (!double.IsNaN(old_time))
                        {
                            new_time = (Predict.Predictor(Settings.Model))(predDist, old_dist, TimeSpan.FromSeconds(old_time));
                        }
                        double ideal_time = double.NaN;
                        if (!double.IsNaN(old_time2))
                        {
                            ideal_time = ExtrapolateView.GetIdeal(activity, predDist, old_dist, old_time2);
                        }
                        s.Add(predDist);
                        s.Add(new_time);
                        s.Add(ideal_time);
                    }
                    objects.Add(s);
                }
            }
            return objects;
        }
    }
}
