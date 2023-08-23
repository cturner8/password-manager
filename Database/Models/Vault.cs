namespace Database.Models;

public class Vault : Entity
{
    public Guid UserId { get; set; }
    public required string Name { get; set; }
    public bool Active { get; set; }

    public required User User { get; set; }

    public required ICollection<VaultLogin> Logins { get; set; }
    public required ICollection<VaultNote> Notes { get; set; }
}
