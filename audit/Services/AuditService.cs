namespace audit.Services;

using audit.Models;
using audit.Repositories;
using audit.Utils;

public sealed class AuditService
{
    private IAuditRepository _repository;

    public AuditService(IAuditRepository repository)
    {
        _repository = repository;
    }

    public async Task<AuditModel> GetAuditModel(Guid id)// this method is useless. remove it!!!
    {
        if (id == Guid.Empty)
        {
            // use (id is default)
            throw new Exception("id can not be empty");
        }

        var auditObject = await _repository.GetAuditObject(id);
        return auditObject.ToViewModel();
    }

    public async Task<IEnumerable<AuditModel>> GetModelsOfAuditableObjectThroughItsLifecycle(Guid id)
    {
        if (id == Guid.Empty)
        {
            // use (id is default)
            throw new Exception("id can not be empty");
        }

        var auditObject = await _repository.GetAuditObject(id);
        if (auditObject is not null)
        {
            var changes = auditObject?.GetChanges();
            if (changes.Any())
            {
                List<AuditModel> list = new();
                var body = auditObject?.Body?.ToString();
                list.Add(new AuditModel
                {
                    Id = id,
                    Body = body
                });
                foreach (var diff in changes)
                {
                    body = Diff.Patch(body, diff.ToString());
                    list.Add(new AuditModel
                    {
                        Id = id,
                        Body = body
                    });
                }
                return list;
            }
        }
        return default;
    }

    public async Task SaveAuditModel(AuditModel model)
    {

        if (model is null)
        {
            throw new NullReferenceException("model can not be null!");
        }

        var auditObject = await _repository.GetAuditObject(model.Id);
        if (auditObject is null)
        {
            auditObject = model.ToEntityModel();
            auditObject.SetLastBody(model.Body.ToString());
            await _repository.SaveAuditObject(auditObject);
        }
        else
        {
            // TODO: make names of diff class methods are more eloquent!
            var diff = Diff.Get(
                auditObject.LastBody.ToString(),
                model.Body.ToString()
            );
            Console.WriteLine($"diff : {diff}");

            // I left it here because here is easier to check how it works
            //var patch = Diff.Patch(body.ToJson(), diff);
            //var unpatch = Diff.Unpatch(entity?.Body?.ToJson(), diff);

            if (!String.IsNullOrEmpty(diff))
            {
                await _repository.UpdateAuditObject(auditObject, diff);
            }
        }
    }
}