using Database.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Database;

public class VaultContextFactory : IDesignTimeDbContextFactory<VaultContext>
{
    public VaultContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<VaultContext>();
        return new VaultContext(optionsBuilder.Options);
    }
}