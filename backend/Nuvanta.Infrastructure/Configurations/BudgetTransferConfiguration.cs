using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nuvanta.Domain.Entities;

namespace Nuvanta.Infrastructure.Configurations;

public class BudgetTransferConfiguration : IEntityTypeConfiguration<BudgetTransfer>
{
    public void Configure(EntityTypeBuilder<BudgetTransfer> builder)
    {
        // Table has an AFTER UPDATE trigger -> EF must not use OUTPUT clause
        builder.ToTable("BudgetTransfers", tb => { tb.HasTrigger("TR_BudgetTransfers_UpdatedAt"); });

        builder.HasKey(e => e.TransferID);

        builder.Property(e => e.TransferID)
            .ValueGeneratedOnAdd();
        builder.Property(e => e.Amount)
            .HasPrecision(18, 4);
        builder.Property(e => e.TransferDate)
            .HasColumnType("datetime2(3)");
        builder.Property(e => e.Status)
            .HasMaxLength(50);
        builder.Property(e => e.CreatedAt)
            .HasColumnType("datetime2(3)")
            .HasDefaultValueSql("SYSDATETIME()");
        builder.Property(e => e.UpdatedAt)
            .HasColumnType("datetime2(3)")
            .HasDefaultValueSql("SYSDATETIME()");

        builder.HasOne(d => d.FromAllocation)
            .WithMany(p => p.BudgetTransfersFromAllocation)
            .HasForeignKey(d => d.FromAllocationID)
            .OnDelete(DeleteBehavior.NoAction)
            .HasConstraintName("FK_BudgetTransfers_FromAllocationID");

        builder.HasOne(d => d.ToAllocation)
            .WithMany(p => p.BudgetTransfersToAllocation)
            .HasForeignKey(d => d.ToAllocationID)
            .OnDelete(DeleteBehavior.NoAction)
            .HasConstraintName("FK_BudgetTransfers_ToAllocationID");

        builder.HasOne(d => d.RequestedByUser)
            .WithMany(p => p.BudgetTransfersRequestedByUser)
            .HasForeignKey(d => d.RequestedBy)
            .OnDelete(DeleteBehavior.NoAction)
            .HasConstraintName("FK_BudgetTransfers_RequestedBy");

        builder.HasOne(d => d.ApprovedByUser)
            .WithMany(p => p.BudgetTransfersApprovedByUser)
            .HasForeignKey(d => d.ApprovedBy)
            .OnDelete(DeleteBehavior.NoAction)
            .HasConstraintName("FK_BudgetTransfers_ApprovedBy");
    }
}
