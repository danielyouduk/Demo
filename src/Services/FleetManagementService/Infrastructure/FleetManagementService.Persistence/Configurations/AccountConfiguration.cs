using Bogus;
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
        var accountIds = new[]
        {
            new Guid("3F21CAE9-E777-425A-8BA5-DC15782A232D"),
            new Guid("0C49E8CE-7884-4665-8591-CB8CA9AFAF34")
        };
        
        var faker = new Faker<Account>()
            .RuleFor(a => a.Id, (f, a) => accountIds[f.IndexFaker % accountIds.Length])
            .RuleFor(a => a.CompanyName, f => f.Company.CompanyName())
            .RuleFor(a => a.CompanyVatNumber, f => f.Random.Replace("#########"))
            .RuleFor(a => a.BillingAddressId, f => Guid.NewGuid())
            .RuleFor(a => a.IsActive, f => f.Random.Bool(0.8f))
            .RuleFor(a => a.CreatedAt, DateTime.UtcNow)
            .RuleFor(a => a.UpdatedAt, DateTime.UtcNow);

        var accounts = faker.Generate(2);
        builder.HasData(accounts);
    }
}