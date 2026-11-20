namespace Nuvanta.Domain.Entities;

public class CostCenter
{
    public int CostCenterID { get; set; }
    public int CompanyID { get; set; }
    public int? DepartmentID { get; set; }
    public string CostCenterName { get; set; } = string.Empty;
    public string? Description { get; set; }

    public Company Company { get; set; } = null!;
    public Department? Department { get; set; }

    public ICollection<BudgetAllocation> BudgetAllocations { get; set; } = new List<BudgetAllocation>();
    public ICollection<PurchaseRequest> PurchaseRequests { get; set; } = new List<PurchaseRequest>();
}
