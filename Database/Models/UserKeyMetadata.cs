using Microsoft.EntityFrameworkCore;

namespace Database.Models;

[Index(nameof(Email))]
public class UserKeyMetadata
{
    public Guid Id { get; set; }
    public required string Email { get; set; }
    public required byte[] Salt { get; set; }
    public required byte[] IV { get; set; }
}
