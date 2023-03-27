using Microsoft.EntityFrameworkCore;
using RetrievalService.Data;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;
var services = builder.Services;

services.AddControllers();
services.AddSwaggerGen();
services.AddDbContext<AppDbContext>(opt =>
    opt.UseMySql(config.GetConnectionString("MySql"), MySqlServerVersion.LatestSupportedServerVersion)
        .LogTo(Console.WriteLine, LogLevel.Information)
        .EnableSensitiveDataLogging()
        .EnableDetailedErrors()
);

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.MapControllers();

app.Run();
