using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nuvanta.Domain.Entities;

namespace Nuvanta.Infrastructure.Configurations;

public class SupplierConfiguration : IEntityTypeConfiguration<Supplier>
{
    public void Configure(EntityTypeBuilder<Supplier> builder)
    {
        builder.ToTable("Suppliers");

        builder.HasKey(e => e.SupplierID);

        builder.Property(e => e.SupplierID)
            .ValueGeneratedOnAdd();
        builder.Property(e => e.SupplierName)
            .HasMaxLength(150);
        builder.Property(e => e.TaxNumber)
            .HasMaxLength(50);
        builder.Property(e => e.Email)
            .HasMaxLength(150);
        builder.Property(e => e.Phone)
            .HasMaxLength(50);
        builder.Property(e => e.Address)
            .HasMaxLength(500);
        builder.Property(e => e.Status)
            .HasMaxLength(50);
        builder.Property(e => e.CreatedAt)
            .HasColumnType("datetime2(3)")
            .HasDefaultValueSql("SYSDATETIME()");

        builder.HasOne(d => d.Company)
            .WithMany(p => p.Suppliers)
            .HasForeignKey(d => d.CompanyID)
            .OnDelete(DeleteBehavior.NoAction)
            .HasConstraintName("FK_Suppliers_CompanyID");
    }
}
