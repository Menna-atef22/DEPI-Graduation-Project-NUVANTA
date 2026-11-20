namespace Nuvanta.Domain.Entities;

public class BudgetTransfer
{
    public int TransferID { get; set; }
    public int FromAllocationID { get; set; }
    public int ToAllocationID { get; set; }
    public decimal Amount { get; set; }
    public int RequestedBy { get; set; }
    public int? ApprovedBy { get; set; }
    public DateTime TransferDate { get; set; }
    public string Status { get; set; } = string.Empty;
    public string? Reason { get; set; }
    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }

    public BudgetAllocation FromAllocation { get; set; } = null!;
    public BudgetAllocation ToAllocation { get; set; } = null!;
    public User RequestedByUser { get; set; } = null!;
    public User? ApprovedByUser { get; set; }
}
