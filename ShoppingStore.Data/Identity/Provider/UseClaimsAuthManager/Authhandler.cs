using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Security.Claims;
using System.IdentityModel.Configuration;

namespace ShoppingStore.Data.Identity.Providers
{
    public class AuthHandler : DelegatingHandler
    {
        protected override async Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name,"Martin")
            };

            var id = new ClaimsIdentity(claims, "house");

            var principal = new ClaimsPrincipal(new[] { id });

            var config = new IdentityConfiguration();

            var newPrincipal = config.ClaimsAuthenticationManager
                .Authenticate(request.RequestUri.ToString(), principal);

            Thread.CurrentPrincipal = newPrincipal;

            if (HttpContext.Current != null)
            {
                HttpContext.Current.User = newPrincipal;

            }

            return await base.SendAsync(request, cancellationToken);

        }
    }

}
