namespace Database.Models;

public class VaultLogin : Entity
{
    public Guid VaultId { get; set; }
    public required string Name { get; set; }
    public required string Description { get; set; }
    public required string URL { get; set; }
    public required string Email { get; set; }
    public required string Username { get; set; }
    public required string Password { get; set; }
    public required string Notes { get; set; }
    public required string Category { get; set; }
    public bool Active { get; set; }

    public required Vault Vault { get; set; }
}
