using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nuvanta.Domain.Entities;

namespace Nuvanta.Infrastructure.Configurations;

public class BranchConfiguration : IEntityTypeConfiguration<Branch>
{
    public void Configure(EntityTypeBuilder<Branch> builder)
    {
        builder.ToTable("Branches");

        builder.HasKey(e => e.BranchID);

        builder.Property(e => e.BranchID)
            .ValueGeneratedOnAdd();
        builder.Property(e => e.BranchName)
            .HasMaxLength(150);
        builder.Property(e => e.Address)
            .HasMaxLength(500);
        builder.Property(e => e.Phone)
            .HasMaxLength(50);

        builder.HasOne(d => d.Company)
            .WithMany(p => p.Branches)
            .HasForeignKey(d => d.CompanyID)
            .OnDelete(DeleteBehavior.NoAction)
            .HasConstraintName("FK_Branches_CompanyID");
    }
}
