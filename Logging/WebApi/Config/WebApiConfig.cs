namespace PeinearyDevelopment.Framework.Logging.Sandbox.WebApi.Config
{
    using System.Web.Http;

    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes();
        }
    }
}
