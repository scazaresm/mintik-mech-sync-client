﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MechanicalSyncApp.Properties {
    
    
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "17.7.0.0")]
    internal sealed partial class Settings : global::System.Configuration.ApplicationSettingsBase {
        
        private static Settings defaultInstance = ((Settings)(global::System.Configuration.ApplicationSettingsBase.Synchronized(new Settings())));
        
        public static Settings Default {
            get {
                return defaultInstance;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("")]
        public string SERVER_URL {
            get {
                return ((string)(this["SERVER_URL"]));
            }
            set {
                this["SERVER_URL"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("60")]
        public int DEFAULT_TIMEOUT_SECONDS {
            get {
                return ((int)(this["DEFAULT_TIMEOUT_SECONDS"]));
            }
            set {
                this["DEFAULT_TIMEOUT_SECONDS"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("")]
        public string EDRAWINGS_VIEWER_CLSID {
            get {
                return ((string)(this["EDRAWINGS_VIEWER_CLSID"]));
            }
            set {
                this["EDRAWINGS_VIEWER_CLSID"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("")]
        public string WORKSPACE_DIRECTORY {
            get {
                return ((string)(this["WORKSPACE_DIRECTORY"]));
            }
            set {
                this["WORKSPACE_DIRECTORY"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("")]
        public string PUBLISHING_DIRECTORY {
            get {
                return ((string)(this["PUBLISHING_DIRECTORY"]));
            }
            set {
                this["PUBLISHING_DIRECTORY"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("")]
        public string SOLIDWORKS_EXE_PATH {
            get {
                return ((string)(this["SOLIDWORKS_EXE_PATH"]));
            }
            set {
                this["SOLIDWORKS_EXE_PATH"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("")]
        public string EDRAWINGS_EXE_PATH {
            get {
                return ((string)(this["EDRAWINGS_EXE_PATH"]));
            }
            set {
                this["EDRAWINGS_EXE_PATH"] = value;
            }
        }
    }
}
