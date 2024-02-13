namespace Contracts.Users.Requests;

public record UserUpdatePasswordRequest(Guid Id, string OldPassword, string NewPassword );



