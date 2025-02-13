using Angular_Core_Auth.Server.Database;
using Angular_Core_Auth.Server.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

/// <summary>
/// Adds authorization services to the application.
/// </summary>
builder.Services.AddAuthorization();

/// <summary>
/// Configures authentication services with cookie and bearer token scheme.
/// </summary>
builder.Services.AddAuthentication()
    .AddBearerToken(IdentityConstants.BearerScheme);

/// <summary>
/// Adds Identity services with Entity Framework stores and API endpoints.
/// </summary>
builder.Services.AddIdentityCore<IdentityUser>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddApiEndpoints();

/// <summary>
/// Configures the application's database context to use SQL Server.
/// </summary>
/// <param name="options">The options to configure the database context.</param>
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("AngularCoreAuth"));
});

var app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.ApplyMigrations();
}

app.UseHttpsRedirection();

app.MapIdentityApi<IdentityUser>();

app.UseAuthorization();

app.MapControllers();

app.MapFallbackToFile("/index.html");

app.Run();
