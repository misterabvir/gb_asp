using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using UsersApi.BusinessLogicalLayer.Services.Base;

namespace UsersApi.BusinessLogicalLayer.Services;

public class EncryptService : IEncryptService
{
    public byte[] GenerateSalt() => Guid.NewGuid().ToByteArray();

    public byte[] HashPassword(string password, byte[] salt) =>
        KeyDerivation.Pbkdf2(
            password: password,
            salt: salt,
            prf: KeyDerivationPrf.HMACSHA512,
            iterationCount: 10000,
            numBytesRequested: 512 / 8);
}