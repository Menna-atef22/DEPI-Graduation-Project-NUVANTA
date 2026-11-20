using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nuvanta.Domain.Entities;

namespace Nuvanta.Infrastructure.Configurations;

public class AuditLogConfiguration : IEntityTypeConfiguration<AuditLog>
{
    public void Configure(EntityTypeBuilder<AuditLog> builder)
    {
        builder.ToTable("AuditLogs");

        builder.HasKey(e => e.AuditLogID);

        builder.Property(e => e.AuditLogID)
            .ValueGeneratedOnAdd();
        builder.Property(e => e.ActionType)
            .HasMaxLength(50);
        builder.Property(e => e.EntityType)
            .HasMaxLength(100);
        builder.Property(e => e.IPAddress)
            .HasMaxLength(45);
        builder.Property(e => e.CorrelationID)
            .HasMaxLength(100);
        builder.Property(e => e.CreatedAt)
            .HasColumnType("datetime2(3)")
            .HasDefaultValueSql("SYSDATETIME()");

        builder.HasOne(d => d.Company)
            .WithMany(p => p.AuditLogs)
            .HasForeignKey(d => d.CompanyID)
            .OnDelete(DeleteBehavior.NoAction)
            .HasConstraintName("FK_AuditLogs_CompanyID");

        builder.HasOne(d => d.User)
            .WithMany(p => p.AuditLogs)
            .HasForeignKey(d => d.UserID)
            .OnDelete(DeleteBehavior.SetNull)
            .HasConstraintName("FK_AuditLogs_UserID");
    }
}
