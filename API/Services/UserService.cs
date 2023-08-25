using API.Dto.User;
using Database.Context;
using Database.Models;
using System.Security.Authentication;

namespace API.Services;

public class UserService
{
    private readonly VaultContext _vaultContext;

    public UserService(VaultContext vaultContext)
    {
        _vaultContext = vaultContext;
    }

    public User Add(CreateUserDto createUserDto)
    {
        var user = new User()
        {
            Email = createUserDto.Email,
            Firstname = createUserDto.Firstname,
            Surname = createUserDto.Surname,
            Vaults = new List<Vault>(),
            CreatedDate = DateTime.UtcNow,
            UpdatedDate = DateTime.UtcNow,
        };

        _vaultContext.Users.Add(user);
        _vaultContext.SaveChanges();

        return user;
    }

    public User Authenticate(UserAuthDto userAuthDto)
    {
        var user = _vaultContext.Users.Where(x => x.Email == userAuthDto.Email).SingleOrDefault();
        if (user == null)
        {
            throw new AuthenticationException();
        }

        return user;
    }
}
