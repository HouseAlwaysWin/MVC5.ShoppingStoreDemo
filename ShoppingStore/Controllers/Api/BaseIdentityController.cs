using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using ShoppingStore.Data.Identity;
using ShoppingStore.Data.Identity.IdentityManagers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ShoppingStore.Controllers.Api
{
    public class BaseIdentityController : ApiController
    {
        private ResponseResultModelFactory responseResult;

        protected AppUserManager userManager
        {
            get
            {
                return Request.GetOwinContext().GetUserManager<AppUserManager>();
            }
        }

        protected AppRoleManager roleManager
        {
            get
            {
                return Request.GetOwinContext().GetUserManager<AppRoleManager>();
            }
        }

        protected IAuthenticationManager Authentication
        {
            get { return Request.GetOwinContext().Authentication; }
        }

        protected ResponseResultModelFactory ResponseResult
        {
            get
            {
                if (responseResult == null)
                {
                    responseResult =
                        new ResponseResultModelFactory(this.Request, userManager);
                }
                return responseResult;
            }
        }

        protected IHttpActionResult GetErrorResult(IdentityResult result)
        {
            if (result == null)
            {
                return InternalServerError();
            }

            if (!result.Succeeded)
            {
                if (result.Errors != null)
                {
                    foreach (string error in result.Errors)
                    {
                        ModelState.AddModelError("", error);
                    }
                }

                if (ModelState.IsValid)
                {
                    // No ModelState errors are available to send, so just return an empty BadRequest.
                    return BadRequest();
                }

                return BadRequest(ModelState);
            }

            return null;
        }


    }
}
