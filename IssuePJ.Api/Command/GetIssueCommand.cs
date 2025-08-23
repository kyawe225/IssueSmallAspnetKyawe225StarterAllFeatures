namespace IssuePJ.Api.Command;

public class GetIssueCommand
{
    public string IssueId { set; get; }
    public GetIssueCommand()
    {

    }

    public GetIssueCommand(string id)
    {
        IssueId = id;
    }
}