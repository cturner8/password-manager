using API.Dto.VaultNote;
using API.Exceptions;
using Database.Context;
using Database.Models;
using Encryption.Services;

namespace API.Services;

public class VaultNoteService
{
    private readonly VaultContext _vaultContext;
    private readonly VaultService _vaultService;
    private readonly EncryptionService _encryptionService;


    public VaultNoteService(VaultContext vaultContext, VaultService vaultService,
        EncryptionService encryptionService

        )
    {
        _vaultContext = vaultContext;
        _vaultService = vaultService;
        _encryptionService = encryptionService;

    }

    public VaultNote Create(CreateVaultNoteDto dto)
    {
        Vault vault = _vaultService.GetUserVault(dto.UserId);
        var vaultNote = new VaultNote()
        {
            Name = _encryptionService.EncryptString(dto.Name),
            Description = dto.Description != null ? _encryptionService.EncryptString(dto.Description) : null,
            Note = _encryptionService.EncryptString(dto.Note),
            Active = true,
            Vault = vault,
            CreatedDate = _encryptionService.EncryptDateTime(DateTime.UtcNow),
            UpdatedDate = _encryptionService.EncryptDateTime(DateTime.UtcNow),
        };

        _vaultContext.VaultNotes.Add(vaultNote);
        _vaultContext.SaveChanges();

        return vaultNote;
    }

    public IEnumerable<VaultNoteSummaryDto> GetAll(GetUserVaultNotesDto dto)
    {
        Vault vault = _vaultService.GetUserVault(dto.UserId);
        return _vaultContext.VaultNotes
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
        Vault vault = _vaultService.GetUserVault(dto.UserId);
        var vaultNoteDetails = _vaultContext.VaultNotes
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
