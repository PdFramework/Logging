using NLog;

namespace PeinearyDevelopment.Framework.Logging.NLog.Web.Mvc
{
    using System.Web.Mvc;

    public class GlobalHandleErrorAttribute : HandleErrorAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            LogManager.GetCurrentClassLogger().Error(context.Exception);
        }
    }
}
