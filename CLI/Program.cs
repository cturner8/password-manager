using API.Dto.User;
using API.Services;
using Database.Context;
using Encryption.Services;
using Microsoft.Extensions.Logging;
using System.Security.Cryptography;

ILogger logger = LoggerFactory.Create((options) => {}).CreateLogger<Program>();

var keyDerivationService = new KeyDerivationService(1000, HashAlgorithmName.SHA256, 32);
var encryptionService = new EncryptionService();
var vaultContext = new VaultContext();
var userEncryptionService = new UserEncryptionService(keyDerivationService, vaultContext);
var userService = new UserService(vaultContext, logger, userEncryptionService, encryptionService) ;

var masterPassword = VaultLoginService.GeneratePassword(new API.Dto.VaultLogin.GeneratePasswordDto());
var signUpDto = new SignUpDto()
{
    Email = "john.doe@contoso.com",
    MasterPassword = masterPassword,
    Firstname = "John",
    Surname = "Doe"
};

var signInDto = new SignInDto() {
    Email = signUpDto.Email,
    MasterPassword = signUpDto.MasterPassword
};

try
{

    var user = userService.SignUp(signUpDto);
    var loggedInUser = userService.SignIn(signInDto);

    Console.WriteLine("done");
}
catch (Exception e)
{
    Console.WriteLine(e.Message);
}
