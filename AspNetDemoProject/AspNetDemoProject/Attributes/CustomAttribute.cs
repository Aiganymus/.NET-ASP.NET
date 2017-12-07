using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace AspNetDemoProject.Attributes
{
    public class CustomAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            //var field = actionContext.
        }
    }

    //public class VerifyUserAttribute : ActionFilterAttribute
    //{
    //    public override void OnActionExecuting(ActionExecutingContext filterContext)
    //    {
    //        var user = filterContext.HttpContext.Session["UserID"];
    //        if (user == null)
    //            filterContext.Result = new RedirectResult(string.Format("/User/Login?targetUrl={0}", filterContext.HttpContext.Request.Url.AbsolutePath));
    //    }
    //}
}