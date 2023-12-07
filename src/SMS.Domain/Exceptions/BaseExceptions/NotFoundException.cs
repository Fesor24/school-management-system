namespace SMS.Domain.Exceptions.BaseExceptions;
public class NotFoundException : Exception
{
    public NotFoundException(string message)  : base(message)
    {
        
    }
}
