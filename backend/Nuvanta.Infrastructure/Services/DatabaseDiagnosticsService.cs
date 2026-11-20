using Microsoft.EntityFrameworkCore;
using Nuvanta.Application.DTOs;
using Nuvanta.Application.Interfaces;
using Nuvanta.Infrastructure.Persistence;

namespace Nuvanta.Infrastructure.Services;

/// <summary>Reads every mapped table once: proves the model matches the real SQL Server schema.</summary>
public class DatabaseDiagnosticsService : IDatabaseDiagnosticsService
{
    private readonly NuvantaDbContext _db;
    public DatabaseDiagnosticsService(NuvantaDbContext db) => _db = db;

    public async Task<DatabaseHealthDto> CheckAsync(CancellationToken ct = default)
    {
        var result = new DatabaseHealthDto();
        result.CanConnect = await _db.Database.CanConnectAsync(ct);
        if (!result.CanConnect) return result;

        result.Tables.Add(await ProbeAsync("Companies", _db.Companies, ct));
        result.Tables.Add(await ProbeAsync("Branches", _db.Branches, ct));
        result.Tables.Add(await ProbeAsync("Departments", _db.Departments, ct));
        result.Tables.Add(await ProbeAsync("Users", _db.Users, ct));
        result.Tables.Add(await ProbeAsync("Roles", _db.Roles, ct));
        result.Tables.Add(await ProbeAsync("UserRoles", _db.UserRoles, ct));
        result.Tables.Add(await ProbeAsync("Permissions", _db.Permissions, ct));
        result.Tables.Add(await ProbeAsync("RolePermissions", _db.RolePermissions, ct));
        result.Tables.Add(await ProbeAsync("FinancialPeriods", _db.FinancialPeriods, ct));
        result.Tables.Add(await ProbeAsync("CostCenters", _db.CostCenters, ct));
        result.Tables.Add(await ProbeAsync("ExpenseCategories", _db.ExpenseCategories, ct));
        result.Tables.Add(await ProbeAsync("Budgets", _db.Budgets, ct));
        result.Tables.Add(await ProbeAsync("BudgetAllocations", _db.BudgetAllocations, ct));
        result.Tables.Add(await ProbeAsync("BudgetTransfers", _db.BudgetTransfers, ct));
        result.Tables.Add(await ProbeAsync("Suppliers", _db.Suppliers, ct));
        result.Tables.Add(await ProbeAsync("Warehouses", _db.Warehouses, ct));
        result.Tables.Add(await ProbeAsync("InventoryCategories", _db.InventoryCategories, ct));
        result.Tables.Add(await ProbeAsync("InventoryItems", _db.InventoryItems, ct));
        result.Tables.Add(await ProbeAsync("PurchaseRequests", _db.PurchaseRequests, ct));
        result.Tables.Add(await ProbeAsync("BudgetReservations", _db.BudgetReservations, ct));
        result.Tables.Add(await ProbeAsync("PurchaseRequestItems", _db.PurchaseRequestItems, ct));
        result.Tables.Add(await ProbeAsync("PurchaseOrders", _db.PurchaseOrders, ct));
        result.Tables.Add(await ProbeAsync("PurchaseOrderItems", _db.PurchaseOrderItems, ct));
        result.Tables.Add(await ProbeAsync("GoodsReceipts", _db.GoodsReceipts, ct));
        result.Tables.Add(await ProbeAsync("GoodsReceiptItems", _db.GoodsReceiptItems, ct));
        result.Tables.Add(await ProbeAsync("StockMovements", _db.StockMovements, ct));
        result.Tables.Add(await ProbeAsync("InventoryBalances", _db.InventoryBalances, ct));
        result.Tables.Add(await ProbeAsync("SupplierInvoices", _db.SupplierInvoices, ct));
        result.Tables.Add(await ProbeAsync("SupplierInvoiceItems", _db.SupplierInvoiceItems, ct));
        result.Tables.Add(await ProbeAsync("Payments", _db.Payments, ct));
        result.Tables.Add(await ProbeAsync("PaymentAllocations", _db.PaymentAllocations, ct));
        result.Tables.Add(await ProbeAsync("Accounts", _db.Accounts, ct));
        result.Tables.Add(await ProbeAsync("LedgerEntries", _db.LedgerEntries, ct));
        result.Tables.Add(await ProbeAsync("LedgerEntryLines", _db.LedgerEntryLines, ct));
        result.Tables.Add(await ProbeAsync("CashAccounts", _db.CashAccounts, ct));
        result.Tables.Add(await ProbeAsync("CashTransactions", _db.CashTransactions, ct));
        result.Tables.Add(await ProbeAsync("ApprovalRules", _db.ApprovalRules, ct));
        result.Tables.Add(await ProbeAsync("ApprovalRequests", _db.ApprovalRequests, ct));
        result.Tables.Add(await ProbeAsync("ApprovalSteps", _db.ApprovalSteps, ct));
        result.Tables.Add(await ProbeAsync("ApprovalActions", _db.ApprovalActions, ct));
        result.Tables.Add(await ProbeAsync("AuditLogs", _db.AuditLogs, ct));

        return result;
    }

    private static async Task<TableProbeDto> ProbeAsync<T>(string name, IQueryable<T> query, CancellationToken ct) where T : class
    {
        try
        {
            var count = await query.AsNoTracking().CountAsync(ct);
            // Materialising one full row validates every column mapping (names, types, computed columns).
            await query.AsNoTracking().Take(1).ToListAsync(ct);
            return new TableProbeDto { Table = name, Ok = true, RowCount = count };
        }
        catch (Exception ex)
        {
            return new TableProbeDto { Table = name, Ok = false, Error = (ex.InnerException ?? ex).Message };
        }
    }
}
