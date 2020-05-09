using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Windows.Media.Animation;

namespace Gallery.Filters
{
    public class ValidateModelStateAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var isValid = filterContext.Controller.ViewData.ModelState.IsValid;
            if (!isValid)
            {
                filterContext.HttpContext.Response.StatusCode = 400;
            }
            base.OnActionExecuting(filterContext);
        }
    }
}