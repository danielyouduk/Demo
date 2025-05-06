using AccountService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Services.Core.DatabaseContext;

namespace AccountService.Persistence.DatabaseContext;

public class AccountDatabaseContext(DbContextOptions<AccountDatabaseContext> options) : BaseDatabaseContext<AccountDatabaseContext>(options)
{
    public DbSet<Account> Accounts { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AccountDatabaseContext).Assembly);
    }
}