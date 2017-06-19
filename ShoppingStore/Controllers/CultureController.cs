using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShoppingStore.Controllers
{
    public class CultureController : Controller
    {
        // GET: Culture
        public ActionResult SetCulture(string culture)
        {

            // Save culture in a cookie
            HttpCookie cookie = Request.Cookies["culture"];
            if (cookie == null)
            {
                cookie = new HttpCookie("culture");
                cookie.Value = culture;
                Response.Cookies.Add(cookie);
            }
            cookie.Value = culture;
            Response.Cookies.Add(cookie);

            return Redirect(Request.UrlReferrer.PathAndQuery);
        }



    }
}