namespace audit.Models;

using System.ComponentModel.DataAnnotations;

public class AuditModel
{
    [Required]
    public Guid Id { get; init; }
    [Required]
    public object Body { get; init; }
}