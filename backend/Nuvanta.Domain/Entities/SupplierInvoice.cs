namespace Nuvanta.Domain.Entities;

public class SupplierInvoice
{
    public int SupplierInvoiceID { get; set; }
    public int CompanyID { get; set; }
    public int SupplierID { get; set; }
    public int? PurchaseOrderID { get; set; }
    public string InvoiceNumber { get; set; } = string.Empty;
    public DateOnly InvoiceDate { get; set; }
    public DateOnly? DueDate { get; set; }
    public string CurrencyCode { get; set; } = string.Empty;
    public decimal TotalAmount { get; set; }
    public string Status { get; set; } = string.Empty;
    public DateTime? CreatedAt { get; set; }
    public int CreatedBy { get; set; }

    public Company Company { get; set; } = null!;
    public Supplier Supplier { get; set; } = null!;
    public PurchaseOrder? PurchaseOrder { get; set; }
    public User CreatedByUser { get; set; } = null!;

    public ICollection<SupplierInvoiceItem> SupplierInvoiceItems { get; set; } = new List<SupplierInvoiceItem>();
    public ICollection<PaymentAllocation> PaymentAllocations { get; set; } = new List<PaymentAllocation>();
}
