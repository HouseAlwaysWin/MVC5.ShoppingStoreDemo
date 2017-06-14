using ShoppingStore.Data.ViewModels;
using ShoppingStore.Domain.Entities;
using ShoppingStore.Domain.Infrastructure;
using ShoppingStore.Infractructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ShoppingStore.Controllers.Api
{
    [RoutePrefix("api/Audience")]
    public class AudienceController : ApiController
    {
        [Route("")]
        public IHttpActionResult Post(AudienceViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            Audience newAudience = AudiencesStoreFactory.AddAudience(model.Name);
            return Ok<Audience>(newAudience);
        }
    }
}
