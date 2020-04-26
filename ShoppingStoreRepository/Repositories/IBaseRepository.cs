using System.Collections.Generic;
using System.Threading.Tasks;
using ShoppingStoreDomain;

namespace ShoppingStoreRepository.Repositories
{
    public interface IBaseRepository<T> where T : class
    {
        void Create(T model);
        Task CreateAsync(T model);
        void Delete(string id, string key);
        void Delete<TId>(TId id, string key);
        Task DeleteAsync(string id, string key);
        IEnumerable<T> GetAll(string id, int? num);
        Task<IEnumerable<T>> GetAllAsync(string id, int? num);
        PageModel<T> GetAllByPage(int currentPage, int itemsPerPage);
        Task<PageModel<T>> GetAllByPageAsync(int currentPage, int itemsPerPage);
        PageModel<T> GetListByPage<TId>(TId id, string key, int currentPage, int itemsPerPage);
        Task<PageModel<T>> GetListByPageAsync<TId>(TId id, string key, int currentPage, int itemsPerPage);
        void Update(T model, string key);
        Task UpdateAsync(T model, string key);
    }
}