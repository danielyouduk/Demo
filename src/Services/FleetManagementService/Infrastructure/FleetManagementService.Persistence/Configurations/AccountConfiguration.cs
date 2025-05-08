using FleetManagementService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Services.Core.ConfigurationExtensions;

namespace FleetManagementService.Persistence.Configurations;

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
    }

    protected override void ConfigureRelationships(EntityTypeBuilder<Account> builder)
    {
        // One-to-Many: Account-Drivers
        builder.HasMany(a => a.Drivers)
            .WithOne(d => d.Account)
            .HasForeignKey(d => d.AccountId);
        
        // One-to-Many: Account-Vehicles
        builder.HasMany(a => a.Vehicles)
            .WithOne(v => v.Account)
            .HasForeignKey(v => v.AccountId);
    }

    protected override void ConfigureSeedData(EntityTypeBuilder<Account> builder)
    {
        builder.HasData(
            new Account
            {
                Id = new Guid("3F21CAE9-E777-425A-8BA5-DC15782A232D"),
                CompanyName = "Company Account One",
                CompanyVatNumber = "123456789",
                BillingAddressId = new Guid("75E08289-B556-44EE-83DA-181AB8642D98"),
                IsActive = true,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            },
            new Account
            {
                Id = new Guid("0C49E8CE-7884-4665-8591-CB8CA9AFAF34"),
                CompanyName = "Company Account Two",
                CompanyVatNumber = "987654321",
                BillingAddressId = new Guid("D9B0CA63-A27C-4F56-8DD2-A8844C36E1C8"),
                IsActive = true,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            });
    }
}