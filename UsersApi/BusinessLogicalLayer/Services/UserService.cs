using Contracts.Users.Requests;
using Contracts.Users.Responses;
using Microsoft.EntityFrameworkCore;
using TokenManager.Base;
using UsersApi.BusinessLogicalLayer.Services.Base;
using UsersApi.DataAccessLayer.Contexts;
using UsersApi.DataAccessLayer.Models;

namespace UsersApi.BusinessLogicalLayer.Services;

public class UserService : IUserService
{
    private const string USERS = "Users";
    private const string USEREMAIL_ = "UserEmail_";
    private const string USERID_ = "UserId_";


    private readonly UserContext _context;
    private readonly ICacheService _cache;
    private readonly IEncryptService _encrypt;
    private readonly ITokenService _token;

    public UserService(UserContext context, ICacheService cache, IEncryptService encrypt, ITokenService token)
    {
        _context = context;
        _cache = cache;
        _encrypt = encrypt;
        _token = token;
    }

    public async Task<IEnumerable<UserResponse>> GetAll()
    {
        var response = await _cache.Get<IEnumerable<UserResponse>>(USERS);
        if (response is null)
        {
            var users = await _context.Users.Include(s => s.Role).AsNoTracking().ToListAsync();
            response = users.Select(s => new UserResponse(s.Email, s.Role!.Name));
            await _cache.Set(USERS, response);
        }
        return response;
    }


    public async Task<IEnumerable<UserResponse>> GetAdminsAll()
    {
        var all = await _cache.Get<IEnumerable<UserResponse>>(USERS);

        if (all is not null)
        {
            return all.Where(s => s.Role == RoleType.Administrator.ToString());
        }
        var users = await _context.Users
            .Include(s => s.Role)
            .AsNoTracking()
            .Where(s => s.Role!.RoleType == RoleType.Administrator)
            .ToListAsync();
        var response = users.Select(s => new UserResponse(s.Email, s.Role!.Name));
        return response;
    }
    public async Task<IEnumerable<UserResponse>> GetNotAdminsAll()
    {
        var all = await _cache.Get<IEnumerable<UserResponse>>(USERS);

        if (all is not null)
        {
            return all.Where(s => s.Role != RoleType.Administrator.ToString());
        }
        var users = await _context.Users
            .Include(s => s.Role)
            .AsNoTracking()
            .Where(s => s.Role!.RoleType != RoleType.Administrator)
            .ToListAsync();
        var response = users.Select(s => new UserResponse(s.Email, s.Role!.Name));
        return response;
    }

    public async Task<IResult> GetByEmail(UserGetByEmailRequest request)
    {
        var response = await _cache.Get<UserResponse>(USEREMAIL_ + request.Email);
        if (response is null)
        {
            var user = await _context.Users
                .Include(s => s.Role)
                .AsNoTracking()
                .FirstOrDefaultAsync(s => s.Email == request.Email);
            if (user is null)
            {
                return Results.BadRequest("User not found");
            }

            response = new UserResponse(user.Email, user.Role!.Name);
            await _cache.Set(USEREMAIL_ + request.Email, response);
        }
        return Results.Ok(response);
    }

    public async Task<IResult> GetById(UserGetByIdRequest request)
    {
        var response = await _cache.Get<UserResponse>(USERID_ + request.Id);
        if (response is null)
        {
            var user = await _context.Users
                .Include(s => s.Role)
                .AsNoTracking()
                .FirstOrDefaultAsync(s => s.Id == request.Id);
            if (user is null)
            {
                return Results.BadRequest("User not found");
            }

            response = new UserResponse(user.Email, user.Role!.Name);
            await _cache.Set(USERID_ + request.Id, response);
        }
        return Results.Ok(response);
    }

    public async Task<IResult> Login(UserAuthRequest request)
    {
        var user = await _context.Users.Include(u=>u.Role).SingleOrDefaultAsync(u => u.Email == request.Email);
        if (user is null)
        {
            return Results.BadRequest($"User with Email {request.Email} not found");
        }

        var password = _encrypt.HashPassword(request.Password, user.Salt);
        if (!password.SequenceEqual(user.Password))
        {
            return Results.BadRequest("Password is incorrect");
        }

        user.Salt = _encrypt.GenerateSalt();
        user.Password = _encrypt.HashPassword(request.Password, user.Salt);
        await _context.SaveChangesAsync();

        var token = _token.GenerateToken(user.Email, user.Role!.Name);
        var response = new UserAuthResponse(user.Id, user.Email, user.Role.Name, token);
        return Results.Ok(response);
    }

    public async Task<IResult> Registration(UserAuthRequest request)
    {
        var user = await _context.Users.SingleOrDefaultAsync(u => u.Email == request.Email);
        if (user is not null)
        {
            return Results.BadRequest($"User with Email {request.Email} already exist");
        }

        var salt = _encrypt.GenerateSalt();
        var password = _encrypt.HashPassword(request.Password, salt);
        var role = await _context.Roles.FirstAsync(r => r.RoleType == RoleType.User);
        user = new User()
        {
            Email = request.Email,
            Password = password,
            Salt = salt,
            Role = role
        };
        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();

        await _cache.Remove(USERS);

        var token = _token.GenerateToken(user.Email, user.Role.Name);
        var response = new UserAuthResponse(user.Id, user.Email, user.Role.Name, token);
        return Results.Ok(response);
    }

    public async Task<IResult> UpdateEmail(UserUpdateEmailRequest request)
    {
        var user = await _context.Users.SingleOrDefaultAsync(u => u.Id == request.Id);
        if (user is null)
        {
            return Results.BadRequest($"User with Id {request.Id} not found");
        }

        var password = _encrypt.HashPassword(request.Password, user.Salt);
        if (!password.SequenceEqual(user.Password))
        {
            return Results.BadRequest("Password is incorrect");
        }

        await _cache.Remove(USERID_ + request.Id);
        await _cache.Remove(USEREMAIL_ + user.Email);

        user.Email = request.Email;
        user.Salt = _encrypt.GenerateSalt();
        user.Password = _encrypt.HashPassword(request.Password, user.Salt);
        await _context.SaveChangesAsync();

        return Results.Ok();
    }

    public async Task<IResult> UpdatePassword(UserUpdatePasswordRequest request)
    {
        var user = await _context.Users.SingleOrDefaultAsync(u => u.Id == request.Id);
        if (user is null)
        {
            return Results.BadRequest($"User with Id {request.Id} not found");
        }

        var password = _encrypt.HashPassword(request.OldPassword, user.Salt);
        if (!password.SequenceEqual(user.Password))
        {
            return Results.BadRequest("Old password is incorrect");
        }

        user.Salt = _encrypt.GenerateSalt();
        user.Password = _encrypt.HashPassword(request.NewPassword, user.Salt);
        await _context.SaveChangesAsync();
        return Results.Ok();
    }

    public async Task<IResult> UpdateSetAdminRights(UserSetAdminRightsRequest request)
    {
        var user = await _context.Users.Include(u => u.Role).SingleOrDefaultAsync(u => u.Id == request.Id);
        if (user is null)
        {
            return Results.BadRequest($"User with Id {request.Id} not found");
        }

        var adminRole = await _context.Roles.SingleOrDefaultAsync(r => r.RoleType == RoleType.Administrator);

        if (user.Role!.RoleType == adminRole!.RoleType)
        {
            return Results.BadRequest($"User already has admin rights");
        }

        user.Role = adminRole;

        await _context.SaveChangesAsync();
        await _cache.Remove(USERS);
        await _cache.Remove(USERID_ + request.Id);
        await _cache.Remove(USEREMAIL_ + user.Email);
        return Results.Ok();
    }

    public async Task<IResult> UpdateRemoveAdminRights(UserRemoveAdminRightsRequest request)
    {
        var user = await _context.Users.Include(u=>u.Role).SingleOrDefaultAsync(u => u.Id == request.Id);
        if (user is null)
        {
            return Results.BadRequest($"User with Id {request.Id} not found");
        }

        var userRole = await _context.Roles.SingleOrDefaultAsync(r => r.RoleType == RoleType.User);
        
        if(user.Role!.RoleType == userRole!.RoleType)
        {
            return Results.BadRequest($"User is not admin");
        }
                
        user.Role = userRole;

        await _context.SaveChangesAsync();
        await _cache.Remove(USERS);
        await _cache.Remove(USERID_ + request.Id);
        await _cache.Remove(USEREMAIL_ + user.Email);
        return Results.Ok();
    }

    public async Task<IResult> Delete(UserDeleteRequest request)
    {
        var user = await _context.Users.SingleOrDefaultAsync(u => u.Id == request.Id);
        if (user is null)
        {
            return Results.BadRequest($"User with Id {request.Id} not found");
        }

        _context.Users.Remove(user);

        await _context.SaveChangesAsync();
        await _cache.Remove(USERS);
        await _cache.Remove(USERID_ + request.Id);
        await _cache.Remove(USEREMAIL_ + user.Email);
        return Results.Ok();
    }
}
