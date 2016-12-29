using NLog;

namespace PeinearyDevelopment.Framework.Logging.Sandbox.Console
{
    using Contracts;
    using NLog.Console;
    using NLog.Core.Extensions;

    class Program
    {
        static void Main(string[] args)
        {
            new ConsoleLoggerConfigurer().ConfigureDefault();
            LogManagerExtensions.LogEvent(LogLevel.Debug, new LogEvent { Id = 1, UserId = "10" });
            LogManagerExtensions.LogEvent(LogLevel.Error, new LogEvent { Id = 2, UserId = "20" });
            LogManagerExtensions.LogEvent(LogLevel.Fatal, new LogEvent { Id = 3, UserId = "30" });
            LogManagerExtensions.LogEvent(LogLevel.Info, new LogEvent { Id = 4, UserId = "40" });
            LogManagerExtensions.LogEvent(LogLevel.Off, new LogEvent { Id = 5, UserId = "50" });
            LogManagerExtensions.LogEvent(LogLevel.Trace, new LogEvent { Id = 6, UserId = "60" });
            LogManagerExtensions.LogEvent(LogLevel.Warn, new LogEvent { Id = 7, UserId = "70" });
            LogManagerExtensions.LogLoginEvent(new LoginEvent { Id = 8, UserId = "80" });
            LogManagerExtensions.LogActionTakenEvent(new ActionTakenEvent { Id = 9, UserId = "90" });
        }
    }
}