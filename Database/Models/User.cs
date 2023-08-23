namespace Database.Models;

public class User : Entity
{
    public required string Email { get; set; }
    public required string Firstname { get; set; }
    public required string Surname { get; set; }

    public required ICollection<Vault> Vaults { get; set; }
}
