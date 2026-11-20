using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nuvanta.Domain.Entities;

namespace Nuvanta.Infrastructure.Configurations;

public class BudgetReservationConfiguration : IEntityTypeConfiguration<BudgetReservation>
{
    public void Configure(EntityTypeBuilder<BudgetReservation> builder)
    {
        builder.ToTable("BudgetReservations");

        builder.HasKey(e => e.ReservationID);

        builder.Property(e => e.ReservationID)
            .ValueGeneratedOnAdd();
        builder.Property(e => e.ReservedAmount)
            .HasPrecision(18, 4);
        builder.Property(e => e.Status)
            .HasMaxLength(50);
        builder.Property(e => e.ReservedAt)
            .HasColumnType("datetime2(3)")
            .HasDefaultValueSql("SYSDATETIME()");
        builder.Property(e => e.ReleasedAt)
            .HasColumnType("datetime2(3)");

        builder.HasOne(d => d.Allocation)
            .WithMany(p => p.BudgetReservations)
            .HasForeignKey(d => d.AllocationID)
            .OnDelete(DeleteBehavior.Cascade)
            .HasConstraintName("FK_BudgetReservations_AllocationID");

        builder.HasOne(d => d.PurchaseRequest)
            .WithMany(p => p.BudgetReservations)
            .HasForeignKey(d => d.PurchaseRequestID)
            .OnDelete(DeleteBehavior.Cascade)
            .HasConstraintName("FK_BudgetReservations_PurchaseRequestID");
    }
}
