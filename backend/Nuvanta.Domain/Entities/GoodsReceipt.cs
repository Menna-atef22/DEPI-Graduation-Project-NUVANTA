namespace Nuvanta.Domain.Entities;

public class GoodsReceipt
{
    public int GoodsReceiptID { get; set; }
    public int PurchaseOrderID { get; set; }
    public int WarehouseID { get; set; }
    public int ReceivedBy { get; set; }
    public string ReceiptNumber { get; set; } = string.Empty;
    public DateOnly ReceiptDate { get; set; }
    public string Status { get; set; } = string.Empty;
    public string? Notes { get; set; }

    public PurchaseOrder PurchaseOrder { get; set; } = null!;
    public Warehouse Warehouse { get; set; } = null!;
    public User ReceivedByUser { get; set; } = null!;

    public ICollection<GoodsReceiptItem> GoodsReceiptItems { get; set; } = new List<GoodsReceiptItem>();
}
