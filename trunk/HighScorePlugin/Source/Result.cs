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
            return String.Format("{0} : {1} {3}, {2} {4}", Goal.ToString(), MeterEnd - MeterEnd, Seconds,Resources.Meters,Resources.Seconds);
        }
    }
}