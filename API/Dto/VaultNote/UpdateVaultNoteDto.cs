namespace API.Dto.VaultNote;

public class UpdateVaultNoteDto : CreateVaultNoteDto
{
    public required Guid Id { get; set; }
}
