using NLog;
using NLog.Config;
using NLog.Internal;
using NLog.Targets;

namespace PeinearyDevelopment.Framework.Logging.NLog.Core.TargetGenerators
{
    using MassTransit;
    using System;
    using System.Collections.Generic;
    using System.Dynamic;
    using System.Linq;

    [Target("MassTransitTarget")]
    public class MassTransitTarget<TEvent> : TargetWithLayout where TEvent : class
    {
        [ArrayParameter(typeof(MessageQueueParameterInformation), "parameter")]
        public IList<MessageQueueParameterInformation> Parameters { get; }

        private IBusControl BusControl { get; set; }

        public MassTransitTarget(IConfigurationManager configurationManager)
        {
            Parameters = new List<MessageQueueParameterInformation>();
            var messagingEndpoint = configurationManager.AppSettings["Messaging.Endpoint"];
            BusControl = Bus.Factory.CreateUsingRabbitMq(cfg =>
            {
                cfg.Host(new Uri(messagingEndpoint), h =>
                {
                    h.Username(configurationManager.AppSettings["Messaging.Username"]);
                    h.Password(configurationManager.AppSettings["Messaging.Password"]);
                });
            });

            BusControl.Start();
        }

        protected override void Write(LogEventInfo logEvent)
        {
            IDictionary<string, object> publishMessage = new ExpandoObject();

            foreach(var property in typeof(TEvent).GetProperties())
            {
                publishMessage[property.Name] = RenderProperty(logEvent, property.Name);
            }

            BusControl.Publish<TEvent>(publishMessage).Wait();
        }

        private string RenderProperty(LogEventInfo logEvent, string propertyName)
        {
            return Parameters.First(parameter => parameter.Name == propertyName).Layout.Render(logEvent);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                BusControl.Stop();
                base.Dispose(disposing);
            }
        }
    }
}
