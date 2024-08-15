using Microsoft.AspNetCore.Mvc;
using SlaveryMarket.Data;
using SlaveryMarket.Data.Entity;
using SlaveryMarket.Data.Model;
using SlaveryMarket.Data.Repository;

namespace SlaveryMarket.Controllers;

public class AccountController : BaseController
{
    private readonly Repository<Janar> _repository;
    private readonly Repository<Order> _orepository;

    public AccountController(Repository<Janar> repository, Repository<Order> orepository)
    {
        _repository = repository;
        _orepository = orepository;
    }

    [HttpPost("add-janar")]
    public IActionResult AddJanar([FromBody] string gender)
    {
        Janar janar = new Janar
        {
            Gender = gender
        };
        _repository.Add(janar);
        
        return Ok();
    }
    
    [HttpGet("get-janars")]
    public IActionResult GetJanars()
    {
        var janars = _repository.GetAll();
        if(janars.Count == 0)
        {
            return NotFound();
        }
        return Ok(janars);
    }
}