using ShoppingStore.Infrastructure.Identity.IdentityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;

namespace ShoppingStore.Data.Identity.Providers
{
    public class ExtendedClaimsProvider
    {

        public static IEnumerable<Claim> GetClaims(AppUser user)
        {
            List<Claim> claims = new List<Claim>();

            var daysInWork = (DateTime.Now.Date - user.JoinDate).TotalSeconds;

            if (daysInWork > 100)
            {
                claims.Add(new Claim("FTE", "1"));
            }
            else
            {
                claims.Add(new Claim("FTE", "0"));
            }

            return claims;
        }


    }
}