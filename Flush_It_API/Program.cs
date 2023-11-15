using Flush_It_API.Data;
using Flush_It_API.Models;
using Flush_It_API.Services;
using Flush_It_API.Utilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("PostgreSQLConnection")));

// JwtService
builder.Services.AddSingleton<IJwtService>(_ => new JwtService(builder.Configuration));


builder.Services.AddControllers();

// Add BCrypt for password hashing
builder.Services.AddSingleton<IBCryptHasher, BCryptHasher>();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapGet("/", () =>
{
    return Results.Ok();
});

app.Run();
