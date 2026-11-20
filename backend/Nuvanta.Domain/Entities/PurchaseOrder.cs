namespace Nuvanta.Domain.Entities;

public class PurchaseOrder
{
    public int PurchaseOrderID { get; set; }
    public int CompanyID { get; set; }
    public int SupplierID { get; set; }
    public int? PurchaseRequestID { get; set; }
    public DateOnly OrderDate { get; set; }
    public DateOnly? ExpectedDeliveryDate { get; set; }
    public string? DeliveryAddress { get; set; }
    public string CurrencyCode { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public decimal TotalAmount { get; set; }
    public int CreatedBy { get; set; }

    public Company Company { get; set; } = null!;
    public Supplier Supplier { get; set; } = null!;
    public PurchaseRequest? PurchaseRequest { get; set; }
    public User CreatedByUser { get; set; } = null!;

    public ICollection<PurchaseOrderItem> PurchaseOrderItems { get; set; } = new List<PurchaseOrderItem>();
    public ICollection<GoodsReceipt> GoodsReceipts { get; set; } = new List<GoodsReceipt>();
    public ICollection<SupplierInvoice> SupplierInvoices { get; set; } = new List<SupplierInvoice>();
}
