namespace audit.Utils;

using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;



public static class ComponentDefinitionExtensions
{
    public static void AddComponentDefinitions(this IServiceCollection services, params Type[] scanMarkers)
    {
        var definitions = new List<IComponentDefinition>();
        foreach (var marker in scanMarkers)
        {
            definitions.AddRange(
                marker.Assembly.ExportedTypes
                    .Where(x=>typeof(IComponentDefinition).IsAssignableFrom(x) && !x.IsAbstract)
                    .Select(Activator.CreateInstance).Cast<IComponentDefinition>()
                );
        }

        foreach (var endpointDefinition in definitions)
        {
            endpointDefinition.DefineServices(services);
        }

        if (definitions.Any())
        {
            services.AddSingleton(definitions as IReadOnlyCollection<IComponentDefinition>);   
        }
    }

    public static void UseComponentDefinitions(this WebApplication app)
    {
        var definitions = app.Services.GetService<IReadOnlyCollection<IComponentDefinition>>();
        foreach (var definition in definitions ?? Enumerable.Empty<IComponentDefinition>())
        {
            definition.DefineComponents(app);
        }
    }
        
}

