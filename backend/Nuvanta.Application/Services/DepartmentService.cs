using Nuvanta.Application.DTOs;
using Nuvanta.Application.Interfaces;
using Nuvanta.Domain.Entities;
using Nuvanta.Domain.Interfaces;

namespace Nuvanta.Application.Services;

public class DepartmentService : IDepartmentService
{
    private readonly IUnitOfWork _uow;
    public DepartmentService(IUnitOfWork uow) => _uow = uow;

    // Multi-tenant rule: every query is filtered by CompanyID.
    public async Task<IReadOnlyList<DepartmentDto>> GetByCompanyAsync(int companyId, CancellationToken ct = default)
    {
        var departments = await _uow.Repository<Department>().FindAsync(d => d.CompanyID == companyId, ct);
        return departments
            .Select(d => new DepartmentDto(d.DepartmentID, d.CompanyID, d.BranchID, d.ParentDepartmentID, d.DepartmentName, d.Description))
            .ToList();
    }
}
