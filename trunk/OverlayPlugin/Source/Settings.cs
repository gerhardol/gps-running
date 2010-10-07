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
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.Reflection;
using ZoneFiveSoftware.Common.Data.Measurement;
using GpsRunningPlugin.Properties;
#if !ST_2_1
using ZoneFiveSoftware.Common.Visuals.Forms;
#endif
namespace GpsRunningPlugin.Source
{
    class Settings
    {
        static Settings()
        {
            uniqueRoutes = getPlugin("UniqueRoutes", "GpsRunningPlugin.Source.UniqueRoutes");
            defaults();
        }

        public readonly static Type uniqueRoutes;

        private static bool autoZoom;
        public static bool AutoZoom
        {
            get { return autoZoom; }
            set
            {
                autoZoom = value;
            }
        }

        private static bool showHeartRate;
        public static bool ShowHeartRate
        {
            get { return showHeartRate; }
            set
            {
                showHeartRate = value;
            }
        }

        private static bool showPace;
        public static bool ShowPace
        {
            get { return showPace; }
            set
            {
                showPace = value;
            }
        }

        private static bool showSpeed;
        public static bool ShowSpeed
        {
            get { return showSpeed; }
            set
            {
                showSpeed = value;
            }
        }

        private static bool showPower;
        public static bool ShowPower
        {
            get { return showPower; }
            set
            {
                showPower = value;
            }
        }

        private static bool showCadence;
        public static bool ShowCadence
        {
            get { return showCadence; }
            set
            {
                showCadence = value;
            }
        }

        private static bool showElevation;
        public static bool ShowElevation
        {
            get { return showElevation; }
            set
            {
                showElevation = value;
            }
        }

        private static bool showTime;
        public static bool ShowTime
        {
            get { return showTime; }
            set
            {
                showTime = value;
            }
        }

        private static bool showDistance;
        public static bool ShowDistance
        {
            get { return showDistance; }
            set
            {
                showDistance = value;
            }
        }

        private static bool showCategoryAverage;
        public static bool ShowCategoryAverage
        {
            get { return showCategoryAverage; }
            set
            {
                showCategoryAverage = value;
            }
        }

        private static bool showMovingAverage;
        public static bool ShowMovingAverage
        {
            get { return showMovingAverage; }
            set
            {
                showMovingAverage = value;
            }
        }

        private static double movingAverageLength;
        public static double MovingAverageLength
        {
            get { return movingAverageLength; }
            set
            {
                movingAverageLength = value;
            }
        }

        private static double movingAverageTime; // seconds
        public static double MovingAverageTime
        {
            get { return movingAverageTime; }
            set
            {
                movingAverageTime = value;
            }
        }

        private static bool useTimeXAxis;
        public static bool UseTimeXAxis
        {
            get { return useTimeXAxis; }
            set
            {
                useTimeXAxis = value;
            }
        }
        
        private static Size windowSize; //viewWidth, viewHeight in xml
        public static Size WindowSize
        {
            get { return windowSize; }
            set
            {
                windowSize = value;
            }
        }

#if ST_2_1
		private static Size savedImageSize;	//savedImageWidth, savedImageHeight in xml
        public static Size SavedImageSize
#else
        private static SaveImageDialog.ImageSizeType savedImageSize;
        public static SaveImageDialog.ImageSizeType SavedImageSize
#endif
		{
			get { return savedImageSize; }
			set
			{
				savedImageSize = value;
			}
		}

		private static string savedImageFolder;
		public static string SavedImageFolder
		{
			get { return savedImageFolder; }
			set
			{
				savedImageFolder = value;
			}
		}

		private static ImageFormat savedImageFormat;
		public static ImageFormat SavedImageFormat
		{
			get { return savedImageFormat; }
			set
			{
				savedImageFormat = value;
			}
		}

        private static int settingsVersion = 0;
        private const int settingsVersionCurrent = 1;

        public static void defaults()
        {
            showHeartRate = true;
            showPace = false;
            showSpeed = false;
            showPower = false;
            showCadence = false;
            showElevation = false;
            showCategoryAverage = false;
            showMovingAverage = false;
            movingAverageLength = 0;
            movingAverageTime = 0;
            autoZoom = true;
            showTime = true;
            windowSize = new Size(800, 480);
#if ST_2_1
            savedImageSize = new Size( 800, 480 );
#else
            savedImageSize = SaveImageDialog.ImageSizeType.Medium;
#endif
            savedImageFolder = "";
			savedImageFormat = ImageFormat.Jpeg;
        }

        public static void ReadOptions(XmlDocument xmlDoc, XmlNamespaceManager nsmgr, XmlElement pluginNode)
        {
            String attr, attr2;

            attr = pluginNode.GetAttribute(xmlTags.settingsVersion);
            if (attr.Length > 0) { settingsVersion = (Int16)XmlConvert.ToInt16(attr); }
            if (0 == settingsVersion)
            {
                // No settings in Preferences.System found, try read old files
                load();
            }

            attr = pluginNode.GetAttribute(xmlTags.showHeartRate);
            if (attr.Length > 0) { showHeartRate = XmlConvert.ToBoolean(attr); }
            attr = pluginNode.GetAttribute(xmlTags.showPace);
            if (attr.Length > 0) { showPace = XmlConvert.ToBoolean(attr); }
            attr = pluginNode.GetAttribute(xmlTags.showSpeed);
            if (attr.Length > 0) { showSpeed = XmlConvert.ToBoolean(attr); }
            attr = pluginNode.GetAttribute(xmlTags.showPower);
            if (attr.Length > 0) { showPower = XmlConvert.ToBoolean(attr); }
            attr = pluginNode.GetAttribute(xmlTags.showCadence);
            if (attr.Length > 0) { showCadence = XmlConvert.ToBoolean(attr); }
            attr = pluginNode.GetAttribute(xmlTags.showElevation);
            if (attr.Length > 0) { showElevation = XmlConvert.ToBoolean(attr); }
            attr = pluginNode.GetAttribute(xmlTags.showCategoryAverage);
            if (attr.Length > 0) { showCategoryAverage = XmlConvert.ToBoolean(attr); }
            attr = pluginNode.GetAttribute(xmlTags.showMovingAverage);
            if (attr.Length > 0) { showMovingAverage = XmlConvert.ToBoolean(attr); }
            attr = pluginNode.GetAttribute(xmlTags.movingAverageLength);
            if (attr.Length > 0) { movingAverageLength = XmlConvert.ToInt16(attr); }
            attr = pluginNode.GetAttribute(xmlTags.movingAverageTime);
            if (attr.Length > 0) { movingAverageTime = XmlConvert.ToInt16(attr); }
            attr = pluginNode.GetAttribute(xmlTags.autoZoom);
            if (attr.Length > 0) { autoZoom = XmlConvert.ToBoolean(attr); }
            attr = pluginNode.GetAttribute(xmlTags.showTime);
            if (attr.Length > 0) { showTime = XmlConvert.ToBoolean(attr); }

            attr = pluginNode.GetAttribute(xmlTags.viewWidth);
            attr2 = pluginNode.GetAttribute(xmlTags.viewHeight);
            if (attr.Length > 0 && attr2.Length > 0)
            {
                windowSize = new Size(XmlConvert.ToInt16(attr), XmlConvert.ToInt16(attr2));
            }

			attr = pluginNode.GetAttribute( xmlTags.savedImageFolder );
			if ( attr.Length > 0 ) { savedImageFolder = attr; }
#if ST_2_1
 			attr = pluginNode.GetAttribute( xmlTags.savedImageWidth );
			attr2 = pluginNode.GetAttribute( xmlTags.savedImageHeight );
            if (attr.Length > 0 && attr2.Length > 0
            && XmlConvert.ToInt16( attr ) > 0 && XmlConvert.ToInt16( attr2 ) > 0)
            {
               savedImageSize = new Size( XmlConvert.ToInt16( attr ), XmlConvert.ToInt16( attr2 ) );
            }
#else
            attr = pluginNode.GetAttribute(xmlTags.savedImageSize);
            if (attr.Length > 0)
            {
                savedImageSize = (SaveImageDialog.ImageSizeType)Enum.Parse(typeof(SaveImageDialog.ImageSizeType), attr);
            }
#endif
			attr = pluginNode.GetAttribute( xmlTags.savedImageFormat );
			if ( attr.Length > 0 )
			{
				//I would have thought this should have been sufficient; however, it is not.
				savedImageFormat = new ImageFormat( XmlConvert.ToGuid( attr ) );
				//Need to this as well.
				savedImageFormat = ImageFormatGuid2ImageFormat( savedImageFormat );
			}
		}
		private static ImageFormat ImageFormatGuid2ImageFormat( ImageFormat imgF )
		{
			if ( imgF.Guid.CompareTo( ImageFormat.Bmp.Guid ) == 0 )
				return ImageFormat.Bmp;
			else if ( imgF.Guid.CompareTo( ImageFormat.Emf.Guid ) == 0 )
				return ImageFormat.Emf;
			else if ( imgF.Guid.CompareTo( ImageFormat.Exif.Guid ) == 0 )
				return ImageFormat.Exif;
			else if ( imgF.Guid.CompareTo( ImageFormat.Gif.Guid ) == 0 )
				return ImageFormat.Gif;
			else if ( imgF.Guid.CompareTo( ImageFormat.Icon.Guid ) == 0 )
				return ImageFormat.Icon;
			else if ( imgF.Guid.CompareTo( ImageFormat.Jpeg.Guid ) == 0 )
				return ImageFormat.Jpeg;
			else if ( imgF.Guid.CompareTo( ImageFormat.MemoryBmp.Guid ) == 0 )
				return ImageFormat.MemoryBmp;
			else if ( imgF.Guid.CompareTo( ImageFormat.Png.Guid ) == 0 )
				return ImageFormat.Png;
			else if ( imgF.Guid.CompareTo( ImageFormat.Tiff.Guid ) == 0 )
				return ImageFormat.Tiff;
			else if ( imgF.Guid.CompareTo( ImageFormat.Wmf.Guid ) == 0 )
				return ImageFormat.Wmf;
			return imgF;
		}

        public static void WriteOptions(XmlDocument xmlDoc, XmlElement pluginNode)
        {
            pluginNode.SetAttribute(xmlTags.settingsVersion, XmlConvert.ToString(settingsVersionCurrent));

            pluginNode.SetAttribute(xmlTags.showHeartRate, XmlConvert.ToString(showHeartRate));
            pluginNode.SetAttribute(xmlTags.showPace, XmlConvert.ToString(showPace));
            pluginNode.SetAttribute(xmlTags.showSpeed, XmlConvert.ToString(showSpeed));
            pluginNode.SetAttribute(xmlTags.showPower, XmlConvert.ToString(showPower));
            pluginNode.SetAttribute(xmlTags.showCadence, XmlConvert.ToString(showCadence));
            pluginNode.SetAttribute(xmlTags.showElevation, XmlConvert.ToString(showElevation));
            pluginNode.SetAttribute(xmlTags.showCategoryAverage, XmlConvert.ToString(showCategoryAverage));
            pluginNode.SetAttribute(xmlTags.showMovingAverage, XmlConvert.ToString(showMovingAverage));
            pluginNode.SetAttribute(xmlTags.movingAverageLength, XmlConvert.ToString(movingAverageLength));
            pluginNode.SetAttribute(xmlTags.movingAverageTime, XmlConvert.ToString(movingAverageTime));
            pluginNode.SetAttribute(xmlTags.autoZoom, XmlConvert.ToString(autoZoom));
            pluginNode.SetAttribute(xmlTags.showTime, XmlConvert.ToString(showTime));

            pluginNode.SetAttribute(xmlTags.viewWidth, XmlConvert.ToString(windowSize.Width));
            pluginNode.SetAttribute(xmlTags.viewHeight, XmlConvert.ToString(windowSize.Height));

			pluginNode.SetAttribute(xmlTags.savedImageFolder, savedImageFolder);
#if ST_2_1
			pluginNode.SetAttribute(xmlTags.savedImageWidth,XmlConvert.ToString(savedImageSize.Width));
			pluginNode.SetAttribute(xmlTags.savedImageHeight,XmlConvert.ToString(savedImageSize.Height));
#else
			pluginNode.SetAttribute(xmlTags.savedImageSize,savedImageSize.ToString());
#endif
            pluginNode.SetAttribute(xmlTags.savedImageFormat,XmlConvert.ToString(savedImageFormat.Guid));
        }

        private class xmlTags
        {
            public const string settingsVersion = "settingsVersion";

            public const string showHeartRate = "showHeartRate";
            public const string showPace = "showPace";
            public const string showSpeed = "showSpeed";
            public const string showPower = "showPower";
            public const string showCadence = "showCadence";
            public const string showElevation = "showElevation";
            public const string showCategoryAverage = "showCategoryAverage";
            public const string showMovingAverage = "showMovingAverage";
            public const string movingAverageLength = "movingAverageLength";
            public const string movingAverageTime = "movingAverageTime";
            public const string autoZoom = "autoZoom";
            public const string showTime = "showTime";
            public const string viewWidth = "viewWidth";
            public const string viewHeight = "viewHeight";

			public const string savedImageFolder = "savedImageFolder";
#if ST_2_1
			public const string savedImageWidth = "savedImageWidth";
			public const string savedImageHeight = "savedImageHeight";
#else
            public const string savedImageSize = "savedImageSize";
#endif
            public const string savedImageFormat = "savedImageFormat";
        }
        
        private static bool load()
        {
            //Backwards compatibility, read old preferences file
            String prefsPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + Path.DirectorySeparatorChar + "OverlayPlugin" + Path.DirectorySeparatorChar + "preferences.xml";

            if (!File.Exists(prefsPath)) return false;
            XmlDocument document = new XmlDocument();
            XmlReader reader = new XmlTextReader(prefsPath);
            document.Load(reader);
            try
            {
                XmlNode elm = document.ChildNodes[0]["view"];
                windowSize = new Size(int.Parse(elm.Attributes["viewWidth"].Value),
                                                    int.Parse(elm.Attributes["viewHeight"].Value));
                showHeartRate = bool.Parse(elm.Attributes["showHeartRate"].Value);
                showPace = bool.Parse(elm.Attributes["showPace"].Value);
                showSpeed = bool.Parse(elm.Attributes["showSpeed"].Value);
                showPower = bool.Parse(elm.Attributes["showPower"].Value);
                showCadence = bool.Parse(elm.Attributes["showCadence"].Value);
                showElevation = bool.Parse(elm.Attributes["showElevation"].Value);
                showTime = bool.Parse(elm.Attributes["showTime"].Value);
                showCategoryAverage = bool.Parse(elm.Attributes["showCategoryAverage"].Value);
                showMovingAverage = bool.Parse(elm.Attributes["showMovingAverage"].Value);
                movingAverageLength = parseDouble(elm.Attributes["movingAverageLength"].Value);
                movingAverageTime = parseDouble(elm.Attributes["movingAverageTime"].Value);
                autoZoom = bool.Parse(elm.Attributes["autoZoom"].Value);

				savedImageFolder = elm.Attributes["savedImageFolder"].Value;
#if ST_2_1
//the old load file could be used when a user migrates from old plugin to ST3. Ignore this.
                savedImageSize = new Size( int.Parse( elm.Attributes["savedImageWidth"].Value ),
										int.Parse( elm.Attributes["savedImageWidth"].Value ) );
#endif
				savedImageFormat = new ImageFormat( new Guid( elm.Attributes["savedImageFormat"].Value ) );

            }
            catch (Exception)
            {
                reader.Close();
                return false;
            }
            reader.Close();
            return true;
        }
        private static double parseDouble(string p)
        {
            //if (!p.Contains(".")) p += ".0";
            double d = double.Parse(p, NumberFormatInfo.InvariantInfo);
            return d;
        }
        private static Type getPlugin(string plugin, string klass)
        {
            try
            {
                //List the assemblies in the current application domain
                AppDomain currentDomain = AppDomain.CurrentDomain;
                Assembly[] assems = currentDomain.GetAssemblies();

                foreach (Assembly assem in assems)
                {
                    AssemblyName assemName = new AssemblyName((assem.FullName));
                    if (assemName.Name.Equals(plugin + "Plugin"))
                    {
                        return assem.GetType(klass);
                    }
                }
            }
            catch (Exception) { }
            return null;
        }

    }
}
