using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nuvanta.Domain.Entities;

namespace Nuvanta.Infrastructure.Configurations;

public class GoodsReceiptItemConfiguration : IEntityTypeConfiguration<GoodsReceiptItem>
{
    public void Configure(EntityTypeBuilder<GoodsReceiptItem> builder)
    {
        builder.ToTable("GoodsReceiptItems");

        builder.HasKey(e => e.GoodsReceiptItemID);

        builder.Property(e => e.GoodsReceiptItemID)
            .ValueGeneratedOnAdd();
        builder.Property(e => e.ReceivedQuantity)
            .HasPrecision(18, 4);
        builder.Property(e => e.AcceptedQuantity)
            .HasPrecision(18, 4);
        builder.Property(e => e.RejectedQuantity)
            .HasPrecision(18, 4);

        builder.HasOne(d => d.GoodsReceipt)
            .WithMany(p => p.GoodsReceiptItems)
            .HasForeignKey(d => d.GoodsReceiptID)
            .OnDelete(DeleteBehavior.Cascade)
            .HasConstraintName("FK_GoodsReceiptItems_GoodsReceiptID");

        builder.HasOne(d => d.PurchaseOrderItem)
            .WithMany(p => p.GoodsReceiptItems)
            .HasForeignKey(d => d.PurchaseOrderItemID)
            .OnDelete(DeleteBehavior.NoAction)
            .HasConstraintName("FK_GoodsReceiptItems_PurchaseOrderItemID");
    }
}
