namespace PeinearyDevelopment.Framework.Logging.NLog.Web
{
    using Core;
    using System.Collections.Generic;

    public class WebLoggerConfigurer : LoggerConfigurer
    {
        public new virtual void ConfigureDefault(IDictionary<string, string> eventPropertyRenderOverrides = null)
        {
            base.ConfigureDefault(GenerateWebEventPropertyOverrides(eventPropertyRenderOverrides));
        }

        public new void Configure(IDictionary<string, string> eventPropertyRenderOverrides = null, params AttemptTargetConfigurer[] configurations)
        {
            base.Configure(GenerateWebEventPropertyOverrides(eventPropertyRenderOverrides), configurations);
        }

        private IDictionary<string, string> GenerateWebEventPropertyOverrides(IDictionary<string, string> eventPropertyRenderOverrides = null)
        {
            if (eventPropertyRenderOverrides == null) eventPropertyRenderOverrides = new Dictionary<string, string>();

            eventPropertyRenderOverrides.Add(new KeyValuePair<string, string>("Username", "${aspnet-user-identity}"));
            eventPropertyRenderOverrides.Add(new KeyValuePair<string, string>("CallSite", "${iis-site-name}"));

            return eventPropertyRenderOverrides;
        }
    }
}
