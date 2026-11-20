namespace Nuvanta.Domain.Entities;

public class BudgetAllocation
{
    public int AllocationID { get; set; }
    public int BudgetID { get; set; }
    public int? DepartmentID { get; set; }
    public int? CostCenterID { get; set; }
    public int? CategoryID { get; set; }
    public decimal AllocatedAmount { get; set; }
    public DateTime? CreatedAt { get; set; }

    public Budget Budget { get; set; } = null!;
    public Department? Department { get; set; }
    public CostCenter? CostCenter { get; set; }
    public ExpenseCategory? Category { get; set; }

    public ICollection<BudgetTransfer> BudgetTransfersFromAllocation { get; set; } = new List<BudgetTransfer>();
    public ICollection<BudgetTransfer> BudgetTransfersToAllocation { get; set; } = new List<BudgetTransfer>();
    public ICollection<BudgetReservation> BudgetReservations { get; set; } = new List<BudgetReservation>();
}
