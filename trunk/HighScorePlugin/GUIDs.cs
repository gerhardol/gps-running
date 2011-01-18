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

using System;

namespace GpsRunningPlugin
{
	class GUIDs {
        public static readonly Guid PluginMain = new Guid("4B84E5C0-EC2B-4C0C-8B8E-3FAEB09F74C6");
        //public static readonly Guid Settings = new Guid("626c0731-1dc3-11e0-ac64-0800200c9a66");
        public static readonly Guid Activity = new Guid("626c0730-1dc3-11e0-ac64-0800200c9a66");
        public static readonly Guid OpenView = new Guid("1dc82ca0-88aa-45a5-a6c6-c25f56ad1fc3");
    }
}
namespace TrailsPlugin
{
    class GUIDs
    {
#if ST_2_1
        public static readonly Guid MapControlLayer = new Guid("626c0732-1dc3-11e0-ac64-0800200c9a66");
#else
        public static readonly Guid TrailPointsControlLayerProvider = new Guid("626c0732-1dc3-11e0-ac64-0800200c9a66");
#endif
    }
}