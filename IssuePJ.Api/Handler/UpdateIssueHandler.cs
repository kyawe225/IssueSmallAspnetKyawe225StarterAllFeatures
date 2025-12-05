using IssuePJ.Api.Command;
using IssuePJ.Api.Context;
using IssuePJ.Api.Exceptions;

namespace IssuePJ.Api.Handler;

public class UpdateIssueHandler
{
    private readonly ApplicationContext _context;
    public UpdateIssueHandler(ApplicationContext _context)
    {
        this._context = _context;
    }

    public async Task Handle(UpdateIssueCommand command)
    {
        var issue = await _context.Issues.FindAsync(command.IssueId);
        if (issue != null)
        {
            command.toIssue(issue,out issue);
            _context.Issues.Update(issue);
            await _context.SaveChangesAsync();
            return;
        }
        throw new NotFoundException("Not Found", "Issue");
    }
}
