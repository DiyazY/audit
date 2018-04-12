using System;
using System.ComponentModel.DataAnnotations;

namespace audit.Models
{
    public class Request
    {
        [Required]
        public string AppName{get;set;}
        [Required]
        public Guid ObjectId{get;set;}
    }
}