using Microsoft.EntityFrameworkCore;
using MGContecnica.Application.Services;
using MGContecnica.Domain.Interfaces.Repositories;
using MGContecnica.Domain.Interfaces.Services;
using MGContecnica.Infrastructure.Data.Context;
using MGContecnica.Infrastructure.Repositories;

namespace MGContecnica.API.Configuration;

public static class DependencyInjectionConfig
{
    public static IServiceCollection AddDependencies(this IServiceCollection services, IConfiguration configuration)
    {
        // DbContext
        services.AddDbContext<MGContecnicaDbContext>(options =>
            options.UseSqlite(configuration.GetConnectionString("DefaultConnection")));

        // Repositories
        services.AddScoped<ICategoriaRepository, CategoriaRepository>();
        services.AddScoped<ITransacaoRepository, TransacaoRepository>();

        // Services
        services.AddScoped<ICategoriaService, CategoriaService>();
        services.AddScoped<ITransacaoService, TransacaoService>();
        services.AddScoped<IRelatorioService, RelatorioService>();

        return services;
    }
}