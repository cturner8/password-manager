using API.Dto.Vault;

namespace API.Dto.VaultNote;

public class GetVaultNoteDto : UserVaultDto
{
    public Guid Id { get; set; }
}

