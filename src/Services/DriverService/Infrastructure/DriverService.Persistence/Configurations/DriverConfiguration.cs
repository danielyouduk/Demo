using DriverService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Services.Core.ConfigurationExtensions;

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

    protected override void ConfigureTable(EntityTypeBuilder<DriverEntity> builder)
    {
        builder.ToTable(TableName);
    }

    protected override void ConfigureProperties(EntityTypeBuilder<DriverEntity> builder)
    {
        builder.Property(entity => entity.FirstName)
            .IsRequired()
            .HasMaxLength(100);
    }

    protected override void ConfigureSeedData(EntityTypeBuilder<DriverEntity> builder)
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