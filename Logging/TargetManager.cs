using NLog.Internal;
using NLog.Targets;

namespace PeinearyDevelopment.Framework.Logging.NLog.Core
{
    using Extensions;
    using TargetGenerators;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class TargetManager : IDisposable
    {
        private readonly IConfigurationManager _configurationManager;
        private static IDictionary<string, string> _eventPropertyRenderOverrides;
        private IList<Target> Targets { get; set; }

        public TargetManager(IConfigurationManager configurationManager, IDictionary<string, string> eventPropertyRenderOverrides)
        {
            _configurationManager = configurationManager;
            _eventPropertyRenderOverrides = eventPropertyRenderOverrides ?? new Dictionary<string, string>();
            Targets = new List<Target>();
        }

        public Target GenerateDefaultTarget<TEvent>(AttemptTargetConfigurer[] configurations) where TEvent : class
        {
            if (configurations == null || !configurations.Any()) return new NullTarget();
            if (configurations.Length == 1) return GenerateDefaultTarget<TEvent>(configurations.First());

            // assumes only want to log to one source at a time and that some precedence should be applied to the targets
            var orderedTargetConfigurers = configurations.OrderBy(config => config.AttemptRank ?? int.MinValue).ThenByDescending(config => (int)config.Type).ToArray();
            var orderedTargets = new Target[orderedTargetConfigurers.Length];
            for (var i = 0; i < orderedTargetConfigurers.Length; i++)
            {
                var orderedTargetConfigurer = orderedTargetConfigurers[i];
                orderedTargets[i] = GenerateDefaultTarget<TEvent>(orderedTargetConfigurer);
            }
            return FallbackGroup.Generate(orderedTargets);
        }

        public Target GenerateDefaultTarget<TEvent>(AttemptTargetConfigurer configuration) where TEvent : class
        {
            var logsPath = _configurationManager.GetAppSettingOrDefault(Constants.AppSettingsKeys.LogsPath, Constants.DefaultLogsPath);
            Target target;
            switch (configuration.Type)
            {
                case TargetType.Console:
                    target = new ColoredConsoleTarget();
                    break;
                case TargetType.File:
                    target = File.Generate<TEvent>(logsPath, _configurationManager, _eventPropertyRenderOverrides);
                    break;
                case TargetType.Database:
                    target = Database.Generate<TEvent>(_configurationManager, _eventPropertyRenderOverrides);
                    break;
                case TargetType.WebService:
                    target = WebService.Generate<TEvent>(_configurationManager, _eventPropertyRenderOverrides);
                    break;
                case TargetType.Messaging:
                    target = MessageQueue.Generate<TEvent>(_configurationManager, _eventPropertyRenderOverrides);
                    break;
                default:
                    target = new NullTarget();
                    break;
            }

            Targets.Add(target);
            return target;
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
                foreach (var target in Targets)
                {
                    target.Dispose();
                }
            }
        }
    }
}
