using API.Dto.Vault;

namespace API.Dto.VaultLogin;

public class GetVaultLoginDto : UserVaultDto
{
    public Guid Id { get; set; }
}

