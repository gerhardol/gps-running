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
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

using ZoneFiveSoftware.Common.Data.Fitness;
using GpsRunningPlugin;
using GpsRunningPlugin.Source;
using GpsRunningPlugin.Properties;
using TrailsPlugin.Integration;

namespace GpsRunningPlugin.Source
{
    public partial class OverlaySettings : UserControl
    {
        public OverlaySettings()
        {
            InitializeComponent();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(new ProcessStartInfo(
                "http://code.google.com/p/gps-running/wiki/Overlay"));
        }
        public bool HidePage()
        {
            return true;
        }
        public void ShowPage(string bookmark)
        {
        }
        public void ThemeChanged(ZoneFiveSoftware.Common.Visuals.ITheme visualTheme)
        {
        }
        public void UICultureChanged(System.Globalization.CultureInfo culture)
        {
            linkLabel1.Text = Resources.Webpage;
            this.lblUniqueRoutes.Text = UniqueRoutes.CompabilityText;
        }
    }
}
