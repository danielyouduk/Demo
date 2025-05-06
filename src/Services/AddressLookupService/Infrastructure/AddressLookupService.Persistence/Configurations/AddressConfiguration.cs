using AddressLookupService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Services.Core.ConfigurationExtensions;

namespace AddressLookupService.Persistence.Configurations;

public class AddressConfiguration : BaseConfiguration<AddressEntity>
{
    private const string TableName = "Address";
    
    public override void Configure(EntityTypeBuilder<AddressEntity> builder)
    {
        base.Configure(builder);
        
        ConfigureTable(builder);
        ConfigureProperties(builder);
        ConfigureRelationships(builder);
        ConfigureSeedData(builder);
    }
    
    protected override void ConfigureTable(EntityTypeBuilder<AddressEntity> builder)
    {
        builder.ToTable(TableName);
    }

    protected override void ConfigureProperties(EntityTypeBuilder<AddressEntity> builder)
    {
        builder.Property(a => a.AccountId);

        builder.Property(a => a.Street)
            .HasMaxLength(50);

        builder.Property(a => a.Town);
        
        builder.Property(a => a.City)
            .IsRequired();
        
        builder.Property(a => a.PostCode)
            .IsRequired();
    }

    protected override void ConfigureSeedData(EntityTypeBuilder<AddressEntity> builder)
    {
        builder.HasData(
            new AddressEntity
            {
                Id = new Guid("63e1389a-571c-491e-9c60-bb98c838d0e2"),
                AccountId = new Guid("63e1389a-571c-491e-9c60-bb98c838d0e2"),
                Street = "123 Main Street",
                Town = "Swinton",
                City = "Manchester",
                PostCode = "SW1A 1AA",
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            });
    }
}