using ShoppingStoreDomain.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingStoreRepository.Repositories
{
    internal class ProductRepository<T> : BaseRepository<T>, IProductRepository<T> where T : class
    {
        public ProductRepository(IDbTransaction transaction) : base(transaction)
        {
        }
        public ProductRepository(IDbTransaction transaction, string tableName) : base(transaction, tableName)
        {
        }
    }
}
