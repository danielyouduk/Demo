using FleetManagementService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Services.Core.DatabaseContext;
using Services.Core.Entities;

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
    
    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        var timestamp = DateTime.UtcNow;

        foreach (var entry in ChangeTracker.Entries<BaseEntity>())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    
                    if (entry.Entity.CreatedAt == default) entry.Entity.CreatedAt = timestamp;
                    if (entry.Entity.UpdatedAt == default) entry.Entity.UpdatedAt = timestamp;
                    
                    break;
                
                case EntityState.Modified:
                    
                    entry.Entity.UpdatedAt = timestamp;
                    entry.Property(x => x.CreatedAt).IsModified = false;
                    
                    break;
            }
        }
        
        return base.SaveChangesAsync(cancellationToken);
    }
}