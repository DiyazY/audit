namespace audit.Services
{
    using audit.Models;
    using audit.Repositories;

    public sealed class AuditService
    {
        private IAuditRepository _repository;
        public AuditService(IAuditRepository repository)
        {
            _repository = repository;
        }

        public Task<AuditModel> GetAuditModel(Guid id)
        {
            if(id == Guid.Empty){// use (id is default)
                System.Console.WriteLine("default"); 
            }

            return _repository.GetAuditModel(id);
        }
    }
}