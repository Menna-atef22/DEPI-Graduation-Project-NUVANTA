using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nuvanta.Domain.Entities;

namespace Nuvanta.Infrastructure.Configurations;

public class CashAccountConfiguration : IEntityTypeConfiguration<CashAccount>
{
    public void Configure(EntityTypeBuilder<CashAccount> builder)
    {
        builder.ToTable("CashAccounts");

        builder.HasKey(e => e.CashAccountID);

        builder.Property(e => e.CashAccountID)
            .ValueGeneratedOnAdd();
        builder.Property(e => e.AccountName)
            .HasMaxLength(150);
        builder.Property(e => e.AccountType)
            .HasMaxLength(50);
        builder.Property(e => e.CurrencyCode)
            .HasMaxLength(10);
        builder.Property(e => e.OpeningBalance)
            .HasPrecision(18, 4);

        builder.HasOne(d => d.Company)
            .WithMany(p => p.CashAccounts)
            .HasForeignKey(d => d.CompanyID)
            .OnDelete(DeleteBehavior.NoAction)
            .HasConstraintName("FK_CashAccounts_CompanyID");
    }
}
