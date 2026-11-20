using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nuvanta.Domain.Entities;

namespace Nuvanta.Infrastructure.Configurations;

public class BudgetConfiguration : IEntityTypeConfiguration<Budget>
{
    public void Configure(EntityTypeBuilder<Budget> builder)
    {
        // Table has an AFTER UPDATE trigger -> EF must not use OUTPUT clause
        builder.ToTable("Budgets", tb => { tb.HasTrigger("TR_Budgets_UpdatedAt"); });

        builder.HasKey(e => e.BudgetID);

        builder.Property(e => e.BudgetID)
            .ValueGeneratedOnAdd();
        builder.Property(e => e.BudgetName)
            .HasMaxLength(150);
        builder.Property(e => e.TotalAmount)
            .HasPrecision(18, 4);
        builder.Property(e => e.CurrencyCode)
            .HasMaxLength(10);
        builder.Property(e => e.Status)
            .HasMaxLength(50);
        builder.Property(e => e.ApprovedAt)
            .HasColumnType("datetime2(3)");
        builder.Property(e => e.CreatedAt)
            .HasColumnType("datetime2(3)")
            .HasDefaultValueSql("SYSDATETIME()");
        builder.Property(e => e.UpdatedAt)
            .HasColumnType("datetime2(3)")
            .HasDefaultValueSql("SYSDATETIME()");

        builder.HasOne(d => d.Company)
            .WithMany(p => p.Budgets)
            .HasForeignKey(d => d.CompanyID)
            .OnDelete(DeleteBehavior.NoAction)
            .HasConstraintName("FK_Budgets_CompanyID");

        builder.HasOne(d => d.FinancialPeriod)
            .WithMany(p => p.Budgets)
            .HasForeignKey(d => d.FinancialPeriodID)
            .OnDelete(DeleteBehavior.Cascade)
            .HasConstraintName("FK_Budgets_FinancialPeriodID");

        builder.HasOne(d => d.CreatedByUser)
            .WithMany(p => p.BudgetsCreatedByUser)
            .HasForeignKey(d => d.CreatedBy)
            .OnDelete(DeleteBehavior.NoAction)
            .HasConstraintName("FK_Budgets_CreatedBy");

        builder.HasOne(d => d.ApprovedByUser)
            .WithMany(p => p.BudgetsApprovedByUser)
            .HasForeignKey(d => d.ApprovedBy)
            .OnDelete(DeleteBehavior.NoAction)
            .HasConstraintName("FK_Budgets_ApprovedBy");
    }
}
