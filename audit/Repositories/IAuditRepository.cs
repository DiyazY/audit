namespace audit.Repositories;

using audit.Models;
using MongoDB.Driver;

public interface IAuditRepository
{
    Task<AuditObject> GetAuditObject(Guid id);
    Task SaveAuditObject(AuditObject auditObject);
    Task UpdateAuditObject(AuditObject auditObject, string diff);
}
