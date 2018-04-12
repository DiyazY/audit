using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace audit.Entities
{
    /// <summary>
    /// Audit object
    /// </summary>
    public class AuditObject
    {
        [BsonId]
        public Guid Id { get; set; }

        #region private fields
        [BsonElement]
        private BsonDocument _body { get; set; }

        [BsonElement]
        private DateTime _createdDate = DateTime.UtcNow;

        [BsonElement]
        private BsonArray _changes { get; set; } = new BsonArray();
        #endregion

        #region methods

        public BsonDocument Body
        {
            get
            {
                return _body;
            }
        }

        public void SetBody(string json)
        {
            _body = BsonDocument.Parse(json);
        }

        public BsonArray GetChanges()
        {
            return _changes;
        }
        #endregion

        public static string EmptyBody = "{}";
    }
}
