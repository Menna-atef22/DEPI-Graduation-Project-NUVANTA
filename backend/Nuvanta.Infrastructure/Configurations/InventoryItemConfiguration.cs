using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nuvanta.Domain.Entities;

namespace Nuvanta.Infrastructure.Configurations;

public class InventoryItemConfiguration : IEntityTypeConfiguration<InventoryItem>
{
    public void Configure(EntityTypeBuilder<InventoryItem> builder)
    {
        builder.ToTable("InventoryItems");

        builder.HasKey(e => e.ItemID);

        builder.Property(e => e.ItemID)
            .ValueGeneratedOnAdd();
        builder.Property(e => e.ItemName)
            .HasMaxLength(150);
        builder.Property(e => e.SKU)
            .HasMaxLength(100);
        builder.Property(e => e.UnitOfMeasure)
            .HasMaxLength(50);
        builder.Property(e => e.MinimumStockLevel)
            .HasPrecision(18, 4)
            .HasDefaultValue(0m);

        builder.HasIndex(e => new { e.CompanyID, e.SKU }).IsUnique().HasDatabaseName("UK_Company_SKU");

        builder.HasOne(d => d.Company)
            .WithMany(p => p.InventoryItems)
            .HasForeignKey(d => d.CompanyID)
            .OnDelete(DeleteBehavior.NoAction)
            .HasConstraintName("FK_InventoryItems_CompanyID");

        builder.HasOne(d => d.InventoryCategory)
            .WithMany(p => p.InventoryItems)
            .HasForeignKey(d => d.InventoryCategoryID)
            .OnDelete(DeleteBehavior.SetNull)
            .HasConstraintName("FK_InventoryItems_InventoryCategoryID");
    }
}
