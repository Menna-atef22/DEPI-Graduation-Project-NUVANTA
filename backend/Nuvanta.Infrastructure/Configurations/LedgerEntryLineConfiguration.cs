using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nuvanta.Domain.Entities;

namespace Nuvanta.Infrastructure.Configurations;

public class LedgerEntryLineConfiguration : IEntityTypeConfiguration<LedgerEntryLine>
{
    public void Configure(EntityTypeBuilder<LedgerEntryLine> builder)
    {
        builder.ToTable("LedgerEntryLines");

        builder.HasKey(e => e.LedgerEntryLineID);

        builder.Property(e => e.LedgerEntryLineID)
            .ValueGeneratedOnAdd();
        builder.Property(e => e.DebitAmount)
            .HasPrecision(18, 4);
        builder.Property(e => e.CreditAmount)
            .HasPrecision(18, 4);

        builder.HasOne(d => d.LedgerEntry)
            .WithMany(p => p.LedgerEntryLines)
            .HasForeignKey(d => d.LedgerEntryID)
            .OnDelete(DeleteBehavior.Cascade)
            .HasConstraintName("FK_LedgerEntryLines_LedgerEntryID");

        builder.HasOne(d => d.Account)
            .WithMany(p => p.LedgerEntryLines)
            .HasForeignKey(d => d.AccountID)
            .OnDelete(DeleteBehavior.NoAction)
            .HasConstraintName("FK_LedgerEntryLines_AccountID");
    }
}
