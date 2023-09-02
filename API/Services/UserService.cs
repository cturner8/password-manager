using API.Dto.User;
using API.Exceptions;
using Database.Context;
using Database.Models;

namespace API.Services;

public class UserService
{
    private readonly VaultContext _vaultContext;

    public UserService(VaultContext vaultContext)
    {
        _vaultContext = vaultContext;
    }

    public User SignUp(SignUpDto dto)
    {
        var user = new User()
        {
            Email = dto.Email,
            Firstname = dto.Firstname,
            Surname = dto.Surname,
            Vaults = new List<Vault>() { },
            CreatedDate = DateTime.UtcNow,
            UpdatedDate = DateTime.UtcNow,
        };
        var userVault = new Vault()
        {
            Name = "My Vault",
            Logins = new List<VaultLogin>(),
            Notes = new List<VaultNote>(),
            User = user,
            CreatedDate = DateTime.UtcNow,
            UpdatedDate = DateTime.UtcNow,
            Active = true
        };

        _vaultContext.Users.Add(user);
        user.Vaults.Add(userVault);

        _vaultContext.SaveChanges();

        return user;
    }

    public User SignIn(SignInDto dto)
    {
        var user = _vaultContext.Users.Where(x => x.Email == dto.Email).SingleOrDefault();
        if (user == null)
        {
            throw new SignInException();
        }

        return user;
    }
}
