using Microsoft.EntityFrameworkCore;
using Nuvanta.Domain.Entities;

namespace Nuvanta.Infrastructure.Persistence;

/// <summary>EF Core context mapped to the existing SQL Server database "nuvanta" (Database-First).</summary>
public class NuvantaDbContext : DbContext
{
    public NuvantaDbContext(DbContextOptions<NuvantaDbContext> options) : base(options) { }

    public DbSet<Company> Companies => Set<Company>();
    public DbSet<Branch> Branches => Set<Branch>();
    public DbSet<Department> Departments => Set<Department>();
    public DbSet<User> Users => Set<User>();
    public DbSet<Role> Roles => Set<Role>();
    public DbSet<UserRole> UserRoles => Set<UserRole>();
    public DbSet<Permission> Permissions => Set<Permission>();
    public DbSet<RolePermission> RolePermissions => Set<RolePermission>();
    public DbSet<FinancialPeriod> FinancialPeriods => Set<FinancialPeriod>();
    public DbSet<CostCenter> CostCenters => Set<CostCenter>();
    public DbSet<ExpenseCategory> ExpenseCategories => Set<ExpenseCategory>();
    public DbSet<Budget> Budgets => Set<Budget>();
    public DbSet<BudgetAllocation> BudgetAllocations => Set<BudgetAllocation>();
    public DbSet<BudgetTransfer> BudgetTransfers => Set<BudgetTransfer>();
    public DbSet<Supplier> Suppliers => Set<Supplier>();
    public DbSet<Warehouse> Warehouses => Set<Warehouse>();
    public DbSet<InventoryCategory> InventoryCategories => Set<InventoryCategory>();
    public DbSet<InventoryItem> InventoryItems => Set<InventoryItem>();
    public DbSet<PurchaseRequest> PurchaseRequests => Set<PurchaseRequest>();
    public DbSet<BudgetReservation> BudgetReservations => Set<BudgetReservation>();
    public DbSet<PurchaseRequestItem> PurchaseRequestItems => Set<PurchaseRequestItem>();
    public DbSet<PurchaseOrder> PurchaseOrders => Set<PurchaseOrder>();
    public DbSet<PurchaseOrderItem> PurchaseOrderItems => Set<PurchaseOrderItem>();
    public DbSet<GoodsReceipt> GoodsReceipts => Set<GoodsReceipt>();
    public DbSet<GoodsReceiptItem> GoodsReceiptItems => Set<GoodsReceiptItem>();
    public DbSet<StockMovement> StockMovements => Set<StockMovement>();
    public DbSet<InventoryBalance> InventoryBalances => Set<InventoryBalance>();
    public DbSet<SupplierInvoice> SupplierInvoices => Set<SupplierInvoice>();
    public DbSet<SupplierInvoiceItem> SupplierInvoiceItems => Set<SupplierInvoiceItem>();
    public DbSet<Payment> Payments => Set<Payment>();
    public DbSet<PaymentAllocation> PaymentAllocations => Set<PaymentAllocation>();
    public DbSet<Account> Accounts => Set<Account>();
    public DbSet<LedgerEntry> LedgerEntries => Set<LedgerEntry>();
    public DbSet<LedgerEntryLine> LedgerEntryLines => Set<LedgerEntryLine>();
    public DbSet<CashAccount> CashAccounts => Set<CashAccount>();
    public DbSet<CashTransaction> CashTransactions => Set<CashTransaction>();
    public DbSet<ApprovalRule> ApprovalRules => Set<ApprovalRule>();
    public DbSet<ApprovalRequest> ApprovalRequests => Set<ApprovalRequest>();
    public DbSet<ApprovalStep> ApprovalSteps => Set<ApprovalStep>();
    public DbSet<ApprovalAction> ApprovalActions => Set<ApprovalAction>();
    public DbSet<AuditLog> AuditLogs => Set<AuditLog>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(NuvantaDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}
