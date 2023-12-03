namespace SMS.Domain.Shared;
public class Result
{
    protected internal Result(bool isSuccess, Error error)
    {
        if(isSuccess && error != Error.None || !isSuccess && error == Error.None)
        {
            throw new ArgumentException("Invalid result object", nameof(error));
        }

        IsSuccess = isSuccess;
        Error = error;
    }

    public bool IsSuccess { get; }
    public bool IsFailure => !IsSuccess;

    public Error Error { get; }
    public static Result Success() => new(true, Error.None);

    public static Result Failure(Error error) => new(false, error);

    public static Result<TValue> Success<TValue>(TValue value) => new(value, true, Error.None);
    public static Result<TValue> Create<TValue>(TValue value) => new(value, true, Error.None);
}
