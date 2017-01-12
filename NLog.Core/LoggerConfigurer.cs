using NLog;
using NLog.Common;
using NLog.Config;
using NLog.Internal;
using NLog.Time;

namespace PeinearyDevelopment.Framework.Logging.NLog.Core
{
    using Extensions;
    using Contracts;
    using System.Collections.Generic;
    using System.IO;
    using System;

    public abstract class LoggerConfigurer : ILoggerConfigurer, IDisposable
    {
        private readonly IConfigurationManager _configurationManager;
        private readonly LogLevel _defaultLogLevel = LogLevel.Warn;
        private TargetManager TargetManager { get; set; }

        protected LoggerConfigurer() : this(new ConfigurationManager())
        {
        }

        protected LoggerConfigurer(IConfigurationManager configurationManager)
        {
            _configurationManager = configurationManager;
        }

        public void ConfigureDefault(IDictionary<string, string> eventPropertyRenderOverrides)
        {
            var defaultTargetConfigurations = new[]
            {
                //new AttemptTargetConfigurer { AttemptRank = 1, ShouldAttemptTarget = true, Type = TargetType.Messaging },
                //new AttemptTargetConfigurer { AttemptRank = 2, ShouldAttemptTarget = true, Type = TargetType.WebService },
                new AttemptTargetConfigurer { AttemptRank = 3, ShouldAttemptTarget = true, Type = TargetType.File }
            };

            Configure(eventPropertyRenderOverrides, defaultTargetConfigurations);
        }

        public void Configure(IDictionary<string, string> eventPropertyRenderOverrides = null, params AttemptTargetConfigurer[] configurations)
        {
            // https://github.com/nlog/NLog/wiki/Time%20Source
            TimeSource.Current = new FastUtcTimeSource();

            // http://stackoverflow.com/questions/25238854/nlog-fallbackgroup-log-exception-on-fail-to-log
            var logsPath = _configurationManager.GetAppSettingOrDefault(Constants.AppSettingsKeys.LogsPath, Constants.DefaultLogsPath);
            InternalLogger.LogFile = Path.Combine(logsPath, "Nlog_log.txt");
            InternalLogger.LogLevel = LogLevel.Info;

            TargetManager = new TargetManager(_configurationManager, eventPropertyRenderOverrides);
            var logEventTarget = TargetManager.GenerateDefaultTarget<LogEvent>(configurations);
            var loginEventTarget = TargetManager.GenerateDefaultTarget<LoginEvent>(configurations);
            var actionTakenEventTarget = TargetManager.GenerateDefaultTarget<ActionTakenEvent>(configurations);

            var config = new LoggingConfiguration();
            config.AddTarget(logEventTarget);
            config.AddTarget(loginEventTarget);
            config.AddTarget(actionTakenEventTarget);

            var logLevel = _configurationManager.GetAppSettingOrDefault(Constants.AppSettingsKeys.LogLevel, _defaultLogLevel, LogLevel.FromString);
            config.LoggingRules.Add(new LoggingRule("LoginLogger", logLevel, loginEventTarget) { Final = true });
            config.LoggingRules.Add(new LoggingRule("ActionTakenLogger", logLevel, actionTakenEventTarget) { Final = true });
            config.LoggingRules.Add(new LoggingRule("*", logLevel, logEventTarget));

            LogManager.Configuration = config;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                TargetManager.Dispose();
            }
        }
    }
}
