namespace Contracts.Users.Responses;

public record UserAuthResponse(Guid Id, string Email, string Role, string Token);