using NLog.Internal;

namespace PeinearyDevelopment.Framework.Logging.NLog.Core.TargetGenerators
{
    using Utilities;
    using System.Collections.Generic;

    public static class MessageQueue
    {
        public static MassTransitTarget<TEvent> Generate<TEvent>(IConfigurationManager configurationManager, IDictionary<string, string> layoutOverrides) where TEvent : class
        {
            var target = new MassTransitTarget<TEvent>(configurationManager)
            {
                Name = TargetNameGenerator.Generate<MassTransitTarget<TEvent>>()
            };

            foreach (var parameter in ParametersGenerator.Generate<MessageQueueParameterInformation, TEvent>(layoutOverrides))
            {
                target.Parameters.Add(parameter);
            }

            return target;
        }
    }
}
