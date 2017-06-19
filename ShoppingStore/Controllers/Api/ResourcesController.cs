using Newtonsoft.Json;
using ShoppingStore.Infrastructure.LanguagesResources;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Resources;
using System.Web.Http;

namespace ShoppingStore.Controllers.Api
{

    public class ResourcesController : ApiController
    {

        public IHttpActionResult GetResources()
        {
            ResourceSet resourceSet =
                Resource.ResourceManager.GetResourceSet(
                    CultureInfo.CurrentUICulture, true, true);

            var resourceDictionary = resourceSet.Cast<DictionaryEntry>()
                .ToDictionary(r => r.Key.ToString(), r => r.Value.ToString());


            var jsonResource = JsonConvert.SerializeObject(resourceDictionary);

            return Ok(jsonResource);
        }
    }
}
