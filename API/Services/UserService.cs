using API.Dto.User;
using API.Exceptions;
using Database.Context;
using Database.Models;
using Encryption.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace API.Services;

public class UserService
{
    private readonly ILogger<UserService> _logger;
    private readonly UserEncryptionService _userEncryptionService;
    private readonly EncryptionService _encryptionService;

    private readonly IDbContextFactory<VaultContext> _contextFactory;


    public UserService(
        ILogger<UserService> logger,
        UserEncryptionService userEncryptionService,
        EncryptionService encryptionService,
        IDbContextFactory<VaultContext> contextFactory
        )
    {
        _contextFactory = contextFactory;
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

    private GetUserDto DecryptUser(User x) => new()
    {
        Id = x.Id,
        CreatedById = x.CreatedById,
        UpdatedById = x.UpdatedById,
        Email = _encryptionService.DecryptString(x.Email),
        Firstname = _encryptionService.DecryptString(x.Firstname),
        Surname = _encryptionService.DecryptString(x.Surname),
        CreatedDate = _encryptionService.DecryptDateTime(x.CreatedDate),
        UpdatedDate = _encryptionService.DecryptDateTime(x.UpdatedDate)
    };

    public GetUserDto GetUser(Guid id)
    {
        using var vaultContext = _contextFactory.CreateDbContext();
        var user = vaultContext.Users
            .AsNoTracking()
            .Select(DecryptUser)
            .SingleOrDefault(x => x.Id == id)
            ?? throw new NotFoundException("User");
        return user;
    }

    public async Task<UserResultDto> SignUp(SignUpDto dto)
    {
        try
        {
            var vaultContext = _contextFactory.CreateDbContext();

            var userKeyMetadata = await _userEncryptionService.GenerateUserKeyMetadata(dto.Email);
            var key = InitialiseEncryption(dto.MasterPassword, userKeyMetadata.Salt, userKeyMetadata.IV);

            var user = new User()
            {
                Email = _encryptionService.EncryptString(dto.Email),
                Firstname = _encryptionService.EncryptString(dto.Firstname),
                Surname = _encryptionService.EncryptString(dto.Surname),
                Vaults = new List<Vault>() { },
                CreatedDate = _encryptionService.EncryptDateTime(DateTime.UtcNow),
                UpdatedDate = _encryptionService.EncryptDateTime(DateTime.UtcNow),
            };

            // TODO
            var userVault = new Vault()
            {
                Name = _encryptionService.EncryptString("My Vault"),
                Logins = new List<VaultLogin>(),
                Notes = new List<VaultNote>(),
                CreatedDate = _encryptionService.EncryptDateTime(DateTime.UtcNow),
                UpdatedDate = _encryptionService.EncryptDateTime(DateTime.UtcNow),
                Active = true
            };

            vaultContext.Users.Add(user);
            user.Vaults.Add(userVault);

            await vaultContext.SaveChangesAsync();

            return new UserResultDto()
            {
                User = DecryptUser(user),
                Key = key,
                IV = userKeyMetadata.IV
            };
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            throw;
        }
    }

    public UserResultDto SignIn(SignInDto dto)
    {
        var vaultContext = _contextFactory.CreateDbContext();

        var emailHash = UserEncryptionService.GenerateUserHash(dto.Email);

        var userKeyMetadata = vaultContext.UserKeyMetadata.Where(x => x.Email == emailHash).SingleOrDefault() ?? throw new SignInException();
        var key = InitialiseEncryption(dto.MasterPassword, userKeyMetadata.Salt, userKeyMetadata.IV);
        var encryptedEmail = _encryptionService.EncryptString(dto.Email);

        var user = vaultContext.Users
            .AsNoTracking()
            .SingleOrDefault(x => x.Email == encryptedEmail) ?? throw new SignInException();

        return new UserResultDto()
        {
            User = DecryptUser(user),
            Key = key,
            IV = userKeyMetadata.IV
        };
    }
}
