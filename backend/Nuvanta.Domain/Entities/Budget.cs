namespace Nuvanta.Domain.Entities;

public class Budget
{
    public int BudgetID { get; set; }
    public int CompanyID { get; set; }
    public int FinancialPeriodID { get; set; }
    public string BudgetName { get; set; } = string.Empty;
    public decimal TotalAmount { get; set; }
    public string CurrencyCode { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public int CreatedBy { get; set; }
    public int? ApprovedBy { get; set; }
    public DateTime? ApprovedAt { get; set; }
    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }

    public Company Company { get; set; } = null!;
    public FinancialPeriod FinancialPeriod { get; set; } = null!;
    public User CreatedByUser { get; set; } = null!;
    public User? ApprovedByUser { get; set; }

    public ICollection<BudgetAllocation> BudgetAllocations { get; set; } = new List<BudgetAllocation>();
    public ICollection<PurchaseRequest> PurchaseRequests { get; set; } = new List<PurchaseRequest>();
}
