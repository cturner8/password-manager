﻿using Database.Context;
using Database.Models;
using Encryption.Services;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace API.Services;


public class UserEncryptionService
{
    private readonly KeyDerivationService _keyDerivationService;

    private readonly IDbContextFactory<VaultContext> _contextFactory;


    public UserEncryptionService(
        KeyDerivationService keyDerivationService,
        IDbContextFactory<VaultContext> contextFactory
        )
    {
        _keyDerivationService = keyDerivationService;
        _contextFactory = contextFactory;

    }

    public byte[] GenerateUserKey(string password, byte[] salt)
    {
        return _keyDerivationService.GenerateKey(password, salt);
    }

    public async Task<UserKeyMetadata> GenerateUserKeyMetadata(string email)
    {
        var vaultContext = _contextFactory.CreateDbContext();

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
        await vaultContext.SaveChangesAsync();

        return userKeyMetadata;
    }

    public static byte[] GenerateUserHash(string email)
    {
        var emailBytes = Encoding.ASCII.GetBytes(email);
        return SHA256.HashData(emailBytes);
    }

}

