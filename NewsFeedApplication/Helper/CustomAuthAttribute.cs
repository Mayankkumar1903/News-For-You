using NewsFeedApplication.Utils;
using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace NewsFeedApplication.Helper
{
    public class CustomAuthAttribute : AuthorizeAttribute
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            var httpContext = filterContext.HttpContext;
            var sessionModel = SessionState.SessionInfo;
            bool isAdmin = sessionModel?.isUserAdmin ?? false;
            string pageName = httpContext.Request.Path.ToLower();

            var urlHelper = new UrlHelper(filterContext.RequestContext);

            if (sessionModel != null && isAdmin)
            {
                if (pageName.Contains("/login") || pageName.Contains("/userregistration"))
                {
                    filterContext.Result = new RedirectResult(urlHelper.Action("Index", "AdminDashboard"));
                    return;
                }
                else if (pageName.Contains("admindashboard"))
                {
                    return;
                }
            }
            else if (sessionModel != null)
            {
                if (pageName.Contains("/login") || pageName.Contains("/userregistration") || pageName.Contains("/admindashboard"))
                {
                    filterContext.Result = new RedirectResult(urlHelper.Action("Index", "Dashboard"));
                    return;
                }
            }
            else
            {
                if (pageName.Contains("/admindashboard"))
                {
                    filterContext.Result = new RedirectResult(urlHelper.Action("Index", "Dashboard"));
                    return;
                }
                else if (pageName.Contains("/login"))
                {
                    return;
                }
                else if (pageName.Contains("/userregistration"))
                {
                    return;
                }
            }

            base.OnAuthorization(filterContext);
        }

    }
}
