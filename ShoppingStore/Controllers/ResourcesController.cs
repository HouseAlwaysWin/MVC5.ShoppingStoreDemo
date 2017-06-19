using Newtonsoft.Json;
using ShoppingStore.Infrastructure.LanguagesResources;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Resources;
using System.Web;
using System.Web.Caching;
using System.Web.Mvc;

namespace ShoppingStore.Controllers
{
    public class ResourcesController : Controller
    {
        // GET: Resources
        [HttpGet]
        [AllowAnonymous]
        public ActionResult GetResources()
        {

            HttpCookie cookie = Request.Cookies["culture"];
            if (cookie == null)
            {
                var userLang = Request.UserLanguages;
                cookie = new HttpCookie("culture");
                cookie.Value = userLang[0];
                Response.Cookies.Add(cookie);
            }

            ResourceSet resourceSet =
                Resource.ResourceManager.GetResourceSet(
                    new CultureInfo(cookie.Value), true, true);

            var resourceDictionary = resourceSet.Cast<DictionaryEntry>()
                .ToDictionary(r => r.Key.ToString(), r => r.Value.ToString());

            var jsonResource = JsonConvert.SerializeObject(resourceDictionary);

            return Json(jsonResource, JsonRequestBehavior.AllowGet);
        }
    }
}