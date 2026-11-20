using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nuvanta.Domain.Entities;

namespace Nuvanta.Infrastructure.Configurations;

public class CompanyConfiguration : IEntityTypeConfiguration<Company>
{
    public void Configure(EntityTypeBuilder<Company> builder)
    {
        builder.ToTable("Companies");

        builder.HasKey(e => e.CompanyID);

        builder.Property(e => e.CompanyID)
            .ValueGeneratedOnAdd();
        builder.Property(e => e.CompanyName)
            .HasMaxLength(150);
        builder.Property(e => e.TaxNumber)
            .HasMaxLength(50);
        builder.Property(e => e.Email)
            .HasMaxLength(150);
        builder.Property(e => e.Phone)
            .HasMaxLength(50);
        builder.Property(e => e.Street)
            .HasMaxLength(255);
        builder.Property(e => e.City)
            .HasMaxLength(100);
        builder.Property(e => e.Country)
            .HasMaxLength(100);
        builder.Property(e => e.CurrencyCode)
            .HasMaxLength(10);
        builder.Property(e => e.CreatedAt)
            .HasColumnType("datetime2(3)")
            .HasDefaultValueSql("SYSDATETIME()");
    }
}
