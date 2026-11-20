namespace Nuvanta.Domain.Entities;

public class Company
{
    public int CompanyID { get; set; }
    public string CompanyName { get; set; } = string.Empty;
    public string? TaxNumber { get; set; }
    public string? Email { get; set; }
    public string? Phone { get; set; }
    public string? Street { get; set; }
    public string? City { get; set; }
    public string? Country { get; set; }
    public string CurrencyCode { get; set; } = string.Empty;
    public bool IsActive { get; set; } = true;
    public DateTime? CreatedAt { get; set; }

    public ICollection<Branch> Branches { get; set; } = new List<Branch>();
    public ICollection<Department> Departments { get; set; } = new List<Department>();
    public ICollection<User> Users { get; set; } = new List<User>();
    public ICollection<Role> Roles { get; set; } = new List<Role>();
    public ICollection<FinancialPeriod> FinancialPeriods { get; set; } = new List<FinancialPeriod>();
    public ICollection<CostCenter> CostCenters { get; set; } = new List<CostCenter>();
    public ICollection<ExpenseCategory> ExpenseCategories { get; set; } = new List<ExpenseCategory>();
    public ICollection<Budget> Budgets { get; set; } = new List<Budget>();
    public ICollection<Supplier> Suppliers { get; set; } = new List<Supplier>();
    public ICollection<Warehouse> Warehouses { get; set; } = new List<Warehouse>();
    public ICollection<InventoryCategory> InventoryCategories { get; set; } = new List<InventoryCategory>();
    public ICollection<InventoryItem> InventoryItems { get; set; } = new List<InventoryItem>();
    public ICollection<PurchaseRequest> PurchaseRequests { get; set; } = new List<PurchaseRequest>();
    public ICollection<PurchaseOrder> PurchaseOrders { get; set; } = new List<PurchaseOrder>();
    public ICollection<SupplierInvoice> SupplierInvoices { get; set; } = new List<SupplierInvoice>();
    public ICollection<Payment> Payments { get; set; } = new List<Payment>();
    public ICollection<Account> Accounts { get; set; } = new List<Account>();
    public ICollection<LedgerEntry> LedgerEntries { get; set; } = new List<LedgerEntry>();
    public ICollection<CashAccount> CashAccounts { get; set; } = new List<CashAccount>();
    public ICollection<ApprovalRule> ApprovalRules { get; set; } = new List<ApprovalRule>();
    public ICollection<AuditLog> AuditLogs { get; set; } = new List<AuditLog>();
}
