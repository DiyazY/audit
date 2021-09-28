using System.Text.Json;

namespace audit.Models // checkout TODO: global using smt smt
{
    public class AuditModel
    {
        public Guid Id { get; set; }
        public object Body { get; set; }
    }
}