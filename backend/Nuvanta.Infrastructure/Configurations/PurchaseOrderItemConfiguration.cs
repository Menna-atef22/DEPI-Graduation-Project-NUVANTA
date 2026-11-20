using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nuvanta.Domain.Entities;

namespace Nuvanta.Infrastructure.Configurations;

public class PurchaseOrderItemConfiguration : IEntityTypeConfiguration<PurchaseOrderItem>
{
    public void Configure(EntityTypeBuilder<PurchaseOrderItem> builder)
    {
        builder.ToTable("PurchaseOrderItems");

        builder.HasKey(e => e.PurchaseOrderItemID);

        builder.Property(e => e.PurchaseOrderItemID)
            .ValueGeneratedOnAdd();
        builder.Property(e => e.Quantity)
            .HasPrecision(18, 4);
        builder.Property(e => e.UnitPrice)
            .HasPrecision(18, 4);
        builder.Property(e => e.LineTotal)
            .HasComputedColumnSql("[Quantity] * [UnitPrice]")
            .HasPrecision(18, 4);
        builder.Property(e => e.ReceivedQuantity)
            .HasComputedColumnSql("[dbo].[fn_POItemReceivedQty]([PurchaseOrderItemID])")
            .HasPrecision(18, 4);

        builder.HasOne(d => d.PurchaseOrder)
            .WithMany(p => p.PurchaseOrderItems)
            .HasForeignKey(d => d.PurchaseOrderID)
            .OnDelete(DeleteBehavior.Cascade)
            .HasConstraintName("FK_PurchaseOrderItems_PurchaseOrderID");

        builder.HasOne(d => d.RequestItem)
            .WithMany(p => p.PurchaseOrderItems)
            .HasForeignKey(d => d.RequestItemID)
            .OnDelete(DeleteBehavior.NoAction)
            .HasConstraintName("FK_PurchaseOrderItems_RequestItemID");

        builder.HasOne(d => d.Item)
            .WithMany(p => p.PurchaseOrderItems)
            .HasForeignKey(d => d.ItemID)
            .OnDelete(DeleteBehavior.NoAction)
            .HasConstraintName("FK_PurchaseOrderItems_ItemID");
    }
}
