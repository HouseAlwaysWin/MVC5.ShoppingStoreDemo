using ShoppingStore.Domain.IdentityModels;
using ShoppingStore.Domain.IdentityModels.Managers;
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
    public class ModelFactory
    {
        private UrlHelper urlHelper;
        private AppUserManager userManager;

        public ModelFactory(
            HttpRequestMessage request,
            AppUserManager appUserManager)
        {
            urlHelper = new UrlHelper(request);
            userManager = appUserManager;
        }

        public UserReturnModel Create(AppUser appUser)
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
}
