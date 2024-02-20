using ComplyTest.Core.Repository;
using Microsoft.Extensions.DependencyInjection;

namespace ComplyTest.Data.Repository;

public static class CoreRepositoryInServiceCollection
{
    public static IServiceCollection AddCoreRepository(this IServiceCollection services)
    {
        services.AddScoped<IItemRepository, ItemRepository>();
        
        return services;
    }
}
