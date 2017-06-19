using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ShoppingStore.Infrastructure.Filters
{
    public class CultureAttribute : FilterAttribute, IActionFilter
    {

        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
            var userLang = HttpContext.Current.Request.UserLanguages;

            // Attempt to read the culture cookie from Request
            HttpCookie cultureCookie = HttpContext.Current.Request.Cookies["culture"];
            if (cultureCookie == null)
            {
                cultureCookie = new HttpCookie("culture");
                cultureCookie.Value = userLang[0];
                HttpContext.Current.Response.Cookies.Add(cultureCookie);
            }
            Thread.CurrentThread.CurrentCulture = new CultureInfo(cultureCookie.Value);
            Thread.CurrentThread.CurrentUICulture = Thread.CurrentThread.CurrentCulture;
        }

        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
        }
    }
}
