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

    public static implicit operator string(Error error) => error.Code;
    public bool Equals(Error? other)
    {
        if (other is null) return false;

        if (!other.Code.Equals(Code) || !other.Message.Equals(Message)) return false;

        return other.Code.Equals(Code) && other.Message.Equals(Message);
    }

    public override bool Equals(object? obj)
    {
        if (obj is null) return false;

        if (obj.GetType() != GetType()) return false;

        var other = (Error)obj;

        if (!other.Code.Equals(Code) || !other.Message.Equals(Message)) return false;

        return other.Code.Equals(Code) && other.Message.Equals(Message);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Code, Message);
    }

    public static implicit operator Result(Error error) => Result.Failure(error);
}
