using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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
        
        var result = await userManager.CreateAsync(user, registerUserDto.Password);
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
    
    public async Task<bool> AssignRoleAsync(string role, Guid userId)
    {
        var userIdString = userId.ToString();
        
        var user = dbContext
            .Users
            .FirstOrDefault(u => u.Id == userIdString);
        
        await userManager.AddToRoleAsync(user, role);
        return true;
    }
}