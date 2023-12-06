namespace SMS.Domain.Shared;
public class Result<TValue> : Result
{
    private readonly TValue? _value;

    protected internal Result(bool isSuccess, Error error) : base(isSuccess, error) { }

    protected internal Result(TValue? value, bool isSuccess, Error error)
        : base(isSuccess, error) => 
        _value = value;

    public TValue Value => IsSuccess ?
        _value :
        throw new Exception("Value of result can not be accessed");

    public static implicit operator Result<TValue>(TValue value) => Create(value);
}
