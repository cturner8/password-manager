using API.Exceptions;
using Database.Context;
using Database.Models;

namespace API.Services;

public class VaultService
{
    private readonly VaultContext _vaultContext;

    public VaultService(VaultContext vaultContext)
    {
        _vaultContext = vaultContext;
    }

    public Vault GetUserVault(Guid userId)
    {
        var userVault = _vaultContext.Vaults
            .Where(x => x.UserId == userId)
            .SingleOrDefault() ?? throw new NotFoundException("Vault");
        return userVault;
    }
}
