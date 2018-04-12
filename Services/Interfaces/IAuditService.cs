using audit.Entities;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace audit.Services.Interfaces
{
    public interface IAuditService
    {
        #region CRUD
        Task<AuditObject> SetAuditObject(AuditObject entity);
        Task<AuditObject> Update(FilterDefinition<AuditObject> filter, UpdateDefinition<AuditObject> update);
        Task<AuditObject> Delete(FilterDefinition<AuditObject> filter);
        Task<IEnumerable<AuditObject>> Read(FilterDefinition<AuditObject> filter);
        //#endregion
        //#region extensions
        //Task<IEnumerable<AuditObject>> CreateMany(IEnumerable<AuditObject> entities);
        //Task<System.Int64> UpdateMany(FilterDefinition<AuditObject> filter, UpdateDefinition<AuditObject> update);
        //Task<System.Int64> DeleteMany(FilterDefinition<AuditObject> filter);
        #endregion
    }
}
