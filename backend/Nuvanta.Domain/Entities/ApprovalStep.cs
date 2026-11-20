namespace Nuvanta.Domain.Entities;

public class ApprovalStep
{
    public int ApprovalStepID { get; set; }
    public int ApprovalRequestID { get; set; }
    public int StepNumber { get; set; }
    public int? AssignedTo { get; set; }
    public int? RequiredRoleID { get; set; }
    public bool IsRequired { get; set; } = true;
    public string Status { get; set; } = string.Empty;
    public DateTime? DecisionDate { get; set; }

    public ApprovalRequest ApprovalRequest { get; set; } = null!;
    public User? AssignedToUser { get; set; }
    public Role? RequiredRole { get; set; }

    public ICollection<ApprovalAction> ApprovalActions { get; set; } = new List<ApprovalAction>();
}
