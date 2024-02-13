using Microsoft.IdentityModel.Tokens;
using System.Security.Cryptography;

namespace TokenManager.Security;

public class JwtConfiguration
{
    public required string Issuer { get; set; }
    public required string Audience { get; set; }

    private RsaSecurityKey? _publicKey;
    private RsaSecurityKey? _privateKey;

    public RsaSecurityKey PublicKey
    {
        get
        {
            if(_publicKey is null)
            {
                var rsa = RSA.Create(); 
                rsa.ImportFromPem(File.ReadAllText("./rsa/public_key.pem"));
                _publicKey = new RsaSecurityKey(rsa);
            }

            return _publicKey;
        }
    }

    public RsaSecurityKey PrivateKey
    {
        get
        {
            if (_privateKey is null)
            {
                var rsa = RSA.Create();
                rsa.ImportFromPem(File.ReadAllText("./rsa/private_key.pem"));
                _privateKey = new RsaSecurityKey(rsa);
            }
            return _privateKey;
        }
    }

    public int ExpirationMinutes { get; set; }
}
