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
using ZoneFiveSoftware.Common.Visuals.Fitness;
using ZoneFiveSoftware.Common.Data.GPS;
using ZoneFiveSoftware.Common.Data;
using System.Data;
using System.Windows.Forms;
using GpsRunningPlugin.Properties;
using ZoneFiveSoftware.Common.Data.Measurement;
using ZoneFiveSoftware.Common.Visuals;
using GpsRunningPlugin.Util;

namespace GpsRunningPlugin.Source
{
    class HighScore
    {
        private HighScore() { }

        public static IList<IList<Object>> getFastestTimesOfDistances(IList<IActivity> activities, IList<double> distances, System.Windows.Forms.ProgressBar progress)
        {
            IList<Goal> goals = new List<Goal>();
            foreach (double distance in distances)
            { 
                goals.Add(new PointGoal(distance, false,
                            GoalParameter.Time, GoalParameter.Distance));
            }

            Result[] results = calculate(activities, goals, progress);
            IList<IList<Object>> objects = new List<IList<Object>>();
            foreach (Result result in results)
            {
                if (result != null)
                {
                    IList<Object> s = new List<Object>();
                    objects.Add(s);
                    s.Add(result.Activity);
                    s.Add(result.Seconds);
                    s.Add(result.MeterStart);
                    s.Add(result.MeterEnd);
                    s.Add(result.TimeStart);
                }
            }
            return objects;
        }

        public static Result[] calculate(IList<IActivity> activities, IList<Goal> goals, System.Windows.Forms.ProgressBar progress)
        {
            Result[] results = new Result[goals.Count];
            progress.Minimum = 0;
            progress.Maximum = activities.Count;
            progress.Value = 0;
            if (activities != null && activities.Count > 0)
            {
                foreach (IActivity activity in activities)
                {
                    if (null != activity && activity.HasStartTime)
                    {
                        if (Settings.IgnoreManualData)
                        {
                            if (!activity.UseEnteredData)
                                calculate(activity, goals, results);
                        }
                        else calculate(activity, goals, results);
                    }
                    progress.Value++;
                }
            }
            return results;
        }

        private static void calculate(IActivity activity, IList<Goal> goals, IList<Result> results)
        {
            ActivityInfo info = ActivityInfoCache.Instance.GetInfo(activity);
            double[] distance, time, elevation, pulse, speed;
            int increment = 5;
            restart:
            {
                IList<LapDetailInfo> laps = info.DistanceLapDetailInfo(20 + increment);
                int length = laps.Count + 1;//info.MovingDistanceMetersTrack.Count;
                distance = new double[length];
                distance[0] = 0;
                time = new double[length];
                time[0] = 0;
                elevation = new double[length];
                pulse = new double[length];
                speed = new double[length];
                DateTime dateTime = activity.StartTime;
                ITimeValueEntry<float> value = info.SmoothedElevationTrack.GetInterpolatedValue(dateTime);
                if (value != null)
                    elevation[0] = value.Value;
                else
                    elevation[0] = 0;
                value = info.SmoothedHeartRateTrack.GetInterpolatedValue(dateTime);
                if (info.SmoothedHeartRateTrack.Max > 0 &&
                     value != null)
                    pulse[0] = value.Value;
                else
                    pulse[0] = 0;
                value = info.SmoothedSpeedTrack.GetInterpolatedValue(dateTime);
                if (value != null)
                    speed[0] = value.Value;
                else
                    speed[0] = 0;
                //double d = 0;
                //TimeSpan t;
                //foreach (LapDetailInfo lap in laps)
                //{
                //    d += lap.LapDistanceMeters;
                //    t = lap.StartElapsed;
                //}
                int index = 1;
                foreach (LapDetailInfo lap in laps)
                {
                    distance[index] = lap.EndDistanceMeters;
                    time[index] = lap.EndElapsed.TotalSeconds;
                    if (time[index] < time[index - 1])
                    {
                        time[index] = time[index - 1];
                        increment += 5;
                        goto restart;
                    }
                    dateTime = lap.EndTime;
                    value = info.SmoothedElevationTrack.GetInterpolatedValue(dateTime);
                    if (value != null)
                        elevation[index] = value.Value;
                    else
                        elevation[index] = 0;
                    value = info.SmoothedHeartRateTrack.GetInterpolatedValue(dateTime);
                    if (info.SmoothedHeartRateTrack.Max > 0 &&
                         value != null)
                        pulse[index] = value.Value;
                    else
                        pulse[index] = 0;
                    value = info.SmoothedSpeedTrack.GetInterpolatedValue(dateTime);
                    if (value != null)
                        speed[index] = value.Value;
                    else
                        speed[index] = 0;
                    index++;
                }
            }
            //foreach (TimeValueEntry<float> pair in info.MovingDistanceMetersTrack)
            //{
            //    distance[index] = pair.Value;
            //    time[index] = pair.ElapsedSeconds;
            //    DateTime dateTime = info.MovingDistanceMetersTrack.StartTime.AddSeconds(time[index]);//activity.StartTime.AddSeconds(pair.ElapsedSeconds);
            //    ITimeValueEntry<float> value = info.SmoothedElevationTrack.GetInterpolatedValue(dateTime);
            //    if (value != null)
            //        elevation[index] = value.Value;
            //    else
            //        elevation[index] = 0;
            //    value = info.SmoothedHeartRateTrack.GetInterpolatedValue(dateTime);
            //    if (info.SmoothedHeartRateTrack.Max > 0 &&
            //         value != null)
            //        pulse[index] = value.Value;
            //    else
            //        pulse[index] = 0;
            //    value = info.SmoothedSpeedTrack.GetInterpolatedValue(dateTime);
            //    if (value != null)
            //        speed[index] = value.Value;
            //    else
            //        speed[index] = 0;
            //    index++;
            //}

            //pad(elevation);
            //pad(pulse);
            //pad(speed);

            //int test = 0;

            for (int i = 0; i < goals.Count; i++)
            {
                Result result = null;
                switch (goals[i].Image)
                {
                    case GoalParameter.PulseZone: 
                    case GoalParameter.SpeedZone:
                    case GoalParameter.PulseZoneSpeedZone:
                        if (info.SmoothedHeartRateTrack.Count > 0)
                            result = calculate(activity, (IntervalsGoal)goals[i],
                                                getRightType(goals[i].Domain, distance, time, elevation),
                                                getRightType(goals[i].Image, pulse, speed),
                                                time, distance, elevation, pulse);
                        break; 
                    default:
                        result = calculate(activity, (PointGoal)goals[i],
                                            getRightType(goals[i].Domain, distance, time, elevation),
                                            getRightType(goals[i].Image, distance, time, elevation),
                                            time, distance, elevation, pulse);
                        break;
                }
                if (result != null && (results[i] == null ||
                                        ((goals[i].UpperBound && result.DomainDiff > results[i].DomainDiff) ||
                                            (!goals[i].UpperBound && result.DomainDiff < results[i].DomainDiff))))

                    results[i] = result;
            }
        }

        private static void pad(double[] ds)
        {
            int index = 0;
            while (index < ds.Length && ds[index] == 0) index++;
            if (index >= ds.Length) return;
            double v = ds[index];
            while (index >= 0) ds[index--] = v;
            index = ds.Length - 1;
            while (index >= 0 && ds[index] == 0) index--;
            v = ds[index];
            while (index < ds.Length) ds[index++] = v;
        }

        private static double[] getRightType(GoalParameter goalParameter, 
            double[] distance, double[] time, double[] elevation)
        {
            switch (goalParameter)
            {
                case GoalParameter.Distance: return distance;
                case GoalParameter.Time: return time;
                case GoalParameter.Elevation: return elevation;
            }
            return null;
        }

        public static IList<double[]> getRightType(GoalParameter goalParamter, 
            double[] pulse, double[] speed)
        {
            IList<double[]> result = new List<double[]>();
            switch (goalParamter)
            {
                case GoalParameter.PulseZone:
                    result.Add(pulse); break;
                case GoalParameter.SpeedZone:
                    result.Add(speed); break;
                case GoalParameter.PulseZoneSpeedZone:
                    result.Add(pulse); result.Add(speed); break;
            }
            return result;
        }

        private static Result calculate(IActivity activity, IntervalsGoal goal, 
            double[] domain, IList<double[]> image,
            double[] time, double[] distance, double[] elevation, double[] pulse)
        {
            bool foundAny = false;
            int back = 0, front = 0;
            int bestBack = 0, bestFront = 0;
            double domainStart = 0, domainEnd = 0;
            double distanceStart = 0, distanceEnd = 0;
            double elevationStart = 0, elevationEnd = 0;
            double timeStart = 0, timeEnd = 0;
            
            double best;
            if (goal.UpperBound) best = double.MinValue;
            else best = double.MaxValue;

            int length = image[0].Length;

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
                    if ((goal.UpperBound && best < domainDiff) ||
                        (!goal.UpperBound && best > domainDiff))
                    {
                        foundAny = true;
                        best = domainDiff;
                        bestBack = back;
                        bestFront = front;
                        domainStart = domain[back];
                        domainEnd = domain[front];
                        timeStart = time[back];
                        timeEnd = time[front];
                        distanceStart = distance[back];
                        distanceEnd = distance[front];
                        elevationStart = elevation[back];
                        elevationEnd = elevation[front];
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
                return new Result(goal, activity, domainStart, domainEnd, (int)timeStart, (int)timeEnd,
                    distanceStart, distanceEnd, elevationStart, elevationEnd, 
                    averagePulse(pulse, time, bestBack, bestFront));
            }
            return null;
        }

        private static double averagePulse(double[] pulse, double[] time, int back, int front)
        {
            double result = 0;
            for (int i = back; i < front; i++)
                result += (pulse[i] + (pulse[i + 1] - pulse[i]) / 2) * (time[i + 1] - time[i]);
            result = result / (time[front] - time[back]);
            return result;
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

        private static Result calculate(IActivity activity, PointGoal goal, 
            double[] domain, double[] image, 
            double[] time, double[] distance, double[] elevation, double[] pulse)
        {
            bool foundAny = false;
            int back = 0, front = 0;
            int bestBack = 0, bestFront = 0;
            double domainStart = 0, domainEnd = 0;
            double distanceStart = 0, distanceEnd = 0;
            double elevationStart = 0, elevationEnd = 0;
            double timeStart = 0, timeEnd = 0;

            double best;
            if (goal.UpperBound) best = double.MinValue;
            else best = double.MaxValue;

            while (front < image.Length && back < image.Length)
            {
                if (image[front] - image[back] >= goal.Value)
                {
                    double domainDiff = domain[front] - domain[back];
                    if ((goal.UpperBound && best < domainDiff) ||
                        (!goal.UpperBound && best > domainDiff))
                    {
                        foundAny = true;
                        best = domainDiff;
                        bestBack = back;
                        bestFront = front;
                        domainStart = domain[back];
                        domainEnd = domain[front];
                        timeStart = time[back];
                        timeEnd = time[front];
                        distanceStart = distance[back];
                        distanceEnd = distance[front];
                        elevationStart = elevation[back];
                        elevationEnd = elevation[front];
                    }
                    back++;
                }
                else
                {
                    front++;
                }
            }
            if (foundAny)
                return new Result(goal, activity, domainStart, domainEnd,
                    (int) timeStart, (int) timeEnd, distanceStart, distanceEnd, elevationStart, elevationEnd,
                    averagePulse(pulse, time, bestBack, bestFront));
            return null;
        }

        public static void generateGoals(GoalParameter domain, GoalParameter image, bool upperBound,
            IList<Goal> goals)
        {
            switch (image)
            {
                case GoalParameter.Distance:
                    foreach (double distance in Settings.distances.Keys)
                    {
                        goals.Add(new PointGoal(distance, upperBound,
                                    domain, GoalParameter.Distance));
                    }
                    break;
                case GoalParameter.Time:
                    foreach (int time in Settings.times.Keys)
                    {
                        goals.Add(new PointGoal(time, upperBound,
                                    domain, GoalParameter.Time));
                    }
                    break;
                case GoalParameter.Elevation:
                    foreach (double elevation in Settings.elevations.Keys)
                    {
                        goals.Add(new PointGoal(elevation, upperBound,
                                    domain, GoalParameter.Elevation));
                    }
                    break;
                case GoalParameter.PulseZone:
                    foreach (double min in Settings.pulseZones.Keys)
                        foreach (double max in Settings.pulseZones[min].Keys)
                        {
                            IList<IList<double>> intervals = new List<IList<double>>();
                            IList<double> interval = new List<double>();
                            interval.Add(min);
                            interval.Add(max);
                            intervals.Add(interval);
                            goals.Add(new IntervalsGoal(intervals, upperBound,
                                        domain, GoalParameter.PulseZone));
                        }
                    break;
                case GoalParameter.SpeedZone:
                    foreach (double min in Settings.speedZones.Keys)
                        foreach (double max in Settings.speedZones[min].Keys)
                        {
                            IList<IList<double>> intervals = new List<IList<double>>();
                            IList<double> interval = new List<double>();
                            interval.Add(min);
                            interval.Add(max);
                            intervals.Add(interval);
                            goals.Add(new IntervalsGoal(intervals, upperBound,
                                        domain, GoalParameter.SpeedZone));
                        }
                    break;
                case GoalParameter.PulseZoneSpeedZone:
                    foreach (double minPulse in Settings.pulseZones.Keys)
                        foreach (double maxPulse in Settings.pulseZones[minPulse].Keys)
                            foreach (double minSpeed in Settings.speedZones.Keys)
                                foreach (double maxSpeed in Settings.speedZones[minSpeed].Keys)
                                {
                                    IList<IList<double>> intervals = new List<IList<double>>();
                                    IList<double> interval = new List<double>();
                                    interval.Add(minPulse);
                                    interval.Add(maxPulse);
                                    intervals.Add(interval);
                                    interval = new List<double>();
                                    interval.Add(minSpeed);
                                    interval.Add(maxSpeed);
                                    intervals.Add(interval);
                                    goals.Add(new IntervalsGoal(intervals, upperBound,
                                        domain, GoalParameter.PulseZoneSpeedZone));
                                }
                    break;
            }
        }

        public static IList<Goal> generateGoals()
        {
            IList<Goal> goals = new List<Goal>();
            generateGoals(Settings.Domain, Settings.Image, Settings.UpperBound, goals);
            return goals;
        }

        public static DataTable generateTable(IList<Result> results, String speedUnit, 
            bool includeLocationAndDate, GoalParameter domain, GoalParameter image, bool upperBound)
        {
            IApplication app = Plugin.GetApplication();
            DataTable table = new DataTable();
            table.Columns.Add(UnitUtil.Distance.LabelAxis);
            table.Columns.Add(CommonResources.Text.LabelTime);
            table.Columns.Add(UnitUtil.PaceOrSpeed.LabelAxis(speedUnit.Equals(CommonResources.Text.LabelPace)));
            table.Columns.Add(CommonResources.Text.LabelStartTime);
            table.Columns.Add(CommonResources.Text.LabelStart + UnitUtil.Distance.LabelAbbr2);
            table.Columns.Add(CommonResources.Text.LabelElevation + " (+/- " + UnitUtil.Elevation.LabelAbbr + ")");
            table.Columns.Add(CommonResources.Text.LabelAvgHR + UnitUtil.HeartRate.LabelAbbr2);
            if (includeLocationAndDate)
            {
                table.Columns.Add(CommonResources.Text.LabelDate);
                table.Columns.Add(CommonResources.Text.LabelLocation);
            }
            table.Columns.Add(HighScoreViewer.ActivityIdColumn);
            foreach (Result result in results)
            {
                if (result != null && result.Goal.Domain.Equals(domain) && 
                    result.Goal.Image.Equals(image)
                    && result.Goal.UpperBound == upperBound)
                {
                    DataRow row = table.NewRow();
                    row[0] = UnitUtil.Distance.ToString(result.Meters);
                    row[1] = UnitUtil.Time.ToString(result.Seconds);
                    if (result.Seconds > 0 && result.Meters > 0)
                    {
                        double speedMS = result.Meters / result.Seconds;
                        row[2] = UnitUtil.PaceOrSpeed.ToString(speedUnit.Equals(CommonResources.Text.LabelPace), speedMS);
                    }
                    else {
                        row[2] = "-";
                    }
                    row[3] = UnitUtil.Time.ToString(result.TimeStart);
                    row[4] = UnitUtil.Distance.ToString(result.MeterStart);
                    row[5] = UnitUtil.Elevation.ToString(result.Elevations);
                    if (result.AveragePulse.Equals(double.NaN))
                        row[6] = "-";
                    else
                        row[6] = Math.Round(result.AveragePulse);
                    if (includeLocationAndDate)
                    {
                        row[7] = result.Activity.StartTime.ToShortDateString();
                        row[8] = result.Activity.Location;
                    }
                    row[HighScoreViewer.ActivityIdColumn] = result.Activity.ReferenceId;
                    table.Rows.Add(row);
                }
            }
            return table;
        }
    }
}