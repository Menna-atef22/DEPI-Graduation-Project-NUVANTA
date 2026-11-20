namespace Nuvanta.Domain.Entities;

public class StockMovement
{
    public int StockMovementID { get; set; }
    public int ItemID { get; set; }
    public int WarehouseID { get; set; }
    public int? GoodsReceiptItemID { get; set; }
    public string MovementType { get; set; } = string.Empty;
    public decimal Quantity { get; set; }
    public decimal? UnitCost { get; set; }
    public DateTime? MovementDate { get; set; }
    public string? ReferenceType { get; set; }
    public int? ReferenceID { get; set; }
    public int CreatedBy { get; set; }
    public string? Notes { get; set; }

    public InventoryItem Item { get; set; } = null!;
    public Warehouse Warehouse { get; set; } = null!;
    public GoodsReceiptItem? GoodsReceiptItem { get; set; }
    public User CreatedByUser { get; set; } = null!;
}
