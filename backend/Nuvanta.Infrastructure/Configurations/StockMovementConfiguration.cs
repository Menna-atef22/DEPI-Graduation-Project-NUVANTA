using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nuvanta.Domain.Entities;

namespace Nuvanta.Infrastructure.Configurations;

public class StockMovementConfiguration : IEntityTypeConfiguration<StockMovement>
{
    public void Configure(EntityTypeBuilder<StockMovement> builder)
    {
        builder.ToTable("StockMovements");

        builder.HasKey(e => e.StockMovementID);

        builder.Property(e => e.StockMovementID)
            .ValueGeneratedOnAdd();
        builder.Property(e => e.MovementType)
            .HasMaxLength(50);
        builder.Property(e => e.Quantity)
            .HasPrecision(18, 4);
        builder.Property(e => e.UnitCost)
            .HasPrecision(18, 4);
        builder.Property(e => e.MovementDate)
            .HasColumnType("datetime2(3)")
            .HasDefaultValueSql("SYSDATETIME()");
        builder.Property(e => e.ReferenceType)
            .HasMaxLength(100);

        builder.HasOne(d => d.Item)
            .WithMany(p => p.StockMovements)
            .HasForeignKey(d => d.ItemID)
            .OnDelete(DeleteBehavior.NoAction)
            .HasConstraintName("FK_StockMovements_ItemID");

        builder.HasOne(d => d.Warehouse)
            .WithMany(p => p.StockMovements)
            .HasForeignKey(d => d.WarehouseID)
            .OnDelete(DeleteBehavior.NoAction)
            .HasConstraintName("FK_StockMovements_WarehouseID");

        builder.HasOne(d => d.GoodsReceiptItem)
            .WithMany(p => p.StockMovements)
            .HasForeignKey(d => d.GoodsReceiptItemID)
            .OnDelete(DeleteBehavior.SetNull)
            .HasConstraintName("FK_StockMovements_GoodsReceiptItemID");

        builder.HasOne(d => d.CreatedByUser)
            .WithMany(p => p.StockMovements)
            .HasForeignKey(d => d.CreatedBy)
            .OnDelete(DeleteBehavior.NoAction)
            .HasConstraintName("FK_StockMovements_CreatedBy");
    }
}
