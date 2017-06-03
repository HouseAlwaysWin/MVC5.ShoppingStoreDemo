using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingStore.Domain.IdentityModels.Managers
{
    public class StoreRoleManager : RoleManager<StoreRole>
    {
        public StoreRoleManager(RoleStore<StoreRole> rolestore) : base(rolestore)
        {
        }

        public static StoreRoleManager Create(
            IdentityFactoryOptions<StoreRoleManager> options,
            IOwinContext context)
        {
            return new StoreRoleManager(
                new RoleStore<StoreRole>(context.Get<StoreDbContext>()));
        }

    }
}
