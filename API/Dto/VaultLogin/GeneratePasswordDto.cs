namespace API.Dto.VaultLogin;

public class GeneratePasswordDto
{
    public int Length { get; set; } = 16;
    public bool IncludeLowercase { get; set; } = true;
    public bool IncludeUppercase { get; set; } = true;
    public bool IncludeNumeric { get; set; } = true;
    public bool IncludeSpecial { get; set; } = true;
}
