namespace Nuvanta.Domain.Entities;

public class LedgerEntryLine
{
    public int LedgerEntryLineID { get; set; }
    public int LedgerEntryID { get; set; }
    public int AccountID { get; set; }
    public decimal DebitAmount { get; set; }
    public decimal CreditAmount { get; set; }
    public string? Description { get; set; }

    public LedgerEntry LedgerEntry { get; set; } = null!;
    public Account Account { get; set; } = null!;
}
