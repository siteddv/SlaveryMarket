using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SlaveryMarket.BL.Dtos;
using SlaveryMarket.Data;
using SlaveryMarket.Data.Entity;

namespace SlaveryMarket.BL.Services;

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

    public async Task<string?> Login(LoginUserDto loginUserDto)
    {
        var user = await userManager.FindByNameAsync(loginUserDto.UserName);
        if (user == null)
            return null;

        var roles = await userManager.GetRolesAsync(user);
        return GenerateToken(user.UserName, roles);
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

    private string GenerateToken(string username, IList<string> roles)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes("gXcsTOS3uPu/Bf1IVe4CC6yLRnlVJYW9HyT35WZ8a4w=");

        var claims = new ClaimsIdentity();
        claims.AddClaim( new Claim(ClaimTypes.Name, username));

        foreach (var role in roles)
        {
            claims.AddClaim( new Claim(ClaimTypes.Role, role));
        }
        
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = claims,
            Expires = DateTime.Now.AddMinutes(2),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}