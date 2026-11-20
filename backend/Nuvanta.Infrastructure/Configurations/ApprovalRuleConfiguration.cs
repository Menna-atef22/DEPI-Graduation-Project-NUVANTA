using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nuvanta.Domain.Entities;

namespace Nuvanta.Infrastructure.Configurations;

public class ApprovalRuleConfiguration : IEntityTypeConfiguration<ApprovalRule>
{
    public void Configure(EntityTypeBuilder<ApprovalRule> builder)
    {
        builder.ToTable("ApprovalRules");

        builder.HasKey(e => e.ApprovalRuleID);

        builder.Property(e => e.ApprovalRuleID)
            .ValueGeneratedOnAdd();
        builder.Property(e => e.RuleName)
            .HasMaxLength(150);
        builder.Property(e => e.EntityType)
            .HasMaxLength(100);
        builder.Property(e => e.MinAmount)
            .HasPrecision(18, 4);
        builder.Property(e => e.MaxAmount)
            .HasPrecision(18, 4);

        builder.HasOne(d => d.Company)
            .WithMany(p => p.ApprovalRules)
            .HasForeignKey(d => d.CompanyID)
            .OnDelete(DeleteBehavior.NoAction)
            .HasConstraintName("FK_ApprovalRules_CompanyID");
    }
}
