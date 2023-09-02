namespace API.Dto.User;

public class SignInDto
{
    public required string Email { get; set; }
    public required string MasterPassword { get; set; }
}
