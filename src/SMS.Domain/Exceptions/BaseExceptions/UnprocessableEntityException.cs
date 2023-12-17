namespace SMS.Domain.Exceptions.BaseExceptions;
public class UnprocessableEntityException : Exception
{
    public IReadOnlyDictionary<string, string[]> Errors { get;}

    public UnprocessableEntityException(IReadOnlyDictionary<string, string[]> errors) : 
        base("One or more validation error(s)") =>
        Errors = errors;
}
