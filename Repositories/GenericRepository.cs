using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace audit.Repositories
{
    /// <summary>
    /// Generic repository
    /// </summary>
    /// <typeparam name="T">entity type</typeparam>
    public class GenericRepository<T> : IGenericRepository<T>
        where T : class
    {
        protected IMongoDatabase _context;
        protected string _type;

        public GenericRepository(IMongoDatabase context)
        {
            Type typeParameterType = typeof(T);
            _type = $"{typeParameterType.Name.ToString()}s";
            _context = context;
        }

        #region CRUD
        public virtual async Task<IEnumerable<T>> Read(FilterDefinition<T> filter)
        {
            var cursor = await _context.GetCollection<T>(_type).FindAsync(filter);
            IEnumerable<T> list = null;
            while (await cursor.MoveNextAsync())
            {
                list = cursor.Current;
            }
            return list;
        }

        public virtual async Task<T> Create(T entity)
        {
            await _context.GetCollection<T>(_type).InsertOneAsync(entity);
            return entity;
        }

        public virtual async Task<T> Delete(FilterDefinition<T> filter)
        {
            return await _context.GetCollection<T>(_type).FindOneAndDeleteAsync(filter);
        }

        public virtual async Task<T> Update(FilterDefinition<T> filter, UpdateDefinition<T> update)
        {
            return await _context.GetCollection<T>(_type).FindOneAndUpdateAsync(filter, update);
        }
        #endregion

        #region extensions (didn't test)
        public virtual async Task<IEnumerable<T>> CreateMany(IEnumerable<T> entities)
        {
            await _context.GetCollection<T>(_type).InsertManyAsync(entities);
            return entities;
        }

        public virtual async Task<long> DeleteMany(FilterDefinition<T> filter)
        {
            var deleteResult = await _context.GetCollection<T>(_type).DeleteManyAsync(filter);
            return deleteResult.DeletedCount;
        }

        public virtual async Task<long> UpdateMany(FilterDefinition<T> filter, UpdateDefinition<T> update)
        {
            var updateResult = await _context.GetCollection<T>(_type).UpdateManyAsync(filter, update);
            return updateResult.ModifiedCount;
        }
        #endregion
    }
}
