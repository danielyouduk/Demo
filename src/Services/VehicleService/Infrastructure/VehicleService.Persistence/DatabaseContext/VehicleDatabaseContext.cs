using Microsoft.EntityFrameworkCore;
using Services.Core.DatabaseContext;
using VehicleService.Domain.Entities;

namespace VehicleService.Persistence.DatabaseContext;

public class VehicleDatabaseContext(DbContextOptions<VehicleDatabaseContext> options) : BaseDatabaseContext<VehicleDatabaseContext>(options)
{
    public DbSet<VehicleEntity> Vehicles { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(VehicleDatabaseContext).Assembly);
    }
}