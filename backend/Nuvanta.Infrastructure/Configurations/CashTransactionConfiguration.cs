using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nuvanta.Domain.Entities;

namespace Nuvanta.Infrastructure.Configurations;

public class CashTransactionConfiguration : IEntityTypeConfiguration<CashTransaction>
{
    public void Configure(EntityTypeBuilder<CashTransaction> builder)
    {
        builder.ToTable("CashTransactions");

        builder.HasKey(e => e.CashTransactionID);

        builder.Property(e => e.CashTransactionID)
            .ValueGeneratedOnAdd();
        builder.Property(e => e.TransactionDate)
            .HasColumnType("datetime2(3)");
        builder.Property(e => e.TransactionType)
            .HasMaxLength(50);
        builder.Property(e => e.Amount)
            .HasPrecision(18, 4);
        builder.Property(e => e.ReferenceNumber)
            .HasMaxLength(100);
        builder.Property(e => e.Status)
            .HasMaxLength(50);

        builder.HasOne(d => d.CashAccount)
            .WithMany(p => p.CashTransactions)
            .HasForeignKey(d => d.CashAccountID)
            .OnDelete(DeleteBehavior.Cascade)
            .HasConstraintName("FK_CashTransactions_CashAccountID");

        builder.HasOne(d => d.Payment)
            .WithMany(p => p.CashTransactions)
            .HasForeignKey(d => d.PaymentID)
            .OnDelete(DeleteBehavior.SetNull)
            .HasConstraintName("FK_CashTransactions_PaymentID");
    }
}
