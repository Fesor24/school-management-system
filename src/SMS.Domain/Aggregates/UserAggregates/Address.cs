using SMS.Domain.Primitives;

namespace SMS.Domain.Aggregates.UserAggregates;
public sealed class Address : ValueObject
{
    public Address(string street, string city, string state)
    {
        Street = street;
        City = city;
        State = state;
    }

    public string Street { get; private set; }
    public string City { get; private set; }
    public string State { get; private set; }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Street;
        yield return City;
        yield return State;
    }
}
