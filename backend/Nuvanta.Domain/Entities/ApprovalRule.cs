namespace Nuvanta.Domain.Entities;

public class ApprovalRule
{
    public int ApprovalRuleID { get; set; }
    public int CompanyID { get; set; }
    public string RuleName { get; set; } = string.Empty;
    public string EntityType { get; set; } = string.Empty;
    public decimal MinAmount { get; set; }
    public decimal? MaxAmount { get; set; }
    public int Priority { get; set; }
    public bool IsActive { get; set; } = true;

    public Company Company { get; set; } = null!;

    public ICollection<ApprovalRequest> ApprovalRequests { get; set; } = new List<ApprovalRequest>();
}
