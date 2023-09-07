using Database.Context;
using Database.Models;
using Encryption.Services;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace API.Services;


public class UserEncryptionService
{
    private readonly KeyDerivationService _keyDerivationService;
    //private readonly VaultContext _vaultContext;

    private readonly IDbContextFactory<VaultContext> _contextFactory;


    public UserEncryptionService(
        KeyDerivationService keyDerivationService,
        //VaultContext vaultContext,
        IDbContextFactory<VaultContext> contextFactory

        )
    {
        _keyDerivationService = keyDerivationService;
        //_vaultContext = vaultContext;
        _contextFactory = contextFactory;

    }

    public byte[] GenerateUserKey(string password, byte[] salt)
    {
        return _keyDerivationService.GenerateKey(password, salt);
    }

    public UserKeyMetadata GenerateUserKeyMetadata(string email)
    {
        using var vaultContext = _contextFactory.CreateDbContext();

        var aes = Aes.Create();
        var salt = KeyDerivationService.GenerateSalt(16);

        var emailHash = GenerateUserHash(email);

        var userKeyMetadata = new UserKeyMetadata()
        {
            Email = emailHash,
            Salt = salt,
            IV = aes.IV
        };

        vaultContext.UserKeyMetadata.Add(userKeyMetadata);
        vaultContext.SaveChanges();

        return userKeyMetadata;
    }

    public static byte[] GenerateUserHash(string email)
    {
        var emailBytes = Encoding.ASCII.GetBytes(email);
        return MD5.HashData(emailBytes);
    }

}

