using API.Dto.User;
using API.Services;
using Database.Context;

var vaultContext = new VaultContext();
var userService = new UserService(vaultContext);

var dto = new UserAuthDto()
{
    Email = "john.doe@contoso.com",
    MasterPassword = ""
};


try
{
    var user = userService.Authenticate(dto);
    Console.WriteLine(user.Id);
}
catch (Exception e)
{
    Console.WriteLine(e.Message);
}
