using ComplyTest.Core.Entity;
using ComplyTest.Data.Context;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace ComplyTest.IntegrationTests;

public class IntegrationTestFixture<TProgram> : WebApplicationFactory<TProgram> where TProgram : class
{
    public readonly List<Item> Items = new List<Item>
    {
        new Item 
        { 
            Id = Guid.NewGuid(),
            Name = "Item one",
            DateCreated = DateTime.UtcNow,
        },
        new Item 
        { 
            Id = Guid.NewGuid(),
            Name = "Item two",
            DateCreated = DateTime.UtcNow,
        },
        new Item 
        { 
            Id = Guid.NewGuid(),
            Name = "Item three",
            DateCreated = DateTime.UtcNow,
        },
        new Item 
        { 
            Id = Guid.NewGuid(),
            Name = "Item four",
            DateCreated = DateTime.UtcNow,
        }
    };

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            services.RemoveAll(typeof(PersistenceContext));
            services.AddDbContext<PersistenceContext>((container, options) =>
            {
                options.UseSqlite("DataSource=:memory:");
            });

            using (var dbContext = services.BuildServiceProvider().GetRequiredService<PersistenceContext>())
            {
                dbContext.AddRange(Items);
                dbContext.SaveChanges();
            }
        });

        builder.UseEnvironment("Development");
    }
}
