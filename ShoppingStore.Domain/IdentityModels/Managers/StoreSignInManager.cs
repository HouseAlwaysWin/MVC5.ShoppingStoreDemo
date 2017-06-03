using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingStore.Domain.IdentityModels.Managers
{
    public class StoreSignInManager : SignInManager<StoreUser, string>
    {
        public StoreSignInManager(
            StoreUserManager userMgr, IAuthenticationManager authMgr) :
            base(userMgr, authMgr)
        {
        }

        public override Task<ClaimsIdentity> CreateUserIdentityAsync(
            StoreUser user)
        {
            return user.GenerateUserIdentityAsync(
                (StoreUserManager)UserManager, DefaultAuthenticationTypes.ApplicationCookie);
        }

        public static StoreSignInManager Create(
            IdentityFactoryOptions<StoreSignInManager> options,
            IOwinContext context)
        {
            return new StoreSignInManager(
                context.GetUserManager<StoreUserManager>(),
                context.Authentication);
        }
    }
}
