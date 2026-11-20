using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nuvanta.Domain.Entities;

namespace Nuvanta.Infrastructure.Configurations;

public class UserRoleConfiguration : IEntityTypeConfiguration<UserRole>
{
    public void Configure(EntityTypeBuilder<UserRole> builder)
    {
        builder.ToTable("UserRoles");

        builder.HasKey(e => new { e.UserID, e.RoleID });

        builder.Property(e => e.AssignedAt)
            .HasColumnType("datetime2(3)")
            .HasDefaultValueSql("SYSDATETIME()");

        builder.HasOne(d => d.User)
            .WithMany(p => p.UserRoles)
            .HasForeignKey(d => d.UserID)
            .OnDelete(DeleteBehavior.Cascade)
            .HasConstraintName("FK_UserRoles_UserID");

        builder.HasOne(d => d.Role)
            .WithMany(p => p.UserRoles)
            .HasForeignKey(d => d.RoleID)
            .OnDelete(DeleteBehavior.Cascade)
            .HasConstraintName("FK_UserRoles_RoleID");
    }
}
