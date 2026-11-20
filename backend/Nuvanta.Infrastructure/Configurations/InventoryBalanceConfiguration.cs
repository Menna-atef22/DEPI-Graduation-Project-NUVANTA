using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nuvanta.Domain.Entities;

namespace Nuvanta.Infrastructure.Configurations;

public class InventoryBalanceConfiguration : IEntityTypeConfiguration<InventoryBalance>
{
    public void Configure(EntityTypeBuilder<InventoryBalance> builder)
    {
        // Table has an AFTER UPDATE trigger -> EF must not use OUTPUT clause
        builder.ToTable("InventoryBalances", tb => { tb.HasTrigger("TR_InventoryBalances_LastUpdatedAt"); });

        builder.HasKey(e => e.InventoryBalanceID);

        builder.Property(e => e.InventoryBalanceID)
            .ValueGeneratedOnAdd();
        builder.Property(e => e.QuantityOnHand)
            .HasPrecision(18, 4);
        builder.Property(e => e.LastUpdatedAt)
            .HasColumnType("datetime2(3)")
            .HasDefaultValueSql("SYSDATETIME()");

        builder.HasIndex(e => new { e.ItemID, e.WarehouseID }).IsUnique().HasDatabaseName("UK_Item_Warehouse");

        builder.HasOne(d => d.Item)
            .WithMany(p => p.InventoryBalances)
            .HasForeignKey(d => d.ItemID)
            .OnDelete(DeleteBehavior.Cascade)
            .HasConstraintName("FK_InventoryBalances_ItemID");

        builder.HasOne(d => d.Warehouse)
            .WithMany(p => p.InventoryBalances)
            .HasForeignKey(d => d.WarehouseID)
            .OnDelete(DeleteBehavior.Cascade)
            .HasConstraintName("FK_InventoryBalances_WarehouseID");
    }
}
