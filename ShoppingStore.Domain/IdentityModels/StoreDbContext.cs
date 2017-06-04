using Microsoft.AspNet.Identity.EntityFramework;

namespace ShoppingStore.Domain.IdentityModels
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