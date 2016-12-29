using NLog;

namespace PeinearyDevelopment.Framework.Logging.NLog.Console
{
    using Core;
    using System;
    using System.Collections.Generic;
    using System.Reflection;

    public class ConsoleLoggerConfigurer : LoggerConfigurer
    {
        public new virtual void ConfigureDefault(IDictionary<string, string> eventPropertyRenderOverrides = null)
        {
            base.ConfigureDefault(eventPropertyRenderOverrides);
            SetupUnhandledExceptionLogging();
        }

        public new void Configure(IDictionary<string, string> eventPropertyRenderOverrides = null, params AttemptTargetConfigurer[] configurations)
        {
            base.Configure(eventPropertyRenderOverrides, configurations);
            SetupUnhandledExceptionLogging();
        }

        public void SetupUnhandledExceptionLogging()
        {
            AppDomain.CurrentDomain.UnhandledException += (sender, eventArgs) =>
            {
                // http://stackoverflow.com/questions/913472/why-is-unhandledexceptioneventargs-exceptionobject-an-object-and-not-an-exceptio#answer-1439602
                // http://knitinr.blogspot.com/2008/07/systemreflection-get-this-assembly.html
                LogManager.GetCurrentClassLogger().Error(eventArgs.ExceptionObject as Exception, $"{Assembly.GetEntryAssembly()}: Exiting application due to unhandled exception.");
                Environment.Exit(1);
            };

            AppDomain.CurrentDomain.ProcessExit += (sender, args) =>
            {
                Dispose();
            };
        }
    }
}
