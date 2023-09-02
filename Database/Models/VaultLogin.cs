namespace Database.Models;

public class VaultLogin : Entity
{
    public Guid VaultId { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }
    public required string URL { get; set; }
    public required string Email { get; set; }
    public string? Username { get; set; }
    public required string Password { get; set; }
    public string? Notes { get; set; }
    public string? Category { get; set; }
    public bool Active { get; set; }

    public required Vault Vault { get; set; }
}
