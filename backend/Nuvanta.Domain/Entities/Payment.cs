namespace Nuvanta.Domain.Entities;

public class Payment
{
    public int PaymentID { get; set; }
    public int CompanyID { get; set; }
    public int? SupplierID { get; set; }
    public DateTime PaymentDate { get; set; }
    public decimal Amount { get; set; }
    public string PaymentMethod { get; set; } = string.Empty;
    public string? ProviderName { get; set; }
    public string? ProviderTransactionID { get; set; }
    public string? PaymentReference { get; set; }
    public string? FailureReason { get; set; }
    public string? TransactionReference { get; set; }
    public string Status { get; set; } = string.Empty;
    public int RecordedBy { get; set; }
    public string? Notes { get; set; }

    public Company Company { get; set; } = null!;
    public Supplier? Supplier { get; set; }
    public User RecordedByUser { get; set; } = null!;

    public ICollection<PaymentAllocation> PaymentAllocations { get; set; } = new List<PaymentAllocation>();
    public ICollection<CashTransaction> CashTransactions { get; set; } = new List<CashTransaction>();
}
