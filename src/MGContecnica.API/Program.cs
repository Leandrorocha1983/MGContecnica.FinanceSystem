using MGContecnica.API.Configuration;
using MGContecnica.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;
using Serilog;

// Configurar Serilog
Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.File("logs/mgcontecnica-.txt", 
        rollingInterval: RollingInterval.Day,
        retainedFileCountLimit: 30)
    .MinimumLevel.Information()
    .CreateLogger();

try
{
    Log.Information("Iniciando aplicação MG Contecnica");

    var builder = WebApplication.CreateBuilder(args);

    // Usar Serilog
    builder.Host.UseSerilog();

    // Add services to the container.
    builder.Services.AddControllers();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    // Add custom dependencies
    builder.Services.AddDependencies(builder.Configuration);

    // Add CORS
    builder.Services.AddCors(options =>
    {
        options.AddPolicy("AllowAll", policy =>
        {
            policy.AllowAnyOrigin()
                  .AllowAnyMethod()
                  .AllowAnyHeader();
        });
    });

    var app = builder.Build();

    // Criar banco automaticamente
    using (var scope = app.Services.CreateScope())
    {
        var context = scope.ServiceProvider.GetRequiredService<MGContecnicaDbContext>();
        context.Database.EnsureCreated();
        Log.Information("Banco de dados inicializado");
    }

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();
    app.UseCors("AllowAll");
    app.UseAuthorization();
    app.MapControllers();

    Log.Information("API MG Contecnica iniciada com sucesso");
    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Aplicação falhou ao iniciar");
}
finally
{
    Log.CloseAndFlush();
}