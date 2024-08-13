using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SlaveryMarket.Data;
using SlaveryMarket.Data.Entity;

namespace SlaveryMarket;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.AddDbContext<AppDbContext>(options =>
            options.UseNpgsql(
                "User ID=postgres; Password=sa;Host=localHost; Port=5432; Database=postgres; Pooling=True; Include Error Detail=True;", 
                b=> b.MigrationsAssembly("SlaveryMarket.Data")));
        
        builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
            .AddEntityFrameworkStores<AppDbContext>()
            .AddDefaultTokenProviders();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();


        app.MapControllers();

        app.Run();
    }
}