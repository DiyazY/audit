namespace audit.Utils;

using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

public interface IComponentDefinition
{
    void DefineServices(IServiceCollection services);
    void DefineComponents(WebApplication app);
}