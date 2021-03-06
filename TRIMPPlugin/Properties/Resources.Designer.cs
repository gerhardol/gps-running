﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.1
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace GpsRunningPlugin.Properties {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Resources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resources() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("GpsRunningPlugin.Properties.Resources", typeof(Resources).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to TRIMP.
        /// </summary>
        internal static string ApplicationName {
            get {
                return ResourceManager.GetString("ApplicationName", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Current rest heart rate: {0}. Maximal heart rate: {1}..
        /// </summary>
        internal static string CurrentRestAndMax {
            get {
                return ResourceManager.GetString("CurrentRestAndMax", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Ignored {0} activities that did not have a heart rate track..
        /// </summary>
        internal static string IgnoredActivities {
            get {
                return ResourceManager.GetString("IgnoredActivities", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Ignored one activity since it did not have a heart rate track..
        /// </summary>
        internal static string IgnoredActivity {
            get {
                return ResourceManager.GetString("IgnoredActivity", resourceCulture);
            }
        }
        
        internal static System.Drawing.Bitmap Image_16_TRIMP {
            get {
                object obj = ResourceManager.GetObject("Image_16_TRIMP", resourceCulture);
                return ((System.Drawing.Bitmap)(obj));
            }
        }
        
        internal static System.Drawing.Bitmap Image_24_TRIMP {
            get {
                object obj = ResourceManager.GetObject("Image_24_TRIMP", resourceCulture);
                return ((System.Drawing.Bitmap)(obj));
            }
        }
        
        internal static System.Drawing.Bitmap Image_32_TRIMP {
            get {
                object obj = ResourceManager.GetObject("Image_32_TRIMP", resourceCulture);
                return ((System.Drawing.Bitmap)(obj));
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to No resting or no maximal heart rate in the logbook.\nThe plugin will not do anything before a maximal heart rate is set.\nTo set this go to the athlete view..
        /// </summary>
        internal static string NoRestOrMaxHR {
            get {
                return ResourceManager.GetString("NoRestOrMaxHR", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Summary.
        /// </summary>
        internal static string Summary {
            get {
                return ResourceManager.GetString("Summary", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to TRIMP of a single activity.
        /// </summary>
        internal static string T1 {
            get {
                return ResourceManager.GetString("T1", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to TRIMP of {0} activities.
        /// </summary>
        internal static string T2 {
            get {
                return ResourceManager.GetString("T2", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Total TRIMP is {0}..
        /// </summary>
        internal static string TotalTRIMP {
            get {
                return ResourceManager.GetString("TotalTRIMP", resourceCulture);
            }
        }
    }
}
