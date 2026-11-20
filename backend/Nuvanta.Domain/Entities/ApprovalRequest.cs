namespace Nuvanta.Domain.Entities;

public class ApprovalRequest
{
    public int ApprovalRequestID { get; set; }
    public int ApprovalRuleID { get; set; }
    public int RequestedBy { get; set; }
    public string EntityType { get; set; } = string.Empty;
    public int EntityID { get; set; }
    public DateTime? SubmittedAt { get; set; }
    public string Status { get; set; } = string.Empty;

    public ApprovalRule ApprovalRule { get; set; } = null!;
    public User RequestedByUser { get; set; } = null!;

    public ICollection<ApprovalStep> ApprovalSteps { get; set; } = new List<ApprovalStep>();
}
