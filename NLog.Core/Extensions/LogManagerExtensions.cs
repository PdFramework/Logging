using NLog;
using Newtonsoft.Json;

namespace PeinearyDevelopment.Framework.Logging.NLog.Core.Extensions
{
    using Contracts;

    public static class LogManagerExtensions
    {
        public static void LogEvent(LogLevel logLevel, LogEvent logEvent)
        {
            LogManager.GetLogger("EventLogger").Log(logLevel, GenerateLogEventInfo(logEvent));
        }

        public static void LogLoginEvent(LoginEvent loginEvent)
        {
            LogManager.GetLogger("LoginEventLogger").Info(GenerateLogEventInfo(loginEvent));
        }

        public static void LogActionTakenEvent(ActionTakenEvent actionTakenEvent)
        {
            var logEventInfo = GenerateLogEventInfo(actionTakenEvent);
            logEventInfo.Properties["Object"] = JsonConvert.SerializeObject(actionTakenEvent.Object);
            LogManager.GetLogger("ActionTakenEventLogger").Info(logEventInfo);
        }

        private static LogEventInfo GenerateLogEventInfo<TEvent>(TEvent tEvent)
        {
            var logEventInfo = new LogEventInfo();
            foreach (var property in typeof(TEvent).GetProperties())
            {
                logEventInfo.Properties[property.Name] = property.GetValue(tEvent);
            }

            return logEventInfo;
        }
    }
}
