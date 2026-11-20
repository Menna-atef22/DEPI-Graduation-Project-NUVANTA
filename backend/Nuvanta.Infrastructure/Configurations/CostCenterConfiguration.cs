using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nuvanta.Domain.Entities;

namespace Nuvanta.Infrastructure.Configurations;

public class CostCenterConfiguration : IEntityTypeConfiguration<CostCenter>
{
    public void Configure(EntityTypeBuilder<CostCenter> builder)
    {
        builder.ToTable("CostCenters");

        builder.HasKey(e => e.CostCenterID);

        builder.Property(e => e.CostCenterID)
            .ValueGeneratedOnAdd();
        builder.Property(e => e.CostCenterName)
            .HasMaxLength(150);

        builder.HasOne(d => d.Company)
            .WithMany(p => p.CostCenters)
            .HasForeignKey(d => d.CompanyID)
            .OnDelete(DeleteBehavior.NoAction)
            .HasConstraintName("FK_CostCenters_CompanyID");

        builder.HasOne(d => d.Department)
            .WithMany(p => p.CostCenters)
            .HasForeignKey(d => d.DepartmentID)
            .OnDelete(DeleteBehavior.SetNull)
            .HasConstraintName("FK_CostCenters_DepartmentID");
    }
}
