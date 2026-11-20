using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nuvanta.Domain.Entities;

namespace Nuvanta.Infrastructure.Configurations;

public class WarehouseConfiguration : IEntityTypeConfiguration<Warehouse>
{
    public void Configure(EntityTypeBuilder<Warehouse> builder)
    {
        builder.ToTable("Warehouses");

        builder.HasKey(e => e.WarehouseID);

        builder.Property(e => e.WarehouseID)
            .ValueGeneratedOnAdd();
        builder.Property(e => e.WarehouseName)
            .HasMaxLength(150);
        builder.Property(e => e.Location)
            .HasMaxLength(500);
        builder.Property(e => e.Status)
            .HasMaxLength(50);

        builder.HasOne(d => d.Company)
            .WithMany(p => p.Warehouses)
            .HasForeignKey(d => d.CompanyID)
            .OnDelete(DeleteBehavior.NoAction)
            .HasConstraintName("FK_Warehouses_CompanyID");

        builder.HasOne(d => d.Branch)
            .WithMany(p => p.Warehouses)
            .HasForeignKey(d => d.BranchID)
            .OnDelete(DeleteBehavior.SetNull)
            .HasConstraintName("FK_Warehouses_BranchID");
    }
}
