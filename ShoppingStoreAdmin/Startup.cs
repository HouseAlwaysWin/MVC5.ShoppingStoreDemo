using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ShoppingStoreAdmin.Startup))]
namespace ShoppingStoreAdmin
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
