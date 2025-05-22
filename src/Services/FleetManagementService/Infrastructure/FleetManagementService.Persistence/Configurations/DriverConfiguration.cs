using Bogus;
using FleetManagementService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Services.Core.ConfigurationExtensions;

namespace FleetManagementService.Persistence.Configurations;

public class DriverConfiguration : BaseConfiguration<Driver>
{
    private const string TableName = "Driver";
    
    public override void Configure(EntityTypeBuilder<Driver> builder)
    {
        base.Configure(builder);
        
        ConfigureTable(builder);
        ConfigureProperties(builder);
        ConfigureRelationships(builder);
        ConfigureSeedData(builder);
    }

    protected override void ConfigureTable(EntityTypeBuilder<Driver> builder)
    {
        builder.ToTable(TableName);
    }

    protected override void ConfigureProperties(EntityTypeBuilder<Driver> builder)
    {
        builder.Property(entity => entity.AccountId)
            .IsRequired();
        
        builder.Property(entity => entity.FirstName)
            .IsRequired()
            .HasMaxLength(100);
        
        builder.Property(entity => entity.LastName)
            .IsRequired()
            .HasMaxLength(100);
    }

    protected override void ConfigureSeedData(EntityTypeBuilder<Driver> builder)
    {
        var accountIds = new[] 
        {
            new Guid("3F21CAE9-E777-425A-8BA5-DC15782A232D"),
            new Guid("0C49E8CE-7884-4665-8591-CB8CA9AFAF34")
        };

        var faker = new Faker<Driver>()
            .RuleFor(d => d.Id, f => Guid.NewGuid())
            .RuleFor(d => d.AccountId, (f, d) => accountIds[f.IndexFaker % accountIds.Length])
            .RuleFor(d => d.FirstName, f => f.Name.FirstName())
            .RuleFor(d => d.LastName, f => f.Name.LastName())
            .RuleFor(d => d.IsActive, f => f.Random.Bool(0.8f))
            .RuleFor(d => d.CreatedAt, DateTime.UtcNow)
            .RuleFor(d => d.UpdatedAt, DateTime.UtcNow);

        var drivers = faker.Generate(80);
        builder.HasData(drivers);
    }
}