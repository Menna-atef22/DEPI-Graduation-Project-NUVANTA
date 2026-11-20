namespace Nuvanta.Domain.Entities;

public class AuditLog
{
    public int AuditLogID { get; set; }
    public int CompanyID { get; set; }
    public int? UserID { get; set; }
    public string ActionType { get; set; } = string.Empty;
    public string EntityType { get; set; } = string.Empty;
    public int? EntityID { get; set; }
    public string? OldValues { get; set; }
    public string? NewValues { get; set; }
    public string? IPAddress { get; set; }
    public string? UserAgent { get; set; }
    public string? CorrelationID { get; set; }
    public DateTime? CreatedAt { get; set; }

    public Company Company { get; set; } = null!;
    public User? User { get; set; }
}
