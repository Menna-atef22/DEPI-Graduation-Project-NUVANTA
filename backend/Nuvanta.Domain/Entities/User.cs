namespace Nuvanta.Domain.Entities;

public class User
{
    public int UserID { get; set; }
    public int CompanyID { get; set; }
    public int? BranchID { get; set; }
    public int? DepartmentID { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public bool IsActive { get; set; } = true;
    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public DateTime? LastLoginAt { get; set; }

    public Company Company { get; set; } = null!;
    public Branch? Branch { get; set; }
    public Department? Department { get; set; }

    public ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
    public ICollection<Budget> BudgetsCreatedByUser { get; set; } = new List<Budget>();
    public ICollection<Budget> BudgetsApprovedByUser { get; set; } = new List<Budget>();
    public ICollection<BudgetTransfer> BudgetTransfersRequestedByUser { get; set; } = new List<BudgetTransfer>();
    public ICollection<BudgetTransfer> BudgetTransfersApprovedByUser { get; set; } = new List<BudgetTransfer>();
    public ICollection<PurchaseRequest> PurchaseRequests { get; set; } = new List<PurchaseRequest>();
    public ICollection<PurchaseOrder> PurchaseOrders { get; set; } = new List<PurchaseOrder>();
    public ICollection<GoodsReceipt> GoodsReceipts { get; set; } = new List<GoodsReceipt>();
    public ICollection<StockMovement> StockMovements { get; set; } = new List<StockMovement>();
    public ICollection<SupplierInvoice> SupplierInvoices { get; set; } = new List<SupplierInvoice>();
    public ICollection<Payment> Payments { get; set; } = new List<Payment>();
    public ICollection<LedgerEntry> LedgerEntries { get; set; } = new List<LedgerEntry>();
    public ICollection<ApprovalRequest> ApprovalRequests { get; set; } = new List<ApprovalRequest>();
    public ICollection<ApprovalStep> ApprovalSteps { get; set; } = new List<ApprovalStep>();
    public ICollection<ApprovalAction> ApprovalActions { get; set; } = new List<ApprovalAction>();
    public ICollection<AuditLog> AuditLogs { get; set; } = new List<AuditLog>();
}
