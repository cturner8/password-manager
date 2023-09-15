namespace Database.Models;

public class Vault : Entity
{
    public Guid UserId { get; set; }
    public required byte[] Name { get; set; }
    public bool Active { get; set; }

    public User? User { get; set; }

    public required ICollection<VaultLogin> Logins { get; set; }
    public required ICollection<VaultNote> Notes { get; set; }
}
