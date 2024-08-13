using Microsoft.AspNetCore.Mvc;
using SlaveryMarket.Data;
using SlaveryMarket.Data.Entity;

namespace SlaveryMarket.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AccountController : ControllerBase
{
    private readonly AppDbContext _context;

    public AccountController(AppDbContext context)
    {
        _context = context;
    }

    [HttpPost("add-janar")]
    public IActionResult AddJanar([FromBody] string gender)
    {
        Janar janar = new Janar
        {
            Gender = gender
        };
        _context.Set<Janar>().Add(janar);
        _context.SaveChanges();
        
        return Ok();
    }
    
    [HttpGet("get-janars")]
    public IActionResult GetJanars()
    {
        var janars = _context.Set<Janar>().ToList();
        if(janars.Count == 0)
        {
            return NotFound();
        }
        return Ok(janars);
    }
    
    
}