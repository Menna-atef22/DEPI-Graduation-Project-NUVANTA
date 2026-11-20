using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nuvanta.Domain.Entities;

namespace Nuvanta.Infrastructure.Configurations;

public class PurchaseRequestConfiguration : IEntityTypeConfiguration<PurchaseRequest>
{
    public void Configure(EntityTypeBuilder<PurchaseRequest> builder)
    {
        // Table has an AFTER UPDATE trigger -> EF must not use OUTPUT clause
        builder.ToTable("PurchaseRequests", tb => { tb.HasTrigger("TR_PurchaseRequests_UpdatedAt"); });

        builder.HasKey(e => e.PurchaseRequestID);

        builder.Property(e => e.PurchaseRequestID)
            .ValueGeneratedOnAdd();
        builder.Property(e => e.RequestDate)
            .HasColumnType("datetime2(3)");
        builder.Property(e => e.SubmittedAt)
            .HasColumnType("datetime2(3)");
        builder.Property(e => e.ApprovedAt)
            .HasColumnType("datetime2(3)");
        builder.Property(e => e.Status)
            .HasMaxLength(50);
        builder.Property(e => e.UpdatedAt)
            .HasColumnType("datetime2(3)")
            .HasDefaultValueSql("SYSDATETIME()");
        builder.Property(e => e.ExpectedTotalAmount)
            .HasComputedColumnSql("[dbo].[fn_PRExpectedTotal]([PurchaseRequestID])")
            .HasPrecision(18, 4);

        builder.HasOne(d => d.Company)
            .WithMany(p => p.PurchaseRequests)
            .HasForeignKey(d => d.CompanyID)
            .OnDelete(DeleteBehavior.NoAction)
            .HasConstraintName("FK_PurchaseRequests_CompanyID");

        builder.HasOne(d => d.RequestedByUser)
            .WithMany(p => p.PurchaseRequests)
            .HasForeignKey(d => d.RequestedBy)
            .OnDelete(DeleteBehavior.NoAction)
            .HasConstraintName("FK_PurchaseRequests_RequestedBy");

        builder.HasOne(d => d.Department)
            .WithMany(p => p.PurchaseRequests)
            .HasForeignKey(d => d.DepartmentID)
            .OnDelete(DeleteBehavior.NoAction)
            .HasConstraintName("FK_PurchaseRequests_DepartmentID");

        builder.HasOne(d => d.Budget)
            .WithMany(p => p.PurchaseRequests)
            .HasForeignKey(d => d.BudgetID)
            .OnDelete(DeleteBehavior.NoAction)
            .HasConstraintName("FK_PurchaseRequests_BudgetID");

        builder.HasOne(d => d.CostCenter)
            .WithMany(p => p.PurchaseRequests)
            .HasForeignKey(d => d.CostCenterID)
            .OnDelete(DeleteBehavior.NoAction)
            .HasConstraintName("FK_PurchaseRequests_CostCenterID");
    }
}
