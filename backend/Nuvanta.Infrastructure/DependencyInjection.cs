using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Nuvanta.Application.Interfaces;
using Nuvanta.Domain.Interfaces;
using Nuvanta.Infrastructure.Persistence;
using Nuvanta.Infrastructure.Repositories;
using Nuvanta.Infrastructure.Services;

namespace Nuvanta.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<NuvantaDbContext>(options =>
            options.UseSqlServer(connectionString, sql => sql.EnableRetryOnFailure()));

        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        services.AddScoped<IDatabaseDiagnosticsService, DatabaseDiagnosticsService>();

        return services;
    }
}
