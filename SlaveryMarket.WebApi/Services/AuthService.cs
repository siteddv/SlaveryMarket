using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SlaveryMarket.Data;
using SlaveryMarket.Data.Entity;
using SlaveryMarket.Dtos;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;

namespace SlaveryMarket.Services;

public class AuthService(UserManager<ApplicationUser> userManager,
    SignInManager<ApplicationUser> signInManager,
    AppDbContext dbContext)
{
    public async Task<IdentityResult> RegisterAsync(RegisterUserDto registerUserDto)
    {
        var user = new ApplicationUser
        {
            UserName = registerUserDto.UserName,
            Email = registerUserDto.Email
        };
        
        IdentityResult result = await userManager.CreateAsync(user, registerUserDto.Password);
        return result;
    }

    public async Task<SignInResult> Login(LoginUserDto loginUserDto)
    {
        var user = await userManager.FindByNameAsync(loginUserDto.UserName);
        if (user == null)
            return SignInResult.Failed;

        var result = await signInManager
            .PasswordSignInAsync(loginUserDto.UserName, loginUserDto.Password, false, false);
        return result;
    }

    public List<RoleDto> GetRoles()
    {
        return dbContext.Roles
            .Select(r => new RoleDto(r.Id, r.Name))
            .ToList();
    }
    
    public async Task<List<UserDto>> GetUsersForRolesAssigningAsync(HttpContext httpContext)
    {
        var currentUser = await userManager.GetUserAsync(httpContext.User);
        return await userManager
            .Users
            .Where(u => u.Id != currentUser.Id)
            .Select(u => new UserDto(u.Id, u.UserName))
            .ToListAsync();
    }

    public async Task<IdentityResult> AssignRoleAsync(string roleId, Guid userId)
    {
        var role = await dbContext.Roles
            .FirstOrDefaultAsync(r => r.Id == roleId);
        
        var user = await userManager.FindByIdAsync(userId.ToString());
        return await userManager.AddToRoleAsync(user, role.Name);
    }
}