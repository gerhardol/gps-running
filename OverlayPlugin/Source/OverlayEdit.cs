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
using ZoneFiveSoftware.Common.Visuals;
using ZoneFiveSoftware.Common.Visuals.Fitness;
using ZoneFiveSoftware.Common.Data.Fitness;

namespace SportTracksOverlayPlugin.Source
{
    class OverlayEdit : IExtendActivityEditActions
    {

        #region IExtendActivityEditActions Members

        public IList<IAction> GetActions(IList<IActivity> activities)
        {
            if (activities.Count > 0)
                return new IAction[] { new OverlayAction(activities) };
            return new IAction[] { };
        }

        public IList<IAction> GetActions(IActivity activity)
        {
            return new IAction[] { new OverlayAction(new IActivity[] { activity }) };
        }

        #endregion
    }
}
