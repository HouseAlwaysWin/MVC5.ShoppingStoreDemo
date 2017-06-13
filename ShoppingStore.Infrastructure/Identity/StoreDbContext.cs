using Microsoft.AspNet.Identity.EntityFramework;
using ShoppingStore.Infrastructure.Identity.IdentityModels;

namespace ShoppingStore.Infrastructure.Identity
{

    public class StoreDbContext : IdentityDbContext<AppUser>
    {
        public StoreDbContext()
            : base("ShoppingStoreDb", throwIfV1Schema: false)
        {
        }

        public static StoreDbContext Create()
        {
            return new StoreDbContext();
        }
    }
}