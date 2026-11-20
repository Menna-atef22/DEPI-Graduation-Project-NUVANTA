namespace Nuvanta.Domain.Entities;

public class PurchaseOrderItem
{
    public int PurchaseOrderItemID { get; set; }
    public int PurchaseOrderID { get; set; }
    public int? RequestItemID { get; set; }
    public int ItemID { get; set; }
    public decimal Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    // Computed column (DB-generated, read-only)
    public decimal LineTotal { get; set; }
    // Computed column (DB-generated, read-only)
    public decimal ReceivedQuantity { get; set; }

    public PurchaseOrder PurchaseOrder { get; set; } = null!;
    public PurchaseRequestItem? RequestItem { get; set; }
    public InventoryItem Item { get; set; } = null!;

    public ICollection<GoodsReceiptItem> GoodsReceiptItems { get; set; } = new List<GoodsReceiptItem>();
    public ICollection<SupplierInvoiceItem> SupplierInvoiceItems { get; set; } = new List<SupplierInvoiceItem>();
}
