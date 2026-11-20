namespace Nuvanta.Domain.Entities;

public class ApprovalAction
{
    public int ApprovalActionID { get; set; }
    public int ApprovalStepID { get; set; }
    public int ActionBy { get; set; }
    public string ActionType { get; set; } = string.Empty;
    public DateTime? ActionDate { get; set; }
    public string? Comments { get; set; }

    public ApprovalStep ApprovalStep { get; set; } = null!;
    public User ActionByUser { get; set; } = null!;
}
