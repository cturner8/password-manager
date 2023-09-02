using API.Dto.VaultLogin;
using Database.Context;
using Database.Models;
using PasswordGenerator;

namespace API.Services;

public class VaultLoginService
{
    private readonly VaultContext _vaultContext;
    private readonly VaultService _vaultService;

    public VaultLoginService(VaultContext vaultContext, VaultService vaultService)
    {
        _vaultContext = vaultContext;
        _vaultService = vaultService;
    }

    public VaultLogin Create(CreateVaultLoginDto dto)
    {
        Vault vault = _vaultService.GetUserVault(dto.UserId);
        var vaultLogin = new VaultLogin()
        {
            Name = dto.Name,
            Description = dto.Description,
            Email = dto.Email,
            Password = dto.Password,
            URL = dto.URL,
            Username = dto.Username,
            Category = dto.Category,
            Notes = dto.Notes,
            Active = true,
            Vault = vault,
            CreatedDate = DateTime.UtcNow,
            UpdatedDate = DateTime.UtcNow,
        };

        _vaultContext.VaultLogins.Add(vaultLogin);
        _vaultContext.SaveChanges();

        return vaultLogin;
    }

    public IEnumerable<VaultLogin> GetAll()
    {
        return _vaultContext.VaultLogins.AsEnumerable();
    }

    public static string GeneratePassword(GeneratePasswordDto dto)
    {
        var password = new Password(
            includeLowercase: dto.IncludeLowercase,
            includeUppercase: dto.IncludeUppercase,
            includeNumeric: dto.IncludeNumeric,
            includeSpecial: dto.IncludeSpecial,
            passwordLength: dto.Length
        );
        return password.Next();
    }
}
