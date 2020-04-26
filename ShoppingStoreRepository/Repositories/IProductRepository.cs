using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingStoreRepository.Repositories
{
    public interface IProductRepository<T> : IBaseRepository<T> where T : class
    {

    }
}
