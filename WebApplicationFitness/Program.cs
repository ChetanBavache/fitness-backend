using Fitness.Application.Interfaces;
using Fitness.Application.Services;
using Fitness.Domain.Interfaces;
using Fitness.Infrastructure.Data;
using Fitness.Infrastructure.Repositories;
using Fitness.API.Middleware;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Controllers
builder.Services.AddControllers();

// DbContext (SQLite)
builder.Services.AddDbContext<FitnessDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

// DI
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// JWT (SAFE)
var jwtSettings = builder.Configuration.GetSection("Jwt");
bool jwtEnabled = !string.IsNullOrWhiteSpace(jwtSettings["Key"]);

if (jwtEnabled)
{
    builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = jwtSettings["Issuer"],
                ValidAudience = jwtSettings["Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(jwtSettings["Key"]!)
                )
            };
        });

    builder.Services.AddAuthorization();
}

var app = builder.Build();

// Swagger FIRST
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Fitness API v1");
});

// Middleware pipeline
app.UseHttpsRedirection();
app.UseMiddleware<ExceptionMiddleware>();

if (jwtEnabled)
{
    app.UseAuthentication();
    app.UseAuthorization();
}

app.MapControllers();
app.Run();
