namespace Nuvanta.Domain.Entities;

public class SupplierInvoiceItem
{
    public int InvoiceItemID { get; set; }
    public int SupplierInvoiceID { get; set; }
    public int? PurchaseOrderItemID { get; set; }
    public int? ItemID { get; set; }
    public string Description { get; set; } = string.Empty;
    public decimal Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal? TaxAmount { get; set; }
    public decimal? DiscountAmount { get; set; }
    // Computed column (DB-generated, read-only)
    public decimal? LineTotal { get; set; }

    public SupplierInvoice SupplierInvoice { get; set; } = null!;
    public PurchaseOrderItem? PurchaseOrderItem { get; set; }
    public InventoryItem? Item { get; set; }
}
