namespace Database.Models;

public class VaultNote : Entity
{
    public Guid VaultId { get; set; }
    public required byte[] Name { get; set; }
    public required byte[] Note { get; set; }
    public byte[]? Description { get; set; }
    public bool Active { get; set; }

    public required Vault Vault { get; set; }
}
