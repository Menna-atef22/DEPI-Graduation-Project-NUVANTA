using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nuvanta.Domain.Entities;

namespace Nuvanta.Infrastructure.Configurations;

public class SupplierInvoiceConfiguration : IEntityTypeConfiguration<SupplierInvoice>
{
    public void Configure(EntityTypeBuilder<SupplierInvoice> builder)
    {
        builder.ToTable("SupplierInvoices");

        builder.HasKey(e => e.SupplierInvoiceID);

        builder.Property(e => e.SupplierInvoiceID)
            .ValueGeneratedOnAdd();
        builder.Property(e => e.InvoiceNumber)
            .HasMaxLength(100);
        builder.Property(e => e.InvoiceDate)
            .HasColumnType("date");
        builder.Property(e => e.DueDate)
            .HasColumnType("date");
        builder.Property(e => e.CurrencyCode)
            .HasMaxLength(10);
        builder.Property(e => e.TotalAmount)
            .HasPrecision(18, 4);
        builder.Property(e => e.Status)
            .HasMaxLength(50);
        builder.Property(e => e.CreatedAt)
            .HasColumnType("datetime2(3)")
            .HasDefaultValueSql("SYSDATETIME()");

        builder.HasOne(d => d.Company)
            .WithMany(p => p.SupplierInvoices)
            .HasForeignKey(d => d.CompanyID)
            .OnDelete(DeleteBehavior.NoAction)
            .HasConstraintName("FK_SupplierInvoices_CompanyID");

        builder.HasOne(d => d.Supplier)
            .WithMany(p => p.SupplierInvoices)
            .HasForeignKey(d => d.SupplierID)
            .OnDelete(DeleteBehavior.NoAction)
            .HasConstraintName("FK_SupplierInvoices_SupplierID");

        builder.HasOne(d => d.PurchaseOrder)
            .WithMany(p => p.SupplierInvoices)
            .HasForeignKey(d => d.PurchaseOrderID)
            .OnDelete(DeleteBehavior.SetNull)
            .HasConstraintName("FK_SupplierInvoices_PurchaseOrderID");

        builder.HasOne(d => d.CreatedByUser)
            .WithMany(p => p.SupplierInvoices)
            .HasForeignKey(d => d.CreatedBy)
            .OnDelete(DeleteBehavior.NoAction)
            .HasConstraintName("FK_SupplierInvoices_CreatedBy");
    }
}
