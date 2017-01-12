using NLog.Internal;

namespace PeinearyDevelopment.Framework.Logging.NLog.Core.Extensions
{
    using System;

    public static class ConfigManager
    {
        public static string GetAppSettingOrDefault(this IConfigurationManager configurationManager, string appSettingKey, string defaultValue)
        {
            return string.IsNullOrWhiteSpace(configurationManager.AppSettings[appSettingKey]) ? defaultValue : configurationManager.AppSettings[appSettingKey];
        }

        public static string GetAppSettingOrDefault(this IConfigurationManager configurationManager, string appSettingKey, Func<string> defaultValueFunc)
        {
            return string.IsNullOrWhiteSpace(configurationManager.AppSettings[appSettingKey]) ? defaultValueFunc() : configurationManager.AppSettings[appSettingKey];
        }

        public static T GetAppSettingOrDefault<T>(this IConfigurationManager configurationManager, string appSettingKey, T defaultValue, Func<string, T> convertAppSettinToValueFunc)
        {
            return string.IsNullOrWhiteSpace(configurationManager.AppSettings[appSettingKey]) ? defaultValue : convertAppSettinToValueFunc(configurationManager.AppSettings[appSettingKey]);
        }
    }
}
