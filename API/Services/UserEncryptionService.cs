using System.Security.Cryptography;
using System.Text;
using Database.Context;
using Database.Models;
using Encryption.Services;

namespace API.Services;


public class UserEncryptionService
{
	private readonly KeyDerivationService _keyDerivationService;
	private readonly VaultContext _vaultContext;


	public UserEncryptionService(KeyDerivationService	keyDerivationService, VaultContext vaultContext)
	{
		_keyDerivationService = keyDerivationService;
		_vaultContext = vaultContext;
	}

	public byte[] GenerateUserKey(string password, byte[] salt)
	{
        return _keyDerivationService.GenerateKey(password, salt);
    }

	public UserKeyMetadata GenerateUserKeyMetadata(string email)
	{
        var aes = Aes.Create();
        var salt = KeyDerivationService.GenerateSalt(16);

		var emailHash = GenerateUserHash(email);

		var userKeyMetadata = new UserKeyMetadata() {
			Email = emailHash,
			Salt = salt,
			IV = aes.IV
		};

		_vaultContext.UserKeyMetadata.Add(userKeyMetadata);
		_vaultContext.SaveChanges();

		return userKeyMetadata;
    }

	public static byte[] GenerateUserHash(string email)
	{
        var emailBytes = Encoding.ASCII.GetBytes(email);
        return MD5.HashData(emailBytes);
    }

}

