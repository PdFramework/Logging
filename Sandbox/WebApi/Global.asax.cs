﻿namespace PeinearyDevelopment.Framework.Logging.Sandbox.WebApi
{
    using Config;
    using System.Web;
    using System.Web.Http;

    public class WebApiApplication : HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
        }
    }
}
