using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nuvanta.Domain.Entities;

namespace Nuvanta.Infrastructure.Configurations;

public class ApprovalActionConfiguration : IEntityTypeConfiguration<ApprovalAction>
{
    public void Configure(EntityTypeBuilder<ApprovalAction> builder)
    {
        builder.ToTable("ApprovalActions");

        builder.HasKey(e => e.ApprovalActionID);

        builder.Property(e => e.ApprovalActionID)
            .ValueGeneratedOnAdd();
        builder.Property(e => e.ActionType)
            .HasMaxLength(50);
        builder.Property(e => e.ActionDate)
            .HasColumnType("datetime2(3)")
            .HasDefaultValueSql("SYSDATETIME()");

        builder.HasOne(d => d.ApprovalStep)
            .WithMany(p => p.ApprovalActions)
            .HasForeignKey(d => d.ApprovalStepID)
            .OnDelete(DeleteBehavior.Cascade)
            .HasConstraintName("FK_ApprovalActions_ApprovalStepID");

        builder.HasOne(d => d.ActionByUser)
            .WithMany(p => p.ApprovalActions)
            .HasForeignKey(d => d.ActionBy)
            .OnDelete(DeleteBehavior.NoAction)
            .HasConstraintName("FK_ApprovalActions_ActionBy");
    }
}
