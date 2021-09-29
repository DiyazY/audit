namespace audit.Models;

public class AuditModel
{
    public Guid Id { get; init; }
    public object Body { get; init; }
}