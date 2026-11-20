namespace Nuvanta.Domain.Entities;

public class Role
{
    public int RoleID { get; set; }
    public int? CompanyID { get; set; }
    public string RoleName { get; set; } = string.Empty;
    public string? Description { get; set; }

    public Company? Company { get; set; }

    public ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
    public ICollection<RolePermission> RolePermissions { get; set; } = new List<RolePermission>();
    public ICollection<ApprovalStep> ApprovalSteps { get; set; } = new List<ApprovalStep>();
}
