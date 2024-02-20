using ComplyTest.Core.Service;
using Microsoft.Extensions.DependencyInjection;

namespace ComplyTest.Service;

public static class CoreServiceInServiceCollection
{
    public static IServiceCollection AddCoreService(this IServiceCollection services)
    {
        services.AddScoped<IItemService, ItemService>();
        
        return services;
    }
}
