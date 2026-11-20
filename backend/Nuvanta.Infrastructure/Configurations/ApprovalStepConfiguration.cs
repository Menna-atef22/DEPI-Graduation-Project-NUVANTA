using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nuvanta.Domain.Entities;

namespace Nuvanta.Infrastructure.Configurations;

public class ApprovalStepConfiguration : IEntityTypeConfiguration<ApprovalStep>
{
    public void Configure(EntityTypeBuilder<ApprovalStep> builder)
    {
        builder.ToTable("ApprovalSteps");

        builder.HasKey(e => e.ApprovalStepID);

        builder.Property(e => e.ApprovalStepID)
            .ValueGeneratedOnAdd();
        builder.Property(e => e.Status)
            .HasMaxLength(50);
        builder.Property(e => e.DecisionDate)
            .HasColumnType("datetime2(3)");

        builder.HasOne(d => d.ApprovalRequest)
            .WithMany(p => p.ApprovalSteps)
            .HasForeignKey(d => d.ApprovalRequestID)
            .OnDelete(DeleteBehavior.Cascade)
            .HasConstraintName("FK_ApprovalSteps_ApprovalRequestID");

        builder.HasOne(d => d.AssignedToUser)
            .WithMany(p => p.ApprovalSteps)
            .HasForeignKey(d => d.AssignedTo)
            .OnDelete(DeleteBehavior.SetNull)
            .HasConstraintName("FK_ApprovalSteps_AssignedTo");

        builder.HasOne(d => d.RequiredRole)
            .WithMany(p => p.ApprovalSteps)
            .HasForeignKey(d => d.RequiredRoleID)
            .OnDelete(DeleteBehavior.SetNull)
            .HasConstraintName("FK_ApprovalSteps_RequiredRoleID");
    }
}
