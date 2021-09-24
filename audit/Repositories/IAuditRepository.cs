namespace audit.Repositories
{
    using audit.Models;
    using MongoDB.Driver;

    public interface IAuditRepository
    {


        Task<AuditModel> GetAuditModel(Guid id);
    }
}