using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using audit.Entities;
using audit.Models;
using audit.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using Newtonsoft.Json.Linq;

namespace audit.Controllers
{
    [Produces("application/json")]
    [Route("api/Audit")]
    public class AuditController : Controller
    {
        private readonly IAuditService _auditService;

        public AuditController(IAuditService auditService)
        {
            _auditService = auditService;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]AuditModel entity)
        {
            try
            {
                var auditObject = new Entities.AuditObject()
                {
                    Id = entity.Id
                };
                auditObject.SetBody(entity.Body.ToString());
                await _auditService.SetAuditObject(auditObject);
                return Ok();
            }
            catch(Exception ex)
            {
                return BadRequest(new
                {
                    error = "error",
                    error_description = "Sorry, something it's going wrong((("
                });
            }
        }

        // GET: api/values
        [HttpGet]
        public async Task<IEnumerable<AuditModel>> Get()
        {
            var filter = Builders<AuditObject>.Filter.Empty;
            return (await _auditService.Read(filter)).Select(p => new AuditModel()
            {
                Id = p.Id,
                Body = JObject.Parse(p.Body?.ToString())
            });
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public async Task<IEnumerable<AuditModel>> Get(System.Guid id)
        {
            var filter = Builders<AuditObject>.Filter.Eq("_id", id);
            return (await _auditService.Read(filter)).Select(p => new AuditModel()
            {
                Id = p.Id,
                Body = JObject.Parse(p.Body?.ToString())
            });
        }
        // Get api/values/6
        [HttpGet("filter")]
        public async Task<IEnumerable<AuditModel>> Get(string filter)
        {
            return (await _auditService.Read(filter)).Select(p=> new AuditModel()
            {
                Id = p.Id,
                Body = JObject.Parse(p.Body?.ToString())
            });
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public async Task<AuditModel> Put(System.Guid id, [FromBody]AuditModel entity)
        {
            var filter = Builders<AuditObject>.Filter.Eq("_id", id);
            var update = Builders<AuditObject>.Update.Set("_body", entity?.Body);
            var auditObject = await _auditService.Update(filter, update);
            return new AuditModel()
            {
                Id = auditObject.Id,
                Body = JObject.Parse(auditObject.Body?.ToString())
            };
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public async Task<AuditModel> Delete(System.Guid id)
        {
            var filter = Builders<AuditObject>.Filter.Eq("_id", id);
            var auditObject =  await _auditService.Delete(filter);
            return new AuditModel()
            {
                Id = auditObject.Id,
                Body = JObject.Parse(auditObject.Body?.ToString())
            };
        }
    }
}