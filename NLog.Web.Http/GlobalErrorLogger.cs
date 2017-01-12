using NLog;

namespace PeinearyDevelopment.Framework.Logging.NLog.Web.Http
{
    using System.Web.Http.ExceptionHandling;

    //https://ruhul.wordpress.com/2014/09/05/how-to-handle-exceptions-globally-in-asp-net-webapi-2/
    public class GlobalErrorLogger : ExceptionLogger
    {
        public override void Log(ExceptionLoggerContext context)
        {
            LogManager.GetCurrentClassLogger().Error(context.Exception);
        }
    }
}
