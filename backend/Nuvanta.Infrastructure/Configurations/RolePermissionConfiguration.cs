using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nuvanta.Domain.Entities;

namespace Nuvanta.Infrastructure.Configurations;

public class RolePermissionConfiguration : IEntityTypeConfiguration<RolePermission>
{
    public void Configure(EntityTypeBuilder<RolePermission> builder)
    {
        builder.ToTable("RolePermissions");

        builder.HasKey(e => new { e.RoleID, e.PermissionID });


        builder.HasOne(d => d.Role)
            .WithMany(p => p.RolePermissions)
            .HasForeignKey(d => d.RoleID)
            .OnDelete(DeleteBehavior.Cascade)
            .HasConstraintName("FK_RolePermissions_RoleID");

        builder.HasOne(d => d.Permission)
            .WithMany(p => p.RolePermissions)
            .HasForeignKey(d => d.PermissionID)
            .OnDelete(DeleteBehavior.Cascade)
            .HasConstraintName("FK_RolePermissions_PermissionID");
    }
}
