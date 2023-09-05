namespace Database.Models;

public class Entity
{
    public Guid Id { get; set; }
    public required byte[] CreatedDate { get; set; }
    public required byte[] UpdatedDate { get; set; }
    public Guid CreatedById { get; set; }
    public Guid UpdatedById { get; set; }
}
