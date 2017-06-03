using Microsoft.AspNet.Identity.EntityFramework;

namespace ShoppingStore.Domain.IdentityModels
{

    public class StoreDbContext : IdentityDbContext<StoreUser>
    {
        public StoreDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static StoreDbContext Create()
        {
            return new StoreDbContext();
        }
    }
}