using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace audit.Models
{
    public class AuditModel
    {
        public Guid Id { get; set; }

        public JObject Body { get; set; }
    }
}
