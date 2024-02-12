using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MechanicalSyncApp.Core.Util
{
    public class SettingsUtils
    {
        public static void UpsertSetting(string key, string value)
        {
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            var settings = config.AppSettings.Settings;

            if (settings[key] != null)
                settings[key].Value = value;
            else
                settings.Add(key, value);

            config.Save(ConfigurationSaveMode.Modified);
        }
    }
}
