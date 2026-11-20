namespace Nuvanta.Domain.Entities;

public class Department
{
    public int DepartmentID { get; set; }
    public int CompanyID { get; set; }
    public int? BranchID { get; set; }
    public int? ParentDepartmentID { get; set; }
    public string DepartmentName { get; set; } = string.Empty;
    public string? Description { get; set; }

    public Company Company { get; set; } = null!;
    public Branch? Branch { get; set; }
    public Department? ParentDepartment { get; set; }

    public ICollection<Department> ChildDepartments { get; set; } = new List<Department>();
    public ICollection<User> Users { get; set; } = new List<User>();
    public ICollection<CostCenter> CostCenters { get; set; } = new List<CostCenter>();
    public ICollection<BudgetAllocation> BudgetAllocations { get; set; } = new List<BudgetAllocation>();
    public ICollection<PurchaseRequest> PurchaseRequests { get; set; } = new List<PurchaseRequest>();
}
