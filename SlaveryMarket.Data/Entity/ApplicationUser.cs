using Microsoft.AspNetCore.Identity;

namespace SlaveryMarket.Data.Entity;

public class ApplicationUser : IdentityUser
{
    public decimal? MoneyAmount { get; set; }
}