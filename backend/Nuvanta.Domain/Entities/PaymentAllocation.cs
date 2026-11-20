namespace Nuvanta.Domain.Entities;

public class PaymentAllocation
{
    public int PaymentAllocationID { get; set; }
    public int PaymentID { get; set; }
    public int SupplierInvoiceID { get; set; }
    public decimal AllocatedAmount { get; set; }

    public Payment Payment { get; set; } = null!;
    public SupplierInvoice SupplierInvoice { get; set; } = null!;
}
