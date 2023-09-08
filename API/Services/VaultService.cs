using API.Exceptions;
using Database.Context;
using Database.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Services;

public class VaultService
{
    private readonly IDbContextFactory<VaultContext> _contextFactory;


    public VaultService(IDbContextFactory<VaultContext> contextFactory)
    {
        _contextFactory = contextFactory;
    }

    public Vault GetUserVault(Guid userId)
    {
        using var vaultContext = _contextFactory.CreateDbContext();

        var userVault = vaultContext.Vaults
            .Where(x => x.UserId == userId)
            .SingleOrDefault() ?? throw new NotFoundException("Vault");
        return userVault;
    }
}
