namespace Nuvanta.Domain.Entities;

public class Warehouse
{
    public int WarehouseID { get; set; }
    public int CompanyID { get; set; }
    public int? BranchID { get; set; }
    public string WarehouseName { get; set; } = string.Empty;
    public string? Location { get; set; }
    public string Status { get; set; } = string.Empty;

    public Company Company { get; set; } = null!;
    public Branch? Branch { get; set; }

    public ICollection<GoodsReceipt> GoodsReceipts { get; set; } = new List<GoodsReceipt>();
    public ICollection<StockMovement> StockMovements { get; set; } = new List<StockMovement>();
    public ICollection<InventoryBalance> InventoryBalances { get; set; } = new List<InventoryBalance>();
}
