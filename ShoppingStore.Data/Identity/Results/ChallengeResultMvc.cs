using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ShoppingStore.Data.Identity.Results
{
    public class ChallengeResultMvc : HttpUnauthorizedResult
    {
        private const string XsrfKey = "XsrfId";
        public string LoginProvider { get; set; }
        public string RedirectUri { get; set; }
        public string UserId { get; set; }

        public ChallengeResultMvc(string provider, string redirectUri)
           : this(provider, redirectUri, null)
        {
        }

        public ChallengeResultMvc(
            string provider, string returnUri, string userId)
        {
            provider = LoginProvider;
            returnUri = RedirectUri;
            userId = UserId;
        }

        public override void ExecuteResult(ControllerContext context)
        {
            var properties = new AuthenticationProperties
            {
                RedirectUri = RedirectUri
            };
            if (UserId != null)
            {
                properties.Dictionary[XsrfKey] = UserId;
            }

            context.HttpContext.GetOwinContext()
                .Authentication.Challenge(properties, LoginProvider);
        }


    }
}
