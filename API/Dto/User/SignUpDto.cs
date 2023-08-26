namespace API.Dto.User;

public class SignUpDto
{
    public required string Email { get; set; }
    public required string Firstname { get; set; }
    public required string Surname { get; set; }
    public required string MasterPassword { get; set; }
}
