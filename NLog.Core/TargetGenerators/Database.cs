using NLog.Internal;
using NLog.Targets;

namespace PeinearyDevelopment.Framework.Logging.NLog.Core.TargetGenerators
{
    using Utilities;
    using System.Collections.Generic;
    using System.Data;

    public class Database
    {
        public static DatabaseTarget Generate<TEvent>(IConfigurationManager configurationManager, IDictionary<string, string> layoutOverrides = null)
        {
            if (string.IsNullOrWhiteSpace(configurationManager.AppSettings[Constants.AppSettingsKeys.ConnectionStringName])) throw new KeyNotFoundException($"No connection string name found in the app settings with key matching: {Constants.AppSettingsKeys.ConnectionStringName}.");

            var connectionStringName = configurationManager.AppSettings[Constants.AppSettingsKeys.ConnectionStringName];
            var target = new DatabaseTarget
            {
                Name = TargetNameGenerator.Generate<DatabaseTarget>(),
                ConnectionStringName = connectionStringName,
                CommandType = CommandType.StoredProcedure,
                CommandText = "[dbo].[CreateEntry]"
            };

            foreach (var parameter in ParametersGenerator.Generate<DatabaseParameterInformation, TEvent>(layoutOverrides))
            {
                parameter.Name = $"@{parameter.Name}";
                target.Parameters.Add(parameter);
            }

            return target;
        }
    }
}
