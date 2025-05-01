using DriverService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Services.Core.Entities;

namespace DriverService.Persistence.DatabaseContext;

public class DriverDatabaseContext(DbContextOptions<DriverDatabaseContext> options) : DbContext(options)
{
    public DbSet<DriverEntity> Drivers { get; set; }
    
    public DbSet<ReportEntity> Reports { get; set; }

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