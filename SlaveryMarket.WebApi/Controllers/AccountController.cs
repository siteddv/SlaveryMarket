using Microsoft.AspNetCore.Mvc;
using SlaveryMarket.Data;
using SlaveryMarket.Data.Entity;
using SlaveryMarket.Data.Repository;
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
}