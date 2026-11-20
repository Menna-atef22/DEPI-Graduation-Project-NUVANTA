using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nuvanta.Domain.Entities;

namespace Nuvanta.Infrastructure.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        // Table has an AFTER UPDATE trigger -> EF must not use OUTPUT clause
        builder.ToTable("Users", tb => { tb.HasTrigger("TR_Users_UpdatedAt"); });

        builder.HasKey(e => e.UserID);

        builder.Property(e => e.UserID)
            .ValueGeneratedOnAdd();
        builder.Property(e => e.FirstName)
            .HasMaxLength(50);
        builder.Property(e => e.LastName)
            .HasMaxLength(50);
        builder.Property(e => e.Email)
            .HasMaxLength(150);
        builder.Property(e => e.PasswordHash)
            .HasMaxLength(255);
        builder.Property(e => e.CreatedAt)
            .HasColumnType("datetime2(3)")
            .HasDefaultValueSql("SYSDATETIME()");
        builder.Property(e => e.UpdatedAt)
            .HasColumnType("datetime2(3)")
            .HasDefaultValueSql("SYSDATETIME()");
        builder.Property(e => e.LastLoginAt)
            .HasColumnType("datetime2(3)");

        builder.HasIndex(e => e.Email).IsUnique().HasDatabaseName("UQ_Users_Email");

        builder.HasOne(d => d.Company)
            .WithMany(p => p.Users)
            .HasForeignKey(d => d.CompanyID)
            .OnDelete(DeleteBehavior.NoAction)
            .HasConstraintName("FK_Users_CompanyID");

        builder.HasOne(d => d.Branch)
            .WithMany(p => p.Users)
            .HasForeignKey(d => d.BranchID)
            .OnDelete(DeleteBehavior.SetNull)
            .HasConstraintName("FK_Users_BranchID");

        builder.HasOne(d => d.Department)
            .WithMany(p => p.Users)
            .HasForeignKey(d => d.DepartmentID)
            .OnDelete(DeleteBehavior.NoAction)
            .HasConstraintName("FK_Users_DepartmentID");
    }
}
