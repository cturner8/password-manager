using API.Dto.VaultNote;
using API.Exceptions;
using Database.Context;
using Database.Models;
using Encryption.Services;
using Microsoft.EntityFrameworkCore;

namespace API.Services;

public class VaultNoteService
{
    private readonly VaultService _vaultService;
    private readonly EncryptionService _encryptionService;
    private readonly IDbContextFactory<VaultContext> _contextFactory;


    public VaultNoteService(
        VaultService vaultService,
        EncryptionService encryptionService,
        IDbContextFactory<VaultContext> contextFactory

    )
    {
        _contextFactory = contextFactory;
        _vaultService = vaultService;
        _encryptionService = encryptionService;
    }

    public async Task<VaultNote> Create(CreateVaultNoteDto dto)
    {
        var vaultContext = _contextFactory.CreateDbContext();

        Vault vault = _vaultService.GetUserVault(dto.UserId);
        var vaultNote = new VaultNote()
        {
            VaultId = vault.Id,
            Name = _encryptionService.EncryptString(dto.Name),
            Description = dto.Description != null ? _encryptionService.EncryptString(dto.Description) : null,
            Note = _encryptionService.EncryptString(dto.Note),
            Active = true,
            CreatedDate = _encryptionService.EncryptDateTime(DateTime.UtcNow),
            UpdatedDate = _encryptionService.EncryptDateTime(DateTime.UtcNow),
        };

        vaultContext.VaultNotes.Add(vaultNote);
        await vaultContext.SaveChangesAsync();

        return vaultNote;
    }

    public IEnumerable<VaultNoteSummaryDto> GetAll(GetUserVaultNotesDto dto)
    {
        var vaultContext = _contextFactory.CreateDbContext();

        Vault vault = _vaultService.GetUserVault(dto.UserId);
        return vaultContext.VaultNotes
            .AsNoTracking()
            .Where(x => x.Vault == vault)
            .Select(x => new VaultNoteSummaryDto()
            {
                Id = x.Id,
                Name = _encryptionService.DecryptString(x.Name),
                CreatedDate = _encryptionService.DecryptDateTime(x.CreatedDate),
                UpdatedDate = _encryptionService.DecryptDateTime(x.UpdatedDate)
            });
    }

    public VaultNoteDetailsDto Get(GetVaultNoteDto dto)
    {
        var vaultContext = _contextFactory.CreateDbContext();

        Vault vault = _vaultService.GetUserVault(dto.UserId);
        var vaultNoteDetails = vaultContext.VaultNotes
            .AsNoTracking()
            .Where(x => x.Vault == vault && x.Id == dto.Id)
            .Select(x => new VaultNoteDetailsDto()
            {
                Id = x.Id,
                Name = _encryptionService.DecryptString(x.Name),
                Description = x.Description != null ? _encryptionService.DecryptString(x.Description) : null,
                Note = _encryptionService.DecryptString(x.Note),
                CreatedDate = _encryptionService.DecryptDateTime(x.CreatedDate),
                UpdatedDate = _encryptionService.DecryptDateTime(x.UpdatedDate)
            })
            .SingleOrDefault() ?? throw new NotFoundException("VaultNote");

        return vaultNoteDetails;
    }
}
