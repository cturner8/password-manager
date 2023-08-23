using System.Security.Cryptography;

namespace Encryption.Services;

public class EncryptionService
{
    private readonly byte[] _key;
    private readonly byte[] _iv;


    public EncryptionService(byte[] key, byte[] iv)
    {
        _key = key;
        _iv = iv;
    }

    public byte[] EncryptString(string plainText)
    {
        // Check arguments.
        if (plainText == null || plainText.Length <= 0)
            throw new ArgumentNullException("plainText");
        if (_key == null || _key.Length <= 0)
            throw new ArgumentNullException("Key");
        if (_iv == null || _iv.Length <= 0)
            throw new ArgumentNullException("IV");

        byte[] encrypted;

        // Create an Aes object
        // with the specified key and IV.
        using (Aes aesAlg = Aes.Create())
        {
            aesAlg.Key = _key;
            aesAlg.IV = _iv;

            // Create an encryptor to perform the stream transform.
            ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

            // Create the streams used for encryption.
            using MemoryStream msEncrypt = new();
            using CryptoStream csEncrypt = new(msEncrypt, encryptor, CryptoStreamMode.Write);
            using (StreamWriter swEncrypt = new(csEncrypt))
            {
                //Write all data to the stream.
                swEncrypt.Write(plainText);
            }
            encrypted = msEncrypt.ToArray();
        }

        // Return the encrypted bytes from the memory stream.
        return encrypted;
    }

    public string DecryptString(byte[] cipherText)
    {
        // Check arguments.
        if (cipherText == null || cipherText.Length <= 0)
            throw new ArgumentNullException("cipherText");
        if (_key == null || _key.Length <= 0)
            throw new ArgumentNullException("Key");
        if (_iv == null || _iv.Length <= 0)
            throw new ArgumentNullException("IV");

        // Declare the string used to hold
        // the decrypted text.
        string? plaintext = null;

        // Create an Aes object
        // with the specified key and IV.
        using (Aes aesAlg = Aes.Create())
        {
            aesAlg.Key = _key;
            aesAlg.IV = _iv;

            // Create a decryptor to perform the stream transform.
            ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

            // Create the streams used for decryption.
            using MemoryStream msDecrypt = new(cipherText);
            using CryptoStream csDecrypt = new(msDecrypt, decryptor, CryptoStreamMode.Read);
            using StreamReader srDecrypt = new(csDecrypt);

            // Read the decrypted bytes from the decrypting stream
            // and place them in a string.
            plaintext = srDecrypt.ReadToEnd();
        }

        // Return the decrypted plaintext from the memory stream.
        return plaintext;
    }
}



