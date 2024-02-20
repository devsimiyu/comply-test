using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ComplyTest.Data.Context;

public static class PersistenceContextServiceCollection
{
    public static IServiceCollection AddPersitenceContext(this IServiceCollection services)
    {
        services.AddDbContext<PersistenceContext>((provider, options) =>
        {
            var configuration = provider.GetService<IConfiguration>();
            var connection = configuration?["Database"];

            options.UseSqlite(connection);
        });

        return services;
    }
}
