using System;
using System.ComponentModel.DataAnnotations;
using IssuePj.Api.Entities;

namespace IssuePJ.Api.Command;

public class UpdateIssueCommand
{
    public string? IssueId { get; set; }
    [Required]
    public string Title { get; set; }
    [Required]
    public string Description { get; set; }
    [Required]
    public string Status { get; set; }
    [Required]
    public DateTime DueDate { get; set; }

    public UpdateIssueCommand()
    {

    }
    public UpdateIssueCommand(string id)
    {
        IssueId = id;
    }

    public void toIssue(Issue tempIssue,out Issue issue)
    {
        issue = tempIssue;
        issue.Id = IssueId;
        issue.Title = Title;
        issue.Description = Description;
        issue.Status = Status;
        issue.CreatedAt = DateTime.UtcNow;
        issue.DueDate = DueDate.ToUniversalTime();
        // issue.AssignedAt = DateTime.MinValue.ToUniversalTime()
    }
}
