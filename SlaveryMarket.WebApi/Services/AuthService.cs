using Microsoft.AspNetCore.Identity;
using SlaveryMarket.Data.Entity;
using SlaveryMarket.Dtos;

namespace SlaveryMarket.Services;

public class AuthService(UserManager<ApplicationUser> userManager,
    SignInManager<ApplicationUser> signInManager)
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
}