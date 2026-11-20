namespace Nuvanta.Domain.Entities;

public class Account
{
    public int AccountID { get; set; }
    public int CompanyID { get; set; }
    public int? ParentAccountID { get; set; }
    public string AccountCode { get; set; } = string.Empty;
    public string AccountName { get; set; } = string.Empty;
    public string AccountType { get; set; } = string.Empty;
    public bool IsActive { get; set; } = true;

    public Company Company { get; set; } = null!;
    public Account? ParentAccount { get; set; }

    public ICollection<Account> ChildAccounts { get; set; } = new List<Account>();
    public ICollection<LedgerEntryLine> LedgerEntryLines { get; set; } = new List<LedgerEntryLine>();
}
