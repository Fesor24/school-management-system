namespace SMS.Domain.DomainEvents.User;
public record UserCreatedEvent(string FirstName, string LastName, string EmailAddress);
