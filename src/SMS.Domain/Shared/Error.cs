namespace SMS.Domain.Shared;
public class Error : IEquatable<Error>
{
    public static readonly Error None = new(string.Empty, string.Empty);
    public static readonly Error NullValue = new("Error.NullValue", "The specified result value is null");

    public Error(string code, string message)
    {
        Code = code;
        Message = message;
    }

    public string Code { get; set; }

    public string Message { get; set; }
    public bool Equals(Error other)
    {
        return other != null && other == this;
    }

    public static implicit operator Result(Error error) => Result.Failure(error);
}
