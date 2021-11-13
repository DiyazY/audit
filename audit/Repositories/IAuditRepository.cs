namespace audit.Repositories;

using audit.Models;
using MongoDB.Driver;
using System.Threading.Tasks;
using MongoDB.Bson;

public interface IAuditRepository
{
    Task<AuditObject> GetAuditObject(Guid id);
    Task SaveAuditObject(AuditObject auditObject);
    Task UpdateAuditObject(Guid id, BsonDocument newLastBody, string diff);
}
