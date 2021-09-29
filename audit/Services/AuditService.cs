namespace audit.Services;

using audit.Models;
using audit.Repositories;

public sealed class AuditService
{
    private IAuditRepository _repository;
    public AuditService(IAuditRepository repository)
    {
        _repository = repository;
    }

    public async Task<AuditModel> GetAuditModel(Guid id)
    {
        if (id == Guid.Empty)
        {// use (id is default)
            Console.WriteLine("default");
        }

        var auditObject = await _repository.GetAuditObject(id);
        return auditObject.ToViewModel();
    }

    public async Task SaveAuditModel(AuditModel model)
    {
        // if(model is null) throw
        var auditObject = await _repository.GetAuditObject(model.Id); 
        if(auditObject is null){
            auditObject = model.ToEntityModel();
            await _repository.SaveAuditObject(auditObject);
        }
        else{
            Console.WriteLine("updates auditable object");
        }
    }
}