using Nuvanta.Application.DTOs;

namespace Nuvanta.Application.Interfaces;

public interface ICompanyService
{
    Task<IReadOnlyList<CompanyDto>> GetAllAsync(CancellationToken ct = default);
    Task<CompanyDto?> GetByIdAsync(int id, CancellationToken ct = default);
    Task<CompanyDto> CreateAsync(CreateCompanyDto dto, CancellationToken ct = default);
}
