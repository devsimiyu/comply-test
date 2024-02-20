using ComplyTest.Core.Entity;
using ComplyTest.Data.Configuration;
using Microsoft.EntityFrameworkCore;

namespace ComplyTest.Data.Context;

public class PersistenceContext : DbContext
{
    public DbSet<Item> Items { get; set; }
    
    public PersistenceContext(DbContextOptions<PersistenceContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder model)
    {
        model.ApplyConfiguration(new ItemConfiguration());
    }
}
