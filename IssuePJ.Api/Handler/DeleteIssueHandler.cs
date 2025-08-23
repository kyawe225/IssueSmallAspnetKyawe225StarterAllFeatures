using IssuePJ.Api.Command;
using IssuePJ.Api.Context;
using IssuePJ.Api.Exceptions;

namespace IssuePJ.Api.Handler;

public class DeleteIssueHandler
{
    private readonly ApplicationContext _context;
    public DeleteIssueHandler(ApplicationContext _context)
    {
        this._context = _context;
    }

    public async Task Handle(DeleteIssueCommand command)
    {
        var issue = await _context.Issues.FindAsync(command.IssueId);
        if (issue != null)
        {
            _context.Issues.Remove(issue);
            await _context.SaveChangesAsync();
        }
        throw new NotFoundException("Not Found", "Issue");
    }
}
