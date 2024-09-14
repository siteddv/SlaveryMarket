using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SlaveryMarket.BL.Dtos;
using SlaveryMarket.BL.Services;
using SlaveryMarket.Data.Common.Enums;
using SlaveryMarket.Helpers;

namespace SlaveryMarket.Controllers;

public class AccountController(AuthService authService) : BaseController
{
    
    [ProducesResponseType<Result<object>>(StatusCodes.Status200OK)]
    [ProducesResponseType<Result<object>>(StatusCodes.Status400BadRequest)]
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterUserDto registerUserDto)
    {
        var result = await authService.RegisterAsync(registerUserDto);
        if (!result.Succeeded)
        {
            var errors = result.Errors.Select(x => x.Description);
            var requestResult = Result<object>.BadRequest(errors);
            return BadRequest(requestResult);
        }
        return Ok(Result<object>.Success());
    }
    
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginUserDto loginUserDto)
    {
        var jwt = await authService.Login(loginUserDto);
        return jwt == null 
            ? BadRequest(Result<string>.BadRequest("Invalid username or password")) 
            : Ok(Result<string>.Success(jwt));
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
    
    [ProducesResponseType<Result<List<RoleDto>>>(StatusCodes.Status200OK)]
    [HttpGet("get-roles")]
    public IActionResult GetRoles()
    {
        var roles = authService.GetRoles();
        var result = Result<List<RoleDto>>.Success(roles);
        return Ok(result);
    }
    
    [HttpGet("get-users-for-roles-assigning")]
    public async Task<IActionResult> GetUsersForRolesAssigning()
    {
        var users = await authService.GetUsersForRolesAssigningAsync(HttpContext);
        var result = Result<List<UserDto>>.Success(users);
        return Ok(result);
    }
}