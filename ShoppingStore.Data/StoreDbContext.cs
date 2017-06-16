using Microsoft.AspNet.Identity.EntityFramework;
using ShoppingStore.Domain.IdentityModels;
using ShoppingStore.Domain.Models;
using System.Data.Entity;

namespace ShoppingStore.Data
{

    public class StoreDbContext : IdentityDbContext<AppUser>
    {
        public DbSet<Product> Products { get; set; }

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