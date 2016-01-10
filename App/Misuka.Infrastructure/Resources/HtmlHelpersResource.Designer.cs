﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.17929
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Misuka.Infrastructure.Resources {
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
    internal class HtmlHelpersResource {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal HtmlHelpersResource() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Misuka.Infrastructure.Resources.HtmlHelpersResource", typeof(HtmlHelpersResource).Assembly);
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
        ///   Looks up a localized string similar to dd.MM.yyyy.
        /// </summary>
        internal static string DatePickerDatePattern {
            get {
                return ResourceManager.GetString("DatePickerDatePattern", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to dd.mm.yy.
        /// </summary>
        internal static string DatePickerJavascriptDatePattern {
            get {
                return ResourceManager.GetString("DatePickerJavascriptDatePattern", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to mm.yy.
        /// </summary>
        internal static string DatePickerJavascriptMonthYearOnlyPattern {
            get {
                return ResourceManager.GetString("DatePickerJavascriptMonthYearOnlyPattern", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to hh:mm.
        /// </summary>
        internal static string DatePickerJavascriptTimePattern {
            get {
                return ResourceManager.GetString("DatePickerJavascriptTimePattern", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to MM.yyyy.
        /// </summary>
        internal static string DatePickerMonthYearOnlyPattern {
            get {
                return ResourceManager.GetString("DatePickerMonthYearOnlyPattern", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to HH:mm.
        /// </summary>
        internal static string DatePickerTimePattern {
            get {
                return ResourceManager.GetString("DatePickerTimePattern", resourceCulture);
            }
        }
    }
}
