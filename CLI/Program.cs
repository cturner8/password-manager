using API.Dto.User;
using API.Services;
using Database.Context;
using Encryption.Services;
using System.Security.Cryptography;

var keyDerivationService = new KeyDerivationService(1000, HashAlgorithmName.SHA256, 32);
var encryptionService = new EncryptionService();
var vaultContext = new VaultContext();

var masterPassword = VaultLoginService.GeneratePassword(new API.Dto.VaultLogin.GeneratePasswordDto());
var signInDto = new SignInDto()
{
    Email = "john.doe@contoso.com",
    MasterPassword = masterPassword,
    //Firstname = "John",
    //Surname = "Doe"
};


try
{
    vaultContext.UserKeyMetadata.RemoveRange(vaultContext.UserKeyMetadata);
    vaultContext.SaveChanges();

    Console.WriteLine($"Encrypting for user {signInDto.Email}");

    var salt = KeyDerivationService.GenerateSalt(16);
    var encryptionKey = keyDerivationService.GenerateKey(signInDto.MasterPassword, salt);

    Console.WriteLine("Initialised encryption key");

    var aes = Aes.Create();
    aes.Key = encryptionKey;

    encryptionService.Initialise(encryptionKey, aes.IV);

    Console.WriteLine("Initialised encryption service");

    var encrypted = encryptionService.EncryptString(signInDto.Email);

    Console.WriteLine("Encrypted user email");

    vaultContext.UserKeyMetadata.Add(new Database.Models.UserKeyMetadata()
    {
        Email = signInDto.Email,
        Salt = salt,
        IV = aes.IV
    });
    vaultContext.SaveChanges();

    var metadataCount = vaultContext.UserKeyMetadata.Count(x => x.Salt == salt && x.IV == aes.IV);

    Console.WriteLine($"count from db metadata {metadataCount}");

    var metadata = vaultContext.UserKeyMetadata.SingleOrDefault(x => x.Email == signInDto.Email);
    if (metadata == null)
    {
        throw new ArgumentException("Decryption error");
    }

    var decryptionKey = keyDerivationService.GenerateKey(signInDto.MasterPassword, metadata.Salt);

    Console.WriteLine("Initialised decryption key");

    encryptionService.Initialise(decryptionKey, metadata.IV);

    var decrypted = encryptionService.DecryptString(encrypted);

    Console.WriteLine($"Decrypted user email {decrypted}");
}
catch (Exception e)
{
    Console.WriteLine(e.Message);
}
