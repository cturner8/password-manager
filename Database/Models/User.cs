namespace Database.Models;

public class User : Entity
{
    public required byte[] Email { get; set; }
    public required byte[] Firstname { get; set; }
    public required byte[] Surname { get; set; }

    public required ICollection<Vault> Vaults { get; set; }
}
