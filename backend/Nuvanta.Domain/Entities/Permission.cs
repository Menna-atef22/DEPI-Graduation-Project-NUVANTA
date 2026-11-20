namespace Nuvanta.Domain.Entities;

public class Permission
{
    public int PermissionID { get; set; }
    public string PermissionName { get; set; } = string.Empty;
    public string? Description { get; set; }

    public ICollection<RolePermission> RolePermissions { get; set; } = new List<RolePermission>();
}
