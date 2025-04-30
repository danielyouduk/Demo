using DriverService.Domain.Entities;
using DriverService.Persistence.Configurations.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DriverService.Persistence.Configurations;

public class DriverConfiguration : BaseConfiguration<DriverEntity>
{
    private const string TableSchema = "dbo";
    private const string TableName = "Driver";
    
    public override void Configure(EntityTypeBuilder<DriverEntity> builder)
    {
        base.Configure(builder);
        
        ConfigureTable(builder);
        ConfigureProperties(builder);
        ConfigureRelationships(builder);
        ConfigureSeedData(builder);
    }

    private static void ConfigureTable(EntityTypeBuilder<DriverEntity> builder)
    {
        builder.ToTable(TableName, TableSchema);
    }
    
    private static void ConfigureProperties(EntityTypeBuilder<DriverEntity> builder)
    {
        builder.Property(entity => entity.Name)
            .IsRequired()
            .HasMaxLength(100);
    }
    
    private static void ConfigureRelationships(EntityTypeBuilder<DriverEntity> builder)
    {
        
    }

    private static void ConfigureSeedData(EntityTypeBuilder<DriverEntity> builder)
    {
        builder.HasData(
            new DriverEntity
            {
                Id = Guid.NewGuid(),
                Name = "David",
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            },
            new DriverEntity
            {
                Id = Guid.NewGuid(),
                Name = "John",
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            });
    }
}