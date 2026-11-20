namespace Nuvanta.Application.DTOs;

public record DepartmentDto(
    int DepartmentID,
    int CompanyID,
    int? BranchID,
    int? ParentDepartmentID,
    string DepartmentName,
    string? Description);
