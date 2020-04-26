using ShoppingStoreDomain.Models;
using ShoppingStoreRepository.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingStoreRepository
{
    public interface IUnitOfWork : IDisposable
    {
        IProductRepository<Product> ProductRepository { get; }
        void Commit();
    }
}
