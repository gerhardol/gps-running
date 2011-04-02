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

using ZoneFiveSoftware.Common.Visuals;
using System.Collections.Generic;
using System.Drawing;
using ZoneFiveSoftware.Common.Data;
using ZoneFiveSoftware.Common.Data.Measurement;
using ZoneFiveSoftware.Common.Data.Fitness;
using GpsRunningPlugin.Util;
using GpsRunningPlugin.Properties;

namespace GpsRunningPlugin.Source
{
#if ST_2_1
        // Interface as in ST3
    public interface IListColumnDefinition
    {
        StringAlignment Align { get; }
        string GroupName { get; }
        string Id { get; }
        int Width { get; }
        string Text(string columnId);
    }
#endif
    public class ListColumnDefinition : IListColumnDefinition
    {
        public ListColumnDefinition(string id, string text, string groupName, int width, StringAlignment align)
        {
            this.align = align;
            this.groupName = groupName;
            this.id = id;
            this.width = width;
            this.text = text;
        }
        private StringAlignment align;
        public StringAlignment Align
        {
            get
            {
                return align;
            }
        }
        string groupName;
        public string GroupName
        {
            get
            {
                return groupName;
            }
        }
        string id;
        public string Id
        {
            get
            {
                return id;
            }
        }
        int width;
        public int Width
        {
            get
            {
                return width;
            }
        }
        string text;
        public string Text(string id)
        {
            return text;
        }
        public override string ToString()
        {
            return text;
        }
        public bool CanSelect
        {
            //Should some fields be unsortable?
            get { return true; }
        }
    }
    public static class ResultColumnIds
    {
        //Time Predict
        public const string Distance = "Distance";
        public const string DistanceNominal = "DistanceNominal";
        public const string PredictedTime = "PredictedTime";
        public const string Speed = "Speed";
        public const string StartDate = "StartDate";
        public const string StartTime = "StartTime";
        public const string UsedTime = "UsedTime";
        public const string StartDistance = "StartDistance";
        public const string UsedDistance = "UsedDistance";

        //Training
        public const string ZoneDistance = "ZoneDistance";
        public const string PercentOfMax = "PercentOfMax";
        public const string TrainRaceHR = "TrainRaceHR";

        public const string OneMile = "OneMile";
        public const string FiveKm = "FiveKm";
        public const string TenKm = "TenKm";

        public const string Duration = "Duration";
        
        public const string Temperature = "Temperature";
        public const string EstimatedTime = "EstimatedTime";
        public const string EstimatedSpeed = "EstimatedSpeed";

        public const string Weight = "Weight";
        public const string AdjustedVDOT = "AdjustedVDOT";

        public static IList<string> TimePredictHsColumns = new List<string> { Distance, DistanceNominal, PredictedTime, Speed, StartDate, StartTime, UsedTime, StartDistance, UsedDistance};
        public static IList<string> TimePredictSingleColumns = new List<string>{ Distance, DistanceNominal, PredictedTime, Speed};

        public static IList<string> TrainingColumns = new List<string> { ZoneDistance, PercentOfMax, TrainRaceHR, Speed};
        public static IList<string> IntervallColumns = new List<string> { Distance, OneMile, FiveKm, TenKm};
        public static IList<string> PaceTempoColumns = new List<string> { Duration, Speed };
        public static IList<string> TemperatureColumns = new List<string> { Temperature, EstimatedTime, EstimatedSpeed };
        public static IList<string> WeightColumns = new List<string> { Weight, AdjustedVDOT, EstimatedTime, EstimatedSpeed };

        public static ICollection<IListColumnDefinition> ColumnDefs()
        {
            IList<IListColumnDefinition> columnDefs = new List<IListColumnDefinition>();
            columnDefs.Add(new ListColumnDefinition(ResultColumnIds.Distance, UnitUtil.Distance.LabelAxis, "", 90, StringAlignment.Near));
            columnDefs.Add(new ListColumnDefinition(ResultColumnIds.DistanceNominal, CommonResources.Text.LabelDistance, "", 70, StringAlignment.Near));
            columnDefs.Add(new ListColumnDefinition(ResultColumnIds.PredictedTime, Resources.PredictedTime, "", 70, StringAlignment.Near));
            columnDefs.Add(new ListColumnDefinition(ResultColumnIds.Speed, UnitUtil.PaceOrSpeed.LabelAxis(Settings.ShowPace), "", 60, StringAlignment.Near));
            columnDefs.Add(new ListColumnDefinition(ResultColumnIds.StartDate, Resources.UsedActivityStartDate, "", 70, StringAlignment.Near));
            columnDefs.Add(new ListColumnDefinition(ResultColumnIds.StartTime, Resources.UsedActivityStartTime, "", 50, StringAlignment.Near));
            columnDefs.Add(new ListColumnDefinition(ResultColumnIds.UsedTime, Resources.UsedTimeOfActivity, "", 80, StringAlignment.Near));
            columnDefs.Add(new ListColumnDefinition(ResultColumnIds.StartDistance, Resources.StartOfPart + UnitUtil.Distance.LabelAbbr2, "", 80, StringAlignment.Near));
            columnDefs.Add(new ListColumnDefinition(ResultColumnIds.UsedDistance, Resources.UsedLengthOfActivity + UnitUtil.Distance.LabelAbbr2, "", 80, StringAlignment.Near));

            columnDefs.Add(new ListColumnDefinition(ResultColumnIds.ZoneDistance, Resources.ZoneDistance, "", 120, StringAlignment.Near));
            columnDefs.Add(new ListColumnDefinition(ResultColumnIds.PercentOfMax, CommonResources.Text.LabelPercentOfMax, "", 80, StringAlignment.Near));
            columnDefs.Add(new ListColumnDefinition(ResultColumnIds.TrainRaceHR, Resources.TrainRaceHR, "", 80, StringAlignment.Near));
            columnDefs.Add(new ListColumnDefinition(ResultColumnIds.OneMile, Length.ToString(1, Length.Units.Mile, "F0u"), "", 80, StringAlignment.Near));
            columnDefs.Add(new ListColumnDefinition(ResultColumnIds.FiveKm, Length.ToString(5, Length.Units.Kilometer, "F0u"), "", 80, StringAlignment.Near));
            columnDefs.Add(new ListColumnDefinition(ResultColumnIds.TenKm, Length.ToString(10, Length.Units.Kilometer, "F0u"), "", 80, StringAlignment.Near));
            columnDefs.Add(new ListColumnDefinition(ResultColumnIds.Duration, CommonResources.Text.LabelDuration + " (" + StringResources.MinutesShort + ")", "", 80, StringAlignment.Near));
            columnDefs.Add(new ListColumnDefinition(ResultColumnIds.Temperature, CommonResources.Text.LabelTemperature + UnitUtil.Temperature.LabelAbbr2, "", 80, StringAlignment.Near));
            columnDefs.Add(new ListColumnDefinition(ResultColumnIds.EstimatedTime, Resources.EstimatedTime, "",100, StringAlignment.Near));
            string estSpeed;
                if (Settings.ShowPace)
            {
                estSpeed = Resources.EstimatedPace + UnitUtil.Pace.LabelAbbr2;
            }
            else
            {
                estSpeed = Resources.EstimatedSpeed + UnitUtil.Speed.LabelAbbr2;
            }            
            columnDefs.Add(new ListColumnDefinition(ResultColumnIds.EstimatedSpeed, estSpeed, "", 1200, StringAlignment.Near));
            columnDefs.Add(new ListColumnDefinition(ResultColumnIds.Weight, Resources.ProjectedWeight + UnitUtil.Weight.LabelAbbr2, "", 80, StringAlignment.Near));
            columnDefs.Add(new ListColumnDefinition(ResultColumnIds.AdjustedVDOT, Resources.AdjustedVDOT, "", 80, StringAlignment.Near));

            return columnDefs;
        }
    }
}
