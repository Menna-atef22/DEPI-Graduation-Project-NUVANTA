namespace Nuvanta.Domain.Entities;

public class Supplier
{
    public int SupplierID { get; set; }
    public int CompanyID { get; set; }
    public string SupplierName { get; set; } = string.Empty;
    public string? TaxNumber { get; set; }
    public string? Email { get; set; }
    public string? Phone { get; set; }
    public string? Address { get; set; }
    public string Status { get; set; } = string.Empty;
    public DateTime? CreatedAt { get; set; }

    public Company Company { get; set; } = null!;

    public ICollection<PurchaseOrder> PurchaseOrders { get; set; } = new List<PurchaseOrder>();
    public ICollection<SupplierInvoice> SupplierInvoices { get; set; } = new List<SupplierInvoice>();
    public ICollection<Payment> Payments { get; set; } = new List<Payment>();
}
