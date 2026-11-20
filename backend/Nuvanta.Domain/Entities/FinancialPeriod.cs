namespace Nuvanta.Domain.Entities;

public class FinancialPeriod
{
    public int FinancialPeriodID { get; set; }
    public int CompanyID { get; set; }
    public string PeriodName { get; set; } = string.Empty;
    public DateOnly StartDate { get; set; }
    public DateOnly EndDate { get; set; }
    public string Status { get; set; } = string.Empty;

    public Company Company { get; set; } = null!;

    public ICollection<Budget> Budgets { get; set; } = new List<Budget>();
    public ICollection<LedgerEntry> LedgerEntries { get; set; } = new List<LedgerEntry>();
}
