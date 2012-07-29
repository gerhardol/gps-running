/*
Copyright (C) 2010 Staffan Nilsson

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
using System.Drawing;
using ZoneFiveSoftware.Common.Data.Fitness;
using ZoneFiveSoftware.Common.Data.Measurement;
using GpsRunningPlugin.Properties;
using GpsRunningPlugin.Util;

namespace GpsRunningPlugin.Source
{
    public class TrainingResult
    {
        private IActivity activity;

        public IActivity Activity
        {
            get
            {
                return activity;
            }
        }

        public string ZoneDistance;
        public double PercentOfMax;
        public double TrainRaceHR;
        public double Speed;

        public TrainingResult(IActivity activity, string ZoneDistance, double PercentOfMax, double TrainRaceHR, double Speed)
        {
            this.activity = activity;
            this.ZoneDistance = ZoneDistance;
            this.PercentOfMax = PercentOfMax;
            this.TrainRaceHR = TrainRaceHR;
            this.Speed = Speed;
        }
        public TrainingResult(IActivity activity, int index)
        {
            this.activity = activity;
            if (m_percentages == null)
            {
                throw new Exception("No results calculated");
            }
            this.ZoneDistance = m_zones[index];
            this.PercentOfMax = m_percentages[index];
            this.TrainRaceHR = m_hrs[index];
            this.Speed = m_paces[index];
        }

        public static void Calculate(double vdot, TimeSpan time, double distance, double maxHr)
        {
            m_zones = getZones();
            m_percentages = getPercentages(vdot);
            m_hrs = getHeartRates(maxHr, m_percentages);
            m_paces = getSpeeds(vdot, time, distance, m_percentages);
        }

        private static IList<String> m_zones;
        private static IList<double> m_percentages = null;
        private static IList<double> m_hrs;
        private static IList<double> m_paces;

        private static IList<string> getZones()
        {
            string[] result = new string[15];
            result[0] = Resources.Recovery;
            result[1] = Resources.EasyAerobicZone;
            result[2] = Resources.EasyAerobicZone;
            result[3] = Resources.EasyAerobicZone;
            result[4] = Resources.ModAerobicZone;
            result[5] = Resources.HighAerobicZone;
            result[6] = StringResources.Marathon;
            result[7] = "1/2 " + StringResources.Marathon;
            result[8] = "15 " + Length.LabelAbbr(Length.Units.Kilometer);
            result[9] = "12 " + Length.LabelAbbr(Length.Units.Kilometer);
            result[10] = "10 " + Length.LabelAbbr(Length.Units.Kilometer);
            result[11] = "8 " + Length.LabelAbbr(Length.Units.Kilometer);
            result[12] = "5 " + Length.LabelAbbr(Length.Units.Kilometer);
            result[13] = "3 " + Length.LabelAbbr(Length.Units.Kilometer);
            result[14] = "1 " + Length.LabelAbbr(Length.Units.Mile);
            return result;
        }

        private static IList<double> getSpeeds(double vdot, TimeSpan time, double distance, IList<double> percentages)
        {
            double[] result = new double[15];
            result[0] = Predict.getTrainingSpeed(vdot, percentages[0]);
            result[1] = Predict.getTrainingSpeed(vdot, percentages[1]);
            result[2] = Predict.getTrainingSpeed(vdot, percentages[2]);
            result[3] = Predict.getTrainingSpeed(vdot, percentages[3]);
            result[6] = Predict.getTrainingSpeed(42195, distance, time);
            result[4] = result[3] / (1 + (result[3] / result[6] - 1) / 6.0);
            result[5] = result[3] / (1 + (result[3] / result[6] - 1) / 3.0);
            result[7] = Predict.getTrainingSpeed(21097.5, distance, time);
            result[8] = Predict.getTrainingSpeed(15000, distance, time);
            result[9] = Predict.getTrainingSpeed(12000, distance, time);
            result[10] = Predict.getTrainingSpeed(10000, distance, time);
            result[11] = Predict.getTrainingSpeed(8000, distance, time);
            result[12] = Predict.getTrainingSpeed(5000, distance, time);
            result[13] = Predict.getTrainingSpeed(3000, distance, time);
            result[14] = Predict.getTrainingSpeed(1609.344, distance, time); ;
            return result;
        }

        private static double[] getPercentages(double vdot)
        {
            double[] result = new double[15];
            result[0] = 0.65;
            result[1] = 0.70;
            result[2] = 0.72;
            result[3] = 0.75;
            result[6] = 0.8 + 0.09 * (vdot - 30) / 55;
            result[4] = result[3] + (result[6] - result[3]) / 6.0;
            result[5] = result[3] + (result[6] - result[3]) / 3.0;
            result[7] = 0.84 + 0.08 * (vdot - 30) / 55;
            result[8] = 0.86 + 0.08 * (vdot - 30) / 55;
            result[9] = 0.87 + 0.08 * (vdot - 30) / 55;
            result[10] = 0.88 + 0.08 * (vdot - 30) / 55;
            result[11] = 0.9 + 0.08 * (vdot - 30) / 55;
            result[12] = 0.94 + 0.05 * (vdot - 30) / 55;
            result[13] = 0.98 + 0.02 * (vdot - 30) / 55;
            result[14] = 1;
            return result;
        }

        private static IList<double> getHeartRates(double maxHr, IList<double> percentages)
        {
            IList<double> result = new List<double>();
            foreach (double p in percentages)
            {
                result.Add(p * maxHr);
            }
            return result;
        }

    }
}
