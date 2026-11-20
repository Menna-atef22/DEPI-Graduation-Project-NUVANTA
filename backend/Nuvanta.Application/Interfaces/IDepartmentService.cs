using Nuvanta.Application.DTOs;

namespace Nuvanta.Application.Interfaces;

public interface IDepartmentService
{
    Task<IReadOnlyList<DepartmentDto>> GetByCompanyAsync(int companyId, CancellationToken ct = default);
}
