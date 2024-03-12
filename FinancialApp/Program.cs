using FinancialApp.Data;
using Microsoft.EntityFrameworkCore;
using FinancialApp.Controllers;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllersWithViews()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
        // Include any other necessary JsonSerializer options here
    });

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<MyDatabaseContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("DatabaseContextString")));
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
