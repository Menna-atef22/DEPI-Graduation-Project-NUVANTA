namespace Nuvanta.Domain.Entities;

public class Branch
{
    public int BranchID { get; set; }
    public int CompanyID { get; set; }
    public string BranchName { get; set; } = string.Empty;
    public string? Address { get; set; }
    public string? Phone { get; set; }
    public bool IsActive { get; set; } = true;

    public Company Company { get; set; } = null!;

    public ICollection<Department> Departments { get; set; } = new List<Department>();
    public ICollection<User> Users { get; set; } = new List<User>();
    public ICollection<Warehouse> Warehouses { get; set; } = new List<Warehouse>();
}
