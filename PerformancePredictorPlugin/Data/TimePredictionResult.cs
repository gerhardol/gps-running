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
using ZoneFiveSoftware.Common.Visuals.Chart;
using GpsRunningPlugin.Util;

namespace GpsRunningPlugin.Source
{
    public class TimePredictionResult
    {
        //Just keep the time result, could be removed (previously more data)

        //public double Distance;
        public double PredictedTime;
        //public double Speed
        //{
        //    get
        //    {
        //        return Distance / PredictedTime;
        //    }
        //}

        public TimePredictionResult(double PredictedTime)
        {
            //this.Distance = Distance;
            this.PredictedTime = PredictedTime;
        }
    }
}
