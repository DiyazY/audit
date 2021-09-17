using audit.Utils;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace audit.ComponentDefinitions
{
    public class SwaggerDefinition: IComponentDefinition
    {
        private const string VERSION = "v1";
        public void DefineServices(IServiceCollection services)
        {
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(c=>c.SwaggerDoc(VERSION, new OpenApiInfo{Title = "audit", Description = "TODO: add some description" }));
        }

        public void DefineComponents(WebApplication app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", $"AUDIT API {VERSION}");
            });
        }
    }
}