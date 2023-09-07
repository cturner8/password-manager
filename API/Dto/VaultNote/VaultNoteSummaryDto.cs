namespace API.Dto.VaultNote;

public class VaultNoteSummaryDto
{
    public required Guid Id { get; set; }
    public required string Name { get; set; }
    public required DateTime CreatedDate { get; set; }
    public required DateTime UpdatedDate { get; set; }
}

