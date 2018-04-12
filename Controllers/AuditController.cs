using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using audit.Entities;
using audit.Models;
using audit.Services.Interfaces;
using audit.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
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

        /// <summary>
        /// Takes object
        /// </summary>
        /// <param name="model">model</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]AuditModel model)
        {
            try
            {
                var auditObject = new Entities.AuditObject()
                {
                    Id = model.Id
                };
                auditObject.SetBody(model.Body.ToString());
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

        /// <summary>
        /// Gets list of objects in historical order
        /// </summary>
        /// <param name="id">object's id</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(List<AuditModel>), 200)]
        public async Task<IActionResult> Get(System.Guid id)
        {
            try
            {
                var filter = Builders<AuditObject>.Filter.Eq("_id", id);
                var auditObject = (await _auditService.Read(filter)).FirstOrDefault();
                var changes = auditObject?.GetChanges();
                List<AuditModel> list = new List<AuditModel>();
                string body = auditObject.Body.ToJson();
                foreach (var diff in changes)
                {
                    body = Diff.Patch(body, diff.ToJson());
                    list.Add(new AuditModel()
                    {
                        Id = id,
                        Body = JObject.Parse(body)
                    });
                }
                return Ok(list);
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    error = "error",
                    error_description = "Sorry, something it's going wrong((("
                });
            }
        }

        /// <summary>
        /// Delete object by id
        /// </summary>
        /// <param name="id">object's id</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(AuditModel), 200)]
        public async Task<IActionResult> Delete(System.Guid id)
        {
            try
            {
                var filter = Builders<AuditObject>.Filter.Eq("_id", id);
                var auditObject = await _auditService.Delete(filter);
                return Ok(new AuditModel()
                {
                    Id = auditObject.Id,
                    Body = JObject.Parse(auditObject.Body?.ToString())
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    error = "error",
                    error_description = "Sorry, something it's going wrong((("
                });
            }
        }


#if DEBUG
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
#endif
    }
}