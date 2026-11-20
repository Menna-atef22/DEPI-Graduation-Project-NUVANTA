namespace Nuvanta.Domain.Entities;

public class InventoryBalance
{
    public int InventoryBalanceID { get; set; }
    public int ItemID { get; set; }
    public int WarehouseID { get; set; }
    public decimal QuantityOnHand { get; set; }
    public DateTime? LastUpdatedAt { get; set; }

    public InventoryItem Item { get; set; } = null!;
    public Warehouse Warehouse { get; set; } = null!;
}
