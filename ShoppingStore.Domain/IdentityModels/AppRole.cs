using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingStore.Domain.IdentityModels
{

    public class AppRole : IdentityRole
    {
        public AppRole() : base()
        {
        }

        public AppRole(string name) : base(name)
        {

        }


    }
}
