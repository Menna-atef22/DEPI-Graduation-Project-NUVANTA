using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nuvanta.Domain.Entities;

namespace Nuvanta.Infrastructure.Configurations;

public class GoodsReceiptConfiguration : IEntityTypeConfiguration<GoodsReceipt>
{
    public void Configure(EntityTypeBuilder<GoodsReceipt> builder)
    {
        builder.ToTable("GoodsReceipts");

        builder.HasKey(e => e.GoodsReceiptID);

        builder.Property(e => e.GoodsReceiptID)
            .ValueGeneratedOnAdd();
        builder.Property(e => e.ReceiptNumber)
            .HasMaxLength(100);
        builder.Property(e => e.ReceiptDate)
            .HasColumnType("date");
        builder.Property(e => e.Status)
            .HasMaxLength(50);

        builder.HasOne(d => d.PurchaseOrder)
            .WithMany(p => p.GoodsReceipts)
            .HasForeignKey(d => d.PurchaseOrderID)
            .OnDelete(DeleteBehavior.NoAction)
            .HasConstraintName("FK_GoodsReceipts_PurchaseOrderID");

        builder.HasOne(d => d.Warehouse)
            .WithMany(p => p.GoodsReceipts)
            .HasForeignKey(d => d.WarehouseID)
            .OnDelete(DeleteBehavior.NoAction)
            .HasConstraintName("FK_GoodsReceipts_WarehouseID");

        builder.HasOne(d => d.ReceivedByUser)
            .WithMany(p => p.GoodsReceipts)
            .HasForeignKey(d => d.ReceivedBy)
            .OnDelete(DeleteBehavior.NoAction)
            .HasConstraintName("FK_GoodsReceipts_ReceivedBy");
    }
}
