using ShoppingStore.Infrastructure.Identity.IdentityManagers;
using ShoppingStore.Infrastructure.Identity.IdentityModels;
using ShoppingStore.Infrastructure.IdentityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Routing;

namespace ShoppingStore.Domain.Infrastructure
{
    public class ResponseResultModelFactory
    {
        private UrlHelper urlHelper;
        private AppUserManager userManager;

        public ResponseResultModelFactory(
            HttpRequestMessage request,
            AppUserManager appUserManager)
        {
            urlHelper = new UrlHelper(request);
            userManager = appUserManager;
        }

        public UserReturnModel ShowUser(AppUser appUser)
        {
            return new UserReturnModel
            {
                Url = urlHelper.Link("GetUserById", new { id = appUser.Id }),
                Id = appUser.Id,
                UserName = appUser.UserName,
                FullName = string.Format("{0} {1}", appUser.FirstName, appUser.LastName),
                Email = appUser.Email,
                EmailConfirmed = appUser.EmailConfirmed,
                Level = appUser.Level,
                JoinDate = appUser.JoinDate,
                Roles = userManager.GetRolesAsync(appUser.Id).Result,
                Claims = userManager.GetClaimsAsync(appUser.Id).Result
            };
        }

        public RoleReturnModel ShowRole(AppRole role)
        {
            return new RoleReturnModel
            {
                Url = urlHelper.Link("GetRoleById", new { id = role.Id }),
                Name = role.Name,
                Id = role.Id
            };
        }


    }

    public class UserReturnModel
    {
        public string Url { get; set; }
        public string Id { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public bool EmailConfirmed { get; set; }
        public int Level { get; set; }
        public DateTime JoinDate { get; set; }
        public IList<string> Roles { get; set; }
        public IList<Claim> Claims { get; set; }
    }

    public class RoleReturnModel
    {
        public string Url { get; set; }
        public string Id { get; set; }
        public string Name { get; set; }
    }
}
