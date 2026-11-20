namespace Nuvanta.Domain.Entities;

public class CashAccount
{
    public int CashAccountID { get; set; }
    public int CompanyID { get; set; }
    public string AccountName { get; set; } = string.Empty;
    public string AccountType { get; set; } = string.Empty;
    public string CurrencyCode { get; set; } = string.Empty;
    public decimal OpeningBalance { get; set; }
    public bool IsActive { get; set; } = true;

    public Company Company { get; set; } = null!;

    public ICollection<CashTransaction> CashTransactions { get; set; } = new List<CashTransaction>();
}
