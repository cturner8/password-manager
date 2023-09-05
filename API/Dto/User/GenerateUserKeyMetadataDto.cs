namespace API.Dto.User;

public class GenerateUserKeyMetadataDto
{
    public required byte[] Salt { get; set; }
    public required byte[] IV { get; set; }
}

