namespace ShoppingStore.Data.Migrations
{
    using Domain.IdentityModels;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<StoreDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(StoreDbContext context)
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

            var userManager = new UserManager<AppUser>(
                new UserStore<AppUser>(new StoreDbContext()));

            var roleManager = new RoleManager<AppRole>(
                new RoleStore<AppRole>(new StoreDbContext()));

            var user = new AppUser
            {
                UserName = "SuperUser",
                Email = "SuperUser@example.com",
                EmailConfirmed = true,
                Level = 1,
                JoinDate = DateTime.UtcNow
            };

            userManager.Create(user, "secret");

            if (roleManager.Roles.Count() == 0)
            {
                roleManager.Create(new AppRole { Name = "SuperUser" });
                roleManager.Create(new AppRole { Name = "Admin" });
                roleManager.Create(new AppRole { Name = "User" });
            }

            var superUser = userManager.FindByName("SuperUser");

            userManager.AddToRole(superUser.Id, "SuperUser");


        }
    }
}
