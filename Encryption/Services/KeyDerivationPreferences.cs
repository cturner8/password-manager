using System.Security.Cryptography;

namespace Encryption.Services;


public class KeyDerivationPreferences
{
    public int Iterations { get; set; }
    public HashAlgorithmName HashAlgorithm { get; set; } = HashAlgorithmName.SHA256;
    public int KeySize { get; set; } = 32;
}

