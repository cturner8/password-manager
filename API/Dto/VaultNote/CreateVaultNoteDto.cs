using API.Dto.Vault;

namespace API.Dto.VaultNote;

public class CreateVaultNoteDto : UserVaultDto
{
    public required string Name { get; set; }
    public required string Note { get; set; }
    public string? Description { get; set; }
}
