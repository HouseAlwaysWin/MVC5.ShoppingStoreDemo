using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;

using System.IdentityModel.Services;
using System.Security.Permissions;

namespace ShoppingStore.Controllers
{
    [Authorize(Roles = "IncidentResolvers")]
    [RoutePrefix("api/value")]
    public class ValuesController : ApiController
    {
        // GET api/values
        //[ClaimsAuthorization(ClaimType = "FTE", ClaimValue = "1")]
        [ClaimsPrincipalPermission(SecurityAction.Demand,
            Operation = "Get", Resource = "Value")]
        [Route("")]
        public IHttpActionResult Get()
        {
            return Ok();
        }

        [HttpPut]
        [Authorize(Roles = "IncidentResolvers")]
        [Route("refund/{orderId}")]
        // GET api/values/5
        public IHttpActionResult Get([FromUri]string orderId)
        {
            return Ok();
        }

        // POST api/values
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
