using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingStore.Domain.IdentityModels
{

    public class StoreRole : IdentityRole
    {
        public StoreRole() : base()
        {
        }

        public StoreRole(string name) : base(name)
        {

        }


    }
}
