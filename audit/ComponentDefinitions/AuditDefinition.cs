using audit.Models;
using audit.Repositories;
using audit.Services;
using audit.Utils;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;

namespace audit.ComponentDefinitions
{
    public class AuditDefinition : IComponentDefinition
    {
        public void DefineServices(IServiceCollection services)
        {
            var mongoClient = new MongoClient(Environment.GetEnvironmentVariable("connection-string"));
            var database = mongoClient.GetDatabase("AuditDb"); // investigate how to inject it efficiently!
            services.AddTransient<IMongoDatabase>(_ => database);
            services.AddTransient<IAuditRepository, AuditRepository>();
            services.AddTransient<AuditService>();
        }

        public void DefineComponents(WebApplication app)
        {
            app.MapGet("/hello-world", () =>
             {
                 return "hello world!";
             });
             app.MapPost("/audit", (AuditModel model)=>{
                 //TODO: adjsut performance 
                Console.WriteLine(model.Body);
             });

             app.MapGet("/audit/{id}", Task<AuditModel> (Guid id, AuditService service) => service.GetAuditModel(id));
        }
    }
}