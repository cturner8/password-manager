namespace Database.Models;

public class VaultLogin : Entity
{
    public Guid VaultId { get; set; }
    public required byte[] Name { get; set; }
    public byte[]? Description { get; set; }
    public required byte[] URL { get; set; }
    public required byte[] Email { get; set; }
    public byte[]? Username { get; set; }
    public required byte[] Password { get; set; }
    public byte[]? Notes { get; set; }
    public byte[]? Category { get; set; }
    public bool Active { get; set; }

    public required Vault Vault { get; set; }
}
