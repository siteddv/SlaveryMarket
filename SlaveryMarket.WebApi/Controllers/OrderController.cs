using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SlaveryMarket.Data;

namespace SlaveryMarket.Controllers;

public class OrderController : BaseController
{
    private readonly AppDbContext _context;

    public OrderController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet("get-orders")]
    public IActionResult GetOrders()
    {
        var orders =  _context.Orders
            .Include(o=> o.Client)
            .ToList();
        return Ok(orders);
    }
}