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
using System.Collections;
using System.Collections.Generic;
using System.Text;

using ZoneFiveSoftware.Common.Data;
using ZoneFiveSoftware.Common.Data.Fitness;
using ZoneFiveSoftware.Common.Data.GPS;
using ZoneFiveSoftware.Common.Visuals;
using ZoneFiveSoftware.Common.Visuals.Fitness;
using ZoneFiveSoftware.Common.Data.Measurement;
using SportTracksHighScorePlugin.Source;
using SportTracksHighScorePlugin.Properties;

namespace SportTracksHighScorePlugin.Source
{
    public class Result
    {
        public Result(Goal goal, IActivity activity,
            double domainStart, double domainEnd, 
            int timeStart, int timeEnd, double meterStart, double meterEnd, double elevationStart,
            double elevationEnd, double averagePulse)
        {
            this.Goal = goal;
            this.DomainDiff = domainEnd - domainStart;
            this.Activity = activity;
            this.DomainStart = domainStart;
            this.DomainEnd = domainEnd;
            this.TimeStart = timeStart;
            this.TimeEnd = timeEnd;
            this.Seconds = timeEnd - timeStart;
            this.MeterStart = meterStart;
            this.MeterEnd = meterEnd;
            this.Meters = MeterEnd - MeterStart;
            this.ElevationStart = elevationStart;
            this.ElevationEnd = elevationEnd;
            this.Elevations = elevationEnd - elevationStart;
            this.AveragePulse = averagePulse;
        }

        public Goal Goal;

        public IActivity Activity;

        public double DomainStart, DomainEnd, DomainDiff,  
            MeterStart, MeterEnd, Meters, ElevationStart, ElevationEnd, Elevations,
            AveragePulse;

        public int TimeStart, TimeEnd, Seconds;

        public override String ToString()
        {
            return String.Format("{0} : {1} {3}, {2} {4}", Goal.ToString(), MeterEnd - MeterEnd, Seconds,Length.LabelPlural(Length.Units.Meter),Time.LabelPlural(Time.TimeRange.Second));
        }
    }
}