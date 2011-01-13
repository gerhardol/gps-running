/*
Copyright (C) 2009 Brendan Doherty

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

//Used in both Trails and Matrix plugin, slightly reduced and modified in Matrix

using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;
using System.IO;
using ZoneFiveSoftware.Common.Visuals.Fitness;

namespace TrailsPlugin
{
    class CommonIcons
    {

        private static IApplication GetApplication()
        {
#if GPSRUNNING_UNIQUEROUTES||GPSRUNNING_OVERLAY||GPSRUNNING_HIGHSCORE||GPSRUNNING_PERFORMANCEPREDICTOR
            return GpsRunningPlugin.Plugin.GetApplication();
#elif MATRIXPLUGIN
            return MatrixPlugin.MatrixPlugin.GetApplication();
#else // TRAILSPLUGIN
            return PluginMain.GetApplication();
#endif
        }

        private static string GetMainGuid()
        {
#if GPSRUNNING_UNIQUEROUTES||GPSRUNNING_OVERLAY||GPSRUNNING_HIGHSCORE||GPSRUNNING_PERFORMANCEPREDICTOR
            return GpsRunningPlugin.GUIDs.PluginMain.ToString();
#elif MATRIXPLUGIN
            return MatrixPlugin.GUIDs.PluginMain.ToString();
#else // TRAILSPLUGIN
            return PluginMain.GUIDs.PluginMain.ToString();
#endif
        }

#if !ST_2_1
        const int brushSize = 6; //Even
        //The outer radius defines the included area
        static public string Circle(int sizeX, int sizeY, out Size iconSize)
        {
            string basePath = GetApplication().Configuration.CommonWebFilesFolder +
                                  System.IO.Path.DirectorySeparatorChar +
                                  GetMainGuid() + System.IO.Path.DirectorySeparatorChar;
            if (!Directory.Exists(basePath))
            {

                DirectoryInfo di = Directory.CreateDirectory(basePath);
            }

            //TODO: get largest meaningful icon somehow, to avoid crashes. Use fixed size for now, using minimal marker
            const int maxCircle = 999;
            if (sizeX > maxCircle || sizeY > maxCircle)
            {
                sizeX = 1;
                sizeY = 1;
            }

            sizeX = Math.Max(sizeX, brushSize * 2 - 1);
            sizeY = Math.Max(sizeY, brushSize * 2 - 1);
            //As the image is anchored in the middle, make size odd so (size/2) give center point
            if (1 != sizeX % 2) { sizeX++; }
            if (1 != sizeY % 2) { sizeY++; }

            iconSize = new Size(sizeX, sizeY);
            string filePath = basePath + "circle-" + sizeX+"_"+sizeY + ".png";
            if (!File.Exists(filePath))
            {
                //No version handling other than filename
                Bitmap myBitmap = new Bitmap(sizeX, sizeY);
                Graphics myGraphics = Graphics.FromImage(myBitmap);
                myGraphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                myGraphics.DrawEllipse(new Pen(Color.Red, brushSize), new Rectangle(brushSize / 2, brushSize / 2, 
                    myBitmap.Width - brushSize, myBitmap.Height - brushSize));
                myGraphics.DrawEllipse(new Pen(Color.Black, 1), new Rectangle(1, 1, 
                    myBitmap.Width - 2, myBitmap.Height - 2));
                FileStream myFileOut = new FileStream(filePath, FileMode.Create);
                myBitmap.Save(myFileOut, ImageFormat.Png);
                myFileOut.Close();
            }
            return "file://" + filePath;
        }
#endif
	}
}
