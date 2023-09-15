using API.Dto.VaultLogin;
using API.Exceptions;
using Database.Context;
using Database.Models;
using Encryption.Services;
using Microsoft.EntityFrameworkCore;
using PasswordGenerator;

namespace API.Services;

public class VaultLoginService
{
    private readonly VaultService _vaultService;
    private readonly EncryptionService _encryptionService;
    private readonly IDbContextFactory<VaultContext> _contextFactory;


    public VaultLoginService(
        VaultService vaultService,
        EncryptionService encryptionService,
        IDbContextFactory<VaultContext> contextFactory
        )
    {
        _contextFactory = contextFactory;
        _vaultService = vaultService;
        _encryptionService = encryptionService;
    }

    public async Task<VaultLogin> Create(CreateVaultLoginDto dto)
    {
        var vaultContext = _contextFactory.CreateDbContext();

        Vault vault = _vaultService.GetUserVault(dto.UserId);
        var vaultLogin = new VaultLogin()
        {
            VaultId = vault.Id,
            Name = _encryptionService.EncryptString(dto.Name),
            Description = dto.Description != null ? _encryptionService.EncryptString(dto.Description) : null,
            Email = _encryptionService.EncryptString(dto.Email),
            Password = _encryptionService.EncryptString(dto.Password),
            URL = _encryptionService.EncryptString(dto.URL),
            Username = dto.Username != null ? _encryptionService.EncryptString(dto.Username) : null,
            Category = dto.Category != null ? _encryptionService.EncryptString(dto.Category) : null,
            Notes = dto.Notes != null ? _encryptionService.EncryptString(dto.Notes) : null,
            Active = true,
            CreatedDate = _encryptionService.EncryptDateTime(DateTime.UtcNow),
            UpdatedDate = _encryptionService.EncryptDateTime(DateTime.UtcNow),
        };

        vaultContext.VaultLogins.Add(vaultLogin);
        await vaultContext.SaveChangesAsync();

        return vaultLogin;
    }


    public async Task<VaultLogin> Update(UpdateVaultLoginDto dto)
    {
        var vaultContext = _contextFactory.CreateDbContext();

        Vault vault = _vaultService.GetUserVault(dto.UserId);

        var vaultLogin = vaultContext.VaultLogins
            .SingleOrDefault(x => x.Id == dto.Id && x.Vault == vault)
            ?? throw new NotFoundException("VaultLogin");

        vaultLogin.Name = _encryptionService.EncryptString(dto.Name);
        vaultLogin.Email = _encryptionService.EncryptString(dto.Email);
        vaultLogin.Password = _encryptionService.EncryptString(dto.Password);
        vaultLogin.URL = _encryptionService.EncryptString(dto.URL);
        vaultLogin.UpdatedDate = _encryptionService.EncryptDateTime(DateTime.UtcNow);

        if (dto.Description != null)
        {
            vaultLogin.Description = _encryptionService.EncryptString(dto.Description);
        }
        if (dto.Username != null)
        {
            vaultLogin.Username = _encryptionService.EncryptString(dto.Username);
        }
        if (dto.Username != null)
        {
            vaultLogin.Username = _encryptionService.EncryptString(dto.Username);
        }
        if (dto.Category != null)
        {
            vaultLogin.Category = _encryptionService.EncryptString(dto.Category);
        }
        if (dto.Notes != null)
        {
            vaultLogin.Notes = _encryptionService.EncryptString(dto.Notes);
        }

        await vaultContext.SaveChangesAsync();

        return vaultLogin;
    }

    public IEnumerable<VaultLoginSummaryDto> GetAll(GetUserLoginsDto dto)
    {
        var vaultContext = _contextFactory.CreateDbContext();

        Vault vault = _vaultService.GetUserVault(dto.UserId);
        return vaultContext.VaultLogins
            .AsNoTracking()
            .Where(x => x.Vault == vault)
            .Select(x => new VaultLoginSummaryDto()
            {
                Id = x.Id,
                Email = _encryptionService.DecryptString(x.Email),
                Name = _encryptionService.DecryptString(x.Name),
                URL = _encryptionService.DecryptString(x.URL),
                Description = x.Description != null ? _encryptionService.DecryptString(x.Description) : null,
                CreatedDate = _encryptionService.DecryptDateTime(x.CreatedDate),
                UpdatedDate = _encryptionService.DecryptDateTime(x.UpdatedDate)
            });
    }

    public VaultLoginDetailsDto Get(GetVaultLoginDto dto)
    {
        var vaultContext = _contextFactory.CreateDbContext();

        Vault vault = _vaultService.GetUserVault(dto.UserId);
        var vaultLoginDetails = vaultContext.VaultLogins
            .AsNoTracking()
            .Where(x => x.Vault == vault && x.Id == dto.Id)
            .Select(x => new VaultLoginDetailsDto()
            {
                Id = x.Id,
                Name = _encryptionService.DecryptString(x.Name),
                Description = x.Description != null ? _encryptionService.DecryptString(x.Description) : null,
                Email = _encryptionService.DecryptString(x.Email),
                Password = _encryptionService.DecryptString(x.Password),
                URL = _encryptionService.DecryptString(x.URL),
                Username = x.Username != null ? _encryptionService.DecryptString(x.Username) : null,
                Category = x.Category != null ? _encryptionService.DecryptString(x.Category) : null,
                Notes = x.Notes != null ? _encryptionService.DecryptString(x.Notes) : null,
                CreatedDate = _encryptionService.DecryptDateTime(x.CreatedDate),
                UpdatedDate = _encryptionService.DecryptDateTime(x.UpdatedDate)
            })
            .SingleOrDefault() ?? throw new NotFoundException("VaultLogin");

        return vaultLoginDetails;
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
