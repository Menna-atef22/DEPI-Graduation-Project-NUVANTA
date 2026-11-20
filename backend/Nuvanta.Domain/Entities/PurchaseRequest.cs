namespace Nuvanta.Domain.Entities;

public class PurchaseRequest
{
    public int PurchaseRequestID { get; set; }
    public int CompanyID { get; set; }
    public int RequestedBy { get; set; }
    public int DepartmentID { get; set; }
    public int BudgetID { get; set; }
    public int? CostCenterID { get; set; }
    public DateTime RequestDate { get; set; }
    public DateTime? SubmittedAt { get; set; }
    public DateTime? ApprovedAt { get; set; }
    public string Status { get; set; } = string.Empty;
    public string Justification { get; set; } = string.Empty;
    public DateTime? UpdatedAt { get; set; }
    // Computed column (DB-generated, read-only)
    public decimal ExpectedTotalAmount { get; set; }

    public Company Company { get; set; } = null!;
    public User RequestedByUser { get; set; } = null!;
    public Department Department { get; set; } = null!;
    public Budget Budget { get; set; } = null!;
    public CostCenter? CostCenter { get; set; }

    public ICollection<BudgetReservation> BudgetReservations { get; set; } = new List<BudgetReservation>();
    public ICollection<PurchaseRequestItem> PurchaseRequestItems { get; set; } = new List<PurchaseRequestItem>();
    public ICollection<PurchaseOrder> PurchaseOrders { get; set; } = new List<PurchaseOrder>();
}
