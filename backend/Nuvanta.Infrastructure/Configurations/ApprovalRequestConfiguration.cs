using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nuvanta.Domain.Entities;

namespace Nuvanta.Infrastructure.Configurations;

public class ApprovalRequestConfiguration : IEntityTypeConfiguration<ApprovalRequest>
{
    public void Configure(EntityTypeBuilder<ApprovalRequest> builder)
    {
        builder.ToTable("ApprovalRequests");

        builder.HasKey(e => e.ApprovalRequestID);

        builder.Property(e => e.ApprovalRequestID)
            .ValueGeneratedOnAdd();
        builder.Property(e => e.EntityType)
            .HasMaxLength(100);
        builder.Property(e => e.SubmittedAt)
            .HasColumnType("datetime2(3)")
            .HasDefaultValueSql("SYSDATETIME()");
        builder.Property(e => e.Status)
            .HasMaxLength(50);

        builder.HasOne(d => d.ApprovalRule)
            .WithMany(p => p.ApprovalRequests)
            .HasForeignKey(d => d.ApprovalRuleID)
            .OnDelete(DeleteBehavior.NoAction)
            .HasConstraintName("FK_ApprovalRequests_ApprovalRuleID");

        builder.HasOne(d => d.RequestedByUser)
            .WithMany(p => p.ApprovalRequests)
            .HasForeignKey(d => d.RequestedBy)
            .OnDelete(DeleteBehavior.NoAction)
            .HasConstraintName("FK_ApprovalRequests_RequestedBy");
    }
}
