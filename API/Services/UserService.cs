using API.Dto.User;
using API.Exceptions;
using Database.Context;
using Database.Models;
using Encryption.Services;
using Microsoft.Extensions.Logging;

namespace API.Services;

public class UserService
{
    private readonly VaultContext _vaultContext;
    private readonly ILogger _logger;
    private readonly UserEncryptionService _userEncryptionService;
    private readonly EncryptionService _encryptionService;


    public UserService(
        VaultContext vaultContext,
        ILogger logger,
        UserEncryptionService userEncryptionService,
        EncryptionService encryptionService
        )
    {
        _vaultContext = vaultContext;
        _logger = logger;
        _userEncryptionService = userEncryptionService;
        _encryptionService = encryptionService;

    }

    private byte[] InitialiseEncryption(string password, byte[] salt, byte[] iv)
    {
        var key = _userEncryptionService.GenerateUserKey(password, salt);
        _encryptionService.Initialise(key, iv);
        return key;
    }

    public User SignUp(SignUpDto dto)
    {
        try
        {
           var userKeyMetadata = _userEncryptionService.GenerateUserKeyMetadata(dto.Email);
            var key = InitialiseEncryption(dto.MasterPassword, userKeyMetadata.Salt, userKeyMetadata.IV);

            var user = new User()
            {
                Email = _encryptionService.EncryptString(dto.Email) ,
                Firstname = _encryptionService.EncryptString(dto.Firstname),
                Surname = _encryptionService.EncryptString(dto.Surname),
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
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            throw;
        }
    }

    public User SignIn(SignInDto dto)
    {
        var emailHash = UserEncryptionService.GenerateUserHash(dto.Email);

        var userKeyMetadata = _vaultContext.UserKeyMetadata.Where(x => x.Email == emailHash).SingleOrDefault();
        if (userKeyMetadata == null)
        {
            throw new SignInException();
        }

        var key = InitialiseEncryption(dto.MasterPassword, userKeyMetadata.Salt, userKeyMetadata.IV);
        var encryptedEmail = _encryptionService.EncryptString(dto.Email);

        var user = _vaultContext.Users.Where(x => x.Email == encryptedEmail).SingleOrDefault();
        if (user == null)
        {
            throw new SignInException();
        }

        return user;
    }
}
