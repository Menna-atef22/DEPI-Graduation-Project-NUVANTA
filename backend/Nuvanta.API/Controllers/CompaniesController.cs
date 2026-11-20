using Microsoft.AspNetCore.Mvc;
using Nuvanta.Application.DTOs;
using Nuvanta.Application.Interfaces;

namespace Nuvanta.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CompaniesController : ControllerBase
{
    private readonly ICompanyService _companies;
    private readonly IDepartmentService _departments;

    public CompaniesController(ICompanyService companies, IDepartmentService departments)
    {
        _companies = companies;
        _departments = departments;
    }

    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<CompanyDto>>> GetAll(CancellationToken ct)
        => Ok(await _companies.GetAllAsync(ct));

    [HttpGet("{id:int}")]
    public async Task<ActionResult<CompanyDto>> GetById(int id, CancellationToken ct)
    {
        var company = await _companies.GetByIdAsync(id, ct);
        return company is null ? NotFound() : Ok(company);
    }

    [HttpGet("{id:int}/departments")]
    public async Task<ActionResult<IReadOnlyList<DepartmentDto>>> GetDepartments(int id, CancellationToken ct)
        => Ok(await _departments.GetByCompanyAsync(id, ct));

    [HttpPost]
    public async Task<ActionResult<CompanyDto>> Create([FromBody] CreateCompanyDto dto, CancellationToken ct)
    {
        if (string.IsNullOrWhiteSpace(dto.CompanyName) || string.IsNullOrWhiteSpace(dto.CurrencyCode))
            return BadRequest("CompanyName and CurrencyCode are required.");

        var created = await _companies.CreateAsync(dto, ct);
        return CreatedAtAction(nameof(GetById), new { id = created.CompanyID }, created);
    }
}
