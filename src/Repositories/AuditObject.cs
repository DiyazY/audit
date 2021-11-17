namespace audit.Repositories;

using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

public sealed class AuditObject
{

    [BsonElement]
    private BsonDocument _body { get; set; }

    [BsonElement]
    private BsonDocument _lastBody { get; set; }

    [BsonElement]
    private DateTime _createdDate = DateTime.UtcNow;

    [BsonElement]
    private BsonArray _changes { get; set; } = new BsonArray();

    [BsonId]
    public Guid Id { get; init; }
    public BsonDocument Body => _body;

    public BsonDocument LastBody => _lastBody;

    public void SetBody(string json)
    {
        _body = BsonDocument.Parse(json);
    }

    public void SetLastBody(string json)
    {
        _lastBody = BsonDocument.Parse(json);
    }

    public BsonArray GetChanges() => _changes;
}