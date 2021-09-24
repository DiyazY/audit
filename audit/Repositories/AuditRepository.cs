namespace audit.Repositories
{
    using audit.Models;
    using MongoDB.Driver;

    public sealed class AuditRepository: IAuditRepository
    {
        private IMongoDatabase _context;

        public AuditRepository(IMongoDatabase context)
        {
            _context = context;
        }

        public async Task<AuditModel> GetAuditModel(Guid id)
        {
            var filter = Builders<AuditObject>.Filter.Eq("_id", id);
            var cursor = await _context.GetCollection<AuditObject>("auditables").FindAsync(filter);
            IEnumerable<AuditObject> list =  new List<AuditObject>(); // check new features of C#10. empty list or something like that
            while (await cursor.MoveNextAsync())
            {
                list = cursor.Current;
            }
            var auditObject = list.First();
            return auditObject.ToViewModel();
        }
    }
}