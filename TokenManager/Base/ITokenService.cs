namespace TokenManager.Base;

public interface ITokenService
{
    string GenerateToken(string email, string roleName);
}
