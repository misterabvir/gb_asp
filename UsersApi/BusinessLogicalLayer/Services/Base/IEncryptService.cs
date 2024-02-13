namespace UsersApi.BusinessLogicalLayer.Services.Base;

public interface IEncryptService
{
    byte[] GenerateSalt();
    byte[] HashPassword(string password, byte[] salt);
}
