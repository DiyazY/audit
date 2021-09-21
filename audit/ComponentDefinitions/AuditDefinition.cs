using audit.Models;
using audit.Services;
using audit.Utils;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace audit.ComponentDefinitions
{
    public class AuditDefinition : IComponentDefinition
    {
        public void DefineServices(IServiceCollection services)
        {

        }

        public void DefineComponents(WebApplication app)
        {
            app.MapGet("/audit", () =>
             {
                 return "hello world!";
             });
             app.MapPost("/audit", (AuditModel model)=>{
                 //TODO: adjsut performance 
                Console.WriteLine(model.Body);
             });
        }
    }
}