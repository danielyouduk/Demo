using FleetManagementService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Services.Core.ConfigurationExtensions;

namespace FleetManagementService.Persistence.Configurations;

public class VehicleConfiguration : BaseConfiguration<VehicleEntity>
{
    private const string TableName = "Vehicle";
    
    public override void Configure(EntityTypeBuilder<VehicleEntity> builder)
    {
        base.Configure(builder);
        
        ConfigureTable(builder);
        ConfigureProperties(builder);
        ConfigureRelationships(builder);
        ConfigureSeedData(builder);
    }
    
    protected override void ConfigureTable(EntityTypeBuilder<VehicleEntity> builder)
    {
        builder.ToTable(TableName);
    }

    protected override void ConfigureProperties(EntityTypeBuilder<VehicleEntity> builder)
    {
        builder.Property(entity => entity.AccountId)
            .IsRequired();
        
        builder.Property(entity => entity.RegistrationNumber)
            .IsRequired()
            .HasMaxLength(100);
    }

    protected override void ConfigureSeedData(EntityTypeBuilder<VehicleEntity> builder)
    {
        builder.HasData(
            new VehicleEntity
            {
                Id = new Guid("63e1389a-571c-491e-9c60-bb98c838d0e2"),
                AccountId = new Guid("63e1389a-571c-491e-9c60-bb98c838d0e2"),
                RegistrationNumber = "MW63 LRN",
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            });
    }
}