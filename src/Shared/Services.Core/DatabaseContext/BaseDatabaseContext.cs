using Microsoft.EntityFrameworkCore;
using Services.Core.Entities;

namespace Services.Core.DatabaseContext;

public class BaseDatabaseContext<T>(DbContextOptions<T> options) : DbContext(options) where T : DbContext
{
    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new())
    {
        foreach (var entry in base.ChangeTracker.Entries<BaseEntity>()
                     .Where(q => q.State is EntityState.Added or EntityState.Modified))
        {
            entry.Entity.UpdatedAt = DateTime.UtcNow;
            
            if (entry.State == EntityState.Added)
            {
                entry.Entity.CreatedAt = DateTime.UtcNow;
            }
        }
        
        return base.SaveChangesAsync(cancellationToken);
    }
}