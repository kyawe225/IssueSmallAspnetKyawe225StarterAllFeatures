using System;

namespace IssuePJ.Api.Command;

public class DeleteIssueCommand
{
    public string IssueId { get; set; }

    public DeleteIssueCommand()
    {

    }
    public DeleteIssueCommand(string id)
    {
        IssueId = id;
    }
}
