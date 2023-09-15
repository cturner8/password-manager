namespace API.Dto.VaultLogin;

public class UpdateVaultLoginDto : CreateVaultLoginDto
{
    public required Guid Id { get; set; }
}
