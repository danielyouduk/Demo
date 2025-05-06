using AccountService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Services.Core.ConfigurationExtensions;

namespace AccountService.Persistence.Configurations;

public class AccountConfiguration : BaseConfiguration<Account>
{
    private const string TableName = "Account";

    public override void Configure(EntityTypeBuilder<Account> builder)
    {
        base.Configure(builder);
        
        ConfigureTable(builder);
        ConfigureProperties(builder);
        ConfigureRelationships(builder);
        ConfigureSeedData(builder);
    }
    
    protected override void ConfigureTable(EntityTypeBuilder<Account> builder)
    {
        builder.ToTable(TableName);
    }

    protected override void ConfigureProperties(EntityTypeBuilder<Account> builder)
    {
        builder.Property(a => a.CompanyName)
            .HasMaxLength(50);

        builder.Property(a => a.CompanyVatNumber)
            .HasMaxLength(50);

        builder.Property(a => a.BillingAddressId);
        
        builder.Property(a => a.NoOfActiveDrivers)
            .IsRequired();
        
        builder.Property(a => a.NoOfActiveVehicles)
            .IsRequired();
        
        builder.Property(a => a.NoOfActiveChecklists)
            .IsRequired();
        
        builder.Property(a => a.NoOfReportsSubmitted)
            .IsRequired();
    }

    protected override void ConfigureSeedData(EntityTypeBuilder<Account> builder)
    {
        builder.HasData(
            new Account
            {
                Id = new Guid("63e1389a-571c-491e-9c60-bb98c838d0e2"),
                CompanyName = "Company Name",
                CompanyVatNumber = "123456789",
                BillingAddressId = new Guid("63e1389a-571c-491e-9c60-bb98c838d0e2"),
                NoOfActiveDrivers = 10,
                NoOfActiveVehicles = 10,
                NoOfActiveChecklists = 10,
                NoOfReportsSubmitted = 10,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            });
    }
}