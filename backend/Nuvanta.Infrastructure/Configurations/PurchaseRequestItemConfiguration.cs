using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nuvanta.Domain.Entities;

namespace Nuvanta.Infrastructure.Configurations;

public class PurchaseRequestItemConfiguration : IEntityTypeConfiguration<PurchaseRequestItem>
{
    public void Configure(EntityTypeBuilder<PurchaseRequestItem> builder)
    {
        builder.ToTable("PurchaseRequestItems");

        builder.HasKey(e => e.RequestItemID);

        builder.Property(e => e.RequestItemID)
            .ValueGeneratedOnAdd();
        builder.Property(e => e.Quantity)
            .HasPrecision(18, 4);
        builder.Property(e => e.EstimatedUnitPrice)
            .HasPrecision(18, 4);
        builder.Property(e => e.LineTotal)
            .HasComputedColumnSql("[Quantity] * [EstimatedUnitPrice]")
            .HasPrecision(18, 4);

        builder.HasOne(d => d.PurchaseRequest)
            .WithMany(p => p.PurchaseRequestItems)
            .HasForeignKey(d => d.PurchaseRequestID)
            .OnDelete(DeleteBehavior.Cascade)
            .HasConstraintName("FK_PurchaseRequestItems_PurchaseRequestID");

        builder.HasOne(d => d.Item)
            .WithMany(p => p.PurchaseRequestItems)
            .HasForeignKey(d => d.ItemID)
            .OnDelete(DeleteBehavior.SetNull)
            .HasConstraintName("FK_PurchaseRequestItems_ItemID");
    }
}
