namespace API.Dto.VaultNote;

public class VaultNoteDetailsDto
{
    public required Guid Id { get; set; }
    public required string Name { get; set; }
    public required string Note { get; set; }
    public string? Description { get; set; }
    public required DateTime CreatedDate { get; set; }
    public required DateTime UpdatedDate { get; set; }
}

