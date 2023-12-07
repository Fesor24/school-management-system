namespace SMS.Domain.Exceptions.BaseExceptions;
public class BadRequestException : Exception
{
    public BadRequestException(string message) : base(message)
    {
        
    }
}
