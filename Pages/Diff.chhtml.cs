using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using audit.Entities;
using audit.Models;
using audit.Services.Interfaces;
using audit.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MongoDB.Bson;
using MongoDB.Driver;
using Newtonsoft.Json.Linq;

namespace audit.Pages
{
    public class DiffModel : PageModel
    {
        private readonly IAuditService _auditService;
        public DiffModel(IAuditService auditService){
            _auditService = auditService;
        }
        public void OnGet()
        {

        }

        [BindProperty]
        public Request RequestModel { get; set; }

        public IEnumerable<string> ErrorMessages{get;set;}

        public IEnumerable<AuditModel> AuditObjects{get;set;}
        public IEnumerable<string> Changes{get;set;}

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid || Guid.Empty == RequestModel.ObjectId)
            {
                if(Guid.Empty == RequestModel.ObjectId && !ModelState.Values.SelectMany(v => v.Errors).Any())
                    ModelState.AddModelError(string.Empty, $"Empty ObjectId's guid: {RequestModel.ObjectId}");
                ErrorMessages = ModelState.Values.SelectMany(v => v.Errors).Select(p=>p.ErrorMessage);
                return Page();
            }
            var filter = Builders<AuditObject>.Filter.Eq("_id", RequestModel.ObjectId);
            var auditObject = (await _auditService.Read(filter)).FirstOrDefault();
            if(auditObject!=null){
                var changes = auditObject?.GetChanges();
                if(changes?.Count>0){
                    List<AuditModel> list = new List<AuditModel>();
                    string body = auditObject?.Body?.ToJson();
                    list.Add(new AuditModel()
                    {
                        Id = RequestModel.ObjectId,
                        Body = JObject.Parse(body)
                    });
                    foreach (var diff in changes)
                    {
                        body = Diff.Patch(body, diff.ToJson());
                        list.Add(new AuditModel()
                        {
                            Id = RequestModel.ObjectId,
                            Body = JObject.Parse(body)
                        });
                    }
                    Changes = changes.Select(p=>p.ToJson());
                    AuditObjects = list;
                }
            }

            return Page();
        }
    }
}
