using DriverService.Domain.Entities;
using DriverService.Domain.Entities.Common;
using Microsoft.EntityFrameworkCore;

namespace DriverService.Persistence.DatabaseContext;

public class DriverDatabaseContext(DbContextOptions<DriverDatabaseContext> options) : DbContext(options)
{
    public DbSet<DriverEntity> Drivers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(DriverDatabaseContext).Assembly);
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new())
    {
        foreach (var entry in base.ChangeTracker.Entries<BaseEntity>()
                     .Where(q => q.State is EntityState.Added or EntityState.Modified))
        {
            entry.Entity.UpdatedAt = DateTime.Now;
            
            if (entry.State == EntityState.Added)
            {
                entry.Entity.CreatedAt = DateTime.Now;
            }
        }
        
        return base.SaveChangesAsync(cancellationToken);
    }
}