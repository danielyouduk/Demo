using FleetManagementService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Services.Core.DatabaseContext;

namespace FleetManagementService.Persistence.DatabaseContext;

public class FleetManagementDatabaseContext(DbContextOptions<FleetManagementDatabaseContext> options) : BaseDatabaseContext<FleetManagementDatabaseContext>(options)
{
    public DbSet<Account> Accounts { get; set; }
    
    public DbSet<Vehicle> Vehicles { get; set; }
    
    public DbSet<Driver> Drivers { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(FleetManagementDatabaseContext).Assembly);
    }
}