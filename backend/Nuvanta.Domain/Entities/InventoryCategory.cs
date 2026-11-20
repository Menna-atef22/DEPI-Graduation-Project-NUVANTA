namespace Nuvanta.Domain.Entities;

public class InventoryCategory
{
    public int InventoryCategoryID { get; set; }
    public int CompanyID { get; set; }
    public string CategoryName { get; set; } = string.Empty;
    public string? Description { get; set; }
    public bool IsActive { get; set; } = true;

    public Company Company { get; set; } = null!;

    public ICollection<InventoryItem> InventoryItems { get; set; } = new List<InventoryItem>();
}
