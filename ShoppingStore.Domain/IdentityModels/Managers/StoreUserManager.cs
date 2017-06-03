using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;

namespace ShoppingStore.Domain.IdentityModels.Managers
{
    // Configure the application user manager used in this application. UserManager is defined in ASP.NET Identity and is used by the application.

    public class StoreUserManager : UserManager<StoreUser>
    {
        public StoreUserManager(IUserStore<StoreUser> store)
            : base(store)
        {
        }

        public static StoreUserManager Create(IdentityFactoryOptions<StoreUserManager> options, IOwinContext context)
        {
            var manager = new StoreUserManager(new UserStore<StoreUser>(context.Get<StoreDbContext>()));
            // Configure validation logic for user names
            manager.UserValidator = new UserValidator<StoreUser>(manager)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = true
            };
            // Configure validation logic for passwords
            manager.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 6,
                RequireNonLetterOrDigit = true,
                RequireDigit = true,
                RequireLowercase = true,
                RequireUppercase = true,
            };
            var dataProtectionProvider = options.DataProtectionProvider;
            if (dataProtectionProvider != null)
            {
                manager.UserTokenProvider = new DataProtectorTokenProvider<StoreUser>(dataProtectionProvider.Create("ASP.NET Identity"));
            }
            return manager;
        }
    }
}
