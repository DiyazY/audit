namespace audit.Repositories;

using audit.Models;

public static class AuditObjectAdapter
{
    public static AuditModel ToViewModel(this AuditObject obj)
    {
        return new () {
            Id = obj.Id,
            Body = obj.Body.ToString()
        };
    }

    public static AuditObject ToEntityModel(this AuditModel model)
    {
        var auditObject = new AuditObject()
        {
            Id = model.Id
        };
        auditObject.SetBody(model.Body.ToString());
        return auditObject;
    }
}
