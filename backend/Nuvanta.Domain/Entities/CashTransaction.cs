namespace Nuvanta.Domain.Entities;

public class CashTransaction
{
    public int CashTransactionID { get; set; }
    public int CashAccountID { get; set; }
    public int? PaymentID { get; set; }
    public DateTime TransactionDate { get; set; }
    public string TransactionType { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    public string? ReferenceNumber { get; set; }
    public string Status { get; set; } = string.Empty;

    public CashAccount CashAccount { get; set; } = null!;
    public Payment? Payment { get; set; }
}
