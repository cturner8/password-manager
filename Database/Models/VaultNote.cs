namespace Database.Models;

public class VaultNote : Entity
{
    public Guid VaultId { get; set; }
    public required string Name { get; set; }
    public required string Note { get; set; }
    public required string Description { get; set; }
    public bool Active { get; set; }

    public required Vault Vault { get; set; }
}
