using API.Dto.Vault;

namespace API.Dto.VaultNote;

public class GetUserVaultNotesDto : UserVaultDto
{
    public Guid Id { get; set; }
}

