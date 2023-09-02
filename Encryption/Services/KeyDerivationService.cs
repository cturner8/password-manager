using System.Security.Cryptography;
using System.Text;

namespace Encryption.Services;

public class KeyDerivationService
{
    private readonly int _iterations;
    private readonly HashAlgorithmName _algorithm;
    private readonly int _outputKeyLength;

    public KeyDerivationService(int iterations, HashAlgorithmName algorithm, int outputKeyLength)
    {
        _iterations = iterations;
        _algorithm = algorithm;
        _outputKeyLength = outputKeyLength;
    }

    public byte[] GenerateKey(string password, byte[] salt)
    {
        var passwordBytes = Encoding.ASCII.GetBytes(password);
        return Rfc2898DeriveBytes.Pbkdf2(passwordBytes, salt, _iterations, _algorithm, _outputKeyLength);
    }

    public static byte[] GenerateSalt(int length)
    {
        return RandomNumberGenerator.GetBytes(length);
    }
}
