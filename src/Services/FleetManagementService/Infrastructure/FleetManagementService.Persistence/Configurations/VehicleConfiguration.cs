using Bogus;
using FleetManagementService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Services.Core.ConfigurationExtensions;

namespace FleetManagementService.Persistence.Configurations;

public class VehicleConfiguration : BaseConfiguration<Vehicle>
{
    private const string TableName = "Vehicle";
    
    public override void Configure(EntityTypeBuilder<Vehicle> builder)
    {
        base.Configure(builder);
        
        ConfigureTable(builder);
        ConfigureProperties(builder);
        ConfigureRelationships(builder);
        ConfigureSeedData(builder);
    }
    
    protected override void ConfigureTable(EntityTypeBuilder<Vehicle> builder)
    {
        builder.ToTable(TableName);
    }

    protected override void ConfigureProperties(EntityTypeBuilder<Vehicle> builder)
    {
        builder.Property(entity => entity.AccountId)
            .IsRequired();
        
        builder.Property(entity => entity.RegistrationNumber)
            .IsRequired()
            .HasMaxLength(10);
    }

    protected override void ConfigureSeedData(EntityTypeBuilder<Vehicle> builder)
    {
        var accountIds = new[] 
        {
            new Guid("3F21CAE9-E777-425A-8BA5-DC15782A232D"),
            new Guid("0C49E8CE-7884-4665-8591-CB8CA9AFAF34")
        };

        var faker = new Faker<Vehicle>()
            .RuleFor(v => v.Id, f => Guid.NewGuid())
            .RuleFor(v => v.AccountId, (f, v) => accountIds[f.IndexFaker % accountIds.Length])
            .RuleFor(v => v.RegistrationNumber, f => f.Random.Replace("?? ## ???"))
            .RuleFor(v => v.CreatedAt, DateTime.UtcNow)
            .RuleFor(v => v.UpdatedAt, DateTime.UtcNow);

        var vehicles = faker.Generate(40);
        builder.HasData(vehicles);
    }
}