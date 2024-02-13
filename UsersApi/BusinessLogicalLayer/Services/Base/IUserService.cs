using Contracts.Users.Requests;
using Contracts.Users.Responses;

namespace UsersApi.BusinessLogicalLayer.Services.Base;

public interface IUserService
{
    Task<IEnumerable<UserResponse>> GetAll();
    Task<IEnumerable<UserResponse>> GetAdminsAll();
    Task<IEnumerable<UserResponse>> GetNotAdminsAll();
    Task<IResult> GetById(UserGetByIdRequest request);
    Task<IResult> GetByEmail(UserGetByEmailRequest request);

    Task<IResult> Login(UserAuthRequest request);
    Task<IResult> Registration(UserAuthRequest request);

    Task<IResult> UpdateEmail(UserUpdateEmailRequest request);
    Task<IResult> UpdatePassword(UserUpdatePasswordRequest request);
    Task<IResult> UpdateSetAdminRights(UserSetAdminRightsRequest request);
    Task<IResult> UpdateRemoveAdminRights(UserRemoveAdminRightsRequest request);
    Task<IResult> Delete(UserDeleteRequest request);

}
