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
    public static class SummaryColumnIds {
        //public const string Order = "Order";
        public const string Color = "Color";
        public const string StartDate = "StartDate";
		public const string StartTime = "StartTime";
		public const string Time = "Time";
		public const string Distance = "Distance";
        public const string AvgSpeedPace = "AvgSpeedPace";
        public const string AvgSpeed = "AvgSpeed";
        public const string AvgPace = "AvgPace";
        public const string AvgHR = "AvgHR";
        public const string CommonStretches = "CommonStretches";

        public static ICollection<IListColumnDefinition> PermanentMultiColumnDefs()
        {
            IList<IListColumnDefinition> columnDefs = new List<IListColumnDefinition>();
            columnDefs.Add(new ListColumnDefinition(SummaryColumnIds.Color, "", "", 10, StringAlignment.Near));

            return columnDefs;
        }
        public static ICollection<IListColumnDefinition> ColumnDefs(IActivity activity)
        {
            IList<IListColumnDefinition> columnDefs = new List<IListColumnDefinition>();
            columnDefs.Add(new ListColumnDefinition(SummaryColumnIds.StartDate, CommonResources.Text.LabelDate, "", 100, StringAlignment.Near));
            columnDefs.Add(new ListColumnDefinition(SummaryColumnIds.StartTime, CommonResources.Text.LabelStartTime, "", 70, StringAlignment.Near));
            columnDefs.Add(new ListColumnDefinition(SummaryColumnIds.Time, UnitUtil.Time.LabelAxis, "", 70, StringAlignment.Near));
            columnDefs.Add(new ListColumnDefinition(SummaryColumnIds.Distance, UnitUtil.Distance.LabelAxis, "", 60, StringAlignment.Near));
            string sSpeedPace = StringResources.ActivitySpeedOrPace;
            if (null != activity)
            {
                sSpeedPace = UnitUtil.PaceOrSpeed.LabelAxis(Settings.ShowPace);
            }
            columnDefs.Add(new ListColumnDefinition(SummaryColumnIds.AvgSpeedPace, sSpeedPace, "", 80, StringAlignment.Near));
            columnDefs.Add(new ListColumnDefinition(SummaryColumnIds.AvgSpeed, UnitUtil.Speed.LabelAxis, "", 80, StringAlignment.Near));
            columnDefs.Add(new ListColumnDefinition(SummaryColumnIds.AvgPace, UnitUtil.Pace.LabelAxis, "", 80, StringAlignment.Near));
            columnDefs.Add(new ListColumnDefinition(SummaryColumnIds.AvgHR, CommonResources.Text.LabelAvgHR + UnitUtil.HeartRate.LabelAbbr2, "", 80, StringAlignment.Near));

            columnDefs.Add(new ListColumnDefinition(SummaryColumnIds.CommonStretches, Resources.CommonStretches, "", 170, StringAlignment.Near));

            return columnDefs;
		}
	}
}
