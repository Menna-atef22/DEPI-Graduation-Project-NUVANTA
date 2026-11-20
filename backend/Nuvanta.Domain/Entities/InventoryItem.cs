namespace Nuvanta.Domain.Entities;

public class InventoryItem
{
    public int ItemID { get; set; }
    public int CompanyID { get; set; }
    public int? InventoryCategoryID { get; set; }
    public string ItemName { get; set; } = string.Empty;
    public string SKU { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string UnitOfMeasure { get; set; } = string.Empty;
    public decimal? MinimumStockLevel { get; set; }
    public bool IsActive { get; set; } = true;

    public Company Company { get; set; } = null!;
    public InventoryCategory? InventoryCategory { get; set; }

    public ICollection<PurchaseRequestItem> PurchaseRequestItems { get; set; } = new List<PurchaseRequestItem>();
    public ICollection<PurchaseOrderItem> PurchaseOrderItems { get; set; } = new List<PurchaseOrderItem>();
    public ICollection<StockMovement> StockMovements { get; set; } = new List<StockMovement>();
    public ICollection<InventoryBalance> InventoryBalances { get; set; } = new List<InventoryBalance>();
    public ICollection<SupplierInvoiceItem> SupplierInvoiceItems { get; set; } = new List<SupplierInvoiceItem>();
}
