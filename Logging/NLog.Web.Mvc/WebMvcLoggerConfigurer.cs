namespace PeinearyDevelopment.Framework.Logging.NLog.Web.Mvc
{
    using Core;
    using System.Collections.Generic;
    using System.Web.Mvc;
    using Web;

    public class WebMvcLoggerConfigurer : WebLoggerConfigurer
    {
        public new virtual void ConfigureDefault(IDictionary<string, string> eventPropertyRenderOverrides = null)
        {
            base.ConfigureDefault(eventPropertyRenderOverrides);
        }

        public new void Configure(IDictionary<string, string> eventPropertyRenderOverrides = null, params AttemptTargetConfigurer[] configurations)
        {
            base.Configure(eventPropertyRenderOverrides, configurations);
        }

        public static void Configure(GlobalFilterCollection filters, AttemptTargetConfigurer[] configurations = null)
        {
            new WebMvcLoggerConfigurer().Configure();
            filters.Add(new GlobalHandleErrorAttribute());
        }
    }
}
