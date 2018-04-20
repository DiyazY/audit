using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace audit.Models
{
    /// <summary>
    /// Audit model
    /// </summary>
    public class AuditModel
    {
        /// <summary>
        /// Object's id
        /// </summary>
        [Required]
        public Guid Id { get; set; }
        /// <summary>
        /// Object's body
        /// </summary>
        [Required]
        public JObject Body { get; set; }
    }
}
