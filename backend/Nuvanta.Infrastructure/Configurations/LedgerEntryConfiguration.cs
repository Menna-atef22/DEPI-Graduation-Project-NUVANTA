using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nuvanta.Domain.Entities;

namespace Nuvanta.Infrastructure.Configurations;

public class LedgerEntryConfiguration : IEntityTypeConfiguration<LedgerEntry>
{
    public void Configure(EntityTypeBuilder<LedgerEntry> builder)
    {
        // Table has an AFTER UPDATE trigger -> EF must not use OUTPUT clause
        builder.ToTable("LedgerEntries", tb => { tb.HasTrigger("TR_LedgerEntries_UpdatedAt"); });

        builder.HasKey(e => e.LedgerEntryID);

        builder.Property(e => e.LedgerEntryID)
            .ValueGeneratedOnAdd();
        builder.Property(e => e.EntryDate)
            .HasColumnType("date");
        builder.Property(e => e.SourceType)
            .HasMaxLength(50);
        builder.Property(e => e.Status)
            .HasMaxLength(50);
        builder.Property(e => e.PostedAt)
            .HasColumnType("datetime2(3)");
        builder.Property(e => e.CurrencyCode)
            .HasMaxLength(10);
        builder.Property(e => e.UpdatedAt)
            .HasColumnType("datetime2(3)")
            .HasDefaultValueSql("SYSDATETIME()");

        builder.HasOne(d => d.Company)
            .WithMany(p => p.LedgerEntries)
            .HasForeignKey(d => d.CompanyID)
            .OnDelete(DeleteBehavior.NoAction)
            .HasConstraintName("FK_LedgerEntries_CompanyID");

        builder.HasOne(d => d.FinancialPeriod)
            .WithMany(p => p.LedgerEntries)
            .HasForeignKey(d => d.FinancialPeriodID)
            .OnDelete(DeleteBehavior.NoAction)
            .HasConstraintName("FK_LedgerEntries_FinancialPeriodID");

        builder.HasOne(d => d.CreatedByUser)
            .WithMany(p => p.LedgerEntries)
            .HasForeignKey(d => d.CreatedBy)
            .OnDelete(DeleteBehavior.NoAction)
            .HasConstraintName("FK_LedgerEntries_CreatedBy");

        builder.HasOne(d => d.ReversedByEntry)
            .WithMany(p => p.ReversedEntries)
            .HasForeignKey(d => d.ReversedByEntryID)
            .OnDelete(DeleteBehavior.NoAction)
            .HasConstraintName("FK_LedgerEntries_ReversedByEntryID");
    }
}
