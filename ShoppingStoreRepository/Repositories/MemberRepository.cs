using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingStoreRepository.Repositories
{
    internal class MemberRepository<T> : BaseRepository<T>, IMemberRepository where T : class
    {
        public MemberRepository(IDbTransaction transaction, string tableName)
            : base(transaction, tableName)
        {
        }

    }
}
