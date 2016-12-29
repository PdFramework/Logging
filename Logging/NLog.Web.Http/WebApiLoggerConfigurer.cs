namespace PeinearyDevelopment.Framework.Logging.NLog.Web.Http
{
    using Core;
    using System.Collections.Generic;
    using System.Web.Http;
    using System.Web.Http.ExceptionHandling;
    using Web;

    public class WebApiLoggerConfigurer : WebLoggerConfigurer
    {
        public new virtual void ConfigureDefault(IDictionary<string, string> eventPropertyRenderOverrides = null)
        {
            base.ConfigureDefault(eventPropertyRenderOverrides);
        }

        public new void Configure(IDictionary<string, string> eventPropertyRenderOverrides = null, params AttemptTargetConfigurer[] configurations)
        {
            base.Configure(eventPropertyRenderOverrides, configurations);
        }

        public static void Configure(HttpConfiguration httpConfiguration)
        {
            new WebApiLoggerConfigurer().Configure();
            httpConfiguration.Services.Add(typeof(IExceptionLogger), new GlobalErrorLogger());
        }
    }
}
