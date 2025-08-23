using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using IssuePJ.Api.Command;
using IssuePJ.Api.Context;
using Wolverine;

namespace IssuePJ.Api.Handler
{
    public class CreateIssueHandler
    {
        private readonly ApplicationContext _context;
        public CreateIssueHandler(ApplicationContext context)
        {
            _context = context;
        }
        public async Task Handle(CreateIssueCommand command, CancellationToken cancellationToken)
        {
            var issue = command.toIssue();
            _context.Issues.Add(issue);
            await _context.SaveChangesAsync(cancellationToken);
            await Task.CompletedTask;
        }
    }
}