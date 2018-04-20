using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace audit.Repositories
{
    public interface IGenericRepository<T> where T : class
    {
        #region CRUD
        Task<IEnumerable<T>> Read(FilterDefinition<T> filter);
        Task<T> Create(T entity);
        Task<T> Delete(FilterDefinition<T> filter);
        Task<T> Update(FilterDefinition<T> filter, UpdateDefinition<T> update);
        #endregion
        #region extensions
        Task<IEnumerable<T>> CreateMany(IEnumerable<T> entities);
        Task<long> DeleteMany(FilterDefinition<T> filter);
        Task<long> UpdateMany(FilterDefinition<T> filter, UpdateDefinition<T> update);
        #endregion
    }
}
