using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nuvanta.Domain.Entities;

namespace Nuvanta.Infrastructure.Configurations;

public class DepartmentConfiguration : IEntityTypeConfiguration<Department>
{
    public void Configure(EntityTypeBuilder<Department> builder)
    {
        builder.ToTable("Departments");

        builder.HasKey(e => e.DepartmentID);

        builder.Property(e => e.DepartmentID)
            .ValueGeneratedOnAdd();
        builder.Property(e => e.DepartmentName)
            .HasMaxLength(150);

        builder.HasOne(d => d.Company)
            .WithMany(p => p.Departments)
            .HasForeignKey(d => d.CompanyID)
            .OnDelete(DeleteBehavior.NoAction)
            .HasConstraintName("FK_Departments_CompanyID");

        builder.HasOne(d => d.Branch)
            .WithMany(p => p.Departments)
            .HasForeignKey(d => d.BranchID)
            .OnDelete(DeleteBehavior.SetNull)
            .HasConstraintName("FK_Departments_BranchID");

        builder.HasOne(d => d.ParentDepartment)
            .WithMany(p => p.ChildDepartments)
            .HasForeignKey(d => d.ParentDepartmentID)
            .OnDelete(DeleteBehavior.NoAction)
            .HasConstraintName("FK_Departments_ParentDepartmentID");
    }
}
