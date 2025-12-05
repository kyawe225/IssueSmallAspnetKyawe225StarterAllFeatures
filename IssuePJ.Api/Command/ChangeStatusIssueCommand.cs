using IssuePj.Api.Entities;

namespace IssuePJ.Api.Command;
public class ChangeStatusIssueCommand
{
    public string IssueId {set;get;}
    public string Status{set;get;}

    public void UpdateIssue(ref Issue issue)
    {
        issue.Status = Status;
    }
}