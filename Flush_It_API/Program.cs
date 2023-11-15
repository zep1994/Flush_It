using Flush_It_API.Data;
using Flush_It_API.Models;
using Microsoft.EntityFrameworkCore;
using System;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("PostgreSQLConnection")));


builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapGet("/", () =>
{
    return Results.Ok();
});

//GET
app.MapGet("api/ibs_count", async (AppDbContext context) =>
{

    return Results.Ok(await context.IbsCount.Where(b => b.Count >= 1).ToListAsync());
});

//POST
app.MapPost("api/ibs_count", async (AppDbContext context, IbsCount ibsCount) =>
{
    await context.IbsCount.AddAsync(ibsCount);
    await context.SaveChangesAsync();

    return Results.Created($"api/ibs_count/{ibsCount.Id}", ibsCount);
});

//UPDATE
app.MapPut("api/ibs_count/{id}", async (AppDbContext context, int id, IbsCount ibsCount) =>
{
    var ibsModel = await context.IbsCount.FirstOrDefaultAsync(x => x.Id == id);

    if (ibsModel == null)
    {
        return Results.NotFound();
    }

    ibsModel.Count = ibsCount.Count;
    await context.SaveChangesAsync();

    return Results.NoContent();

});

app.MapDelete("api/ibs_count/{id}", async (AppDbContext context, int id) =>
{
    var ibsModel = await context.IbsCount.FirstOrDefaultAsync(x => x.Id == id);

    if (ibsModel == null)
    {
        return Results.NotFound();
    }

    context.IbsCount.Remove(ibsModel);

    await context.SaveChangesAsync();

    return Results.NoContent();
});


app.Run();
