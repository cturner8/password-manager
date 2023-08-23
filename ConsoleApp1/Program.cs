using ConsoleApp1;
using System.Security.Cryptography;

Console.WriteLine("Hello, World!");

string original = "Here is some data to encrypt!";


Aes aes = Aes.Create();
var encryptionService = new EncryptionService(aes.Key, aes.IV);

Console.WriteLine($"Data to encrypt: {original}");
Console.WriteLine($"Key: {aes.Key}");
Console.WriteLine($"IV: {aes.IV}");

var encryptedString = encryptionService.EncryptString(original);

Console.WriteLine($"Encrypted data: {encryptedString}");

var decryptedString = encryptionService.DecryptString(encryptedString);

Console.WriteLine($"Decrypted data: {decryptedString}");

Console.WriteLine("Done");