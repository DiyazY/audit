using audit.Entities;
using audit.Repositories;
using audit.Services.Interfaces;
using audit.Utils;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace audit.Services.Implementations
{
    public class AuditService: IAuditService
    {
        private readonly IGenericRepository<AuditObject> _repository;
        public AuditService(IGenericRepository<AuditObject> repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Create new object or change
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<AuditObject> SetAuditObject(AuditObject entity)
        {
            try
            {
                if (entity == null)
                {
                    throw new ArgumentNullException("Entity is null!");
                }
                if (entity?.Id == null || Guid.Empty == entity?.Id)
                {
                    throw new ArgumentNullException("Entity's id isn't correct!");
                }
                if (entity?.Body == null || entity?.Body?.ToString() == AuditObject.EmptyBody)
                {
                    throw new ArgumentNullException("Body isn't correct!!");
                }

                var filter = Builders<AuditObject>.Filter.Eq("_id", entity?.Id);
                var auditObject = (await _repository.Read(filter));
                if (auditObject == null || auditObject.Count() == 0)
                {
                    return await _repository.Create(entity);
                }
                else
                {
                    var body = auditObject?.FirstOrDefault()?.Body;
                    var diff = Diff.Get(body?.ToJson(), entity?.Body?.ToJson());

                    //var patch = Diff.Patch(body.ToJson(), diff);
                    //var unpatch = Diff.Unpatch(entity.GetBsonBody().ToJson(), diff);

                    if (!String.IsNullOrEmpty(diff))
                    {
                        var update = Builders<AuditObject>.Update.Push<BsonDocument>("_changes", BsonDocument.Parse(diff));
                        return await _repository.Update(filter, update);
                    }
                    return entity;
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"AuditService-SetAuditObject: {ex.Message} {ex.StackTrace}");
            }
        }

        /// <summary>
        /// Update audit objects by filter
        /// </summary>
        /// <param name="filter">Filter Definition</param>
        /// <param name="update">Update Definition</param>
        /// <returns></returns>
        public async Task<AuditObject> Update(FilterDefinition<AuditObject> filter,  UpdateDefinition<AuditObject> update)
        {
            if (update == null)
            {
                throw new ArgumentNullException("update is null");
            }
            if (filter == null)
            {
                throw new ArgumentNullException("filter is null");
            }
            var result = await _repository.Update(filter, update);
            return result;
        }

        /// <summary>
        /// Delete audit object by filter
        /// </summary>
        /// <param name="filter">Filter Definition</param>
        /// <returns></returns>
        public async Task<AuditObject> Delete(FilterDefinition<AuditObject> filter)
        {
            if (filter == null)
            {
                throw new ArgumentNullException("filter is null");
            }
            var result = await _repository.Delete(filter);
            return result;
        }

        /// <summary>
        /// Read audit objects from collection by filter
        /// </summary>
        /// <param name="filter">Filter Definition</param>
        /// <returns></returns>
        public async Task<IEnumerable<AuditObject>> Read(FilterDefinition<AuditObject> filter)
        {
            if (filter == null)
            {
                throw new ArgumentNullException("filter is null");
            }
            var result = await _repository.Read(filter);
            return result;
        }
    }
}

