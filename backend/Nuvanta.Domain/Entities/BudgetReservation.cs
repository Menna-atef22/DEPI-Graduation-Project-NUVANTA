namespace Nuvanta.Domain.Entities;

public class BudgetReservation
{
    public int ReservationID { get; set; }
    public int AllocationID { get; set; }
    public int PurchaseRequestID { get; set; }
    public decimal ReservedAmount { get; set; }
    public string Status { get; set; } = string.Empty;
    public DateTime? ReservedAt { get; set; }
    public DateTime? ReleasedAt { get; set; }

    public BudgetAllocation Allocation { get; set; } = null!;
    public PurchaseRequest PurchaseRequest { get; set; } = null!;
}
