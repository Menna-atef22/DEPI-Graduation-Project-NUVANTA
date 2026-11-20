using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nuvanta.Domain.Entities;

namespace Nuvanta.Infrastructure.Configurations;

public class PermissionConfiguration : IEntityTypeConfiguration<Permission>
{
    public void Configure(EntityTypeBuilder<Permission> builder)
    {
        builder.ToTable("Permissions");

        builder.HasKey(e => e.PermissionID);

        builder.Property(e => e.PermissionID)
            .ValueGeneratedOnAdd();
        builder.Property(e => e.PermissionName)
            .HasMaxLength(150);

        builder.HasIndex(e => e.PermissionName).IsUnique().HasDatabaseName("UQ_Permissions_PermissionName");
    }
}
