using Nuvanta.Application.Interfaces;
using Nuvanta.Application.Services;
using Nuvanta.Infrastructure;

namespace Nuvanta.API.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddNuvantaServices(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");
        if (string.IsNullOrWhiteSpace(connectionString))
            throw new InvalidOperationException(
                "ConnectionStrings:DefaultConnection is empty. Set it with: " +
                "dotnet user-secrets set \"ConnectionStrings:DefaultConnection\" \"<your connection string>\" --project backend/Nuvanta.API");

        // Infrastructure: DbContext + repositories + unit of work
        services.AddInfrastructure(connectionString);

        // Application services (one per module; add new ones here)
        services.AddScoped<ICompanyService, CompanyService>();
        services.AddScoped<IDepartmentService, DepartmentService>();

        return services;
    }

    public static IServiceCollection AddNuvantaCors(this IServiceCollection services, IConfiguration configuration)
    {
        var origins = configuration.GetSection("Cors:AllowedOrigins").Get<string[]>()
                      ?? new[] { "http://localhost:5173", "http://localhost:3000" };

        services.AddCors(o => o.AddPolicy("Frontend", p =>
            p.WithOrigins(origins).AllowAnyHeader().AllowAnyMethod()));
        return services;
    }
}
