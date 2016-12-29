using NLog.Internal;
using NLog.Targets;

namespace PeinearyDevelopment.Framework.Logging.NLog.Core.TargetGenerators
{
    using Extensions;
    using Utilities;
    using System.IO;
    using System.Reflection;
    using System.Collections.Generic;

    public static class File
    {
        public static FileTarget Generate<TEvent>(string logsDirectoryPath, IConfigurationManager configurationManager, IDictionary<string, string> layoutOverrides = null)
        {
            var applicationName = configurationManager.GetAppSettingOrDefault(Constants.AppSettingsKeys.ApplicationName, () => Assembly.GetEntryAssembly().FullName);
            var target = new FileTarget
            {
                CreateDirs = true,
                FileName = Path.Combine(logsDirectoryPath, $"{applicationName}_log.txt"),
                Layout = ParametersGenerator.GenerateLayoutString<TEvent>(layoutOverrides),
                Name = TargetNameGenerator.Generate<FileTarget>()
            };

            return target;
        }
    }
}
