using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nuvanta.Domain.Entities;

namespace Nuvanta.Infrastructure.Configurations;

public class AccountConfiguration : IEntityTypeConfiguration<Account>
{
    public void Configure(EntityTypeBuilder<Account> builder)
    {
        builder.ToTable("Accounts");

        builder.HasKey(e => e.AccountID);

        builder.Property(e => e.AccountID)
            .ValueGeneratedOnAdd();
        builder.Property(e => e.AccountCode)
            .HasMaxLength(50);
        builder.Property(e => e.AccountName)
            .HasMaxLength(150);
        builder.Property(e => e.AccountType)
            .HasMaxLength(50);

        builder.HasIndex(e => new { e.CompanyID, e.AccountCode }).IsUnique().HasDatabaseName("UK_Company_AccountCode");

        builder.HasOne(d => d.Company)
            .WithMany(p => p.Accounts)
            .HasForeignKey(d => d.CompanyID)
            .OnDelete(DeleteBehavior.NoAction)
            .HasConstraintName("FK_Accounts_CompanyID");

        builder.HasOne(d => d.ParentAccount)
            .WithMany(p => p.ChildAccounts)
            .HasForeignKey(d => d.ParentAccountID)
            .OnDelete(DeleteBehavior.NoAction)
            .HasConstraintName("FK_Accounts_ParentAccountID");
    }
}
