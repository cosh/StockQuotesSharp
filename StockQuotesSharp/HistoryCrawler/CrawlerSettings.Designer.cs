﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.237
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace cosh.Stock {
    
    
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "10.0.0.0")]
    internal sealed partial class CrawlerSettings : global::System.Configuration.ApplicationSettingsBase {
        
        private static CrawlerSettings defaultInstance = ((CrawlerSettings)(global::System.Configuration.ApplicationSettingsBase.Synchronized(new CrawlerSettings())));
        
        public static CrawlerSettings Default {
            get {
                return defaultInstance;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("data")]
        public string DataDirectory {
            get {
                return ((string)(this["DataDirectory"]));
            }
            set {
                this["DataDirectory"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("True")]
        public bool OverWrite {
            get {
                return ((bool)(this["OverWrite"]));
            }
            set {
                this["OverWrite"] = value;
            }
        }
    }
}
