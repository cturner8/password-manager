namespace API.Dto.User;

public class GetUserDto
{
    public required Guid Id { get; set; }
    public required DateTime CreatedDate { get; set; }
    public required DateTime UpdatedDate { get; set; }
    public required Guid CreatedById { get; set; }
    public required Guid UpdatedById { get; set; }
    public required string Email { get; set; }
    public required string Firstname { get; set; }
    public required string Surname { get; set; }
}
