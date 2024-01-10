using Microsoft.AspNetCore.Identity;
using SMS.Domain.Shared;

namespace SMS.Domain.Aggregates.UserAggregates;
public sealed class User : IdentityUser<Guid>
{
    public User() { }

    public User(string firstName, string lastName, Gender gender, string street, string city, string state,
        string email, string userName)
    {
        FirstName = firstName;
        LastName = lastName;
        Gender = gender;
        Address = new(street, city, state);
        Email = email;
        UserName = userName;
        NormalizedEmail = email.ToUpperInvariant();
        NormalizedUserName = userName.ToUpperInvariant();
    }

    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public string RefreshToken { get; private set; }
    public DateTime RefreshTokenExpiry { get; private set; }
    public Gender Gender { get; private set; }
    public Address Address { get; private set; }

    public static Result<User, Error> Create(string firstName, string lastName, Gender gender, 
        string street, string city, string state, string email, string userName)
    {
        User user = new(firstName, lastName, gender, street, city, state, email, userName);

        return user;
    }
}
