using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace audit.Utils
{
    public interface IComponentDefinition
    {
        void DefineServices(IServiceCollection services);
        void DefineComponents(WebApplication app);
    }
}