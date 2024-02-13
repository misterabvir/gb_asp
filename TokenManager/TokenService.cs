using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using TokenManager.Base;
using TokenManager.Security;


namespace TokenManager.Services;

public class TokenService : ITokenService
{
    private readonly JwtConfiguration _jwtConfiguration;
    public TokenService(JwtConfiguration jwtConfiguration)
    {
        _jwtConfiguration = jwtConfiguration;
    }

    public string GenerateToken(string email, string roleName)
    {
        var credentials = new SigningCredentials(
            _jwtConfiguration.PrivateKey,
            SecurityAlgorithms.RsaSha256);

        var claims = new[]
        {
            new Claim(ClaimTypes.Email, email),
            new Claim(ClaimTypes.Role, roleName)
        };

        var token = new JwtSecurityToken(
            issuer: _jwtConfiguration.Issuer,
            audience: _jwtConfiguration.Audience,
            claims: claims,
            expires: DateTime.Now.AddMinutes(_jwtConfiguration.ExpirationMinutes),
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
