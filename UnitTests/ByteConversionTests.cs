using System.Security.Cryptography;
using System.Text;

namespace UnitTests;

public class ByteConversionTests
{
    [Fact]
    public void CanConvertToAndFromString()
    {
        var startString = "ABC123";

        var bytes = Encoding.Default.GetBytes(startString);
        var outString = Encoding.Default.GetString(bytes);

        Assert.Equal(startString, outString);

        var outBytes = Encoding.Default.GetBytes(outString);

        Assert.Equal(bytes, outBytes);
    }

    [Fact]
    public void CanConvertEncryptionKeyToAndFromString()
    {
        using var aes = Aes.Create();

        var key = aes.Key;

        var keyString = Convert.ToBase64String(key);

        var outKey = Convert.FromBase64String(keyString);

        Assert.Equal(key, outKey);
    }
}
