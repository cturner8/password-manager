using API.Dto.User;
using API.Dto.VaultLogin;
using API.Dto.VaultNote;
using API.Services;
using Bogus;
using Database.Context;
using Microsoft.EntityFrameworkCore;

var vaultContext = new VaultContext(new DbContextOptions<VaultContext> { });

var userService = new UserService(vaultContext);
var vaultService = new VaultService(vaultContext);
var vaultNoteService = new VaultNoteService(vaultContext, vaultService);
var vaultLoginService = new VaultLoginService(vaultContext, vaultService);


var signInDto = new SignInDto()
{
    Email = "john.doe@contoso.com",
    MasterPassword = "",
    //Firstname = "John",
    //Surname = "Doe"
};

var createVaultNoteDto = new Faker<CreateVaultNoteDto>()
    .RuleFor(x => x.Name, (f, u) => String.Join(" ", f.Lorem.Words()))
    .RuleFor(x => x.Note, (f, u) => String.Join(" ", f.Lorem.Words(16)))
    .RuleFor(x => x.Description, (f, u) => String.Join(" ", f.Lorem.Paragraph()))
    .Generate();

var createVaultLoginDto = new Faker<CreateVaultLoginDto>()
    .RuleFor(x => x.Name, (f, u) => String.Join(" ", f.Lorem.Words()))
    .RuleFor(x => x.Notes, (f, u) => String.Join(" ", f.Lorem.Words(8)))
    .RuleFor(x => x.Description, (f, u) => String.Join(" ", f.Lorem.Paragraph()))
    .RuleFor(x => x.URL, (f, u) => f.Internet.Url())
    .RuleFor(x => x.Username, (f, u) => f.Internet.UserName())
    .RuleFor(x => x.Email, (f, u) => $"{u.Username}@{f.Internet.DomainName()}")
    .RuleFor(x => x.Password, (f, u) => f.Internet.Password(f.Random.Number(8, 30)))
    .Generate();

try
{
    var user = userService.SignIn(signInDto);
    Console.WriteLine($"User: {user.Id}");

    var vault = vaultService.GetUserVault(user.Id);
    Console.WriteLine($"User Vault: {vault.Id}");

    createVaultNoteDto.UserId = user.Id;
    createVaultLoginDto.UserId = user.Id;

    var vaultNote = vaultNoteService.Create(createVaultNoteDto);
    Console.WriteLine($"Vault Note: {vaultNote.Id}");

    var vaultLogin = vaultLoginService.Create(createVaultLoginDto);
    Console.WriteLine($"Vault Login: {vaultLogin.Id}");

    var randomPassword = vaultLoginService.GeneratePassword(
        new GeneratePasswordDto
        {
            Length = 8,
            IncludeLowercase = true,
            IncludeUppercase = false,
            IncludeNumeric = false,
            IncludeSpecial = false
        }
    );

    Console.WriteLine($"Random Password: {randomPassword}");

}
catch (Exception e)
{
    Console.WriteLine(e.Message);
}
