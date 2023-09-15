
namespace API.Dto.User;

public class UserResultDto
{
    public required GetUserDto User { get; set; }
    public required byte[] Key { get; set; }
    public required byte[] IV { get; set; }
}
