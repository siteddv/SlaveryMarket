using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SlaveryMarket.Dtos;
using SlaveryMarket.Services;

namespace SlaveryMarket.Controllers;

public class AccountController(AuthService authService) : BaseController
{
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterUserDto registerUserDto)
    {
        var result = await authService.RegisterAsync(registerUserDto);
        if (!result.Succeeded)
        {
            return BadRequest(result.Errors);
        }
        return Ok();
    }
    
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginUserDto loginUserDto)
    {
        var result = await authService.Login(loginUserDto);
        if (!result.Succeeded)
        {
            return BadRequest("Invalid username or password");
        }
        return Ok();
    }
    
    [Authorize(Roles = "Admin")]
    [HttpPost("keep-calm")]
    public IActionResult KeepCalm()
    {
        return Ok("Keep calm and be an admin");
    }

    [HttpPut("assign-role")]
    public async Task<IActionResult> AssignRole(string roleId, Guid userId)
    {
        var result = await authService.AssignRoleAsync(roleId, userId);

        if (!result.Succeeded)
        {
            return BadRequest(result.Errors);
        }
        
        return Ok();
    }
    
    [HttpGet("get-roles")]
    public IActionResult GetRoles()
    {
        var roles = authService.GetRoles();
        return Ok(roles);
    }
    
    [Authorize]
    [HttpGet("get-users-for-roles-assigning")]
    public async Task<IActionResult> GetUsersForRolesAssigning()
    {
        var users = await authService.GetUsersForRolesAssigningAsync(HttpContext);
        return Ok(users);
    }
}