using Microsoft.EntityFrameworkCore;
using ParseService.Controllers;
using ParseService.Data;
using ParseService.Extensions;
using ParseService.Helper;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
var config = builder.Configuration;

services.AddControllers();
services.AddSwaggerGen();
services.AddTransient<ICanalHelper, CanalHelper>();
services.AddTransient<TestController>();
services.AddDbContext<AppDbContext>(opt => opt.UseMySql(config.GetConnectionString("MySql"), MySqlServerVersion.LatestSupportedServerVersion));

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();
app.MapControllers();
app.UseRabbitMQ();
app.Migre();
app.Run();
