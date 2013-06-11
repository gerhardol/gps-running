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
using System.Diagnostics;
//using ZoneFiveSoftware.Common.Visuals.Fitness;
//using ZoneFiveSoftware.Common.Data.GPS;
using ZoneFiveSoftware.Common.Data;
using System.Data;
//using System.Windows.Forms;
//using GpsRunningPlugin.Properties;
using ZoneFiveSoftware.Common.Data.Measurement;
//using ZoneFiveSoftware.Common.Visuals;
//using GpsRunningPlugin.Util;

namespace GpsRunningPlugin.Source
{
    static class HighScore
    {
        //Compatibility with PerformancePredictor
        public static IList<IList<Object>> getFastestTimesOfDistances(IList<IActivity> activities, IList<double> distances, System.Windows.Forms.ProgressBar progressBar)
        {
            return global::HighScore.Export.HighScore.getFastestTimesOfDistances(activities, distances, progressBar);
        }

        public static IList<Result> calculateActivities(IList<IActivity> activities, IList<IValueRangeSeries<DateTime>> pauses, IList<Goal> goals, System.Windows.Forms.ProgressBar progressBar)
        {
            if (progressBar != null)
            {
                progressBar.Minimum = 0;
                progressBar.Value = 0;
                progressBar.Maximum = 0;//Set below
                progressBar.Visible = true;
                progressBar.BringToFront();
            }
            SortedList<Result, Result>[] resultsArray = calculateActivities2(activities, pauses, goals, progressBar);
            IList<Result> results = new List<Result>();
            //Null results must be removed from array
            foreach (SortedList<Result, Result> r in resultsArray)
            {
                if (r != null)
                {
                    //int i = 1;
                    foreach (Result ra in r.Values)
                    {
                        //ra.Order = i;
                        results.Add(ra);
                        //i++;
                    }
                }
            }

            if (progressBar != null)
            {
                progressBar.Visible = false;
            }
            return results;
        }

        //No init of progressbar
        private static SortedList<Result, Result>[] calculateActivities2(IList<IActivity> activities, IList<IValueRangeSeries<DateTime>> pauses, IList<Goal> goals, System.Windows.Forms.ProgressBar progressBar)
        {
            SortedList<Result, Result>[] results = new SortedList<Result, Result>[goals.Count];
            DateTime s = DateTime.Now;
            if (activities != null && activities.Count > 0)
            {
                if (progressBar != null && progressBar.Maximum < progressBar.Value + activities.Count)
                {
                    progressBar.Maximum += activities.Count;
                }
                if (pauses == null)
                {
                    pauses = new List<IValueRangeSeries<DateTime>>();
                    //Pauses not set, use activity pauses
                    foreach (IActivity activity in activities)
                    {
                        pauses.Add(activity.TimerPauses);
                    }
                }
                int i = 0;
                foreach (IActivity activity in activities)
                {
                    if (null != activity && activity.HasStartTime && 
                        (!Settings.IgnoreManualData || /*Settings.IgnoreManualData &&*/ !activity.UseEnteredData))
                    {
                        calculateActivity(activity, pauses[i], goals, results);
                    }
                    if (progressBar != null)
                    {
                        progressBar.Value++;
                    }
                    i++;
                }
            }
            //Debug
            //DateTime s2 = DateTime.Now;
            //results[0] = new Result(goals[0], activities[0], 0, 1, 0, (s2 - s).TotalSeconds, 0, 1000, 1, 100, s, s2);
            return results;
        }

        private static void calculateActivity(IActivity activity, IValueRangeSeries<DateTime> pause, IList<Goal> goals, SortedList<Result, Result>[] results)
        {
            ActInfo act = new ActInfo(activity, pause, goals);
            foreach (Goal goal in goals)
            {
                Result result = null;
                if (Goal.IsZoneGoal(goal.Image))
                {
                    if (act.ZoneOk(goal))
                    {
                        result = calculateActivityZoneGoal(activity, (IntervalsGoal)goal, act,
                                            act.getGoalTrack(goal.Domain),
                                            act.getGoalZoneTrack(goal.Image));
                    }
                }
                else
                {
                    result = calculateActivityPointGoal(activity, (PointGoal)goal, act,
                                        act.getGoalTrack(goal.Domain),
                                        act.getGoalTrack(goal.Image));
                }

                if (result != null)
                {
                    //results array are referenced by goal index. (Could be dictionary)
                    int resultIndex = goals.IndexOf(goal);
                    if (results[resultIndex] == null)
                    {
                        results[resultIndex] = new SortedList<Result, Result>();
                    }
                    if (results[resultIndex].Count > 0 &&
                        results[resultIndex].Count >= goal.Order)
                    {
                        Result last = Result.LastResult(results[resultIndex]);
                        if (result.BetterResult(last))
                        {
                            results[resultIndex].Remove(last);
                        }
                    }
                    if (results[resultIndex].Count < goal.Order)
                    {
                        results[resultIndex].Add(result, result);
                    }
                }
            }
        }

        private static Result calculateActivityZoneGoal(IActivity activity, IntervalsGoal goal, 
            ActInfo act, double[] domain, IList<double[]> image)
        {
            bool foundAny = false;
            int back = 0, front = 0;
            int bestBack = 0, bestFront = 0;
            
            double best;
            if (goal.UpperBound) best = double.MinValue;
            else best = double.MaxValue;

            int length = act.Length;

            while (front < length)
            {
                bool inWindow = true;
                for (int i = 0; i < image.Count; i++)
                {
                    if (image[i][back] < goal.Intervals[i][0] ||
                        image[i][front] > goal.Intervals[i][1])
                    {
                        inWindow = false;
                        break;
                    }
                }
                if (inWindow)
                {
                    double domainDiff = domain[front] - domain[back];
                    int upperBound = goal.UpperBound ? 1 : -1;
                    if (upperBound*best < upperBound*domainDiff &&
                        (goal.Domain == GoalParameter.Elevation ||
                        act.validElevation && (act.aElevation[front] - act.aElevation[back]) / (act.aDistance[front] - act.aDistance[back]) >= Settings.MinGrade))
                    {
                        foundAny = true;
                        best = domainDiff;
                        bestBack = back;
                        bestFront = front;
                    }
                    if (back == front || 
                        (front < length - 1 && isInZone(image, goal, front + 1))) 
                        front++;
                    else back++;
                }
                else
                {
                    if (back == front)
                    {
                        if (front < length - 1 && !isInZone(image, goal, front + 1)) 
                            back++;
                        front++;
                    }
                    else back++;
                }
            }

            if (foundAny)
            {
                return new Result(goal, activity, act.Pauses, domain[bestBack], domain[bestFront], act.aTime[bestBack], act.aTime[bestFront],
                    act.aDistance[bestBack], act.aDistance[bestFront], act.aElevation[bestBack], act.aElevation[bestFront],
                    act.aDateTime[bestBack], act.aDateTime[bestFront]);
            }
            return null;
        }

        private static bool isInZone(IList<double[]> image, IntervalsGoal goal, int index)
        {
            for (int i = 0; i < image.Count; i++)
            {
                if (image[i][index] < goal.Intervals[i][0] ||
                    image[i][index] > goal.Intervals[i][1])
                {
                    return false;
                }
            }
            return true;
        }

        private static Result calculateActivityPointGoal(IActivity activity, PointGoal goal, 
            ActInfo act, double[] domain, double[] image)
        {
            bool foundAny = false;
            int back = 0, front = 0;
            int bestBack = 0, bestFront = 0;

            double best;
            if (goal.UpperBound) best = double.MinValue;
            else best = double.MaxValue;

            while (front < act.Length && back < act.Length)
            {
                if (image[front] - image[back] >= goal.Value)
                {
                    double domainDiff = domain[front] - domain[back];
                    int upperBound = goal.UpperBound ? 1 : -1;
                    if (upperBound*best < upperBound*domainDiff &&
                        (!act.validElevation || act.validElevation && (act.aElevation[front] - act.aElevation[back]) / (act.aDistance[front] - act.aDistance[back]) >=
                        Settings.MinGrade))
                    {
                        foundAny = true;
                        best = domainDiff;
                        bestBack = back;
                        bestFront = front;
                    }
                    back++;
                }
                else
                {
                    front++;
                }
            }
            if (foundAny)
            {
                return new Result(goal, activity, act.Pauses, domain[bestBack], domain[bestFront], act.aTime[bestBack], act.aTime[bestFront],
                    act.aDistance[bestBack], act.aDistance[bestFront], act.aElevation[bestBack], act.aElevation[bestFront],
                    act.aDateTime[bestBack], act.aDateTime[bestFront]);
            }
            return null;
        }
    }

    ///
    /// Encapsulate the activities somehow. 
    /// Many calls of track.GetInterpolatedValue is too slow, use laps to chop up and put in arrays
    /// The caller uses the arrays directly, so encapsulation is weak.
    /// 
    class ActInfo
    {
        public double[] aDistance, aTime, aElevation, aPulse, aSpeed;
        public DateTime[] aDateTime;
        public bool validElevation = true;
        public int Length;
        public IValueRangeSeries<DateTime> Pauses;

        public ActInfo(IActivity activity, IValueRangeSeries<DateTime> pauses, IList<Goal> goals)
        {
            this.Pauses = pauses;
            ActivityInfo info = ActivityInfoCache.Instance.GetInfo(activity);
            int increment = 5;

        //restart:
            {
                //TBD: The following is what consumes almost all CPU, what is slowing down
                IList<LapDetailInfo> laps = info.DistanceLapDetailInfo(20 + increment);
                int length = laps.Count + 1;

                aDateTime = new DateTime[length];
                aTime = new double[length];
                aDistance = new double[length];
                aElevation = new double[length];

                foreach (Goal goal in goals)
                {
                    if (goal.Image == GoalParameter.PulseZone ||
                        goal.Image == GoalParameter.PulseZoneSpeedZone)
                    {
                        if (info.SmoothedHeartRateTrack.Max > 0)
                        {
                            aPulse = new double[length];
                        }
                        break;
                    }
                }
                foreach (Goal goal in goals)
                {
                    if (goal.Image == GoalParameter.SpeedZone ||
                        goal.Image == GoalParameter.PulseZoneSpeedZone)
                    {
                        aSpeed = new double[length];
                        break;
                    }
                }

                DateTime dateTime = activity.StartTime;
                aDateTime[0] = dateTime;

                int index = 0;
                double timeOffset = 0;
                float distOffset = 0;
                foreach (LapDetailInfo lap in laps)
                {
                    dateTime = lap.EndTime;
                    if (ZoneFiveSoftware.Common.Data.Algorithm.DateTimeRangeSeries.IsPaused(lap.StartTime, pauses) ||
                        ZoneFiveSoftware.Common.Data.Algorithm.DateTimeRangeSeries.IsPaused(lap.EndTime, pauses))
                    {
                        //Adjust the extracted track info to pauses
                        timeOffset += lap.LapElapsed.TotalSeconds;
                        distOffset += lap.LapDistanceMeters;
                        //Skip this lap (only start checked)
                        continue;
                    }
                    if (index == 0)
                    {
                        //First point not yet valid, set 0 index
                        updateTracks(info, index, lap.StartTime, lap.StartElapsed.TotalSeconds - timeOffset, lap.StartDistanceMeters - distOffset);
                    }
                    index++;
                    
                    //Time and distance must be calculated in the same way, why the following will not work
                    //double elapsed = ZoneFiveSoftware.Common.Data.Algorithm.DateTimeRangeSeries.TimeNotPaused(aDateTime[0], dateTime, pauses).TotalSeconds;
                    updateTracks(info, index, dateTime, lap.EndElapsed.TotalSeconds - timeOffset, lap.EndDistanceMeters - distOffset);

                    //This section is no longer needed (was when elapsed was lap.EndElapsed.TotalSeconds?)
                    //if (aTime[index] < aTime[index - 1])
                    //{
                    //    aTime[index] = aTime[index - 1];
                    //    increment += 5;
                    //    goto restart;
                    //}
                }
                this.Length = index+1;
            }
        }

        public void updateTracks(ActivityInfo info, int index, DateTime dateTime, double time, float distance)
        {
            aDateTime[index] = dateTime;

            aTime[index] = time;
            aDistance[index] = distance;
            ITimeValueEntry<float> value = info.SmoothedElevationTrack.GetInterpolatedValue(dateTime);
            if (value != null)
            {
                aElevation[index] = value.Value;
            }
            else
            {
                aElevation[index] = 0;
                validElevation = false;
            }

            if (aPulse != null)
            {
                value = info.SmoothedHeartRateTrack.GetInterpolatedValue(dateTime);
                if (value != null)
                    aPulse[index] = value.Value;
                else
                    aPulse[index] = 0;
            }
            if (aSpeed != null)
            {
                value = info.SmoothedSpeedTrack.GetInterpolatedValue(dateTime);
                if (value != null)
                    aSpeed[index] = value.Value;
                else
                    aSpeed[index] = 0;
            }
        }

        public double[] getGoalTrack(GoalParameter goal)
        {
            switch (goal)
            {
                case GoalParameter.Distance:
                    return this.aDistance;
                case GoalParameter.Time:
                    return this.aTime;
                case GoalParameter.Elevation:
                    return this.aElevation;
            }
            return null;
        }

        public IList<double[]> getGoalZoneTrack(GoalParameter goal)
        {
            IList<double[]> result = new List<double[]>();
            switch (goal)
            {
                case GoalParameter.PulseZone:
                    result.Add(this.aPulse);
                    break;
                case GoalParameter.SpeedZone:
                    result.Add(this.aSpeed);
                    break;
                case GoalParameter.PulseZoneSpeedZone:
                    result.Add(this.aPulse);
                    result.Add(this.aSpeed);
                    break;
            }
            return result;
        }

        public bool ZoneOk(Goal goal) { return (goal.Image == GoalParameter.SpeedZone) || (aPulse != null && aPulse.Length > 0); }

    }
}