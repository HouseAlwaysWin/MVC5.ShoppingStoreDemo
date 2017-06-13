using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using ShoppingStore.Infrastructure.Identity.IdentityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingStore.Infrastructure.Identity.IdentityManagers
{
    public class AppSignInManager : SignInManager<AppUser, string>
    {
        public AppSignInManager(
            AppUserManager userMgr, IAuthenticationManager authMgr) :
            base(userMgr, authMgr)
        {
        }

        public override Task<ClaimsIdentity> CreateUserIdentityAsync(
            AppUser user)
        {
            return user.GenerateUserIdentityAsync(
                (AppUserManager)UserManager,
                DefaultAuthenticationTypes.ApplicationCookie);
        }

        public static AppSignInManager Create(
            IdentityFactoryOptions<AppSignInManager> options,
            IOwinContext context)
        {
            return new AppSignInManager(
                context.GetUserManager<AppUserManager>(),
                context.Authentication);
        }
    }
}
