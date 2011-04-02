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
        public TrainingResult(IActivity activity, int index, double vdot, double seconds, double distance, double maxHr)
        {
            this.activity = activity;
            if (m_percentages == null)
            {
                m_zones = getZones();
                m_percentages = getPercentages(vdot);
                m_hrs = getHeartRates(maxHr, m_percentages);
                m_paces = getSpeeds(vdot, seconds, distance, m_percentages);
            }
            this.ZoneDistance = m_zones[index];
            this.PercentOfMax = m_percentages[index];
            this.TrainRaceHR = m_hrs[index];
            this.Speed = m_paces[index];
        }

            IList<String> m_zones;
            IList<double> m_percentages = null;
            IList<double> m_hrs;
            IList<double> m_paces;

        private IList<string> getZones()
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

        private IList<double> getSpeeds(double vdot, double seconds, double distance, IList<double> percentages)
        {
            double[] result = new double[15];
            result[0] = TrainingView.getTrainingSpeed(vdot, percentages[0]);
            result[1] = TrainingView.getTrainingSpeed(vdot, percentages[1]);
            result[2] = TrainingView.getTrainingSpeed(vdot, percentages[2]);
            result[3] = TrainingView.getTrainingSpeed(vdot, percentages[3]);
            result[6] = TrainingView.getTrainingSpeed(42195, distance, seconds);
            result[4] = result[3] / (1 + (result[3] / result[6] - 1) / 6.0);
            result[5] = result[3] / (1 + (result[3] / result[6] - 1) / 3.0);
            result[7] = TrainingView.getTrainingSpeed(21097.5, distance, seconds);
            result[8] = TrainingView.getTrainingSpeed(15000, distance, seconds);
            result[9] = TrainingView.getTrainingSpeed(12000, distance, seconds);
            result[10] = TrainingView.getTrainingSpeed(10000, distance, seconds);
            result[11] = TrainingView.getTrainingSpeed(8000, distance, seconds);
            result[12] = TrainingView.getTrainingSpeed(5000, distance, seconds);
            result[13] = TrainingView.getTrainingSpeed(3000, distance, seconds);
            result[14] = TrainingView.getTrainingSpeed(1609.344, distance, seconds); ;
            return result;
        }

        private double[] getPercentages(double vdot)
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

        private IList<double> getHeartRates(double maxHr, IList<double> percentages)
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
