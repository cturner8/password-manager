namespace API.Dto.VaultNote;

public class CreateVaultNoteDto
{
    public Guid UserId { get; set; }
    public required string Name { get; set; }
    public required string Note { get; set; }
    public string? Description { get; set; }
}
