using System.Web.Mvc;

namespace Gallery.Filters
{
    public class LogFilterAttribute : ActionFilterAttribute 
    {
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            Logger.Info("Login Request");
            base.OnActionExecuting(filterContext);
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            Logger.Info("Signed in");
            base.OnActionExecuted(filterContext);
        }
    }
}