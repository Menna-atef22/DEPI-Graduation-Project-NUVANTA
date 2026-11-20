using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nuvanta.Domain.Entities;

namespace Nuvanta.Infrastructure.Configurations;

public class ExpenseCategoryConfiguration : IEntityTypeConfiguration<ExpenseCategory>
{
    public void Configure(EntityTypeBuilder<ExpenseCategory> builder)
    {
        builder.ToTable("ExpenseCategories");

        builder.HasKey(e => e.CategoryID);

        builder.Property(e => e.CategoryID)
            .ValueGeneratedOnAdd();
        builder.Property(e => e.CategoryName)
            .HasMaxLength(150);

        builder.HasOne(d => d.Company)
            .WithMany(p => p.ExpenseCategories)
            .HasForeignKey(d => d.CompanyID)
            .OnDelete(DeleteBehavior.NoAction)
            .HasConstraintName("FK_ExpenseCategories_CompanyID");
    }
}
