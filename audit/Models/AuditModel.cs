using System.Text.Json;

namespace audit.Models
{
    public class AuditModel
    {
        public Guid? Id { get; set; }
        public object Body { get; set; }
    }
}