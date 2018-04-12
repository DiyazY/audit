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
        /// App name
        /// </summary>
        [Required]
        public string AppName{get;set;}
        /// <summary>
        /// Object's id
        /// </summary>
        [Required]
        public Guid ObjectId{get;set;}
    }
}