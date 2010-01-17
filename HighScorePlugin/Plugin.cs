using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

using ZoneFiveSoftware.Common.Visuals.Fitness;

namespace SportTracksHighScorePlugin
{
    class Plugin : IPlugin
    {

        #region IPlugin Members

        public IApplication Application
        {
            set { application = value; }
        }

        public Guid Id
        {
            get { return new Guid(Properties.Resources.HighScoreGuid); }
        }

        public string Name
        {
            get { return "High Score Plugin"; }
        }

        public void ReadOptions(XmlDocument xmlDoc, XmlNamespaceManager nsmgr, XmlElement pluginNode)
        {
        }

        public string Version
        {
            get { return GetType().Assembly.GetName().Version.ToString(3); }
        }

        public void WriteOptions(XmlDocument xmlDoc, XmlElement pluginNode)
        {
        }

        #endregion

        public static IApplication GetApplication()
        {
            return application;
        }

        #region Private members
        private static IApplication application;
        #endregion
    }
}
