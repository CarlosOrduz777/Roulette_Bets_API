
using MongoDB.Driver.Core.Configuration;
using MongoDB.Driver;
using RouletteBetsApi.Models;
using RouletteBetsApi.Repositories;
using System.Collections;
using System.Xml.Linq;

var builder = WebApplication.CreateBuilder(args);



builder.Services.Configure<RouletteBetsDBSettings>(builder.Configuration.GetSection("BetServiceSettings"));
builder.Services.AddSingleton<BetService>();
builder.Services.Configure<RouletteBetsDBSettings>(builder.Configuration.GetSection("RouletteServiceSettings"));
builder.Services.AddSingleton<RouletteService>();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
