using FleetManagementService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Services.Core.DatabaseContext;

namespace FleetManagementService.Persistence.DatabaseContext;

// todo: Rename
public class AccountDatabaseContext(DbContextOptions<AccountDatabaseContext> options) : BaseDatabaseContext<AccountDatabaseContext>(options)
{
    public DbSet<Account> Accounts { get; set; }
    
    public DbSet<VehicleEntity> Vehicles { get; set; }
    
    public DbSet<DriverEntity> Drivers { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AccountDatabaseContext).Assembly);
    }
}