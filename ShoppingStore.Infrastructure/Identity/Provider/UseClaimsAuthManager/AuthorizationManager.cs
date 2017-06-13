using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;

namespace ShoppingStore.Infrastructure.Providers
{
    public class AuthorizationManager : ClaimsAuthorizationManager
    {
        public override bool CheckAccess(AuthorizationContext context)
        {
            var resource = context.Resource;
            var action = context.Action;

            string resourceName =
                resource.First(c => c.Type == ClaimTypes.Name).Value;

            string actionName =
                action.First(c => c.Type == ClaimTypes.Name).Value;

            if (actionName == "Get" &&
                resourceName == "Value")
            {
                ClaimsIdentity id =
                    (context.Principal.Identities as ClaimsIdentity);

                if (!id.IsAuthenticated)
                {
                    return false;
                }
                var claims = id.Claims;
                string FTAClaim = resource.First(c => c.Type == "FTA").Value;

                if (claims.Any(c => c.Type == "FTA" && c.Value == "1"))
                {
                    return true;
                }
            }

            return false;
        }
    }
}