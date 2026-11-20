using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nuvanta.Domain.Entities;

namespace Nuvanta.Infrastructure.Configurations;

public class PaymentAllocationConfiguration : IEntityTypeConfiguration<PaymentAllocation>
{
    public void Configure(EntityTypeBuilder<PaymentAllocation> builder)
    {
        builder.ToTable("PaymentAllocations");

        builder.HasKey(e => e.PaymentAllocationID);

        builder.Property(e => e.PaymentAllocationID)
            .ValueGeneratedOnAdd();
        builder.Property(e => e.AllocatedAmount)
            .HasPrecision(18, 4);

        builder.HasOne(d => d.Payment)
            .WithMany(p => p.PaymentAllocations)
            .HasForeignKey(d => d.PaymentID)
            .OnDelete(DeleteBehavior.Cascade)
            .HasConstraintName("FK_PaymentAllocations_PaymentID");

        builder.HasOne(d => d.SupplierInvoice)
            .WithMany(p => p.PaymentAllocations)
            .HasForeignKey(d => d.SupplierInvoiceID)
            .OnDelete(DeleteBehavior.Cascade)
            .HasConstraintName("FK_PaymentAllocations_SupplierInvoiceID");
    }
}
