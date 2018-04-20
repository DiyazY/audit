using System;
using System.ComponentModel.DataAnnotations;

namespace audit.Models
{
    /// <summary>
    /// Request
    /// </summary>
    public class Request
    {
        /// <summary>
        /// Object's id
        /// </summary>
        [Required]
        public Guid ObjectId{get;set;}
    }
}