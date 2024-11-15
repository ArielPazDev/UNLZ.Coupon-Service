using Clientes.API.Context;
using Microsoft.EntityFrameworkCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

// Swagger
builder.Services.AddSwaggerGen();

// Database
builder.Services.AddDbContext<DatabaseContext>
    (option => option.UseSqlServer(builder.Configuration.GetConnectionString("DatabaseConnection")));

// Log
Log.Logger = new LoggerConfiguration()
    .Enrich.FromLogContext()
    .WriteTo.Logger(l => l.Filter.ByIncludingOnly(evt => evt.Level == Serilog.Events.LogEventLevel.Error)
    .WriteTo.File("Logs/Error-.log", rollingInterval: RollingInterval.Day))
    .WriteTo.Logger(l => l.Filter.ByIncludingOnly(evt => evt.Level == Serilog.Events.LogEventLevel.Information)
    .WriteTo.File("Logs/Information-.log", rollingInterval: RollingInterval.Day))
    .CreateLogger();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
