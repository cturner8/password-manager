namespace API.Exceptions;

public class NotFoundException : Exception
{
    public NotFoundException(string model) : base($"{model} not found.")
    {
    }
}
