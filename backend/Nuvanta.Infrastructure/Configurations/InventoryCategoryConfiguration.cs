using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nuvanta.Domain.Entities;

namespace Nuvanta.Infrastructure.Configurations;

public class InventoryCategoryConfiguration : IEntityTypeConfiguration<InventoryCategory>
{
    public void Configure(EntityTypeBuilder<InventoryCategory> builder)
    {
        builder.ToTable("InventoryCategories");

        builder.HasKey(e => e.InventoryCategoryID);

        builder.Property(e => e.InventoryCategoryID)
            .ValueGeneratedOnAdd();
        builder.Property(e => e.CategoryName)
            .HasMaxLength(150);

        builder.HasOne(d => d.Company)
            .WithMany(p => p.InventoryCategories)
            .HasForeignKey(d => d.CompanyID)
            .OnDelete(DeleteBehavior.NoAction)
            .HasConstraintName("FK_InventoryCategories_CompanyID");
    }
}
