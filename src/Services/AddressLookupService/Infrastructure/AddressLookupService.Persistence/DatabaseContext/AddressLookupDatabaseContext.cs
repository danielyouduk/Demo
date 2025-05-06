using AddressLookupService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Services.Core.DatabaseContext;

namespace AddressLookupService.Persistence.DatabaseContext;

public class AddressLookupDatabaseContext(DbContextOptions<AddressLookupDatabaseContext> options) : BaseDatabaseContext<AddressLookupDatabaseContext>(options)
{
    public DbSet<AddressEntity> Addresses { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AddressLookupDatabaseContext).Assembly);
    }
}