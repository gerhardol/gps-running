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
using System.Xml;

using ZoneFiveSoftware.Common.Visuals.Fitness;
using GpsRunningPlugin.Source;

namespace GpsRunningPlugin
{
    class Plugin : IPlugin
    {

        #region IPlugin Members

        public IApplication Application
        {
            set { application = value; }
        }

        public static IApplication GetApplication()
        {
            return application;
        }

        public Guid Id
        {
            get { return new Guid("{F4C8F7D6-B4A5-4E69-9205-48044356D3CF}"); }
        }

        public string Name
        {
            get { return "TRIMP Plugin"; }
        }

        public string Version
        {
            get { return GetType().Assembly.GetName().Version.ToString(3); }
        }

        public void ReadOptions(XmlDocument xmlDoc, XmlNamespaceManager nsmgr, XmlElement pluginNode)
        {
            String attr;
            attr = pluginNode.GetAttribute(xmlTags.Verbose);
            if (attr.Length > 0) { Verbose = XmlConvert.ToInt16(attr); }

            Settings.ReadOptions(xmlDoc, nsmgr, pluginNode);
        }

        public void WriteOptions(XmlDocument xmlDoc, XmlElement pluginNode)
        {
            pluginNode.SetAttribute(xmlTags.Verbose, XmlConvert.ToString(Verbose));
            Settings.WriteOptions(xmlDoc, pluginNode);
        }

        #endregion

        #region Private members
        private class xmlTags
        {
            public const string Verbose = "Verbose";
        }
        private static IApplication application;

        #endregion

        public static int Verbose = 0;  //Only changed in xml file
    }
}
