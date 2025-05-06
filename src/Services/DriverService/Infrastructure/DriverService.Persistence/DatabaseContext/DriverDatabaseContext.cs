using DriverService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Services.Core.DatabaseContext;
using Services.Core.Entities;

namespace DriverService.Persistence.DatabaseContext;

public class DriverDatabaseContext(DbContextOptions<DriverDatabaseContext> options) : BaseDatabaseContext<DriverDatabaseContext>(options)
{
    public DbSet<DriverEntity> Drivers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(DriverDatabaseContext).Assembly);
    }
}