namespace Nuvanta.Domain.Entities;

public class ExpenseCategory
{
    public int CategoryID { get; set; }
    public int CompanyID { get; set; }
    public string CategoryName { get; set; } = string.Empty;
    public string? Description { get; set; }

    public Company Company { get; set; } = null!;

    public ICollection<BudgetAllocation> BudgetAllocations { get; set; } = new List<BudgetAllocation>();
}
