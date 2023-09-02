using Encryption.Services;
using System.Security.Cryptography;

namespace UnitTests.Encryption.Services;

public class EncryptionServiceTests
{
    [Fact]
    public void EncryptsAndDecryptsData()
    {
        var aes = Aes.Create();
        var encryptionService = new EncryptionService(aes.Key, aes.IV);

        var input = "Super secret data...";

        var encryptedString = encryptionService.EncryptString(input);

        Assert.NotEmpty(encryptedString);

        var decryptedString = encryptionService.DecryptString(encryptedString);

        Assert.Equal(input, decryptedString);
    }

}
