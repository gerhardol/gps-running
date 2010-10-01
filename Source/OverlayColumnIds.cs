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
    public static class OverlayColumnIds
    {
        public const string Visible = "Visible";
        public const string Colour = "Colour";
        public const string StartTime = "StartTime";
        public const string Offset = "Offset";
        public const string Time = "Time";
        public const string Distance = "DistanceMeters";
        public const string AvgSpeed = "AverageSpeedMetersPerSecond";
        public const string AvgPace = "AvgPace";
        public const string AvgHR = "AverageHeartRate";

        public static ICollection<IListColumnDefinition> ColumnDefs()
        {
            IList<IListColumnDefinition> columnDefs = new List<IListColumnDefinition>();
            columnDefs.Add(new ListColumnDefinition(OverlayColumnIds.Visible, StringResources.Visible, "", 50, StringAlignment.Near));
            columnDefs.Add(new ListColumnDefinition(OverlayColumnIds.Colour, StringResources.Colour, "", 50, StringAlignment.Near));
            columnDefs.Add(new ListColumnDefinition(OverlayColumnIds.StartTime, CommonResources.Text.LabelStartTime, "", 150, StringAlignment.Near));
            columnDefs.Add(new ListColumnDefinition(OverlayColumnIds.Offset, StringResources.Offset, "", 70, StringAlignment.Near));
            columnDefs.Add(new ListColumnDefinition(OverlayColumnIds.Time, CommonResources.Text.LabelTime, "", 70, StringAlignment.Near));
            columnDefs.Add(new ListColumnDefinition(OverlayColumnIds.Distance, CommonResources.Text.LabelDistance, "", 60, StringAlignment.Near));
            columnDefs.Add(new ListColumnDefinition(OverlayColumnIds.AvgSpeed, CommonResources.Text.LabelAvgSpeed, "", 80, StringAlignment.Near));
            columnDefs.Add(new ListColumnDefinition(OverlayColumnIds.AvgPace, CommonResources.Text.LabelAvgPace, "", 80, StringAlignment.Near));
            columnDefs.Add(new ListColumnDefinition(OverlayColumnIds.AvgHR, CommonResources.Text.LabelAvgHR + UnitUtil.HeartRate.LabelAbbr2, "", 80, StringAlignment.Near));

            return columnDefs;
        }
    }
}
