using Nuvanta.Application.DTOs;

namespace Nuvanta.Application.Interfaces;

public interface IDatabaseDiagnosticsService
{
    Task<DatabaseHealthDto> CheckAsync(CancellationToken ct = default);
}
