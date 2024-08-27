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
}