using IssuePJ.Api.Command;
using IssuePJ.Api.Context;

namespace IssuePJ.Api.Handler;

public class ChangeStatusIssueHandler(ApplicationContext _context)
{
    public async Task Handle(ChangeStatusIssueCommand command, CancellationToken cancellationToken)
    {
        var issue = _context.Issues.Where(p=> p.Id == command.IssueId).FirstOrDefault();
        if (issue == null)
        {
            throw new Exception("Issue not found");
        }
        command.UpdateIssue(ref issue);
        _context.Issues.Update(issue);
        await _context.SaveChangesAsync(cancellationToken);
        await Task.CompletedTask;
    }
}