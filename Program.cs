
using MongoDB.Driver.Core.Configuration;
using MongoDB.Driver;
using RouletteBetsApi.Repositories;
using RouletteBetsApi.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using RouletteBetsApi.Configurations;
using System.Reflection;
using Microsoft.IdentityModel.Logging;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<RouletteBetsDBSettings>(builder.Configuration.GetSection("RouletteBetsDBSettings"));
builder.Services.AddSingleton<BetRepository>();
builder.Services.AddSingleton<RouletteRepository>();
builder.Services.AddSingleton<UserRepository>();
builder.Services.AddSingleton<BetService>();
builder.Services.AddSingleton<RouletteService>();
builder.Services.AddSingleton<UserService>();

builder.Services.AddAWSLambdaHosting(LambdaEventSource.HttpApi);
//Redis Cache
builder.Services.AddStackExchangeRedisCache(options => {
    options.Configuration = builder.Configuration.GetConnectionString("Redis");
    options.InstanceName = "redis";
});
builder.Services.AddRazorPages();
builder.Services.AddSwaggerGen();
// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// AutoMapper for dtos
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddControllersWithViews();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(o =>
{
    o.TokenValidationParameters = new TokenValidationParameters
    {
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = false,
        ValidateIssuerSigningKey = true
    };
}); builder.Services.AddAuthorization();// Add configuration from appsettings.json
builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddEnvironmentVariables();
var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    if (app.Environment.IsDevelopment() || app.Environment.IsProduction())

    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Roulette Bets Api v1");
        c.RoutePrefix = string.Empty;
        
    }
});
IdentityModelEventSource.ShowPII = true;
app.AddGlobalErrorHandler();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
IConfiguration configuration = app.Configuration; 
IWebHostEnvironment environment = app.Environment;
app.UseStaticFiles();
app.MapControllers();
app.MapRazorPages();
app.Run();

