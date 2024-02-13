namespace Contracts.Users.Requests;

public record UserUpdateEmailRequest(Guid Id, string Email, string Password);



