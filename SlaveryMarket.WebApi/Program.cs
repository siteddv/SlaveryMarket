using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using SlaveryMarket.BL.Services;
using SlaveryMarket.Data;
using SlaveryMarket.Data.Entity;
using SlaveryMarket.Data.Repository;

namespace SlaveryMarket;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(c =>
        {
            var securityScheme = new OpenApiSecurityScheme
            {
                Name = "JWT Authentication",
                Description = "Enter your JWT token in this field",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.Http,
                Scheme = "bearer",
                BearerFormat = "JWT"
            };

            c.AddSecurityDefinition("Bearer", securityScheme);

            var securityRequirement = new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    new string[] {}
                }
            };

            c.AddSecurityRequirement(securityRequirement);

        });
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

        var key = Encoding.ASCII.GetBytes("gXcsTOS3uPu/Bf1IVe4CC6yLRnlVJYW9HyT35WZ8a4w=");
        builder.Services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key)
            };
        });

        var app = builder.Build();
        
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseMiddleware<ExceptionMiddleware>();
        app.UseHttpsRedirection();
        app.UseAuthentication(); // Add this line
        app.UseAuthorization();
        app.MapControllers();


        app.Run();
    }
}