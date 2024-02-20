using ComplyTest.Core.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ComplyTest.Data.Configuration;

public class ItemConfiguration : IEntityTypeConfiguration<Item>
{
    public void Configure(EntityTypeBuilder<Item> entity)
    {
        entity.ToTable("items");
        entity.HasKey(e => e.Id).HasName("id");
        entity.Property(e => e.Name).HasColumnName("name").HasMaxLength(500).IsRequired();
        entity.Property(e => e.DateCreated).HasColumnName("date_created");
        entity.Property(e => e.DateModified).HasColumnName("date_modified");
    }
}
