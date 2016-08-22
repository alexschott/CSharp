using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
namespace Aloe.Helper
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, Inherited = true, AllowMultiple = true)]
    public class AuthorizeUserAttribute : AuthorizeAttribute
    {
        private string UnAuthUrl = string.Empty;
        /// <summary>
        /// Url:redirect if not authorized, params: Adding authorized roles 
        /// </summary>
        /// <returns></returns>
        public AuthorizeUserAttribute(string url,params object[] roles)
        {
            if (roles.Any(r => r.GetType().BaseType != typeof(Enum)))
                throw new ArgumentException("roles");
            UnAuthUrl = url;
            this.Roles = string.Join(",", roles.Select(r => Enum.GetName(typeof(AppRole), r).ToString().Replace("_", " ")));
           
        }
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {   
            if (filterContext.HttpContext.Request.IsAuthenticated)
            {
                string authUrl = UnAuthUrl;
                if (String.IsNullOrEmpty(authUrl))
                    authUrl = "/Home/";
                if (!String.IsNullOrEmpty(authUrl))
                    filterContext.HttpContext.Response.Redirect(authUrl);
            }
            base.HandleUnauthorizedRequest(filterContext);
        }
    }
}