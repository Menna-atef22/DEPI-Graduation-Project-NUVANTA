using Nuvanta.Application.DTOs;
using Nuvanta.Application.Interfaces;
using Nuvanta.Domain.Entities;
using Nuvanta.Domain.Interfaces;

namespace Nuvanta.Application.Services;

public class CompanyService : ICompanyService
{
    private readonly IUnitOfWork _uow;
    public CompanyService(IUnitOfWork uow) => _uow = uow;

    public async Task<IReadOnlyList<CompanyDto>> GetAllAsync(CancellationToken ct = default)
    {
        var companies = await _uow.Repository<Company>().GetAllAsync(ct);
        return companies.Select(ToDto).ToList();
    }

    public async Task<CompanyDto?> GetByIdAsync(int id, CancellationToken ct = default)
    {
        var company = await _uow.Repository<Company>().FirstOrDefaultAsync(c => c.CompanyID == id, ct);
        return company is null ? null : ToDto(company);
    }

    public async Task<CompanyDto> CreateAsync(CreateCompanyDto dto, CancellationToken ct = default)
    {
        var company = new Company
        {
            CompanyName = dto.CompanyName,
            CurrencyCode = dto.CurrencyCode,
            TaxNumber = dto.TaxNumber,
            Email = dto.Email,
            Phone = dto.Phone,
            Street = dto.Street,
            City = dto.City,
            Country = dto.Country
        };

        await _uow.Repository<Company>().AddAsync(company, ct);
        await _uow.SaveChangesAsync(ct);
        return ToDto(company);
    }

    private static CompanyDto ToDto(Company c) => new(
        c.CompanyID, c.CompanyName, c.TaxNumber, c.Email, c.Phone,
        c.Street, c.City, c.Country, c.CurrencyCode, c.IsActive, c.CreatedAt);
}
