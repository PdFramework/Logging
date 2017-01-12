[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(PeinearyDevelopment.Framework.Logging.NLog.Web.Http.WebActivator.LoggingWebActivator), "PreStart")]
namespace PeinearyDevelopment.Framework.Logging.NLog.Web.Http.WebActivator
{
    using System.Web.Http;
    using Http;

    public static class LoggingWebActivator
    {
        public static void PreStart()
        {
            WebApiLoggerConfigurer.Configure(GlobalConfiguration.Configuration);
        }
    }
}
