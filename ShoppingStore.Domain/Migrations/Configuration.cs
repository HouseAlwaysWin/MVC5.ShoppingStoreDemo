namespace ShoppingStore.Domain.Migrations
{
    using IdentityModels;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<ShoppingStore.Domain.IdentityModels.StoreDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(ShoppingStore.Domain.IdentityModels.StoreDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

            var manager = new UserManager<AppUser>(
                new UserStore<AppUser>(new StoreDbContext()));

            var user = new AppUser()
            {
                UserName = "SuperUser",
                Email = "martinwang7963@gmail.com",
                EmailConfirmed = true,
                FirstName = "Martin",
                LastName = "Wang",
                Level = 1,
                JoinDate = DateTime.Now
            };

            manager.Create(user, "secret");
        }
    }
}
