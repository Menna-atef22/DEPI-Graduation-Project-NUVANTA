namespace Nuvanta.Domain.Entities;

public class UserRole
{
    public int UserID { get; set; }
    public int RoleID { get; set; }
    public DateTime? AssignedAt { get; set; }

    public User User { get; set; } = null!;
    public Role Role { get; set; } = null!;
}
