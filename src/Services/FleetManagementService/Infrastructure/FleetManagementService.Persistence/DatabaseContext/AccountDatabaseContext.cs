using FleetManagementService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Services.Core.DatabaseContext;

namespace FleetManagementService.Persistence.DatabaseContext;

// todo: Rename
public class AccountDatabaseContext(DbContextOptions<AccountDatabaseContext> options) : BaseDatabaseContext<AccountDatabaseContext>(options)
{
    public DbSet<Account> Accounts { get; set; }
    
    public DbSet<Vehicle> Vehicles { get; set; }
    
    public DbSet<Driver> Drivers { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AccountDatabaseContext).Assembly);
    }
}