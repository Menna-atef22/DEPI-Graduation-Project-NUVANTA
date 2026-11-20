using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nuvanta.Domain.Entities;

namespace Nuvanta.Infrastructure.Configurations;

public class RoleConfiguration : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.ToTable("Roles");

        builder.HasKey(e => e.RoleID);

        builder.Property(e => e.RoleID)
            .ValueGeneratedOnAdd();
        builder.Property(e => e.RoleName)
            .HasMaxLength(100);

        builder.HasOne(d => d.Company)
            .WithMany(p => p.Roles)
            .HasForeignKey(d => d.CompanyID)
            .OnDelete(DeleteBehavior.NoAction)
            .HasConstraintName("FK_Roles_CompanyID");
    }
}
