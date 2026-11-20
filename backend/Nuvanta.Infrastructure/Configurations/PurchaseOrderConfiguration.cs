using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nuvanta.Domain.Entities;

namespace Nuvanta.Infrastructure.Configurations;

public class PurchaseOrderConfiguration : IEntityTypeConfiguration<PurchaseOrder>
{
    public void Configure(EntityTypeBuilder<PurchaseOrder> builder)
    {
        builder.ToTable("PurchaseOrders");

        builder.HasKey(e => e.PurchaseOrderID);

        builder.Property(e => e.PurchaseOrderID)
            .ValueGeneratedOnAdd();
        builder.Property(e => e.OrderDate)
            .HasColumnType("date");
        builder.Property(e => e.ExpectedDeliveryDate)
            .HasColumnType("date");
        builder.Property(e => e.DeliveryAddress)
            .HasMaxLength(500);
        builder.Property(e => e.CurrencyCode)
            .HasMaxLength(10);
        builder.Property(e => e.Status)
            .HasMaxLength(50);
        builder.Property(e => e.TotalAmount)
            .HasPrecision(18, 4);

        builder.HasOne(d => d.Company)
            .WithMany(p => p.PurchaseOrders)
            .HasForeignKey(d => d.CompanyID)
            .OnDelete(DeleteBehavior.NoAction)
            .HasConstraintName("FK_PurchaseOrders_CompanyID");

        builder.HasOne(d => d.Supplier)
            .WithMany(p => p.PurchaseOrders)
            .HasForeignKey(d => d.SupplierID)
            .OnDelete(DeleteBehavior.NoAction)
            .HasConstraintName("FK_PurchaseOrders_SupplierID");

        builder.HasOne(d => d.PurchaseRequest)
            .WithMany(p => p.PurchaseOrders)
            .HasForeignKey(d => d.PurchaseRequestID)
            .OnDelete(DeleteBehavior.SetNull)
            .HasConstraintName("FK_PurchaseOrders_PurchaseRequestID");

        builder.HasOne(d => d.CreatedByUser)
            .WithMany(p => p.PurchaseOrders)
            .HasForeignKey(d => d.CreatedBy)
            .OnDelete(DeleteBehavior.NoAction)
            .HasConstraintName("FK_PurchaseOrders_CreatedBy");
    }
}
