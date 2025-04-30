using DriverService.Domain.Entities;
using DriverService.Persistence.Configurations.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DriverService.Persistence.Configurations;

public class DriverConfiguration : BaseConfiguration<DriverEntity>
{
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
        builder.ToTable(TableName);
    }
    
    private static void ConfigureProperties(EntityTypeBuilder<DriverEntity> builder)
    {
        builder.Property(entity => entity.FirstName)
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
                Id = new Guid("63e1389a-571c-491e-9c60-bb98c838d0e2"),
                FirstName = "David",
                LastName = "Smith",
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            },
            new DriverEntity
            {
                Id = new Guid("81f31f9a-0955-49b5-b529-3c37c117fa03"),
                FirstName = "John",
                LastName = "Davis",
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            },
            new DriverEntity
            {
                Id = new Guid("df62c086-9697-465b-aa48-c4d35e14b477"),
                FirstName = "Jane",
                LastName = "Kendal",
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            });
    }
}