using SMS.Domain.Primitives;
using SMS.Domain.Shared;

namespace SMS.Domain.Aggregates.UserAggregates;
public sealed class User : AggregateRoot
{
    public User() { }

    public User(Guid Id, string firstName, string lastName, Gender gender, string street, string city, string state) :
        base(Id)
    {
        FirstName = firstName;
        LastName = lastName;
        Gender = gender;
        Address = new(street, city, state);
    }

    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public Gender Gender { get; private set; }
    public Address Address { get; private set; }

    public static Result<User, Error> Create(string firstName, string lastName, Gender gender, 
        string street, string city, string state)
    {
        User user = new(Guid.NewGuid(), firstName, lastName, gender, street, city, state);

        return user;
    }
}
