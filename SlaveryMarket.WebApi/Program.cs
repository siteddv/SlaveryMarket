using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SlaveryMarket.Data;
using SlaveryMarket.Data.Entity;
using SlaveryMarket.Data.Repository;
using SlaveryMarket.Services;

namespace SlaveryMarket;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddScoped<AuthService>();

        builder.Services.AddDbContext<AppDbContext>(options =>
            options.UseNpgsql(
                "User ID=postgres; Password=sa;Host=localHost; Port=5432; Database=postgres; Pooling=True; Include Error Detail=True;", 
                b=> b.MigrationsAssembly("SlaveryMarket.Data")));
        
        builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
            .AddEntityFrameworkStores<AppDbContext>()
            .AddDefaultTokenProviders();
        
        builder.Services.AddScoped(typeof(Repository<>));
        builder.Services.AddScoped<ProductRepository>();
        builder.Services.AddScoped<ExceptionMiddleware>();
        

        var app = builder.Build();
        
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseMiddleware<ExceptionMiddleware>();
        app.UseHttpsRedirection();
        app.UseAuthorization();
        app.MapControllers();


        app.Run();
    }
}