using Contracts.Users.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UsersApi.BusinessLogicalLayer.Services.Base;

namespace UsersApi.Controllers;

[Route("users-api")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;

    public UsersController(IUserService userService)
    {
        _userService = userService;
    }


    [Authorize(Roles = "Administrator")]
    [HttpGet(template: "get-all")]
    public async Task<IActionResult> GetAll()
    {
        var response = await _userService.GetAll();
        return Ok(response);
    }

    [Authorize(Roles = "Administrator")]
    [HttpGet(template: "get-admins-all")]
    public async Task<IActionResult> GetAdminsAll()
    {
        var response = await _userService.GetAdminsAll();
        return Ok(response);
    }

    [Authorize(Roles = "Administrator, User")]
    [HttpGet(template: "get-not-admins-all")]
    public async Task<IActionResult> GetNotAdminsAll()
    {
        var response = await _userService.GetNotAdminsAll();
        return Ok(response);
    }


    [Authorize(Roles = "Administrator")]
    [HttpGet(template: "get-by-id")]
    public async Task<IResult> GetById([FromQuery] UserGetByIdRequest request)
    {
        var response = await _userService.GetById(request);
        return response;
    }

    [Authorize(Roles = "Administrator")]
    [HttpGet(template: "get-by-email")]
    public async Task<IResult> GetByEmail([FromQuery] UserGetByEmailRequest request)
    {
        var response = await _userService.GetByEmail(request);
        return response;
    }

    [AllowAnonymous]
    [HttpPost(template: "registration")]
    public async Task<IResult> Registration(UserAuthRequest request)
    {
        var response = await _userService.Registration(request);
        return response;
    }

    [AllowAnonymous]
    [HttpPost(template: "login")]
    public async Task<IResult> Login(UserAuthRequest request)
    {
        var response = await _userService.Login(request);
        return response;
    }

    [Authorize(Roles = "Administrator, User")]
    [HttpPut(template: "update-email")]
    public async Task<IResult> UpdateEmail(UserUpdateEmailRequest request)
    {
        var response = await _userService.UpdateEmail(request);
        return response;
    }

    [Authorize(Roles = "Administrator, User")]
    [HttpPut(template: "update-password")]
    public async Task<IResult> UpdatePassword(UserUpdatePasswordRequest request)
    {
        var response = await _userService.UpdatePassword(request);
        return response;
    }


    [Authorize(Roles = "Administrator")]
    [HttpPut(template: "set-admin-rights")]
    public async Task<IResult> SetAdminRights(UserSetAdminRightsRequest request)
    {
        var response = await _userService.UpdateSetAdminRights(request);
        return response;
    }

    [Authorize(Roles = "Administrator")]
    [HttpPut(template: "remove-admin-rights")]
    public async Task<IResult> SetAdminRights(UserRemoveAdminRightsRequest request)
    {
        var response = await _userService.UpdateRemoveAdminRights(request);
        return response;
    }

    [Authorize(Roles = "Administrator")]
    [HttpDelete(template: "delete")]
    public async Task<IResult> Delete(UserDeleteRequest request)
    {
        var response = await _userService.Delete(request);
        return response;
    }
}
