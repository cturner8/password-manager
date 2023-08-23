using Database.Context;
using Database.Models;

namespace PasswordManager.Services;

public class UserService
{
    private readonly VaultContext _vaultContext;

    public UserService(VaultContext vaultContext)
    {
        _vaultContext = vaultContext;
    }

    public Task<IEnumerable<User>> GetUsers()
    {
        return Task.FromResult(_vaultContext.Users.OrderBy(x => x.Email).AsEnumerable());
    }
}
