using DriverService.Domain.Entities;
using DriverService.Persistence.Configurations.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DriverService.Persistence.Configurations;

public class ReportConfiguration : BaseConfiguration<ReportEntity>
{
    private const string TableName = "Report";
    
    public override void Configure(EntityTypeBuilder<ReportEntity> builder)
    {
        base.Configure(builder);
        
        ConfigureTable(builder);
        ConfigureProperties(builder);
        ConfigureRelationships(builder);
        ConfigureSeedData(builder);
    }

    protected override void ConfigureTable(EntityTypeBuilder<ReportEntity> builder)
    {
        builder.ToTable(TableName);
    }

    protected override void ConfigureProperties(EntityTypeBuilder<ReportEntity> builder)
    {
        builder.Property(entity => entity.AccountId)
            .IsRequired();
        
        builder.Property(entity => entity.DriverId)
            .IsRequired();
        
        builder.Property(entity => entity.VehicleId)
            .IsRequired();
    }
    
    protected override void ConfigureSeedData(EntityTypeBuilder<ReportEntity> builder)
    {
        builder.HasData(
            new ReportEntity
            {
                Id = new Guid("63e1389a-571c-491e-9c60-bb98c838d0e2"),
                AccountId = 1,
                DriverId = 1,
                VehicleId = 1,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            });
    }
}