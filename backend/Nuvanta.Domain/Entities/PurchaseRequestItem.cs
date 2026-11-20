namespace Nuvanta.Domain.Entities;

public class PurchaseRequestItem
{
    public int RequestItemID { get; set; }
    public int PurchaseRequestID { get; set; }
    public int? ItemID { get; set; }
    public string ItemDescription { get; set; } = string.Empty;
    public decimal Quantity { get; set; }
    public decimal EstimatedUnitPrice { get; set; }
    // Computed column (DB-generated, read-only)
    public decimal LineTotal { get; set; }

    public PurchaseRequest PurchaseRequest { get; set; } = null!;
    public InventoryItem? Item { get; set; }

    public ICollection<PurchaseOrderItem> PurchaseOrderItems { get; set; } = new List<PurchaseOrderItem>();
}
