namespace Encryption.Exceptions;

public class DecryptionException : Exception
{
    public DecryptionException(string type) : base($"Error decrypting {type} data.")
    {
    }
}

