namespace API.Dto.User;

public class CreateUserDto
{
    public required string Email { get; set; }
    public required string Firstname { get; set; }
    public required string Surname { get; set; }
}

