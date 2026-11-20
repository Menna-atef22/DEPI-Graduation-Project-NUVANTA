using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nuvanta.Domain.Entities;

namespace Nuvanta.Infrastructure.Configurations;

public class SupplierInvoiceItemConfiguration : IEntityTypeConfiguration<SupplierInvoiceItem>
{
    public void Configure(EntityTypeBuilder<SupplierInvoiceItem> builder)
    {
        builder.ToTable("SupplierInvoiceItems");

        builder.HasKey(e => e.InvoiceItemID);

        builder.Property(e => e.InvoiceItemID)
            .ValueGeneratedOnAdd();
        builder.Property(e => e.Quantity)
            .HasPrecision(18, 4);
        builder.Property(e => e.UnitPrice)
            .HasPrecision(18, 4);
        builder.Property(e => e.TaxAmount)
            .HasPrecision(18, 4)
            .HasDefaultValue(0m);
        builder.Property(e => e.DiscountAmount)
            .HasPrecision(18, 4)
            .HasDefaultValue(0m);
        builder.Property(e => e.LineTotal)
            .HasComputedColumnSql("[Quantity] * [UnitPrice] - [DiscountAmount] + [TaxAmount]")
            .HasPrecision(18, 4);

        builder.HasOne(d => d.SupplierInvoice)
            .WithMany(p => p.SupplierInvoiceItems)
            .HasForeignKey(d => d.SupplierInvoiceID)
            .OnDelete(DeleteBehavior.Cascade)
            .HasConstraintName("FK_SupplierInvoiceItems_SupplierInvoiceID");

        builder.HasOne(d => d.PurchaseOrderItem)
            .WithMany(p => p.SupplierInvoiceItems)
            .HasForeignKey(d => d.PurchaseOrderItemID)
            .OnDelete(DeleteBehavior.NoAction)
            .HasConstraintName("FK_SupplierInvoiceItems_PurchaseOrderItemID");

        builder.HasOne(d => d.Item)
            .WithMany(p => p.SupplierInvoiceItems)
            .HasForeignKey(d => d.ItemID)
            .OnDelete(DeleteBehavior.SetNull)
            .HasConstraintName("FK_SupplierInvoiceItems_ItemID");
    }
}
