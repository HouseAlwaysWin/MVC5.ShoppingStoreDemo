using ShoppingStoreDomain;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using System.Reflection;
using System.ComponentModel;

namespace ShoppingStoreRepository.Repositories
{
    internal abstract class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        protected IDbTransaction Transaction { get; private set; }
        protected IDbConnection Connection { get { return Transaction.Connection; } }
        private readonly string _tableName;
        private IEnumerable<PropertyInfo> GetProperties => typeof(T).GetProperties();
        public BaseRepository(IDbTransaction transaction)
        {
            _tableName = typeof(T).FullName;
            Transaction = transaction;
        }

        public BaseRepository(IDbTransaction transaction, string tableName)
        {
            _tableName = tableName;
            Transaction = transaction;
        }

        public virtual void Create(T model)
        {
            var insertQuery = new StringBuilder($"INSERT INTO {_tableName} (");
            var props = GenerateListOfProperties(GetProperties);
            foreach (var prop in props)
            {
                insertQuery.Append($"[{prop}],");
            }
            insertQuery.Remove(insertQuery.Length - 1, 1).Append(") VALUES (");
            foreach (var prop in props)
            {
                insertQuery.Append($"@{prop},");
            }
            insertQuery.Remove(insertQuery.Length - 1, 1).Append(")");

            Connection.Execute(insertQuery.ToString(), model, transaction: Transaction);
        }

        public virtual async Task CreateAsync(T model)
        {
            var insertQuery = new StringBuilder($"INSERT INTO {_tableName} (");
            var props = GenerateListOfProperties(GetProperties);
            foreach (var prop in props)
            {
                insertQuery.Append($"[{prop}],");
            }
            insertQuery.Remove(insertQuery.Length - 1, 1).Append(") VALUES (");
            foreach (var prop in props)
            {
                insertQuery.Append($"@{prop},");
            }
            insertQuery.Remove(insertQuery.Length - 1, 1).Append(")");

            await Connection.ExecuteAsync(insertQuery.ToString(), model, transaction: Transaction);
        }

        public virtual void Update(T model, string key)
        {
            var updateQuery = new StringBuilder($"UPDATE {_tableName} SET ");
            var props = GenerateListOfProperties(GetProperties);

            string pk = $"{_tableName}ID";
            if (!string.IsNullOrEmpty(key))
            {
                pk = key;
            }
            foreach (var prop in props)
            {
                if (!prop.Equals(pk))
                {
                    updateQuery.Append($"{prop}=@{prop},");
                }
            }
            updateQuery.Remove(updateQuery.Length - 1, 1);
            updateQuery.Append($" WHERE {pk}=@{pk}");

            Connection.Query<T>(updateQuery.ToString(), model, transaction: Transaction);
        }

        public virtual async Task UpdateAsync(T model, string key)
        {
            var updateQuery = new StringBuilder($"UPDATE {_tableName} SET ");
            var props = GenerateListOfProperties(GetProperties);

            string pk = $"{_tableName}ID";
            if (!string.IsNullOrEmpty(key))
            {
                pk = key;
            }
            foreach (var prop in props)
            {
                if (!prop.Equals(pk))
                {
                    updateQuery.Append($"{prop}=@{prop},");
                }
            }
            updateQuery.Remove(updateQuery.Length - 1, 1);
            updateQuery.Append($" WHERE {pk}=@{pk}");

            await Connection.QueryAsync<T>(updateQuery.ToString(), model, transaction: Transaction);
        }

        public virtual IEnumerable<T> GetAll(string id, int? num)
        {
            string topQuery = string.Empty;
            if (num != null)
            {
                topQuery = $"TOP({num})";
            }
            return Connection.Query<T>($"SELECT {topQuery} * FROM {_tableName}", transaction: Transaction);
        }

        public virtual async Task<IEnumerable<T>> GetAllAsync(string id, int? num)
        {
            string topQuery = string.Empty;
            if (num != null)
            {
                topQuery = $"TOP({num})";
            }
            var result = await Connection.QueryAsync<T>($"SELECT {topQuery} * FROM {_tableName}", transaction: Transaction);
            return result;
        }

        public virtual PageModel<T> GetAllByPage(int currentPage, int itemsPerPage)
        {
            StringBuilder getQuery = new StringBuilder(@"
            DECLARE @Start int = (@CurrentPage - 1) * @ItemsPerPages
                    SELECT COUNT(*) 
                        FROM ");
            getQuery.Append(_tableName);
            getQuery.Append(" SELECT ");
            var props = GenerateListOfProperties(GetProperties);
            for (int i = 0; i < props.Count; i++)
            {
                getQuery.Append("[");
                getQuery.Append(props[i]);
                getQuery.Append("]");
                if (i != props.Count - 1)
                {
                    getQuery.Append(",");
                }
            }
            getQuery.Append(" FROM ");
            getQuery.Append(_tableName);
            getQuery.Append(@" OFFSET @Start ROWS
                               FETCH NEXT @ItemsPerPages ROWS ONLY");
            var sqlSring = getQuery.ToString();
            var sqlResult = Connection.QueryMultiple(sqlSring, transaction: Transaction);
            var result = new PageModel<T>
            {
                TotalCount = sqlResult.ReadSingle<int>(),
                Data = sqlResult.Read<T>()
            };
            return result;
        }

        public virtual async Task<PageModel<T>> GetAllByPageAsync(int currentPage, int itemsPerPage)
        {
            StringBuilder getQuery = new StringBuilder(@"
            DECLARE @Start int = (@CurrentPage - 1) * @ItemsPerPages
                    SELECT COUNT(*) 
                        FROM ");
            getQuery.Append(_tableName);
            getQuery.Append(" SELECT ");
            var props = GenerateListOfProperties(GetProperties);
            for (int i = 0; i < props.Count; i++)
            {
                getQuery.Append("[");
                getQuery.Append(props[i]);
                getQuery.Append("]");
                if (i != props.Count - 1)
                {
                    getQuery.Append(",");
                }
            }
            getQuery.Append(" FROM ");
            getQuery.Append(_tableName);
            getQuery.Append(@" OFFSET @Start ROWS
                               FETCH NEXT @ItemsPerPages ROWS ONLY");
            var sqlSring = getQuery.ToString();
            var sqlResult = await Connection.QueryMultipleAsync(sqlSring, transaction: Transaction);
            var result = new PageModel<T>
            {
                TotalCount = sqlResult.ReadSingle<int>(),
                Data = sqlResult.Read<T>()
            };
            return result;
        }

        public virtual PageModel<T> GetListByPage<TId>(TId id, string key, int currentPage, int itemsPerPage)
        {
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentNullException("key");
            }

            var props = GenerateListOfProperties(GetProperties);
            string pk = $"{_tableName}ID";
            if (!string.IsNullOrEmpty(key))
            {
                pk = key;
            }

            if (!props.Contains(key))
            {
                throw new ArgumentException("key is not inside the model");
            }

            StringBuilder getQuery = new StringBuilder(@"
            DECLARE @Start int = (@CurrentPage - 1) * @ItemsPerPages
                    SELECT COUNT(*) 
                        FROM ");
            getQuery.Append(_tableName);
            getQuery.Append(" WHERE ");
            getQuery.Append(pk);
            getQuery.Append(" = @");
            getQuery.Append(id);

            getQuery.Append(" SELECT ");
            for (int i = 0; i < props.Count; i++)
            {
                getQuery.Append("[");
                getQuery.Append(props[i]);
                getQuery.Append("]");
                if (i != props.Count - 1)
                {
                    getQuery.Append(",");
                }
            }
            getQuery.Append(" FROM ");
            getQuery.Append(_tableName);
            getQuery.Append(" WHERE ");
            getQuery.Append(pk);
            getQuery.Append(" = @");
            getQuery.Append(id);
            getQuery.Append(@" OFFSET @Start ROWS
                               FETCH NEXT @ItemsPerPages ROWS ONLY");
            var sqlSring = getQuery.ToString();
            var sqlResult = Connection.QueryMultiple(sqlSring, transaction: Transaction);
            var result = new PageModel<T>
            {
                TotalCount = sqlResult.ReadSingle<int>(),
                Data = sqlResult.Read<T>()
            };
            return result;
        }

        public virtual async Task<PageModel<T>> GetListByPageAsync<TId>(TId id, string key, int currentPage, int itemsPerPage)
        {
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentNullException("key");
            }

            var props = GenerateListOfProperties(GetProperties);
            string pk = $"{_tableName}ID";
            if (!string.IsNullOrEmpty(key))
            {
                pk = key;
            }

            if (!props.Contains(key))
            {
                throw new ArgumentException("key is not inside the model");
            }

            StringBuilder getQuery = new StringBuilder(@"
            DECLARE @Start int = (@CurrentPage - 1) * @ItemsPerPages
                    SELECT COUNT(*) 
                        FROM ");
            getQuery.Append(_tableName);
            getQuery.Append(" WHERE ");
            getQuery.Append(pk);
            getQuery.Append(" = @");
            getQuery.Append(id);

            getQuery.Append(" SELECT ");
            for (int i = 0; i < props.Count; i++)
            {
                getQuery.Append("[");
                getQuery.Append(props[i]);
                getQuery.Append("]");
                if (i != props.Count - 1)
                {
                    getQuery.Append(",");
                }
            }
            getQuery.Append(" FROM ");
            getQuery.Append(_tableName);
            getQuery.Append(" WHERE ");
            getQuery.Append(pk);
            getQuery.Append(" = @");
            getQuery.Append(id);
            getQuery.Append(@" OFFSET @Start ROWS
                               FETCH NEXT @ItemsPerPages ROWS ONLY");
            var sqlSring = getQuery.ToString();
            var sqlResult = await Connection.QueryMultipleAsync(sqlSring, transaction: Transaction);
            var result = new PageModel<T>
            {
                TotalCount = sqlResult.ReadSingle<int>(),
                Data = sqlResult.Read<T>()
            };
            return result;
        }

        public virtual void Delete(string id, string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentNullException("key");
            }

            var updateQuery = new StringBuilder($"DELETE  {_tableName} SET ");
            var props = GenerateListOfProperties(GetProperties);

            string pk = $"{_tableName}ID";
            if (!string.IsNullOrEmpty(key))
            {
                pk = key;
            }
            if (!props.Contains(key))
            {
                throw new ArgumentException("key is not inside model");
            }

            foreach (var prop in props)
            {
                if (!prop.Equals(pk))
                {
                    updateQuery.Append($"{prop}=@{prop},");
                }
            }
            updateQuery.Remove(updateQuery.Length - 1, 1);
            updateQuery.Append($" WHERE {pk}=@{id}");
            var sqlString = updateQuery.ToString();
            Connection.Execute(sqlString, id, transaction: Transaction);
        }

        public virtual async Task DeleteAsync(string id, string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentNullException("key");
            }

            var updateQuery = new StringBuilder($"DELETE  {_tableName} SET ");
            var props = GenerateListOfProperties(GetProperties);

            string pk = $"{_tableName}ID";
            if (!string.IsNullOrEmpty(key))
            {
                pk = key;
            }
            if (!props.Contains(key))
            {
                throw new ArgumentException("key is not inside model");
            }

            foreach (var prop in props)
            {
                if (!prop.Equals(pk))
                {
                    updateQuery.Append($"{prop}=@{prop},");
                }
            }
            updateQuery.Remove(updateQuery.Length - 1, 1);
            updateQuery.Append($" WHERE {pk}=@{id}");
            var sqlString = updateQuery.ToString();
            await Connection.ExecuteAsync(sqlString, id);
        }

        public virtual void Delete<TId>(TId id, string key)
        {
            var updateQuery = new StringBuilder($"DELETE  {_tableName} SET ");
            var props = GenerateListOfProperties(GetProperties);

            string pk = $"{_tableName}ID";
            if (!string.IsNullOrEmpty(key))
            {
                pk = key;
            }
            foreach (var prop in props)
            {
                if (!prop.Equals(pk))
                {
                    updateQuery.Append($"{prop}=@{prop},");
                }
            }
            updateQuery.Remove(updateQuery.Length - 1, 1);
            updateQuery.Append($" WHERE {pk}=@{id}");
        }

        private static List<string> GenerateListOfProperties(IEnumerable<PropertyInfo> listOfProperties)
        {
            return (from prop in listOfProperties
                    let attributes = prop.GetCustomAttributes(typeof(DescriptionAttribute), false)
                    where attributes.Length <= 0 || (attributes[0] as DescriptionAttribute)?.Description != "ignore"
                    select prop.Name).ToList();
        }


    }
}
