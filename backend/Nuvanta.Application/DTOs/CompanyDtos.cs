namespace Nuvanta.Application.DTOs;

public record CompanyDto(
    int CompanyID,
    string CompanyName,
    string? TaxNumber,
    string? Email,
    string? Phone,
    string? Street,
    string? City,
    string? Country,
    string CurrencyCode,
    bool IsActive,
    DateTime? CreatedAt);

public record CreateCompanyDto(
    string CompanyName,
    string CurrencyCode,
    string? TaxNumber,
    string? Email,
    string? Phone,
    string? Street,
    string? City,
    string? Country);
