using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using System.Windows.Media.Animation;

namespace Gallery.Filters
{
    public class ValidateModelStateAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            var isValid = actionContext.ModelState.IsValid;

            if (!isValid)
            {
                actionContext.Response.StatusCode = HttpStatusCode.BadRequest;
            }
            base.OnActionExecuting(actionContext);
        }
    }
}