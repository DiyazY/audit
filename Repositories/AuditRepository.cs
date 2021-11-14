namespace audit.Repositories;

using MongoDB.Driver;
using MongoDB.Bson;
using System.Collections.Generic;

public sealed class AuditRepository : IAuditRepository
{
    private IMongoDatabase _context;
    private const string _collection = "auditables";

    public AuditRepository(IMongoDatabase context)
    {
        ArgumentNullException.ThrowIfNull(context);
        _context = context;
    }

    public async Task<AuditObject> GetAuditObject(Guid id)
    {
        var filter = Builders<AuditObject>.Filter.Eq("_id", id);
        var cursor = await _context.GetCollection<AuditObject>(_collection).FindAsync(filter);
        IEnumerable<AuditObject> list = new List<AuditObject>(); // check new features of C#10. empty list or something like that
        while (await cursor.MoveNextAsync())
        {
            list = cursor.Current;
        }
        var auditObject = list.FirstOrDefault();
        return auditObject;
    }

    public async Task SaveAuditObject(AuditObject auditObject)
    {
        await _context.GetCollection<AuditObject>(_collection).InsertOneAsync(auditObject);
    }

    public async Task UpdateAuditObject(Guid id, BsonDocument newLastBody, string diff) // use C#10/.NET6 features
    {
        ///var filter = Builders<AuditObject>.Filter.Eq("_id", auditObject.Id); TODO: why not filter
        var update = Builders<AuditObject>.Update
            .Push<BsonDocument>("_changes", BsonDocument.Parse(diff))
            .Set("_lastBody", newLastBody); // TODO fix it!!!!!!!!!!!!
        await _context
            .GetCollection<AuditObject>(_collection)
            .UpdateOneAsync<AuditObject>(p=>p.Id == id, update);
    }
}
