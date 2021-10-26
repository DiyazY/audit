namespace audit.ComponentDefinitions;

using audit.Models;
using audit.Repositories;
using audit.Services;
using audit.Utils;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;


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
        app.MapPost("/audit", async (AuditModel model, AuditService service)=>{
            //TODO: adjsut performance 
            await service.SaveAuditModel(model);
        
            ///Console.WriteLine(model.Body);
        });

        app.MapGet("/audit/{id}", Task<AuditModel> (Guid id, AuditService service) => service.GetAuditModel(id));
        app.MapGet("/audit/{id}/models", Task<IEnumerable<AuditModel>> (Guid id, AuditService service) => service.GetModelsOfAuditableObjectThroughItsLifecycle(id));        
    }
}