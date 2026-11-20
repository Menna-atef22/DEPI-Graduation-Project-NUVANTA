namespace Nuvanta.Domain.Entities;

public class LedgerEntry
{
    public int LedgerEntryID { get; set; }
    public int CompanyID { get; set; }
    public int FinancialPeriodID { get; set; }
    public DateOnly EntryDate { get; set; }
    public string? Description { get; set; }
    public string? SourceType { get; set; }
    public int? SourceID { get; set; }
    public string Status { get; set; } = string.Empty;
    public int CreatedBy { get; set; }
    public DateTime? PostedAt { get; set; }
    public int? ReversedByEntryID { get; set; }
    public string CurrencyCode { get; set; } = string.Empty;
    public DateTime? UpdatedAt { get; set; }

    public Company Company { get; set; } = null!;
    public FinancialPeriod FinancialPeriod { get; set; } = null!;
    public User CreatedByUser { get; set; } = null!;
    public LedgerEntry? ReversedByEntry { get; set; }

    public ICollection<LedgerEntry> ReversedEntries { get; set; } = new List<LedgerEntry>();
    public ICollection<LedgerEntryLine> LedgerEntryLines { get; set; } = new List<LedgerEntryLine>();
}
