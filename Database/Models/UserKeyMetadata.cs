using Microsoft.EntityFrameworkCore;

namespace Database.Models;

[Index(nameof(Email), IsUnique = true)]
public class UserKeyMetadata
{
    public Guid Id { get; set; }
    public required byte[] Email { get; set; }
    public required byte[] Salt { get; set; }
    public required byte[] IV { get; set; }
}
