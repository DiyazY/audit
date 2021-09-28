namespace audit.Repositories
{
    using MongoDB.Bson;
    using MongoDB.Bson.Serialization.Attributes;
    public sealed class AuditObject
    {

        [BsonId]
        public Guid Id { get; set; }
        public BsonDocument Body { get; set; }

    }

}