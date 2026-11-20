namespace Nuvanta.Domain.Entities;

public class GoodsReceiptItem
{
    public int GoodsReceiptItemID { get; set; }
    public int GoodsReceiptID { get; set; }
    public int PurchaseOrderItemID { get; set; }
    public decimal ReceivedQuantity { get; set; }
    public decimal AcceptedQuantity { get; set; }
    public decimal RejectedQuantity { get; set; }
    public string? Notes { get; set; }

    public GoodsReceipt GoodsReceipt { get; set; } = null!;
    public PurchaseOrderItem PurchaseOrderItem { get; set; } = null!;

    public ICollection<StockMovement> StockMovements { get; set; } = new List<StockMovement>();
}
