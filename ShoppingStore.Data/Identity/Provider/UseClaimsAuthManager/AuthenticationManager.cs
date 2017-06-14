using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Security.Claims;
using System.Web;

namespace ShoppingStore.Data.Identity.Providers
{
    public class AuthenticationManager : ClaimsAuthenticationManager
    {
        public override ClaimsPrincipal Authenticate(
            string resourceName, ClaimsPrincipal incomingPrincipal)
        {

            if (incomingPrincipal == null ||
                string.IsNullOrWhiteSpace(incomingPrincipal.Identity.Name))
            {
                throw new SecurityException("Name claim missing");
            }

            ClaimsIdentity id = (ClaimsIdentity)incomingPrincipal.Identity;

            return incomingPrincipal;
        }
    }
}