namespace Database.Models;

public class User
{
    public Guid Id { get; set; }
    public required string Email { get; set; }
    public required string Firstname { get; set; }
    public required string Surname { get; set; }
}
