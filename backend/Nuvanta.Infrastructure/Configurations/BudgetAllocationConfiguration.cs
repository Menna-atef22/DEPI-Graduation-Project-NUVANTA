using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nuvanta.Domain.Entities;

namespace Nuvanta.Infrastructure.Configurations;

public class BudgetAllocationConfiguration : IEntityTypeConfiguration<BudgetAllocation>
{
    public void Configure(EntityTypeBuilder<BudgetAllocation> builder)
    {
        builder.ToTable("BudgetAllocations");

        builder.HasKey(e => e.AllocationID);

        builder.Property(e => e.AllocationID)
            .ValueGeneratedOnAdd();
        builder.Property(e => e.AllocatedAmount)
            .HasPrecision(18, 4);
        builder.Property(e => e.CreatedAt)
            .HasColumnType("datetime2(3)")
            .HasDefaultValueSql("SYSDATETIME()");

        builder.HasOne(d => d.Budget)
            .WithMany(p => p.BudgetAllocations)
            .HasForeignKey(d => d.BudgetID)
            .OnDelete(DeleteBehavior.Cascade)
            .HasConstraintName("FK_BudgetAllocations_BudgetID");

        builder.HasOne(d => d.Department)
            .WithMany(p => p.BudgetAllocations)
            .HasForeignKey(d => d.DepartmentID)
            .OnDelete(DeleteBehavior.SetNull)
            .HasConstraintName("FK_BudgetAllocations_DepartmentID");

        builder.HasOne(d => d.CostCenter)
            .WithMany(p => p.BudgetAllocations)
            .HasForeignKey(d => d.CostCenterID)
            .OnDelete(DeleteBehavior.NoAction)
            .HasConstraintName("FK_BudgetAllocations_CostCenterID");

        builder.HasOne(d => d.Category)
            .WithMany(p => p.BudgetAllocations)
            .HasForeignKey(d => d.CategoryID)
            .OnDelete(DeleteBehavior.SetNull)
            .HasConstraintName("FK_BudgetAllocations_CategoryID");
    }
}
