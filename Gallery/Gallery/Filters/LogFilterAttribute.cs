
using System;
using System.Web.Mvc;

namespace Gallery.Filters
{
    public class LogFilterAttribute : ActionFilterAttribute, IExceptionFilter
    {
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {

            var request = filterContext.HttpContext.Request;

            var browser = request.Browser.Browser;

            var currentExecutionFilePath = request.CurrentExecutionFilePath;

            var userHostAddress = request.UserHostAddress;
            var httpMethod = request.HttpMethod;
            var machineName = filterContext.HttpContext.Server.MachineName;
            string userLanguages = "";
            for (int langCount = 0; langCount < request.UserLanguages.Length; langCount++)
            {
                userLanguages = userLanguages + langCount + 1 + ": " + request.UserLanguages[langCount] + ' ';
            }

            var logMessage = "\nBrowser = " + browser + "\nCurrentExecutionFilePath = " + currentExecutionFilePath +
                             "\nUserHostAddress = " + userHostAddress +
                             "\nHttpMethod = " + httpMethod + "\nMachineName = " + machineName + "\nUserLanguages = " +
                             userLanguages;
            Logger.Info(logMessage);
            base.OnActionExecuting(filterContext);
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            var response = filterContext.HttpContext.Response;

            var status = response.Status;
            var headers = response.Headers;


            var logMessage = "\nStatusCode and StatusDescription = " + status + "\nHeaders = " + headers;
            Logger.Info(logMessage);
            base.OnActionExecuted(filterContext);
        }

       public void OnException(ExceptionContext filterContext) 
       {
             if (!filterContext.ExceptionHandled)
             {
                 filterContext.ExceptionHandled = true;
             }
             var exceptionStack = filterContext.Exception.StackTrace;
             var exceptionMessage = filterContext.Exception.Message;
             var raukt = filterContext.Result;
             var logMessage = $"Bозникло исключение: \n {exceptionMessage} \n {exceptionStack}";
             Logger.Error(logMessage);
            
        }
    }
}