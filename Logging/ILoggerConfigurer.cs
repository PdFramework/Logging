namespace PeinearyDevelopment.Framework.Logging.NLog.Core
{
    using System.Collections.Generic;

    public interface ILoggerConfigurer
    {
        void ConfigureDefault(IDictionary<string, string> eventPropertyRenderOverrides);
        // void Configure(IDictionary<string, string> eventPropertyRenderOverrides, params TargetAttemptConfigurer[] configurations);
    }
}
