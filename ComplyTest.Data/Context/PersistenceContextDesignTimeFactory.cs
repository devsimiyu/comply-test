using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace ComplyTest.Data.Context;

public class PersistenceContextDesignTimeFactory : IDesignTimeDbContextFactory<PersistenceContext>
{
    public PersistenceContext CreateDbContext(string[] args)
    {
        var configuration = new ConfigurationBuilder()
            .AddCommandLine(args)
            .Build();

        var connection = configuration?["Database"];

        var options = new DbContextOptionsBuilder<PersistenceContext>()
            .UseSqlite(connection)
            .Options;

        return new PersistenceContext(options);
    }
}
