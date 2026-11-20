using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nuvanta.Domain.Entities;

namespace Nuvanta.Infrastructure.Configurations;

public class FinancialPeriodConfiguration : IEntityTypeConfiguration<FinancialPeriod>
{
    public void Configure(EntityTypeBuilder<FinancialPeriod> builder)
    {
        builder.ToTable("FinancialPeriods");

        builder.HasKey(e => e.FinancialPeriodID);

        builder.Property(e => e.FinancialPeriodID)
            .ValueGeneratedOnAdd();
        builder.Property(e => e.PeriodName)
            .HasMaxLength(100);
        builder.Property(e => e.StartDate)
            .HasColumnType("date");
        builder.Property(e => e.EndDate)
            .HasColumnType("date");
        builder.Property(e => e.Status)
            .HasMaxLength(50);

        builder.HasOne(d => d.Company)
            .WithMany(p => p.FinancialPeriods)
            .HasForeignKey(d => d.CompanyID)
            .OnDelete(DeleteBehavior.NoAction)
            .HasConstraintName("FK_FinancialPeriods_CompanyID");
    }
}
