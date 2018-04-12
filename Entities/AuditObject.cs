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
        /// <summary>
        /// Id
        /// </summary>
        [BsonId]
        public Guid Id { get; set; }

        #region private fields
        /// <summary>
        /// Body (initial)
        /// </summary>
        [BsonElement]
        private BsonDocument _body { get; set; }

        /// <summary>
        /// Last inserted body
        /// </summary>
        [BsonElement]
        private BsonDocument _lastBody { get; set; }

        /// <summary>
        /// Created date
        /// </summary>
        [BsonElement]
        private DateTime _createdDate = DateTime.UtcNow;

        /// <summary>
        /// Changes
        /// </summary>
        [BsonElement]
        private BsonArray _changes { get; set; } = new BsonArray();
        #endregion

        #region methods

        /// <summary>
        /// Body (initial)
        /// </summary>
        public BsonDocument Body
        {
            get
            {
                return _body;
            }
        }
        /// <summary>
        /// Last inserted body
        /// </summary>
        public BsonDocument LastBody
        {
            get
            {
                return _lastBody;
            }
        }
        /// <summary>
        /// Set body
        /// </summary>
        /// <param name="json">json</param>
        public void SetBody(string json)
        {
            _body = BsonDocument.Parse(json);
        }

        /// <summary>
        /// Set last inserted body
        /// </summary>
        /// <param name="json">json</param>
        public void SetLastBody(string json)
        {
            _lastBody = BsonDocument.Parse(json);
        }

        /// <summary>
        /// Get changes
        /// </summary>
        /// <returns></returns>
        public BsonArray GetChanges()
        {
            return _changes;
        }
        #endregion

        /// <summary>
        /// Empty body (constant)
        /// </summary>
        public static string EmptyBody = "{}";
    }
}
