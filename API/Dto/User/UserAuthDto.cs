namespace API.Dto.User;

public class UserAuthDto
{
    public required string Email { get; set; }
    public required string MasterPassword { get; set; }
}
