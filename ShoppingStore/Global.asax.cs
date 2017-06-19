using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace ShoppingStore
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        // Get culture
        protected void Application_BeginRequest()
        {
            var userLang = Request.UserLanguages;
            HttpCookie cookie = Request.Cookies["culture"];
            if (cookie == null)
            {
                cookie = new HttpCookie("culture");
                cookie.Value = userLang[0];
                Response.Cookies.Add(cookie);
            }
            Thread.CurrentThread.CurrentUICulture =
                CultureInfo.CreateSpecificCulture(cookie.Value);
        }
    }
}
