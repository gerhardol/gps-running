using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using SportTracksAccumulatedSummaryPlugin.Properties;

namespace SportTracksAccumulatedSummaryPlugin.Source
{
    public partial class AccumulatedSummarySettings : UserControl
    {
        public AccumulatedSummarySettings()
        {
            InitializeComponent();
            linkLabel1.Text = Resources.Webpage;
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(new ProcessStartInfo("IExplore",
                "http://gpsrunning.nicolajsen.nl/?path=SportTracks/Accumulated+Summary"));
        }
    }
}
