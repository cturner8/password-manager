using API.Dto.VaultNote;
using Database.Context;
using Database.Models;

namespace API.Services;

public class VaultNoteService
{
    private readonly VaultContext _vaultContext;
    private readonly VaultService _vaultService;


    public VaultNoteService(VaultContext vaultContext, VaultService vaultService)
    {
        _vaultContext = vaultContext;
        _vaultService = vaultService;
    }

    public VaultNote Create(CreateVaultNoteDto dto)
    {
        Vault vault = _vaultService.GetUserVault(dto.UserId);
        var vaultNote = new VaultNote()
        {
            Name = dto.Name,
            Description = dto.Description,
            Note = dto.Note,
            Active = true,
            Vault = vault,
            CreatedDate = DateTime.UtcNow,
            UpdatedDate = DateTime.UtcNow,
        };

        _vaultContext.VaultNotes.Add(vaultNote);
        _vaultContext.SaveChanges();

        return vaultNote;
    }
}
