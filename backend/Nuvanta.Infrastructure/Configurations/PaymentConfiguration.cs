using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nuvanta.Domain.Entities;

namespace Nuvanta.Infrastructure.Configurations;

public class PaymentConfiguration : IEntityTypeConfiguration<Payment>
{
    public void Configure(EntityTypeBuilder<Payment> builder)
    {
        builder.ToTable("Payments");

        builder.HasKey(e => e.PaymentID);

        builder.Property(e => e.PaymentID)
            .ValueGeneratedOnAdd();
        builder.Property(e => e.PaymentDate)
            .HasColumnType("datetime2(3)");
        builder.Property(e => e.Amount)
            .HasPrecision(18, 4);
        builder.Property(e => e.PaymentMethod)
            .HasMaxLength(50);
        builder.Property(e => e.ProviderName)
            .HasMaxLength(100);
        builder.Property(e => e.ProviderTransactionID)
            .HasMaxLength(150);
        builder.Property(e => e.PaymentReference)
            .HasMaxLength(150);
        builder.Property(e => e.TransactionReference)
            .HasMaxLength(150);
        builder.Property(e => e.Status)
            .HasMaxLength(50);

        builder.HasOne(d => d.Company)
            .WithMany(p => p.Payments)
            .HasForeignKey(d => d.CompanyID)
            .OnDelete(DeleteBehavior.NoAction)
            .HasConstraintName("FK_Payments_CompanyID");

        builder.HasOne(d => d.Supplier)
            .WithMany(p => p.Payments)
            .HasForeignKey(d => d.SupplierID)
            .OnDelete(DeleteBehavior.SetNull)
            .HasConstraintName("FK_Payments_SupplierID");

        builder.HasOne(d => d.RecordedByUser)
            .WithMany(p => p.Payments)
            .HasForeignKey(d => d.RecordedBy)
            .OnDelete(DeleteBehavior.NoAction)
            .HasConstraintName("FK_Payments_RecordedBy");
    }
}
